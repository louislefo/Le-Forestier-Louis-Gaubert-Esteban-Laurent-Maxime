using System;
using System.Collections.Generic;
using System.Data;
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
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT u.nom, u.prenom, COUNT(*) as nombre_livraisons " +
                               "FROM commande c " +
                               "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                               "INNER JOIN utilisateur u ON cu.id_utilisateur = u.id_utilisateur " +
                               "GROUP BY u.nom, u.prenom " +
                               "ORDER BY nombre_livraisons DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nNombre de livraisons par cuisinier :");
                        Console.WriteLine("----------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["prenom"] + " " + reader["nom"] + ": " + 
                                           reader["nombre_livraisons"] + " livraisons");
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des livraisons par cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les commandes par periode
        /// </summary>
        public void AfficherCommandesParPeriode(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT c.*, u.nom as nom_client, u.prenom as prenom_client, " +
                               "p.nom_plat, cu.nom as nom_cuisinier, cu.prenom as prenom_cuisinier " +
                               "FROM commande c " +
                               "INNER JOIN utilisateur u ON c.id_client = u.id_utilisateur " +
                               "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                               "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                               "WHERE c.date_commande BETWEEN @dateDebut AND @dateFin " +
                               "ORDER BY c.date_commande";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                    cmd.Parameters.AddWithValue("@dateFin", dateFin);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nCommandes entre " + dateDebut.ToShortDateString() + " et " + 
                                       dateFin.ToShortDateString() + " :");
                        Console.WriteLine("----------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine("Commande #" + reader["id_commande"]);
                            Console.WriteLine("Date: " + reader["date_commande"]);
                            Console.WriteLine("Client: " + reader["prenom_client"] + " " + reader["nom_client"]);
                            Console.WriteLine("Cuisinier: " + reader["prenom_cuisinier"] + " " + reader["nom_cuisinier"]);
                            Console.WriteLine("Plat: " + reader["nom_plat"]);
                            Console.WriteLine("Prix: " + reader["prix_total"] + "€");
                            Console.WriteLine("----------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des commandes par periode : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche la moyenne des prix des commandes
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT AVG(prix_total) as moyenne_prix FROM commande";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    double moyenne = Convert.ToDouble(cmd.ExecuteScalar());

                    Console.WriteLine("\nMoyenne des prix des commandes : " + moyenne.ToString("F2") + "€");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul de la moyenne des prix : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche la moyenne des comptes clients
        /// </summary>
        public void AfficherMoyenneComptesClients()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT AVG(solde) as moyenne_solde FROM client";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    double moyenne = Convert.ToDouble(cmd.ExecuteScalar());

                    Console.WriteLine("\nMoyenne des soldes des comptes clients : " + moyenne.ToString("F2") + "€");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul de la moyenne des comptes : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les commandes par type de plat
        /// </summary>
        public void AfficherCommandesParTypePlat(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT p.type_plat, COUNT(*) as nombre_commandes " +
                               "FROM commande c " +
                               "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                               "WHERE c.date_commande BETWEEN @dateDebut AND @dateFin " +
                               "GROUP BY p.type_plat " +
                               "ORDER BY nombre_commandes DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                    cmd.Parameters.AddWithValue("@dateFin", dateFin);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nCommandes par type de plat entre " + dateDebut.ToShortDateString() + 
                                       " et " + dateFin.ToShortDateString() + " :");
                        Console.WriteLine("----------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["type_plat"] + ": " + reader["nombre_commandes"] + " commandes");
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des commandes par type de plat : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche des statistiques creatives
        /// </summary>
        public void AfficherStatistiquesCreatives()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // plat le plus commande par jour
                    string sqlPlatJour = "SELECT TOP 1 p.nom_plat, COUNT(*) as nombre_commandes " +
                                       "FROM commande c " +
                                       "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                                       "WHERE CAST(c.date_commande AS DATE) = CAST(GETDATE() AS DATE) " +
                                       "GROUP BY p.nom_plat " +
                                       "ORDER BY nombre_commandes DESC";

                    SqlCommand cmdPlatJour = new SqlCommand(sqlPlatJour, conn);
                    using (SqlDataReader reader = cmdPlatJour.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("\nPlat le plus commande aujourd'hui : " + reader["nom_plat"] + 
                                           " (" + reader["nombre_commandes"] + " commandes)");
                        }
                    }

                    // cuisinier le plus populaire
                    string sqlCuisinierPop = "SELECT TOP 1 u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                                           "FROM commande c " +
                                           "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                                           "INNER JOIN utilisateur u ON cu.id_utilisateur = u.id_utilisateur " +
                                           "GROUP BY u.nom, u.prenom " +
                                           "ORDER BY nombre_commandes DESC";

                    SqlCommand cmdCuisinierPop = new SqlCommand(sqlCuisinierPop, conn);
                    using (SqlDataReader reader = cmdCuisinierPop.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Cuisinier le plus populaire : " + reader["prenom"] + " " + 
                                           reader["nom"] + " (" + reader["nombre_commandes"] + " commandes)");
                        }
                    }

                    // client le plus fidele
                    string sqlClientFidele = "SELECT TOP 1 u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                                           "FROM commande c " +
                                           "INNER JOIN client cl ON c.id_client = cl.id_utilisateur " +
                                           "INNER JOIN utilisateur u ON cl.id_utilisateur = u.id_utilisateur " +
                                           "GROUP BY u.nom, u.prenom " +
                                           "ORDER BY nombre_commandes DESC";

                    SqlCommand cmdClientFidele = new SqlCommand(sqlClientFidele, conn);
                    using (SqlDataReader reader = cmdClientFidele.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Client le plus fidele : " + reader["prenom"] + " " + 
                                           reader["nom"] + " (" + reader["nombre_commandes"] + " commandes)");
                        }
                    }

                    // plat le plus rentable
                    string sqlPlatRentable = "SELECT TOP 1 p.nom_plat, SUM(c.prix_total) as chiffre_affaires " +
                                           "FROM commande c " +
                                           "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                                           "GROUP BY p.nom_plat " +
                                           "ORDER BY chiffre_affaires DESC";

                    SqlCommand cmdPlatRentable = new SqlCommand(sqlPlatRentable, conn);
                    using (SqlDataReader reader = cmdPlatRentable.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Plat le plus rentable : " + reader["nom_plat"] + 
                                           " (" + reader["chiffre_affaires"] + "€ de chiffre d'affaires)");
                        }
                    }

                    // heure de pointe des commandes
                    string sqlHeurePointe = "SELECT TOP 1 DATEPART(HOUR, date_commande) as heure, " +
                                          "COUNT(*) as nombre_commandes " +
                                          "FROM commande " +
                                          "GROUP BY DATEPART(HOUR, date_commande) " +
                                          "ORDER BY nombre_commandes DESC";

                    SqlCommand cmdHeurePointe = new SqlCommand(sqlHeurePointe, conn);
                    using (SqlDataReader reader = cmdHeurePointe.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Heure de pointe des commandes : " + reader["heure"] + "h " +
                                           "(" + reader["nombre_commandes"] + " commandes)");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des statistiques creatives : " + ex.Message);
            }
        }
    }
} 