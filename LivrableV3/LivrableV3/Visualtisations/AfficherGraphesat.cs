using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace LivrableV3
{
    /// Affiche le graphe du métro sur une carte OpenStreetMap classique
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

        private (int, int) LatLonEnTuile(double lat, double lon, int zoom)
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
                int zoom = 14;
                var (x1, y1) = LatLonEnTuile(minLat, minLon, zoom);
                var (x2, y2) = LatLonEnTuile(maxLat, maxLon, zoom);

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
                                int posX = (x - x1) * 256;
                                int posY = (y - y1) * 256;
                                dessin.DrawImage(tuile, posX, posY);
                            }
                        }

                        System.Threading.Thread.Sleep(200);
                    }
                }
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

            double echelleLon = (largeur - 2 * marge) / (maxLon - minLon);
            double echelleLat = (hauteur - 2 * marge) / (maxLat - minLat);

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

                int x1 = (int)((s1.Longitude - minLon) * echelleLon + marge);
                int y1 = hauteur - (int)((s1.Latitude - minLat) * echelleLat + marge);
                int x2 = (int)((s2.Longitude - minLon) * echelleLon + marge);
                int y2 = hauteur - (int)((s2.Latitude - minLat) * echelleLat + marge);

                try
                {
                    Color couleur = ColorTranslator.FromHtml(s1.CouleurLigne);
                    using (Pen crayon = new Pen(couleur, 3))
                    {
                        dessin.DrawLine(crayon, x1, y1, x2, y2);
                    }
                }
                catch
                {
                    using (Pen crayon = new Pen(Color.White, 3))
                    {
                        dessin.DrawLine(crayon, x1, y1, x2, y2);
                    }
                }
            }

            foreach (var noeud in graphe.Noeuds.Values)
            {
                int x = (int)Math.Round((noeud.Longitude - minLon) * echelleLon + marge);
                int y = hauteur - (int)Math.Round((noeud.Latitude - minLat) * echelleLat + marge);
                positionsStations[noeud.Id] = new Point(x, y);

                int taille = nbLignesParStation[noeud.NomStation] > 1 ? 8 : 5;

                using (SolidBrush blanc = new SolidBrush(Color.White))
                {
                    dessin.FillEllipse(blanc, x - taille, y - taille, taille * 2, taille * 2);
                }

                using (Pen contour = new Pen(Color.Black, nbLignesParStation[noeud.NomStation] > 1 ? 2 : 1))
                {
                    dessin.DrawEllipse(contour, x - taille, y - taille, taille * 2, taille * 2);
                }

                if (nbLignesParStation[noeud.NomStation] > 1)
                {
                    using (Font police = new Font("Arial", 8, FontStyle.Bold))
                    {
                        SizeF tailleTexte = dessin.MeasureString(noeud.NomStation, police);
                        using (SolidBrush fond = new SolidBrush(Color.White))
                        {
                            dessin.FillRectangle(fond, x + 10, y - 10, tailleTexte.Width, tailleTexte.Height);
                        }
                        using (SolidBrush texte = new SolidBrush(Color.Black))
                        {
                            dessin.DrawString(noeud.NomStation, police, texte, x + 10, y - 10);
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
    }
}
