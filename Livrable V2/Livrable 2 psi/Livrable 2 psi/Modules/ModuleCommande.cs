using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        /// modifie une commande de facon simple
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

        /// calcule le prix dune commande de facon simple
        public double CalculerPrixCommande(string idCommande)
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

                Console.WriteLine("prix de la commande " + idCommande + ": " + prix + " euros");
                return prix;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du calcul du prix de la commande : " + ex.Message);
                return 0;
            }
        }

        /// determine le chemin de livraison et affiche la route
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

                // récupérer les stations du client et du cuisinier (à la fois nom et ID)
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
                
                // trouver les ID des stations correspondantes dans le graphe metro
                string stationDepartId = "";
                string stationArriveeId = "";
                
                // recherche des identifiants de stations dans le graphe
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
                    
                    // si on a trouvé les deux stations, on peut arrêter la recherche
                    if (stationDepartId != "" && stationArriveeId != "")
                    {
                        break;
                    }
                }
                
                // vérifier qu'on a bien trouvé les stations
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
                
                // recherche et affichage de l'itinéraire avec les IDs
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
                        
                        // ouverture de l'image
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
    }
} 