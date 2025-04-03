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
        public bool applicationEnCours;
        public ConnexionBDDCuisinier connexionBDDCuisinier;
        public Graphe<int> grapheMetro;

        /// constructeur par defaut
        public AffichageCuisinier(ConnexionBDDCuisinier connexion,Authentification authentification, Graphe<int> grapheMetro)
        {
            this.authentification = authentification;
            this.connexionBDDCuisinier = connexion;
            this.grapheMetro = grapheMetro;
            applicationEnCours = true;
        }

        /// affiche le menu du cuisinier
        public void AfficherMenuCuisinier(string nomUtilisateur)
        {
            bool continuer = true;

            while (continuer && applicationEnCours)
            {
                Console.WriteLine("\n=== Menu Cuisinier ===");
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
                        Console.Clear();
                        // TODO: Ajouter un plat
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "2":
                        Console.Clear();
                        // TODO: Afficher les plats
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "3":
                        Console.Clear();
                        // TODO: Afficher les commandes
                        Console.WriteLine("Fonctionnalite a venir");
                        break;
                    case "4":
                        Console.Clear();
                        authentification.SeDeconnecter();
                        connexionBDDCuisinier.FermerConnexionCuisinier();
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
