using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using PSI_livrable1;

namespace PSI_livrable1.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe Graphe
    /// </summary>
    public class GrapheTests
    {
        /// <summary>
        /// Vérifie que le constructeur initialise correctement les collections
        /// </summary>
        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            var graphe = new Graphe();

            Assert.NotNull(graphe.Noeuds);
            Assert.NotNull(graphe.Liens);
            Assert.Empty(graphe.Noeuds);
            Assert.Empty(graphe.Liens);
        }

        /// <summary>
        /// Vérifie que la méthode AjouterLien crée correctement un lien entre deux noeuds
        /// </summary>
        [Fact]
        public void AjouterLien_CreeLienCorrectement()
        {
            var graphe = new Graphe();

            graphe.AjouterLien(1, 2);

            Assert.Equal(2, graphe.Noeuds.Count);
            Assert.Single(graphe.Liens);
            Assert.Contains(1, graphe.Noeuds.Keys);
            Assert.Contains(2, graphe.Noeuds.Keys);
            Assert.Contains(graphe.Noeuds[2], graphe.Noeuds[1].Voisins);
            Assert.Contains(graphe.Noeuds[1], graphe.Noeuds[2].Voisins);
        }

        /// <summary>
        /// Vérifie que la méthode AjouterLien évite les doublons
        /// </summary>
        [Fact]
        public void AjouterLien_EviteDoublons()
        {
            var graphe = new Graphe();

            graphe.AjouterLien(1, 2);
            graphe.AjouterLien(1, 2);

            Assert.Equal(2, graphe.Noeuds.Count);
            Assert.Single(graphe.Liens);
        }

        /// <summary>
        /// Vérifie que la méthode ObtenirPremierNoeud retourne null pour un graphe vide
        /// </summary>
        [Fact]
        public void ObtenirPremierNoeud_GrapheVide_RetourneNull()
        {
            var graphe = new Graphe();

            var resultat = graphe.ObtenirPremierNoeud();

            Assert.Null(resultat);
        }

        /// <summary>
        /// Vérifie que la méthode ObtenirPremierNoeud retourne le premier noeud pour un graphe non vide
        /// </summary>
        [Fact]
        public void ObtenirPremierNoeud_GrapheNonVide_RetournePremierNoeud()
        {
            var graphe = new Graphe();
            graphe.AjouterLien(1, 2);

            var resultat = graphe.ObtenirPremierNoeud();

            Assert.NotNull(resultat);
            Assert.Equal(1, resultat.Id);
        }

        /// <summary>
        /// Vérifie que la méthode largeur effectue correctement le parcours en largeur
        /// </summary>
        [Fact]
        public void Largeur_ParcourCorrectement()
        {
            var graphe = new Graphe();
            graphe.AjouterLien(1, 2);
            graphe.AjouterLien(1, 3);
            graphe.AjouterLien(2, 4);
            var sortie = new StringWriter();
            Console.SetOut(sortie);

            graphe.largeur(graphe.Noeuds[1]);

            var resultat = sortie.ToString().Trim();
            Assert.Contains("1", resultat);
            Assert.Contains("2", resultat);
            Assert.Contains("3", resultat);
            Assert.Contains("4", resultat);
        }

        /// <summary>
        /// Vérifie que la méthode Profondeur effectue correctement le parcours en profondeur
        /// </summary>
        [Fact]
        public void Profondeur_ParcourCorrectement()
        {
            var graphe = new Graphe();
            graphe.AjouterLien(1, 2);
            graphe.AjouterLien(1, 3);
            graphe.AjouterLien(2, 4);
            var sortie = new StringWriter();
            Console.SetOut(sortie);
            var visites = new List<int>();

            graphe.Profondeur(graphe.Noeuds[1], visites);

            var resultat = sortie.ToString().Trim();
            Assert.Contains("1", resultat);
            Assert.Contains("2", resultat);
            Assert.Contains("3", resultat);
            Assert.Contains("4", resultat);
        }
    }
}