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
            string cheminFichierMetro = @".\.\MetroParisNoeuds.csv";
            string cheminFichierArcs = @".\.\MetroParisArcs.csv";

            // charge les fichiers
            ChargerFichiers chargeur = new ChargerFichiers();
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);
            
            // ajoute les noeuds au graphe
            foreach (var noeud in noeudsMetro)
            {
                grapheMetro.Noeuds[noeud.Key] = noeud.Value;
            }

            // charge les arcs  
            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);  // Modif a faire lien et arcs

            // crée la visualisation du métro
            Visualisation<int> visMetro = new Visualisation<int>(grapheMetro, 1200, 800);
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

            // PARTIE BDD
            Console.WriteLine("\nTest de la connexion a la base de donnees :");
            Connexion maConnexion = new Connexion();
            maConnexion.TestConnexion();
            maConnexion.FermerConnexion();
        }
    }
}

