using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_livrable1
{
    /// <summary>
    /// Représente une connexion bidirectionnelle entre deux nœuds dans un graphe non orienté.
    /// </summary>
    public class Lien
    {
        // Champs privés pour stocker les données
        private Noeud noeud1;
        private Noeud noeud2;

        /// <summary>
        /// Obtient ou définit le premier nœud de la connexion.
        /// </summary>
        public Noeud Noeud1
        {
            get { return noeud1; }
            set { noeud1 = value; }
        }

        /// <summary>
        /// Obtient ou définit le deuxième nœud de la connexion.
        /// </summary>
        public Noeud Noeud2
        {
            get { return noeud2; }
            set { noeud2 = value; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Lien avec deux nœuds spécifiés.
        /// </summary>
        /// <param name="n1">Le premier nœud de la connexion.</param>
        /// <param name="n2">Le deuxième nœud de la connexion.</param>
        public Lien(Noeud n1, Noeud n2)
        {
            Noeud1 = n1;
            Noeud2 = n2;
        }
    }
}
