using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
    /// <summary>
    /// cette classe represente une connexion entre deux stations de metro
    /// elle stocke les deux stations et le temps de trajet entre elles
    /// elle est utilisee pour representer les trajets possibles dans le metro
    /// le temps de trajet est en minutes
    /// </summary>
    public class Lien<T>
    {
        // Champs privés pour stocker les données
        private Noeud<T> noeud1;
        private Noeud<T> noeud2;
        private double poids;

        /// <summary>
        /// recupere ou modifie la premiere station de la connexion
        /// c'est la station de depart du trajet
        /// elle contient toutes les informations sur la station
        /// </summary>
        public Noeud<T> Noeud1
        {
            get { return noeud1; }
            set { noeud1 = value; }
        }

        /// <summary>
        /// recupere ou modifie la deuxieme station de la connexion
        /// c'est la station d'arrivee du trajet
        /// elle contient toutes les informations sur la station
        /// </summary>
        public Noeud<T> Noeud2
        {
            get { return noeud2; }
            set { noeud2 = value; }
        }

        /// <summary>
        /// recupere ou modifie le temps de trajet entre les stations
        /// ce temps est en minutes
        /// il represente le temps moyen pour aller d'une station a l'autre
        /// </summary>
        public double Poids
        {
            get { return poids; }
            set { poids = value; }
        }

        /// <summary>
        /// cree une nouvelle connexion entre deux stations
        /// initialise le temps de trajet a 1 minute par defaut
        /// les deux stations doivent exister dans le metro
        /// </summary>
        public Lien(Noeud<T> n1, Noeud<T> n2, double poids = 1.0)
        {
            Noeud1 = n1;
            Noeud2 = n2;
            Poids = poids;
        }
    }
}
