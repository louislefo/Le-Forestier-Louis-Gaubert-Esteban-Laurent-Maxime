using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Livrable_2_psi
{
    public class Authentification
    {
        public string nomUtilisateur;
        public string motDePasse;
        public string nom;
        public string prenom;
        public string email;
        public string telephone;
        public string adresse;
        public string stationMetro;
        public bool estClient;
        public bool estCuisinier;
        public bool estConnecte;
        public ConnexionBDD connexionBDD;

        public AffichageCuisinier affichageCuisinier;
        public AffichageClient affichageClient;
        public ValidationRequette ValidationRequette;
        public Graphe<int> GrapheMetro;

        /// constructeur par defaut
        public Authentification(ConnexionBDD connexionBDD, Graphe<int> GrapheMetro)
        {
            nomUtilisateur = "";
            motDePasse = "";
            nom = "";
            prenom = "";
            email = "";
            telephone = "";
            adresse = "";
            stationMetro = "";
            estClient = false;
            estCuisinier = false;
            estConnecte = false;
            this.connexionBDD = connexionBDD;
            this.GrapheMetro = GrapheMetro;
            ValidationRequette newrequette = new ValidationRequette(GrapheMetro);

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
        
        /// <summary>
        /// genere un id unique pour un client
        /// </summary>
        private string GenererIdClient()
        {
            try
            {
                // recupere le dernier id client
                string sql = "SELECT id_client FROM client WHERE id_client LIKE 'CLI%' ORDER BY id_client DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    // si aucun client n'existe, commence par CLI001
                    return "CLI001";
                }
                
                string dernierId = result.ToString();
                // extrait le numero
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                // formate le nouvel id
                return "CLI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id client : " + ex.Message);
                // en cas d'erreur, genere un id avec timestamp
                return "CLI" + DateTime.Now.Ticks;
            }
        }
        
        /// methode pour se connecter
        public bool SeConnecter()
        {
            Console.WriteLine("=== Connexion ===");
            
            // validation de l'email
            email = ValidationRequette.DemanderEmail("entrez votre email : ");
            
            // validation du mot de passe
            motDePasse = ValidationRequette.DemanderMotDePasse("entrez votre mot de passe : ");

            try
            {
                // verifie si l'utilisateur existe et recupere ses informations
                string requete = "SELECT u.*, c.StationMetro as station_client, cu.StationMetro as station_cuisinier " +
                               "FROM utilisateur u " +
                               "LEFT JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                               "LEFT JOIN cuisinier cu ON u.id_utilisateur = cu.id_utilisateur " +
                               "WHERE u.email = @email";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.Parameters.AddWithValue("@email", email);

                MySqlDataReader reader = commande.ExecuteReader();

                if (reader.Read())
                {
                    string mdpBDD = (string)reader["mot_de_passe"];
                    string typeUtilisateur = (string)reader["type__Cuisinier_Client_"];

                    if (mdpBDD == motDePasse)
                    {
                        estConnecte = true;
                        nom = (string)reader["nom"];
                        prenom = (string)reader["prénom"];
                        telephone = (string)reader["téléphone"];
                        adresse = (string)reader["adresse"];

                        if (typeUtilisateur == "Cuisinier")
                        {
                            estCuisinier = true;
                            estClient = false;
                            stationMetro = (string)reader["station_cuisinier"];
                        }
                        else
                        {
                            estCuisinier = false;
                            estClient = true;
                            stationMetro = (string)reader["station_client"];
                        }
                        Console.WriteLine("connexion reussie");
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("mot de passe incorrect");
                    }
                }
                else
                {
                    Console.WriteLine("utilisateur non trouve");
                }

                reader.Close();
                commande.Dispose();

                return estConnecte;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de la connexion : " + e.Message);
                return false;
            }
        }

       

        /// methode pour s'inscrire
        public bool SInscrire()
        {
            Console.WriteLine("=== Inscription ===");
            
            try
            {
                // validation des données utilisateur
                nomUtilisateur = ValidationRequette.DemanderNom("entrez votre nom : ");
                prenom = ValidationRequette.DemanderNom("entrez votre prenom : ");
                email = ValidationRequette.DemanderEmail("entrez votre email : ");

                // verifie si l'email existe deja
                string sqlVerifEmail = "SELECT COUNT(*) FROM utilisateur WHERE email = @email";
                MySqlCommand cmdVerifEmail = new MySqlCommand(sqlVerifEmail, connexionBDD.maConnexion);
                cmdVerifEmail.Parameters.AddWithValue("@email", email);
                int countEmail = Convert.ToInt32(cmdVerifEmail.ExecuteScalar());

                if (countEmail > 0)
                {
                    Console.WriteLine("cet email est deja utilise");
                    return false;
                }


                telephone = ValidationRequette.DemanderTelephone("entrez votre telephone : ");
                adresse = ValidationRequette.DemanderAdresse("entrez votre adresse : ");
                motDePasse = ValidationRequette.DemanderMotDePasse("entrez votre mot de passe : ");
                
                
                
                // generation d'un id unique pour l'utilisateur
                string idUtilisateur = GenererIdUtilisateur();
                
                // insertion dans la table utilisateur
                string sqlUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                Console.WriteLine("Compte utilisateur créé avec succès !");
                
                // demande du type de compte supplémentaire
                Console.WriteLine("\nVoulez-vous créer un compte cuisinier et/ou client ?");
                Console.WriteLine("1 : Créer un compte cuisinier");
                Console.WriteLine("2 : Créer un compte client");
                Console.WriteLine("3 : Créer les deux");
                
                int choix = ValidationRequette.DemanderTypeUtilisateur("Entrez votre choix (1-3) : ");
                
                // creation d'une instance de ValidationRequette avec le graphe
                ValidationRequette validation = new ValidationRequette(GrapheMetro);
                
                switch (choix)
                {
                    case 1: // compte cuisinier uniquement
                        Console.WriteLine("\n=== Création du compte cuisinier ===");
                        string stationMetroCuisinier = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                        Console.WriteLine("Entrez les zones de livraison (séparées par des virgules) : ");
                        string zonesLivraison = Console.ReadLine();
                        
                        // verification des zones de livraison
                        if (string.IsNullOrWhiteSpace(zonesLivraison))
                        {
                            Console.WriteLine("Les zones de livraison ne peuvent pas être vides");
                            return false;
                        }
                        
                        string idCuisinier = GenererIdCuisinier();
                        string requeteCuisinier = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" + 
                            idCuisinier + "', '" + idUtilisateur + "', '" + stationMetroCuisinier + "', '" + zonesLivraison + "', 0, 0)";
                        
                        MySqlCommand cmdCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                        cmdCuisinier.ExecuteNonQuery();
                        Console.WriteLine("Compte cuisinier créé avec succès !");
                        break;

                        string CreateBDDCuisinier = "CREATE USER IF NOT EXISTS '"+nom+"'@'localhost' IDENTIFIED BY '"+motDePasse+"';";
                        MySqlCommand cmdCreateBDDCuisinier = new MySqlCommand(CreateBDDCuisinier, connexionBDD.maConnexion);
                        cmdCreateBDDCuisinier.ExecuteNonQuery();
                        string GrantBDDCuisinier = "GRANT ALL PRIVILEGES ON PSI_LoMaEs.* TO '"+nom+"'@'localhost';";
                        MySqlCommand cmdGrantBDDCuisinier = new MySqlCommand(GrantBDDCuisinier, connexionBDD.maConnexion);
                        cmdGrantBDDCuisinier.ExecuteNonQuery();
                        
                    case 2: // compte client uniquement
                        Console.WriteLine("\n=== Création du compte client ===");
                        string stationMetroClient = validation.DemanderStationMetro("Entrez la station metro du client : ");
                        Console.WriteLine("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                        int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                        
                        string entrepriseNom = null;
                        string referent = null;
                        
                        if (typeClient == 2)
                        {
                            entrepriseNom = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                            referent = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                            
                            // verification des informations entreprise
                            if (string.IsNullOrWhiteSpace(entrepriseNom) || string.IsNullOrWhiteSpace(referent))
                            {
                                Console.WriteLine("Les informations de l'entreprise ne peuvent pas être vides");
                                return false;
                            }
                        }
                        
                        string idClient = GenererIdClient();
                        string requeteClient = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                            idClient + "', '" + idUtilisateur + "', '" + stationMetroClient + "', " + 
                            (entrepriseNom == null ? "NULL" : "'" + entrepriseNom + "'") + ", " + 
                            (referent == null ? "NULL" : "'" + referent + "'") + ")";
                        
                        MySqlCommand cmdClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                        cmdClient.ExecuteNonQuery();
                        Console.WriteLine("Compte client créé avec succès !");
                        break;
                        
                    case 3: // les deux comptes
                        Console.WriteLine("\n=== Création des comptes cuisinier et client ===");
                        
                        // compte cuisinier
                        string stationMetroCuisinier2 = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                        Console.WriteLine("Entrez les zones de livraison (séparées par des virgules) : ");
                        string zonesLivraison2 = Console.ReadLine();
                        
                        // verification des zones de livraison
                        if (string.IsNullOrWhiteSpace(zonesLivraison2))
                        {
                            Console.WriteLine("Les zones de livraison ne peuvent pas être vides");
                            return false;
                        }
                        
                        string idCuisinier2 = GenererIdCuisinier();
                        string requeteCuisinier2 = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" + 
                            idCuisinier2 + "', '" + idUtilisateur + "', '" + stationMetroCuisinier2 + "', '" + zonesLivraison2 + "', 0, 0)";
                        
                        MySqlCommand cmdCuisinier2 = new MySqlCommand(requeteCuisinier2, connexionBDD.maConnexion);
                        cmdCuisinier2.ExecuteNonQuery();
                        
                        // compte client
                        string stationMetroClient2 = validation.DemanderStationMetro("Entrez la station metro du client : ");
                        Console.WriteLine("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                        int typeClient2 = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                        
                        string entrepriseNom2 = null;
                        string referent2 = null;
                        
                        if (typeClient2 == 2)
                        {
                            entrepriseNom2 = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                            referent2 = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                            
                            // verification des informations entreprise
                            if (string.IsNullOrWhiteSpace(entrepriseNom2) || string.IsNullOrWhiteSpace(referent2))
                            {
                                Console.WriteLine("Les informations de l'entreprise ne peuvent pas être vides");
                                return false;
                            }
                        }
                        
                        string idClient2 = GenererIdClient();
                        string requeteClient2 = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                            idClient2 + "', '" + idUtilisateur + "', '" + stationMetroClient2 + "', " + 
                            (entrepriseNom2 == null ? "NULL" : "'" + entrepriseNom2 + "'") + ", " + 
                            (referent2 == null ? "NULL" : "'" + referent2 + "'") + ")";
                        
                        MySqlCommand cmdClient2 = new MySqlCommand(requeteClient2, connexionBDD.maConnexion);
                        cmdClient2.ExecuteNonQuery();
                        
                        Console.WriteLine("Comptes cuisinier et client créés avec succès !");
                        break;
                }
                
                Console.Clear();
                estConnecte = true;
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de l'inscription : " + e.Message);
                return false;
            }
        }

        /// methode pour se deconnecter
        public void SeDeconnecter()
        {
            estConnecte = false;
            nomUtilisateur = "";
            motDePasse = "";
            nom = "";
            prenom = "";
            email = "";
            telephone = "";
            adresse = "";
            stationMetro = "";
            estClient = false;
            estCuisinier = false;
        }

        public int Qui()
        {
            if (estClient)
            {
                return 1;
            }
            else if (estCuisinier)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }
}