using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
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
        /// recupere ou modifie lidentifiant du noeud
        /// </summary>
        public T Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// recupere ou modifie la liste des voisins du noeud
        /// </summary>
        public List<Noeud<T>> Voisins
        {
            get { return voisins; }
            set { voisins = value; }
        }

        /// <summary>
        /// recupere ou modifie le nom de la station
        /// </summary>
        public string NomStation
        {
            get { return nomStation; }
            set { nomStation = value; }
        }

        /// <summary>
        /// recupere ou modifie la longitude de la station
        /// </summary>
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        /// <summary>
        /// recupere ou modifie la latitude de la station
        /// </summary>
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        /// <summary>
        /// recupere ou modifie le numero de la ligne
        /// </summary>
        public string NumeroLigne
        {
            get { return numeroLigne; }
            set { numeroLigne = value; }
        }

        /// <summary>
        /// recupere ou modifie la couleur de la ligne
        /// </summary>
        public string CouleurLigne
        {
            get { return couleurLigne; }
            set { couleurLigne = value; }
        }

        /// <summary>
        /// recupere ou modifie le temps de correspondance de la station
        /// </summary>
        public int TempsCorrespondance
        {
            get { return tempsCorrespondance; }
            set { tempsCorrespondance = value; }
        }

        /// <summary>
        /// cree un nouveau noeud avec un identifiant
        /// </summary>
        public Noeud(T id)
        {
            this.id = id;
            Voisins = new List<Noeud<T>>();
            tempsCorrespondance = 0;
        }

        /// <summary>
        /// cree un nouveau noeud metro
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
        /// ajoute un voisin a la liste des voisins du noeud
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
