using System;
using Xunit;
using PSI_livrable1;

namespace PSI_livrable1.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe Lien
    /// </summary>
    public class LienTests
    {
        /// <summary>
        /// Vérifie que le constructeur initialise correctement les noeuds
        /// </summary>
        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            // Arrangement
            var noeud1 = new Noeud(1);
            var noeud2 = new Noeud(2);

            // Action
            var lien = new Lien(noeud1, noeud2);

            // Assert
            Assert.Equal(noeud1, lien.Noeud1);
            Assert.Equal(noeud2, lien.Noeud2);
        }

        /// <summary>
        /// Vérifie que les propriétés peuvent être modifiées
        /// </summary>
        [Fact]
        public void Proprietes_PeuventEtreModifiees()
        {
            // Arrangement
            var noeud1 = new Noeud(1);
            var noeud2 = new Noeud(2);
            var noeud3 = new Noeud(3);
            var noeud4 = new Noeud(4);
            var lien = new Lien(noeud1, noeud2);

            // Action
            lien.Noeud1 = noeud3;
            lien.Noeud2 = noeud4;

            // Assert
            Assert.Equal(noeud3, lien.Noeud1);
            Assert.Equal(noeud4, lien.Noeud2);
        }
    }
}