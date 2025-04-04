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
        /// cette methode sert a generer un id unique pour un cuisinier
        /// elle fait pareil que pour l'utilisateur mais avec CUI au lieu de USR
        /// </summary>
        private string GenererIdCuisinier()
        {
            try
            {
                // on cherche le dernier id cuisinier dans la base
                string sql = "SELECT id_cuisinier FROM cuisinier WHERE id_cuisinier LIKE 'CUI%' ORDER BY id_cuisinier DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si y a pas de cuisinier on commence a 1
                    return "CUI001";
                }
                
                string dernierId = result.ToString();
                // on prend juste le numero a la fin
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // on remet le format CUI001
                return "CUI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id cuisinier : " + ex.Message);
                // si ca marche pas on met un timestamp
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
                
                // on cree une instance de validation pour la station metro
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                
                // on genere les ids
                string idUtilisateur = GenererIdUtilisateur();
                string idCuisinier = GenererIdCuisinier();
                
                // on insere dans la table utilisateur
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                // on insere dans la table cuisinier
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
        /// cette methode sert a ajouter un cuisinier a partir d'un utilisateur qui existe deja
        /// elle demande l'id de l'utilisateur et les infos specifiques au cuisinier
        /// </summary>
        public void AjouterCuisinierExistant()
        {
            try
            {
                // on demande l'id de l'utilisateur
                Console.WriteLine("Entrez l'ID de l'utilisateur existant : ");
                string idUtilisateur = Console.ReadLine();
                
                // on verifie si l'utilisateur existe
                string sqlVerif = "SELECT COUNT(*) FROM utilisateur WHERE id_utilisateur = @id";
                MySqlCommand cmdVerif = new MySqlCommand(sqlVerif, connexionBDD.maConnexion);
                cmdVerif.Parameters.AddWithValue("@id", idUtilisateur);
                
                int count = Convert.ToInt32(cmdVerif.ExecuteScalar());
                
                if (count == 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " n'existe pas dans la base de données.");
                    return;
                }
                
                // on verifie si c'est deja un cuisinier
                string sqlVerifCuisinier = "SELECT COUNT(*) FROM cuisinier WHERE id_utilisateur = @id";
                MySqlCommand cmdVerifCuisinier = new MySqlCommand(sqlVerifCuisinier, connexionBDD.maConnexion);
                cmdVerifCuisinier.Parameters.AddWithValue("@id", idUtilisateur);
                
                int countCuisinier = Convert.ToInt32(cmdVerifCuisinier.ExecuteScalar());
                
                if (countCuisinier > 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " est déjà un cuisinier.");
                    return;
                }
                
                // on demande les infos du cuisinier
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                
                // on demande des zones de livraison
                Console.WriteLine("Entrez les zones de livraison (séparées par des virgules) : ");
                string zonesLivraison = Console.ReadLine();
                
                // on genere l'id cuisinier
                string idCuisinier = GenererIdCuisinier();
                
                // on insere dans la table cuisinier
                string requeteCuisinier = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" + 
                    idCuisinier + "', '" + idUtilisateur + "', '" + stationMetro + "', '" + zonesLivraison + "', 0, 0)";
                
                MySqlCommand cmdCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                cmdCuisinier.ExecuteNonQuery();
                
                Console.WriteLine("Cuisinier ajouté avec succès à partir de l'utilisateur existant !");
                
                cmdVerif.Dispose();
                cmdVerifCuisinier.Dispose();
                cmdCuisinier.Dispose();
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
        /// cette methode sert a modifier les infos d'un cuisinier
        /// elle met a jour le nom, prenom, adresse et station metro
        /// </summary>
        public void ModifierCuisinier(int idCuisinier, string nom, string prenom, string adresse, string stationMetro)
        {
            try
            {
                // on modifie la table utilisateur
                string sqlUtilisateur = "UPDATE utilisateur SET nom = @nom, prenom = @prenom, adresse = @adresse WHERE id_utilisateur = @idCuisinier";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdUtilisateur.Parameters.AddWithValue("@nom", nom);
                cmdUtilisateur.Parameters.AddWithValue("@prenom", prenom);
                cmdUtilisateur.Parameters.AddWithValue("@adresse", adresse);
                cmdUtilisateur.ExecuteNonQuery();

                // on modifie la table cuisinier
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
        /// cette methode sert a afficher les clients servis par un cuisinier
        /// elle fait une requete qui montre tous les clients qui ont commande chez ce cuisinier
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
        /// cette methode sert a afficher les plats realises par un cuisinier
        /// elle compte combien de fois chaque plat a ete commande
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
        /// cette methode sert a afficher le plat du jour d'un cuisinier
        /// elle cherche dans la base le plat du jour pour aujourd'hui
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