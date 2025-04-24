using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

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

        /// affiche le graphe des clients et cuisiniers colore
        public void AfficherGrapheColore()
        {
            // on cree une nouvelle fenetre pour afficher le graphe
            Form fenetreGraphe = new Form();
            fenetreGraphe.Text = "Graphe des Clients et Cuisiniers Colore";
            fenetreGraphe.Size = new Size(800, 600);

            // on cree un panel pour dessiner le graphe
            Panel panelGraphe = new Panel();
            panelGraphe.Dock = DockStyle.Fill;
            panelGraphe.Paint += (sender, e) => DessinerGraphe(e.Graphics);
            fenetreGraphe.Controls.Add(panelGraphe);

            // on affiche la fenetre
            fenetreGraphe.ShowDialog();
        }

        /// dessine le graphe des clients et cuisiniers
        private void DessinerGraphe(Graphics g)
        {
            // on cree un tableau de couleurs pour les noeuds
            Color[] couleurs = new Color[]
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Magenta,
                Color.Cyan,
                Color.Orange,
                Color.Purple
            };

            // on recupere les groupes de noeuds
            var groupes = TrouverGroupesIndependants();

            // on calcule les positions des noeuds
            Dictionary<Noeud<T>, Point> positions = new Dictionary<Noeud<T>, Point>();
            int rayon = 10;
            int espacement = 100;

            // on place les noeuds en cercle
            int centreX = 400;
            int centreY = 300;
            int rayonCercle = 200;

            for (int i = 0; i < groupes.Count; i++)
            {
                for (int j = 0; j < groupes[i].Count; j++)
                {
                    double angle = 2 * Math.PI * j / groupes[i].Count;
                    int x = centreX + (int)(rayonCercle * Math.Cos(angle));
                    int y = centreY + (int)(rayonCercle * Math.Sin(angle));
                    positions[groupes[i][j]] = new Point(x, y);
                }
            }

            // on dessine les liens entre les noeuds
            foreach (var groupe in groupes)
            {
                foreach (var noeud in groupe)
                {
                    foreach (var voisin in noeud.Voisins)
                    {
                        if (positions.ContainsKey(voisin))
                        {
                            g.DrawLine(Pens.Gray, positions[noeud], positions[voisin]);
                        }
                    }
                }
            }

            // on dessine les noeuds avec leurs couleurs
            for (int i = 0; i < groupes.Count; i++)
            {
                Color couleurActuelle = couleurs[i % couleurs.Length];
                foreach (var noeud in groupes[i])
                {
                    if (positions.ContainsKey(noeud))
                    {
                        Point pos = positions[noeud];
                        g.FillEllipse(new SolidBrush(couleurActuelle), pos.X - rayon, pos.Y - rayon, 2 * rayon, 2 * rayon);
                        g.DrawString(noeud.NomStation, new Font("Arial", 8), Brushes.Black, pos.X + rayon + 2, pos.Y - 8);
                    }
                }
            }

            // on ajoute une legende
            int legendeX = 20;
            int legendeY = 20;

            // Informations sur le graphe
            string infoGraphe = "Nombre de couleurs : " + NombreCouleurs + "\n" +
                               "Graphe biparti : " + (EstBiparti() ? "Oui" : "Non") + "\n" +
                               "Graphe planaire : " + (EstPlanaire() ? "Oui" : "Non");
            g.DrawString(infoGraphe, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, legendeX, legendeY);
            legendeY += 80;

            // Légende des couleurs
            for (int i = 0; i < groupes.Count; i++)
            {
                g.FillRectangle(new SolidBrush(couleurs[i % couleurs.Length]), legendeX, legendeY, 10, 10);
                g.DrawString("Groupe " + (i + 1), new Font("Arial", 8), Brushes.Black, legendeX + 15, legendeY);
                legendeY += 20;
            }
        }

        /// affiche les resultats de la coloration
        public void AfficherResultats()
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
            AfficherGrapheColore();
        }
    }
}