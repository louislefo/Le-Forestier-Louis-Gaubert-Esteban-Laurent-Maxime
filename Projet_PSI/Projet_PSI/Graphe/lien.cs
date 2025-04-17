using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI
{
    public class Lien<T>
    {
        // Champs privés pour stocker les données
        private Noeud<T> noeud1;
        private Noeud<T> noeud2;
        private double poids;

        /// <summary>
        /// recupere ou modifie le premier noeud du lien
        /// </summary>
        public Noeud<T> Noeud1
        {
            get { return noeud1; }
            set { noeud1 = value; }
        }

        /// <summary>
        /// recupere ou modifie le deuxieme noeud du lien
        /// </summary>
        public Noeud<T> Noeud2
        {
            get { return noeud2; }
            set { noeud2 = value; }
        }

        /// <summary>
        /// recupere ou modifie le poids du lien
        /// </summary>
        public double Poids
        {
            get { return poids; }
            set { poids = value; }
        }

        /// <summary>
        /// cree un nouveau lien entre deux noeuds
        /// </summary>
        public Lien(Noeud<T> n1, Noeud<T> n2, double poids = 1.0)
        {
            Noeud1 = n1;
            Noeud2 = n2;
            Poids = poids;
        }
    }
}
