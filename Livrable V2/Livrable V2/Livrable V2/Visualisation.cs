using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_V2
{
    public class Visualisation
    {
        private Graphe graphe;
        private int largeur;
        private int hauteur;
        private int rayonNoeud = 20; // rayon du cercle représentant un nœud

        /// <summary>
        /// Constructeur de la classe Visualisation.
        /// </summary>
        /// <param name="graphe">Le graphe à visualiser</param>
        /// <param name="largeur">Largeur de l’image (par défaut 800)</param>
        /// <param name="hauteur">Hauteur de l’image (par défaut 600)</param>
        public Visualisation(Graphe graphe, int largeur = 800, int hauteur = 600)
        {
            this.graphe = graphe;
            this.largeur = largeur;
            this.hauteur = hauteur;
        }

        /// <summary>
        /// Dessine le graphe et retourne une image Bitmap.
        /// Les nœuds sont placés sur un cercle et les liens sont tracés entre eux.
        /// </summary>
        /// <returns>Bitmap contenant le dessin du graphe</returns>
        public Bitmap DessinerGraphe()
        {
            Bitmap bmp = new Bitmap(largeur, hauteur);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            // Disposition circulaire des nœuds
            int nbNodes = graphe.Noeuds.Count;
            if (nbNodes == 0)
                return bmp;

            int centreX = largeur / 2;
            int centreY = hauteur / 2;
            int rayonDisposition = Math.Min(largeur, hauteur) / 2 - rayonNoeud - 0;

            // Dictionnaire pour stocker la position (coordonnées) de chaque nœud
            Dictionary<int, Point> positions = new Dictionary<int, Point>();
            int index = 0;
            foreach (var noeud in graphe.Noeuds.Values)
            {
                double angle = 2 * Math.PI * index / nbNodes;
                int x = centreX + (int)(rayonDisposition * Math.Cos(angle));
                int y = centreY + (int)(rayonDisposition * Math.Sin(angle));
                positions[noeud.Id] = new Point(x, y);
                index++;
            }

            // Dessiner les liens (les arêtes)
            Pen penLien = new Pen(Color.Black, 2);
            foreach (Lien lien in graphe.Liens)
            {
                Point p1 = positions[lien.Noeud1.Id];
                Point p2 = positions[lien.Noeud2.Id];
                g.DrawLine(penLien, p1, p2);
            }

            // Dessiner les nœuds
            Brush brushNoeud = Brushes.Blue;
            Pen penNoeud = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 10);
            foreach (var noeud in graphe.Noeuds.Values)
            {
                Point pos = positions[noeud.Id];
                Rectangle rect = new Rectangle(pos.X - rayonNoeud, pos.Y - rayonNoeud, rayonNoeud * 2, rayonNoeud * 2);
                g.FillEllipse(brushNoeud, rect);
                g.DrawEllipse(penNoeud, rect);

                // Afficher l’identifiant du nœud centré dans le cercle
                string idStr = noeud.Id.ToString();
                SizeF tailleTexte = g.MeasureString(idStr, font);
                g.DrawString(idStr, font, Brushes.White, pos.X - tailleTexte.Width / 2, pos.Y - tailleTexte.Height / 2);
            }

            g.Dispose();
            return bmp;
        }

        /// <summary>
        /// Sauvegarde l’image du graphe dans un fichier.
        /// </summary>
        /// <param name="cheminFichier">Chemin et nom du fichier (par ex. "graphe.png")</param>
        public void SauvegarderGraphique(string cheminFichier)
        {
            using (Bitmap bmp = DessinerGraphe())
            {
                bmp.Save(cheminFichier, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
