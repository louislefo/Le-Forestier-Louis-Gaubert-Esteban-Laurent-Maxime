using System;
using System.Collections.Generic;

namespace LivrableV3
{
    /// <summary>
    /// cette classe gere les trajets dans le metro
    /// elle permet de trouver le meilleur chemin entre deux stations
    /// elle utilise les algorithmes de plus court chemin comme dijkstra
    /// elle calcule aussi les temps de trajet et les correspondances
    /// </summary>
    public class GestionnaireItineraire<T> where T : IComparable<T>
    {
        private Graphe<T> grapheMetro;
        private PlusCourtChemin<T> plusCourtChemin;
        public double tempsTotal;
        public string detail;

        /// <summary>
        /// cree un nouveau gestionnaire de trajets
        /// initialise le graphe du metro et l'algorithme de plus court chemin
        /// le graphe doit contenir toutes les stations et leurs connexions
        /// </summary>
        public GestionnaireItineraire(Graphe<T> graphe)
        {
            grapheMetro = graphe;
            plusCourtChemin = new PlusCourtChemin<T>();
        }

        /// <summary>
        /// affiche la liste de toutes les stations du metro
        /// montre le numero, le nom et la ligne de chaque station
        /// utile pour choisir une station de depart ou d'arrivee
        /// </summary>
        public void AfficherListeStations()
        {
            Console.WriteLine("\nListe des stations disponibles :");
            foreach (Noeud<T> noeud in grapheMetro.Noeuds.Values)
            {
                Console.WriteLine(noeud.Id + " - " + noeud.NomStation + " (Ligne " + noeud.NumeroLigne + ")");
            }
        }

        /// <summary>
        /// cherche un trajet entre deux stations
        /// utilise les numeros des stations pour les trouver
        /// retourne la liste des stations a parcourir dans l'ordre
        /// calcule aussi le temps total du trajet
        /// </summary>
        public List<Noeud<T>> RechercherItineraire(string idDepart, string idArrivee)
        {
            // convertit les IDs en type T
            T depart = (T)Convert.ChangeType(idDepart, typeof(T));
            T arrivee = (T)Convert.ChangeType(idArrivee, typeof(T));

            // verifie que les stations existent
            if (!grapheMetro.Noeuds.ContainsKey(depart) || !grapheMetro.Noeuds.ContainsKey(arrivee))
            {
                Console.WriteLine("Une des stations n'existe pas !");
                return new List<Noeud<T>>();
            }

            Noeud<T> stationDepart = grapheMetro.Noeuds[depart];
            Noeud<T> stationArrivee = grapheMetro.Noeuds[arrivee];

            Console.WriteLine("\nRecherche du plus court chemin entre " + stationDepart.NomStation + " et " + stationArrivee.NomStation);

            // calcule le plus court chemin avec Dijkstra
            List<Noeud<T>> chemin = plusCourtChemin.Dijkstra(grapheMetro, stationDepart, stationArrivee);
            List<Noeud<T>> chemin1 = plusCourtChemin.BellmanFord(grapheMetro, stationDepart, stationArrivee);
            Dictionary<(Noeud<T>, Noeud<T>), double> chemin2 = plusCourtChemin.FloydWarshall(grapheMetro);

            // affiche le resultat
            if (chemin.Count == 0)
            {
                Console.WriteLine("Aucun chemin trouvé entre ces stations.");
                return chemin;
            }
            this.detail = AfficherItineraire(chemin);

            return chemin;
        }

        /// <summary>
        /// affiche un trajet de facon detaillee
        /// montre chaque station et les changements de ligne
        /// indique les temps de trajet et de correspondance
        /// calcule le temps total du trajet en minutes
        /// </summary>
        public string AfficherItineraire(List<Noeud<T>> chemin)
        {
            string rep = " Itineraire trouve :\r\n";
            Console.WriteLine("\nItineraire trouve :");

            if (chemin.Count == 0)
            {
                rep += "Aucun chemin trouve.\r\n";
                return rep;
            }

            string ligneActuelle = chemin[0].NumeroLigne;
            double tempsTotal = 0;

            rep += "Depart : " + chemin[0].NomStation + " (Ligne " + ligneActuelle + ")\r\n";

            for (int i = 1; i < chemin.Count; i++)
            {
                Noeud<T> station = chemin[i];
                Noeud<T> stationPrecedente = chemin[i - 1];

                // si on change de ligne
                if (station.NumeroLigne != ligneActuelle)
                {
                    rep += "Correspondance a " + station.NomStation + " :\r\n";
                    rep += "  - Ligne " + ligneActuelle + " -> Ligne " + station.NumeroLigne + "\r\n";
                    rep += "  - Temps de correspondance : " + station.TempsCorrespondance + " minutes\r\n";
                    tempsTotal += station.TempsCorrespondance;
                    ligneActuelle = station.NumeroLigne;
                }

                // on ajoute le temps de trajet entre les stations
                foreach (Lien<T> lien in grapheMetro.Liens)
                {
                    if ((lien.Noeud1 == stationPrecedente && lien.Noeud2 == station) ||
                        (lien.Noeud1 == station && lien.Noeud2 == stationPrecedente))
                    {
                        tempsTotal += lien.Poids;
                        break;
                    }
                }

                rep += "  " + station.NomStation + " (Ligne " + station.NumeroLigne + ")\r\n";
            }

            rep += "\r\nTemps total du trajet : " + tempsTotal + " minutes\r\n";
            this.tempsTotal = tempsTotal;

            return rep;
        }
    }
} 