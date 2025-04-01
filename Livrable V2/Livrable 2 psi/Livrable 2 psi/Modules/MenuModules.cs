using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class MenuModules
    {
        public bool moduleEnCours;
        private ModuleClient moduleClient;
        private ModuleCuisinier moduleCuisinier;
        private ModuleCommande moduleCommande;
        private ModuleStatistiques moduleStatistiques;
        private ModuleGraphe moduleGraphe;

        private Graphe<int> grapheMetro;

        /// constructeur
        public MenuModules(ConnexionBDD connexionBDD,Graphe<int> grapheMetro)
        {
            moduleEnCours = true;
            this.moduleClient = new ModuleClient(connexionBDD);
            this.moduleCuisinier = new ModuleCuisinier(connexionBDD);
            this.moduleCommande = new ModuleCommande(connexionBDD);
            this.moduleStatistiques = new ModuleStatistiques(connexionBDD);
            this.moduleGraphe = new ModuleGraphe(grapheMetro);
        }

        /// affiche le menu des modules
        public void AfficherMenuModules()
        {
            while (moduleEnCours)
            {
                Console.Clear();
                Console.WriteLine("\n=== Menu des Modules ===");
                Console.WriteLine("1. Module Client");
                Console.WriteLine("2. Module Cuisinier");
                Console.WriteLine("3. Module Commande");
                Console.WriteLine("4. Module Statistiques");
                Console.WriteLine("5. Module Graphe");
                Console.WriteLine("6. Retour au menu principal");
                Console.Write("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.Clear();
                        AfficherModuleClient();
                        break;
                    case "2":
                        Console.Clear();
                        AfficherModuleCuisinier();
                        break;
                    case "3":
                        Console.Clear();
                        AfficherModuleCommande();
                        break;
                    case "4":
                        Console.Clear();
                        AfficherModuleStatistiques();
                        break;
                    case "5":
                        AfficherModuleGraphe();
                        break;
                    case "6":
                        moduleEnCours = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }

        /// affiche le module client
        private void AfficherModuleClient()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("\n=== Module Client ===");
                Console.WriteLine("1. Ajouter un client");
                Console.WriteLine("2. Supprimer un client");
                Console.WriteLine("3. Modifier un client");
                Console.WriteLine("4. Afficher les clients par ordre alphabetique");
                Console.WriteLine("5. Afficher les clients par rue");
                Console.WriteLine("6. Afficher les clients par montant des achats");
                Console.WriteLine("7. Retour");
                Console.Write("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        moduleClient.AjouterClientConsole();
                        break;
                    
                    case "2":
                        Console.Write("ID du client : ");
                        int idSupprimer = int.Parse(Console.ReadLine());
                        moduleClient.SupprimerClient(idSupprimer);
                        break;
                    case "3":
                        Console.Write("ID du client : ");
                        int idModifier = int.Parse(Console.ReadLine());
                        Console.Write("nouveau nom : ");
                        string nom = Console.ReadLine();
                        Console.Write("nouveau prenom : ");
                        string prenom = Console.ReadLine();
                        Console.Write("nouvelle adresse : ");
                        string adresse = Console.ReadLine();
                        Console.Write("nouvelle station metro : ");
                        string stationMetro = Console.ReadLine();
                        moduleClient.ModifierClient(idModifier, nom, prenom, adresse, stationMetro);
                        break;
                    case "4":
                        moduleClient.AfficherClientsAlphabetique();
                        break;
                    case "5":
                        moduleClient.AfficherClientsParRue();
                        break;
                    case "6":
                        moduleClient.AfficherClientsParAchats();
                        break;
                    case "7":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("choix invalide");
                        break;
                }
            }
        }

        /// affiche le module cuisinier
        private void AfficherModuleCuisinier()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("\n=== Module Cuisinier ===");
                Console.WriteLine("1. Ajouter un cuisinier");
                Console.WriteLine("2. Supprimer un cuisinier");
                Console.WriteLine("3. Modifier un cuisinier");
                Console.WriteLine("4. Afficher les clients servis");
                Console.WriteLine("5. Afficher les plats realises par frequence");
                Console.WriteLine("6. Afficher le plat du jour");
                Console.WriteLine("7. Retour");
                Console.Write("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        moduleCuisinier.AjouterCuisinierConsole();
                        break;  
                    case "2":
                        Console.Write("ID du cuisinier : ");
                        int idSupprimer = int.Parse(Console.ReadLine());
                        moduleCuisinier.SupprimerCuisinier(idSupprimer);
                        break;
                    case "3":
                        Console.Write("ID du cuisinier : ");
                        int idModifier = int.Parse(Console.ReadLine());
                        Console.Write("nouveau nom : ");
                        string nom = Console.ReadLine();
                        Console.Write("nouveau prenom : ");
                        string prenom = Console.ReadLine();
                        Console.Write("nouvelle adresse : ");
                        string adresse = Console.ReadLine();
                        Console.Write("nouvelle station metro : ");
                        string stationMetro = Console.ReadLine();
                        moduleCuisinier.ModifierCuisinier(idModifier, nom, prenom, adresse, stationMetro);
                        break;
                    case "4":
                        Console.Write("ID du cuisinier : ");
                        int idCuisinier = int.Parse(Console.ReadLine());
                        Console.Write("date debut (format: yyyy-mm-dd) : ");
                        DateTime? dateDebut = DateTime.Parse(Console.ReadLine());
                        Console.Write("date fin (format: yyyy-mm-dd) : ");
                        DateTime? dateFin = DateTime.Parse(Console.ReadLine());
                        moduleCuisinier.AfficherClientsServis(idCuisinier, dateDebut, dateFin);
                        break;
                    case "5":
                        Console.Write("ID du cuisinier : ");
                        int idPlats = int.Parse(Console.ReadLine());
                        moduleCuisinier.AfficherPlatsRealises(idPlats);
                        break;
                    case "6":
                        Console.Write("ID du cuisinier : ");
                        int idPlatJour = int.Parse(Console.ReadLine());
                        moduleCuisinier.AfficherPlatDuJour(idPlatJour);
                        break;
                    case "7":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("choix invalide");
                        break;
                }
            }
        }

        /// affiche le module commande
        private void AfficherModuleCommande()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("\n=== Module Commande ===");
                Console.WriteLine("1. Creer une commande");
                Console.WriteLine("2. Modifier une commande");
                Console.WriteLine("3. Calculer le prix d'une commande");
                Console.WriteLine("4. Determiner le chemin de livraison");
                Console.WriteLine("5. Simuler les etapes d'une commande");
                Console.WriteLine("6. Retour");
                Console.Write("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.Write("ID du client : ");
                        int idClient = int.Parse(Console.ReadLine());
                        Console.Write("ID du cuisinier : ");
                        int idCuisinier = int.Parse(Console.ReadLine());
                        Console.Write("ID du plat : ");
                        int idPlat = int.Parse(Console.ReadLine());
                        moduleCommande.CreerCommande(idClient, idCuisinier, idPlat, DateTime.Now);
                        break;
                    case "2":
                        Console.Write("ID de la commande : ");
                        int idCommande = int.Parse(Console.ReadLine());
                        Console.Write("nouvel ID du plat : ");
                        int nouveauIdPlat = int.Parse(Console.ReadLine());
                        moduleCommande.ModifierCommande(idCommande, nouveauIdPlat, DateTime.Now);
                        break;
                    case "3":
                        Console.Write("ID de la commande : ");
                        int idPrix = int.Parse(Console.ReadLine());
                        double prix = moduleCommande.CalculerPrixCommande(idPrix);
                        Console.WriteLine("prix total : " + prix + "€");
                        break;
                    case "4":
                        Console.Write("ID de la commande : ");
                        int idChemin = int.Parse(Console.ReadLine());
                        var (stationDepart, stationArrivee) = moduleCommande.DeterminerCheminLivraison(idChemin);
                        Console.WriteLine("chemin de livraison : " + stationDepart + " -> " + stationArrivee);
                        break;
                    case "5":
                        Console.Write("ID de la commande : ");
                        int idSimulation = int.Parse(Console.ReadLine());
                        moduleCommande.SimulerEtapesCommande(idSimulation);
                        break;
                    case "6":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("choix invalide");
                        break;
                }
            }
        }

        /// affiche le module statistiques
        private void AfficherModuleStatistiques()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("\n=== Module Statistiques ===");
                Console.WriteLine("1. Afficher les livraisons par cuisinier");
                Console.WriteLine("2. Afficher les commandes par periode");
                Console.WriteLine("3. Afficher la moyenne des prix des commandes");
                Console.WriteLine("4. Afficher la moyenne des comptes clients");
                Console.WriteLine("5. Afficher les commandes par type de plat");
                Console.WriteLine("6. Afficher les statistiques creatives");
                Console.WriteLine("7. Retour");
                Console.Write("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        moduleStatistiques.AfficherLivraisonsParCuisinier();
                        break;
                    case "2":
                        Console.Write("date debut (format: yyyy-mm-dd) : ");
                        DateTime dateDebut = DateTime.Parse(Console.ReadLine());
                        Console.Write("date fin (format: yyyy-mm-dd) : ");
                        DateTime dateFin = DateTime.Parse(Console.ReadLine());
                        moduleStatistiques.AfficherCommandesParPeriode(dateDebut, dateFin);
                        break;
                    case "3":
                        moduleStatistiques.AfficherMoyennePrixCommandes();
                        break;
                    case "4":
                        moduleStatistiques.AfficherMoyenneComptesClients();
                        break;
                    case "5":
                        Console.Write("date debut (format: yyyy-mm-dd) : ");
                        DateTime dateDebutType = DateTime.Parse(Console.ReadLine());
                        Console.Write("date fin (format: yyyy-mm-dd) : ");
                        DateTime dateFinType = DateTime.Parse(Console.ReadLine());
                        moduleStatistiques.AfficherCommandesParTypePlat(dateDebutType, dateFinType);
                        break;
                    case "6":
                        moduleStatistiques.AfficherStatistiquesCreatives();
                        break;
                    case "7":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("choix invalide");
                        break;
                }
            }
        }
        private void AfficherModuleGraphe()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("\n=== Module Graphe ===");
                Console.WriteLine("1. Afficher la carte du metro");
                Console.WriteLine("2. Rechercher un itineraire");
                Console.WriteLine("3. Afficher les informations du metro");
                Console.WriteLine("4. Afficher les stations d'une ligne de metro");
                Console.WriteLine("5. Afficher les commandes par type de plat");
                Console.WriteLine("6. Afficher les statistiques creatives");
                Console.WriteLine("7. Retour");
                Console.Write("Choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        moduleGraphe.AfficherCarteMetro();
                        break;
                    case "2":
                        moduleGraphe.RechercherItineraire();
                        break;
                    case "3":
                        moduleGraphe.AfficherInformationsMetro();
                        break;
                    case "4":
                        moduleStatistiques.AfficherMoyenneComptesClients();
                        break;
                    case "5":
                        moduleGraphe.AfficherStationsParLigne();
                        break;
                    case "6":
                        moduleStatistiques.AfficherStatistiquesCreatives();
                        break;
                    case "7":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("choix invalide");
                        break;
                }
            }
        }
    }
}
