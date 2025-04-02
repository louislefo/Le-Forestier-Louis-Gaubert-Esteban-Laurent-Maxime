using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;
using System.Data;

namespace Livrable_2_psi
{
    public class Application
    {
        public Authentification authentification;
        public ConnexionBDD connexionBDD;
        public bool applicationEnCours;

        private Graphe<int> grapheMetro;
        private string cheminFichierMetro = @"../../../Données/MetroParisNoeuds.csv";
        private string cheminFichierArcs = @"../../../Données/MetroParisArcs.csv";
        private ChargerFichiers chargeur;
        private GestionnaireItineraire<int> gestionnaire;
        public MenuModules menuModules;

        /// constructeur par defaut
        public Application()
        {
            connexionBDD = new ConnexionBDD();
            authentification = new Authentification(connexionBDD);
            menuModules = new MenuModules(connexionBDD, grapheMetro);

            // graphe
            grapheMetro = new Graphe<int>();
            chargeur = new ChargerFichiers();
            ChargerDonneesMetro();
            gestionnaire = new GestionnaireItineraire<int>(grapheMetro);
        }

        private void ChargerDonneesMetro()
        {
            // chargement des noeuds
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);
            Console.WriteLine("Nombre de noeuds charges : " + noeudsMetro.Count);

            // ajout des noeuds au graphe
            foreach (int id in noeudsMetro.Keys)
            {
                grapheMetro.Noeuds[id] = noeudsMetro[id];
            }

            // chargement des arcs
            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);
            Console.WriteLine("Nombre de liens dans le graphe : " + grapheMetro.Liens.Count);
        }

        /// methode pour demarrer l'application
        public void Demarrer()
        {
            try
            {
                connexionBDD.TestConnexion();
                Console.WriteLine("connexion a la base de donnees reussie");

                bool continuer = true;
                while (continuer)
                {
                    Console.WriteLine("\n=== Menu Principal ===");
                    Console.WriteLine("1. Se connecter");
                    Console.WriteLine("2. S'inscrire");
                    Console.WriteLine("3. Quitter");
                    Console.Write("Choix : ");

                    string choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            if (authentification.SeConnecter())
                            {
                                menuModules.AfficherMenuModules();
                            }
                            break;
                        case "2":
                            if (authentification.SInscrire())
                            {
                                Console.WriteLine("inscription reussie, vous pouvez maintenant vous connecter");
                            }
                            break;
                        case "3":
                            continuer = false;
                            break;
                        default:
                            Console.WriteLine("choix invalide");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors du demarrage de l'application : " + ex.Message);
            }
            finally
            {
                connexionBDD.FermerConnexion();
            }
        }
    }
}
