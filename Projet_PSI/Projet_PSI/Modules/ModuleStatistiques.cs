using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    /// <summary>
    /// cette classe sert a gerer tout ce qui concerne les stats dans l'application
    /// elle permet de voir combien de livraisons ont ete faites, les commandes par periode
    /// et plein d'autres stats utiles pour voir comment ca marche
    /// </summary>
    public class ModuleStatistiques
    {
        public ConnexionBDD connexionBDD;

        /// <summary>
        /// on cree le module avec la connexion a la base
        /// comme ca on peut faire des requetes pour les stats
        /// </summary>
        public ModuleStatistiques(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
        }

        /// <summary>
        /// cette methode sert a voir combien de livraisons chaque cuisinier a fait
        /// elle fait une requete qui montre le nom du cuisinier et son nombre de livraisons
        /// </summary>
        public void AfficherLivraisonsParCuisinier()
        {
            try
            {
                // on fait une requete pour avoir les livraisons par cuisinier
                string requete = "SELECT nom, prénom, nombre_livraisons FROM cuisinier, utilisateur " + 
                               "WHERE cuisinier.id_utilisateur = utilisateur.id_utilisateur";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nvoici les livraisons par cuisinier");
                Console.WriteLine("----------------------------------");

                // on affiche chaque cuisinier avec son nombre de livraisons
                while (reader.Read())
                {
                    string nom = reader["nom"].ToString();
                    string prenom = reader["prénom"].ToString();
                    string nbLivraisons = reader["nombre_livraisons"].ToString();
                    Console.WriteLine("le cuisto " + prenom + " " + nom + " a fait " + nbLivraisons + " livraison");
                }
                Console.WriteLine("----------------------------------");

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a voir les commandes entre deux dates
        /// elle montre l'id de la commande, la date et le prix
        /// </summary>
        public void AfficherCommandesParPeriode(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                // on fait une requete pour avoir les commandes entre les deux dates
                string requete = "SELECT id_commande, date_commande, prix_total FROM Commande_ " + 
                               "WHERE date_commande >= '" + dateDebut.ToString("yyyy-MM-dd") + "' " + 
                               "AND date_commande <= '" + dateFin.ToString("yyyy-MM-dd") + "'";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nles commande entre " + dateDebut.ToShortDateString() + " et " + dateFin.ToShortDateString());
                Console.WriteLine("----------------------------------");

                // on affiche chaque commande avec ses infos
                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string date = reader["date_commande"].ToString();
                    string prix = reader["prix_total"].ToString();
                    Console.WriteLine("commande numero " + idCommande + " faite le " + date + " pour " + prix + " euro");
                }
                Console.WriteLine("----------------------------------");

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a calculer la moyenne des prix des commandes
        /// elle fait une requete qui calcule la moyenne et l'affiche
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            try
            {
                // on fait une requete pour avoir la moyenne des prix
                string requete = "SELECT AVG(prix_total) as moyenne FROM Commande_";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                // on affiche la moyenne arrondie a 2 chiffres
                while (reader.Read())
                {
                    double moyenne = Convert.ToDouble(reader["moyenne"]);
                    // on arrondi a 2 chiffres apres la virgule
                    moyenne = Math.Round(moyenne, 2);
                    Console.WriteLine("\nla moyen des commande est de " + moyenne + " euro");
                }

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a voir combien chaque client a depense
        /// elle fait une requete qui calcule le total des commandes par client
        /// </summary>
        public void AfficherMoyenneComptesClients()
        {
            try
            {
                // on fait une requete pour avoir le total depense par client
                string requete = "SELECT nom, prénom, SUM(montant) as total FROM Transaction_, Commande_, client, utilisateur " + 
                               "WHERE Transaction_.id_commande = Commande_.id_commande " + 
                               "AND Commande_.id_client = client.id_client " + 
                               "AND client.id_utilisateur = utilisateur.id_utilisateur " + 
                               "GROUP BY nom, prénom";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nvoila combien les client on depense");
                Console.WriteLine("----------------------------------");

                // on affiche chaque client avec son total depense
                while (reader.Read())
                {
                    string nom = reader["nom"].ToString();
                    string prenom = reader["prénom"].ToString();
                    double total = Convert.ToDouble(reader["total"]);
                    // on arrondi a 2 chiffres apres la virgule
                    total = Math.Round(total, 2);
                    Console.WriteLine(prenom + " " + nom + " a depense " + total + " euro en tout");
                }
                Console.WriteLine("----------------------------------");

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a voir combien de commandes il y a eu par type de plat
        /// elle compte les commandes entre deux dates et les groupe par type de plat
        /// </summary>
        public void AfficherCommandesParTypePlat(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                // on fait une requete pour avoir le nombre de commandes par type de plat
                string requete = "SELECT type as type_plat, COUNT(*) as nombre FROM Plat_, Commande_ " + 
                               "WHERE Plat_.id_plat = Commande_.id_plat " + 
                               "AND date_commande >= '" + dateDebut.ToString("yyyy-MM-dd") + "' " + 
                               "AND date_commande <= '" + dateFin.ToString("yyyy-MM-dd") + "' " + 
                               "GROUP BY type";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nvoici les commande par type");
                Console.WriteLine("----------------------------------");

                // on affiche chaque type de plat avec son nombre de commandes
                while (reader.Read())
                {
                    string type = reader["type_plat"].ToString();
                    string nombre = reader["nombre"].ToString();
                    Console.WriteLine("ya eu " + nombre + " commande de " + type);
                }
                Console.WriteLine("----------------------------------");

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }
    }
} 