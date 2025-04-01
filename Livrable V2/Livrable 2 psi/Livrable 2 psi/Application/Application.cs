using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    public class Application
    {
        public Authentification authentification;
        public AffichageCuisinier affichageCuisinier;
        public AffichageClient affichageClient;
        public ConnexionBDD connexionBDD;
        public bool applicationEnCours;

        private Graphe<int> grapheMetro;
        private string cheminFichierMetro = @"../../../Données/MetroParisNoeuds.csv";
        private string cheminFichierArcs = @"../../../Données/MetroParisArcs.csv";
        private ChargerFichiers chargeur;
        private GestionnaireItineraire<int> gestionnaire;

        
        /// constructeur par defaut
        public Application()
        {
            try
            {
                connexionBDD = new ConnexionBDD();
                authentification = new Authentification(connexionBDD);
                affichageCuisinier = new AffichageCuisinier(connexionBDD);
                affichageClient = new AffichageClient(connexionBDD);


                // partage l'instance d'authentification
                affichageCuisinier.authentification = authentification;
                affichageClient.authentification = authentification;

                // partage l'etat de l'application
                applicationEnCours = true;
                affichageCuisinier.applicationEnCours = true;
                affichageClient.applicationEnCours = true;

                // graphe
                grapheMetro = new Graphe<int>();
                chargeur = new ChargerFichiers();
                ChargerDonneesMetro();
                gestionnaire = new GestionnaireItineraire<int>(grapheMetro);
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de la connexion a la base de donnees : " + e.Message);
                Environment.Exit(1);
            }
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
            while (applicationEnCours)
            {
                if (!authentification.estConnecte)
                {
                    Console.WriteLine("\n=== Bienvenue sur Liv'In Paris ===");
                    Console.WriteLine("1. Se connecter");
                    Console.WriteLine("2. S'inscrire");
                    Console.WriteLine("3. Modules");
                    Console.WriteLine("4. Quitter");
                    Console.WriteLine("Choix : ");

                    string choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            authentification.SeConnecter();
                            Console.Clear();
                            break;
                        case "2":
                            authentification.SInscrire();
                            Console.Clear();
                            break;
                        case "3":
                            MenuModules menuModules = new MenuModules(connexionBDD,grapheMetro);
                            Console.Clear();
                            menuModules.AfficherMenuModules();
                            
                            break;

                        case "4":
                            applicationEnCours = false;
                            affichageCuisinier.applicationEnCours = false;
                            affichageClient.applicationEnCours = false;
                            break;
                        default:
                            Console.WriteLine("Choix invalide");
                            break;
                    }
                }
                else
                {
                    if (authentification.estCuisinier)
                    {
                        affichageCuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                    }
                    else
                    {
                        affichageClient.AfficherMenuClient(authentification.nomUtilisateur);
                    }
                }
            }

            // ferme la connexion a la base de donnees quand on quitte l'application
            if (connexionBDD != null)
            {
                connexionBDD.FermerConnexion();
            }
        }
    }
}
