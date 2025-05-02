using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrableV3
{
    /// <summary>
    /// cette classe gère les connexions entre les stations
    /// elle stocke les deux stations et le temps de trajet
    /// </summary>
    public class Lien<T>
    {
        private Noeud<T> noeud1;
        private Noeud<T> noeud2;
        private double poids;


        public Noeud<T> Noeud1
        {
            get { return noeud1; }
            set { noeud1 = value; }
        }

        public Noeud<T> Noeud2
        {
            get { return noeud2; }
            set { noeud2 = value; }
        }


        public double Poids
        {
            get { return poids; }
            set { poids = value; }
        }


        public Lien(Noeud<T> n1, Noeud<T> n2, double poids = 1.0)
        {
            Noeud1 = n1;
            Noeud2 = n2;
            Poids = poids;
        }
    }
}
