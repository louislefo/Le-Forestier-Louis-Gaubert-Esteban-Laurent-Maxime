using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_livrable1
{
    /// <summary>
    /// Représente un nœud dans un graphe non orienté.
    /// Chaque nœud possède un identifiant unique et une liste de nœuds voisins.
    /// </summary>
    public class Noeud
    {
        // Champs privés pour stocker les données
        private int id;
        private List<Noeud> voisins;

        /// <summary>
        /// Obtient ou définit l'identifiant unique du nœud.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Obtient ou définit la liste des nœuds voisins connectés à ce nœud.
        /// </summary>
        public List<Noeud> Voisins
        {
            get { return voisins; }
            set { voisins = value; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Noeud avec un identifiant spécifié.
        /// </summary>
        /// <param name="id">L'identifiant unique du nœud.</param>
        public Noeud(int id)
        {
            this.id = id;
            Voisins = new List<Noeud>();
        }

        /// <summary>
        /// Ajoute un nœud voisin à la liste des voisins de ce nœud.
        /// Établit une relation bidirectionnelle entre les deux nœuds.
        /// </summary>
        /// <param name="voisin">Le nœud à ajouter comme voisin.</param>
        public void AjouterVoisin(Noeud voisin)
        {
            if (!Voisins.Contains(voisin))
            {
                Voisins.Add(voisin);
                voisin.Voisins.Add(this);  
            }
        }
    }
}
