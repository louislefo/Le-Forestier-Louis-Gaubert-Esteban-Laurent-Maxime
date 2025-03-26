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

        /// constructeur par defaut
        public Application()
        {
            try
            {
                connexionBDD = new ConnexionBDD();
                authentification = new Authentification(connexionBDD);
                affichageCuisinier = new AffichageCuisinier(connexionBDD);
                affichageClient = new AffichageClient(connexionBDD);
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur lors de la connexion a la base de donnees : " + e.Message);
                Environment.Exit(1);
            }
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
