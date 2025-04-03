using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;

namespace TestProject1
{
    public class NoeudsTests
    {
        [Fact]
        public void Constructeur_Simple_InitialiseCorrectement()
        {
            int id = 1;
            Noeud<int> noeud = new Noeud<int>(id);

            Assert.Equal(id, noeud.Id);
            Assert.Empty(noeud.Voisins);
            Assert.Equal(0, noeud.TempsCorrespondance);
        }

        [Fact]
        public void Constructeur_Metro_InitialiseCorrectement()
        {
            int id = 1;
            string nomStation = "Chatelet";
            double longitude = 2.3488;
            double latitude = 48.8589;
            string numeroLigne = "1";
            string couleurLigne = "#FFCD00";

            Noeud<int> noeud = new Noeud<int>(id, nomStation, longitude, latitude, numeroLigne, couleurLigne);

            Assert.Equal(id, noeud.Id);
            Assert.Equal(nomStation, noeud.NomStation);
            Assert.Equal(longitude, noeud.Longitude);
            Assert.Equal(latitude, noeud.Latitude);
            Assert.Equal(numeroLigne, noeud.NumeroLigne);
            Assert.Equal(couleurLigne, noeud.CouleurLigne);
            Assert.Empty(noeud.Voisins);
            Assert.Equal(0, noeud.TempsCorrespondance);
        }

        [Fact]
        public void AjouterVoisin_AjouteCorrectement()
        {
            Noeud<int> noeud1 = new Noeud<int>(1);
            Noeud<int> noeud2 = new Noeud<int>(2);

            noeud1.AjouterVoisin(noeud2);

            Assert.Single(noeud1.Voisins);
            Assert.Single(noeud2.Voisins);
            Assert.Contains(noeud2, noeud1.Voisins);
            Assert.Contains(noeud1, noeud2.Voisins);
        }

        [Fact]
        public void AjouterVoisin_NeDoitPasAjouterDeuxFois()
        {
            Noeud<int> noeud1 = new Noeud<int>(1);
            Noeud<int> noeud2 = new Noeud<int>(2);

            noeud1.AjouterVoisin(noeud2);
            noeud1.AjouterVoisin(noeud2);

            Assert.Single(noeud1.Voisins);
            Assert.Single(noeud2.Voisins);
        }
    }
} 