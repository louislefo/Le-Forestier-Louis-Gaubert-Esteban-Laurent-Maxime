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

        /// constructeur
        public MenuModules()
        {
            moduleEnCours = true;
        }

        /// affiche le menu des modules
        public void AfficherMenuModules()
        {
            while (moduleEnCours)
            {
                Console.WriteLine("\n=== Menu des Modules ===");
                Console.WriteLine("1. Module Client");
                Console.WriteLine("2. Module Cuisinier");
                Console.WriteLine("3. Module Commande");
                Console.WriteLine("4. Module Statistiques");
                Console.WriteLine("5. Retour au menu principal");
                Console.WriteLine("Choix : ");

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
            Console.WriteLine("\n=== Module Client ===");
            Console.WriteLine("Fonctionnalites disponibles :");
            Console.WriteLine("- Ajouter un client");
            Console.WriteLine("- Modifier un client");
            Console.WriteLine("- Supprimer un client");
            Console.WriteLine("- Afficher les clients par ordre alphabetique");
            Console.WriteLine("- Afficher les clients par rue");
            Console.WriteLine("- Afficher les clients par montant total des achats");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
        }

        /// affiche le module cuisinier
        private void AfficherModuleCuisinier()
        {
            Console.WriteLine("\n=== Module Cuisinier ===");
            Console.WriteLine("Fonctionnalites disponibles :");
            Console.WriteLine("- Ajouter un cuisinier");
            Console.WriteLine("- Modifier un cuisinier");
            Console.WriteLine("- Supprimer un cuisinier");
            Console.WriteLine("- Afficher les clients servis");
            Console.WriteLine("- Afficher les plats realises");
            Console.WriteLine("- Afficher le plat du jour");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
        }

        /// affiche le module commande
        private void AfficherModuleCommande()
        {
            Console.WriteLine("\n=== Module Commande ===");
            Console.WriteLine("Fonctionnalites disponibles :");
            Console.WriteLine("- Creer une commande");
            Console.WriteLine("- Modifier une commande");
            Console.WriteLine("- Simuler les etapes d'une commande");
            Console.WriteLine("- Calculer le prix d'une commande");
            Console.WriteLine("- Calculer le chemin de livraison optimal");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
        }

        /// affiche le module statistiques
        private void AfficherModuleStatistiques()
        {
            Console.WriteLine("\n=== Module Statistiques ===");
            Console.WriteLine("Fonctionnalites disponibles :");
            Console.WriteLine("- Nombre de livraisons par cuisinier");
            Console.WriteLine("- Commandes par periode");
            Console.WriteLine("- Moyennes des prix");
            Console.WriteLine("- Moyennes des comptes clients");
            Console.WriteLine("- Commandes par type de plats et periode");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
