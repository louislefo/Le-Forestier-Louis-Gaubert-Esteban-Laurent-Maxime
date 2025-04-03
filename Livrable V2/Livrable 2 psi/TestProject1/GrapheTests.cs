using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;

namespace TestProject1
{
    public class GrapheTests
    {
        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            Graphe<int> graphe = new Graphe<int>();

            Assert.NotNull(graphe.Noeuds);
            Assert.NotNull(graphe.Liens);
            Assert.Empty(graphe.Noeuds);
            Assert.Empty(graphe.Liens);
        }

        [Fact]
        public void AjouterLien_AjouteCorrectement()
        {
            Graphe<int> graphe = new Graphe<int>();
            int id1 = 1;
            int id2 = 2;
            double poids = 5.5;

            graphe.AjouterLien(id1, id2, poids);

            Assert.Equal(2, graphe.Noeuds.Count);
            Assert.Single(graphe.Liens);
            Assert.Contains(id1, graphe.Noeuds.Keys);
            Assert.Contains(id2, graphe.Noeuds.Keys);
        }

        [Fact]
        public void AjouterLien_AvecNoeudsExistants()
        {
            Graphe<int> graphe = new Graphe<int>();
            int id1 = 1;
            int id2 = 2;
            int id3 = 3;

            graphe.AjouterLien(id1, id2, 5.5);
            graphe.AjouterLien(id2, id3, 3.0);

            Assert.Equal(3, graphe.Noeuds.Count);
            Assert.Equal(2, graphe.Liens.Count);
        }

        [Fact]
        public void ObtenirPremierNoeud_GrapheVide()
        {
            Graphe<int> graphe = new Graphe<int>();

            var premierNoeud = graphe.ObtenirPremierNoeud();

            Assert.Null(premierNoeud);
        }

        [Fact]
        public void ObtenirPremierNoeud_GrapheNonVide()
        {
            Graphe<int> graphe = new Graphe<int>();
            int id1 = 1;
            int id2 = 2;

            graphe.AjouterLien(id1, id2, 5.5);

            var premierNoeud = graphe.ObtenirPremierNoeud();

            Assert.NotNull(premierNoeud);
            Assert.Contains(premierNoeud.Id, new[] { id1, id2 });
        }
    }
} 