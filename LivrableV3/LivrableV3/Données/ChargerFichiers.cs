using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace LivrableV3
{
   
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
                
                using (StreamReader sr = new StreamReader(cheminFichierMetro))
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

                            Noeud<int> noeudMetro = new Noeud<int>(id, nomStation, longitude, latitude, numeroLigne, couleurLigne);
                            noeudsMetro[id] = noeudMetro;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la lecture du fichier des noeuds : " + e.Message);
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
                using (StreamReader sr = new StreamReader(cheminFichierArcs))
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
                            double poids = double.Parse(colonnes[4]); 
                            int tempsCorrespondance = int.Parse(colonnes[5]);

                            // Met à jour le temps de correspondance de la station
                            if (grapheMetro.Noeuds.ContainsKey(idStation))
                            {
                                grapheMetro.Noeuds[idStation].TempsCorrespondance = tempsCorrespondance;
                            }

                            // Ajoute le lien avec la station précédente si elle existe
                            if (idPrecedent != 0)
                            {
                                grapheMetro.AjouterLien(idStation, idPrecedent, poids);
                            }

                            // Ajoute le lien avec la station suivante si elle existe
                            if (idSuivant != 0)
                            {
                                grapheMetro.AjouterLien(idStation, idSuivant, poids);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la lecture du fichier des arcs : " + e.Message);
            }
        }
    }
}
