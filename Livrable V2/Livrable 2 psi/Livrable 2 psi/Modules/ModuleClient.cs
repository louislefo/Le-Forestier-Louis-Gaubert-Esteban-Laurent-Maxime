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
            try
            {
                // validation des données avec ValidationRequette
                string nom = ValidationRequette.DemanderNom("Entrez le nom du client : ");
                string prenom = ValidationRequette.DemanderNom("Entrez le prenom du client : ");
                string adresse = ValidationRequette.DemanderAdresse("Entrez l'adresse du client : ");
                string email = ValidationRequette.DemanderEmail("Entrez l'email du client : ");
                string telephone = ValidationRequette.DemanderTelephone("Entrez le telephone du client : ");
                string motDePasse = ValidationRequette.DemanderMotDePasse("Entrez le mot de passe du client : ");
                
                // creation d'une instance de ValidationRequette avec le graphe
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du client : ");
                
                // demande du type de client (particulier ou entreprise)
                Console.WriteLine("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                
                string typeClientStr = (typeClient == 1) ? "Particulier" : "Entreprise";
                string entrepriseNom = null;
                string referent = null;
                
                // si c'est une entreprise, demander les informations supplémentaires
                if (typeClient == 2)
                {
                    entrepriseNom = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                    referent = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                }
                
                // generation d'un id unique pour l'utilisateur
                string idUtilisateur = "U" + DateTime.Now.Ticks;
                string idClient = "C" + DateTime.Now.Ticks;
                
                // insertion dans la table utilisateur
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                // insertion dans la table client
                string requeteClient = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                    idClient + "', '" + idUtilisateur + "', '" + stationMetro + "', " + 
                    (entrepriseNom == null ? "NULL" : "'" + entrepriseNom + "'") + ", " + 
                    (referent == null ? "NULL" : "'" + referent + "'") + ")";
                
                MySqlCommand cmdClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                cmdClient.ExecuteNonQuery();
                
                Console.WriteLine("Client ajouté avec succès !");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du client : " + e.Message);
            }
        }

        public void SupprimerClient(string id)
        {
            try
            {
                // supprime d'abord de la table client
                string sqlClient = "DELETE FROM client WHERE id_utilisateur = @id";
                MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                cmdClient.Parameters.AddWithValue("@id", id);
                cmdClient.ExecuteNonQuery();

                // ensuite supprime de la table utilisateur
                string sqlUtilisateur = "DELETE FROM utilisateur WHERE id_utilisateur = @id";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@id", id);
                cmdUtilisateur.ExecuteNonQuery();

                Console.WriteLine("Client supprimé avec succès !");

                cmdClient.Dispose();
                cmdUtilisateur.Dispose();
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
        
        /// <summary>
        /// ajoute un client a partir d un utilisateur existant
        /// </summary>
        public void AjouterClientExistant()
        {
            try
            {
                // demande l'id de l'utilisateur existant
                Console.WriteLine("Entrez l'ID de l'utilisateur existant : ");
                string idUtilisateur = Console.ReadLine();
                
                // verifie si l'utilisateur existe
                string sqlVerif = "SELECT COUNT(*) FROM utilisateur WHERE id_utilisateur = @id";
                MySqlCommand cmdVerif = new MySqlCommand(sqlVerif, connexionBDD.maConnexion);
                cmdVerif.Parameters.AddWithValue("@id", idUtilisateur);
                
                int count = Convert.ToInt32(cmdVerif.ExecuteScalar());
                
                if (count == 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " n'existe pas dans la base de données.");
                    return;
                }
                
                // verifie si l'utilisateur est deja un client
                string sqlVerifClient = "SELECT COUNT(*) FROM client WHERE id_utilisateur = @id";
                MySqlCommand cmdVerifClient = new MySqlCommand(sqlVerifClient, connexionBDD.maConnexion);
                cmdVerifClient.Parameters.AddWithValue("@id", idUtilisateur);
                
                int countClient = Convert.ToInt32(cmdVerifClient.ExecuteScalar());
                
                if (countClient > 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " est déjà un client.");
                    return;
                }
                
                // demande les informations du client
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du client : ");
                
                // demande du type de client (particulier ou entreprise)
                Console.WriteLine("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                
                string entrepriseNom = null;
                string referent = null;
                
                // si c'est une entreprise, demander les informations supplémentaires
                if (typeClient == 2)
                {
                    entrepriseNom = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                    referent = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                }
                
                // generation d'un id unique pour le client
                string idClient = "C" + DateTime.Now.Ticks;
                
                // insertion dans la table client
                string requeteClient = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                    idClient + "', '" + idUtilisateur + "', '" + stationMetro + "', " + 
                    (entrepriseNom == null ? "NULL" : "'" + entrepriseNom + "'") + ", " + 
                    (referent == null ? "NULL" : "'" + referent + "'") + ")";
                
                MySqlCommand cmdClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                cmdClient.ExecuteNonQuery();
                
                Console.WriteLine("Client ajouté avec succès à partir de l'utilisateur existant !");
                
                cmdVerif.Dispose();
                cmdVerifClient.Dispose();
                cmdClient.Dispose();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du client : " + e.Message);
            }
        }
    }
} 