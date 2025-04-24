using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    public class TestColorationClientsMetro
    {
        /// teste la coloration de graphe avec les stations de metro des clients et cuisiniers
        public static void TesterColorationClientsMetro(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
        {
            try
            {
                // on cree un graphe de test
                Graphe<string> grapheTest = new Graphe<string>();

                // on recupere les clients et leurs stations
                string requeteClients = "SELECT c.id_client, u.nom, u.prénom, c.StationMetro " +
                                      "FROM client c " +
                                      "JOIN utilisateur u ON c.id_utilisateur = u.id_utilisateur";
                MySqlCommand cmdClients = new MySqlCommand(requeteClients, connexionBDD.maConnexion);
                MySqlDataReader readerClients = cmdClients.ExecuteReader();

                // on ajoute les clients au graphe
                while (readerClients.Read())
                {
                    string idClient = readerClients["id_client"].ToString();
                    string nom = readerClients["nom"].ToString();
                    string prenom = readerClients["prénom"].ToString();
                    string station = readerClients["StationMetro"].ToString();

                    // on cherche la station dans le graphe du metro
                    Noeud<int> stationMetro = null;
                    foreach (var noeud in grapheMetro.Noeuds.Values)
                    {
                        if (noeud.NomStation == station)
                        {
                            stationMetro = noeud;
                            break;
                        }
                    }

                    if (stationMetro != null)
                    {
                        // on cree un noeud pour le client avec les coordonnees de la station
                        Noeud<string> noeudClient = new Noeud<string>(idClient);
                        noeudClient.NomStation = station;
                        noeudClient.Longitude = stationMetro.Longitude;
                        noeudClient.Latitude = stationMetro.Latitude;
                        noeudClient.Nom = prenom + " " + nom; // on stocke le nom complet
                        grapheTest.Noeuds[idClient] = noeudClient;
                    }
                }
                readerClients.Close();

                // on recupere les cuisiniers et leurs stations
                string requeteCuisiniers = "SELECT c.id_cuisinier, u.nom, u.prénom, c.StationMetro " +
                                         "FROM cuisinier c " +
                                         "JOIN utilisateur u ON c.id_utilisateur = u.id_utilisateur";
                MySqlCommand cmdCuisiniers = new MySqlCommand(requeteCuisiniers, connexionBDD.maConnexion);
                MySqlDataReader readerCuisiniers = cmdCuisiniers.ExecuteReader();

                // on ajoute les cuisiniers au graphe
                while (readerCuisiniers.Read())
                {
                    string idCuisinier = readerCuisiniers["id_cuisinier"].ToString();
                    string nom = readerCuisiniers["nom"].ToString();
                    string prenom = readerCuisiniers["prénom"].ToString();
                    string station = readerCuisiniers["StationMetro"].ToString();

                    // on cherche la station dans le graphe du metro
                    Noeud<int> stationMetro = null;
                    foreach (var noeud in grapheMetro.Noeuds.Values)
                    {
                        if (noeud.NomStation == station)
                        {
                            stationMetro = noeud;
                            break;
                        }
                    }

                    if (stationMetro != null)
                    {
                        // on cree un noeud pour le cuisinier avec les coordonnees de la station
                        Noeud<string> noeudCuisinier = new Noeud<string>(idCuisinier);
                        noeudCuisinier.NomStation = station;
                        noeudCuisinier.Longitude = stationMetro.Longitude;
                        noeudCuisinier.Latitude = stationMetro.Latitude;
                        noeudCuisinier.Nom = prenom + " " + nom; // on stocke le nom complet
                        grapheTest.Noeuds[idCuisinier] = noeudCuisinier;
                    }
                }
                readerCuisiniers.Close();

                // on recupere les commandes pour creer les liens
                string requeteCommandes = "SELECT id_client, id_cuisinier FROM Commande_";
                MySqlCommand cmdCommandes = new MySqlCommand(requeteCommandes, connexionBDD.maConnexion);
                MySqlDataReader readerCommandes = cmdCommandes.ExecuteReader();

                // on ajoute les liens entre clients et cuisiniers
                while (readerCommandes.Read())
                {
                    string idClient = readerCommandes["id_client"].ToString();
                    string idCuisinier = readerCommandes["id_cuisinier"].ToString();

                    if (grapheTest.Noeuds.ContainsKey(idClient) && grapheTest.Noeuds.ContainsKey(idCuisinier))
                    {
                        grapheTest.AjouterLien(idClient, idCuisinier, 1);
                    }
                }
                readerCommandes.Close();

                // on cree l'objet de coloration
                ColorationGraphe<string> coloration = new ColorationGraphe<string>();

                // on applique l'algorithme de welsh powell
                coloration.AppliquerWelshPowell(grapheTest);

                // on affiche les resultats
                coloration.AfficherResultats(grapheMetro);

                Console.WriteLine();
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la coloration du graphe : " + ex.Message);
            }
        }
    }
} 