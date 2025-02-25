using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_livrable1
{
    public class Noeud
    {
        public int Id { get; set; }
        public List<Noeud> Voisins { get; set; }

        public Noeud(int id)
        {
            Id = id;
            Voisins = new List<Noeud>();
        }

        public void AjouterVoisin(Noeud voisin)
        {
            if (!Voisins.Contains(voisin))
            {
                Voisins.Add(voisin);
                voisin.Voisins.Add(this);  // Relation bidirectionnelle
            }
        }
    }
}
