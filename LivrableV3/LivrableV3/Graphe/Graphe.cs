using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    /// <summary>
    /// cette classe represente un graphe qui peut contenir n'importe quel type de donnees
    /// elle stocke les stations et les connexions entre elles
    /// elle permet de faire des operations comme ajouter des connexions ou chercher des chemins
    /// elle est utilisee pour representer le metro de paris
    /// </summary>
    public class Graphe<T>
    {
        // Champs privés pour stocker les données
        private Dictionary<T, Noeud<T>> noeuds;
        private List<Lien<T>> liens;
        public string Nom;

        /// <summary>
        /// recupere ou modifie la liste des stations du metro
        /// chaque station est stockee avec son numero comme cle
        /// les stations contiennent les informations comme le nom et la position
        /// </summary>
        public Dictionary<T, Noeud<T>> Noeuds
        {
            get { return noeuds; }
            set { noeuds = value; }
        }

        /// <summary>
        /// recupere ou modifie la liste des connexions entre les stations
        /// chaque connexion represente un trajet possible entre deux stations
        /// les connexions contiennent le temps de trajet entre les stations
        /// </summary>
        public List<Lien<T>> Liens
        {
            get { return liens; }
            set { liens = value; }
        }

        /// <summary>
        /// cree un nouveau graphe vide pour le metro
        /// initialise les listes pour stocker les stations et les connexions
        /// le graphe est vide au debut et sera rempli avec les donnees du metro
        /// </summary>
        public Graphe()
        {
            Noeuds = new Dictionary<T, Noeud<T>>();
            Liens = new List<Lien<T>>();
        }

        /// <summary>
        /// ajoute une connexion entre deux stations avec un temps de trajet
        /// cree les stations si elles n'existent pas encore
        /// ajoute la connexion dans les deux sens car on peut aller dans les deux sens
        /// le temps de trajet est en minutes
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
        /// donne la premiere station du metro
        /// utile pour commencer a chercher un chemin
        /// retourne rien si le metro est vide
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
        /// fait un parcours en largeur du metro a partir d'une station
        /// visite toutes les stations accessibles en passant par les connexions
        /// affiche les stations dans l'ordre ou on les visite
        /// utile pour voir toutes les stations qu'on peut atteindre
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

        /// <summary>
        /// cherche une station dans le metro a partir de son nom
        /// retourne le numero de la station trouvee ou -1 si pas trouvee
        /// la recherche ne fait pas la difference entre majuscules et minuscules
        /// </summary>
        public int TrouverIdParNom(string nomRecherche)
        {
            // on met -1 si on trouve pas
            int idTrouve = -1;

            // on met le nom en minuscule pour comparer
            string nomEnMinuscule = nomRecherche.ToLower();

            // on cherche dans tous les noeuds
            foreach (var noeud in Noeuds.Values)
            {
                if (noeud.NomStation.ToLower() == nomEnMinuscule)
                {
                    idTrouve = Convert.ToInt32(noeud.Id);
                    break;
                }
            }

            return idTrouve;
        }
    }
}
