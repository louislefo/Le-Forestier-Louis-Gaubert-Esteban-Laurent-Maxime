using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe principale qui gere le programme
    /// </summary>
    class Program
    {
        /// <summary>
        /// point de depart du programme
        /// </summary>
        static void Main(string[] args)
        {
            // PARTIE METRO
            Graphe<int> grapheMetro = new Graphe<int>();
            string cheminFichierMetro = @"../../../MetroParisNoeuds.csv";
            string cheminFichierArcs = @"../../../MetroParisArcs.csv";

            // charge les fichiers
            ChargerFichiers chargeur = new ChargerFichiers();
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);
            Console.WriteLine($"Nombre de noeuds charges : {noeudsMetro.Count}");
            
            // ajoute les noeuds au graphe
            foreach (var noeud in noeudsMetro)
            {
                grapheMetro.Noeuds[noeud.Key] = noeud.Value;
            }
            Console.WriteLine($"Nombre de noeuds dans le graphe : {grapheMetro.Noeuds.Count}");

            // charge les arcs  
            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);
            Console.WriteLine($"Nombre de liens dans le graphe : {grapheMetro.Liens.Count}");

            // affiche quelques infos sur les noeuds pour debug
            foreach (var noeud in grapheMetro.Noeuds.Values)
            {
                Console.WriteLine($"Station: {noeud.NomStation}, Ligne: {noeud.NumeroLigne}, Couleur: {noeud.CouleurLigne}");
                Console.WriteLine($"Position: ({noeud.Longitude}, {noeud.Latitude})");
                break; // on affiche juste le premier noeud pour tester
            }

            // crée la visualisation du métro
            Visualisation visMetro = new Visualisation(1200, 800);
            visMetro.DessinerGraphe(grapheMetro);
            visMetro.SauvegarderImage("metro.png");
            Console.WriteLine("\nCarte du métro sauvegardée sous le nom de metro.png");

            // ouvre le fichier metro.png
            try
            {
                Process.Start(new ProcessStartInfo("metro.png") { UseShellExecute = true });
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'ouverture du fichier : " + e.Message);
            }

            // PARTIE PLUS COURT CHEMIN
            Console.WriteLine("\nTest des algorithmes de plus court chemin :");
            
            // on prend deux stations pour tester
            Noeud<int> stationDepart = null;
            Noeud<int> stationArrivee = null;
            
            foreach (var noeud in grapheMetro.Noeuds.Values)
            {
                if (stationDepart == null)
                    stationDepart = noeud;
                else if (stationArrivee == null)
                {
                    stationArrivee = noeud;
                    break;
                }
            }

            if (stationDepart != null && stationArrivee != null)
            {
                Console.WriteLine($"\nRecherche du plus court chemin entre {stationDepart.NomStation} et {stationArrivee.NomStation}");

                PlusCourtChemin<int> plusCourtChemin = new PlusCourtChemin<int>();

                // test de Dijkstra
                Console.WriteLine("\nAlgorithme de Dijkstra :");
                List<Noeud<int>> cheminDijkstra = plusCourtChemin.Dijkstra(grapheMetro, stationDepart, stationArrivee);
                AfficherChemin(cheminDijkstra);

                // test de Bellman-Ford
                Console.WriteLine("\nAlgorithme de Bellman-Ford :");
                List<Noeud<int>> cheminBellmanFord = plusCourtChemin.BellmanFord(grapheMetro, stationDepart, stationArrivee);
                AfficherChemin(cheminBellmanFord);
            }

            // PARTIE BDD
            Console.WriteLine("\nTest de la connexion a la base de donnees :");
            Connexion maConnexion = new Connexion();
            maConnexion.TestConnexion();
            maConnexion.FermerConnexion();
        }

        /// <summary>
        /// affiche un chemin de stations
        /// </summary>
        private static void AfficherChemin(List<Noeud<int>> chemin)
        {
            if (chemin.Count == 0)
            {
                Console.WriteLine("Aucun chemin trouvé");
                return;
            }

            Console.WriteLine("Chemin trouvé :");
            for (int i = 0; i < chemin.Count; i++)
            {
                Console.Write(chemin[i].NomStation);
                if (i < chemin.Count - 1)
                    Console.Write(" -> ");
            }
            Console.WriteLine();
        }
    }
}

