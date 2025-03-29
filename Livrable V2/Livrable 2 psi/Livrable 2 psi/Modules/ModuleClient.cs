using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les operations sur les clients
    /// </summary>
    public class ModuleClient
    {
        public ConnexionBDD connexionBDD;

        public ModuleClient(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
        }

        /// <summary>
        /// ajoute un client depuis la console
        /// </summary>
        public void AjouterClientConsole()
        {
            Console.WriteLine("entrez le nom du client :");
            string nom = Console.ReadLine();

            Console.WriteLine("entrez le prenom du client :");
            string prenom = Console.ReadLine();

            Console.WriteLine("entrez l'adresse du client :");
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

                // insere dans la table client
                string sqlClient = "INSERT INTO client (id_utilisateur, station_metro) VALUES (@idUtilisateur, @stationMetro)";
                MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                cmdClient.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                cmdClient.Parameters.AddWithValue("@stationMetro", stationMetro);

                cmdClient.ExecuteNonQuery();
                Console.WriteLine("client ajoute avec succes");

                cmdUtilisateur.Dispose();
                cmdClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'ajout du client : " + ex.Message);
            }
        }

        /// <summary>
        /// ajoute des clients depuis un fichier
        /// </summary>
        public void AjouterClientsFichier(string cheminFichier)
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

                            // insere dans la table client
                            string sqlClient = "INSERT INTO client (id_utilisateur, station_metro) VALUES (@idUtilisateur, @stationMetro)";
                            MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                            cmdClient.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                            cmdClient.Parameters.AddWithValue("@stationMetro", stationMetro);

                            cmdClient.ExecuteNonQuery();

                            cmdUtilisateur.Dispose();
                            cmdClient.Dispose();
                        }
                    }
                }
                Console.WriteLine("clients ajoutes avec succes depuis le fichier");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'ajout des clients depuis le fichier : " + ex.Message);
            }
        }

        /// <summary>
        /// supprime un client
        /// </summary>
        public void SupprimerClient(int idClient)
        {
            try
            {
                // supprime d'abord de la table client
                string sqlClient = "DELETE FROM client WHERE id_utilisateur = @idClient";
                MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                cmdClient.Parameters.AddWithValue("@idClient", idClient);
                cmdClient.ExecuteNonQuery();

                // puis de la table utilisateur
                string sqlUtilisateur = "DELETE FROM utilisateur WHERE id_utilisateur = @idClient";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@idClient", idClient);
                cmdUtilisateur.ExecuteNonQuery();

                Console.WriteLine("client supprime avec succes");

                cmdClient.Dispose();
                cmdUtilisateur.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la suppression du client : " + ex.Message);
            }
        }

        /// <summary>
        /// modifie un client
        /// </summary>
        public void ModifierClient(int idClient, string nom, string prenom, string adresse, string stationMetro)
        {
            try
            {
                // modifie la table utilisateur
                string sqlUtilisateur = "UPDATE utilisateur SET nom = @nom, prenom = @prenom, adresse = @adresse WHERE id_utilisateur = @idClient";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@idClient", idClient);
                cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);
                cmdUtilisateur.ExecuteNonQuery();

                // modifie la table client
                string sqlClient = "UPDATE client SET station_metro = @stationMetro WHERE id_utilisateur = @idClient";
                MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                cmdClient.Parameters.AddWithValue("@idClient", idClient);
                cmdClient.Parameters.AddWithValue("@stationMetro", stationMetro);
                cmdClient.ExecuteNonQuery();

                Console.WriteLine("client modifie avec succes");

                cmdUtilisateur.Dispose();
                cmdClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la modification du client : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les clients par ordre alphabetique
        /// </summary>
        public void AfficherClientsAlphabetique()
        {
            try
            {
                string sql = "SELECT u.*, c.station_metro FROM utilisateur u " +
                           "INNER JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                           "ORDER BY u.nom, u.prenom";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des clients par ordre alphabetique :");
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
                Console.WriteLine("erreur lors de l'affichage des clients : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les clients par rue
        /// </summary>
        public void AfficherClientsParRue()
        {
            try
            {
                string sql = "SELECT u.*, c.station_metro FROM utilisateur u " +
                           "INNER JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                           "ORDER BY u.adresse";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des clients par rue :");
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
                Console.WriteLine("erreur lors de l'affichage des clients : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les clients par montant des achats cumules
        /// </summary>
        public void AfficherClientsParAchats()
        {
            try
            {
                string sql = "SELECT u.*, c.station_metro, " +
                           "IFNULL(SUM(co.prix_total), 0) as total_achats " +
                           "FROM utilisateur u " +
                           "INNER JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                           "LEFT JOIN commande co ON u.id_utilisateur = co.id_client " +
                           "GROUP BY u.id_utilisateur, u.nom, u.prenom, u.adresse, c.station_metro " +
                           "ORDER BY total_achats DESC";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nListe des clients par montant des achats :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader["id_utilisateur"]);
                        Console.WriteLine("Nom: " + reader["nom"]);
                        Console.WriteLine("Prenom: " + reader["prenom"]);
                        Console.WriteLine("Adresse: " + reader["adresse"]);
                        Console.WriteLine("Station Metro: " + reader["station_metro"]);
                        Console.WriteLine("Total des achats: " + reader["total_achats"] + "€");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des clients : " + ex.Message);
            }
        }
    }
} 