using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_livrable1
{
    public class Lien
    {
        public Noeud Noeud1 { get; set; }
        public Noeud Noeud2 { get; set; }

        public Lien(Noeud n1, Noeud n2)
        {
            Noeud1 = n1;
            Noeud2 = n2;
        }
    }
}
