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
            Console.WriteLine("\nRecherche d'un itineraire :");
            GestionnaireItineraire<int> gestionnaire = new GestionnaireItineraire<int>(grapheMetro);
            gestionnaire.AfficherListeStations();

            Console.WriteLine("\nEntrez l'ID de la station de depart :");
            string idDepart = Console.ReadLine();

            Console.WriteLine("Entrez l'ID de la station d'arrivee :");
            string idArrivee = Console.ReadLine();

            gestionnaire.RechercherItineraire(idDepart, idArrivee);

            // PARTIE BDD
            /*
            Console.WriteLine("\nTest de la connexion a la base de donnees :");
            Connexion maConnexion = new Connexion();
            maConnexion.TestConnexion();
            maConnexion.FermerConnexion();
            */
        }
    }
}

