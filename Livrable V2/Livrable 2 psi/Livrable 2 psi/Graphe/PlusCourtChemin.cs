using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Livrable_2_psi
{
    /// <summary>
    /// cette classe sert a trouver le plus court chemin dans le metro
    /// elle utilise plusieurs algorithmes comme Dijkstra et Bellman-Ford
    /// c'est utile pour trouver le meilleur trajet pour les livraisons
    /// </summary>
    public class PlusCourtChemin<T>  // where T : IComparable<T>  // element de la documentation Csharp qui permet de garder l'element T 
    {
        /// <summary>
        /// cette methode sert a trouver les stations ou on peut changer de ligne
        /// elle regarde toutes les stations et trouve celles qui sont sur plusieurs lignes
        /// </summary>
        private List<Noeud<T>> TrouverStationsCorrespondance(Graphe<T> graphe)
        {
            List<Noeud<T>> stationsCorrespondance = new List<Noeud<T>>();
            Dictionary<string, List<string>> stationsParNom = new Dictionary<string, List<string>>();

            // on met toutes les stations dans un dictionnaire par nom
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                if (!stationsParNom.ContainsKey(noeud.NomStation))
                {
                    stationsParNom[noeud.NomStation] = new List<string>();
                }
                stationsParNom[noeud.NomStation].Add(noeud.NumeroLigne);
            }

            // on cherche les stations qui sont sur plusieurs lignes
            foreach (string nomStation in stationsParNom.Keys)
            {
                if (stationsParNom[nomStation].Count > 1)
                {
                    // on prend la premiere station de ce nom qu'on trouve
                    foreach (Noeud<T> noeud in graphe.Noeuds.Values)
                    {
                        if (noeud.NomStation == nomStation)
                        {
                            stationsCorrespondance.Add(noeud);
                            break;  // on sort de la boucle car on a trouve la station
                        }
                    }
                }
            }

            return stationsCorrespondance;
        }

        /// <summary>
        /// cette methode sert a trouver le plus court chemin avec Dijkstra
        /// elle commence par la station de depart et cherche le chemin le plus rapide
        /// elle prend en compte le temps de correspondance quand on change de ligne
        /// </summary>
        public List<Noeud<T>> Dijkstra(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // on demarre le chrono pour voir combien de temps ca prend
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // on trouve les stations ou on peut changer de ligne
            List<Noeud<T>> stationsCorrespondance = TrouverStationsCorrespondance(graphe);

            // on cree des dictionnaires pour stocker les temps et les stations precedentes
            Dictionary<Noeud<T>, double> temps = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();
            List<Noeud<T>> nonVisites = new List<Noeud<T>>();

            // on met un temps infini pour toutes les stations au debut
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                temps[noeud] = double.MaxValue;  // on met un temps infini pour commencer
                nonVisites.Add(noeud);
            }
            temps[depart] = 0;  // le depart est a 0

            // on cherche le chemin le plus court
            while (nonVisites.Count > 0)
            {
                // on cherche la station non visitee avec le plus petit temps
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

                // si on est arrive a la station d'arrivee, on peut s'arreter
                if (noeudActuel.NomStation == arrivee.NomStation) break;

                nonVisites.Remove(noeudActuel);

                // on met a jour les temps des stations voisines
                foreach (Lien<T> lien in graphe.Liens)
                {
                    // on regarde dans les deux sens car le metro va dans les deux sens
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

            // on reconstruit le chemin a partir de l'arrivee
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = null;

            // on cherche la station d'arrivee avec le plus petit temps
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
                return new List<Noeud<T>>(); // on retourne une liste vide si pas de chemin
            }

            // on remonte le chemin jusqu'au depart
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

            // on verifie que le chemin commence bien par la station de depart
            if (chemin.Count == 0 || chemin[0].NomStation != depart.NomStation)
            {
                return new List<Noeud<T>>(); // on retourne une liste vide si le chemin est pas bon
            }

            // on arrete le chrono et on affiche le temps
            timer.Stop();
            Console.WriteLine("Temps pour trouver le plus court chemin via Dijkstra : "+timer.ElapsedMilliseconds+" ms");

            return chemin;
        }

        /// <summary>
        /// cette methode sert a trouver le plus court chemin avec Bellman-Ford
        /// elle fait pareil que Dijkstra mais elle peut gerer les poids negatifs
        /// elle est plus lente mais plus sure
        /// </summary>
        public List<Noeud<T>> BellmanFord(Graphe<T> graphe, Noeud<T> depart, Noeud<T> arrivee)
        {
            // on demarre le chrono
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // on trouve les stations de correspondance
            List<Noeud<T>> stationsCorrespondance = TrouverStationsCorrespondance(graphe);

            // on cree des dictionnaires pour les temps et les stations precedentes
            Dictionary<Noeud<T>, double> temps = new Dictionary<Noeud<T>, double>();
            Dictionary<Noeud<T>, Noeud<T>> predecesseurs = new Dictionary<Noeud<T>, Noeud<T>>();

            // on met un temps infini pour toutes les stations
            foreach (Noeud<T> noeud in graphe.Noeuds.Values)
            {
                temps[noeud] = double.MaxValue;  // on met un temps infini pour commencer
            }
            temps[depart] = 0;  // le depart est a 0

            // on fait n-1 iterations pour etre sur d'avoir le plus court chemin
            for (int i = 0; i < graphe.Noeuds.Count - 1; i++)
            {
                foreach (Lien<T> lien in graphe.Liens)
                {
                    // on regarde dans les deux sens
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

                // on ajoute les changements de ligne aux stations de correspondance
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

            // on reconstruit le chemin
            List<Noeud<T>> chemin = new List<Noeud<T>>();
            Noeud<T> noeudCourant = null;

            // on cherche la station d'arrivee avec le plus petit temps
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
                return new List<Noeud<T>>(); // on retourne une liste vide si pas de chemin
            }

            // on remonte le chemin jusqu'au depart
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

            // on verifie que le chemin commence bien par la station de depart
            if (chemin.Count == 0 || chemin[0].NomStation != depart.NomStation)
            {
                return new List<Noeud<T>>(); // on retourne une liste vide si le chemin est pas bon
            }

            // on arrete le chrono et on affiche le temps
            timer.Stop();
            Console.WriteLine("Temps pour trouver le plus court chemin via Bellman-Ford : " +timer.ElapsedMilliseconds +" ms");

            return chemin;
        }

        /// <summary>
        /// cette methode sert a trouver tous les plus courts chemins entre toutes les stations
        /// elle utilise l'algorithme de Floyd-Warshall qui est plus lent mais plus complet
        /// c'est utile pour avoir tous les chemins d'un coup
        /// </summary>
        public Dictionary<(Noeud<T>, Noeud<T>), double> FloydWarshall(Graphe<T> graphe)
        {
            // on demarre le chrono
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // on cree une liste des stations pour acceder facilement aux indices
            List<Noeud<T>> noeuds = new List<Noeud<T>>(graphe.Noeuds.Values);
            int n = noeuds.Count;

            // on cree une matrice pour stocker les distances
            double[,] distances = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;  // la distance a soi-meme est 0
                    }
                    else
                    {
                        distances[i, j] = int.MaxValue;  // on met une distance infinie pour commencer
                    }
                }
            }

            // on remplit la matrice avec les temps entre les stations
            foreach (Lien<T> lien in graphe.Liens)
            {
                int i = noeuds.IndexOf(lien.Noeud1);
                int j = noeuds.IndexOf(lien.Noeud2);
                distances[i, j] = lien.Poids;
                distances[j, i] = lien.Poids; // le metro va dans les deux sens
            }

            // on applique l'algorithme de Floyd-Warshall
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

            // on convertit la matrice en dictionnaire pour plus facile a utiliser
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

            // on arrete le chrono et on affiche le temps
            timer.Stop();
            Console.WriteLine("Temps pour trouver le plus court chemin via Floyd-Warshall : "+timer.ElapsedMilliseconds+" ms");

            return resultat;
        }
    }
} 