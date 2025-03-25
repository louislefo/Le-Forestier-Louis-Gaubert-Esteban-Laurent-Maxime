using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class Graphe<T>
    {
        // Champs privés pour stocker les données
        private Dictionary<T, Noeud<T>> noeuds;
        private List<Lien<T>> liens;

        /// <summary>
        /// recupere ou modifie la liste des noeuds du graphe
        /// </summary>
        public Dictionary<T, Noeud<T>> Noeuds
        {
            get { return noeuds; }
            set { noeuds = value; }
        }

        /// <summary>
        /// recupere ou modifie la liste des liens du graphe
        /// </summary>
        public List<Lien<T>> Liens
        {
            get { return liens; }
            set { liens = value; }
        }

        /// <summary>
        /// cree un nouveau graphe vide
        /// </summary>
        public Graphe()
        {
            Noeuds = new Dictionary<T, Noeud<T>>();
            Liens = new List<Lien<T>>();
        }

        /// <summary>
        /// ajoute un lien entre deux noeuds
        /// </summary>
        public void AjouterLien(T id1, T id2, double poids)
        {
            if (!Noeuds.ContainsKey(id1))
                Noeuds[id1] = new Noeud<T>(id1);

            if (!Noeuds.ContainsKey(id2))
                Noeuds[id2] = new Noeud<T>(id2);

            Noeud<T> n1 = Noeuds[id1];
            Noeud<T> n2 = Noeuds[id2];

            if (!n1.Voisins.Contains(n2))
            {
                n1.AjouterVoisin(n2);
                Liens.Add(new Lien<T>(n1, n2, poids));
            }
        }

        /// <summary>
        /// donne le premier noeud du graphe
        /// </summary>
        public Noeud<T> ObtenirPremierNoeud()
        {
            foreach (var noeud in Noeuds.Values)
            {
                return noeud;
            }
            return null;
        }

        /// <summary>
        /// fait un parcours en largeur du graphe
        /// </summary>
        public void ParcoursLargeur(Noeud<T> depart)
        {
            List<T> visites = new List<T>();
            Queue<Noeud<T>> file = new Queue<Noeud<T>>();

            file.Enqueue(depart);
            visites.Add(depart.Id);
            Console.Write(depart.Id + " ");

            while (file.Count > 0)
            {
                Noeud<T> actuel = file.Dequeue();

                for (int i = 0; i < actuel.Voisins.Count; i++)
                {
                    Noeud<T> voisin = actuel.Voisins[i];
                    bool estDejaVisite = false;

                    for (int j = 0; j < visites.Count; j++)
                    {
                        if (visites[j].Equals(voisin.Id))
                        {
                            estDejaVisite = true;
                            break;
                        }
                    }

                    if (!estDejaVisite)
                    {
                        visites.Add(voisin.Id);
                        file.Enqueue(voisin);
                        Console.Write(voisin.Id + " ");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
