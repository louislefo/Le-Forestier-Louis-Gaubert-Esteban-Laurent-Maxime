using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace LivrableV3
{
    public class ColorationGraphe<T>
    {
        /// stocke les couleurs pour chaque noeud
        private Dictionary<Noeud<T>, int> couleursNoeuds;

        /// le nombre de couleurs utilisees
        public int NombreCouleurs { get; private set; }

        /// constructeur de la classe
        public ColorationGraphe()
        {
            couleursNoeuds = new Dictionary<Noeud<T>, int>();
            NombreCouleurs = 0;
        }

        /// applique lalgo de welsh powell pour colorier le graphe
        public Dictionary<Noeud<T>, int> AppliquerWelshPowell(Graphe<T> graphe)
        {
            couleursNoeuds.Clear();
            List<Noeud<T>> listeNoeuds = new List<Noeud<T>>();

            for (int i = 0; i < graphe.Noeuds.Count; i++)
            {
                listeNoeuds.Add(graphe.Noeuds.ElementAt(i).Value);
            }

            for (int i = 0; i < listeNoeuds.Count - 1; i++)
            {
                for (int j = 0; j < listeNoeuds.Count - i - 1; j++)
                {
                    if (listeNoeuds[j].Voisins.Count < listeNoeuds[j + 1].Voisins.Count)
                    {
                        Noeud<T> temp = listeNoeuds[j];
                        listeNoeuds[j] = listeNoeuds[j + 1];
                        listeNoeuds[j + 1] = temp;
                    }
                }
            }

            int couleurActuelle = 1;

            for (int i = 0; i < listeNoeuds.Count; i++)
            {
                Noeud<T> noeud = listeNoeuds[i];
                bool couleurTrouvee = false;
                int couleurTestee = 1;

                while (!couleurTrouvee)
                {
                    couleurTrouvee = true;

                    for (int j = 0; j < noeud.Voisins.Count; j++)
                    {
                        Noeud<T> voisin = noeud.Voisins[j];
                        if (couleursNoeuds.ContainsKey(voisin) && couleursNoeuds[voisin] == couleurTestee)
                        {
                            couleurTrouvee = false;
                            couleurTestee++;
                            break;
                        }
                    }
                }

                couleursNoeuds[noeud] = couleurTestee;

                if (couleurTestee > couleurActuelle)
                {
                    couleurActuelle = couleurTestee;
                }
            }

            NombreCouleurs = couleurActuelle;
            return couleursNoeuds;
        }

        /// verifie si le graphe est biparti
        public bool EstBiparti()
        {
            return NombreCouleurs == 2;
        }

        /// verifie si le graphe est planaire selon le theoreme des 4 couleurs
        public bool EstPlanaire()
        {
            return NombreCouleurs <= 4;
        }

        /// trouve les groupes independants a partir de la coloration
        public List<List<Noeud<T>>> TrouverGroupesIndependants()
        {
            List<List<Noeud<T>>> groupes = new List<List<Noeud<T>>>();

            for (int i = 1; i <= NombreCouleurs; i++)
            {
                groupes.Add(new List<Noeud<T>>());
            }

            for (int i = 0; i < couleursNoeuds.Count; i++)
            {
                var noeud = couleursNoeuds.ElementAt(i);
                groupes[noeud.Value - 1].Add(noeud.Key);
            }

            return groupes;
        }

        public void AfficherResultats(Graphe<int> grapheMetro, Dictionary<T, Noeud<int>> correspondanceStations)
        {
            Console.WriteLine("Resultats de la coloration des clients et cuisiniers :");
            Console.WriteLine("Nombre de couleurs utilisees : " + NombreCouleurs);
            Console.WriteLine();

            Console.WriteLine("Le graphe est-il biparti ? " + (EstBiparti() ? "Oui" : "Non"));
            if (EstBiparti())
            {
                Console.WriteLine("car on peut le colorier avec 2 couleurs");
            }
            else
            {
                Console.WriteLine("car il faut plus de 2 couleurs");
            }
            Console.WriteLine();

            Console.WriteLine("Le graphe est-il planaire ? " + (EstPlanaire() ? "Oui" : "Non"));
            if (EstPlanaire())
            {
                Console.WriteLine("car on peut le colorier avec 4 couleurs ou moins");
            }
            else
            {
                Console.WriteLine("car il faut plus de 4 couleurs");
            }
            Console.WriteLine();

            AfficherGrapheColore(grapheMetro, correspondanceStations);
        }

        /// affiche le graphe des clients et cuisiniers colore
        public void AfficherGrapheColore(Graphe<int> grapheMetro, Dictionary<T, Noeud<int>> correspondanceStations)
        {
            Form fenetreGraphe = new Form();
            fenetreGraphe.Text = "Graphe des Clients et Cuisiniers Colore";
            fenetreGraphe.Size = new Size(800, 600);

            Panel panelGraphe = new Panel();
            panelGraphe.Dock = DockStyle.Fill;
            panelGraphe.Paint += (sender, e) =>
            {
                AfficherCarteOSM visMetro = new AfficherCarteOSM(panelGraphe.Width, panelGraphe.Height);
                visMetro.DessinerGraphe(grapheMetro);
                e.Graphics.DrawImage(visMetro.GetImage(), 0, 0, panelGraphe.Width, panelGraphe.Height);
                DessinerClientsCuisiniers(e.Graphics, grapheMetro, correspondanceStations);
            };
            fenetreGraphe.Controls.Add(panelGraphe);
            fenetreGraphe.ShowDialog();
        }

        /// dessine les clients et cuisiniers sur la carte
        private void DessinerClientsCuisiniers(Graphics g, Graphe<int> grapheMetro, Dictionary<T, Noeud<int>> correspondanceStations)
        {
            var groupes = TrouverGroupesIndependants();

            double minLong = double.MaxValue;
            double maxLong = double.MinValue;
            double minLat = double.MaxValue;
            double maxLat = double.MinValue;

            foreach (var noeud in grapheMetro.Noeuds.Values)
            {
                minLong = Math.Min(minLong, noeud.Longitude);
                maxLong = Math.Max(maxLong, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            double marge = 0.005;
            minLong -= marge;
            maxLong += marge;
            minLat -= marge;
            maxLat += marge;

            double echelleLong = (800 - 2 * 50) / (maxLong - minLong);
            double echelleLat = (600 - 2 * 50) / (maxLat - minLat);

            for (int i = 0; i < groupes.Count; i++)
            {
                Color couleur = i == 0 ? Color.Red : Color.Blue;
                foreach (var noeud in groupes[i])
                {
                    if (correspondanceStations.TryGetValue(noeud.Id, out var stationMetro))
                    {
                        int x = (int)((stationMetro.Longitude - minLong) * echelleLong) + 50;
                        int y = 600 - ((int)((stationMetro.Latitude - minLat) * echelleLat) + 50);

                        g.FillEllipse(new SolidBrush(couleur), x - 6, y - 6, 12, 12);

                        string texteStation = stationMetro.NomStation;
                        string textePersonne = noeud.Nom;

                        Rectangle rectStation = new Rectangle(x + 8, y - 8, 100, 15);
                        Rectangle rectPersonne = new Rectangle(x + 8, y + 2, 150, 15);

                        g.FillRectangle(Brushes.White, rectStation);
                        g.FillRectangle(Brushes.White, rectPersonne);

                        g.DrawString(texteStation, new Font("Arial", 6), Brushes.Black, x + 8, y - 8);
                        g.DrawString((i == 0 ? "Cuisinier: " : "Client: ") + textePersonne, new Font("Arial", 6, FontStyle.Bold), Brushes.Black, x + 8, y + 2);
                    }
                }
            }

            int legendeX = 20;
            int legendeY = 20;

            Rectangle rectLegende = new Rectangle(legendeX - 5, legendeY - 5, 200, 100);
            g.FillRectangle(Brushes.White, rectLegende);

            string infoGraphe = "Nombre de couleurs : " + NombreCouleurs + "\n" +
                               "Graphe biparti : " + (EstBiparti() ? "Oui" : "Non") + "\n" +
                               "Graphe planaire : " + (EstPlanaire() ? "Oui" : "Non");
            g.DrawString(infoGraphe, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, legendeX, legendeY);
            legendeY += 80;

            g.FillRectangle(new SolidBrush(Color.Red), legendeX, legendeY, 10, 10);
            g.DrawString("Cuisiniers", new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
            legendeY += 20;

            g.FillRectangle(new SolidBrush(Color.Blue), legendeX, legendeY, 10, 10);
            g.DrawString("Clients", new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
        }
    }
}