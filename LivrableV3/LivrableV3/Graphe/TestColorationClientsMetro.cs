using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LivrableV3
{
    public class TestColorationClientsMetro
    {
        /// teste la coloration de graphe avec les stations de metro des clients et cuisiniers
        public static void TesterColorationClientsMetro(ConnexionBDD connexionBDD, Graphe<int> grapheMetro)
        {
            try
            {
                Graphe<string> grapheTest = new Graphe<string>();
                Dictionary<string, Noeud<int>> correspondanceStations = new Dictionary<string, Noeud<int>>();

                string requeteClients = "SELECT c.id_client, u.nom, u.prénom, c.StationMetro " +
                                      "FROM client c " +
                                      "JOIN utilisateur u ON c.id_utilisateur = u.id_utilisateur";
                MySqlCommand cmdClients = new MySqlCommand(requeteClients, connexionBDD.maConnexion);
                MySqlDataReader readerClients = cmdClients.ExecuteReader();

                while (readerClients.Read())
                {
                    string idClient = readerClients["id_client"].ToString();
                    string nom = readerClients["nom"].ToString();
                    string prenom = readerClients["prénom"].ToString();
                    string station = readerClients["StationMetro"].ToString();

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
                        Noeud<string> noeudClient = new Noeud<string>(idClient);
                        noeudClient.NomStation = station;
                        noeudClient.Longitude = stationMetro.Longitude;
                        noeudClient.Latitude = stationMetro.Latitude;
                        noeudClient.Nom = prenom + " " + nom;
                        grapheTest.Noeuds[idClient] = noeudClient;
                        correspondanceStations[idClient] = stationMetro;
                    }
                }
                readerClients.Close();

                string requeteCuisiniers = "SELECT c.id_cuisinier, u.nom, u.prénom, c.StationMetro " +
                                         "FROM cuisinier c " +
                                         "JOIN utilisateur u ON c.id_utilisateur = u.id_utilisateur";
                MySqlCommand cmdCuisiniers = new MySqlCommand(requeteCuisiniers, connexionBDD.maConnexion);
                MySqlDataReader readerCuisiniers = cmdCuisiniers.ExecuteReader();

                while (readerCuisiniers.Read())
                {
                    string idCuisinier = readerCuisiniers["id_cuisinier"].ToString();
                    string nom = readerCuisiniers["nom"].ToString();
                    string prenom = readerCuisiniers["prénom"].ToString();
                    string station = readerCuisiniers["StationMetro"].ToString();

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
                        Noeud<string> noeudCuisinier = new Noeud<string>(idCuisinier);
                        noeudCuisinier.NomStation = station;
                        noeudCuisinier.Longitude = stationMetro.Longitude;
                        noeudCuisinier.Latitude = stationMetro.Latitude;
                        noeudCuisinier.Nom = prenom + " " + nom;
                        grapheTest.Noeuds[idCuisinier] = noeudCuisinier;
                        correspondanceStations[idCuisinier] = stationMetro;
                    }
                }
                readerCuisiniers.Close();

                string requeteCommandes = "SELECT id_client, id_cuisinier FROM Commande_";
                MySqlCommand cmdCommandes = new MySqlCommand(requeteCommandes, connexionBDD.maConnexion);
                MySqlDataReader readerCommandes = cmdCommandes.ExecuteReader();

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

                ColorationGraphe<string> coloration = new ColorationGraphe<string>();
                coloration.AppliquerWelshPowell(grapheTest);
                coloration.AfficherResultats(grapheMetro, correspondanceStations);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la coloration du graphe : " + ex.Message);
            }
        }
    }
} 