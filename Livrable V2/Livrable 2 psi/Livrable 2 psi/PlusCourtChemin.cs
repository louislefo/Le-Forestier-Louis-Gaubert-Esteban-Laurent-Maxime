using System;
using System.Collections.Generic;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les algorithmes de plus court chemin
    /// </summary>
    public class PlusCourtChemin<T>
    {
        /// <summary>
        /// trouve le plus court chemin avec l'algorithme de Dijkstra
        /// </summary>
        public List<Noeud<T>> Dijkstra(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // initialisation des structures de données
            Dictionary<Noeud<T>, double> distances = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();
            List<Noeud<T>> noeudsNonVisites = new List<Noeud<T>>();

            // initialisation des distances
            foreach (var noeud in graphe.Noeuds.Values)
            {
                distances[noeud] = double.MaxValue;
                noeudsNonVisites.Add(noeud);
            }
            distances[depart] = 0;

            // boucle principale
            while (noeudsNonVisites.Count > 0)
            {
                // trouver le noeud avec la plus petite distance
                Noeud<T> noeudActuel = null;
                double distanceMin = double.MaxValue;

                for (int i = 0; i < noeudsNonVisites.Count; i++)
                {
                    if (distances[noeudsNonVisites[i]] < distanceMin)
                    {
                        distanceMin = distances[noeudsNonVisites[i]];
                        noeudActuel = noeudsNonVisites[i];
                    }
                }

                if (noeudActuel == null) break;

                // marquer le noeud comme visite
                noeudsNonVisites.Remove(noeudActuel);

                // si on a atteint l'arrivée, on peut s'arrêter
                if (noeudActuel == arrivee) break;

                // mettre à jour les distances des voisins
                for (int i = 0; i < noeudActuel.Voisins.Count; i++)
                {
                    Noeud<T> voisin = noeudActuel.Voisins[i];
                    if (!noeudsNonVisites.Contains(voisin)) continue;

                    // trouver le poids du lien entre noeudActuel et voisin
                    double poidsLien = 1.0; // par défaut
                    foreach (var lien in graphe.Liens)
                    {
                        if ((lien.Noeud1 == noeudActuel && lien.Noeud2 == voisin) ||
                            (lien.Noeud2 == noeudActuel && lien.Noeud1 == voisin))
                        {
                            poidsLien = lien.Poids;
                            break;
                        }
                    }

                    double nouvelleDistance = distances[noeudActuel] + poidsLien;
                    if (nouvelleDistance < distances[voisin])
                    {
                        distances[voisin] = nouvelleDistance;
                        predecesseurs[voisin] = noeudActuel;
                    }
                }
            }

            // reconstruire le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = arrivee;

            while (noeudCourant != null)
            {
                chemin.Insert(0, noeudCourant);
                if (predecesseurs.ContainsKey(noeudCourant))
                    noeudCourant = predecesseurs[noeudCourant];
                else
                    break;
            }

            return chemin;
        }

        /// <summary>
        /// trouve le plus court chemin avec l'algorithme de Bellman-Ford
        /// </summary>
        public List<Noeud<T>> BellmanFord(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // initialisation des structures de données
            Dictionary<Noeud<T>, double> distances = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();

            // initialisation des distances
            foreach (var noeud in graphe.Noeuds.Values)
            {
                distances[noeud] = double.MaxValue;
            }
            distances[depart] = 0;

            // boucle principale
            for (int i = 0; i < graphe.Noeuds.Count - 1; i++)
            {
                for (int j = 0; j < graphe.Liens.Count; j++)
                {
                    var lien = graphe.Liens[j];
                    Noeud<T> u = lien.Noeud1;
                    Noeud<T> v = lien.Noeud2;

                    if (distances[u] != double.MaxValue && distances[u] + lien.Poids < distances[v])
                    {
                        distances[v] = distances[u] + lien.Poids;
                        predecesseurs[v] = u;
                    }

                    // vérifier aussi dans l'autre sens car le graphe est non orienté
                    if (distances[v] != double.MaxValue && distances[v] + lien.Poids < distances[u])
                    {
                        distances[u] = distances[v] + lien.Poids;
                        predecesseurs[u] = v;
                    }
                }
            }

            // reconstruire le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = arrivee;

            while (noeudCourant != null)
            {
                chemin.Insert(0, noeudCourant);
                if (predecesseurs.ContainsKey(noeudCourant))
                    noeudCourant = predecesseurs[noeudCourant];
                else
                    break;
            }

            return chemin;
        }
    }
} 