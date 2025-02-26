using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace PSI_livrable1
{
    /// <summary>
    /// Classe principale du programme qui gère la lecture d'un fichier de graphe et effectue différentes opérations sur celui-ci.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Point d'entrée principal du programme.
        /// Lit un fichier de graphe au format MTX, construit le graphe correspondant et effectue diverses analyses.
        /// </summary>
        static void Main()
        {
            /*string cheminFichier = @".\.\soc-karate.mtx";

            Graphe monGraphe = new Graphe();

            /// Lecture du fichier et ajout des liens au graphe
            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    while (!sr.EndOfStream)
                    {
                        string ligne = sr.ReadLine();

                        /// Ignorer les commentaires et lignes vides
                        if (string.IsNullOrWhiteSpace(ligne) || ligne.StartsWith("%"))
                            continue;

                        /// Lecture des liens entre les nœuds
                        string[] elements = ligne.Split(' ');
                        if (elements.Length >= 2 && int.TryParse(elements[0], out int id1) && int.TryParse(elements[1], out int id2))
                        {
                            monGraphe.AjouterLien(id1, id2);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier : " + e.Message);
                return;
            }

            /// Vérification du chargement du graphe
            Console.WriteLine("Nombre de nœuds : " + monGraphe.Noeuds.Count);
            Console.WriteLine("Nombre de liens : " + monGraphe.Liens.Count);

            foreach (var noeud in monGraphe.Noeuds.Values)
            {
                Console.Write("Noeud " + noeud.Id + " -> ");
                foreach (var voisin in noeud.Voisins)
                    Console.Write(voisin.Id + " ");
                Console.WriteLine();
            }

            /// Vérification du premier nœud
            Noeud premierNoeud = monGraphe.ObtenirPremierNoeud();
            if (premierNoeud == null)
            {
                Console.WriteLine("Erreur : plus de noeuds");
                //return;
            }

            /// Test du parcours en largeur (largeur)
            Console.WriteLine("\n Parcours largeur :");
            monGraphe.largeur(premierNoeud);

            /// Test du parcours en profondeur (Profondeur)
            Console.WriteLine("\n Parcours Profondeur :");
            HashSet<int> visiteDFS = new HashSet<int>();
            monGraphe.Profondeur(premierNoeud, visiteDFS);
            Console.WriteLine();

            /// Vérification de la connexité
            Console.WriteLine("\n Le graphe est connexe ? " + monGraphe.EstConnexe());

            /// Vérification des cycles
            Console.WriteLine(" Le graphe contient un cycle ? " + monGraphe.ContientCycle());
        */

            /// MAIN TEMPORAIRE POUR TESTER VISUALISATION.CS
            string filePath = @".\.\soc-karate.txt";
            List<(int, int)> edges = new List<(int, int)>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                Regex regex = new Regex(@"\((\d+),\s*(\d+)\)"); // Capture les paires (x, y)

                foreach (string line in lines)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        int node1 = int.Parse(match.Groups[1].Value);
                        int node2 = int.Parse(match.Groups[2].Value);
                        edges.Add((node1, node2));
                    }
                }

                if (edges.Count > 0)
                {
                    Visualisation graph = new Visualisation(edges);
                    graph.DrawGraph("graph.png");
                }
                else
                {
                    Console.WriteLine("Aucune arête valide trouvée dans le fichier.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier : " + ex.Message);
            }
        }
    }
}

