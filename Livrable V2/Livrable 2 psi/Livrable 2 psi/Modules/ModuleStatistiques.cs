using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    
    public class ModuleStatistiques
    {
        public ConnexionBDD connexionBDD;

        public ModuleStatistiques(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
        }

        /// <summary>
        /// compte le nombre de livraison par cuisto pas ouf
        /// </summary>
        public void AfficherLivraisonsParCuisinier()
        {
            try
            {
                string requete = "SELECT nom, prénom, nombre_livraisons FROM cuisinier, utilisateur " + 
                               "WHERE cuisinier.id_utilisateur = utilisateur.id_utilisateur";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nvoici les livraisons par cuisinier");
                Console.WriteLine("----------------------------------");

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
        /// affiche les commande entre 2 dates pas ouf
        /// </summary>
        public void AfficherCommandesParPeriode(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                string requete = "SELECT id_commande, date_commande, prix_total FROM Commande_ " + 
                               "WHERE date_commande >= '" + dateDebut.ToString("yyyy-MM-dd") + "' " + 
                               "AND date_commande <= '" + dateFin.ToString("yyyy-MM-dd") + "'";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                Console.WriteLine("\nles commande entre " + dateDebut.ToShortDateString() + " et " + dateFin.ToShortDateString());
                Console.WriteLine("----------------------------------");

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
        /// calcule la moyen des prix des commande pas ouf
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            try
            {
                string requete = "SELECT AVG(prix_total) as moyenne FROM Commande_";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

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
        /// compte combien dargent les client on depense pas ouf
        /// </summary>
        public void AfficherMoyenneComptesClients()
        {
            try
            {
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
        /// compte les commande par type de plat pas ouf
        /// </summary>
        public void AfficherCommandesParTypePlat(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
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