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
    /// <summary>
    /// cette classe affiche le graphe du metro
    /// elle dessine les stations et les lignes avec leurs couleurs
    /// </summary>
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
        /// cette methode dessine le graphe du metro
        /// elle affiche les stations et les lignes avec leurs couleurs
        /// </summary>
        public void DessinerGraphe(Graphe<int> graphe)
        {
            double minLong = double.MaxValue, maxLong = double.MinValue;
            double minLat = double.MaxValue, maxLat = double.MinValue;

            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                minLong = Math.Min(minLong, noeud.Longitude);
                maxLong = Math.Max(maxLong, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            double echelleLong = (largeur - 2 * marge) / (maxLong - minLong);
            double echelleLat = (hauteur - 2 * marge) / (maxLat - minLat);

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

            foreach (Lien<int> lien in graphe.Liens)
            {
                Noeud<int> noeud1 = graphe.Noeuds[lien.Noeud1.Id];
                Noeud<int> noeud2 = graphe.Noeuds[lien.Noeud2.Id];

                int x1 = (int)((noeud1.Longitude - minLong) * echelleLong) + marge;
                int y1 = hauteur - ((int)((noeud1.Latitude - minLat) * echelleLat) + marge);
                int x2 = (int)((noeud2.Longitude - minLong) * echelleLong) + marge;
                int y2 = hauteur - ((int)((noeud2.Latitude - minLat) * echelleLat) + marge);

                bool allerRetour = LienExiste(graphe, lien.Noeud2.Id, lien.Noeud1.Id);

                int decal = 0;
                if (allerRetour)
                {
                    decal = 7;
                }

                double dx = x2 - x1;
                double dy = y2 - y1;
                double longueur = Math.Sqrt(dx * dx + dy * dy);
                double px = 0;
                double py = 0;
                if (longueur != 0)
                {
                    px = -dy / longueur * decal;
                    py = dx / longueur * decal;
                }

                int nx1 = x1 + (int)px;
                int ny1 = y1 + (int)py;
                int nx2 = x2 + (int)px;
                int ny2 = y2 + (int)py;

                try
                {
                    Color couleurLigne = ColorTranslator.FromHtml(noeud1.CouleurLigne);
                    using (Pen pen = new Pen(couleurLigne, 2))
                    {
                        graphics.DrawLine(pen, nx1, ny1, nx2, ny2);
                    }
                    DessinerFleche(nx1, ny1, nx2, ny2, couleurLigne);
                }
                catch
                {
                    using (Pen pen = new Pen(Color.Gray, 2))
                    {
                        graphics.DrawLine(pen, nx1, ny1, nx2, ny2);
                    }
                    DessinerFleche(nx1, ny1, nx2, ny2, Color.Gray);
                }
            }

            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                int x = (int)((noeud.Longitude - minLong) * echelleLong) + marge;
                int y = hauteur - ((int)((noeud.Latitude - minLat) * echelleLat) + marge);

                positionsNoeuds[noeud.Id] = new Point(x, y);

                int tailleNoeud = nombreLignesParStation[noeud.NomStation] > 1 ? 6 : 4;

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillEllipse(brush, x - tailleNoeud, y - tailleNoeud, tailleNoeud * 2, tailleNoeud * 2);
                }

                using (Pen pen = new Pen(Color.Black, nombreLignesParStation[noeud.NomStation] > 1 ? 2 : 1))
                {
                    graphics.DrawEllipse(pen, x - tailleNoeud, y - tailleNoeud, tailleNoeud * 2, tailleNoeud * 2);
                }

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

        private bool LienExiste(Graphe<int> graphe, int id1, int id2)
        {
            for (int i = 0; i < graphe.Liens.Count; i++)
            {
                if (graphe.Liens[i].Noeud1.Id == id1 && graphe.Liens[i].Noeud2.Id == id2)
                {
                    return true;
                }
            }
            return false;
        }

        public void DessinerFleche(int x1, int y1, int x2, int y2, Color couleur)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double longueur = Math.Sqrt(dx * dx + dy * dy);
            if (longueur == 0) return;
            double ratio = 10.0 / longueur;
            int xf = (int)(x2 - dx * ratio);
            int yf = (int)(y2 - dy * ratio);
            int taille = 7;
            double angle = Math.Atan2(dy, dx);
            Point[] fleche = new Point[3];
            fleche[0] = new Point(x2, y2);
            fleche[1] = new Point((int)(xf - taille * Math.Sin(angle - 0.5)), (int)(yf + taille * Math.Cos(angle - 0.5)));
            fleche[2] = new Point((int)(xf - taille * Math.Sin(angle + 0.5)), (int)(yf + taille * Math.Cos(angle + 0.5)));
            using (SolidBrush brush = new SolidBrush(couleur))
            {
                graphics.FillPolygon(brush, fleche);
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

        /// <summary>
        /// cette methode retourne l'image du graphe
        /// </summary>
        public Bitmap GetImage()
        {
            return image;
        }
    }
}
