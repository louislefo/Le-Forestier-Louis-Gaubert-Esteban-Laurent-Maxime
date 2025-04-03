using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;

namespace TestProject1
{
    
    public class LienTests
    {
        
        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            Noeud<int> noeud1 = new Noeud<int>(1);
            Noeud<int> noeud2 = new Noeud<int>(2);
            double poids = 5.5;

            Lien<int> lien = new Lien<int>(noeud1, noeud2, poids);

            Assert.Equal(noeud1, lien.Noeud1);
            Assert.Equal(noeud2, lien.Noeud2);
            Assert.Equal(poids, lien.Poids);
        }

        
        [Fact]
        public void Proprietes_PeuventEtreModifiees()
        {
            Noeud<int> noeud1 = new Noeud<int>(1);
            Noeud<int> noeud2 = new Noeud<int>(2);
            Noeud<int> noeud3 = new Noeud<int>(3);
            double poids = 5.5;

            Lien<int> lien = new Lien<int>(noeud1, noeud2, poids);

            lien.Noeud1 = noeud3;
            lien.Noeud2 = noeud1;
            lien.Poids = 10.0;

            Assert.Equal(noeud3, lien.Noeud1);
            Assert.Equal(noeud1, lien.Noeud2);
            Assert.Equal(10.0, lien.Poids);
        }

        [Fact]
        public void Constructeur_AvecPoidsParDefaut()
        {
            Noeud<int> noeud1 = new Noeud<int>(1);
            Noeud<int> noeud2 = new Noeud<int>(2);

            Lien<int> lien = new Lien<int>(noeud1, noeud2);

            Assert.Equal(1.0, lien.Poids);
        }
    }
}
