using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class Application
    {
        public Authentification authentification;

        /// constructeur par defaut
        public Application()
        {
            authentification = new Authentification();
        }

        /// methode pour demarrer l'application
        public void Demarrer()
        {
            bool continuer = true;

            while (continuer)
            {
                if (!authentification.estConnecte)
                {
                    Console.WriteLine("=== Liv'In Paris ===");
                    Console.WriteLine("1. Se connecter");
                    Console.WriteLine("2. S'inscrire");
                    Console.WriteLine("3. Quitter");
                    Console.WriteLine("Choix : ");

                    string choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            authentification.SeConnecter();
                            break;
                        case "2":
                            authentification.SInscrire();
                            break;
                        case "3":
                            continuer = false;
                            break;
                        default:
                            Console.WriteLine("Choix invalide");
                            break;
                    }
                }
                else
                {
                    // ici on va ajouter le menu principal
                    Console.WriteLine("Bienvenue " + authentification.nomUtilisateur);
                    Console.WriteLine("1. Se déconnecter");
                    Console.WriteLine("2. Quitter");
                    Console.WriteLine("Choix : ");

                    string choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            authentification.SeDeconnecter();
                            break;
                        case "2":
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
}
