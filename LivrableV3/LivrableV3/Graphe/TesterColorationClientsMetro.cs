using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

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
                Dictionary<string, Noeud<int>> correspondanceStations = new Dictionary<string, Noeud<int>>();

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
                        correspondanceStations[idClient] = stationMetro;
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
                        correspondanceStations[idCuisinier] = stationMetro;
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
                coloration.AfficherResultats(grapheMetro, correspondanceStations);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la coloration du graphe : " + ex.Message);
            }
        }

        /// affiche les resultats de la coloration
        private static void AfficherResultatsColoration(Graphe<int> grapheMetro, Graphe<string> grapheTest, ColorationGraphe<string> coloration)
        {
            // on cree d'abord l'image du metro
            VisualisationCarte visMetro = new VisualisationCarte(800, 600);
            visMetro.DessinerGraphe(grapheMetro);

            // on cree une nouvelle fenetre pour afficher le graphe
            Form fenetreGraphe = new Form();
            fenetreGraphe.Text = "Graphe des Clients et Cuisiniers Colore";
            fenetreGraphe.Size = new Size(800, 600);

            // on cree un panel pour dessiner le graphe
            Panel panelGraphe = new Panel();
            panelGraphe.Dock = DockStyle.Fill;
            panelGraphe.Paint += (sender, e) =>
            {
                // on dessine d'abord le metro
                if (File.Exists("metro.png"))
                {
                    using (var image = Image.FromFile("metro.png"))
                    {
                        e.Graphics.DrawImage(image, 0, 0, panelGraphe.Width, panelGraphe.Height);
                    }
                }

                // on dessine les clients et cuisiniers
                DessinerClientsCuisiniers(e.Graphics, grapheMetro, grapheTest, coloration);
            };
            fenetreGraphe.Controls.Add(panelGraphe);

            // on affiche la fenetre
            fenetreGraphe.ShowDialog();
        }

        /// dessine les clients et cuisiniers sur la carte
        private static void DessinerClientsCuisiniers(Graphics g, Graphe<int> grapheMetro, Graphe<string> grapheTest, ColorationGraphe<string> coloration)
        {
            // on recupere les groupes de noeuds
            var groupes = coloration.TrouverGroupesIndependants();

            // on dessine les clients et cuisiniers
            for (int i = 0; i < groupes.Count; i++)
            {
                Color couleur = i == 0 ? Color.Red : Color.Blue;
                foreach (var noeud in groupes[i])
                {
                    // on cherche la station correspondante dans le graphe du metro
                    Noeud<int> stationMetro = null;
                    foreach (var station in grapheMetro.Noeuds.Values)
                    {
                        if (station.NomStation == noeud.NomStation)
                        {
                            stationMetro = station;
                            break;
                        }
                    }

                    if (stationMetro != null)
                    {
                        // on calcule la position de la station
                        double minLong = double.MaxValue, maxLong = double.MinValue;
                        double minLat = double.MaxValue, maxLat = double.MinValue;

                        foreach (var n in grapheMetro.Noeuds.Values)
                        {
                            minLong = Math.Min(minLong, n.Longitude);
                            maxLong = Math.Max(maxLong, n.Longitude);
                            minLat = Math.Min(minLat, n.Latitude);
                            maxLat = Math.Max(maxLat, n.Latitude);
                        }

                        double marge = 0.01;
                        minLong -= marge;
                        maxLong += marge;
                        minLat -= marge;
                        maxLat += marge;

                        double echelleLong = (800 - 2 * 50) / (maxLong - minLong);
                        double echelleLat = (600 - 2 * 50) / (maxLat - minLat);

                        int x = (int)((stationMetro.Longitude - minLong) * echelleLong) + 50;
                        int y = 600 - ((int)((stationMetro.Latitude - minLat) * echelleLat) + 50);

                        // on dessine un point plus gros pour le client/cuisinier
                        g.FillEllipse(new SolidBrush(couleur), x - 6, y - 6, 12, 12);

                        // on affiche le nom de la station et de la personne
                        string texteStation = noeud.NomStation;
                        string textePersonne = noeud.Nom;

                        if (i == 0) // premier groupe = cuisiniers
                        {
                            g.DrawString(texteStation, new Font("Arial", 6), Brushes.Black, x + 8, y - 8);
                            g.DrawString("Cuisinier: " + textePersonne, new Font("Arial", 6, FontStyle.Bold), Brushes.Black, x + 8, y + 2);
                        }
                        else // deuxième groupe = clients
                        {
                            g.DrawString(texteStation, new Font("Arial", 6), Brushes.Black, x + 8, y - 8);
                            g.DrawString("Client: " + textePersonne, new Font("Arial", 6, FontStyle.Bold), Brushes.Black, x + 8, y + 2);
                        }
                    }
                }
            }

            // on ajoute une legende
            int legendeX = 20;
            int legendeY = 20;

            // Informations sur le graphe
            string infoGraphe = "Nombre de couleurs : " + coloration.NombreCouleurs + "\n" +
                               "Graphe biparti : " + (coloration.EstBiparti() ? "Oui" : "Non") + "\n" +
                               "Graphe planaire : " + (coloration.EstPlanaire() ? "Oui" : "Non");
            g.DrawString(infoGraphe, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, legendeX, legendeY);
            legendeY += 80;

            // Légende des couleurs
            g.FillRectangle(new SolidBrush(Color.Red), legendeX, legendeY, 10, 10);
            g.DrawString("Cuisiniers", new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
            legendeY += 20;

            g.FillRectangle(new SolidBrush(Color.Blue), legendeX, legendeY, 10, 10);
            g.DrawString("Clients", new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
        }
    }
}