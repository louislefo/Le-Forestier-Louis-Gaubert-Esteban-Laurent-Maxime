using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
    /// <summary>
    /// cette classe represente une station de metro dans le graphe
    /// elle stocke toutes les informations sur la station comme son nom et sa position
    /// elle garde aussi la liste des stations directement connectees
    /// elle est utilisee pour representer chaque station du metro de paris
    /// </summary>
    public class Noeud<T>
    {
        // Champs privés pour stocker les données
        private T id;
        private List<Noeud<T>> voisins;
        private string nomStation;
        private double longitude;
        private double latitude;
        private string numeroLigne;
        private string couleurLigne;
        private int tempsCorrespondance;
        public string Nom; // pour la coloration 

        /// <summary>
        /// recupere ou modifie le numero de la station
        /// ce numero sert a identifier la station dans le metro
        /// il est unique pour chaque station
        /// </summary>
        public T Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// recupere ou modifie la liste des stations connectees
        /// ces stations sont directement accessibles depuis cette station
        /// on peut aller dans les deux sens entre ces stations
        /// </summary>
        public List<Noeud<T>> Voisins
        {
            get { return voisins; }
            set { voisins = value; }
        }

        /// <summary>
        /// recupere ou modifie le nom de la station
        /// ce nom est affiche sur les plans du metro
        /// il permet aux voyageurs d'identifier la station
        /// </summary>
        public string NomStation
        {
            get { return nomStation; }
            set { nomStation = value; }
        }

        /// <summary>
        /// recupere ou modifie la longitude de la station
        /// cette valeur sert a positionner la station sur la carte
        /// elle est en degres decimaux
        /// </summary>
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        /// <summary>
        /// recupere ou modifie la latitude de la station
        /// cette valeur sert a positionner la station sur la carte
        /// elle est en degres decimaux
        /// </summary>
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        /// <summary>
        /// recupere ou modifie le numero de la ligne de metro
        /// ce numero indique a quelle ligne appartient la station
        /// il peut y avoir plusieurs lignes a une meme station
        /// </summary>
        public string NumeroLigne
        {
            get { return numeroLigne; }
            set { numeroLigne = value; }
        }

        /// <summary>
        /// recupere ou modifie la couleur de la ligne de metro
        /// cette couleur est utilisee pour afficher la ligne sur les plans
        /// elle aide a identifier facilement la ligne
        /// </summary>
        public string CouleurLigne
        {
            get { return couleurLigne; }
            set { couleurLigne = value; }
        }

        /// <summary>
        /// recupere ou modifie le temps de correspondance de la station
        /// ce temps est en minutes et represente le temps pour changer de ligne
        /// il est plus long dans les grandes stations avec beaucoup de correspondances
        /// </summary>
        public int TempsCorrespondance
        {
            get { return tempsCorrespondance; }
            set { tempsCorrespondance = value; }
        }

        /// <summary>
        /// cree une nouvelle station avec un numero donne
        /// initialise la liste des stations connectees vide
        /// le temps de correspondance est mis a zero par defaut
        /// </summary>
        public Noeud(T id)
        {
            this.id = id;
            Voisins = new List<Noeud<T>>();
            tempsCorrespondance = 0;
        }

        /// <summary>
        /// cree une nouvelle station avec toutes ses informations
        /// initialise le nom, la position, la ligne et sa couleur
        /// la liste des stations connectees est vide au debut
        /// </summary>
        public Noeud(T id, string nomStation, double longitude, double latitude, string numeroLigne, string couleurLigne)
        {
            this.id = id;
            this.nomStation = nomStation;
            this.longitude = longitude;
            this.latitude = latitude;
            this.numeroLigne = numeroLigne;
            this.couleurLigne = couleurLigne;
            Voisins = new List<Noeud<T>>();
            tempsCorrespondance = 0;
        }

        /// <summary>
        /// ajoute une station a la liste des stations connectees
        /// ajoute aussi cette station comme connectee a l'autre station
        /// car on peut aller dans les deux sens dans le metro
        /// </summary>
        public void AjouterVoisin(Noeud<T> voisin)
        {
            if (!Voisins.Contains(voisin))
            {
                Voisins.Add(voisin);
                voisin.Voisins.Add(this);
            }
        }
    }
}
