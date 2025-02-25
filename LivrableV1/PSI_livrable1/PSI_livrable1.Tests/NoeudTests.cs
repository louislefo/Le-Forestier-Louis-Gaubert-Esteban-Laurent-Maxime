using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PSI_livrable1;

namespace PSI_livrable1.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe Noeud
    /// </summary>
    public class NoeudTests
    {
        /// <summary>
        /// Vérifie que le constructeur initialise correctement l'ID et la liste des voisins
        /// </summary>
        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            // Arrangement
            int id = 1;

            // Action
            var noeud = new Noeud(id);

            // Assert
            Assert.Equal(id, noeud.Id);
            Assert.Empty(noeud.Voisins);
        }

        /// <summary>
        /// Vérifie que la méthode AjouterVoisin ajoute correctement un voisin
        /// </summary>
        [Fact]
        public void AjouterVoisin_AjouteCorrectement()
        {
            // Arrangement
            var noeud1 = new Noeud(1);
            var noeud2 = new Noeud(2);

            // Action
            noeud1.AjouterVoisin(noeud2);

            // Assert
            Assert.Contains(noeud2, noeud1.Voisins);
            Assert.Contains(noeud1, noeud2.Voisins);
        }

        /// <summary>
        /// Vérifie que la méthode AjouterVoisin n'ajoute pas de doublons
        /// </summary>
        [Fact]
        public void AjouterVoisin_EviteDoublons()
        {
            // Arrangement
            var noeud1 = new Noeud(1);
            var noeud2 = new Noeud(2);

            // Action
            noeud1.AjouterVoisin(noeud2);
            noeud1.AjouterVoisin(noeud2); // Tentative d'ajout en double

            // Assert
            Assert.Single(noeud1.Voisins);
            Assert.Single(noeud2.Voisins);
        }
    }
}
