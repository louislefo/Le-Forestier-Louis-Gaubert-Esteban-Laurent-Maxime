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
            // on vide le dictionnaire des couleurs
            couleursNoeuds.Clear();

            // on cree une liste pour stocker les noeuds
            List<Noeud<T>> listeNoeuds = new List<Noeud<T>>();

            // on met tous les noeuds dans la liste
            for (int i = 0; i < graphe.Noeuds.Count; i++)
            {
                listeNoeuds.Add(graphe.Noeuds.ElementAt(i).Value);
            }

            // on trie les noeuds par nombre de voisins decroissant
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

            // on commence avec la couleur 1
            int couleurActuelle = 1;

            // on colorie chaque noeud
            for (int i = 0; i < listeNoeuds.Count; i++)
            {
                Noeud<T> noeud = listeNoeuds[i];

                // on cherche la plus petite couleur disponible
                bool couleurTrouvee = false;
                int couleurTestee = 1;

                while (!couleurTrouvee)
                {
                    couleurTrouvee = true;

                    // on verifie tous les voisins
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

                // on colorie le noeud avec la couleur trouvee
                couleursNoeuds[noeud] = couleurTestee;

                // on met a jour le nombre de couleurs si necessaire
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

            // on initialise les listes pour chaque couleur
            for (int i = 1; i <= NombreCouleurs; i++)
            {
                groupes.Add(new List<Noeud<T>>());
            }

            // on met les noeuds dans leurs groupes
            for (int i = 0; i < couleursNoeuds.Count; i++)
            {
                var noeud = couleursNoeuds.ElementAt(i);
                groupes[noeud.Value - 1].Add(noeud.Key);
            }

            return groupes;
        }

        /// affiche les resultats de la coloration
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

            // on affiche le graphe colore
            AfficherGrapheColore(grapheMetro, correspondanceStations);
        }

        /// affiche le graphe des clients et cuisiniers colore
        public void AfficherGrapheColore(Graphe<int> grapheMetro, Dictionary<T, Noeud<int>> correspondanceStations)
        {
            // on cree une nouvelle fenetre pour afficher le graphe
            Form fenetreGraphe = new Form();
            fenetreGraphe.Text = "Graphe des Clients et Cuisiniers Colore";
            fenetreGraphe.Size = new Size(800, 600);

            // on cree un panel pour dessiner le graphe
            Panel panelGraphe = new Panel();
            panelGraphe.Dock = DockStyle.Fill;
            panelGraphe.Paint += (sender, e) =>
            {
                // on cree une nouvelle instance de visualisation carte osm
                AfficherCarteOSM visMetro = new AfficherCarteOSM(panelGraphe.Width, panelGraphe.Height);
            
                // on dessine le metro directement sur le graphics du panel
                visMetro.DessinerGraphe(grapheMetro);
            
                // on dessine l'image du metro
                e.Graphics.DrawImage(visMetro.GetImage(), 0, 0, panelGraphe.Width, panelGraphe.Height);

                // on dessine les clients et cuisiniers
                DessinerClientsCuisiniers(e.Graphics, grapheMetro, correspondanceStations);
            };
            fenetreGraphe.Controls.Add(panelGraphe);

            // on affiche la fenetre
            fenetreGraphe.ShowDialog();
        }

        /// dessine les clients et cuisiniers sur la carte
        private void DessinerClientsCuisiniers(Graphics g, Graphe<int> grapheMetro, Dictionary<T, Noeud<int>> correspondanceStations)
        {
            // on recupere les groupes de noeuds
            var groupes = TrouverGroupesIndependants();

            // on calcule les limites de la carte en utilisant le graphe du metro
            double minLong = double.MaxValue;
            double maxLong = double.MinValue;
            double minLat = double.MaxValue;
            double maxLat = double.MinValue;

            // on utilise les coordonnees du graphe du metro pour les limites
            foreach (var noeud in grapheMetro.Noeuds.Values)
            {
                minLong = Math.Min(minLong, noeud.Longitude);
                maxLong = Math.Max(maxLong, noeud.Longitude);
                minLat = Math.Min(minLat, noeud.Latitude);
                maxLat = Math.Max(maxLat, noeud.Latitude);
            }

            // on ajoute une petite marge
            double marge = 0.005;
            minLong -= marge;
            maxLong += marge;
            minLat -= marge;
            maxLat += marge;

            // on calcule l echelle pour le dessin
            double echelleLong = (800 - 2 * 50) / (maxLong - minLong);
            double echelleLat = (600 - 2 * 50) / (maxLat - minLat);

            // on dessine les clients et cuisiniers
            for (int i = 0; i < groupes.Count; i++)
            {
                Color couleur = i == 0 ? Color.Red : Color.Blue;
                foreach (var noeud in groupes[i])
                {
                    if (correspondanceStations.TryGetValue(noeud.Id, out var stationMetro))
                    {
                        // on utilise les coordonnees exactes de la station du metro
                        int x = (int)((stationMetro.Longitude - minLong) * echelleLong) + 50;
                        int y = 600 - ((int)((stationMetro.Latitude - minLat) * echelleLat) + 50);

                        // on dessine un point plus gros pour le client/cuisinier
                        g.FillEllipse(new SolidBrush(couleur), x - 6, y - 6, 12, 12);

                        // on affiche le nom de la station et de la personne
                        string texteStation = stationMetro.NomStation; // on utilise le nom de la station du metro
                        string textePersonne = noeud.Nom;

                        // on dessine les textes avec un fond blanc pour plus de lisibilite
                        Rectangle rectStation = new Rectangle(x + 8, y - 8, 100, 15);
                        Rectangle rectPersonne = new Rectangle(x + 8, y + 2, 150, 15);

                        g.FillRectangle(Brushes.White, rectStation);
                        g.FillRectangle(Brushes.White, rectPersonne);

                        g.DrawString(texteStation, new Font("Arial", 6), Brushes.Black, x + 8, y - 8);
                        g.DrawString((i == 0 ? "Cuisinier: " : "Client: ") + textePersonne, new Font("Arial", 6, FontStyle.Bold), Brushes.Black, x + 8, y + 2);
                    }
                }
            }

            // on ajoute une legende
            int legendeX = 20;
            int legendeY = 20;

            // on dessine un fond blanc pour la legende
            Rectangle rectLegende = new Rectangle(legendeX - 5, legendeY - 5, 200, 100);
            g.FillRectangle(Brushes.White, rectLegende);

            // Informations sur le graphe
            string infoGraphe = "Nombre de couleurs : " + NombreCouleurs + "\n" +
                               "Graphe biparti : " + (EstBiparti() ? "Oui" : "Non") + "\n" +
                               "Graphe planaire : " + (EstPlanaire() ? "Oui" : "Non");
            g.DrawString(infoGraphe, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, legendeX, legendeY);
            legendeY += 80;

            // Legende des couleurs
            g.FillRectangle(new SolidBrush(Color.Red), legendeX, legendeY, 10, 10);
            g.DrawString("Cuisiniers", new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
            legendeY += 20;

            g.FillRectangle(new SolidBrush(Color.Blue), legendeX, legendeY, 10, 10);
            g.DrawString("Clients", new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
        }
    }
}