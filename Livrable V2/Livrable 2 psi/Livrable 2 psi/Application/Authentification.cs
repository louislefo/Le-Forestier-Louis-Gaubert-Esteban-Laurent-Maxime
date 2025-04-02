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

        /// constructeur par defaut
        public Authentification(ConnexionBDD connexionBDD)
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
            
            // validation du nom d'utilisateur
            nomUtilisateur = ValidationRequette.DemanderNomUtilisateur("entrez votre nom d'utilisateur : ");

            try
            {
                // verifie si l'utilisateur existe deja
                string requeteVerif = "SELECT COUNT(*) FROM utilisateur WHERE id_utilisateur = @nomUtilisateur";
                MySqlCommand commandeVerif = new MySqlCommand(requeteVerif, connexionBDD.maConnexion);
                commandeVerif.Parameters.AddWithValue("@nomUtilisateur", nomUtilisateur);

                int count = Convert.ToInt32(commandeVerif.ExecuteScalar());
                commandeVerif.Dispose();

                if (count > 0)
                {
                    Console.WriteLine("cet utilisateur existe deja");
                    return false;
                }

                // validation du mot de passe
                motDePasse = ValidationRequette.DemanderMotDePasse("entrez votre mot de passe : ");

                // validation du nom et prenom
                nom = ValidationRequette.DemanderNom("entrez votre nom : ");
                prenom = ValidationRequette.DemanderNom("entrez votre prenom : ");

                // validation de l'email
                email = ValidationRequette.DemanderEmail("entrez votre email : ");

                // validation du telephone
                telephone = ValidationRequette.DemanderTelephone("entrez votre telephone : ");

                // validation de l'adresse
                adresse = ValidationRequette.DemanderAdresse("entrez votre adresse : ");

                // validation de la station de metro
                stationMetro = ValidationRequette.DemanderStationMetro("entrez votre station de metro la plus proche : ");

                // validation du type d'utilisateur
                int reponse = ValidationRequette.DemanderTypeUtilisateur("voulez-vous etre cuisinier: 1 ou client : 2 (tapez le chiffre)");

                // determine le type d'utilisateur
                string typeUtilisateur;
                if (reponse == 1)
                {
                    estCuisinier = true;
                    estClient = false;
                    typeUtilisateur = "Cuisinier";
                }
                else
                {
                    estCuisinier = false;
                    estClient = true;
                    typeUtilisateur = "Client";
                }

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

                // insere dans la table utilisateur
                string sqlUtilisateur = "INSERT INTO utilisateur (nom_utilisateur, email, telephone, mot_de_passe, type_utilisateur) " +
                                      "VALUES (@nomUtilisateur, @email, @telephone, @motDePasse, @typeUtilisateur); " +
                                      "SELECT LAST_INSERT_ID();";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, connexionBDD.maConnexion);
                cmdUtilisateur.Parameters.AddWithValue("@nomUtilisateur", nomUtilisateur);
                cmdUtilisateur.Parameters.AddWithValue("@email", email);
                cmdUtilisateur.Parameters.AddWithValue("@telephone", telephone);
                cmdUtilisateur.Parameters.AddWithValue("@motDePasse", motDePasse);
                cmdUtilisateur.Parameters.AddWithValue("@typeUtilisateur", typeUtilisateur);

                int idUtilisateur = Convert.ToInt32(cmdUtilisateur.ExecuteScalar());

                // insere dans la table correspondante selon le type
                if (estClient)
                {
                    string sqlClient = "INSERT INTO client (id_utilisateur) VALUES (@idUtilisateur)";
                    MySqlCommand cmdClient = new MySqlCommand(sqlClient, connexionBDD.maConnexion);
                    cmdClient.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                    cmdClient.ExecuteNonQuery();
                }
                else
                {
                    string sqlCuisinier = "INSERT INTO cuisinier (id_utilisateur) VALUES (@idUtilisateur)";
                    MySqlCommand cmdCuisinier = new MySqlCommand(sqlCuisinier, connexionBDD.maConnexion);
                    cmdCuisinier.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                    cmdCuisinier.ExecuteNonQuery();
                }

                Console.WriteLine("inscription reussie");
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