using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui charge les fichiers du metro
    /// </summary>
    public class ChargerFichiers
    {
        /// <summary>
        /// charge les noeuds du metro
        /// </summary>
        public Dictionary<int, Noeud<int>> ChargerNoeudsMetro(string cheminFichierMetro)
        {
            Dictionary<int, Noeud<int>> noeudsMetro = new Dictionary<int, Noeud<int>>();

            try
            {
                using (StreamReader sr = new StreamReader(cheminFichierMetro, System.Text.Encoding.UTF8))
                {
                    string ligneEnTete = sr.ReadLine();

                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        string[] colonnes = ligne.Split(';');
                        if (colonnes.Length >= 8)
                        {
                            int id = int.Parse(colonnes[0]);
                            string numeroLigne = colonnes[1];
                            string nomStation = colonnes[2];
                            double longitude = double.Parse(colonnes[3], CultureInfo.InvariantCulture);
                            double latitude = double.Parse(colonnes[4], CultureInfo.InvariantCulture);
                            string couleurLigne = colonnes[7];

                            var noeudMetro = new Noeud<int>(id, nomStation, longitude, latitude, numeroLigne, couleurLigne);
                            noeudsMetro[id] = noeudMetro;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier des noeuds : " + e.Message);
            }

            return noeudsMetro;
        }

        /// <summary>
        /// charge les arcs du metro
        /// </summary>
        public void ChargerArcsMetro(Graphe<int> grapheMetro, string cheminFichierArcs)
        {
            try
            {
                using (StreamReader sr = new StreamReader(cheminFichierArcs, System.Text.Encoding.UTF8))
                {
                    string ligneEnTete = sr.ReadLine();

                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        string[] colonnes = ligne.Split(';');
                        if (colonnes.Length >= 7)
                        {
                            int idStation = int.Parse(colonnes[0]);
                            int idPrecedent = int.Parse(colonnes[2]);
                            int idSuivant = int.Parse(colonnes[3]);

                            // Ajoute le lien avec la station précédente si elle existe
                            if (idPrecedent != 0)
                            {
                                grapheMetro.AjouterLien(idStation, idPrecedent);
                            }

                            // Ajoute le lien avec la station suivante si elle existe
                            if (idSuivant != 0)
                            {
                                grapheMetro.AjouterLien(idStation, idSuivant);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier des arcs : " + e.Message);
            }
        }
    }
}
