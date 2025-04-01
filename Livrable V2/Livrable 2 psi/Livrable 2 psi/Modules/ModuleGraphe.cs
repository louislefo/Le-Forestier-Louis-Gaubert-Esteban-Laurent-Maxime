using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Livrable_2_psi
{
    
    public class ModuleGraphe
    {
        private Graphe<int> grapheMetro;
        private string  cheminFichierMetro = @"../../../Données/MetroParisNoeuds.csv";
        private string  cheminFichierArcs = @"../../../Données/MetroParisArcs.csv";
        private ChargerFichiers  chargeur;
        private GestionnaireItineraire<int>   gestionnaire;

        /// <summary>
        /// constructeur du module graphe
        /// </summary>
        public ModuleGraphe()
        {
            grapheMetro = new Graphe<int>();
            chargeur =  new ChargerFichiers();
            ChargerDonneesMetro();
            gestionnaire = new GestionnaireItineraire<int>(grapheMetro);
        }

        /// <summary>
        /// charge les donnees du metro
        /// </summary>
        private void ChargerDonneesMetro()
        {
            // chargement des noeuds
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);
            Console.WriteLine("Nombre de noeuds charges : " + noeudsMetro.Count);
            
            // ajout des noeuds au graphe
            foreach (int id in noeudsMetro.Keys)
            {
                grapheMetro.Noeuds[id] = noeudsMetro[id];
            }
            
            // chargement des arcs
            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);
            Console.WriteLine("Nombre de liens dans le graphe : " + grapheMetro.Liens.Count);
        }

        /// <summary>
        /// menu principal de la partie graphe
        /// </summary>
        public void AfficherMenu()
        {
            bool continuer = true;
            
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("\n=== MENU GRAPHE METRO ===");
                Console.WriteLine("1. Afficher la carte du metro");
                Console.WriteLine("2. Rechercher un itineraire");
                Console.WriteLine("3. Afficher les informations du metro");
                Console.WriteLine("4. Afficher les stations d'une ligne");
                Console.WriteLine("5. Analyser le reseau");
                Console.WriteLine("0. Retour au menu principal");
                
                Console.Write("\nVotre choix : ");
                string  choix = Console.ReadLine();
                
                switch (choix)
                {
                    case "1":
                        AfficherCarteMetro();
                        break;
                    case "2":
                        RechercherItineraire();
                        break;
                    case "3":
                        AfficherInformationsMetro();
                        break;
                    case "4":
                        AfficherStationsParLigne();
                        break;
                    case "5":
                        AnalyserReseau();
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix non valide !");
                        break;
                }
                
                if (continuer)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// affiche la carte du metro
        /// </summary>
        private void AfficherCarteMetro()
        {
            Console.WriteLine("\nCreation de la carte du metro...");
            
            // dessin de la carte
            VisualisationCarte   visMetro = new VisualisationCarte(1200, 800);
            visMetro.DessinerGraphe(grapheMetro);
            visMetro.SauvegarderImage("metro.png");
            Console.WriteLine("Carte du metro sauvegardee sous le nom de metro.png");
            
            // ouverture de l'image
            Console.WriteLine("Ouverture de l'image...");
            try
            {
                Process.Start(new ProcessStartInfo("metro.png") { UseShellExecute = true });
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'ouverture du fichier : " + e.Message);
            }
        }

        /// <summary>
        /// recherche un itineraire dans le metro
        /// </summary>
        private void RechercherItineraire()
        {
            Console.WriteLine("\n=== RECHERCHE D'ITINERAIRE ===");
            
            
            // saisie des stations
            Console.WriteLine("\nEntrez l'ID de la station de depart :");
            string  idDepart = Console.ReadLine();
            
            Console.WriteLine("Entrez l'ID de la station d'arrivee :");
            string idArrivee = Console.ReadLine();
            
            // recherche de l'itineraire
            List<Noeud<int>> itineraire = gestionnaire.RechercherItineraire(idDepart, idArrivee);
            
            // visualisation de l'itineraire
            if (itineraire != null && itineraire.Count > 0)
            {
                Console.WriteLine("\nCreation de la carte d'itineraire...");
                VisualisationItineraire  visItineraire = new VisualisationItineraire(1200, 800);
                string   texteItineraire = "Itineraire de " + itineraire[0].NomStation + " a " + itineraire[itineraire.Count - 1].NomStation;
                visItineraire.DessinerItineraire(grapheMetro, itineraire, texteItineraire);
                visItineraire.SauvegarderImage("itineraire.png");
                Console.WriteLine("Carte de l'itineraire sauvegardee sous le nom de itineraire.png");
                
                // ouverture de l'image
                Console.WriteLine("Ouverture de l'image...");
                try
                {
                    Process.Start(new ProcessStartInfo("itineraire.png") { UseShellExecute = true });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur lors de l'ouverture du fichier : " + e.Message);
                }
            }
        }

        /// <summary>
        /// affiche les infos generales du metro
        /// </summary>
        private void AfficherInformationsMetro()
        {
            Console.WriteLine("\n=== INFORMATIONS DU METRO ===");
            
            // compter les stations par ligne
            Dictionary<string, int>  stationsParLigne = new Dictionary<string, int>();
            Dictionary<string, List<string>>  nomsStationsParLigne = new Dictionary<string, List<string>>();
            
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                if (!stationsParLigne.ContainsKey(noeud.NumeroLigne))
                {
                    stationsParLigne[noeud.NumeroLigne] = 0;
                    nomsStationsParLigne[noeud.NumeroLigne] = new List<string>();
                }
                
                if (!nomsStationsParLigne[noeud.NumeroLigne].Contains(noeud.NomStation))
                {
                    stationsParLigne[noeud.NumeroLigne]++;
                    nomsStationsParLigne[noeud.NumeroLigne].Add(noeud.NomStation);
                }
            }
            
            // affichage des statistiques
            Console.WriteLine("Nombre total de stations : " + nomsStationsParLigne.Values.Sum(liste => liste.Count));
            Console.WriteLine("Nombre total de noeuds : " + grapheMetro.Noeuds.Count);
            Console.WriteLine("Nombre total de liens : " + grapheMetro.Liens.Count);
            Console.WriteLine("\nNombre de stations par ligne :");
            
            foreach (KeyValuePair<string, int> ligne in stationsParLigne.OrderBy(l => l.Key))
            {
                Console.WriteLine("Ligne " + ligne.Key + " : " + ligne.Value + " stations");
            }
        }

        /// <summary>
        /// affiche les stations dune ligne de metro
        /// </summary>
        private void AfficherStationsParLigne()
        {
            // récupérer la liste des lignes
            HashSet<string>  lignes = new HashSet<string>();
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                lignes.Add(noeud.NumeroLigne);
            }
            
            // afficher la liste des lignes
            Console.WriteLine("\n=== LIGNES DE METRO DISPONIBLES ===");
            foreach (string  ligne in lignes.OrderBy(l => l))
            {
                Console.WriteLine("Ligne " + ligne);
            }
            
            // saisie de la ligne
            Console.WriteLine("\nEntrez le numero de ligne :");
            string   ligneChoisie = Console.ReadLine();
            
            // vérifier que la ligne existe
            if (!lignes.Contains(ligneChoisie))
            {
                Console.WriteLine("Cette ligne n'existe pas !");
                return;
            }
            
            // récupérer et afficher les stations de la ligne
            Console.WriteLine("\n=== STATIONS DE LA LIGNE " + ligneChoisie + " ===");
            HashSet<string>  stationsDeLigne = new HashSet<string>();
            
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                if (noeud.NumeroLigne == ligneChoisie)
                {
                    stationsDeLigne.Add(noeud.NomStation);
                }
            }
            
            foreach (string   station in stationsDeLigne.OrderBy(s => s))
            {
                Console.WriteLine("- " + station);
            }
            
            Console.WriteLine("\nNombre total de stations : " + stationsDeLigne.Count);
        }

        /// <summary>
        /// analyse le reseau de metro
        /// </summary>
        private void AnalyserReseau()
        {
            Console.WriteLine("\n=== ANALYSE DU RESEAU DE METRO ===");
            
            // compter les correspondances
            Dictionary<string, int>  correspondancesParStation = new Dictionary<string, int>();
            
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                if (!correspondancesParStation.ContainsKey(noeud.NomStation))
                {
                    correspondancesParStation[noeud.NomStation] = 0;
                }
                
                correspondancesParStation[noeud.NomStation]++;
            }
            
            // afficher les stations avec correspondances
            Console.WriteLine("\nStations avec correspondances :");
            int  nbCorrespondances = 0;
            
            foreach (KeyValuePair<string, int> station in correspondancesParStation.OrderByDescending(s => s.Value))
            {
                if (station.Value > 1)
                {
                    Console.WriteLine(station.Key + " : " + station.Value + " lignes");
                    nbCorrespondances++;
                }
            }
            
            Console.WriteLine("\nNombre total de stations avec correspondances : " + nbCorrespondances);
        }
    }
}
