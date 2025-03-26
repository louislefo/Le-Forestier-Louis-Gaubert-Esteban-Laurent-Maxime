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

        /// affiche le menu d'authentification
        public void AfficherMenuAuthentification()
        {
            bool continuer = true;
            while (continuer && applicationEnCours)
            {
                Console.WriteLine("\n=== Menu Authentification ===");
                Console.WriteLine("1. Se connecter");
                Console.WriteLine("2. S'inscrire");
                Console.WriteLine("3. Quitter");
                Console.WriteLine("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        if (authentification.SeConnecter())
                        {
                            if (authentification.estCuisinier)
                            {
                                affichageCuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                            }
                            else
                            {
                                AfficherMenuClient(authentification.nomUtilisateur);
                            }
                        }
                        break;
                    case "2":
                        if (authentification.SInscrire())
                        {
                            if (authentification.estCuisinier)
                            {
                                affichageCuisinier.AfficherMenuCuisinier(authentification.nomUtilisateur);
                            }
                            else
                            {
                                AfficherMenuClient(authentification.nomUtilisateur);
                            }
                        }
                        break;
                    case "3":
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
                        Console.WriteLine("Vous etes deconnecte");
                        AfficherMenuAuthentification();
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
