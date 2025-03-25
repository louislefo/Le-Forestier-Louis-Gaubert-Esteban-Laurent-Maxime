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
            // PARTIE AUTHENTIFICATION
            Console.WriteLine("=== Bienvenue sur Liv'In Paris ===");
            Application application = new Application();
            application.Demarrer();

            // PARTIE METRO
            Graphe<int> grapheMetro = new Graphe<int>();
            string cheminFichierMetro = @"../../../MetroParisNoeuds.csv";
            string cheminFichierArcs = @"../../../MetroParisArcs.csv";

            // charge les fichiers
            ChargerFichiers chargeur = new ChargerFichiers();
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);
            Console.WriteLine("Nombre de noeuds charges : " + noeudsMetro.Count);
            
            // ajoute les noeuds au graphe
            foreach (int id in noeudsMetro.Keys)
            {
                grapheMetro.Noeuds[id] = noeudsMetro[id];
            }
            Console.WriteLine("Nombre de noeuds dans le graphe : " + grapheMetro.Noeuds.Count);

            // charge les arcs  
            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);
            Console.WriteLine("Nombre de liens dans le graphe : " + grapheMetro.Liens.Count);

            // affiche quelques infos sur les noeuds pour debug
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                Console.WriteLine("Station: " + noeud.NomStation + ", Ligne: " + noeud.NumeroLigne + ", Couleur: " + noeud.CouleurLigne);
                Console.WriteLine("Position: (" + noeud.Longitude + ", " + noeud.Latitude + ")");
                break; // on affiche juste le premier noeud pour tester
            }

            // crée la visualisation du métro
            VisualisationCarte visMetro = new VisualisationCarte(1200, 800);
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

            List<Noeud<int>> itineraire = gestionnaire.RechercherItineraire(idDepart, idArrivee);

            // crée la visualisation de l'itineraire
            if (itineraire != null && itineraire.Count > 0)
            {
                VisualisationItineraire visItineraire = new VisualisationItineraire(1200, 800);
                string texteItineraire = "Itineraire de " + itineraire[0].NomStation + " a " + itineraire[itineraire.Count - 1].NomStation;
                visItineraire.DessinerItineraire(grapheMetro, itineraire, texteItineraire);
                visItineraire.SauvegarderImage("itineraire.png");
                Console.WriteLine("\nCarte de l'itineraire sauvegardee sous le nom de itineraire.png");

                // ouvre le fichier itineraire.png
                try
                {
                    Process.Start(new ProcessStartInfo("itineraire.png") { UseShellExecute = true });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur lors de l'ouverture du fichier : " + e.Message);
                }
            }

            // PARTIE BDD
            /*
            Console.WriteLine("\nTest de la connexion a la base de donnees :");
            ConnexionBDD maConnexion = new ConnexionBDD();
            maConnexion.TestConnexion();
            maConnexion.FermerConnexion();
            */
        }
    }
}

