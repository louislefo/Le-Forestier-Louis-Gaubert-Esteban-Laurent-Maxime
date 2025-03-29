using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les statistiques
    /// </summary>
    public class ModuleStatistiques
    {
        private string connectionString;

        public ModuleStatistiques(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// affiche le nombre de livraisons par cuisinier
        /// </summary>
        public void AfficherLivraisonsParCuisinier()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT u.nom, u.prenom, COUNT(*) as nombre_livraisons " +
                           "FROM commande co " +
                           "INNER JOIN utilisateur u ON co.id_cuisinier = u.id_utilisateur " +
                           "GROUP BY u.id_utilisateur, u.nom, u.prenom " +
                           "ORDER BY nombre_livraisons DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nNombre de livraisons par cuisinier :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Cuisinier: {reader["prenom"]} {reader["nom"]}");
                        Console.WriteLine($"Nombre de livraisons: {reader["nombre_livraisons"]}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
        }

        /// <summary>
        /// affiche les commandes selon une periode
        /// </summary>
        public void AfficherCommandesParPeriode(DateTime dateDebut, DateTime dateFin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT co.*, u.nom as nom_client, u.prenom as prenom_client, " +
                           "p.nom_plat, cu.nom as nom_cuisinier, cu.prenom as prenom_cuisinier " +
                           "FROM commande co " +
                           "INNER JOIN utilisateur u ON co.id_client = u.id_utilisateur " +
                           "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                           "INNER JOIN utilisateur cu ON co.id_cuisinier = cu.id_utilisateur " +
                           "WHERE co.date_commande BETWEEN @dateDebut AND @dateFin " +
                           "ORDER BY co.date_commande";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@dateFin", dateFin);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nCommandes par periode :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Commande #{reader["id_commande"]}");
                        Console.WriteLine($"Date: {reader["date_commande"]}");
                        Console.WriteLine($"Client: {reader["prenom_client"]} {reader["nom_client"]}");
                        Console.WriteLine($"Cuisinier: {reader["prenom_cuisinier"]} {reader["nom_cuisinier"]}");
                        Console.WriteLine($"Plat: {reader["nom_plat"]}");
                        Console.WriteLine($"Prix: {reader["prix_total"]}€");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
        }

        /// <summary>
        /// affiche la moyenne des prix des commandes
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT AVG(prix_total) as moyenne_prix FROM commande";

                SqlCommand cmd = new SqlCommand(sql, conn);
                double moyenne = Convert.ToDouble(cmd.ExecuteScalar());

                Console.WriteLine("\nMoyenne des prix des commandes :");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Prix moyen: {moyenne:F2}€");
                Console.WriteLine("----------------------------------------");
            }
        }

        /// <summary>
        /// affiche la moyenne des comptes clients
        /// </summary>
        public void AfficherMoyenneComptesClients()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT AVG(prix_total) as moyenne_compte " +
                           "FROM commande co " +
                           "GROUP BY co.id_client";

                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    double somme = 0;
                    int nombreClients = 0;

                    while (reader.Read())
                    {
                        somme += Convert.ToDouble(reader["moyenne_compte"]);
                        nombreClients++;
                    }

                    double moyenne = nombreClients > 0 ? somme / nombreClients : 0;

                    Console.WriteLine("\nMoyenne des comptes clients :");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"Compte moyen par client: {moyenne:F2}€");
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        /// <summary>
        /// affiche les commandes par type de plat et periode
        /// </summary>
        public void AfficherCommandesParTypePlat(DateTime dateDebut, DateTime dateFin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT p.type_plat, COUNT(*) as nombre_commandes " +
                           "FROM commande co " +
                           "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                           "WHERE co.date_commande BETWEEN @dateDebut AND @dateFin " +
                           "GROUP BY p.type_plat " +
                           "ORDER BY nombre_commandes DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@dateFin", dateFin);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nCommandes par type de plat :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Type de plat: {reader["type_plat"]}");
                        Console.WriteLine($"Nombre de commandes: {reader["nombre_commandes"]}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
        }

        /// <summary>
        /// affiche les statistiques creatives
        /// </summary>
        public void AfficherStatistiquesCreatives()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Les plats les plus commandes par jour de la semaine
                string sql1 = "SELECT DATENAME(WEEKDAY, date_commande) as jour, " +
                            "p.nom_plat, COUNT(*) as nombre_commandes " +
                            "FROM commande co " +
                            "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                            "GROUP BY DATENAME(WEEKDAY, date_commande), p.nom_plat " +
                            "ORDER BY jour, nombre_commandes DESC";

                // 2. Les cuisiniers les plus populaires (note moyenne)
                string sql2 = "SELECT u.nom, u.prenom, AVG(co.note) as note_moyenne " +
                            "FROM commande co " +
                            "INNER JOIN utilisateur u ON co.id_cuisinier = u.id_utilisateur " +
                            "WHERE co.note IS NOT NULL " +
                            "GROUP BY u.id_utilisateur, u.nom, u.prenom " +
                            "ORDER BY note_moyenne DESC";

                // 3. Les clients les plus fideles (nombre de commandes)
                string sql3 = "SELECT u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                            "FROM commande co " +
                            "INNER JOIN utilisateur u ON co.id_client = u.id_utilisateur " +
                            "GROUP BY u.id_utilisateur, u.nom, u.prenom " +
                            "ORDER BY nombre_commandes DESC";

                // 4. Les plats les plus rentables (prix moyen)
                string sql4 = "SELECT p.nom_plat, AVG(co.prix_total) as prix_moyen " +
                            "FROM commande co " +
                            "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                            "GROUP BY p.id_plat, p.nom_plat " +
                            "ORDER BY prix_moyen DESC";

                // 5. Les heures de pointe des commandes
                string sql5 = "SELECT DATEPART(HOUR, date_commande) as heure, " +
                            "COUNT(*) as nombre_commandes " +
                            "FROM commande " +
                            "GROUP BY DATEPART(HOUR, date_commande) " +
                            "ORDER BY nombre_commandes DESC";

                Console.WriteLine("\nStatistiques creatives :");
                Console.WriteLine("----------------------------------------");

                // Affiche les resultats
                Console.WriteLine("\n1. Plats les plus commandes par jour :");
                using (SqlCommand cmd = new SqlCommand(sql1, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Jour: {reader["jour"]}");
                        Console.WriteLine($"Plat: {reader["nom_plat"]}");
                        Console.WriteLine($"Nombre de commandes: {reader["nombre_commandes"]}");
                    }
                }

                Console.WriteLine("\n2. Cuisiniers les plus populaires :");
                using (SqlCommand cmd = new SqlCommand(sql2, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Cuisinier: {reader["prenom"]} {reader["nom"]}");
                        Console.WriteLine($"Note moyenne: {reader["note_moyenne"]}");
                    }
                }

                Console.WriteLine("\n3. Clients les plus fideles :");
                using (SqlCommand cmd = new SqlCommand(sql3, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Client: {reader["prenom"]} {reader["nom"]}");
                        Console.WriteLine($"Nombre de commandes: {reader["nombre_commandes"]}");
                    }
                }

                Console.WriteLine("\n4. Plats les plus rentables :");
                using (SqlCommand cmd = new SqlCommand(sql4, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Plat: {reader["nom_plat"]}");
                        Console.WriteLine($"Prix moyen: {reader["prix_moyen"]}€");
                    }
                }

                Console.WriteLine("\n5. Heures de pointe des commandes :");
                using (SqlCommand cmd = new SqlCommand(sql5, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Heure: {reader["heure"]}h");
                        Console.WriteLine($"Nombre de commandes: {reader["nombre_commandes"]}");
                    }
                }

                Console.WriteLine("----------------------------------------");
            }
        }
    }
} 