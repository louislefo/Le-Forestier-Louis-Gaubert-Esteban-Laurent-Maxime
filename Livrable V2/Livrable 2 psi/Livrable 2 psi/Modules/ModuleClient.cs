using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Livrable_2_psi
{
    /// <summary>
    /// cette classe sert a gerer tout ce qui concerne les clients dans l'application
    /// elle permet d'ajouter des clients, de les modifier, de les supprimer et de les afficher
    /// c'est une classe importante car elle gere les interactions avec la base de donnees pour les clients
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
        /// cette methode sert a generer un id unique pour un utilisateur
        /// elle regarde le dernier id dans la base et ajoute 1
        /// si y a pas d'id elle commence a 1
        /// </summary>
        private string GenererIdUtilisateur()
        {
            try
            {
                // on cherche le dernier id utilisateur dans la base
                string sql = "SELECT id_utilisateur FROM utilisateur WHERE id_utilisateur LIKE 'USR%' ORDER BY id_utilisateur DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si y a pas d'utilisateur on commence a 1
                    return "USR001";
                }
                
                string dernierId = result.ToString();
                // on prend juste le numero a la fin
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // on remet le format USR001
                return "USR" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id utilisateur : " + ex.Message);
                // si ca marche pas on met un timestamp
                return "USR" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode sert a generer un id unique pour un client
        /// elle fait pareil que pour l'utilisateur mais avec CLI au lieu de USR
        /// </summary>
        private string GenererIdClient()
        {
            try
            {
                // on cherche le dernier id client dans la base
                string sql = "SELECT id_client FROM client WHERE id_client LIKE 'CLI%' ORDER BY id_client DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si y a pas de client on commence a 1
                    return "CLI001";
                }
                
                string dernierId = result.ToString();
                // on prend juste le numero a la fin
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // on remet le format CLI001
                return "CLI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id client : " + ex.Message);
                // si ca marche pas on met un timestamp
                return "CLI" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode sert a ajouter un client depuis la console
        /// elle demande toutes les infos necessaires et les valide
        /// puis elle cree l'utilisateur et le client dans la base
        /// </summary>
        public void AjouterClientConsole()
        {
            try
            {
                // on demande toutes les infos avec validation
                string nom = ValidationRequette.DemanderNom("Entrez le nom du client : ");
                string prenom = ValidationRequette.DemanderNom("Entrez le prenom du client : ");
                string adresse = ValidationRequette.DemanderAdresse("Entrez l'adresse du client : ");
                string email = ValidationRequette.DemanderEmail("Entrez l'email du client : ");
                string telephone = ValidationRequette.DemanderTelephone("Entrez le telephone du client : ");
                string motDePasse = ValidationRequette.DemanderMotDePasse("Entrez le mot de passe du client : ");
                
                // on cree une instance de validation pour la station metro
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du client : ");
                
                // on demande si c'est un particulier ou une entreprise
                int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                
                string typeClientStr = (typeClient == 1) ? "Particulier" : "Entreprise";
                string entrepriseNom = null;
                string referent = null;
                
                // si c'est une entreprise on demande plus d'infos
                if (typeClient == 2)
                {
                    entrepriseNom = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                    referent = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                }
                
                // on genere les ids
                string idUtilisateur = GenererIdUtilisateur();
                string idClient = GenererIdClient();
                
                // on insere dans la table utilisateur
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                try
                {
                    // on insere dans la table client
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
        /// cette methode sert a ajouter un client a partir d'un utilisateur qui existe deja
        /// elle demande l'id de l'utilisateur et les infos specifiques au client
        /// </summary>
        public void AjouterClientExistant()
        {
            try
            {
                // on demande l'id de l'utilisateur
                Console.WriteLine("Entrez l'ID de l'utilisateur existant : ");
                string idUtilisateur = Console.ReadLine();
                
                // on verifie si l'utilisateur existe
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
                
                // on verifie si c'est deja un client
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
                
                // on demande les infos du client
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du client : ");
                
                // on demande le type de client
                Console.WriteLine("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                
                string entrepriseNom = "NULL";
                string referent = "NULL";
                
                // si c'est une entreprise on demande plus d'infos
                if (typeClient == 2)
                {
                    entrepriseNom = "'" + ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ") + "'";
                    referent = "'" + ValidationRequette.DemanderNom("Entrez le nom du référent : ") + "'";
                }
                
                // on genere l'id client
                string idClient = GenererIdClient();
                
                // on insere dans la table client
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
        /// cette methode sert a supprimer un client et son utilisateur
        /// elle supprime d'abord les commandes, puis le client, puis l'utilisateur
        /// </summary>
        public void SupprimerClient()
        {
            try
            {
                Console.WriteLine("Entrez l'ID de l'utilisateur à supprimer : ");
                string id = Console.ReadLine();

                // on verifie si le client existe
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

                // on supprime d'abord les commandes
                string requete2 = "DELETE FROM Commande_ WHERE id_client IN (SELECT id_client FROM client WHERE id_utilisateur='" + id + "')";
                MySqlCommand commande2 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande2.CommandText = requete2;
                commande2.ExecuteNonQuery();

                // on supprime le client
                string requete3 = "DELETE FROM client WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande3 = new MySqlCommand(requete3, connexionBDD.maConnexion);
                commande3.CommandText = requete3;
                commande3.ExecuteNonQuery();

                // on supprime l'utilisateur
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
        /// cette methode sert a modifier les infos d'un client
        /// elle demande les nouvelles infos et met a jour la base
        /// </summary>
        public void ModifierClient()
        {
            try
            {
                Console.WriteLine("Entrez l'ID du client à modifier : ");
                string id = Console.ReadLine();

                // on verifie si le client existe
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

                // on demande les nouvelles infos
                Console.WriteLine("Entrez le nouveau nom : ");
                string nom = Console.ReadLine();

                Console.WriteLine("Entrez le nouveau prénom : ");
                string prenom = Console.ReadLine();

                Console.WriteLine("Entrez la nouvelle adresse : ");
                string adresse = Console.ReadLine();

                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la nouvelle station de métro : ");

                // on met a jour l'utilisateur
                string requete2 = "UPDATE utilisateur SET nom='" + nom + "', prénom='" + prenom + "', adresse='" + adresse + "' WHERE id_utilisateur='" + id + "'";
                MySqlCommand commande2 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande2.CommandText = requete2;
                commande2.ExecuteNonQuery();

                // on met a jour le client
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
        /// cette methode sert a afficher les clients par ordre alphabetique
        /// elle fait une requete qui trie par nom puis prenom
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
        /// cette methode sert a afficher les clients par rue
        /// elle fait une requete qui trie par adresse
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
        /// cette methode sert a afficher les clients par montant des achats
        /// elle fait une requete qui calcule la somme des commandes et trie par montant
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