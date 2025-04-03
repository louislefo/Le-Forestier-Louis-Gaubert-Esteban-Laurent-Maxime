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

        /// <summary>
        /// genere un id unique pour un utilisateur
        /// </summary>
        private string GenererIdUtilisateur()
        {
            try
            {
                // recupere le dernier id utilisateur
                string sql = "SELECT id_utilisateur FROM utilisateur WHERE id_utilisateur LIKE 'USR%' ORDER BY id_utilisateur DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si aucun utilisateur n'existe, commence par USR001
                    return "USR001";
                }
                
                string dernierId = result.ToString();
                // extrait le numero
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // formate le nouvel id
                return "USR" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id utilisateur : " + ex.Message);
                // en cas d'erreur, genere un id avec timestamp
                return "USR" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// genere un id unique pour un client
        /// </summary>
        private string GenererIdClient()
        {
            try
            {
                // recupere le dernier id client
                string sql = "SELECT id_client FROM client WHERE id_client LIKE 'CLI%' ORDER BY id_client DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si aucun client n'existe, commence par CLI001
                    return "CLI001";
                }
                
                string dernierId = result.ToString();
                // extrait le numero
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // formate le nouvel id
                return "CLI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id client : " + ex.Message);
                // en cas d'erreur, genere un id avec timestamp
                return "CLI" + DateTime.Now.Ticks;
            }
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
                
                // generation d'un id unique pour l'utilisateur et le client
                string idUtilisateur = GenererIdUtilisateur();
                string idClient = GenererIdClient();
                
                // insertion dans la table utilisateur
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                try
                {
                    // insertion dans la table client
                    string requeteClient = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                        idClient + "', '" + idUtilisateur + "', '" + stationMetro + "', " + 
                        (entrepriseNom == null ? "NULL" : "'" + entrepriseNom + "'") + ", " + 
                        (referent == null ? "NULL" : "'" + referent + "'") + ")";
                    
                    MySqlCommand cmdClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                    cmdClient.ExecuteNonQuery();

                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur lors de l'ajout du client : " + e.Message);
                }
                
                
                Console.WriteLine("Client ajouté avec succès !");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du client : " + e.Message);
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
                
                // verifie si l'utilisateur existe avec une requete simple
                string requete = "SELECT COUNT(*) FROM utilisateur WHERE id_utilisateur='" + idUtilisateur + "'";
                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;
                
                int count = Convert.ToInt32(commande0.ExecuteScalar());
                
                if (count == 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " n'existe pas dans la base de données.");
                    commande0.Dispose();
                    return;
                }
                
                // verifie si l'utilisateur est deja un client avec une requete simple
                string requete2 = "SELECT COUNT(*) FROM client WHERE id_utilisateur='" + idUtilisateur + "'";
                MySqlCommand commande1 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande1.CommandText = requete2;
                
                int countClient = Convert.ToInt32(commande1.ExecuteScalar());
                
                if (countClient > 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " est déjà un client.");
                    commande0.Dispose();
                    commande1.Dispose();
                    return;
                }
                
                // demande les informations du client
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du client : ");
                
                // demande du type de client (particulier ou entreprise)
                Console.WriteLine("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                
                string entrepriseNom = "NULL";
                string referent = "NULL";
                
                // si c'est une entreprise, demander les informations supplémentaires
                if (typeClient == 2)
                {
                    entrepriseNom = "'" + ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ") + "'";
                    referent = "'" + ValidationRequette.DemanderNom("Entrez le nom du référent : ") + "'";
                }
                
                // generation d'un id unique pour le client
                string idClient = GenererIdClient();
                
                // insertion dans la table client avec une requete simple
                string requete3 = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                    idClient + "', '" + idUtilisateur + "', '" + stationMetro + "', " + entrepriseNom + ", " + referent + ")";
                
                MySqlCommand commande2 = new MySqlCommand(requete3, connexionBDD.maConnexion);
                commande2.CommandText = requete3;
                commande2.ExecuteNonQuery();
                
                Console.WriteLine("Client ajouté avec succès à partir de l'utilisateur existant !");
                
                commande0.Dispose();
                commande1.Dispose();
                commande2.Dispose();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du client : " + e.Message);
            }
        }

        /// <summary>
        /// supprime un client et son utilisateur de la base
        /// </summary>
        public void SupprimerClient()
        {
            try
            {
                Console.WriteLine("Entrez l'ID de l'utilisateur à supprimer : ");
                string id = Console.ReadLine();

                // verifie si le client existe
                string requete1 = "SELECT COUNT(*) FROM client WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande1 = new MySqlCommand(requete1, connexionBDD.maConnexion);
                commande1.CommandText = requete1;
                
                int count = Convert.ToInt32(commande1.ExecuteScalar());
                
                if (count == 0)
                {
                    Console.WriteLine("Le client avec l'ID " + id + " n'existe pas");
                    commande1.Dispose();
                    return;
                }

                // supprime d'abord les commandes liées au client
                string requete2 = "DELETE FROM Commande_ WHERE id_client IN (SELECT id_client FROM client WHERE id_utilisateur='" + id + "')";
                MySqlCommand commande2 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande2.CommandText = requete2;
                commande2.ExecuteNonQuery();

                // supprime le client
                string requete3 = "DELETE FROM client WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande3 = new MySqlCommand(requete3, connexionBDD.maConnexion);
                commande3.CommandText = requete3;
                commande3.ExecuteNonQuery();

                // supprime l'utilisateur
                string requete4 = "DELETE FROM utilisateur WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande4 = new MySqlCommand(requete4, connexionBDD.maConnexion);
                commande4.CommandText = requete4;
                commande4.ExecuteNonQuery();

                Console.WriteLine("Client supprimé avec succès !");

                commande1.Dispose();
                commande2.Dispose();
                commande3.Dispose();
                commande4.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la suppression du client : " + ex.Message);
            }
        }

        /// <summary>
        /// modifie les informations d un client
        /// </summary>
        public void ModifierClient()
        {
            try
            {
                Console.WriteLine("Entrez l'ID du client à modifier : ");
                string id = Console.ReadLine();

                // verifie si le client existe
                string requete1 = "SELECT COUNT(*) FROM client WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande1 = new MySqlCommand(requete1, connexionBDD.maConnexion);
                commande1.CommandText = requete1;
                
                int count = Convert.ToInt32(commande1.ExecuteScalar());
                
                if (count == 0)
                {
                    Console.WriteLine("Le client avec l'ID " + id + " n'existe pas");
                    commande1.Dispose();
                    return;
                }

                // demande les nouvelles informations
                Console.WriteLine("Entrez le nouveau nom : ");
                string nom = Console.ReadLine();

                Console.WriteLine("Entrez le nouveau prénom : ");
                string prenom = Console.ReadLine();

                Console.WriteLine("Entrez la nouvelle adresse : ");
                string adresse = Console.ReadLine();

                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la nouvelle station de métro : ");

                // mise a jour de l'utilisateur
                string requete2 = "UPDATE utilisateur SET nom='" + nom + "', prénom='" + prenom + "', adresse='" + adresse + "' WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande2 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande2.CommandText = requete2;
                commande2.ExecuteNonQuery();

                // mise a jour du client
                string requete3 = "UPDATE client SET StationMetro='" + stationMetro + "' WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande3 = new MySqlCommand(requete3, connexionBDD.maConnexion);
                commande3.CommandText = requete3;
                commande3.ExecuteNonQuery();

                Console.WriteLine("Client modifié avec succès !");

                commande1.Dispose();
                commande2.Dispose();
                commande3.Dispose();
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
                string requete = "SELECT u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro FROM utilisateur u, client c WHERE u.id_utilisateur = c.id_utilisateur ORDER BY u.nom ASC, u.prénom ASC";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nListe des clients par ordre alphabetique :");
                Console.WriteLine("----------------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine("ID : " + reader["id_utilisateur"]);
                    Console.WriteLine("Nom : " + reader["nom"]);
                    Console.WriteLine("Prenom : " + reader["prénom"]);
                    Console.WriteLine("Adresse : " + reader["adresse"]);
                    Console.WriteLine("Station Metro : " + reader["StationMetro"]);
                    Console.WriteLine("----------------------------------------");
                }

                reader.Close();
                commande.Dispose();
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
                string requete = "SELECT u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro FROM utilisateur u, client c WHERE u.id_utilisateur = c.id_utilisateur ORDER BY u.adresse ASC";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nListe des clients par rue :");
                Console.WriteLine("----------------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine("ID : " + reader["id_utilisateur"]);
                    Console.WriteLine("Nom : " + reader["nom"]);
                    Console.WriteLine("Prenom : " + reader["prénom"]);
                    Console.WriteLine("Adresse : " + reader["adresse"]);
                    Console.WriteLine("Station Metro : " + reader["StationMetro"]);
                    Console.WriteLine("----------------------------------------");
                }

                reader.Close();
                commande.Dispose();
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
                string requete = "SELECT u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro, SUM(co.prix_total) as total FROM utilisateur u, client c, Commande_ co WHERE u.id_utilisateur = c.id_utilisateur AND c.id_client = co.id_client GROUP BY u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro ORDER BY total DESC";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nListe des clients par montant des achats :");
                Console.WriteLine("----------------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine("ID : " + reader["id_utilisateur"]);
                    Console.WriteLine("Nom : " + reader["nom"]);
                    Console.WriteLine("Prenom : " + reader["prénom"]);
                    Console.WriteLine("Adresse : " + reader["adresse"]);
                    Console.WriteLine("Station Metro : " + reader["StationMetro"]);
                    Console.WriteLine("Total des achats : " + reader["total"] + " euros");
                    Console.WriteLine("----------------------------------------");
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des clients : " + ex.Message);
            }
        }

        
        
        
    }
} 