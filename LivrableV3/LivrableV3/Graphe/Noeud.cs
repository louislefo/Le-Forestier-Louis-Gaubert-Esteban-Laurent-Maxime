using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
    /// <summary>
    /// cette classe gère une station de metro dans le graphe
    /// elle garde la liste des stations directement connectees
    /// </summary>
    public class Noeud<T>
    {

        private T id;
        private List<Noeud<T>> voisins;
        private string nomStation;
        private double longitude;
        private double latitude;
        private string numeroLigne;
        private string couleurLigne;
        private int tempsCorrespondance;
        public string Nom; 

        public T Id
        {
            get { return id; }
            set { id = value; }
        }


        public List<Noeud<T>> Voisins
        {
            get { return voisins; }
            set { voisins = value; }
        }

        public string NomStation
        {
            get { return nomStation; }
            set { nomStation = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }


        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public string NumeroLigne
        {
            get { return numeroLigne; }
            set { numeroLigne = value; }
        }


        public string CouleurLigne
        {
            get { return couleurLigne; }
            set { couleurLigne = value; }
        }

        public int TempsCorrespondance
        {
            get { return tempsCorrespondance; }
            set { tempsCorrespondance = value; }
        }

        public Noeud(T id)
        {
            this.id = id;
            Voisins = new List<Noeud<T>>();
            tempsCorrespondance = 0;
        }

        /// <summary>
        /// cree une nouvelle station avec toutes ses informations
        /// initialise le nom, la position, la ligne et sa couleur
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
