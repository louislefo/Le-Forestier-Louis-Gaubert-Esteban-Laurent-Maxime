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

        /// constructeur par defaut
        public AffichageClient()
        {
            authentification = new Authentification();
        }

        /// affiche le menu du client
        public void AfficherMenuClient(string nomUtilisateur)
        {
            bool continuer = true;

            while (continuer)
            {
                Console.WriteLine("=== Menu Client ===");
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
                        // TODO: Afficher les plats disponibles
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "2":
                        // TODO: Afficher les commandes
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "3":
                        // TODO: Passer une commande
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "4":
                        authentification.SeDeconnecter();
                        continuer = false;
                        break;
                    case "5":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }
    }
}
