using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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

        /// constructeur par defaut
        public Authentification(ConnexionBDD connexion)
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
            connexionBDD = connexion;
        }

        /// methode pour se connecter
        public bool SeConnecter()
        {
            Console.WriteLine("=== Connexion ===");
            Console.WriteLine("Entrez votre nom d'utilisateur : ");
            nomUtilisateur = Console.ReadLine();

            Console.WriteLine("Entrez votre mot de passe : ");
            motDePasse = Console.ReadLine();

            try
            {
                // verifie si l'utilisateur existe et recupere ses informations
                string requete = "SELECT u.*, c.StationMetro as station_client, cu.StationMetro as station_cuisinier " +
                               "FROM utilisateur u " +
                               "LEFT JOIN client c ON u.id_utilisateur = c.id_utilisateur " +
                               "LEFT JOIN cuisinier cu ON u.id_utilisateur = cu.id_utilisateur " +
                               "WHERE u.id_utilisateur = '" + nomUtilisateur + "'";
                
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

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
                        email = (string)reader["email"];
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
            Console.WriteLine("Entrez votre nom d'utilisateur : ");
            nomUtilisateur = Console.ReadLine();

            Console.WriteLine("Entrez votre mot de passe : ");
            motDePasse = Console.ReadLine();

            Console.WriteLine("Entrez votre nom : ");
            nom = Console.ReadLine();

            Console.WriteLine("Entrez votre prenom : ");
            prenom = Console.ReadLine();

            Console.WriteLine("Entrez votre email : ");
            email = Console.ReadLine();

            Console.WriteLine("Entrez votre telephone : ");
            telephone = Console.ReadLine();

            Console.WriteLine("Entrez votre adresse : ");
            adresse = Console.ReadLine();

            Console.WriteLine("Entrez votre station de metro la plus proche : ");
            stationMetro = Console.ReadLine();

            Console.WriteLine("Voulez-vous être cuisinier: 1 ou client : 2 (tapez le chiffre)");
            int reponse = Convert.ToInt32(Console.ReadLine());

            try
            {
                // verifie si l'utilisateur existe deja
                string requeteVerif = "SELECT COUNT(*) FROM utilisateur WHERE id_utilisateur = '" + nomUtilisateur + "'";
                MySqlCommand commandeVerif = new MySqlCommand(requeteVerif, connexionBDD.maConnexion);
                commandeVerif.CommandText = requeteVerif;

                int count = Convert.ToInt32(commandeVerif.ExecuteScalar());
                commandeVerif.Dispose();

                if (count > 0)
                {
                    Console.WriteLine("cet utilisateur existe deja");
                    return false;
                }

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

                // insere le nouvel utilisateur
                string requeteInsert = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, téléphone, adresse, type__Cuisinier_Client_, mot_de_passe) " +
                                     "VALUES ('" + nomUtilisateur + "', '" + nom + "', '" + prenom + "', '" + email + "', '" + telephone + "', '" + adresse + "', '" + typeUtilisateur + "', '" + motDePasse + "')";
                
                MySqlCommand commandeInsert = new MySqlCommand(requeteInsert, connexionBDD.maConnexion);
                commandeInsert.CommandText = requeteInsert;
                commandeInsert.ExecuteNonQuery();
                commandeInsert.Dispose();

                // insere dans la table client ou cuisinier selon le type
                if (estClient)
                {
                    string requeteClient = "INSERT INTO client (id_client, id_utilisateur, type_client__Particulier_Entreprise_, StationMetro) " +
                                         "VALUES ('" + nomUtilisateur + "_client', '" + nomUtilisateur + "', 'Particulier', '" + stationMetro + "')";
                    MySqlCommand commandeClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                    commandeClient.ExecuteNonQuery();
                    commandeClient.Dispose();
                }
                else
                {
                    string requeteCuisinier = "INSERT INTO cuisinier (id_cuisinier, type__Cuisinier_Client_, zones_livraison, note_moyenne, nombre_livraisons, id_utilisateur, StationMetro) " +
                                            "VALUES ('" + nomUtilisateur + "_cuisinier', 'Cuisinier', '', 0.0, 0, '" + nomUtilisateur + "', '" + stationMetro + "')";
                    MySqlCommand commandeCuisinier = new MySqlCommand(requeteCuisinier, connexionBDD.maConnexion);
                    commandeCuisinier.ExecuteNonQuery();
                    commandeCuisinier.Dispose();
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
    }
}
