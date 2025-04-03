using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;

namespace TestProject1
{
    public class PlusCourtCheminTests
    {
        private Graphe<int> CreerGrapheTest()
        {
            Graphe<int> graphe = new Graphe<int>();

            graphe.AjouterLien(1, 2, 2.0);
            graphe.AjouterLien(2, 3, 3.0);
            graphe.AjouterLien(1, 3, 6.0);

            graphe.Noeuds[1].NomStation = "Station1";
            graphe.Noeuds[2].NomStation = "Station2";
            graphe.Noeuds[3].NomStation = "Station3";

            graphe.Noeuds[1].NumeroLigne = "1";
            graphe.Noeuds[2].NumeroLigne = "1";
            graphe.Noeuds[3].NumeroLigne = "1";

            return graphe;
        }

        [Fact]
        public void Dijkstra_TrouveLePlusCourtChemin()
        {
            var graphe = CreerGrapheTest();
            var plusCourtChemin = new PlusCourtChemin<int>();

            var chemin = plusCourtChemin.Dijkstra(graphe, graphe.Noeuds[1], graphe.Noeuds[3]);

            Assert.Equal(3, chemin.Count);
            Assert.Equal(1, chemin[0].Id);
            Assert.Equal(2, chemin[1].Id);
            Assert.Equal(3, chemin[2].Id);
        }

        [Fact]
        public void BellmanFord_TrouveLePlusCourtChemin()
        {
            var graphe = CreerGrapheTest();
            var plusCourtChemin = new PlusCourtChemin<int>();

            var chemin = plusCourtChemin.BellmanFord(graphe, graphe.Noeuds[1], graphe.Noeuds[3]);

            Assert.Equal(3, chemin.Count);
            Assert.Equal(1, chemin[0].Id);
            Assert.Equal(2, chemin[1].Id);
            Assert.Equal(3, chemin[2].Id);
        }

        [Fact]
        public void FloydWarshall_CalculeToutesLesDistances()
        {
            var graphe = CreerGrapheTest();
            var plusCourtChemin = new PlusCourtChemin<int>();

            var distances = plusCourtChemin.FloydWarshall(graphe);

            Assert.NotNull(distances);
            Assert.NotEmpty(distances);

            var distanceDirecte = distances[(graphe.Noeuds[1], graphe.Noeuds[2])];
            Assert.Equal(2.0, distanceDirecte);

            var distanceIndirecte = distances[(graphe.Noeuds[1], graphe.Noeuds[3])];
            Assert.Equal(5.0, distanceIndirecte);
        }

        [Fact]
        public void Dijkstra_CheminImpossible()
        {
            var graphe = new Graphe<int>();
            graphe.AjouterLien(1, 2, 1.0);
            graphe.AjouterLien(3, 4, 1.0);

            graphe.Noeuds[1].NomStation = "Station1";
            graphe.Noeuds[2].NomStation = "Station2";
            graphe.Noeuds[3].NomStation = "Station3";
            graphe.Noeuds[4].NomStation = "Station4";

            var plusCourtChemin = new PlusCourtChemin<int>();

            var chemin = plusCourtChemin.Dijkstra(graphe, graphe.Noeuds[1], graphe.Noeuds[3]);

            Assert.Empty(chemin);
        }
    }
} 