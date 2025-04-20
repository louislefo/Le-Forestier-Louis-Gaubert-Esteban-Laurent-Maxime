using System;
using System.Collections.Generic;

namespace LivrableV3
{
    /// <summary>
    /// classe qui gere les itineraires du metro
    /// </summary>
    public class GestionnaireItineraire<T> where T : IComparable<T>
    {
        private Graphe<T> grapheMetro;
        private PlusCourtChemin<T> plusCourtChemin;
        public double tempsTotal;
        public string detail;

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
            foreach (Noeud<T> noeud in grapheMetro.Noeuds.Values)
            {
                Console.WriteLine(noeud.Id + " - " + noeud.NomStation + " (Ligne " + noeud.NumeroLigne + ")");
            }
        }

        /// <summary>
        /// recherche un itineraire entre deux stations
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
        /// affiche un itineraire
        /// </summary>
        public string AfficherItineraire(List<Noeud<T>> chemin)
        {
            string rep = " Itineraire trouve :\r\n";
            Console.WriteLine("\nItineraire trouve :");

            for (int i = 0; i < chemin.Count; i++)
            {
                rep += " "+chemin[i].NomStation+" ";
                Console.Write(chemin[i].NomStation);
                if (i < chemin.Count - 1)
                {
                    rep += " -> ";
                    Console.Write(" -> ");
                    // affiche la ligne de metro si on change de ligne
                    if (chemin[i].NumeroLigne != chemin[i + 1].NumeroLigne)
                    {
                        rep += "[Correspondance Ligne " + chemin[i + 1].NumeroLigne + "] -> ";
                        Console.Write("[Correspondance Ligne " + chemin[i + 1].NumeroLigne + "] -> ");
                    }
                }
            }
            rep += "\n";
            Console.WriteLine("\n");
            List<Noeud<T>> chemin2 = new List<Noeud<T>>(chemin);
            rep += AfficherDetailsTrajet(chemin2);

            return rep;
        }

        /// <summary>
        /// affiche les details du trajet
        /// </summary>
        private string AfficherDetailsTrajet(List<Noeud<T>> chemin)
        {
            string rep = "Details du trajet :\r\n";
            Console.WriteLine("Details du trajet :");
            rep+= "--------------------\r\n";
            Console.WriteLine("-------------------");

            string ligneActuelle = chemin[0].NumeroLigne;
            rep += "Départ : " + chemin[0].NomStation + " (Ligne " + ligneActuelle + ")\r\n";
            Console.WriteLine("\nDépart : " + chemin[0].NomStation + " (Ligne " + ligneActuelle + ")");

            double tempsTotal = 0;
            int nombreCorrespondances = 0;

            for (int i = 1; i < chemin.Count; i++)
            {
                if (chemin[i].NumeroLigne != ligneActuelle)
                {
                    rep += "\nCorrespondance à " + chemin[i].NomStation + "\r\n";
                    Console.WriteLine("\nCorrespondance à " + chemin[i].NomStation);
                    rep += "Prendre la Ligne " + chemin[i].NumeroLigne + "\r\n";
                    Console.WriteLine("Prendre la Ligne " + chemin[i].NumeroLigne);
                    rep += "Temps de correspondance : " + chemin[i].TempsCorrespondance + " minutes\r\n";
                    Console.WriteLine("Temps de correspondance : " + chemin[i].TempsCorrespondance + " minutes");
                    rep += "\r\n";
                    ligneActuelle = chemin[i].NumeroLigne;
                    nombreCorrespondances++;
                    tempsTotal += chemin[i].TempsCorrespondance;
                }
                else
                {
                    rep += "\nStation suivante : " + chemin[i].NomStation + "\r\n";
                    Console.WriteLine("Station suivante : " + chemin[i].NomStation);
                    rep+= "\r\n";
                    // ajoute le temps entre les stations
                    foreach (Lien<T> lien in grapheMetro.Liens)
                    {
                        if ((lien.Noeud1 == chemin[i-1] && lien.Noeud2 == chemin[i]) ||
                            (lien.Noeud2 == chemin[i-1] && lien.Noeud1 == chemin[i]))
                        {
                            tempsTotal += lien.Poids;
                            break;
                        }
                    }
                }
            }

            rep += "\nArrivée : " + chemin[chemin.Count - 1].NomStation + "\r\n";
            Console.WriteLine("\nArrivée : " + chemin[chemin.Count - 1].NomStation);
            rep += "Temps total estimé : " + tempsTotal + " minutes\r\n";
            Console.WriteLine("Nombre total de stations : " + chemin.Count);
            rep += "Nombre total de stations : " + chemin.Count + "\r\n";
            Console.WriteLine("Nombre de correspondances : " + nombreCorrespondances);
            rep += "Nombre de correspondances : " + nombreCorrespondances + "\r\n";
            Console.WriteLine("Temps total estimé : " + tempsTotal + " minutes");
            this.tempsTotal = tempsTotal;
            return rep;
        }
    }
} 