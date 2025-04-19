using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
    public class VisualisationCarte
    {
        private Bitmap image;
        private Graphics graphics;
        private int largeur;
        private int hauteur;
        private int marge = 50;
        private Dictionary<int, Point> positionsNoeuds;
        private Dictionary<string, int> nombreLignesParStation;

        /// <summary>
        /// constructeur de la classe
        /// </summary>
        public VisualisationCarte(int largeur, int hauteur)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.image = new Bitmap(largeur, hauteur);
            this.graphics = Graphics.FromImage(image);
            this.graphics.Clear(Color.White);
            this.positionsNoeuds = new Dictionary<int, Point>();
            this.nombreLignesParStation = new Dictionary<string, int>();
        }

        /// <summary>
        /// dessine le graphe du metro
        /// </summary>
        public void DessinerGraphe(Graphe<int> graphe)
        {
            // calcul des limites de la carte
            double minLong = double.MaxValue, maxLong = double.MinValue;
            double minLat = double.MaxValue, maxLat = double.MinValue;

            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                minLong = Math.Min(minLong, noeud.Longitude);
                maxLong = Math.Max(maxLong, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            // calcul de l echelle
            double echelleLong = (largeur - 2 * marge) / (maxLong - minLong);
            double echelleLat = (hauteur - 2 * marge) / (maxLat - minLat);

            // compte le nombre de lignes par station
            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                if (!nombreLignesParStation.ContainsKey(noeud.NomStation))
                {
                    nombreLignesParStation[noeud.NomStation] = 1;
                }
                else
                {
                    nombreLignesParStation[noeud.NomStation]++;
                }
            }

            // dessine les liens 
            foreach (Lien<int> lien in graphe.Liens)
            {
                Noeud<int> noeud1 = graphe.Noeuds[lien.Noeud1.Id];
                Noeud<int> noeud2 = graphe.Noeuds[lien.Noeud2.Id];

                // conversion en pixels
                int x1 = (int)((noeud1.Longitude - minLong) * echelleLong) + marge;
                int y1 = hauteur - ((int)((noeud1.Latitude - minLat) * echelleLat) + marge);
                int x2 = (int)((noeud2.Longitude - minLong) * echelleLong) + marge;
                int y2 = hauteur - ((int)((noeud2.Latitude - minLat) * echelleLat) + marge);

                try
                {
                    Color couleurLigne = ColorTranslator.FromHtml(noeud1.CouleurLigne);
                    using (Pen pen = new Pen(couleurLigne, 2))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
                catch
                {
                    // si erreur de couleur utilise gris
                    using (Pen pen = new Pen(Color.Gray, 2))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }

            // dessine les noeuds et les noms des stations importantes
            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                int x = (int)((noeud.Longitude - minLong) * echelleLong) + marge;
                int y = hauteur - ((int)((noeud.Latitude - minLat) * echelleLat) + marge);

                positionsNoeuds[noeud.Id] = new Point(x, y);

                // noeud plus gros si cest une station de connexion
                int tailleNoeud = nombreLignesParStation[noeud.NomStation] > 1 ? 6 : 4;

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillEllipse(brush, x - tailleNoeud, y - tailleNoeud, tailleNoeud * 2, tailleNoeud * 2);
                }

                using (Pen pen = new Pen(Color.Black, nombreLignesParStation[noeud.NomStation] > 1 ? 2 : 1))
                {
                    graphics.DrawEllipse(pen, x - tailleNoeud, y - tailleNoeud, tailleNoeud * 2, tailleNoeud * 2);
                }

                // affiche uniquement les noms des stations de correspondance
                if (nombreLignesParStation[noeud.NomStation] > 1)
                {
                    using (Font font = new Font("Arial", 7, FontStyle.Bold))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        graphics.DrawString(noeud.NomStation, font, brush, x + 10, y - 10);
                    }
                }
            }
        }


        public void SauvegarderImage(string chemin)
        {
            if (File.Exists(chemin))
            {
                File.Delete(chemin); 
            }

            image.Save(chemin, ImageFormat.Png); // Sauvegarde l’image
        }
    }
}
