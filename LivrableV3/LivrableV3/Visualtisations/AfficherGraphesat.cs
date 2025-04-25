using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace LivrableV3
{
    /// affiche le graphe du metro sur une carte osm pas ouf
    public class AfficherCarteOSM
    {
        private Bitmap imageCarte;
        private Graphics dessin;
        private int largeur;
        private int hauteur;
        private int marge = 50;

        private Dictionary<int, Point> positionsStations;
        private Dictionary<string, int> nbLignesParStation;

        public AfficherCarteOSM(int largeur, int hauteur)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.imageCarte = new Bitmap(largeur, hauteur);
            this.dessin = Graphics.FromImage(imageCarte);
            this.dessin.Clear(Color.White);
            this.positionsStations = new Dictionary<int, Point>();
            this.nbLignesParStation = new Dictionary<string, int>();
        }

        /// convertit lat lon en position sur la tuile
        private (double pixelX, double pixelY) ConvertirLatLonEnPixels(double lat, double lon, double minLat, double maxLat, double minLon, double maxLon)
        {
            double x = (lon - minLon) / (maxLon - minLon) * (largeur - 2 * marge) + marge;
            double y = hauteur - ((lat - minLat) / (maxLat - minLat) * (hauteur - 2 * marge) + marge);
            return (x, y);
        }

        /// convertit lat lon en numero de tuile osm
        private (int x, int y) LatLonEnTuile(double lat, double lon, int zoom)
        {
            int x = (int)((lon + 180.0) / 360.0 * (1 << zoom));
            double latRad = lat * Math.PI / 180.0;
            int y = (int)((1.0 - Math.Log(Math.Tan(latRad) + 1.0 / Math.Cos(latRad)) / Math.PI) / 2.0 * (1 << zoom));
            return (x, y);
        }

        private void ChargerImageCarte(double minLat, double maxLat, double minLon, double maxLon)
        {
            try
            {
                int zoom = 13;  // zoom plus petit pour voir plus large
                (int x1, int y1) = LatLonEnTuile(maxLat, minLon, zoom);  // inverser maxLat et minLat
                (int x2, int y2) = LatLonEnTuile(minLat, maxLon, zoom);

                int nbTuilesX = x2 - x1 + 1;
                int nbTuilesY = y2 - y1 + 1;

                int largeurTotale = nbTuilesX * 256;
                int hauteurTotale = nbTuilesY * 256;

                double facteurEchelle = Math.Min((double)largeur / largeurTotale, (double)hauteur / hauteurTotale);
                
                int nouvelleLargeur = (int)(largeurTotale * facteurEchelle);
                int nouvelleHauteur = (int)(hauteurTotale * facteurEchelle);

                Bitmap carteComplete = new Bitmap(largeurTotale, hauteurTotale);
                Graphics g = Graphics.FromImage(carteComplete);

                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        string url = $"https://tile.openstreetmap.org/{zoom}/{x}/{y}.png";

                        using (WebClient client = new WebClient())
                        {
                            client.Headers.Add("User-Agent", "LivrableV3MetroApp/1.0 (leforestierlouis26@gmail.com)");
                            byte[] imageData = client.DownloadData(url);

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Bitmap tuile = new Bitmap(ms);
                                g.DrawImage(tuile, (x - x1) * 256, (y - y1) * 256);
                            }
                        }
                        System.Threading.Thread.Sleep(200);
                    }
                }

                g.Dispose();
                dessin.Clear(Color.White);
                dessin.DrawImage(carteComplete, 0, 0, largeur, hauteur);
                carteComplete.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des tuiles OSM : " + ex.Message);
                dessin.Clear(Color.LightGray);
            }
        }

        public void DessinerGraphe(Graphe<int> graphe)
        {
            double minLon = double.MaxValue, maxLon = double.MinValue;
            double minLat = double.MaxValue, maxLat = double.MinValue;

            foreach (var noeud in graphe.Noeuds.Values)
            {
                minLon = Math.Min(minLon, noeud.Longitude);
                maxLon = Math.Max(maxLon, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            double margeGeo = 0.01;
            minLon -= margeGeo; maxLon += margeGeo;
            minLat -= margeGeo; maxLat += margeGeo;

            ChargerImageCarte(minLat, maxLat, minLon, maxLon);

            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (!nbLignesParStation.ContainsKey(noeud.NomStation))
                    nbLignesParStation[noeud.NomStation] = 1;
                else
                    nbLignesParStation[noeud.NomStation]++;
            }

            foreach (var lien in graphe.Liens)
            {
                var s1 = lien.Noeud1;
                var s2 = lien.Noeud2;

                (double x1, double y1) = ConvertirLatLonEnPixels(s1.Latitude, s1.Longitude, minLat, maxLat, minLon, maxLon);
                (double x2, double y2) = ConvertirLatLonEnPixels(s2.Latitude, s2.Longitude, minLat, maxLat, minLon, maxLon);

                try
                {
                    Color couleur = ColorTranslator.FromHtml(s1.CouleurLigne);
                    using (Pen crayon = new Pen(couleur, 3))
                    {
                        dessin.DrawLine(crayon, (int)x1, (int)y1, (int)x2, (int)y2);
                    }
                }
                catch
                {
                    using (Pen crayon = new Pen(Color.White, 3))
                    {
                        dessin.DrawLine(crayon, (int)x1, (int)y1, (int)x2, (int)y2);
                    }
                }
            }

            foreach (var noeud in graphe.Noeuds.Values)
            {
                (double x, double y) = ConvertirLatLonEnPixels(noeud.Latitude, noeud.Longitude, minLat, maxLat, minLon, maxLon);
                positionsStations[noeud.Id] = new Point((int)x, (int)y);

                int taille = nbLignesParStation[noeud.NomStation] > 1 ? 8 : 5;

                using (SolidBrush blanc = new SolidBrush(Color.White))
                {
                    dessin.FillEllipse(blanc, (int)x - taille, (int)y - taille, taille * 2, taille * 2);
                }

                using (Pen contour = new Pen(Color.Black, nbLignesParStation[noeud.NomStation] > 1 ? 2 : 1))
                {
                    dessin.DrawEllipse(contour, (int)x - taille, (int)y - taille, taille * 2, taille * 2);
                }

                if (nbLignesParStation[noeud.NomStation] > 1)
                {
                    using (Font police = new Font("Arial", 8, FontStyle.Bold))
                    {
                        SizeF tailleTexte = dessin.MeasureString(noeud.NomStation, police);
                        using (SolidBrush fond = new SolidBrush(Color.White))
                        {
                            dessin.FillRectangle(fond, (int)x + 10, (int)y - 10, tailleTexte.Width, tailleTexte.Height);
                        }
                        using (SolidBrush texte = new SolidBrush(Color.Black))
                        {
                            dessin.DrawString(noeud.NomStation, police, texte, (int)x + 10, (int)y - 10);
                        }
                    }
                }
            }
        }

        public void SauvegarderImage(string chemin)
        {
            if (File.Exists(chemin))
                File.Delete(chemin);

            imageCarte.Save(chemin, ImageFormat.Png);
        }

        /// retourne l'image du graphe
        public Bitmap GetImage()
        {
            return imageCarte;
        }
    }
}
