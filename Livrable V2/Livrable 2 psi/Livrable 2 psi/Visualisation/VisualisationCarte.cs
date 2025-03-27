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
    public class VisualisationCarte
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
            int maxTentatives = 8; // nombre maximum de tentatives pour trouver une position

            for (int i = 0; i < maxTentatives && !positionTrouvee; i++)
            {
                // teste differentes positions autour du point
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

                // verifie si la position est libre
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
        /// dessine le graphe du metro
        /// </summary>
        public void DessinerGraphe(Graphe<int> graphe)
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

            // Compter le nombre de lignes par station
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

            // Dessiner les liens
            foreach (Lien<int> lien in graphe.Liens)
            {
                Noeud<int> noeud1 = graphe.Noeuds[lien.Noeud1.Id];
                Noeud<int> noeud2 = graphe.Noeuds[lien.Noeud2.Id];

                // Convertir les coordonnées en pixels
                int x1 = (int)((noeud1.Longitude - minLong) * echelleLong) + marge;
                int y1 = hauteur - ((int)((noeud1.Latitude - minLat) * echelleLat) + marge);
                int x2 = (int)((noeud2.Longitude - minLong) * echelleLong) + marge;
                int y2 = hauteur - ((int)((noeud2.Latitude - minLat) * echelleLat) + marge);

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
                    Console.WriteLine("Erreur de couleur: " + e.Message + ", utilisation de la couleur par défaut");
                    using (Pen pen = new Pen(Color.Gray, 2))
                    {
                        graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }

            // Dessiner les noeuds et calculer les positions des textes
            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                int x = (int)((noeud.Longitude - minLong) * echelleLong) + marge;
                int y = hauteur - ((int)((noeud.Latitude - minLat) * echelleLat) + marge);

                positionsNoeuds[noeud.Id] = new Point(x, y);

                // Dessiner le noeud plus gros si c'est une station de connexion
                int tailleNoeud = nombreLignesParStation[noeud.NomStation] > 1 ? 6 : 4;

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillEllipse(brush, x - tailleNoeud, y - tailleNoeud, tailleNoeud * 2, tailleNoeud * 2);
                }

                using (Pen pen = new Pen(Color.Black, nombreLignesParStation[noeud.NomStation] > 1 ? 2 : 1))
                {
                    graphics.DrawEllipse(pen, x - tailleNoeud, y - tailleNoeud, tailleNoeud * 2, tailleNoeud * 2);
                }

                // Calculer la position du texte seulement si le nom n'a pas déjà été affiché
                if (!nomsDejaAffiches.ContainsKey(noeud.NomStation))
                {
                    Point positionTexte = CalculerPositionTexte(x, y, noeud.NomStation);
                    positionsTextes[noeud.Id] = positionTexte;
                    nomsDejaAffiches[noeud.NomStation] = true;
                }
            }

            // Dessiner les noms des stations
            foreach (Noeud<int> noeud in graphe.Noeuds.Values)
            {
                if (positionsTextes.ContainsKey(noeud.Id))
                {
                    Point positionTexte = positionsTextes[noeud.Id];
                    // Utiliser une police en gras pour les stations de connexion
                    FontStyle style = nombreLignesParStation[noeud.NomStation] > 1 ? FontStyle.Bold : FontStyle.Regular;
                    using (Font font = new Font("Arial", 7, style))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        graphics.DrawString(noeud.NomStation, font, brush, positionTexte.X, positionTexte.Y);
                    }
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
