using System;
using System.Collections.Generic;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les algorithmes de plus court chemin
    /// </summary>
    public class PlusCourtChemin<T> where T : IComparable<T>
    {
        /// <summary>
        /// trouve les stations de correspondance (stations sur plusieurs lignes)
        /// </summary>
        private List<Noeud<T>> TrouverStationsCorrespondance(Graphe<T> graphe)
        {
            List<Noeud<T>> stationsCorrespondance = new List<Noeud<T>>();
            Dictionary<string, HashSet<string>> stationsParNom = new Dictionary<string, HashSet<string>>();

            // regroupe les stations par nom
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (!stationsParNom.ContainsKey(noeud.NomStation))
                {
                    stationsParNom[noeud.NomStation] = new HashSet<string>();
                }
                stationsParNom[noeud.NomStation].Add(noeud.NumeroLigne);
            }

            // ajoute les stations qui ont plusieurs lignes
            foreach (var station in stationsParNom)
            {
                if (station.Value.Count > 1)
                {
                    // trouve le premier noeud de cette station
                    foreach (var noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == station.Key)
                        {
                            stationsCorrespondance.Add(noeud);
                            break;
                        }
                    }
                }
            }

            return stationsCorrespondance;
        }

        /// <summary>
        /// algorithme de Dijkstra pour trouver le plus court chemin
        /// </summary>
        public List<Noeud<T>> Dijkstra(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // trouve les stations de correspondance
            List<Noeud<T>> stationsCorrespondance = TrouverStationsCorrespondance(graphe);

            // initialisation des structures de donnees
            Dictionary<Noeud<T>, double> distances = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();
            HashSet<Noeud<T>> nonVisites = new HashSet<Noeud<T>>();

            // initialise les distances
            foreach (var noeud in graphe.Noeuds.Values)
            {
                distances[noeud] = double.MaxValue;
                nonVisites.Add(noeud);
            }
            distances[depart] = 0;

            // boucle principale
            while (nonVisites.Count > 0)
            {
                // trouve le noeud non visite avec la plus petite distance
                Noeud<T> noeudActuel = null;
                double distanceMin = double.MaxValue;

                foreach (var noeud in nonVisites)
                {
                    if (distances[noeud] < distanceMin)
                    {
                        distanceMin = distances[noeud];
                        noeudActuel = noeud;
                    }
                }

                if (noeudActuel == null) break;

                // si on a atteint le noeud d'arrivee, on peut s'arreter
                if (noeudActuel.NomStation == arrivee.NomStation) break;

                nonVisites.Remove(noeudActuel);

                // met a jour les distances des voisins
                foreach (var lien in graphe.Liens)
                {
                    // verifie dans les deux sens car le graphe est non oriente
                    if (lien.Noeud1 == noeudActuel && nonVisites.Contains(lien.Noeud2))
                    {
                        double nouvelleDistance = distances[noeudActuel] + lien.Poids;
                        if (nouvelleDistance < distances[lien.Noeud2])
                        {
                            distances[lien.Noeud2] = nouvelleDistance;
                            predecesseurs[lien.Noeud2] = noeudActuel;
                        }
                    }
                    if (lien.Noeud2 == noeudActuel && nonVisites.Contains(lien.Noeud1))
                    {
                        double nouvelleDistance = distances[noeudActuel] + lien.Poids;
                        if (nouvelleDistance < distances[lien.Noeud1])
                        {
                            distances[lien.Noeud1] = nouvelleDistance;
                            predecesseurs[lien.Noeud1] = noeudActuel;
                        }
                    }
                }

                // si on est sur une station de correspondance, on peut changer de ligne
                if (stationsCorrespondance.Exists(s => s.NomStation == noeudActuel.NomStation))
                {
                    foreach (var noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == noeudActuel.NomStation && noeud.NumeroLigne != noeudActuel.NumeroLigne)
                        {
                            double nouvelleDistance = distances[noeudActuel] + 1; // cout de 1 pour changer de ligne
                            if (nouvelleDistance < distances[noeud])
                            {
                                distances[noeud] = nouvelleDistance;
                                predecesseurs[noeud] = noeudActuel;
                            }
                        }
                    }
                }
            }

            // reconstruit le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = null;

            // trouve le noeud d'arrivee avec le plus petit poids
            double distanceMinArrivee = double.MaxValue;
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (noeud.NomStation == arrivee.NomStation && distances[noeud] < distanceMinArrivee)
                {
                    distanceMinArrivee = distances[noeud];
                    noeudCourant = noeud;
                }
            }

            if (noeudCourant == null)
            {
                return new List<Noeud<T>>(); // retourne une liste vide si pas de chemin
            }

            // reconstruit le chemin de l'arrivee vers le depart
            while (noeudCourant != null)
            {
                chemin.Insert(0, noeudCourant);
                if (predecesseurs.ContainsKey(noeudCourant))
                {
                    noeudCourant = predecesseurs[noeudCourant];
                }
                else
                {
                    break;
                }
            }

            // verifie si le chemin commence bien par le noeud de depart
            if (chemin.Count == 0 || chemin[0].NomStation != depart.NomStation)
            {
                return new List<Noeud<T>>(); // retourne une liste vide si le chemin n'est pas valide
            }

            return chemin;
        }

        /// <summary>
        /// algorithme de Bellman-Ford pour trouver le plus court chemin
        /// </summary>
        public List<Noeud<T>> BellmanFord(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // trouve les stations de correspondance
            List<Noeud<T>> stationsCorrespondance = TrouverStationsCorrespondance(graphe);

            // initialisation des structures de donnees
            Dictionary<Noeud<T>, double> distances = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();

            // initialise les distances
            foreach (var noeud in graphe.Noeuds.Values)
            {
                distances[noeud] = double.MaxValue;
            }
            distances[depart] = 0;

            // boucle principale
            for (int i = 0; i < graphe.Noeuds.Count - 1; i++)
            {
                foreach (var lien in graphe.Liens)
                {
                    // verifie dans les deux sens car le graphe est non oriente
                    if (distances[lien.Noeud1] != double.MaxValue && 
                        distances[lien.Noeud1] + lien.Poids < distances[lien.Noeud2])
                    {
                        distances[lien.Noeud2] = distances[lien.Noeud1] + lien.Poids;
                        predecesseurs[lien.Noeud2] = lien.Noeud1;
                    }

                    if (distances[lien.Noeud2] != double.MaxValue && 
                        distances[lien.Noeud2] + lien.Poids < distances[lien.Noeud1])
                    {
                        distances[lien.Noeud1] = distances[lien.Noeud2] + lien.Poids;
                        predecesseurs[lien.Noeud1] = lien.Noeud2;
                    }
                }

                // ajoute les changements de ligne aux stations de correspondance
                foreach (var station in stationsCorrespondance)
                {
                    foreach (var noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == station.NomStation && noeud.NumeroLigne != station.NumeroLigne)
                        {
                            if (distances[station] != double.MaxValue && 
                                distances[station] + 1 < distances[noeud])
                            {
                                distances[noeud] = distances[station] + 1;
                                predecesseurs[noeud] = station;
                            }
                        }
                    }
                }
            }

            // reconstruit le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = null;

            // trouve le noeud d'arrivee avec le plus petit poids
            double distanceMinArrivee = double.MaxValue;
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (noeud.NomStation == arrivee.NomStation && distances[noeud] < distanceMinArrivee)
                {
                    distanceMinArrivee = distances[noeud];
                    noeudCourant = noeud;
                }
            }

            if (noeudCourant == null)
            {
                return new List<Noeud<T>>(); // retourne une liste vide si pas de chemin
            }

            // reconstruit le chemin de l'arrivee vers le depart
            while (noeudCourant != null)
            {
                chemin.Insert(0, noeudCourant);
                if (predecesseurs.ContainsKey(noeudCourant))
                {
                    noeudCourant = predecesseurs[noeudCourant];
                }
                else
                {
                    break;
                }
            }

            // verifie si le chemin commence bien par le noeud de depart
            if (chemin.Count == 0 || chemin[0].NomStation != depart.NomStation)
            {
                return new List<Noeud<T>>(); // retourne une liste vide si le chemin n'est pas valide
            }

            return chemin;
        }
    }
} 