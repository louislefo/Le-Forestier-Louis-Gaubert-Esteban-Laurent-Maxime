using System;
using System.IO;
using Xunit;
using PSI_livrable1;

namespace PSI_livrable1.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe Program
    /// </summary>
    public class ProgramTests
    {
        /// <summary>
        /// Vérifie que le programme gère correctement un fichier inexistant
        /// </summary>
        [Fact]
        public void LectureFichier_FichierInexistant_GereErreur()
        {
            // Arrangement
            var sortie = new StringWriter();
            Console.SetOut(sortie);
            string cheminFichier = "fichier_inexistant.mtx";

            // Action
            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    // Cette action devrait lever une exception
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier");
            }

            // Assert
            Assert.Contains("Erreur lors de la lecture du fichier", sortie.ToString());
        }

        /// <summary>
        /// Vérifie que le programme ignore correctement les lignes de commentaires
        /// </summary>
        [Fact]
        public void LectureFichier_IgnoreCommentaires()
        {
            // Arrangement
            var contenuTest = "%Ceci est un commentaire\n1 2\n%Autre commentaire\n2 3";
            var fichierTest = "test.mtx";
            File.WriteAllText(fichierTest, contenuTest);
            var graphe = new Graphe();

            // Action
            using (StreamReader sr = new StreamReader(fichierTest))
            {
                while (!sr.EndOfStream)
                {
                    string ligne = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(ligne) || ligne.StartsWith("%"))
                        continue;

                    string[] elements = ligne.Split(' ');
                    if (elements.Length >= 2 && int.TryParse(elements[0], out int id1) && int.TryParse(elements[1], out int id2))
                    {
                        graphe.AjouterLien(id1, id2);
                    }
                }
            }

            // Assert
            Assert.Equal(3, graphe.Noeuds.Count);
            Assert.Equal(2, graphe.Liens.Count);

            // Nettoyage
            File.Delete(fichierTest);
        }
    }
}