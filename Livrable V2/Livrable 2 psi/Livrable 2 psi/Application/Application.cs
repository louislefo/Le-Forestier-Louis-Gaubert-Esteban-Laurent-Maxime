using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;

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

        /// constructeur par defaut
        public Application()
        {
            try
            {
                connexionBDD = new ConnexionBDD();
                authentification = new Authentification(connexionBDD);
                

            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de la connexion a la base de donnees : " + e.Message);
            }

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
            applicationEnCours = true;
            while (applicationEnCours)
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
                        if (authentification.Qui() == 1)
                        {
                            AffichageClient affichageclient = new AffichageClient(connexionBDD, authentification, grapheMetro);
                            affichageclient.AfficherMenuClient(authentification.nomUtilisateur);
                        }
                        else
                        {
                            AffichageCuisinier affichagecuisinier = new AffichageCuisinier(connexionBDD, authentification, grapheMetro);
                            affichagecuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                        }
                        Console.Clear();
                        break;
                    case "2":
                        authentification.SInscrire();
                        if (authentification.Qui() == 1)
                        {
                            AffichageClient affichageclient = new AffichageClient(connexionBDD, authentification, grapheMetro);
                            affichageclient.AfficherMenuClient(authentification.nomUtilisateur);
                        }
                        else
                        {
                            AffichageCuisinier affichagecuisinier = new AffichageCuisinier(connexionBDD, authentification, grapheMetro);
                            affichagecuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                        }
                        Console.Clear();
                        break;
                    case "3":
                        MenuModules menuModules = new MenuModules(connexionBDD,grapheMetro);
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
    }
}
