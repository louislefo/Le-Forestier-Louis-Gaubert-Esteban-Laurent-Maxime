using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les operations sur les commandes
    /// </summary>
    public class ModuleCommande
    {
        public ConnexionBDD connexionBDD;

        public ModuleCommande(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
        }

        /// <summary>
        /// cree une nouvelle commande
        /// </summary>
        public int CreerCommande(int idClient, int idCuisinier, int idPlat, DateTime dateCommande)
        {
            try
            {
                // verifie si le client existe
                string sqlVerifClient = "SELECT COUNT(*) FROM client WHERE id_utilisateur = @idClient";
                MySqlCommand cmdVerifClient = new MySqlCommand(sqlVerifClient, connexionBDD.maConnexion);
                cmdVerifClient.Parameters.AddWithValue("@idClient", idClient);
                int countClient = Convert.ToInt32(cmdVerifClient.ExecuteScalar());

                if (countClient == 0)
                {
                    throw new Exception("le client n'existe pas");
                }

                // recupere le prix du plat
                string sqlPrix = "SELECT prix FROM plat WHERE id_plat = @idPlat";
                MySqlCommand cmdPrix = new MySqlCommand(sqlPrix, connexionBDD.maConnexion);
                cmdPrix.Parameters.AddWithValue("@idPlat", idPlat);
                double prixPlat = Convert.ToDouble(cmdPrix.ExecuteScalar());

                // insere la commande
                string sqlCommande = "INSERT INTO commande (id_client, id_cuisinier, id_plat, date_commande, prix_total) " +
                                   "VALUES (@idClient, @idCuisinier, @idPlat, @dateCommande, @prixTotal); " +
                                   "SELECT LAST_INSERT_ID();";
                MySqlCommand cmdCommande = new MySqlCommand(sqlCommande, connexionBDD.maConnexion);
                cmdCommande.Parameters.AddWithValue("@idClient", idClient);
                cmdCommande.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCommande.Parameters.AddWithValue("@idPlat", idPlat);
                cmdCommande.Parameters.AddWithValue("@dateCommande", dateCommande);
                cmdCommande.Parameters.AddWithValue("@prixTotal", prixPlat);

                int idCommande = Convert.ToInt32(cmdCommande.ExecuteScalar());
                Console.WriteLine("commande creee avec succes");

                cmdVerifClient.Dispose();
                cmdPrix.Dispose();
                cmdCommande.Dispose();

                return idCommande;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la creation de la commande : " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// modifie une commande
        /// </summary>
        public void ModifierCommande(int idCommande, int idPlat, DateTime dateCommande)
        {
            try
            {
                // recupere le prix du nouveau plat
                string sqlPrix = "SELECT prix FROM plat WHERE id_plat = @idPlat";
                MySqlCommand cmdPrix = new MySqlCommand(sqlPrix, connexionBDD.maConnexion);
                cmdPrix.Parameters.AddWithValue("@idPlat", idPlat);
                double prixPlat = Convert.ToDouble(cmdPrix.ExecuteScalar());

                // modifie la commande
                string sqlCommande = "UPDATE commande SET id_plat = @idPlat, date_commande = @dateCommande, " +
                                   "prix_total = @prixTotal WHERE id_commande = @idCommande";
                MySqlCommand cmdCommande = new MySqlCommand(sqlCommande, connexionBDD.maConnexion);
                cmdCommande.Parameters.AddWithValue("@idCommande", idCommande);
                cmdCommande.Parameters.AddWithValue("@idPlat", idPlat);
                cmdCommande.Parameters.AddWithValue("@dateCommande", dateCommande);
                cmdCommande.Parameters.AddWithValue("@prixTotal", prixPlat);

                cmdCommande.ExecuteNonQuery();
                Console.WriteLine("commande modifiee avec succes");

                cmdPrix.Dispose();
                cmdCommande.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la modification de la commande : " + ex.Message);
            }
        }

        /// <summary>
        /// calcule le prix d'une commande
        /// </summary>
        public double CalculerPrixCommande(int idCommande)
        {
            try
            {
                string sql = "SELECT prix_total FROM commande WHERE id_commande = @idCommande";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);

                double prix = Convert.ToDouble(cmd.ExecuteScalar());
                Console.WriteLine("prix de la commande " + idCommande + ": " + prix + "€");

                cmd.Dispose();
                return prix;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul du prix de la commande : " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// determine le chemin de livraison
        /// </summary>
        public (string stationDepart, string stationArrivee) DeterminerCheminLivraison(int idCommande)
        {
            try
            {
                string sql = "SELECT c.station_metro as station_client, cu.station_metro as station_cuisinier " +
                           "FROM commande co " +
                           "INNER JOIN client c ON co.id_client = c.id_utilisateur " +
                           "INNER JOIN cuisinier cu ON co.id_cuisinier = cu.id_utilisateur " +
                           "WHERE co.id_commande = @idCommande";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string stationDepart = reader["station_cuisinier"].ToString();
                        string stationArrivee = reader["station_client"].ToString();
                        Console.WriteLine("chemin de livraison: de " + stationDepart + " a " + stationArrivee);
                        return (stationDepart, stationArrivee);
                    }
                    else
                    {
                        throw new Exception("commande non trouvee");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la determination du chemin de livraison : " + ex.Message);
                return (null, null);
            }
        }

        /// <summary>
        /// simule les etapes d'une commande
        /// </summary>
        public void SimulerEtapesCommande(int idCommande)
        {
            try
            {
                // recupere les informations de la commande
                string sql = "SELECT co.*, u.nom as nom_client, u.prenom as prenom_client, " +
                           "p.nom_plat, cu.station_metro as station_cuisinier, c.station_metro as station_client " +
                           "FROM commande co " +
                           "INNER JOIN utilisateur u ON co.id_client = u.id_utilisateur " +
                           "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                           "INNER JOIN cuisinier cu ON co.id_cuisinier = cu.id_utilisateur " +
                           "INNER JOIN client c ON co.id_client = c.id_utilisateur " +
                           "WHERE co.id_commande = @idCommande";

                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nSimulation des etapes de la commande :");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Commande #" + reader["id_commande"]);
                        Console.WriteLine("Client: " + reader["prenom_client"] + " " + reader["nom_client"]);
                        Console.WriteLine("Plat: " + reader["nom_plat"]);
                        Console.WriteLine("Prix: " + reader["prix_total"] + "€");
                        Console.WriteLine("Date: " + reader["date_commande"]);
                        Console.WriteLine("Station depart: " + reader["station_cuisinier"]);
                        Console.WriteLine("Station arrivee: " + reader["station_client"]);
                        Console.WriteLine("----------------------------------------");

                        // simule les etapes
                        Console.WriteLine("1. Commande recue");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("2. Preparation en cours");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("3. Livraison en cours");
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine("4. Commande livree");
                    }
                    else
                    {
                        throw new Exception("commande non trouvee");
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la simulation des etapes de la commande : " + ex.Message);
            }
        }
    }
} 