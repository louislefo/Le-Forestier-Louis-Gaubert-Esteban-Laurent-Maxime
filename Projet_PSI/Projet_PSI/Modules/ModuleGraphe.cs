using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Livrable_2_psi
{
    /// <summary>
    /// cette classe sert a gerer tout ce qui concerne le metro dans l'application
    /// elle permet de voir la carte du metro, de chercher des itinéraires et de voir les infos sur les stations
    /// c'est une classe importante car elle gere les trajets pour les livraisons
    /// </summary>
    public class ModuleGraphe
    {
        private Graphe<int> grapheMetro;
        
        private GestionnaireItineraire<int>   gestionnaire;

        /// <summary>
        /// on cree le module avec le graphe du metro
        /// on initialise aussi le gestionnaire d'itinéraire qui va nous servir pour chercher les chemins
        /// </summary>
        public ModuleGraphe(Graphe<int> grapheMetro)
        {
            this.grapheMetro = grapheMetro;
            
            gestionnaire = new GestionnaireItineraire<int>(grapheMetro);
        }

        /// <summary>
        /// cette methode sert a afficher la carte du metro
        /// elle cree une image avec toutes les stations et les lignes
        /// puis elle ouvre l'image pour qu'on puisse la voir
        /// </summary>
        public void AfficherCarteMetro()
        {
            Console.WriteLine("\nCreation de la carte du metro...");
            
            // on cree la carte avec une taille de 1200x800
            VisualisationCarte   visMetro = new VisualisationCarte(1200, 800);
            visMetro.DessinerGraphe(grapheMetro);
            visMetro.SauvegarderImage("metro.png");
            Console.WriteLine("Carte du metro sauvegardee sous le nom de metro.png");
            
            // on essaie d'ouvrir l'image
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
        /// cette methode sert a chercher un itineraire entre deux stations
        /// elle demande les stations de depart et d'arrivee
        /// puis elle cherche le chemin et cree une image avec le trajet
        /// </summary>
        public void RechercherItineraire()
        {
            Console.WriteLine("\n=== RECHERCHE D'ITINERAIRE ===");
            
            // on demande les stations
            Console.WriteLine("\nEntrez l'ID de la station de depart :");
            string  idDepart = Console.ReadLine();
            
            Console.WriteLine("Entrez l'ID de la station d'arrivee :");
            string idArrivee = Console.ReadLine();
            
            // on cherche l'itineraire
            List<Noeud<int>> itineraire = gestionnaire.RechercherItineraire(idDepart, idArrivee);
            
            // si on a trouve un itineraire, on cree l'image
            if (itineraire != null && itineraire.Count > 0)
            {
                Console.WriteLine("\nCreation de la carte d'itineraire...");
                VisualisationItineraire  visItineraire = new VisualisationItineraire(1200, 800);
                string   texteItineraire = "Itineraire de " + itineraire[0].NomStation + " a " + itineraire[itineraire.Count - 1].NomStation;
                visItineraire.DessinerItineraire(grapheMetro, itineraire, texteItineraire);
                visItineraire.SauvegarderImage("itineraire.png");
                Console.WriteLine("Carte de l'itineraire sauvegardee sous le nom de itineraire.png");
                
                // on essaie d'ouvrir l'image
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
        /// cette methode sert a afficher des infos sur le metro
        /// elle compte combien il y a de stations par ligne
        /// et elle montre aussi toutes les stations uniques
        /// </summary>
        public void AfficherInformationsMetro()
        {
            Console.WriteLine("\n=== INFORMATIONS DU METRO ===");
            
            // on cree des dictionnaires pour compter les stations
            Dictionary<string, int>  stationsParLigne = new Dictionary<string, int>();
            Dictionary<string, List<string>>  nomsStationsParLigne = new Dictionary<string, List<string>>();
            
            // on cree une liste pour toutes les stations uniques
            List<string>  toutesLesStations = new List<string>();
            
            // on parcourt tous les noeuds du graphe
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                // on ajoute la ligne si elle existe pas
                if (!stationsParLigne.ContainsKey(noeud.NumeroLigne))
                {
                    stationsParLigne[noeud.NumeroLigne] = 0;
                    nomsStationsParLigne[noeud.NumeroLigne] = new List<string>();
                }
                
                // on ajoute la station si elle existe pas dans la ligne
                if (!nomsStationsParLigne[noeud.NumeroLigne].Contains(noeud.NomStation))
                {
                    stationsParLigne[noeud.NumeroLigne]++;
                    nomsStationsParLigne[noeud.NumeroLigne].Add(noeud.NomStation);
                }
                
                // on ajoute la station a la liste totale si elle existe pas
                if (!toutesLesStations.Contains(noeud.NomStation))
                {
                    toutesLesStations.Add(noeud.NomStation);
                }
            }
            
            // on affiche les stats
            Console.WriteLine("Nombre total de stations uniques : " + toutesLesStations.Count);
            Console.WriteLine("Nombre total de noeuds (avec doublons) : " + grapheMetro.Noeuds.Count);
            Console.WriteLine("Nombre total de liens : " + grapheMetro.Liens.Count);
            Console.WriteLine("\nNombre de stations par ligne :");
            
            // on affiche le nombre de stations par ligne
            foreach (KeyValuePair<string, int> ligne in stationsParLigne.OrderBy(l => l.Key))
            {
                Console.WriteLine("Ligne " + ligne.Key + " : " + ligne.Value + " stations");
            }
        }

        /// <summary>
        /// cette methode sert a afficher toutes les stations d'une ligne
        /// elle verifie d'abord si la ligne existe
        /// puis elle montre toutes les stations dans l'ordre
        /// </summary>
        public void AfficherStationsParLigne(string ligneChoisie)
        {
            // on cree une liste pour toutes les lignes
            List<string>  lignes = new List<string>();
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                lignes.Add(noeud.NumeroLigne);
            }
            
            // on verifie si la ligne existe
            if (!lignes.Contains(ligneChoisie))
            {
                Console.WriteLine("Cette ligne n'existe pas !");
                return;
            }
            
            // on cree une liste pour les stations de la ligne
            Console.WriteLine("\n=== STATIONS DE LA LIGNE " + ligneChoisie + " ===");
            List<string>  stationsDeLigne = new List<string>();
            
            // on ajoute toutes les stations de la ligne
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                if (noeud.NumeroLigne == ligneChoisie)
                {
                    stationsDeLigne.Add(noeud.NomStation);
                }
            }
            
            // on affiche les stations dans l'ordre
            foreach (string   station in stationsDeLigne.OrderBy(s => s))
            {
                Console.WriteLine("- " + station);
            }
            
            Console.WriteLine("\nNombre total de stations : " + stationsDeLigne.Count);
        }
    }
}
