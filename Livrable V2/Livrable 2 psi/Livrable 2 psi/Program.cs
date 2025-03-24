using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;

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
            string cheminFichierMetro = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MetroParisNoeuds.csv");
            Dictionary<int, NoeudMetro> noeudsMetro = new Dictionary<int, NoeudMetro>();

            try
            {
                // lit le fichier CSV avec StreamReader
                using (StreamReader sr = new StreamReader(cheminFichierMetro, System.Text.Encoding.UTF8))
                {
                    // saute la ligne d'en-tête
                    string ligneEnTete = sr.ReadLine();

                    // lit chaque ligne
                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        string[] colonnes = ligne.Split(';');
                        if (colonnes.Length >= 7)
                        {
                            int id = int.Parse(colonnes[0]);
                            string numeroLigne = colonnes[1];
                            string nomStation = colonnes[2];
                            double longitude = double.Parse(colonnes[3], CultureInfo.InvariantCulture);
                            double latitude = double.Parse(colonnes[4], CultureInfo.InvariantCulture);

                            var noeudMetro = new NoeudMetro(id, nomStation, longitude, latitude, numeroLigne);
                            noeudsMetro[id] = noeudMetro;
                            grapheMetro.Noeuds[id] = noeudMetro;
                        }
                    }
                }

                // lit le fichier des arcs pour ajouter les liens
                string cheminFichierArcs = @".\.\MetroParisArcs.csv";
                using (StreamReader sr = new StreamReader(cheminFichierArcs, System.Text.Encoding.UTF8))
                {
                    // saute la ligne d'en-tête
                    string ligneEnTete = sr.ReadLine();

                    // lit chaque ligne
                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        string[] colonnes = ligne.Split(';');
                        if (colonnes.Length >= 5 && !string.IsNullOrEmpty(colonnes[3]))
                        {
                            int idStation = int.Parse(colonnes[0]);
                            int idSuivant = int.Parse(colonnes[3]); // ID de la station suivante

                            // ajoute le lien entre la station actuelle et la station suivante
                            grapheMetro.AjouterLien(idStation, idSuivant);
                        }
                    }
                }

                // crée la visualisation du métro
                GrapheMetro visMetro = new GrapheMetro(grapheMetro);
                visMetro.SauvegarderGraphique("metro.png");
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier CSV : " + e.Message);
            }

            /*
            string cheminFichier = @".\.\soc-karate.mtx";

            Graphe<int> monGraphe = new Graphe<int>();

            /// lit le fichier et ajoute les liens
            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    while (!sr.EndOfStream)
                    {
                        string ligne = sr.ReadLine();

                        /// ignore les commentaires et lignes vides
                        if (string.IsNullOrWhiteSpace(ligne) || ligne.StartsWith("%"))
                            continue;

                        /// lit les liens entre les noeuds
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

            /// affiche les infos du graphe
            Console.WriteLine("Nombre de nœuds : " + monGraphe.Noeuds.Count);
            Console.WriteLine("Nombre de liens : " + monGraphe.Liens.Count);

            foreach (var noeud in monGraphe.Noeuds.Values)
            {
                Console.Write("Noeud " + noeud.Id + " -> ");
                foreach (var voisin in noeud.Voisins)
                    Console.Write(voisin.Id + " ");
                Console.WriteLine();
            }

            /// verifie le premier noeud
            Noeud<int> premierNoeud = monGraphe.ObtenirPremierNoeud();
            if (premierNoeud == null)
            {
                Console.WriteLine("Erreur : plus de noeuds");
                //return;
            }

            /// test du parcours en largeur
            Console.WriteLine("\n Parcours largeur :");
            monGraphe.largeur(premierNoeud);

            /// test du parcours en profondeur
            Console.WriteLine("\n Parcours Profondeur :");
            List<int> visiteDFS = new List<int>();
            monGraphe.Profondeur(premierNoeud, visiteDFS);
            Console.WriteLine();

            /// verifie si le graphe est connexe
            Console.WriteLine("\n Le graphe est connexe ? " + monGraphe.EstConnexe());

            /// verifie si le graphe a un cycle
            Console.WriteLine(" Le graphe contient un cycle ? " + monGraphe.ContientCycle());


            // PARTIE VISUALISATION (PAS TOUCHE MOUCHE)
            Visualisation<int> vis = new Visualisation<int>(monGraphe);
            vis.SauvegarderGraphique("graphe.png");
            Console.WriteLine("\nGraphique sauvegardé sous le nom de graphe.png");

            // ouvre le fichier graphe.png
            try
            {
                Process.Start(new ProcessStartInfo("graphe.png") { UseShellExecute = true });
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'ouverture du fichier : " + e.Message);
            }
            */


        }
    }
}

