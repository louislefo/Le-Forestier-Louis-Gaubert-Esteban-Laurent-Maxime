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

        /// <summary>
        /// Creation de l'application
        /// Ouverture de la connectionBDD en mode root
        /// creation du graphe
        /// </summary>
        public Application()
        {
            connexionBDD = new ConnexionBDD();

            
            grapheMetro = new Graphe<int>();
            chargeur = new ChargerFichiers();
            ChargerDonneesMetro();
            gestionnaire = new GestionnaireItineraire<int>(grapheMetro);


            authentification = new Authentification(connexionBDD,grapheMetro);
            menuModules = new MenuModules(connexionBDD, grapheMetro);

            
        }

        private void ChargerDonneesMetro()
        {
            
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);
            Console.WriteLine("Nombre de noeuds charges : " + noeudsMetro.Count);

            
            foreach (int id in noeudsMetro.Keys)
            {
                grapheMetro.Noeuds[id] = noeudsMetro[id];
            }

            
            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);
            Console.WriteLine("Nombre de liens dans le graphe : " + grapheMetro.Liens.Count);
        }

        /// <summary>
        /// demarage de l'application et affichage du menu principal
        /// </summary>
        public void Demarrer()
        {
            try
            {
                

                bool continuer = true;
                while (continuer)
                {
                    Console.WriteLine("\n=== Menu Principal ===");
                    Console.WriteLine("1. Se connecter");
                    Console.WriteLine("2. S'inscrire");
                    Console.WriteLine("3. Modules");
                    Console.WriteLine("4. Quitter");
                    Console.Write("Choix : ");

                    string choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            authentification.SeConnecter();
                            if (authentification.Qui() == 1)
                            {
                                ConnexionBDDClient connexionBDDClient = new ConnexionBDDClient(authentification.nomUtilisateur, authentification.motDePasse);
                                AffichageClient affichageclient = new AffichageClient(connexionBDDClient, authentification, grapheMetro);
                                affichageclient.AfficherMenuClient(authentification.nomUtilisateur);
                            }
                            else
                            {
                                ConnexionBDDCuisinier connexionBDDCuisinier = new ConnexionBDDCuisinier(authentification.nomUtilisateur, authentification.motDePasse);
                                AffichageCuisinier affichagecuisinier = new AffichageCuisinier(connexionBDDCuisinier, authentification, grapheMetro);
                                affichagecuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                            }
                            Console.Clear();
                            break;
                        case "2":
                            authentification.SInscrire();
                            if (authentification.Qui() == 1)
                            {
                                ConnexionBDDClient connexionBDDClient = new ConnexionBDDClient(authentification.nomUtilisateur, authentification.motDePasse);
                                AffichageClient affichageclient = new AffichageClient(connexionBDDClient, authentification, grapheMetro);
                                affichageclient.AfficherMenuClient(authentification.nomUtilisateur);
                            }
                            else
                            {
                                ConnexionBDDCuisinier connexionBDDCuisinier = new ConnexionBDDCuisinier(authentification.nomUtilisateur, authentification.motDePasse);
                                AffichageCuisinier affichagecuisinier = new AffichageCuisinier(connexionBDDCuisinier, authentification, grapheMetro);
                                affichagecuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                            }
                            Console.Clear();
                            break;
                        case "3":
                            MenuModules menuModules = new MenuModules(connexionBDD, grapheMetro);
                            Console.Clear();
                            menuModules.AfficherMenuModules();
                            break;
                        case "4":
                            applicationEnCours = false;
                            connexionBDD.FermerConnexion();
                            Console.WriteLine("Au revoir ! ");
                            break;
                        default:
                            Console.WriteLine("Choix invalide");
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
