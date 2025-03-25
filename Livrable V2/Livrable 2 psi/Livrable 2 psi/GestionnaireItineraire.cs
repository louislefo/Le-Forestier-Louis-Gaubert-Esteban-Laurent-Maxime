using System;
using System.Collections.Generic;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere les itineraires du metro
    /// </summary>
    public class GestionnaireItineraire<T> where T : IComparable<T>
    {
        private Graphe<T> grapheMetro;
        private PlusCourtChemin<T> plusCourtChemin;

        /// <summary>
        /// constructeur de la classe
        /// </summary>
        public GestionnaireItineraire(Graphe<T> graphe)
        {
            grapheMetro = graphe;
            plusCourtChemin = new PlusCourtChemin<T>();
        }

        /// <summary>
        /// affiche la liste des stations disponibles
        /// </summary>
        public void AfficherListeStations()
        {
            Console.WriteLine("\nListe des stations disponibles :");
            foreach (var noeud in grapheMetro.Noeuds.Values)
            {
                Console.WriteLine($"{noeud.Id} - {noeud.NomStation} (Ligne {noeud.NumeroLigne})");
            }
        }

        /// <summary>
        /// recherche un itineraire entre deux stations
        /// </summary>
        public void RechercherItineraire(string idDepart, string idArrivee)
        {
            // convertit les IDs en type T
            T depart = (T)Convert.ChangeType(idDepart, typeof(T));
            T arrivee = (T)Convert.ChangeType(idArrivee, typeof(T));

            // verifie que les stations existent
            if (!grapheMetro.Noeuds.ContainsKey(depart) || !grapheMetro.Noeuds.ContainsKey(arrivee))
            {
                Console.WriteLine("Une des stations n'existe pas !");
                return;
            }

            Noeud<T> stationDepart = grapheMetro.Noeuds[depart];
            Noeud<T> stationArrivee = grapheMetro.Noeuds[arrivee];

            Console.WriteLine($"\nRecherche du plus court chemin entre {stationDepart.NomStation} et {stationArrivee.NomStation}");

            // calcule le plus court chemin avec Dijkstra
            List<Noeud<T>> chemin = plusCourtChemin.Dijkstra(grapheMetro, stationDepart, stationArrivee);

            // affiche le resultat
            if (chemin.Count == 0)
            {
                Console.WriteLine("Aucun chemin trouvé entre ces stations.");
                return;
            }

            AfficherItineraire(chemin);
            AfficherDetailsTrajet(chemin);
        }

        /// <summary>
        /// affiche un itineraire
        /// </summary>
        private void AfficherItineraire(List<Noeud<T>> chemin)
        {
            Console.WriteLine("\nItineraire trouve :");
            for (int i = 0; i < chemin.Count; i++)
            {
                Console.Write(chemin[i].NomStation);
                if (i < chemin.Count - 1)
                {
                    Console.Write(" -> ");
                    // affiche la ligne de metro si on change de ligne
                    if (chemin[i].NumeroLigne != chemin[i + 1].NumeroLigne)
                    {
                        Console.Write($"[Correspondance Ligne {chemin[i + 1].NumeroLigne}] -> ");
                    }
                }
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// affiche les details du trajet
        /// </summary>
        private void AfficherDetailsTrajet(List<Noeud<T>> chemin)
        {
            Console.WriteLine("Details du trajet :");
            Console.WriteLine("-------------------");

            string ligneActuelle = chemin[0].NumeroLigne;
            Console.WriteLine($"\nDépart : {chemin[0].NomStation} (Ligne {ligneActuelle})");

            for (int i = 1; i < chemin.Count; i++)
            {
                if (chemin[i].NumeroLigne != ligneActuelle)
                {
                    Console.WriteLine($"\nCorrespondance à {chemin[i].NomStation}");
                    Console.WriteLine($"Prendre la Ligne {chemin[i].NumeroLigne}");
                    ligneActuelle = chemin[i].NumeroLigne;
                }
                else
                {
                    Console.WriteLine($"Station suivante : {chemin[i].NomStation}");
                }
            }

            Console.WriteLine($"\nArrivée : {chemin[chemin.Count - 1].NomStation}");
            Console.WriteLine($"Nombre total de stations : {chemin.Count}");
            Console.WriteLine($"Nombre de correspondances : {CompterCorrespondances(chemin)}");
        }

        /// <summary>
        /// compte le nombre de correspondances dans le trajet
        /// </summary>
        private int CompterCorrespondances(List<Noeud<T>> chemin)
        {
            int correspondances = 0;
            for (int i = 1; i < chemin.Count; i++)
            {
                if (chemin[i].NumeroLigne != chemin[i - 1].NumeroLigne)
                {
                    correspondances++;
                }
            }
            return correspondances;
        }
    }
} 