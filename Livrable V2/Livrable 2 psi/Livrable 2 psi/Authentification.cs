using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_2_psi
{
    public class Authentification
    {
        public string nomUtilisateur;
        public string motDePasse;
        public bool estCuisinier;
        public bool estConnecte;

        /// constructeur par defaut
        public Authentification()
        {
            nomUtilisateur = "";
            motDePasse = "";
            estCuisinier = false;
            estConnecte = false;
        }

        /// methode pour se connecter
        public bool SeConnecter()
        {
            Console.WriteLine("=== Connexion ===");
            Console.WriteLine("Entrez votre nom d'utilisateur : ");
            nomUtilisateur = Console.ReadLine();

            Console.WriteLine("Entrez votre mot de passe : ");
            motDePasse = Console.ReadLine();

            // ici on va verifier dans la base de donnees
            // pour l'instant on simule une connexion reussie
            estConnecte = true;
            return true;
        }

        /// methode pour s'inscrire
        public bool SInscrire()
        {
            Console.WriteLine("=== Inscription ===");
            Console.WriteLine("Entrez votre nom d'utilisateur : ");
            nomUtilisateur = Console.ReadLine();

            Console.WriteLine("Entrez votre mot de passe : ");
            motDePasse = Console.ReadLine();

            Console.WriteLine("Voulez-vous être cuisinier ? (oui/non)");
            string reponse = Console.ReadLine();

            if (reponse.ToLower() == "oui")
            {
                estCuisinier = true;
            }
            else
            {
                estCuisinier = false;
            }

            // ici on va ajouter dans la base de donnees
            // pour l'instant on simule une inscription reussie
            estConnecte = true;
            return true;
        }

        /// methode pour se deconnecter
        public void SeDeconnecter()
        {
            estConnecte = false;
            nomUtilisateur = "";
            motDePasse = "";
        }
    }
}
