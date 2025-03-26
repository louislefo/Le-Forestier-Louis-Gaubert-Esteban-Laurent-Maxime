using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class AffichageCuisinier
    {
        public Authentification authentification;

        /// constructeur par defaut
        public AffichageCuisinier(ConnexionBDD connexion)
        {
            authentification = new Authentification(connexion);
        }

        /// affiche le menu du cuisinier
        public void AfficherMenuCuisinier(string nomUtilisateur)
        {
            bool continuer = true;

            while (continuer)
            {
                Console.WriteLine("=== Menu Cuisinier ===");
                Console.WriteLine("Bienvenue " + nomUtilisateur);
                Console.WriteLine("1. Ajouter un plat");
                Console.WriteLine("2. Voir mes plats");
                Console.WriteLine("3. Voir les commandes en cours");
                Console.WriteLine("4. Se deconnecter");
                Console.WriteLine("5. Quitter");
                Console.WriteLine("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        // TODO: Ajouter un plat
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "2":
                        // TODO: Afficher les plats
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "3":
                        // TODO: Afficher les commandes
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
