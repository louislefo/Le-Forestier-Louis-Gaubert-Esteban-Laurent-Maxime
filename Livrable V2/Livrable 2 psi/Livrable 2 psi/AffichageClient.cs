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

        /// constructeur par defaut
        public AffichageClient(ConnexionBDD connexion)
        {
            authentification = new Authentification(connexion);
            affichageCuisinier = new AffichageCuisinier(connexion);
            
            applicationEnCours = true;
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
                        // TODO: Afficher les plats disponibles
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "2":
                        Console.Clear();
                        // TODO: Afficher les commandes
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "3":
                        Console.Clear();
                        // TODO: Passer une commande
                        Console.WriteLine("Fonctionnalite a venir");
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
