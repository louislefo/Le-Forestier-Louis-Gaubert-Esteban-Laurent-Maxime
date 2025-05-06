using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace LivrableV3
{
    /// <summary>
    /// cette classe permet de colorer le metro avec l'algorithme de welsh powell
    /// elle attribue des couleurs aux stations pour que deux stations voisines n'ai pas la meme couleur
    /// elle utilise les coordonnees des stations pour les afficher sur une carte
    /// </summary>
    public class ColorationMetro
    {
        private Graphe<int> grapheMetro;
        private Dictionary<int, int> couleursStations;
        private List<Color> listeCouleurs;

        /// <summary>
        /// cree une nouvelle coloration pour le metro
        /// initialise le graphe et prepare les couleurs pour colorer les stations
        /// les couleurs sont stockees dans une liste pour etre utilisees plus tard
        /// </summary>
        public ColorationMetro(Graphe<int> graphe)
        {
            grapheMetro = graphe;
            couleursStations = new Dictionary<int, int>();
            listeCouleurs = new List<Color>
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Purple,
                Color.Orange,
                Color.Pink,
                Color.Cyan,
                Color.Magenta,
                Color.Brown
            };
        }

        /// <summary>
        /// applique l'algorithme de welsh powell sur le metro
        /// trie les stations par nombre de voisins
        /// attribue les couleurs en verifiant que les stations voisines ont des couleurs differentes
        /// utilise le moins de couleurs possible pour colorer tout le metro
        /// </summary>
        public void AppliquerWelshPowell()
        {
            List<Noeud<int>> stationsTriees = new List<Noeud<int>>(grapheMetro.Noeuds.Values);
            stationsTriees.Sort((a, b) => b.Voisins.Count.CompareTo(a.Voisins.Count));

            foreach (Noeud<int> station in stationsTriees)
            {
                couleursStations[station.Id] = -1;
            }

            int couleurActuelle = 0;
            while (true)
            {
                Noeud<int> stationNonColoree = null;
                for (int i = 0; i < stationsTriees.Count; i++)
                {
                    if (couleursStations[stationsTriees[i].Id] == -1)
                    {
                        stationNonColoree = stationsTriees[i];
                        break;
                    }
                }

                if (stationNonColoree == null)
                {
                    break;
                }

                couleursStations[stationNonColoree.Id] = couleurActuelle;

                for (int i = 0; i < stationsTriees.Count; i++)
                {
                    Noeud<int> station = stationsTriees[i];
                    if (couleursStations[station.Id] == -1)
                    {
                        bool peutEtreColoree = true;
                        foreach (Noeud<int> voisin in station.Voisins)
                        {
                            if (couleursStations[voisin.Id] == couleurActuelle)
                            {
                                peutEtreColoree = false;
                                break;
                            }
                        }

                        if (peutEtreColoree)
                        {
                            couleursStations[station.Id] = couleurActuelle;
                        }
                    }
                }

                couleurActuelle++;
            }
        }

        /// <summary>
        /// affiche le metro colore dans une fenetre
        /// dessine les stations avec leurs couleurs attribuees
        /// affiche les noms des stations et les liens entre elles
        /// utilise les coordonnees gps pour positionner les stations sur la carte
        /// </summary>
        public void AfficherGrapheColore()
        {
            Form fenetreGraphe = new Form();
            fenetreGraphe.Text = "Graphe du Metro Colore";
            fenetreGraphe.Size = new Size(1200, 800);

            Panel panelGraphe = new Panel();
            panelGraphe.Dock = DockStyle.Fill;

            double minLon = double.MaxValue;
            double maxLon = double.MinValue;
            double minLat = double.MaxValue;
            double maxLat = double.MinValue;

            foreach (Noeud<int> station in grapheMetro.Noeuds.Values)
            {
                minLon = Math.Min(minLon, station.Longitude);
                maxLon = Math.Max(maxLon, station.Longitude);
                minLat = Math.Min(minLat, station.Latitude);
                maxLat = Math.Max(maxLat, station.Latitude);
            }

            double margeEspace = 50;
            double facteurZoomX = (1200 - 2 * margeEspace) / (maxLon - minLon);
            double facteurZoomY = (800 - 2 * margeEspace) / (maxLat - minLat);

            panelGraphe.Paint += (sender, e) =>
            {
                foreach (Lien<int> lien in grapheMetro.Liens)
                {
                    Noeud<int> station1 = lien.Noeud1;
                    Noeud<int> station2 = lien.Noeud2;

                    int x1 = (int)(margeEspace + (station1.Longitude - minLon) * facteurZoomX);
                    int y1 = (int)(800 - margeEspace - (station1.Latitude - minLat) * facteurZoomY);
                    int x2 = (int)(margeEspace + (station2.Longitude - minLon) * facteurZoomX);
                    int y2 = (int)(800 - margeEspace - (station2.Latitude - minLat) * facteurZoomY);

                    using (Pen pen = new Pen(Color.Gray, 2))
                    {
                        e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                foreach (Noeud<int> station in grapheMetro.Noeuds.Values)
                {
                    int x = (int)(margeEspace + (station.Longitude - minLon) * facteurZoomX);
                    int y = (int)(800 - margeEspace - (station.Latitude - minLat) * facteurZoomY);

                    int couleurIndex = couleursStations[station.Id];
                    Color couleur = listeCouleurs[couleurIndex % listeCouleurs.Count];
                    using (Brush brush = new SolidBrush(couleur))
                    {
                        e.Graphics.FillEllipse(brush, x - 5, y - 5, 10, 10);
                    }

                    using (Font font = new Font("Arial", 8))
                    {
                        e.Graphics.DrawString(station.NomStation, font, Brushes.Black, x + 5, y - 5);
                    }
                }
            };

            fenetreGraphe.Controls.Add(panelGraphe);
            fenetreGraphe.ShowDialog();
        }
    }
}