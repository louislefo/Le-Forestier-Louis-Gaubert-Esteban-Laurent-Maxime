using System;
using System.Collections.Generic;

namespace LivrableV3
{
    public class TestColoration
    {
        /// teste la coloration de graphe avec welsh powell
        public static void TesterColorationGraphe()
        {
            // on cree un graphe de test
            Graphe<int> grapheTest = new Graphe<int>();

            // on ajoute des liens pour creer un graphe de test
            grapheTest.AjouterLien(1, 2, 1);
            grapheTest.AjouterLien(1, 3, 1);
            grapheTest.AjouterLien(2, 3, 1);
            grapheTest.AjouterLien(2, 4, 1);
            grapheTest.AjouterLien(3, 4, 1);
            grapheTest.AjouterLien(4, 5, 1);
            grapheTest.AjouterLien(5, 1, 1);

            // on cree l'objet de coloration
            ColorationGraphe<int> coloration = new ColorationGraphe<int>();

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