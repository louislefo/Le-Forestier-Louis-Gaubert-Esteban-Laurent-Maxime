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
        private Graphe<int> grapheMetro;

        public ModuleClient(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
        {
            this.connexionBDD = connexionBDD;
            this.grapheMetro = grapheMetro;
        }

        public void AjouterClientConsole()
        {
            // creation de la connexion a la bdd
            ConnexionBDD connexion = new ConnexionBDD();
            
            try
            {
                // validation des données avec ValidationRequette
                string nom = ValidationRequette.DemanderNom("Entrez le nom du client : ");
                string prenom = ValidationRequette.DemanderNom("Entrez le prenom du client : ");
                string adresse = ValidationRequette.DemanderAdresse("Entrez l'adresse du client : ");
                
                // creation d'une instance de ValidationRequette avec le graphe
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du client : ");
                
                // generation d'un id unique pour l'utilisateur
                string idUtilisateur = "U" + DateTime.Now.Ticks;
                string idClient = "C" + DateTime.Now.Ticks;
                
                // insertion dans la table utilisateur
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, adresse, type__Cuisinier_Client_) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + adresse + "', 'Client')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexion.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                // insertion dans la table client
                string requeteClient = "INSERT INTO client (id_client, id_utilisateur, type_client__Particulier_Entreprise_, StationMetro) VALUES ('" + 
                    idClient + "', '" + idUtilisateur + "', 'Particulier', '" + stationMetro + "')";
                
                MySqlCommand cmdClient = new MySqlCommand(requeteClient, connexion.maConnexion);
                cmdClient.ExecuteNonQuery();
                
                Console.WriteLine("Client ajouté avec succès !");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du client : " + e.Message);
            }
            finally
            {
                connexion.FermerConnexion();
            }
        }

        public void SupprimerClient(int id)
        {
            try
            {
                // supprime d'abord de la table client
                string sqlClient = "DELETE FROM client WHERE id_utilisateur = "+id+";";
                MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                cmdClient.Parameters.AddWithValue("@id", id);
                cmdClient.ExecuteNonQuery();

                string sql = "DELETE FROM client WHERE id_utilisateur = @id";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Client supprimé avec succès !");

                cmdClient.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la suppression du client : " + ex.Message);
            }

        }

        public void ModifierClient(int id, string nom, string prenom, string adresse, string stationMetro)
        {
            Console.WriteLine("A faire");
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
                           "ORDER BY u.nom, u.prenom DESC";

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
                           "ORDER BY u.adresse DESC";

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

        
        public void ExecuterRequetePersonnalisee()
        {
            Console.WriteLine("\nEntrez votre requête SQL (attention: soyez prudent) :");
            string requete = Console.ReadLine();
            
            try
            {
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;
                
                // vérifie si c'est une requête SELECT ou autre
                if (requete.Trim().ToUpper().StartsWith("SELECT"))
                {
                    // requête de type SELECT
                    MySqlDataReader reader = commande.ExecuteReader();
                    
                    Console.WriteLine("\n=== RÉSULTATS DE LA REQUÊTE ===");
                    
                    // affiche les noms des colonnes
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + " | ");
                    }
                    Console.WriteLine("\n----------------------------------------");
                    
                    string[]  valueString = new string[reader.FieldCount];
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            valueString[i] = reader.GetValue(i).ToString();
                            Console.Write(valueString[i] + " | ");
                        }
                        Console.WriteLine();
                    }
                    
                    reader.Close();
                }
                else
                {
                    // requête de type non-SELECT (INSERT, UPDATE, DELETE, etc.)
                    int  nbLignes = commande.ExecuteNonQuery();
                    Console.WriteLine("\nRequête exécutée avec succès. " + nbLignes + " ligne(s) affectée(s).");
                }
                
                commande.Dispose();
                Console.WriteLine("\n--------------------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'exécution de la requête : " + ex.Message);
            }
        }
        

        

        
    }
} 