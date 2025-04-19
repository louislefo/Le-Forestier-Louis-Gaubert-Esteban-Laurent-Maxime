using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public class Graphe<T>
    {
        // Champs privés pour stocker les données
        private Dictionary<T, Noeud<T>> noeuds;
        private List<Lien<T>> liens;
        public string Nom;

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
        /// fait un parcours en largeur du graphe ello
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
        /// cherche un noeud avec le nom donne
        public int TrouverIdParNom(string nomRecherche)
        {
            // on met -1 si on trouve pas
            int idTrouve = -1;

            // on met le nom en minuscule pour comparer
            string nomEnMinuscule = nomRecherche.ToLower();

            // on fait un tableau avec les noeuds pour utiliser for
            Noeud<T>[] tableauNoeuds = new Noeud<T>[Noeuds.Count];
            Noeuds.Values.CopyTo(tableauNoeuds, 0);

            // on regarde tous les noeuds un par un
            for (int i = 0; i < tableauNoeuds.Length; i++)
            {
                // on regarde si le nom du noeud est pareil
                if (tableauNoeuds[i].NomStation.ToLower() == nomEnMinuscule)
                {
                    // on a trouve le noeud on prend son id
                    idTrouve = (int)Convert.ChangeType(tableauNoeuds[i].Id, typeof(int));
                    break;  // on arrete de chercher
                }
            }

            // on dit si on a pas trouve
            if (idTrouve == -1)
            {
                MessageBox.Show("pas trouve la station : " + nomRecherche, "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return idTrouve;
        }


    }
}
