using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Livrable_2_psi
{
    /// <summary>
    /// cette classe sert a gerer tout ce qui concerne les cuisiniers dans l'application
    /// elle permet d'ajouter des cuisiniers, de les modifier, de les supprimer et de voir leurs plats
    /// c'est une classe importante car elle gere les cuisiniers qui preparent les plats
    /// </summary>
    public class ModuleCuisinier
    {
        public ConnexionBDD connexionBDD;
        private Graphe<int> grapheMetro;

        public ModuleCuisinier(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
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
                string sql = "SELECT id_utilisateur FROM utilisateur WHERE id_utilisateur LIKE 'USR%' ORDER BY id_utilisateur DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    return "USR001";
                }
                
                string dernierId = result.ToString();
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                return "USR" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id utilisateur : " + ex.Message);
                return "USR" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode sert a generer un id unique pour un cuisinier
        /// elle fait pareil que pour l'utilisateur mais avec CUI au lieu de USR
        /// </summary>
        private string GenererIdCuisinier()
        {
            try
            {
                string sql = "SELECT id_cuisinier FROM cuisinier WHERE id_cuisinier LIKE 'CUI%' ORDER BY id_cuisinier DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    return "CUI001";
                }
                
                string dernierId = result.ToString();
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // on remet le format CUI001
                return "CUI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id cuisinier : " + ex.Message);
                return "CUI" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode sert a ajouter un cuisinier depuis la console
        /// elle demande toutes les infos necessaires et les valide
        /// puis elle cree l'utilisateur et le cuisinier dans la base
        /// </summary>
        public void AjouterCuisinierConsole()
        {
            try
            {
                // on demande toutes les infos avec validation
                string nom = ValidationRequette.DemanderNom("Entrez le nom du cuisinier : ");
                string prenom = ValidationRequette.DemanderNom("Entrez le prenom du cuisinier : ");
                string adresse = ValidationRequette.DemanderAdresse("Entrez l'adresse du cuisinier : ");
                string email = ValidationRequette.DemanderEmail("Entrez l'email du cuisinier : ");
                string telephone = ValidationRequette.DemanderTelephone("Entrez le telephone du cuisinier : ");
                string motDePasse = ValidationRequette.DemanderMotDePasse("Entrez le mot de passe du cuisinier : ");
                
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                
                string idUtilisateur = GenererIdUtilisateur();
                string idCuisinier = GenererIdCuisinier();
                
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                string requeteCuisinier = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, note_moyenne, nombre_livraisons) VALUES ('" + 
                    idCuisinier + "', '" + idUtilisateur + "', '" + stationMetro + "', 0, 0)";
                
                MySqlCommand cmdCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                cmdCuisinier.ExecuteNonQuery();
                
                Console.WriteLine("Cuisinier ajouté avec succès !");
                
                cmdUtilisateur.Dispose();
                cmdCuisinier.Dispose();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du cuisinier : " + e.Message);
            }
        }

        /// <summary>
        /// cette methode sert a ajouter un cuisinier a partir d'un utilisateur qui existe
        /// verifie si il existe et si il est pas deja cuisinier
        /// </summary>
        public void AjouterCuisinierExistant()
        {
            try
            {
                Console.WriteLine("Entrez l'ID de l'utilisateur existant : ");
                string idUtilisateur = Console.ReadLine();
                
                string requete = "Select * from utilisateur where id_utilisateur='" + idUtilisateur + "';";
                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();
                
                if (!reader.Read())
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " n'existe pas");
                    reader.Close();
                    commande0.Dispose();
                    return;
                }
                reader.Close();
                commande0.Dispose();
                
                string requete2 = "Select * from cuisinier where id_utilisateur='" + idUtilisateur + "';";
                MySqlCommand commande1 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande1.CommandText = requete2;

                MySqlDataReader reader2 = commande1.ExecuteReader();
                
                if (reader2.Read())
                {
                    Console.WriteLine("L'utilisateur est deja un cuisinier");
                    reader2.Close();
                    commande1.Dispose();
                    return;
                }
                reader2.Close();
                commande1.Dispose();
                
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                
                string idCuisinier = GenererIdCuisinier();
                
                string requete3 = "Insert into cuisinier (id_cuisinier, id_utilisateur, StationMetro, note_moyenne, nombre_livraisons) values ('" + idCuisinier + "', '" + idUtilisateur + "', '" + stationMetro + "', 0, 0);";
                MySqlCommand commande2 = new MySqlCommand(requete3, connexionBDD.maConnexion);
                commande2.CommandText = requete3;
                commande2.ExecuteNonQuery();
                
                Console.WriteLine("Cuisinier ajoute avec succes");
                commande2.Dispose();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur lors de l'ajout du cuisinier : " + e.Message);
            }
        }

        /// <summary>
        /// cette methode sert a supprimer un cuisinier
        /// elle supprime d'abord le cuisinier puis l'utilisateur
        /// </summary>
        public void SupprimerCuisinier(string idCuisinier)
        {
            try
            {
                // on supprime d'abord de la table cuisinier
                string sqlCuisinier = "DELETE FROM cuisinier WHERE id_utilisateur = "+idCuisinier+";";
                MySqlCommand cmdCuisinier = new MySqlCommand(sqlCuisinier, connexionBDD.maConnexion);
                cmdCuisinier.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCuisinier.ExecuteNonQuery();
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
        /// cette methode sert a modifier les infos d'un cuisinier
        /// elle met a jour le nom, prenom, adresse et station metro
        /// </summary>
        public void ModifierCuisinier(string idCuisinier, string nom, string prenom, string adresse, string stationMetro)
        {
            try
            {
                string requete = "Update utilisateur set nom='" + nom + "', prénom='" + prenom + "', adresse='" + adresse + "' where id_utilisateur='" + idCuisinier + "';";
                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;
                commande0.ExecuteNonQuery();

                string requete2 = "Update cuisinier set StationMetro='" + stationMetro + "' where id_utilisateur='" + idCuisinier + "';";
                MySqlCommand commande1 = new MySqlCommand(requete2, connexionBDD.maConnexion);
                commande1.CommandText = requete2;
                commande1.ExecuteNonQuery();

                Console.WriteLine("cuisinier modifie avec succes");

                commande0.Dispose();
                commande1.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la modification du cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a afficher les clients servis par un cuisinier
        /// elle fait une requete qui montre tous les clients qui ont commande chez ce cuisinier
        /// </summary>
        public void AfficherClientsServis(string idCuisinier)
        {
            try
            {
                string requete = "Select * from utilisateur u, client c, Commande_ co where u.id_utilisateur=c.id_utilisateur and c.id_client=co.id_client and co.id_cuisinier='" + idCuisinier + "';";
                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nListe des clients servis :");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine("ID: " + reader["id_utilisateur"]);
                    Console.WriteLine("Nom: " + reader["nom"]);
                    Console.WriteLine("Prenom: " + reader["prénom"]);
                    Console.WriteLine("Adresse: " + reader["adresse"]);
                    Console.WriteLine("----------------------------------------");
                }

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des clients servis : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a afficher les plats realises par un cuisinier
        /// elle compte combien de fois chaque plat a ete commande
        /// </summary>
        public void AfficherPlatsRealises(string idCuisinier)
        {
            try
            {
                string requete = "Select p.nom, count(*) as nombre from Plat_ p, Commande_ co where p.id_plat=co.id_plat and co.id_cuisinier='" + idCuisinier + "' group by p.id_plat, p.nom;";
                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nListe des plats realises :");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine("Plat: " + reader["nom"]);
                    Console.WriteLine("Nombre de fois: " + reader["nombre"]);
                    Console.WriteLine("----------------------------------------");
                }

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des plats realises : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a afficher le plat du jour d'un cuisinier
        /// elle cherche dans la base le plat du jour pour aujourd'hui
        /// </summary>
        public void AfficherPlatDuJour(string idCuisinier)
        {
            try
            {
                string requete = "Select p.* from Plat_ p, plat_du_jour pdj where p.id_plat=pdj.id_plat and pdj.id_cuisinier='" + idCuisinier + "' and pdj.date=CURDATE();";
                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine("\nPlat du jour :");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("ID: " + reader["id_plat"]);
                    Console.WriteLine("Nom: " + reader["nom"]);
                    Console.WriteLine("Description: " + reader["description"]);
                    Console.WriteLine("Prix: " + reader["prix"] + " euros");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nPas de plat du jour aujourd'hui");
                }

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage du plat du jour : " + ex.Message);
            }
        }
    }
} 