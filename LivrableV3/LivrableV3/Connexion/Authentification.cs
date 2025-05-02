using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace LivrableV3
{
    /// <summary>
    /// cette classe gere l'authentification des utilisateurs dans l'application
    /// elle permet de se connecter, s'inscrire et gerer les differents types d'utilisateurs
    /// c'est une classe importante car elle verifie l'identite des utilisateurs
    /// </summary>
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
        public string idUtilisateur;
        public string stationMetroCuisinier;
        public string stationMetroClient;

        public ValidationRequette ValidationRequette;
        public Graphe<int> GrapheMetro;

        
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
        /// cette methode sert a generer un id unique pour un utilisateur
        /// elle regarde le dernier id dans la base et ajoute 1
        /// si y a pas d'id elle commence a 1
        /// </summary>
        private string GenererIdUtilisateur()
        {
            try
            {
                
                string sql = "SELECT id_utilisateur FROM utilisateur WHERE id_utilisateur LIKE 'USR%' ORDER BY id_utilisateur DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    return "USR001";
                }
                
                string dernierId = result.ToString();
                
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                
                return "USR" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id utilisateur : " + ex.Message);
                return "USR" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode sert a generer un id unique pour un cuisinier
        /// elle fait pareil que pour l'utilisateur mais avec CUI au lieu de USR
        /// </summary>
        private string GenererIdCuisinier()
        {
            try
            {
               
                string sql = "SELECT id_cuisinier FROM cuisinier WHERE id_cuisinier LIKE 'CUI%' ORDER BY id_cuisinier DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    return "CUI001";
                }
                
                string dernierId = result.ToString();
                
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                
                
                return "CUI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id cuisinier : " + ex.Message);
                return "CUI" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode sert a generer un id unique pour un client
        /// elle fait pareil que pour l'utilisateur mais avec CLI au lieu de USR
        /// </summary>
        private string GenererIdClient()
        {
            try
            {
                
                string sql = "SELECT id_client FROM client WHERE id_client LIKE 'CLI%' ORDER BY id_client DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();
                
                if (result == null)
                {
                    return "CLI001";
                }
                
                string dernierId = result.ToString();
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                return "CLI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la generation de l'id client : " + ex.Message);
                return "CLI" + DateTime.Now.Ticks;
            }
        }
        
        /// <summary>
        /// cette methode permet a un utilisateur de se connecter
        /// elle verifie l'email et le mot de passe dans la BDD
        /// elle met a jour les informations de l'utilisateur si la connexion reussit
        /// </summary>
        public bool SeConnecter()
        {
            Console.WriteLine("=== Connexion ===");
            
            email = ValidationRequette.DemanderEmail("entrez votre email : ");
            
            motDePasse = ValidationRequette.DemanderMotDePasse("entrez votre mot de passe : ");

            try
            {
                string requete = "SELECT * FROM utilisateur WHERE email='" + email + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                if (reader.Read())
                {
                    string mdpBDD = reader.GetString("mot_de_passe");

                    if (mdpBDD == motDePasse)
                    {
                        estConnecte = true;
                        
                        idUtilisateur = reader.GetString("id_utilisateur");
                        nomUtilisateur = reader.GetString("nom");  
                        nom = reader.GetString("nom");
                        prenom = reader.GetString("prénom");
                        telephone = reader.GetString("telephone");
                        adresse = reader.GetString("adresse");

                        reader.Close();
                        commande.Dispose();

                        string requeteClient = "SELECT StationMetro FROM client WHERE id_utilisateur='" + idUtilisateur + "'";
                        MySqlCommand commandeClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                        commandeClient.CommandText = requeteClient;
                        MySqlDataReader readerClient = commandeClient.ExecuteReader();

                        if (readerClient.Read())
                        {
                            estClient = true;
                            estCuisinier = false;
                            stationMetro = readerClient.GetString("StationMetro");
                            readerClient.Close();
                            commandeClient.Dispose();
                        }
                        else
                        {
                            readerClient.Close();
                            commandeClient.Dispose();

                            string requeteCuisinier = "SELECT StationMetro FROM cuisinier WHERE id_utilisateur='" + idUtilisateur + "'";
                            MySqlCommand commandeCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                            commandeCuisinier.CommandText = requeteCuisinier;
                            MySqlDataReader readerCuisinier = commandeCuisinier.ExecuteReader();

                            if (readerCuisinier.Read())
                            {
                                estCuisinier = true;
                                estClient = false;
                                stationMetro = readerCuisinier.GetString("StationMetro");
                            }

                            readerCuisinier.Close();
                            commandeCuisinier.Dispose();
                        }

                        Console.WriteLine("connexion reussie");
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("mot de passe incorrect");
                        reader.Close();
                        commande.Dispose();
                    }
                }
                else
                {
                    Console.WriteLine("utilisateur non trouve");
                    reader.Close();
                    commande.Dispose();
                }

                return estConnecte;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de la connexion : " + e.Message);
                return false;
            }
        }

       

        /// <summary>
        /// cette methode permet a un utilisateur de s'inscrire
        /// elle cree un compte utilisateur et peut creer un compte cuisinier et/ou client dans la BDD
        /// elle gere aussi la creation des comptes d'acces a la base de donnees
        /// </summary>
        public bool SInscrire()
        {
            Console.WriteLine("=== Inscription ===");
            
            try
            {
                nomUtilisateur = ValidationRequette.DemanderNom("entrez votre nom : ");
                prenom = ValidationRequette.DemanderNom("entrez votre prenom : ");
                email = ValidationRequette.DemanderEmail("entrez votre email : ");

                string sqlVerifEmail = "SELECT COUNT(*) FROM utilisateur WHERE email = '" + email + "'";
                MySqlCommand cmdVerifEmail = new MySqlCommand(sqlVerifEmail, connexionBDD.maConnexion);
                int countEmail = Convert.ToInt32(cmdVerifEmail.ExecuteScalar());

                if (countEmail > 0)
                {
                    Console.WriteLine("cet email est deja utilise");
                    while (countEmail > 0)
                    {
                        Console.WriteLine("veuillez entrer un autre email");
                        email = ValidationRequette.DemanderEmail("entrez votre email : ");
                        sqlVerifEmail = "SELECT COUNT(*) FROM utilisateur WHERE email = '" + email + "'";
                        cmdVerifEmail = new MySqlCommand(sqlVerifEmail, connexionBDD.maConnexion);
                        countEmail = Convert.ToInt32(cmdVerifEmail.ExecuteScalar());
                    }
                }

                telephone = ValidationRequette.DemanderTelephone("entrez votre telephone : ");
                adresse = ValidationRequette.DemanderAdresse("entrez votre adresse : ");
                motDePasse = ValidationRequette.DemanderMotDePasse("entrez votre mot de passe : ");
                
                
                
                idUtilisateur = GenererIdUtilisateur();
                
                string sqlUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    idUtilisateur + "', '" + nomUtilisateur + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.ExecuteNonQuery();
                
                Console.WriteLine("Compte utilisateur créé avec succès !");
                
                Console.WriteLine("\nVoulez-vous créer un compte cuisinier et/ou client ?");
                Console.WriteLine("1 : Créer un compte cuisinier");
                Console.WriteLine("2 : Créer un compte client");
                Console.WriteLine("3 : Créer les deux");
                
                int choix = ValidationRequette.DemanderTypeUtilisateur("Entrez votre choix (1-3) : ");
                
                ValidationRequette validation = new ValidationRequette(GrapheMetro);
                
                switch (choix)
                {
                    case 1: // cuisinier
                        Console.WriteLine("\n=== Création du compte cuisinier ===");
                       this.stationMetroCuisinier = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                        Console.WriteLine("Entrez les zones de livraison (séparées par des virgules) : ");
                        string zonesLivraison = Console.ReadLine();
                        
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
                        
                        
                        try // creation du compte acces BDD cuisinier
                        {
                            string CreateBDDCuisinier = "CREATE USER IF NOT EXISTS '"+nomUtilisateur+"'@'localhost' IDENTIFIED BY '"+motDePasse+"';";
                            MySqlCommand cmdCreateBDDCuisinier = new MySqlCommand(CreateBDDCuisinier, connexionBDD.maConnexion);
                            cmdCreateBDDCuisinier.ExecuteNonQuery();


                            string GrantBDDCuisinier = "GRANT SELECT, INSERT, UPDATE ON PSI_LoMaEs.*  TO '"+nomUtilisateur+"'@'localhost';";
                            MySqlCommand cmdGrantBDDCuisinier = new MySqlCommand(GrantBDDCuisinier, connexionBDD.maConnexion);
                            cmdGrantBDDCuisinier.ExecuteNonQuery();

                            cmdCreateBDDCuisinier.Dispose();
                            cmdGrantBDDCuisinier.Dispose();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("erreur lors de la creation du compte acces BDD cuisinier : " + e.Message);
                        }

                        estClient = false;
                        estCuisinier = true;

                        Console.WriteLine("Compte cuisinier créé avec succès !");

                        break;

                    case 2: // compte client 
                        Console.WriteLine("\n=== Création du compte client ===");
                        this.stationMetroClient = validation.DemanderStationMetro("Entrez la station metro du client : ");
                        int typeClient = ValidationRequette.DemanderTypeUtilisateur("Entrez le type de client (1: Particulier, 2: Entreprise) : ");
                        
                        string entrepriseNom = null;
                        string referent = null;
                        
                        if (typeClient == 2)
                        {
                            entrepriseNom = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                            referent = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                            
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


                        try // creation du compte acces BDD client
                        {
                            string CreateBDDClient = "CREATE USER IF NOT EXISTS '"+nomUtilisateur+"'@'localhost' IDENTIFIED BY '"+motDePasse+"';";
                            MySqlCommand cmdCreateBDDClient = new MySqlCommand(CreateBDDClient, connexionBDD.maConnexion);
                            cmdCreateBDDClient.ExecuteNonQuery();


                            string GrantBDDClient = "GRANT SELECT, INSERT, UPDATE ON PSI_LoMaEs.*  TO '"+nomUtilisateur+"'@'localhost';";
                            MySqlCommand cmdGrantBDDClient = new MySqlCommand(GrantBDDClient, connexionBDD.maConnexion);
                            cmdGrantBDDClient.ExecuteNonQuery();

                            cmdCreateBDDClient.Dispose();
                            cmdGrantBDDClient.Dispose();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("erreur lors de la creation du compte acces BDD client : " + e.Message);
                        }
                        estClient = true;
                        estCuisinier = false;

                        Console.WriteLine("Compte client créé avec succès !");
                        break;
                        
                    case 3: // les deux comptes
                        Console.WriteLine("\n=== Création des comptes cuisinier et client ===");
                        
                        string stationMetroCuisinier2 = validation.DemanderStationMetro("Entrez la station metro du cuisinier : ");
                        Console.WriteLine("Entrez les zones de livraison (séparées par des virgules) : ");
                        string zonesLivraison2 = Console.ReadLine();
                        
                        if (string.IsNullOrWhiteSpace(zonesLivraison2))
                        {
                            Console.WriteLine("Les zones de livraison ne peuvent pas être vides");
                            while (string.IsNullOrWhiteSpace(zonesLivraison2))
                            {
                                Console.WriteLine("Les zones de livraison ne peuvent pas être vides");
                                zonesLivraison2 = Console.ReadLine();
                            }
                        }
                        
                        string idCuisinier2 = GenererIdCuisinier();
                        string requeteCuisinier2 = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" + 
                            idCuisinier2 + "', '" + idUtilisateur + "', '" + stationMetroCuisinier2 + "', '" + zonesLivraison2 + "', 0, 0)";
                        
                        MySqlCommand cmdCuisinier2 = new MySqlCommand(requeteCuisinier2, connexionBDD.maConnexion);
                        cmdCuisinier2.ExecuteNonQuery();
                        
                        string stationMetroClient2 = validation.DemanderStationMetro("Entrez la station metro du client : ");
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
                                while (string.IsNullOrWhiteSpace(entrepriseNom2) || string.IsNullOrWhiteSpace(referent2))
                                {
                                    Console.WriteLine("Les informations de l'entreprise ne peuvent pas être vides");
                                    entrepriseNom2 = ValidationRequette.DemanderNom("Entrez le nom de l'entreprise : ");
                                    referent2 = ValidationRequette.DemanderNom("Entrez le nom du référent : ");
                                }
                            }
                        }
                        
                        string idClient2 = GenererIdClient();
                        string requeteClient2 = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                            idClient2 + "', '" + idUtilisateur + "', '" + stationMetroClient2 + "', " + 
                            (entrepriseNom2 == null ? "NULL" : "'" + entrepriseNom2 + "'") + ", " + 
                            (referent2 == null ? "NULL" : "'" + referent2 + "'") + ")";
                        
                        MySqlCommand cmdClient2 = new MySqlCommand(requeteClient2, connexionBDD.maConnexion);
                        cmdClient2.ExecuteNonQuery();


                        try // creation du compte acces BDD cuisinier
                        {
                            string CreateBDDCuisinier = "CREATE USER IF NOT EXISTS '"+nomUtilisateur+"'@'localhost' IDENTIFIED BY '"+motDePasse+"';";
                            MySqlCommand cmdCreateBDDCuisinier = new MySqlCommand(CreateBDDCuisinier, connexionBDD.maConnexion);
                            cmdCreateBDDCuisinier.ExecuteNonQuery();


                            string GrantBDDCuisinier = "GRANT SELECT, INSERT, UPDATE ON PSI_LoMaEs.*  TO '"+nomUtilisateur+"'@'localhost';";
                            MySqlCommand cmdGrantBDDCuisinier = new MySqlCommand(GrantBDDCuisinier, connexionBDD.maConnexion);
                            cmdGrantBDDCuisinier.ExecuteNonQuery();

                            cmdCreateBDDCuisinier.Dispose();
                            cmdGrantBDDCuisinier.Dispose();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("erreur lors de la creation du compte acces BDD cuisinier : " + e.Message);
                        }

                        try // creation du compte acces BDD client
                        {
                            string CreateBDDClient = "CREATE USER IF NOT EXISTS '"+nomUtilisateur+"'@'localhost' IDENTIFIED BY '"+motDePasse+"';";
                            MySqlCommand cmdCreateBDDClient = new MySqlCommand(CreateBDDClient, connexionBDD.maConnexion);
                            cmdCreateBDDClient.ExecuteNonQuery();

                            string GrantBDDClient = "GRANT SELECT, INSERT, UPDATE ON PSI_LoMaEs.*  TO '"+nomUtilisateur+"'@'localhost';";
                            MySqlCommand cmdGrantBDDClient = new MySqlCommand(GrantBDDClient, connexionBDD.maConnexion);
                            cmdGrantBDDClient.ExecuteNonQuery();

                            cmdCreateBDDClient.Dispose();
                            cmdGrantBDDClient.Dispose();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("erreur lors de la creation du compte acces BDD client : " + e.Message);
                        }   

                        
                        estClient = true;
                        estCuisinier = true;
                        
                        Console.WriteLine("Comptes cuisinier et client créés avec succès !");
                        break;
                }
                
                
                estConnecte = true;
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de l'inscription : " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// cette methode permet de se deconnecter
        /// elle reinitialise toutes les variables de l'utilisateur
        /// </summary>
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

        /// <summary>
        /// cette methode permet de savoir quel type d'utilisateur est connecte
        /// elle retourne 1 pour client, 2 pour cuisinier, 3 pour les deux et 0 pour aucun
        /// </summary>
        public int Qui()
        {
            if (estClient)
            {
                return 1;
            }
            if (estCuisinier)
            {
                return 2;
            }
            if (estClient && estCuisinier)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
}