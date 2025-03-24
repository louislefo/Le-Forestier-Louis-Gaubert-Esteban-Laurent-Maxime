using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class Noeud<T>
    {
        // Champs privés pour stocker les données
        private T id;
        private List<Noeud<T>> voisins;

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
        /// cree un nouveau noeud avec un identifiant
        /// </summary>
        public Noeud(T id)
        {
            this.id = id;
            Voisins = new List<Noeud<T>>();
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
