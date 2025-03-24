using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

namespace Livrable_2_psi
{
    public class Visualisation<T>
    {
        private Graphe<T> graphe;
        private int largeur;
        private int hauteur;
        private int rayonNoeud = 5; // taille du cercle pour un noeud
        private Dictionary<T, Point> positionsNoeuds;

        /// <summary>
        /// cree une nouvelle visualisation du graphe
        /// </summary>
        /// <param name="graphe">Le graphe à visualiser</param>
        /// <param name="largeur">Largeur de l'image (par défaut 800)</param>
        /// <param name="hauteur">Hauteur de l'image (par défaut 600)</param>
        public Visualisation(Graphe<T> graphe, int largeur = 800, int hauteur = 600)
        {
            this.graphe = graphe;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.positionsNoeuds = new Dictionary<T, Point>();
        }

        /// <summary>
        /// dessine le graphe et le met dans une image
        /// </summary>
        /// <returns>Bitmap contenant le dessin du graphe</returns>
        public Bitmap DessinerGraphe()
        {
            Bitmap bitmap = new Bitmap(largeur, hauteur);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                // trouve les limites des coordonnees
                double minLong = double.MaxValue, maxLong = double.MinValue;
                double minLat = double.MaxValue, maxLat = double.MinValue;

                foreach (var noeud in graphe.Noeuds.Values)
                {
                    if (noeud is Noeud<T> noeudMetro)
                    {
                        minLong = Math.Min(minLong, noeudMetro.Longitude);
                        maxLong = Math.Max(maxLong, noeudMetro.Longitude);
                        minLat = Math.Min(minLat, noeudMetro.Latitude);
                        maxLat = Math.Max(maxLat, noeudMetro.Latitude);
                    }
                }

                // calcule les facteurs d'echelle
                double echelleX = (largeur - 100) / (maxLong - minLong);
                double echelleY = (hauteur - 100) / (maxLat - minLat);

                // dessine les liens
                foreach (var lien in graphe.Liens)
                {
                    if (lien.Noeud1 is Noeud<T> n1 && lien.Noeud2 is Noeud<T> n2)
                    {
                        int x1 = (int)((n1.Longitude - minLong) * echelleX) + 50;
                        int y1 = (int)((maxLat - n1.Latitude) * echelleY) + 50;
                        int x2 = (int)((n2.Longitude - minLong) * echelleX) + 50;
                        int y2 = (int)((maxLat - n2.Latitude) * echelleY) + 50;

                        using (Pen pen = new Pen(Color.Gray, 1))
                        {
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                    }
                }

                // dessine les noeuds
                foreach (var noeud in graphe.Noeuds.Values)
                {
                    if (noeud is Noeud<T> noeudMetro)
                    {
                        int x = (int)((noeudMetro.Longitude - minLong) * echelleX) + 50;
                        int y = (int)((maxLat - noeudMetro.Latitude) * echelleY) + 50;

                        positionsNoeuds[noeud.Id] = new Point(x, y);

                        // dessine le cercle du noeud
                        using (Brush brush = new SolidBrush(Color.Red))
                        {
                            g.FillEllipse(brush, x - rayonNoeud, y - rayonNoeud, rayonNoeud * 2, rayonNoeud * 2);
                        }

                        // met le nom de la station
                        using (Font font = new Font("Arial", 8))
                        {
                            g.DrawString(noeudMetro.NomStation, font, Brushes.Black, x + 5, y - 5);
                        }
                    }
                }
            }

            return bitmap;
        }

        /// <summary>
        /// sauvegarde limage du graphe dans un fichier
        /// </summary>
        public void SauvegarderGraphique(string nomFichier)
        {
            using (Bitmap bitmap = DessinerGraphe())
            {
                bitmap.Save(nomFichier, ImageFormat.Png);
            }
        }
    }
}
