using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace LivrableV3
{
    public class ModuleCommande
    {
        public ConnexionBDD connexionBDD;
        public Graphe<int> grapheMetro;

        public ModuleCommande(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
        {
            this.connexionBDD = connexionBDD;
            this.grapheMetro = grapheMetro;
        }

        /*

        /// <summary>
        /// cette methode sert a creer une nouvelle commande
        /// elle verifie que le client et le plat existent
        /// elle calcule le prix et cree la commande dans la base
        /// </summary>
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
                    throw new Exception("le client n'existe pas");
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
                    throw new Exception("le plat n'existe pas");
                }
                lecteurPrix.Close();
                commandePrix.Dispose();

                // on cree un id de commande
                string idCommande = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // on insere la commande avec les bons champs
                string requeteCommande = "INSERT INTO Commande_ (id_commande, id_client, id_cuisinier, id_plat, date_commande, prix_total, statut) " +
                                        "VALUES ('" + idCommande + "', '" + idClient + "', '" + idCuisinier + "', '" + idPlat + "', '" + dateCommande.ToString("yyyy-MM-dd HH:mm:ss") + "', " + prixPlat.ToString().Replace(',', '.') + ", 'En attente')";

                MySqlCommand commandeInsert = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commandeInsert.CommandText = requeteCommande;
                int resultat = commandeInsert.ExecuteNonQuery();
                commandeInsert.Dispose();

                Console.WriteLine("commande créée avec succès");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la création de la commande : " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// cette methode sert a modifier une commande existante
        /// elle verifie que la commande existe et que le nouveau plat existe
        /// elle met a jour le prix et les infos de la commande
        /// </summary>
        public void ModifierCommande(string idCommande, string idPlat, DateTime dateCommande)
        {
            try
            {
                // on vérifie si la commande existe
                string requeteCommande = "SELECT id_commande FROM Commande_ WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commandeVerif = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commandeVerif.CommandText = requeteCommande;

                MySqlDataReader lecteurCommande = commandeVerif.ExecuteReader();
                if (lecteurCommande.Read() == false)
                {
                    lecteurCommande.Close();
                    commandeVerif.Dispose();
                    throw new Exception("la commande n'existe pas");
                }
                lecteurCommande.Close();
                commandeVerif.Dispose();

                // on récupère le prix du nouveau plat
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
                    throw new Exception("le plat n'existe pas");
                }
                lecteurPrix.Close();
                commandePrix.Dispose();

                // on modifie la commande
                string requeteModif = "UPDATE Commande_ SET id_plat = '" + idPlat + "', date_commande = '" + dateCommande.ToString("yyyy-MM-dd HH:mm:ss") + "', prix_total = " + prixPlat.ToString().Replace(',', '.') + " WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commandeModif = new MySqlCommand(requeteModif, connexionBDD.maConnexion);
                commandeModif.CommandText = requeteModif;
                commandeModif.ExecuteNonQuery();
                commandeModif.Dispose();

                Console.WriteLine("commande modifiée avec succès");
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la modification de la commande : " + ex.Message);
            }
        }

        /// <summary>
        /// cette methode sert a calculer le prix d'une commande
        /// elle recupere le prix dans la base et le retourne
        /// </summary>
        
        public string CalculerPrixCommande(string idCommande)
        {

            try
            {
                string requete = "SELECT prix_total FROM Commande_ WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader lecteur = commande.ExecuteReader();
                double prix = 0;
                if (lecteur.Read())
                {
                    prix = Convert.ToDouble(lecteur["prix_total"]);
                }
                else
                {
                    lecteur.Close();
                    commande.Dispose();
                    throw new Exception("la commande n'existe pas");
                }
                lecteur.Close();
                commande.Dispose();
                
                string rep = "prix de la commande " + idCommande + ": " + prix + " euros";
                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur lors du calcul du prix de la commande : " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// cette methode sert a determiner le chemin de livraison
        /// elle trouve les stations du cuisinier et du client
        /// elle cherche le meilleur chemin dans le metro
        /// elle cree une image avec le chemin
        /// </summary>
        public (string stationDepart, string stationArrivee) DeterminerCheminLivraison(string idCommande)
        {
            try
            {
                // on vérifie si la commande existe
                string requeteVerif = "SELECT id_commande FROM Commande_ WHERE id_commande = '" + idCommande + "'";
                MySqlCommand commandeVerif = new MySqlCommand(requeteVerif, connexionBDD.maConnexion);
                commandeVerif.CommandText = requeteVerif;

                MySqlDataReader lecteurVerif = commandeVerif.ExecuteReader();
                if (lecteurVerif.Read() == false)
                {
                    lecteurVerif.Close();
                    commandeVerif.Dispose();
                    throw new Exception("la commande n'existe pas");
                }
                lecteurVerif.Close();
                commandeVerif.Dispose();

                // on récupère les stations du client et du cuisinier
                string requete = "SELECT cu.StationMetro as station_cuisinier, c.StationMetro as station_client " +
                               "FROM Commande_ co " +
                               "JOIN client c ON co.id_client = c.id_client " +
                               "JOIN Plat_ p ON co.id_plat = p.id_plat " +
                               "JOIN cuisinier cu ON co.id_cuisinier = cu.id_cuisinier " +
                               "WHERE co.id_commande = '" + idCommande + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader lecteur = commande.ExecuteReader();
                string stationDepartNom = "";
                string stationArriveeNom = "";

                if (lecteur.Read())
                {
                    stationDepartNom = lecteur["station_cuisinier"].ToString();
                    stationArriveeNom = lecteur["station_client"].ToString();
                    Console.WriteLine("chemin de livraison: de " + stationDepartNom + " à " + stationArriveeNom);
                }
                else
                {
                    throw new Exception("problème avec les stations");
                }

                lecteur.Close();
                commande.Dispose();

                // on cherche les ID des stations dans le graphe
                string stationDepartId = "";
                string stationArriveeId = "";

                // on parcourt tous les noeuds pour trouver les stations
                foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
                {
                    if (noeud.NomStation.Equals(stationDepartNom, StringComparison.OrdinalIgnoreCase))
                    {
                        stationDepartId = noeud.Id.ToString();
                    }
                    if (noeud.NomStation.Equals(stationArriveeNom, StringComparison.OrdinalIgnoreCase))
                    {
                        stationArriveeId = noeud.Id.ToString();
                    }

                    // si on a trouvé les deux stations, on peut s'arrêter
                    if (stationDepartId != "" && stationArriveeId != "")
                    {
                        break;
                    }
                }

                // on vérifie qu'on a bien trouvé les stations
                if (stationDepartId == "")
                {
                    Console.WriteLine("ATTENTION: Station de départ '" + stationDepartNom + "' non trouvée dans le graphe");
                    return (stationDepartNom, stationArriveeNom);
                }

                if (stationArriveeId == "")
                {
                    Console.WriteLine("ATTENTION: Station d'arrivée '" + stationArriveeNom + "' non trouvée dans le graphe");
                    return (stationDepartNom, stationArriveeNom);
                }

                Console.WriteLine("ID stations: départ=" + stationDepartId + ", arrivée=" + stationArriveeId);

                // on cherche et affiche l'itinéraire
                if (stationDepartId != "" && stationArriveeId != "")
                {
                    Console.WriteLine("\nRecherche de l'itinéraire de livraison...");

                    GestionnaireItineraire<int> gestionItineraire = new GestionnaireItineraire<int>(grapheMetro);
                    List<Noeud<int>> itineraire = gestionItineraire.RechercherItineraire(stationDepartId, stationArriveeId);

                    if (itineraire != null && itineraire.Count > 0)
                    {
                        Console.WriteLine("\nCréation de la carte d'itinéraire...");
                        VisualisationItineraire visItineraire = new VisualisationItineraire(1200, 800);
                        string texteItineraire = "Itinéraire de livraison: " + itineraire[0].NomStation + " à " + itineraire[itineraire.Count - 1].NomStation;
                        visItineraire.DessinerItineraire(grapheMetro, itineraire, texteItineraire);

                        string nomFichier = "livraison_" + idCommande + ".png";
                        visItineraire.SauvegarderImage(nomFichier);
                        Console.WriteLine("Carte de l'itinéraire sauvegardée sous le nom " + nomFichier);

                        // on ouvre l'image
                        Console.WriteLine("Ouverture de l'image...");
                        try
                        {
                            Process.Start(new ProcessStartInfo(nomFichier) { UseShellExecute = true });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Erreur lors de l'ouverture du fichier : " + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucun itinéraire trouvé entre les stations.");
                    }
                }

                return (stationDepartNom, stationArriveeNom);
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la détermination du chemin de livraison : " + ex.Message);
                return (null, null);
            }
        }

        public List<string> ListeCommandes()
        {
            List<string> listeCommandes = new List<string>();
            try
            {
                string requete = "SELECT id_commande FROM Commande_";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;
                MySqlDataReader lecteur = commande.ExecuteReader();
                while (lecteur.Read())
                {
                    listeCommandes.Add(lecteur["id_commande"].ToString());
                }
                lecteur.Close();
                commande.Dispose();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur lors de la récupération des commandes : " + ex.Message);
            }
            return listeCommandes;
        }




        */


    }
}
