using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class AffichageClient
    {
        public Authentification authentification;
        public bool applicationEnCours;
        public AffichageCuisinier affichageCuisinier;
        public ConnexionBDDClient connexionBDDClient;
        public Graphe<int> grapheMetro;
        public SqlClient sqlClient;

        /// constructeur par defaut
        public AffichageClient(ConnexionBDDClient connexionBDDClient,Authentification authentification, Graphe<int> grapheMetro)
        {
            this.authentification = authentification;
            this.connexionBDDClient = connexionBDDClient;
            this.grapheMetro = grapheMetro;
            applicationEnCours = true;
            sqlClient = new SqlClient(connexionBDDClient);
        }


        /// affiche le menu du client
        public void AfficherMenuClient(string nomUtilisateur)
        {
            bool continuer = true;

            while (continuer && applicationEnCours)
            {
                Console.WriteLine("\n=== Menu Client ===");
                Console.WriteLine("Bienvenue " + nomUtilisateur);
                Console.WriteLine("1. Voir les plats disponibles");
                Console.WriteLine("2. Voir mes commandes");
                Console.WriteLine("3. Passer une commande");
                Console.WriteLine("4. Se deconnecter");
                Console.WriteLine("5. Quitter");
                Console.WriteLine("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.Clear();
                        sqlClient.VoirPlatsDisponibles();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Fonctionnalite en cours de dev");
                        //sqlClient.VoirCommandesClient(authentification.Idutilisateur);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Fonctionnalite en cours de dev");
                        //sqlClient.PasserCommande(authentification.Idutilisateur);
                        break;
                    case "4":
                        Console.Clear();
                        authentification.SeDeconnecter();
                        continuer = false;
                        Console.WriteLine("Vous etes deconnecte");
                        break;
                    case "5":
                        applicationEnCours = false;
                        continuer = false;
                        Console.WriteLine("Au revoir !");
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }
    }
}
