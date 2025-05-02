using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
    /// <summary>
    /// cette classe affiche l'itineraire sur le graphe 
    /// </summary>
    public class VisualisationItineraire
    {
        private Bitmap image;
        private Graphics graphics;
        private int largeur;
        private int hauteur;
        private int marge = 50;
        private Dictionary<int, Point> positionsNoeuds;
        private Dictionary<int, Point> positionsTextes;
        private Dictionary<string, bool> nomsDejaAffiches;
        private Dictionary<string, int> nombreLignesParStation;


        public VisualisationItineraire(int largeur, int hauteur)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.image = new Bitmap(largeur, hauteur);
            this.graphics = Graphics.FromImage(image);
            this.graphics.Clear(Color.White);
            this.positionsNoeuds = new Dictionary<int, Point>();
            this.positionsTextes = new Dictionary<int, Point>();
            this.nomsDejaAffiches = new Dictionary<string, bool>();
            this.nombreLignesParStation = new Dictionary<string, int>();
        }

        /// <summary>
        /// calcule la position du texte pour eviter les superpositions
        /// </summary>
        private Point CalculerPositionTexte(int x, int y, string nomStation)
        {
            int decalageX = 10;
            int decalageY = 0;
            bool positionTrouvee = false;
            int maxTentatives = 8;

            for (int i = 0; i < maxTentatives && !positionTrouvee; i++)
            {
                switch (i)
                {
                    case 0: // droite
                        decalageX = 10;
                        decalageY = 0;
                        break;
                    case 1: // gauche
                        decalageX = -10 - (int)(nomStation.Length * 4);
                        decalageY = 0;
                        break;
                    case 2: // haut
                        decalageX = 0;
                        decalageY = -15;
                        break;
                    case 3: // bas
                        decalageX = 0;
                        decalageY = 15;
                        break;
                    case 4: // haut droite
                        decalageX = 10;
                        decalageY = -15;
                        break;
                    case 5: // haut gauche
                        decalageX = -10 - (int)(nomStation.Length * 4);
                        decalageY = -15;
                        break;
                    case 6: // bas droite
                        decalageX = 10;
                        decalageY = 15;
                        break;
                    case 7: // bas gauche
                        decalageX = -10 - (int)(nomStation.Length * 4);
                        decalageY = 15;
                        break;
                }

                bool positionLibre = true;
                foreach (Point posTexte in positionsTextes.Values)
                {
                    if (Math.Abs((x + decalageX) - posTexte.X) < 50 && Math.Abs((y + decalageY) - posTexte.Y) < 20)
                    {
                        positionLibre = false;
                        break;
                    }
                }

                if (positionLibre)
                {
                    positionTrouvee = true;
                }
            }

            return new Point(x + decalageX, y + decalageY);
        }

        /// <summary>
        /// dessine l'itineraire sur la carte
        /// affiche le chemin en couleur et les stations importantes
        /// </summary>
        public void DessinerItineraire(Graphe<int> graphe, List<Noeud<int>> itineraire, string texteItineraire)
        {
            // Trouver les limites des coordonnées
            double minLong = double.MaxValue, maxLong = double.MinValue;
            double minLat = double.MaxValue, maxLat = double.MinValue;

            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                minLong = Math.Min(minLong, noeud.Longitude);
                maxLong = Math.Max(maxLong, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            // Calculer les facteurs d'échelle
            double echelleLong = (largeur - 2 * marge) / (maxLong - minLong);
            double echelleLat = (hauteur - 2 * marge) / (maxLat - minLat);

            // Dessiner tous les liens en gris clair
            foreach (Lien<int> lien in graphe.Liens)
            {
                Noeud<int> noeud1 = graphe.Noeuds[lien.Noeud1.Id];
                Noeud<int> noeud2 = graphe.Noeuds[lien.Noeud2.Id];

                int x1 = (int)((noeud1.Longitude - minLong) * echelleLong) + marge;
                int y1 = hauteur - ((int)((noeud1.Latitude - minLat) * echelleLat) + marge);
                int x2 = (int)((noeud2.Longitude - minLong) * echelleLong) + marge;
                int y2 = hauteur - ((int)((noeud2.Latitude - minLat) * echelleLat) + marge);

                using (Pen pen = new Pen(Color.LightGray, 1))
                {
                    graphics.DrawLine(pen, x1, y1, x2, y2);
                }
            }

            // Dessiner l'itineraire avec les couleurs des lignes
            for (int i = 0; i < itineraire.Count - 1; i++)
            {
                Noeud<int> noeud1 = itineraire[i];
                Noeud<int> noeud2 = itineraire[i + 1];

                int x1 = (int)((noeud1.Longitude - minLong) * echelleLong) + marge;
                int y1 = hauteur - ((int)((noeud1.Latitude - minLat) * echelleLat) + marge);
                int x2 = (int)((noeud2.Longitude - minLong) * echelleLong) + marge;
                int y2 = hauteur - ((int)((noeud2.Latitude - minLat) * echelleLat) + marge);

                try
                {
                    Color couleurLigne = ColorTranslator.FromHtml(noeud1.CouleurLigne);
                    using (Pen pen = new Pen(couleurLigne, 3))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur de couleur: " + e.Message + ", utilisation de la couleur par defaut");
                    using (Pen pen = new Pen(Color.Blue, 3))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }

            // Dessiner les noeuds
            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                int x = (int)((noeud.Longitude - minLong) * echelleLong) + marge;
                int y = hauteur - ((int)((noeud.Latitude - minLat) * echelleLat) + marge);

                positionsNoeuds[noeud.Id] = new Point(x, y);

                // Dessiner le noeud en blanc avec un contour noir
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillEllipse(brush, x - 4, y - 4, 8, 8);
                }

                using (Pen pen = new Pen(Color.Black, 1))
                {
                    graphics.DrawEllipse(pen, x - 4, y - 4, 8, 8);
                }

                // Calculer la position du texte pour les stations de l'itineraire
                if (itineraire.Contains(noeud) && !nomsDejaAffiches.ContainsKey(noeud.NomStation))
                {
                    Point positionTexte = CalculerPositionTexte(x, y, noeud.NomStation);
                    positionsTextes[noeud.Id] = positionTexte;
                    nomsDejaAffiches[noeud.NomStation] = true;
                }
            }

            // Dessiner les noms des stations de l'itineraire
            foreach (Noeud<int> noeud in itineraire)
            {
                if (positionsTextes.ContainsKey(noeud.Id))
                {
                    Point positionTexte = positionsTextes[noeud.Id];
                    Color couleurTexte;
                    try
                    {
                        couleurTexte = ColorTranslator.FromHtml(noeud.CouleurLigne);
                    }
                    catch
                    {
                        couleurTexte = Color.Blue;
                    }

                    using (Font font = new Font("Arial", 8, FontStyle.Bold))
                    using (SolidBrush brush = new SolidBrush(couleurTexte))
                    {
                        graphics.DrawString(noeud.NomStation, font, brush, positionTexte.X, positionTexte.Y);
                    }
                }
            }

            
        }

        /// <summary>
        /// cette methode sauvegarde l'image dans un fichier
        /// elle supprime le fichier s'il existe deja
        /// </summary>
        public void SauvegarderImage(string chemin)
        {
            if (File.Exists(chemin))
            {
                File.Delete(chemin);
            }

            image.Save(chemin, ImageFormat.Png); 
        }
    }
}
