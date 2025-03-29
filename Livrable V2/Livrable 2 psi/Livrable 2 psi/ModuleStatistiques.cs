namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les statistiques
    /// </summary>
    public class ModuleStatistiques
    {
        private string connectionString;

        public ModuleStatistiques(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// affiche le nombre de livraisons par cuisinier
        /// </summary>
        public void AfficherLivraisonsParCuisinier()
        {
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT u.nom, u.prenom, COUNT(*) as nombre_livraisons " +
                               "FROM commande c " +
                               "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                               "INNER JOIN utilisateur u ON cu.id_utilisateur = u.id_utilisateur " +
                               "GROUP BY u.nom, u.prenom " +
                               "ORDER BY nombre_livraisons DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nNombre de livraisons par cuisinier :");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine(reader["prenom"] + " " + reader["nom"] + ": " + 
                                   reader["nombre_livraisons"] + " livraisons");
                }
                Console.WriteLine("----------------------------------------");
                reader.Close();

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT c.*, u.nom as nom_client, u.prenom as prenom_client, " +
                               "p.nom_plat, cu.nom as nom_cuisinier, cu.prenom as prenom_cuisinier " +
                               "FROM commande c " +
                               "INNER JOIN utilisateur u ON c.id_client = u.id_utilisateur " +
                               "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                               "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                               "WHERE c.date_commande BETWEEN @dateDebut AND @dateFin " +
                               "ORDER BY c.date_commande";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@dateDebut", dateDebut);
                commande.Parameters.AddWithValue("@dateFin", dateFin);

                MySqlDataReader reader = commande.ExecuteReader();

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
                reader.Close();

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT AVG(prix_total) as moyenne_prix FROM commande";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                double moyenne = Convert.ToDouble(commande.ExecuteScalar());

                Console.WriteLine("\nMoyenne des prix des commandes : " + moyenne.ToString("F2") + "€");

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT AVG(solde) as moyenne_solde FROM client";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                double moyenne = Convert.ToDouble(commande.ExecuteScalar());

                Console.WriteLine("\nMoyenne des soldes des comptes clients : " + moyenne.ToString("F2") + "€");

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT p.type_plat, COUNT(*) as nombre_commandes " +
                               "FROM commande c " +
                               "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                               "WHERE c.date_commande BETWEEN @dateDebut AND @dateFin " +
                               "GROUP BY p.type_plat " +
                               "ORDER BY nombre_commandes DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@dateDebut", dateDebut);
                commande.Parameters.AddWithValue("@dateFin", dateFin);

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nCommandes par type de plat entre " + dateDebut.ToShortDateString() + 
                               " et " + dateFin.ToShortDateString() + " :");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine(reader["type_plat"] + ": " + reader["nombre_commandes"] + " commandes");
                }
                Console.WriteLine("----------------------------------------");
                reader.Close();

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                // plat le plus commande par jour
                string requetePlatJour = "SELECT TOP 1 p.nom_plat, COUNT(*) as nombre_commandes " +
                                       "FROM commande c " +
                                       "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                                       "WHERE CAST(c.date_commande AS DATE) = CAST(GETDATE() AS DATE) " +
                                       "GROUP BY p.nom_plat " +
                                       "ORDER BY nombre_commandes DESC";

                MySqlCommand commandePlatJour = new MySqlCommand(requetePlatJour, connexionBDD.maConnexion);
                MySqlDataReader reader = commandePlatJour.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("\nPlat le plus commande aujourd'hui : " + reader["nom_plat"] + 
                                   " (" + reader["nombre_commandes"] + " commandes)");
                }
                reader.Close();

                // cuisinier le plus populaire
                string requeteCuisinierPop = "SELECT TOP 1 u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                                           "FROM commande c " +
                                           "INNER JOIN cuisinier cu ON c.id_cuisinier = cu.id_utilisateur " +
                                           "INNER JOIN utilisateur u ON cu.id_utilisateur = u.id_utilisateur " +
                                           "GROUP BY u.nom, u.prenom " +
                                           "ORDER BY nombre_commandes DESC";

                MySqlCommand commandeCuisinierPop = new MySqlCommand(requeteCuisinierPop, connexionBDD.maConnexion);
                reader = commandeCuisinierPop.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Cuisinier le plus populaire : " + reader["prenom"] + " " + 
                                   reader["nom"] + " (" + reader["nombre_commandes"] + " commandes)");
                }
                reader.Close();

                // client le plus fidele
                string requeteClientFidele = "SELECT TOP 1 u.nom, u.prenom, COUNT(*) as nombre_commandes " +
                                           "FROM commande c " +
                                           "INNER JOIN client cl ON c.id_client = cl.id_utilisateur " +
                                           "INNER JOIN utilisateur u ON cl.id_utilisateur = u.id_utilisateur " +
                                           "GROUP BY u.nom, u.prenom " +
                                           "ORDER BY nombre_commandes DESC";

                MySqlCommand commandeClientFidele = new MySqlCommand(requeteClientFidele, connexionBDD.maConnexion);
                reader = commandeClientFidele.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Client le plus fidele : " + reader["prenom"] + " " + 
                                   reader["nom"] + " (" + reader["nombre_commandes"] + " commandes)");
                }
                reader.Close();

                // plat le plus rentable
                string requetePlatRentable = "SELECT TOP 1 p.nom_plat, SUM(c.prix_total) as chiffre_affaires " +
                                           "FROM commande c " +
                                           "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                                           "GROUP BY p.nom_plat " +
                                           "ORDER BY chiffre_affaires DESC";

                MySqlCommand commandePlatRentable = new MySqlCommand(requetePlatRentable, connexionBDD.maConnexion);
                reader = commandePlatRentable.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Plat le plus rentable : " + reader["nom_plat"] + 
                                   " (" + reader["chiffre_affaires"] + "€ de chiffre d'affaires)");
                }
                reader.Close();

                // heure de pointe des commandes
                string requeteHeurePointe = "SELECT TOP 1 DATEPART(HOUR, date_commande) as heure, " +
                                          "COUNT(*) as nombre_commandes " +
                                          "FROM commande " +
                                          "GROUP BY DATEPART(HOUR, date_commande) " +
                                          "ORDER BY nombre_commandes DESC";

                MySqlCommand commandeHeurePointe = new MySqlCommand(requeteHeurePointe, connexionBDD.maConnexion);
                reader = commandeHeurePointe.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Heure de pointe des commandes : " + reader["heure"] + "h " +
                                   "(" + reader["nombre_commandes"] + " commandes)");
                }
                reader.Close();

                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des statistiques creatives : " + ex.Message);
            }
        }
    }
} 