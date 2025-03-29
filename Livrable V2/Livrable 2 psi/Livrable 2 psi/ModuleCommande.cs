using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les operations sur les commandes
    /// </summary>
    public class ModuleCommande
    {
        private string connectionString;

        public ModuleCommande(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// cree une nouvelle commande
        /// </summary>
        public int CreerCommande(int idClient, int idCuisinier, int idPlat, DateTime dateCommande)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // verifie si le client existe
                string sqlVerifClient = "SELECT COUNT(*) FROM client WHERE id_utilisateur = @idClient";
                SqlCommand cmdVerifClient = new SqlCommand(sqlVerifClient, conn);
                cmdVerifClient.Parameters.AddWithValue("@idClient", idClient);
                int countClient = Convert.ToInt32(cmdVerifClient.ExecuteScalar());

                if (countClient == 0)
                {
                    throw new Exception("le client n'existe pas");
                }

                // recupere le prix du plat
                string sqlPrix = "SELECT prix FROM plat WHERE id_plat = @idPlat";
                SqlCommand cmdPrix = new SqlCommand(sqlPrix, conn);
                cmdPrix.Parameters.AddWithValue("@idPlat", idPlat);
                double prixPlat = Convert.ToDouble(cmdPrix.ExecuteScalar());

                // insere la commande
                string sqlCommande = "INSERT INTO commande (id_client, id_cuisinier, id_plat, date_commande, prix_total) " +
                                   "VALUES (@idClient, @idCuisinier, @idPlat, @dateCommande, @prixTotal); " +
                                   "SELECT SCOPE_IDENTITY();";
                SqlCommand cmdCommande = new SqlCommand(sqlCommande, conn);
                cmdCommande.Parameters.AddWithValue("@idClient", idClient);
                cmdCommande.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                cmdCommande.Parameters.AddWithValue("@idPlat", idPlat);
                cmdCommande.Parameters.AddWithValue("@dateCommande", dateCommande);
                cmdCommande.Parameters.AddWithValue("@prixTotal", prixPlat);

                int idCommande = Convert.ToInt32(cmdCommande.ExecuteScalar());
                Console.WriteLine("commande creee avec succes");
                return idCommande;
            }
        }

        /// <summary>
        /// modifie une commande
        /// </summary>
        public void ModifierCommande(int idCommande, int idPlat, DateTime dateCommande)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // recupere le prix du nouveau plat
                string sqlPrix = "SELECT prix FROM plat WHERE id_plat = @idPlat";
                SqlCommand cmdPrix = new SqlCommand(sqlPrix, conn);
                cmdPrix.Parameters.AddWithValue("@idPlat", idPlat);
                double prixPlat = Convert.ToDouble(cmdPrix.ExecuteScalar());

                // modifie la commande
                string sqlCommande = "UPDATE commande SET id_plat = @idPlat, date_commande = @dateCommande, " +
                                   "prix_total = @prixTotal WHERE id_commande = @idCommande";
                SqlCommand cmdCommande = new SqlCommand(sqlCommande, conn);
                cmdCommande.Parameters.AddWithValue("@idCommande", idCommande);
                cmdCommande.Parameters.AddWithValue("@idPlat", idPlat);
                cmdCommande.Parameters.AddWithValue("@dateCommande", dateCommande);
                cmdCommande.Parameters.AddWithValue("@prixTotal", prixPlat);

                cmdCommande.ExecuteNonQuery();
                Console.WriteLine("commande modifiee avec succes");
            }
        }

        /// <summary>
        /// calcule le prix d'une commande
        /// </summary>
        public double CalculerPrixCommande(int idCommande)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT prix_total FROM commande WHERE id_commande = @idCommande";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);

                double prix = Convert.ToDouble(cmd.ExecuteScalar());
                Console.WriteLine($"prix de la commande {idCommande}: {prix}€");
                return prix;
            }
        }

        /// <summary>
        /// determine le chemin de livraison
        /// </summary>
        public (string stationDepart, string stationArrivee) DeterminerCheminLivraison(int idCommande)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT c.station_metro as station_client, cu.station_metro as station_cuisinier " +
                           "FROM commande co " +
                           "INNER JOIN client c ON co.id_client = c.id_utilisateur " +
                           "INNER JOIN cuisinier cu ON co.id_cuisinier = cu.id_utilisateur " +
                           "WHERE co.id_commande = @idCommande";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string stationDepart = reader["station_cuisinier"].ToString();
                        string stationArrivee = reader["station_client"].ToString();
                        Console.WriteLine($"chemin de livraison: de {stationDepart} a {stationArrivee}");
                        return (stationDepart, stationArrivee);
                    }
                    else
                    {
                        throw new Exception("commande non trouvee");
                    }
                }
            }
        }

        /// <summary>
        /// simule les etapes d'une commande
        /// </summary>
        public void SimulerEtapesCommande(int idCommande)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // recupere les informations de la commande
                string sql = "SELECT co.*, u.nom as nom_client, u.prenom as prenom_client, " +
                           "p.nom_plat, cu.station_metro as station_cuisinier, c.station_metro as station_client " +
                           "FROM commande co " +
                           "INNER JOIN utilisateur u ON co.id_client = u.id_utilisateur " +
                           "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                           "INNER JOIN cuisinier cu ON co.id_cuisinier = cu.id_utilisateur " +
                           "INNER JOIN client c ON co.id_client = c.id_utilisateur " +
                           "WHERE co.id_commande = @idCommande";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nSimulation des etapes de la commande :");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine($"Commande #{idCommande}");
                        Console.WriteLine($"Client: {reader["prenom_client"]} {reader["nom_client"]}");
                        Console.WriteLine($"Plat: {reader["nom_plat"]}");
                        Console.WriteLine($"Prix: {reader["prix_total"]}€");
                        Console.WriteLine($"Date: {reader["date_commande"]}");
                        Console.WriteLine($"Station depart: {reader["station_cuisinier"]}");
                        Console.WriteLine($"Station arrivee: {reader["station_client"]}");
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
            }
        }
    }
} 