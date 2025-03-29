namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les operations sur les cuisiniers
    /// </summary>
    public class ModuleCuisinier
    {
        private string connectionString;

        public ModuleCuisinier(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// ajoute un cuisinier depuis la console
        /// </summary>
        public void AjouterCuisinierConsole()
        {
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                Console.WriteLine("entrez le nom du cuisinier :");
                string nom = Console.ReadLine();
                Console.WriteLine("entrez le prenom du cuisinier :");
                string prenom = Console.ReadLine();
                Console.WriteLine("entrez l'adresse du cuisinier :");
                string adresse = Console.ReadLine();
                Console.WriteLine("entrez la station de metro la plus proche :");
                string stationMetro = Console.ReadLine();

                string requete = "INSERT INTO utilisateur (nom, prenom, adresse, station_metro) VALUES (@nom, @prenom, @adresse, @stationMetro); " +
                               "SELECT LAST_INSERT_ID();";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@stationMetro", stationMetro);

                int idUtilisateur = Convert.ToInt32(commande.ExecuteScalar());

                string requeteCuisinier = "INSERT INTO cuisinier (id_utilisateur) VALUES (@idUtilisateur)";
                MySqlCommand commandeCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                commandeCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);

                commandeCuisinier.ExecuteNonQuery();
                Console.WriteLine("cuisinier ajoute avec succes");

                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'ajout du cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// ajoute des cuisiniers depuis un fichier
        /// </summary>
        public void AjouterCuisiniersFichier(string cheminFichier)
        {
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string[] lignes = File.ReadAllLines(cheminFichier);
                foreach (string ligne in lignes)
                {
                    string[] donnees = ligne.Split(',');
                    if (donnees.Length >= 4)
                    {
                        string nom = donnees[0].Trim();
                        string prenom = donnees[1].Trim();
                        string adresse = donnees[2].Trim();
                        string stationMetro = donnees[3].Trim();

                        string requete = "INSERT INTO utilisateur (nom, prenom, adresse, station_metro) VALUES (@nom, @prenom, @adresse, @stationMetro); " +
                                       "SELECT LAST_INSERT_ID();";

                        MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                        commande.Parameters.AddWithValue("@nom", nom);
                        commande.Parameters.AddWithValue("@prenom", prenom);
                        commande.Parameters.AddWithValue("@adresse", adresse);
                        commande.Parameters.AddWithValue("@stationMetro", stationMetro);

                        int idUtilisateur = Convert.ToInt32(commande.ExecuteScalar());

                        string requeteCuisinier = "INSERT INTO cuisinier (id_utilisateur) VALUES (@idUtilisateur)";
                        MySqlCommand commandeCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                        commandeCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);

                        commandeCuisinier.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("cuisiniers ajoutes avec succes");

                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'ajout des cuisiniers : " + ex.Message);
            }
        }

        /// <summary>
        /// supprime un cuisinier
        /// </summary>
        public void SupprimerCuisinier(int idCuisinier)
        {
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "DELETE FROM cuisinier WHERE id_utilisateur = @idCuisinier";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                commande.ExecuteNonQuery();

                string requeteUtilisateur = "DELETE FROM utilisateur WHERE id_utilisateur = @idCuisinier";
                MySqlCommand commandeUtilisateur = new MySqlCommand(requeteUtilisateur, connexionBDD.maConnexion);
                commandeUtilisateur.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                commandeUtilisateur.ExecuteNonQuery();

                Console.WriteLine("cuisinier supprime avec succes");

                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la suppression du cuisinier : " + ex.Message);
            }
        }

        /// <summary>
        /// modifie un cuisinier
        /// </summary>
        public void ModifierCuisinier(int idCuisinier, string nom, string prenom, string adresse, string stationMetro)
        {
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "UPDATE utilisateur SET nom = @nom, prenom = @prenom, adresse = @adresse, " +
                               "station_metro = @stationMetro WHERE id_utilisateur = @idCuisinier";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@stationMetro", stationMetro);

                commande.ExecuteNonQuery();
                Console.WriteLine("cuisinier modifie avec succes");

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT DISTINCT u.nom, u.prenom, c.date_commande " +
                               "FROM commande c " +
                               "INNER JOIN client cl ON c.id_client = cl.id_utilisateur " +
                               "INNER JOIN utilisateur u ON cl.id_utilisateur = u.id_utilisateur " +
                               "WHERE c.id_cuisinier = @idCuisinier";

                if (dateDebut.HasValue && dateFin.HasValue)
                {
                    requete += " AND c.date_commande BETWEEN @dateDebut AND @dateFin";
                }

                requete += " ORDER BY c.date_commande DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCuisinier", idCuisinier);
                if (dateDebut.HasValue && dateFin.HasValue)
                {
                    commande.Parameters.AddWithValue("@dateDebut", dateDebut.Value);
                    commande.Parameters.AddWithValue("@dateFin", dateFin.Value);
                }

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nClients servis par le cuisinier :");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine("Client: " + reader["prenom"] + " " + reader["nom"]);
                    Console.WriteLine("Date de commande: " + reader["date_commande"]);
                    Console.WriteLine("----------------------------------------");
                }
                reader.Close();

                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage des clients servis : " + ex.Message);
            }
        }

        /// <summary>
        /// affiche les plats realises par un cuisinier
        /// </summary>
        public void AfficherPlatsRealises(int idCuisinier)
        {
            try
            {
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT p.nom_plat, COUNT(*) as nombre_commandes " +
                               "FROM commande c " +
                               "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                               "WHERE c.id_cuisinier = @idCuisinier " +
                               "GROUP BY p.nom_plat " +
                               "ORDER BY nombre_commandes DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCuisinier", idCuisinier);

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nPlats realises par le cuisinier :");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine(reader["nom_plat"] + ": " + reader["nombre_commandes"] + " commandes");
                }
                Console.WriteLine("----------------------------------------");
                reader.Close();

                connexionBDD.Deconnecter();
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
                ConnexionBDD connexionBDD = new ConnexionBDD(connectionString);
                connexionBDD.Connecter();

                string requete = "SELECT p.nom_plat, p.description, p.prix " +
                               "FROM commande c " +
                               "INNER JOIN plat p ON c.id_plat = p.id_plat " +
                               "WHERE c.id_cuisinier = @idCuisinier " +
                               "AND CAST(c.date_commande AS DATE) = CAST(GETDATE() AS DATE) " +
                               "ORDER BY c.date_commande DESC " +
                               "LIMIT 1";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@idCuisinier", idCuisinier);

                MySqlDataReader reader = commande.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("\nPlat du jour :");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Nom: " + reader["nom_plat"]);
                    Console.WriteLine("Description: " + reader["description"]);
                    Console.WriteLine("Prix: " + reader["prix"] + "€");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("aucun plat commande aujourd'hui");
                }
                reader.Close();

                connexionBDD.Deconnecter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de l'affichage du plat du jour : " + ex.Message);
            }
        }
    }
} 