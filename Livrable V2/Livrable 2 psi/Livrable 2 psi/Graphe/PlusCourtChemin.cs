using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les algorithmes de plus court chemin en temps
    /// </summary>
    public class PlusCourtChemin<T> where T : IComparable<T>  // �l�ment de la documentation Csharp qui permet de garder l'�l�ment T 
    {
        /// <summary>
        /// trouve les stations de correspondance (stations sur plusieurs lignes)
        /// </summary>
        private List<Noeud<T>> TrouverStationsCorrespondance(Graphe<T> graphe)
        {
            List<Noeud<T>> stationsCorrespondance = new List<Noeud<T>>();
            Dictionary<string, List<string>> stationsParNom = new Dictionary<string, List<string>>();

            // regroupe les stations par nom
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                if (!stationsParNom.ContainsKey(noeud.NomStation))
                {
                    stationsParNom[noeud.NomStation] = new List<string>();
                }
                stationsParNom[noeud.NomStation].Add(noeud.NumeroLigne);
            }

            // ajoute les stations qui ont plusieurs lignes
            foreach (string nomStation in stationsParNom.Keys)
            {
                if (stationsParNom[nomStation].Count > 1)
                {
                    // trouve le premier noeud de cette station
                    foreach (Noeud<T> noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == nomStation)
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
        /// algorithme de Dijkstra pour trouver le plus court chemin en temps
        /// </summary>
        public List<Noeud<T>> Dijkstra(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // demarre le timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // trouve les stations de correspondance
            List<Noeud<T>> stationsCorrespondance = TrouverStationsCorrespondance(graphe);

            // initialisation des structures de donnees
            Dictionary<Noeud<T>, double> temps = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();
            List<Noeud<T>> nonVisites = new List<Noeud<T>>();

            // initialise les temps
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                temps[noeud] = double.MaxValue;
                nonVisites.Add(noeud);
            }
            temps[depart] = 0;

            // boucle principale
            while (nonVisites.Count > 0)
            {
                // trouve le noeud non visite avec le plus petit temps
                Noeud<T> noeudActuel = null;
                double tempsMin = double.MaxValue;

                foreach (Noeud<T> noeud in nonVisites)
                {
                    if (temps[noeud] < tempsMin)
                    {
                        tempsMin = temps[noeud];
                        noeudActuel = noeud;
                    }
                }

                if (noeudActuel == null) break;

                // si on a atteint le noeud d'arrivee, on peut s'arreter
                if (noeudActuel.NomStation == arrivee.NomStation) break;

                nonVisites.Remove(noeudActuel);

                // met a jour les temps des voisins
                foreach (Lien<T> lien in graphe.Liens)
                {
                    // verifie dans les deux sens car le graphe est non oriente
                    if (lien.Noeud1 == noeudActuel && nonVisites.Contains(lien.Noeud2))
                    {
                        double nouveauTemps = temps[noeudActuel] + lien.Poids;
                        if (nouveauTemps < temps[lien.Noeud2])
                        {
                            temps[lien.Noeud2] = nouveauTemps;
                            predecesseurs[lien.Noeud2] = noeudActuel;
                        }
                    }
                    if (lien.Noeud2 == noeudActuel && nonVisites.Contains(lien.Noeud1))
                    {
                        double nouveauTemps = temps[noeudActuel] + lien.Poids;
                        if (nouveauTemps < temps[lien.Noeud1])
                        {
                            temps[lien.Noeud1] = nouveauTemps;
                            predecesseurs[lien.Noeud1] = noeudActuel;
                        }
                    }
                }

                // si on est sur une station de correspondance, on peut changer de ligne
                if (stationsCorrespondance.Exists(s => s.NomStation == noeudActuel.NomStation))
                {
                    foreach (Noeud<T> noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == noeudActuel.NomStation && noeud.NumeroLigne != noeudActuel.NumeroLigne)
                        {
                            double nouveauTemps = temps[noeudActuel] + noeudActuel.TempsCorrespondance;
                            if (nouveauTemps < temps[noeud])
                            {
                                temps[noeud] = nouveauTemps;
                                predecesseurs[noeud] = noeudActuel;
                            }
                        }
                    }
                }
            }

            // reconstruit le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = null;

            // trouve le noeud d'arrivee avec le plus petit temps
            double tempsMinArrivee = double.MaxValue;
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                if (noeud.NomStation == arrivee.NomStation && temps[noeud] < tempsMinArrivee)
                {
                    tempsMinArrivee = temps[noeud];
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

            // arrete le timer et affiche le temps
            timer.Stop();
            Console.WriteLine("Temps pour trouver le plus court chemin via Dijkstra : "+timer.ElapsedMilliseconds+" ms");

            return chemin;
        }

        /// <summary>
        /// algorithme de Bellman-Ford pour trouver le plus court chemin en temps
        /// </summary>
        public List<Noeud<T>> BellmanFord(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // demarre le timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // trouve les stations de correspondance
            List<Noeud<T>> stationsCorrespondance = TrouverStationsCorrespondance(graphe);

            // initialisation des structures de donnees
            Dictionary<Noeud<T>, double> temps = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();

            // initialise les temps
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                temps[noeud] = double.MaxValue;
            }
            temps[depart] = 0;

            // boucle principale
            for (int i = 0; i < graphe.Noeuds.Count - 1; i++)
            {
                foreach (Lien<T> lien in graphe.Liens)
                {
                    // verifie dans les deux sens car le graphe est non oriente
                    if (temps[lien.Noeud1] != double.MaxValue && 
                        temps[lien.Noeud1] + lien.Poids < temps[lien.Noeud2])
                    {
                        temps[lien.Noeud2] = temps[lien.Noeud1] + lien.Poids;
                        predecesseurs[lien.Noeud2] = lien.Noeud1;
                    }

                    if (temps[lien.Noeud2] != double.MaxValue && 
                        temps[lien.Noeud2] + lien.Poids < temps[lien.Noeud1])
                    {
                        temps[lien.Noeud1] = temps[lien.Noeud2] + lien.Poids;
                        predecesseurs[lien.Noeud1] = lien.Noeud2;
                    }
                }

                // ajoute les changements de ligne aux stations de correspondance
                foreach (Noeud<T> station in stationsCorrespondance)
                {
                    foreach (Noeud<T> noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == station.NomStation && noeud.NumeroLigne != station.NumeroLigne)
                        {
                            if (temps[station] != double.MaxValue && 
                                temps[station] + station.TempsCorrespondance < temps[noeud])
                            {
                                temps[noeud] = temps[station] + station.TempsCorrespondance;
                                predecesseurs[noeud] = station;
                            }
                        }
                    }
                }
            }

            // reconstruit le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = null;

            // trouve le noeud d'arrivee avec le plus petit temps
            double tempsMinArrivee = double.MaxValue;
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                if (noeud.NomStation == arrivee.NomStation && temps[noeud] < tempsMinArrivee)
                {
                    tempsMinArrivee = temps[noeud];
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

            // arrete le timer et affiche le temps
            timer.Stop();
            Console.WriteLine("Temps pour trouver le plus court chemin via Bellman-Ford : " +timer.ElapsedMilliseconds +" ms");

            return chemin;
        }

        /// <summary>
        /// algorithme de Floyd-Warshall pour trouver les plus courts chemins entre tous les sommets
        /// </summary>
        public Dictionary<(Noeud<T>, Noeud<T>), double> FloydWarshall(Graphe<T> graphe)
        {
            // demarre le timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // cree une liste des noeuds pour acceder facilement aux indices
            List<Noeud<T>> noeuds = new List<Noeud<T>>(graphe.Noeuds.Values);
            int n = noeuds.Count;

            // initialise la matrice des distances
            double[,] distances = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else
                    {
                        distances[i, j] = int.MaxValue;
                    }
                }
            }

            // remplit la matrice avec les poids des liens existants
            foreach (Lien<T> lien in graphe.Liens)
            {
                int i = noeuds.IndexOf(lien.Noeud1);
                int j = noeuds.IndexOf(lien.Noeud2);
                distances[i, j] = lien.Poids;
                distances[j, i] = lien.Poids; // car le graphe est non oriente
            }

            // applique l'algorithme de Floyd-Warshall
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (distances[i, k] != int.MaxValue && distances[k, j] != int.MaxValue)
                        {
                            double nouvelleDistance = distances[i, k] + distances[k, j];
                            if (nouvelleDistance < distances[i, j])
                            {
                                distances[i, j] = nouvelleDistance;
                            }
                        }
                    }
                }
            }

            // convertit la matrice en dictionnaire pour un acces plus facile
            Dictionary<(Noeud<T>, Noeud<T>), double> resultat = new Dictionary<(Noeud<T>, Noeud<T>), double>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (distances[i, j] != int.MaxValue)
                    {
                        resultat[(noeuds[i], noeuds[j])] = distances[i, j];
                    }
                }
            }

            // arrete le timer et affiche le temps
            timer.Stop();
            Console.WriteLine("Temps pour trouver le plus court chemin via Floyd-Warshall : "+timer.ElapsedMilliseconds+" ms");

            return resultat;
        }
    }
} 