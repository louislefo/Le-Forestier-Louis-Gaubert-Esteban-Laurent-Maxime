using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les statistiques
    /// </summary>
    public class ModuleStatistiques
    {
        public ConnexionBDD connexionBDD;

        public ModuleStatistiques(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
        }

        /// <summary>
        /// affiche le nombre de livraisons par cuisinier
        /// </summary>
        public void AfficherLivraisonsParCuisinier()
        {
            try
            {
                string sql = "SELECT u.nom, u.prenom, COUNT(*) as nombre_livraisons " +
                           "FROM commande c " +
                           "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                           "INNER JOIN utilisateur u ON cu.id_utilisateur = u.id_utilisateur " +
                           "GROUP BY u.nom, u.prenom " +
                           "ORDER BY nombre_livraisons DESC";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nNombre de livraisons par cuisinier :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["prenom"] + " " + reader["nom"] + ": " + 
                                       reader["nombre_livraisons"] + " livraisons");
                    }
                    Console.WriteLine("----------------------------------------");
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des livraisons par cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les commandes par periode
        /// </summary>
        public void AfficherCommandesParPeriode(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                string sql = "SELECT c.*, u.nom as nom_client, u.prenom as prenom_client, " +
                           "p.nom_plat, cu.nom as nom_cuisinier, cu.prenom as prenom_cuisinier " +
                           "FROM commande c " +
                           "INNER JOIN utilisateur u ON c.id_client = u.id_utilisateur " +
                           "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                           "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                           "WHERE c.date_commande BETWEEN @dateDebut AND @dateFin " +
                           "ORDER BY c.date_commande";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@dateFin", dateFin);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nCommandes entre " + dateDebut.ToShortDateString() + " et " + 
                                   dateFin.ToShortDateString() + " :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("Commande #" + reader["id_commande"]);
                        Console.WriteLine("Date: " + reader["date_commande"]);
                        Console.WriteLine("Client: " + reader["prenom_client"] + " " + reader["nom_client"]);
                        Console.WriteLine("Cuisinier: " + reader["prenom_cuisinier"] + " " + reader["nom_cuisinier"]);
                        Console.WriteLine("Plat: " + reader["nom_plat"]);
                        Console.WriteLine("Prix: " + reader["prix_total"] + "€");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des commandes par periode : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche la moyenne des prix des commandes
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            try
            {
                string sql = "SELECT AVG(prix_total) as moyenne_prix FROM commande";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                double moyenne = Convert.ToDouble(cmd.ExecuteScalar());

                Console.WriteLine("\nMoyenne des prix des commandes : " + moyenne.ToString("F2") + "€");
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul de la moyenne des prix : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche la moyenne des comptes clients
        /// </summary>
        public void AfficherMoyenneComptesClients()
        {
            try
            {
                string sql = "SELECT AVG(solde) as moyenne_solde FROM client";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                double moyenne = Convert.ToDouble(cmd.ExecuteScalar());

                Console.WriteLine("\nMoyenne des soldes des comptes clients : " + moyenne.ToString("F2") + "€");
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul de la moyenne des comptes : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les commandes par type de plat
        /// </summary>
        public void AfficherCommandesParTypePlat(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                string sql = "SELECT p.type_plat, COUNT(*) as nombre_commandes " +
                           "FROM commande c " +
                           "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                           "WHERE c.date_commande BETWEEN @dateDebut AND @dateFin " +
                           "GROUP BY p.type_plat " +
                           "ORDER BY nombre_commandes DESC";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@dateFin", dateFin);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nCommandes par type de plat entre " + dateDebut.ToShortDateString() + 
                                   " et " + dateFin.ToShortDateString() + " :");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["type_plat"] + ": " + reader["nombre_commandes"] + " commandes");
                    }
                    Console.WriteLine("----------------------------------------");
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des commandes par type de plat : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche des statistiques creatives
        /// </summary>
        public void AfficherStatistiquesCreatives()
        {
            try
            {
                // plat le plus commande par jour
                string sqlPlatJour = "SELECT p.nom_plat, COUNT(*) as nombre_commandes " +
                                   "FROM commande c " +
                                   "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                                   "WHERE DATE(c.date_commande) = CURDATE() " +
                                   "GROUP BY p.nom_plat " +
                                   "ORDER BY nombre_commandes DESC " +
                                   "LIMIT 1";

                MySqlCommand cmdPlatJour = new MySqlCommand(sqlPlatJour, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmdPlatJour.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nPlat le plus commande aujourd'hui : " + reader["nom_plat"] + 
                                       " (" + reader["nombre_commandes"] + " commandes)");
                    }
                }
                cmdPlatJour.Dispose();

                // cuisinier le plus populaire
                string sqlCuisinierPop = "SELECT u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                                       "FROM commande c " +
                                       "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                                       "INNER JOIN utilisateur u ON cu.id_utilisateur = u.id_utilisateur " +
                                       "GROUP BY u.nom, u.prenom " +
                                       "ORDER BY nombre_commandes DESC " +
                                       "LIMIT 1";

                MySqlCommand cmdCuisinierPop = new MySqlCommand(sqlCuisinierPop, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmdCuisinierPop.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Cuisinier le plus populaire : " + reader["prenom"] + " " + 
                                       reader["nom"] + " (" + reader["nombre_commandes"] + " commandes)");
                    }
                }
                cmdCuisinierPop.Dispose();

                // client le plus fidele
                string sqlClientFidele = "SELECT u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                                       "FROM commande c " +
                                       "INNER JOIN client cl ON c.id_client = cl.id_utilisateur " +
                                       "INNER JOIN utilisateur u ON cl.id_utilisateur = u.id_utilisateur " +
                                       "GROUP BY u.nom, u.prenom " +
                                       "ORDER BY nombre_commandes DESC " +
                                       "LIMIT 1";

                MySqlCommand cmdClientFidele = new MySqlCommand(sqlClientFidele, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmdClientFidele.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Client le plus fidele : " + reader["prenom"] + " " + 
                                       reader["nom"] + " (" + reader["nombre_commandes"] + " commandes)");
                    }
                }
                cmdClientFidele.Dispose();

                // plat le plus rentable
                string sqlPlatRentable = "SELECT p.nom_plat, SUM(c.prix_total) as chiffre_affaires " +
                                       "FROM commande c " +
                                       "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                                       "GROUP BY p.nom_plat " +
                                       "ORDER BY chiffre_affaires DESC " +
                                       "LIMIT 1";

                MySqlCommand cmdPlatRentable = new MySqlCommand(sqlPlatRentable, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmdPlatRentable.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Plat le plus rentable : " + reader["nom_plat"] + 
                                       " (" + reader["chiffre_affaires"] + "€ de chiffre d'affaires)");
                    }
                }
                cmdPlatRentable.Dispose();

                // heure de pointe des commandes
                string sqlHeurePointe = "SELECT HOUR(date_commande) as heure, " +
                                      "COUNT(*) as nombre_commandes " +
                                      "FROM commande " +
                                      "GROUP BY HOUR(date_commande) " +
                                      "ORDER BY nombre_commandes DESC " +
                                      "LIMIT 1";

                MySqlCommand cmdHeurePointe = new MySqlCommand(sqlHeurePointe, connexionBDD.maConnexion);
                using (MySqlDataReader reader = cmdHeurePointe.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Heure de pointe des commandes : " + reader["heure"] + "h " +
                                       "(" + reader["nombre_commandes"] + " commandes)");
                    }
                }
                cmdHeurePointe.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des statistiques creatives : " + ex.Message);
            }
        }
    }
} 