using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les operations sur les cuisiniers
    /// </summary>
    public class ModuleCuisinier
    {
        public ConnexionBDD connexionBDD;

        public ModuleCuisinier(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
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

            try
            {
                // insere dans la table utilisateur
                string sqlUtilisateur = "INSERT INTO utilisateur (nom, prenom, adresse) VALUES (@nom, @prenom, @adresse); SELECT LAST_INSERT_ID();";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);

                int idUtilisateur = Convert.ToInt32(cmdUtilisateur.ExecuteScalar());

                // insere dans la table cuisinier
                string sqlCuisinier = "INSERT INTO cuisinier (id_utilisateur, station_metro) VALUES (@idUtilisateur, @stationMetro)";
                MySqlCommand cmdCuisinier = new MySqlCommand(sqlCuisinier, connexionBDD.maConnexion);
                cmdCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                cmdCuisinier.Parameters.AddWithValue("@stationMetro", stationMetro);

                cmdCuisinier.ExecuteNonQuery();
                Console.WriteLine("cuisinier ajoute avec succes");

                cmdUtilisateur.Dispose();
                cmdCuisinier.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'ajout du cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// ajoute des cuisiniers depuis un fichier
        /// </summary>
        public void AjouterCuisiniersFichier(string cheminFichier)
        {
            try
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

                            // insere dans la table utilisateur
                            string sqlUtilisateur = "INSERT INTO utilisateur (nom, prenom, adresse) VALUES (@nom, @prenom, @adresse); SELECT LAST_INSERT_ID();";
                            MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                            cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                            cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                            cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);

                            int idUtilisateur = Convert.ToInt32(cmdUtilisateur.ExecuteScalar());

                            // insere dans la table cuisinier
                            string sqlCuisinier = "INSERT INTO cuisinier (id_utilisateur, station_metro) VALUES (@idUtilisateur, @stationMetro)";
                            MySqlCommand cmdCuisinier = new MySqlCommand(sqlCuisinier, connexionBDD.maConnexion);
                            cmdCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                            cmdCuisinier.Parameters.AddWithValue("@stationMetro", stationMetro);

                            cmdCuisinier.ExecuteNonQuery();

                            cmdUtilisateur.Dispose();
                            cmdCuisinier.Dispose();
                        }
                    }
                }
                Console.WriteLine("cuisiniers ajoutes avec succes depuis le fichier");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'ajout des cuisiniers depuis le fichier : " + ex.Message);
            }
        }

        /// <summary>
        /// supprime un cuisinier
        /// </summary>
        public void SupprimerCuisinier(int idCuisinier)
        {
            try
            {
                // supprime d'abord de la table cuisinier
                string sqlCuisinier = "DELETE FROM cuisinier WHERE id_utilisateur = @idCuisinier";
                MySqlCommand cmdCuisinier = new MySqlCommand(sqlCuisinier, connexionBDD.maConnexion);
                cmdCuisinier.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCuisinier.ExecuteNonQuery();

                // puis de la table utilisateur
                string sqlUtilisateur = "DELETE FROM utilisateur WHERE id_utilisateur = @idCuisinier";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdUtilisateur.ExecuteNonQuery();

                Console.WriteLine("cuisinier supprime avec succes");

                cmdCuisinier.Dispose();
                cmdUtilisateur.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la suppression du cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// modifie un cuisinier
        /// </summary>
        public void ModifierCuisinier(int idCuisinier, string nom, string prenom, string adresse, string stationMetro)
        {
            try
            {
                // modifie la table utilisateur
                string sqlUtilisateur = "UPDATE utilisateur SET nom = @nom, prenom = @prenom, adresse = @adresse WHERE id_utilisateur = @idCuisinier";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);
                cmdUtilisateur.ExecuteNonQuery();

                // modifie la table cuisinier
                string sqlCuisinier = "UPDATE cuisinier SET station_metro = @stationMetro WHERE id_utilisateur = @idCuisinier";
                MySqlCommand cmdCuisinier = new MySqlCommand(sqlCuisinier, connexionBDD.maConnexion);
                cmdCuisinier.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCuisinier.Parameters.AddWithValue("@stationMetro", stationMetro);
                cmdCuisinier.ExecuteNonQuery();

                Console.WriteLine("cuisinier modifie avec succes");

                cmdUtilisateur.Dispose();
                cmdCuisinier.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la modification du cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les clients servis par un cuisinier
        /// </summary>
        public void AfficherClientsServis(int idCuisinier, DateTime? dateDebut = null, DateTime? dateFin = null)
        {
            try
            {
                string sql = "SELECT DISTINCT u.*, c.station_metro " +
                           "FROM utilisateur u " +
                           "INNER JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                           "INNER JOIN commande co ON u.id_utilisateur = co.id_client " +
                           "WHERE co.id_cuisinier = @idCuisinier " +
                           (dateDebut.HasValue ? "AND co.date_commande >= @dateDebut " : "") +
                           (dateFin.HasValue ? "AND co.date_commande <= @dateFin " : "") +
                           "ORDER BY u.nom, u.prenom";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                if (dateDebut.HasValue)
                    cmd.Parameters.AddWithValue("@dateDebut", dateDebut.Value);
                if (dateFin.HasValue)
                    cmd.Parameters.AddWithValue("@dateFin", dateFin.Value);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des clients servis :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader["id_utilisateur"]);
                        Console.WriteLine("Nom: " + reader["nom"]);
                        Console.WriteLine("Prenom: " + reader["prenom"]);
                        Console.WriteLine("Adresse: " + reader["adresse"]);
                        Console.WriteLine("Station Metro: " + reader["station_metro"]);
                        Console.WriteLine("----------------------------------------");
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des clients servis : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les plats realises par un cuisinier avec leur frequence
        /// </summary>
        public void AfficherPlatsRealises(int idCuisinier)
        {
            try
            {
                string sql = "SELECT p.nom_plat, COUNT(*) as frequence " +
                           "FROM plat p " +
                           "INNER JOIN commande co ON p.id_plat = co.id_plat " +
                           "WHERE co.id_cuisinier = @idCuisinier " +
                           "GROUP BY p.id_plat, p.nom_plat " +
                           "ORDER BY frequence DESC";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@idCuisinier", idCuisinier);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des plats realises avec leur frequence :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("Plat: " + reader["nom_plat"]);
                        Console.WriteLine("Nombre de fois realise: " + reader["frequence"]);
                        Console.WriteLine("----------------------------------------");
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des plats realises : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche le plat du jour d'un cuisinier
        /// </summary>
        public void AfficherPlatDuJour(int idCuisinier)
        {
            try
            {
                string sql = "SELECT p.* " +
                           "FROM plat p " +
                           "INNER JOIN plat_du_jour pdj ON p.id_plat = pdj.id_plat " +
                           "WHERE pdj.id_cuisinier = @idCuisinier " +
                           "AND DATE(pdj.date) = CURDATE()";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@idCuisinier", idCuisinier);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nPlat du jour :");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("ID: " + reader["id_plat"]);
                        Console.WriteLine("Nom: " + reader["nom_plat"]);
                        Console.WriteLine("Description: " + reader["description"]);
                        Console.WriteLine("Prix: " + reader["prix"] + "€");
                        Console.WriteLine("----------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("\nAucun plat du jour pour aujourd'hui");
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage du plat du jour : " + ex.Message);
            }
        }
    }
} 