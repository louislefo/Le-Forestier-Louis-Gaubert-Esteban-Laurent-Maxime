using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    /// classe qui gere les commandes pour un debutant
    public class ModuleCommande
    {
        public ConnexionBDD connexionBDD;
        public Graphe<int> grapheMetro;

        public ModuleCommande(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
        {
            this.connexionBDD = connexionBDD;
            this.grapheMetro = grapheMetro;
        }

        /// cree une nouvelle commande de facon simple
        public int CreerCommande(string idClient, string idCuisinier, string idPlat, DateTime dateCommande)
        {
            try
            {
                // on verifie si le client existe
                string requeteClient = "SELECT id_client FROM client WHERE id_client = '" + idClient + "'";
                MySqlCommand commandeClient = new MySqlCommand(requeteClient, connexionBDD.maConnexion);
                commandeClient.CommandText = requeteClient;

                MySqlDataReader lecteurClient = commandeClient.ExecuteReader();
                if (lecteurClient.Read() == false)
                {
                    lecteurClient.Close();
                    commandeClient.Dispose();
                    throw new Exception("le client nexiste pas");
                }
                lecteurClient.Close();
                commandeClient.Dispose();

                // on recupere le prix du plat
                string requetePrix = "SELECT prix_par_personne FROM Plat_ WHERE id_plat = '" + idPlat + "'";
                MySqlCommand commandePrix = new MySqlCommand(requetePrix, connexionBDD.maConnexion);
                commandePrix.CommandText = requetePrix;

                MySqlDataReader lecteurPrix = commandePrix.ExecuteReader();
                double prixPlat = 0;
                if (lecteurPrix.Read())
                {
                    prixPlat = Convert.ToDouble(lecteurPrix["prix_par_personne"]);
                }
                else
                {
                    lecteurPrix.Close();
                    commandePrix.Dispose();
                    throw new Exception("le plat nexiste pas");
                }
                lecteurPrix.Close();
                commandePrix.Dispose();

                // on cree un id de commande
                string idCommande = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // on insere la commande avec les bon champs
                string requeteCommande = "INSERT INTO Commande_ (id_commande, id_client, date_commande, total_prix, statut_En_attente__Confirmée__En_cours__Livrée__Annulée_) " +
                                       "VALUES ('" + idCommande + "', '" + idClient + "', '" + dateCommande.ToString("yyyy-MM-dd HH:mm:ss") + "', " + prixPlat + ", 'En attente')";

                MySqlCommand commandeInsert = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commandeInsert.CommandText = requeteCommande;
                commandeInsert.ExecuteNonQuery();
                commandeInsert.Dispose();

                Console.WriteLine("commande creee avec succes");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la creation de la commande : " + ex.Message);
                return -1;
            }
        }

        /// modifie une commande de facon simple
        public void ModifierCommande(string idCommande, string idPlat, DateTime dateCommande)
        {
            try
            {
                // on verifie si la commande existe
                string requeteCommande = "SELECT id_commande FROM Commande_ WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commandeVerif = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commandeVerif.CommandText = requeteCommande;

                MySqlDataReader lecteurCommande = commandeVerif.ExecuteReader();
                if (lecteurCommande.Read() == false)
                {
                    lecteurCommande.Close();
                    commandeVerif.Dispose();
                    throw new Exception("la commande nexiste pas");
                }
                lecteurCommande.Close();
                commandeVerif.Dispose();

                // on recupere le prix du nouveau plat
                string requetePrix = "SELECT prix_par_personne FROM Plat_ WHERE id_plat = '" + idPlat + "'";
                MySqlCommand commandePrix = new MySqlCommand(requetePrix, connexionBDD.maConnexion);
                commandePrix.CommandText = requetePrix;

                MySqlDataReader lecteurPrix = commandePrix.ExecuteReader();
                double prixPlat = 0;
                if (lecteurPrix.Read())
                {
                    prixPlat = Convert.ToDouble(lecteurPrix["prix_par_personne"]);
                }
                else
                {
                    lecteurPrix.Close();
                    commandePrix.Dispose();
                    throw new Exception("le plat nexiste pas");
                }
                lecteurPrix.Close();
                commandePrix.Dispose();

                // on modifie la commande
                string requeteModif = "UPDATE Commande_ SET date_commande = '" + dateCommande.ToString("yyyy-MM-dd HH:mm:ss") + "', total_prix = " + prixPlat + " WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commandeModif = new MySqlCommand(requeteModif, connexionBDD.maConnexion);
                commandeModif.CommandText = requeteModif;
                commandeModif.ExecuteNonQuery();
                commandeModif.Dispose();

                Console.WriteLine("commande modifiee avec succes");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la modification de la commande : " + ex.Message);
            }
        }

        /// calcule le prix dune commande de facon simple
        public double CalculerPrixCommande(string idCommande)
        {
            try
            {
                string requete = "SELECT total_prix FROM Commande_ WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader lecteur = commande.ExecuteReader();
                double prix = 0;
                if (lecteur.Read())
                {
                    prix = Convert.ToDouble(lecteur["total_prix"]);
                }
                else
                {
                    lecteur.Close();
                    commande.Dispose();
                    throw new Exception("la commande nexiste pas");
                }
                lecteur.Close();
                commande.Dispose();

                Console.WriteLine("prix de la commande " + idCommande + ": " + prix + " euros");
                return prix;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul du prix de la commande : " + ex.Message);
                return 0;
            }
        }

        /// determine le chemin de livraison de facon simple
        public (string stationDepart, string stationArrivee) DeterminerCheminLivraison(string idCommande)
        {
            try
            {
                // on verifie si la commande existe
                string requeteVerif = "SELECT id_commande FROM Commande_ WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commandeVerif = new MySqlCommand(requeteVerif, connexionBDD.maConnexion);
                commandeVerif.CommandText = requeteVerif;

                MySqlDataReader lecteurVerif = commandeVerif.ExecuteReader();
                if (lecteurVerif.Read() == false)
                {
                    lecteurVerif.Close();
                    commandeVerif.Dispose();
                    throw new Exception("la commande nexiste pas");
                }
                lecteurVerif.Close();
                commandeVerif.Dispose();

                string requete = "SELECT cu.StationMetro as station_cuisinier, c.StationMetro as station_client " +
                               "FROM Commande_ co " +
                               "JOIN client c ON co.id_client = c.id_client " +
                               "JOIN Plat_ p ON co.id_plat = p.id_plat " +
                               "JOIN cuisinier cu ON p.id_cuisinier = cu.id_cuisinier " +
                               "WHERE co.id_commande = '" + idCommande + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader lecteur = commande.ExecuteReader();
                string stationDepart = "";
                string stationArrivee = "";

                if (lecteur.Read())
                {
                    stationDepart = lecteur["station_cuisinier"].ToString();
                    stationArrivee = lecteur["station_client"].ToString();
                    Console.WriteLine("chemin de livraison: de " + stationDepart + " a " + stationArrivee);
                }
                else
                {
                    throw new Exception("probleme avec les stations");
                }

                lecteur.Close();
                commande.Dispose();
                return (stationDepart, stationArrivee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la determination du chemin de livraison : " + ex.Message);
                return (null, null);
            }
        }
    }
} 