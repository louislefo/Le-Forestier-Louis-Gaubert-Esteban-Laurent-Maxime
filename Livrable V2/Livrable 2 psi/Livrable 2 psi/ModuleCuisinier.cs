using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les operations sur les cuisiniers
    /// </summary>
    public class ModuleCuisinier
    {
        private string connectionString;

        public ModuleCuisinier(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// ajoute un cuisinier depuis la console
        /// </summary>
        public void AjouterCuisinierConsole()
        {
            Console.WriteLine("entrez le nom du cuisinier :");
            string nom = Console.ReadLine();

            Console.WriteLine("entrez le prenom du cuisinier :");
            string prenom = Console.ReadLine();

            Console.WriteLine("entrez l'adresse du cuisinier :");
            string adresse = Console.ReadLine();

            Console.WriteLine("entrez la station metro la plus proche :");
            string stationMetro = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // insere dans la table utilisateur
                string sqlUtilisateur = "INSERT INTO utilisateur (nom, prenom, adresse) VALUES (@nom, @prenom, @adresse); SELECT SCOPE_IDENTITY();";
                SqlCommand cmdUtilisateur = new SqlCommand(sqlUtilisateur, conn);
                cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);

                int idUtilisateur = Convert.ToInt32(cmdUtilisateur.ExecuteScalar());

                // insere dans la table cuisinier
                string sqlCuisinier = "INSERT INTO cuisinier (id_utilisateur, station_metro) VALUES (@idUtilisateur, @stationMetro)";
                SqlCommand cmdCuisinier = new SqlCommand(sqlCuisinier, conn);
                cmdCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                cmdCuisinier.Parameters.AddWithValue("@stationMetro", stationMetro);

                cmdCuisinier.ExecuteNonQuery();
                Console.WriteLine("cuisinier ajoute avec succes");
            }
        }

        /// <summary>
        /// ajoute des cuisiniers depuis un fichier
        /// </summary>
        public void AjouterCuisiniersFichier(string cheminFichier)
        {
            using (StreamReader sr = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] donnees = ligne.Split(',');
                    if (donnees.Length >= 4)
                    {
                        string nom = donnees[0].Trim();
                        string prenom = donnees[1].Trim();
                        string adresse = donnees[2].Trim();
                        string stationMetro = donnees[3].Trim();

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            // insere dans la table utilisateur
                            string sqlUtilisateur = "INSERT INTO utilisateur (nom, prenom, adresse) VALUES (@nom, @prenom, @adresse); SELECT SCOPE_IDENTITY();";
                            SqlCommand cmdUtilisateur = new SqlCommand(sqlUtilisateur, conn);
                            cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                            cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                            cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);

                            int idUtilisateur = Convert.ToInt32(cmdUtilisateur.ExecuteScalar());

                            // insere dans la table cuisinier
                            string sqlCuisinier = "INSERT INTO cuisinier (id_utilisateur, station_metro) VALUES (@idUtilisateur, @stationMetro)";
                            SqlCommand cmdCuisinier = new SqlCommand(sqlCuisinier, conn);
                            cmdCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                            cmdCuisinier.Parameters.AddWithValue("@stationMetro", stationMetro);

                            cmdCuisinier.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// supprime un cuisinier
        /// </summary>
        public void SupprimerCuisinier(int idCuisinier)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // supprime d'abord de la table cuisinier
                string sqlCuisinier = "DELETE FROM cuisinier WHERE id_utilisateur = @idCuisinier";
                SqlCommand cmdCuisinier = new SqlCommand(sqlCuisinier, conn);
                cmdCuisinier.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCuisinier.ExecuteNonQuery();

                // puis de la table utilisateur
                string sqlUtilisateur = "DELETE FROM utilisateur WHERE id_utilisateur = @idCuisinier";
                SqlCommand cmdUtilisateur = new SqlCommand(sqlUtilisateur, conn);
                cmdUtilisateur.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdUtilisateur.ExecuteNonQuery();

                Console.WriteLine("cuisinier supprime avec succes");
            }
        }

        /// <summary>
        /// modifie un cuisinier
        /// </summary>
        public void ModifierCuisinier(int idCuisinier, string nom, string prenom, string adresse, string stationMetro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // modifie la table utilisateur
                string sqlUtilisateur = "UPDATE utilisateur SET nom = @nom, prenom = @prenom, adresse = @adresse WHERE id_utilisateur = @idCuisinier";
                SqlCommand cmdUtilisateur = new SqlCommand(sqlUtilisateur, conn);
                cmdUtilisateur.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);
                cmdUtilisateur.ExecuteNonQuery();

                // modifie la table cuisinier
                string sqlCuisinier = "UPDATE cuisinier SET station_metro = @stationMetro WHERE id_utilisateur = @idCuisinier";
                SqlCommand cmdCuisinier = new SqlCommand(sqlCuisinier, conn);
                cmdCuisinier.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCuisinier.Parameters.AddWithValue("@stationMetro", stationMetro);
                cmdCuisinier.ExecuteNonQuery();

                Console.WriteLine("cuisinier modifie avec succes");
            }
        }

        /// <summary>
        /// affiche les clients servis par un cuisinier
        /// </summary>
        public void AfficherClientsServis(int idCuisinier, DateTime? dateDebut = null, DateTime? dateFin = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT DISTINCT u.*, c.station_metro " +
                           "FROM utilisateur u " +
                           "INNER JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                           "INNER JOIN commande co ON u.id_utilisateur = co.id_client " +
                           "WHERE co.id_cuisinier = @idCuisinier " +
                           (dateDebut.HasValue ? "AND co.date_commande >= @dateDebut " : "") +
                           (dateFin.HasValue ? "AND co.date_commande <= @dateFin " : "") +
                           "ORDER BY u.nom, u.prenom";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                if (dateDebut.HasValue)
                    cmd.Parameters.AddWithValue("@dateDebut", dateDebut.Value);
                if (dateFin.HasValue)
                    cmd.Parameters.AddWithValue("@dateFin", dateFin.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des clients servis :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id_utilisateur"]}");
                        Console.WriteLine($"Nom: {reader["nom"]}");
                        Console.WriteLine($"Prenom: {reader["prenom"]}");
                        Console.WriteLine($"Adresse: {reader["adresse"]}");
                        Console.WriteLine($"Station Metro: {reader["station_metro"]}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
        }

        /// <summary>
        /// affiche les plats realises par un cuisinier avec leur frequence
        /// </summary>
        public void AfficherPlatsRealises(int idCuisinier)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT p.nom_plat, COUNT(*) as frequence " +
                           "FROM plat p " +
                           "INNER JOIN commande co ON p.id_plat = co.id_plat " +
                           "WHERE co.id_cuisinier = @idCuisinier " +
                           "GROUP BY p.id_plat, p.nom_plat " +
                           "ORDER BY frequence DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCuisinier", idCuisinier);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des plats realises avec leur frequence :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Plat: {reader["nom_plat"]}");
                        Console.WriteLine($"Nombre de fois realise: {reader["frequence"]}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
        }

        /// <summary>
        /// affiche le plat du jour d'un cuisinier
        /// </summary>
        public void AfficherPlatDuJour(int idCuisinier)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT p.* " +
                           "FROM plat p " +
                           "INNER JOIN plat_du_jour pdj ON p.id_plat = pdj.id_plat " +
                           "WHERE pdj.id_cuisinier = @idCuisinier " +
                           "AND pdj.date = CAST(GETDATE() AS DATE)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCuisinier", idCuisinier);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nPlat du jour :");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine($"ID: {reader["id_plat"]}");
                        Console.WriteLine($"Nom: {reader["nom_plat"]}");
                        Console.WriteLine($"Description: {reader["description"]}");
                        Console.WriteLine($"Prix: {reader["prix"]}€");
                        Console.WriteLine("----------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("\nAucun plat du jour pour aujourd'hui");
                    }
                }
            }
        }
    }
} 