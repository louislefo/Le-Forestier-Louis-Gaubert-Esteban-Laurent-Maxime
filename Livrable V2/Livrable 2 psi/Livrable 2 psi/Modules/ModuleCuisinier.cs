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
        private Graphe<int> grapheMetro;

        public ModuleCuisinier(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
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
        /// genere un id unique pour un cuisinier
        /// </summary>
        private string GenererIdCuisinier()
        {
            try
            {
                // recupere le dernier id cuisinier
                string sql = "SELECT id_cuisinier FROM cuisinier WHERE id_cuisinier LIKE 'CUI%' ORDER BY id_cuisinier DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si aucun cuisinier n'existe, commence par CUI001
                    return "CUI001";
                }
                
                string dernierId = result.ToString();
                // extrait le numero
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // formate le nouvel id
                return "CUI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id cuisinier : " + ex.Message);
                // en cas d'erreur, genere un id avec timestamp
                return "CUI" + DateTime.Now.Ticks;
            }
        }
        
        public void AjouterCuisinierConsole()
        {
            try
            {
                // validation des données avec ValidationRequette
                string nom = ValidationRequette.DemanderNom("Entrez le nom du cuisinier : ");
                string prenom = ValidationRequette.DemanderNom("Entrez le prenom du cuisinier : ");
                string adresse = ValidationRequette.DemanderAdresse("Entrez l'adresse du cuisinier : ");
                string email = ValidationRequette.DemanderEmail("Entrez l'email du cuisinier : ");
                string telephone = ValidationRequette.DemanderTelephone("Entrez le telephone du cuisinier : ");
                string motDePasse = ValidationRequette.DemanderMotDePasse("Entrez le mot de passe du cuisinier : ");
                
                // creation d'une instance de ValidationRequette avec le graphe
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                
                // generation d'un id unique pour l'utilisateur et le cuisinier
                string idUtilisateur = GenererIdUtilisateur();
                string idCuisinier = GenererIdCuisinier();
                
                // insertion dans la table utilisateur
                string requeteUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                // insertion dans la table cuisinier
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
        /// ajoute un cuisinier a partir d un utilisateur existant
        /// </summary>
        public void AjouterCuisinierExistant()
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
                
                // verifie si l'utilisateur est deja un cuisinier
                string sqlVerifCuisinier = "SELECT COUNT(*) FROM cuisinier WHERE id_utilisateur = @id";
                MySqlCommand cmdVerifCuisinier = new MySqlCommand(sqlVerifCuisinier, connexionBDD.maConnexion);
                cmdVerifCuisinier.Parameters.AddWithValue("@id", idUtilisateur);
                
                int countCuisinier = Convert.ToInt32(cmdVerifCuisinier.ExecuteScalar());
                
                if (countCuisinier > 0)
                {
                    Console.WriteLine("L'utilisateur avec l'ID " + idUtilisateur + " est déjà un cuisinier.");
                    return;
                }
                
                // demande les informations du cuisinier
                ValidationRequette validation = new ValidationRequette(grapheMetro);
                string stationMetro = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                
                // demande des zones de livraison
                Console.WriteLine("Entrez les zones de livraison (séparées par des virgules) : ");
                string zonesLivraison = Console.ReadLine();
                
                // generation d'un id unique pour le cuisinier
                string idCuisinier = GenererIdCuisinier();
                
                // insertion dans la table cuisinier
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

        public void SupprimerCuisinier(string idCuisinier)
        {
            try
            {
                // supprime d'abord de la table cuisinier
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