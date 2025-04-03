using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;

namespace TestProject1
{
    public class GestionnaireItineraireTests
    {
        private Graphe<int> CreerGrapheTest()
        {
            Graphe<int> graphe = new Graphe<int>();

            graphe.AjouterLien(1, 2, 2.0);
            graphe.AjouterLien(2, 3, 3.0);
            graphe.AjouterLien(3, 4, 2.0);

            graphe.Noeuds[1].NomStation = "Chatelet";
            graphe.Noeuds[2].NomStation = "Bastille";
            graphe.Noeuds[3].NomStation = "Nation";
            graphe.Noeuds[4].NomStation = "Vincennes";

            graphe.Noeuds[1].NumeroLigne = "1";
            graphe.Noeuds[2].NumeroLigne = "1";
            graphe.Noeuds[3].NumeroLigne = "1";
            graphe.Noeuds[4].NumeroLigne = "1";

            return graphe;
        }

        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            var graphe = CreerGrapheTest();
            var gestionnaire = new GestionnaireItineraire<int>(graphe);

            Assert.NotNull(gestionnaire);
        }

        [Fact]
        public void RechercherItineraire_TrouveChemin()
        {
            var graphe = CreerGrapheTest();
            var gestionnaire = new GestionnaireItineraire<int>(graphe);

            var chemin = gestionnaire.RechercherItineraire("1", "4");

            Assert.NotNull(chemin);
            Assert.Equal(4, chemin.Count);
            Assert.Equal("Chatelet", chemin[0].NomStation);
            Assert.Equal("Vincennes", chemin[3].NomStation);
        }

        [Fact]
        public void RechercherItineraire_StationInexistante()
        {
            var graphe = CreerGrapheTest();
            var gestionnaire = new GestionnaireItineraire<int>(graphe);

            var chemin = gestionnaire.RechercherItineraire("1", "99");

            Assert.Empty(chemin);
        }

        [Fact]
        public void RechercherItineraire_AvecCorrespondance()
        {
            var graphe = CreerGrapheTest();
            graphe.Noeuds[3].NumeroLigne = "2";
            graphe.Noeuds[4].NumeroLigne = "2";
            graphe.Noeuds[3].TempsCorrespondance = 5;

            var gestionnaire = new GestionnaireItineraire<int>(graphe);

            var chemin = gestionnaire.RechercherItineraire("1", "4");

            Assert.NotNull(chemin);
            Assert.Equal(4, chemin.Count);
            Assert.Equal("1", chemin[0].NumeroLigne);
            Assert.Equal("2", chemin[3].NumeroLigne);
        }
    }
} 