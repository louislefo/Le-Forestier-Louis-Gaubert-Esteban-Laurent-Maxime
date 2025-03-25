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
    /// <summary>
    /// classe qui gere la visualisation du graphe
    /// </summary>
    public class Visualisation
    {
        private Bitmap image;
        private Graphics graphics;
        private int largeur;
        private int hauteur;
        private int marge = 50;
        private Dictionary<int, Point> positionsNoeuds;

        /// <summary>
        /// constructeur de la classe
        /// </summary>
        public Visualisation(int largeur, int hauteur)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.image = new Bitmap(largeur, hauteur);
            this.graphics = Graphics.FromImage(image);
            this.graphics.Clear(Color.White);
            this.positionsNoeuds = new Dictionary<int, Point>();
        }

        /// <summary>
        /// dessine le graphe du metro
        /// </summary>
        public void DessinerGraphe(Graphe<int> graphe)
        {
            // Trouver les limites des coordonnées
            double minLong = double.MaxValue, maxLong = double.MinValue;
            double minLat = double.MaxValue, maxLat = double.MinValue;

            foreach (var noeud in graphe.Noeuds.Values)
            {
                minLong = Math.Min(minLong, noeud.Longitude);
                maxLong = Math.Max(maxLong, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            // Calculer les facteurs d'échelle
            double echelleLong = (largeur - 2 * marge) / (maxLong - minLong);
            double echelleLat = (hauteur - 2 * marge) / (maxLat - minLat);

            // Dessiner les liens
            foreach (var lien in graphe.Liens)
            {
                var noeud1 = graphe.Noeuds[lien.Noeud1.Id];
                var noeud2 = graphe.Noeuds[lien.Noeud2.Id];

                // Convertir les coordonnées en pixels
                int x1 = (int)((noeud1.Longitude - minLong) * echelleLong) + marge;
                int y1 = hauteur - ((int)((noeud1.Latitude - minLat) * echelleLat) + marge);
                int x2 = (int)((noeud2.Longitude - minLong) * echelleLong) + marge;
                int y2 = hauteur - ((int)((noeud2.Latitude - minLat) * echelleLat) + marge);

                // Debug: afficher les coordonnées
                Console.WriteLine($"Lien: ({x1}, {y1}) -> ({x2}, {y2})");

                try
                {
                    // Utiliser la couleur de la ligne
                    Color couleurLigne = ColorTranslator.FromHtml(noeud1.CouleurLigne);
                    using (Pen pen = new Pen(couleurLigne, 2))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erreur de couleur: {e.Message}, utilisation de la couleur par défaut");
                    using (Pen pen = new Pen(Color.Gray, 2))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }

            // Dessiner les noeuds
            foreach (var noeud in graphe.Noeuds.Values)
            {
                int x = (int)((noeud.Longitude - minLong) * echelleLong) + marge;
                int y = hauteur - ((int)((noeud.Latitude - minLat) * echelleLat) + marge);

                positionsNoeuds[noeud.Id] = new Point(x, y);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillEllipse(brush, x - 4, y - 4, 8, 8);
                }

                using (Pen pen = new Pen(Color.Black))
                {
                    graphics.DrawEllipse(pen, x - 4, y - 4, 8, 8);
                }

                // Afficher le nom de la station
                using (Font font = new Font("Arial", 7))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    graphics.DrawString(noeud.NomStation, font, brush, x + 4, y - 4);
                }
            }
        }

        /// <summary>
        /// sauvegarde l'image
        /// </summary>
        public void SauvegarderImage(string chemin)
        {
            image.Save(chemin);
        }
    }
}
