using System;
using System.Collections.Generic;

namespace LivrableV3
{
    public class TestColorationClients
    {
        /// teste la coloration de graphe avec des clients et des cuisiniers
        public static void TesterColorationClients()
        {
            // on cree un graphe de test
            Graphe<string> grapheTest = new Graphe<string>();

            // on ajoute des clients
            grapheTest.AjouterLien("Client1", "Cuisinier1", 1);
            grapheTest.AjouterLien("Client1", "Cuisinier2", 1);
            grapheTest.AjouterLien("Client2", "Cuisinier1", 1);
            grapheTest.AjouterLien("Client2", "Cuisinier3", 1);
            grapheTest.AjouterLien("Client3", "Cuisinier2", 1);
            grapheTest.AjouterLien("Client3", "Cuisinier3", 1);
            grapheTest.AjouterLien("Client4", "Cuisinier1", 1);
            grapheTest.AjouterLien("Client4", "Cuisinier2", 1);

            // on cree l'objet de coloration
            ColorationGraphe<string> coloration = new ColorationGraphe<string>();

            // on applique l'algorithme de welsh powell
            coloration.AppliquerWelshPowell(grapheTest);

            // on affiche les resultats
            coloration.AfficherResultats();

            Console.WriteLine();
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }
} 