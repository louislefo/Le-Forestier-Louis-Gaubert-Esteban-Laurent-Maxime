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

        
        public AffichageClient(ConnexionBDDClient connexionBDDClient,Authentification authentification, Graphe<int> grapheMetro)
        {
            this.authentification = authentification;
            this.connexionBDDClient = connexionBDDClient;
            this.grapheMetro = grapheMetro;
            applicationEnCours = true;
            sqlClient = new SqlClient(connexionBDDClient);
        }


        /// <summary>
        /// affiche le menu du client avec les options 
        /// </summary>
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
                        sqlClient.VoirCommandesClient(authentification.idUtilisateur);
                        break;
                    case "3":
                        Console.Clear();
                        sqlClient.PasserCommande(authentification.idUtilisateur);
                        
                        break;
                    case "4":
                        Console.Clear();
                        authentification.SeDeconnecter();
                        connexionBDDClient.FermerConnexionClient();
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
