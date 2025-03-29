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
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                // verifie si le client existe
                string requeteVerifClient = "SELECT COUNT(*) FROM client WHERE id_utilisateur = @idClient";
                MySqlCommand commandeVerifClient = new MySqlCommand(requeteVerifClient, connexionBDD.maConnexion);
                commandeVerifClient.Parameters.AddWithValue("@idClient", idClient);
                int countClient = Convert.ToInt32(commandeVerifClient.ExecuteScalar());

                if (countClient == 0)
                {
                    throw new Exception("le client n'existe pas");
                }

                // recupere le prix du plat
                string requetePrix = "SELECT prix FROM plat WHERE id_plat = @idPlat";
                MySqlCommand commandePrix = new MySqlCommand(requetePrix, connexionBDD.maConnexion);
                commandePrix.Parameters.AddWithValue("@idPlat", idPlat);
                double prixPlat = Convert.ToDouble(commandePrix.ExecuteScalar());

                // insere la commande
                string requeteCommande = "INSERT INTO commande (id_client, id_cuisinier, id_plat, date_commande, prix_total) " +
                                       "VALUES (@idClient, @idCuisinier, @idPlat, @dateCommande, @prixTotal); " +
                                       "SELECT LAST_INSERT_ID();";
                MySqlCommand commandeCommande = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commandeCommande.Parameters.AddWithValue("@idClient", idClient);
                commandeCommande.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                commandeCommande.Parameters.AddWithValue("@idPlat", idPlat);
                commandeCommande.Parameters.AddWithValue("@dateCommande", dateCommande);
                commandeCommande.Parameters.AddWithValue("@prixTotal", prixPlat);

                int idCommande = Convert.ToInt32(commandeCommande.ExecuteScalar());
                Console.WriteLine("commande creee avec succes");

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                // recupere le prix du nouveau plat
                string requetePrix = "SELECT prix FROM plat WHERE id_plat = @idPlat";
                MySqlCommand commandePrix = new MySqlCommand(requetePrix, connexionBDD.maConnexion);
                commandePrix.Parameters.AddWithValue("@idPlat", idPlat);
                double prixPlat = Convert.ToDouble(commandePrix.ExecuteScalar());

                // modifie la commande
                string requeteCommande = "UPDATE commande SET id_plat = @idPlat, date_commande = @dateCommande, " +
                                       "prix_total = @prixTotal WHERE id_commande = @idCommande";
                MySqlCommand commandeCommande = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commandeCommande.Parameters.AddWithValue("@idCommande", idCommande);
                commandeCommande.Parameters.AddWithValue("@idPlat", idPlat);
                commandeCommande.Parameters.AddWithValue("@dateCommande", dateCommande);
                commandeCommande.Parameters.AddWithValue("@prixTotal", prixPlat);

                commandeCommande.ExecuteNonQuery();
                Console.WriteLine("commande modifiee avec succes");

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT prix_total FROM commande WHERE id_commande = @idCommande";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCommande", idCommande);

                double prix = Convert.ToDouble(commande.ExecuteScalar());
                Console.WriteLine("prix de la commande " + idCommande + ": " + prix + "€");

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT c.station_metro as station_client, cu.station_metro as station_cuisinier " +
                               "FROM commande co " +
                               "INNER JOIN client c ON co.id_client = c.id_utilisateur " +
                               "INNER JOIN cuisinier cu ON co.id_cuisinier = cu.id_utilisateur " +
                               "WHERE co.id_commande = @idCommande";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCommande", idCommande);

                MySqlDataReader reader = commande.ExecuteReader();
                if (reader.Read())
                {
                    string stationDepart = reader["station_cuisinier"].ToString();
                    string stationArrivee = reader["station_client"].ToString();
                    Console.WriteLine("chemin de livraison: de " + stationDepart + " a " + stationArrivee);
                    reader.Close();
                    connexionBDD.Deconnecter();
                    return (stationDepart, stationArrivee);
                }
                else
                {
                    reader.Close();
                    connexionBDD.Deconnecter();
                    throw new Exception("commande non trouvee");
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                // recupere les informations de la commande
                string requete = "SELECT co.*, u.nom as nom_client, u.prenom as prenom_client, " +
                               "p.nom_plat, cu.station_metro as station_cuisinier, c.station_metro as station_client " +
                               "FROM commande co " +
                               "INNER JOIN utilisateur u ON co.id_client = u.id_utilisateur " +
                               "INNER JOIN plat p ON co.id_plat = p.id_plat " +
                               "INNER JOIN cuisinier cu ON co.id_cuisinier = cu.id_utilisateur " +
                               "INNER JOIN client c ON co.id_client = c.id_utilisateur " +
                               "WHERE co.id_commande = @idCommande";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCommande", idCommande);

                MySqlDataReader reader = commande.ExecuteReader();
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
                reader.Close();
                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la simulation des etapes de la commande : " + ex.Message);
            }
        }
    }
} 