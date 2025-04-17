using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using Livrable_2_psi;

namespace Projet_PSI
{
    /// <summary>
    /// cette classe sert a verifier que les donnees entrees sont bonnes
    /// elle a plein de methodes pour verifier les noms, adresses, emails etc
    /// c'est utile pour pas avoir de donnees fausses dans la base
    /// </summary>
    public class ValidationRequette // agit comme une grande bibliothèque de fonction que l'on peut utiliser pour la connexion et l'incription des utilisateur
                                    // toutes les fonctions ne sont pas forcéménet utilisées mais la plupart permettent de bien remplir la base de données et 
                                    // éviter les coquilles
                                    // cela permet aussi de guider l'utilisateur lors de sa connexion ou de son incription
                                    // En temps que root les fonctions sont aussi utilisées pour vérifier que le root ajoute de bonnes donnnées dans la BDD
    {
        public Graphe<int> GrapheMetro;
        public Dictionary<int, Noeud<int>> noeuds;

        public ValidationRequette(Graphe<int> GrapheMetro)
        {
            this.GrapheMetro = GrapheMetro;
            this.noeuds = GrapheMetro.Noeuds;
        }

        /// <summary>
        /// cette methode sert a demander un nom et verifier qu'il est bon
        /// elle verifie que le nom a au moins 2 lettres et que c'est que des lettres
        /// </summary>
        public static string DemanderNom(string message)
        {
            string nom;
            bool valide = false;

            do
            {
                Console.Write(message);
                nom = Console.ReadLine();
                
                // on verifie si le nom est vide
                if (string.IsNullOrEmpty(nom))
                {
                    Console.WriteLine("le nom ne peut pas etre vide");
                    continue;
                }

                // on verifie que le nom est assez long
                if (nom.Length < 2)
                {
                    Console.WriteLine("le nom doit contenir au moins 2 caracteres");
                    continue;
                }

                // on verifie que le nom a que des lettres et des espaces
                        valide = true;
                for (int i = 0; i < nom.Length; i++)
                {
                    if (!char.IsLetter(nom[i]) && nom[i] != ' ' && nom[i] != '-')
                    {
                        valide = false;
                        Console.WriteLine("le nom ne peut contenir que des lettres, espaces et tirets");
                        break;
                    }
                }
            } while (!valide);

            return nom;
        }

        /// <summary>
        /// cette methode sert a demander une adresse et verifier qu'elle est bonne
        /// elle verifie que l'adresse commence par un numero et a au moins 5 caracteres
        /// </summary>
        public static string DemanderAdresse(string message)
        {
            string adresse;
            bool valide = false;

            do
            {
                Console.Write(message);
                adresse = Console.ReadLine();

                // on verifie si l'adresse est vide
                if (string.IsNullOrEmpty(adresse))
                {
                    Console.WriteLine("l'adresse ne peut pas etre vide");
                    continue;
                }

                if (adresse.Length < 5)
                {
                    Console.WriteLine("ladresse doit avoir au moins 5 caracteres");
                    continue;
                }

                // on verifie que l'adresse commence par un numero
                if (!char.IsDigit(adresse[0]))
                {
                    Console.WriteLine("l'adresse doit commencer par un numero");
                    continue;
                }

                bool contientLettre = false;
                for (int i = 0; i < adresse.Length; i++)
                {
                    if (char.IsLetter(adresse[i]))
                    {
                        contientLettre = true;
                        break;
                    }
                }

                if (!contientLettre)
                {
                    Console.WriteLine("l'adresse doit contenir au moins une lettre");
                    continue;
                }

                valide = true;
            } while (!valide);

            return adresse;
        }

        /// <summary>
        /// cette methode sert a demander une station de metro et verifier qu'elle existe
        /// elle regarde dans le graphe du metro si la station existe
        /// </summary>
        public string DemanderStationMetro(string message)
        {
            string station;
            bool valide = false;

            // on utilise le dictionnaire noeuds du graphe
            try
            {
                if(noeuds == null || noeuds.Count == 0)
                {
                    Console.WriteLine("le graphe nest pas charge");
                    return "";
                }

                do
                {
                    Console.Write(message);
                    station = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(station))
                    {
                        Console.WriteLine("la station ne peut pas etre vide");
                        continue;
                    }

                    if (station.Length < 2)
                    {
                        Console.WriteLine("la station doit avoir au moins 2 caracteres");
                        continue;
                    }

                    bool formatValide = true;
                    for (int i = 0; i < station.Length; i++)
                    {
                        if (!char.IsLetter(station[i]) && station[i] != ' ' && station[i] != '-')
                        {
                            formatValide = false;
                            Console.WriteLine("la station ne peut contenir que des lettres, espaces et tirets");
                            break;
                        }
                    }

                    if (!formatValide)
                    {
                        continue;
                    }

                    // on verifie si la station existe dans le metro
                    bool stationExiste = false;
                    foreach (var noeud in noeuds.Values)
                    {
                        if (noeud.NomStation.ToLower() == station)
                        {
                            stationExiste = true;
                            break;
                        }
                    }

                    if (!stationExiste)
                    {
                        Console.WriteLine("cette station n'existe pas dans le reseau de metro");
                        continue;
                    }

                    valide = true;
            } while (!valide);
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la verification de la station : " + ex.Message);
                return "";
            }

            return station;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un email est bien forme
        /// elle verifie qu'il y a un @ et un point apres
        /// </summary>
        public static bool ValiderEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("l'email ne peut pas etre vide");
                return false;
            }

            // on verifie qu'il y a un @
            if (!email.Contains("@"))
            {
                Console.WriteLine("l'email doit contenir un @");
                return false;
            }

            // on separe l'email en deux parties
            string[] parties = email.Split('@');
            if (parties.Length != 2)
            {
                Console.WriteLine("format d'email invalide");
                return false;
            }

            // on verifie la partie avant le @
            if (parties[0].Length < 1)
            {
                Console.WriteLine("la partie locale de l'email ne peut pas etre vide");
                return false;
            }

            // on verifie la partie apres le @
            if (!parties[1].Contains("."))
            {
                Console.WriteLine("l'email doit contenir un point");
                return false;
            }

            string[] domaine = parties[1].Split('.');
            if (domaine.Length < 2 || domaine[domaine.Length - 1].Length < 2)
            {
                Console.WriteLine("l'email doit avoir une extension valide");
                return false;
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un numero de telephone est bon
        /// elle verifie qu'il commence par 0 ou +33 et a 10 chiffres
        /// </summary>
        public static bool ValiderTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone))
            {
                Console.WriteLine("le numero de telephone ne peut pas etre vide");
                return false;
            }

            // on enleve les espaces
            telephone = telephone.Replace(" ", "");

            // on verifie la longueur
            if (telephone.Length != 10 && telephone.Length != 11)
            {
                Console.WriteLine("le numero doit avoir 10 chiffres");
                return false;
            }

            // on verifie que ca commence par 0 ou +33
            if (telephone[0] != '0' && !telephone.StartsWith("+33"))
            {
                Console.WriteLine("le numero doit commencer par 0 ou +33");
                return false;
            }

            // on verifie que c'est que des chiffres (sauf le +)
            for (int i = 0; i < telephone.Length; i++)
            {
                if (telephone[i] != '+' && !char.IsDigit(telephone[i]))
                {
                    Console.WriteLine("le numero ne peut contenir que des chiffres");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un mot de passe est assez fort
        /// elle verifie qu'il a au moins 6 caracteres, une majuscule, une minuscule et un chiffre
        /// </summary>
        public static bool ValiderMotDePasse(string motDePasse)
        {
            if (string.IsNullOrEmpty(motDePasse))
            {
                Console.WriteLine("le mot de passe ne peut pas etre vide");
                return false;
            }

            if (motDePasse.Length < 6)
            {
                Console.WriteLine("le mot de passe doit avoir au moins 6 caracteres");
                return false;
            }

            bool contientMajuscule = false;
            bool contientMinuscule = false;
            bool contientChiffre = false;

            for (int i = 0; i < motDePasse.Length; i++)
            {
                if (char.IsUpper(motDePasse[i]))
                {
                    contientMajuscule = true;
                }
                else if (char.IsLower(motDePasse[i]))
                {
                    contientMinuscule = true;
                }
                else if (char.IsDigit(motDePasse[i]))
                {
                    contientChiffre = true;
                }
            }

            if (!contientMajuscule)
            {
                Console.WriteLine("le mot de passe doit contenir au moins une majuscule");
                return false;
            }

            if (!contientMinuscule)
            {
                Console.WriteLine("le mot de passe doit contenir au moins une minuscule");
                return false;
            }

            if (!contientChiffre)
            {
                Console.WriteLine("le mot de passe doit contenir au moins un chiffre");
                return false;
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un nom d'utilisateur est bon
        /// elle verifie qu'il a au moins 3 caracteres et que c'est que des lettres, chiffres ou _
        /// </summary>
        public static bool ValiderNomUtilisateur(string nomUtilisateur)
        {
            if (string.IsNullOrEmpty(nomUtilisateur))
            {
                Console.WriteLine("le nom d'utilisateur ne peut pas etre vide");
                return false;
            }

            if (nomUtilisateur.Length < 3)
            {
                Console.WriteLine("le nom d'utilisateur doit etre superieur ou egal a 3 caracteres");
                return false;
            }

            for (int i = 0; i < nomUtilisateur.Length; i++)
            {
                if (!char.IsLetterOrDigit(nomUtilisateur[i]) && nomUtilisateur[i] != '_')
                {
                    Console.WriteLine("le nom d'utilisateur ne peut contenir que des lettres, chiffres et des tirets");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a demander un email et verifier qu'il est bon
        /// elle utilise la methode ValiderEmail pour verifier
        /// </summary>
        public static string DemanderEmail(string message)
        {
            string email;
            bool valide = false;

            do
            {
                Console.Write(message);
                email = Console.ReadLine();

                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("l'email ne peut pas etre vide");
                    continue;
                }

                if (!email.Contains("@"))
                {
                    Console.WriteLine("l'email doit contenir un @");
                    continue;
                }

                string[] parties = email.Split('@');
                if (parties.Length != 2)
                {
                    Console.WriteLine("format de mail invalide");
                    continue;
                }

                if (parties[0].Length < 1)
                {
                    Console.WriteLine("la partie devant le @ ne peut pas etre vide");
                    continue;
                }

                if (!parties[1].Contains("."))
                {
                    Console.WriteLine("l'email doit contenir un point");
                    continue;
                }

                valide = true;
            } while (!valide);

            return email;
        }

        /// <summary>
        /// cette methode sert a demander un telephone et verifier qu'il est bon
        /// elle utilise la methode ValiderTelephone pour verifier
        /// </summary>
        public static string DemanderTelephone(string message)
        {
            string telephone;
            bool valide = false;

            do
            {
                Console.Write(message);
                telephone = Console.ReadLine();

                if (string.IsNullOrEmpty(telephone))
                {
                    Console.WriteLine("le telephone ne peut pas etre vide");
                    continue;
                }

                telephone = telephone.Replace(" ", "");

                if (telephone.Length != 10 && telephone.Length != 11)
                {
                    Console.WriteLine("le numero doit avoir 10 chiffres");
                    continue;
                }

                if (telephone[0] != '0' && !telephone.StartsWith("+33"))
                {
                    Console.WriteLine("le numero doit commencer par 0 ou +33");
                    continue;
                }

                valide = true;
                for (int i = 0; i < telephone.Length; i++)
                {
                    if (telephone[i] != '+' && !char.IsDigit(telephone[i]))
                    {
                        valide = false;
                        Console.WriteLine("le numero ne peut contenir que des chiffres");
                        break;
                    }
                }
            } while (!valide);

            return telephone;
        }

        /// <summary>
        /// cette methode sert a demander un mot de passe et verifier qu'il est assez fort
        /// elle utilise la methode ValiderMotDePasse pour verifier
        /// </summary>
        public static string DemanderMotDePasse(string message)
        {
            string motDePasse;
            bool valide = false;

            do
            {
                Console.Write(message);
                motDePasse = Console.ReadLine();

                if (string.IsNullOrEmpty(motDePasse))
                {
                    Console.WriteLine("le mot de passe ne peut pas etre vide");
                    continue;
                }

                if (motDePasse.Length < 6)
                {
                    Console.WriteLine("le mot de passe doit avoir au moins 6 caracteres");
                    continue;
                }

                bool contientMajuscule = false;
                bool contientMinuscule = false;
                bool contientChiffre = false;

                for (int i = 0; i < motDePasse.Length; i++)
                {
                    if (char.IsUpper(motDePasse[i]))
                    {
                        contientMajuscule = true;
                    }
                    else if (char.IsLower(motDePasse[i]))
                    {
                        contientMinuscule = true;
                    }
                    else if (char.IsDigit(motDePasse[i]))
                    {
                        contientChiffre = true;
                    }
                }

                if (!contientMajuscule)
                {
                    Console.WriteLine("le mot de passe doit contenir au moins une majuscule");
                    continue;
                }

                if (!contientMinuscule)
                {
                    Console.WriteLine("le mot de passe doit contenir au moins une minuscule");
                    continue;
                }

                if (!contientChiffre)
                {
                    Console.WriteLine("le mot de passe doit contenir au moins un chiffre");
                    continue;
                }

                valide = true;
            } while (!valide);

            return motDePasse;
        }

        /// <summary>
        /// cette methode sert a demander un nom d'utilisateur et verifier qu'il est bon
        /// elle utilise la methode ValiderNomUtilisateur pour verifier
        /// </summary>
        public static string DemanderNomUtilisateur(string message)
        {
            string nomUtilisateur;
            bool valide = false;

            do
            {
                Console.Write(message);
                nomUtilisateur = Console.ReadLine();

                if (string.IsNullOrEmpty(nomUtilisateur))
                {
                    Console.WriteLine("le nom d'utilisateur ne peut pas etre vide");
                    continue;
                }

                if (nomUtilisateur.Length < 3)
                {
                    Console.WriteLine("le nom d'utilisateur doit avoir au moins 3 caracteres");
                    continue;
                }

                valide = true;
                for (int i = 0; i < nomUtilisateur.Length; i++)
                {
                    if (!char.IsLetterOrDigit(nomUtilisateur[i]) && nomUtilisateur[i] != '_')
                    {
                        valide = false;
                        Console.WriteLine("le nom d'utilisateur ne peut contenir que des lettres, chiffres et underscores");
                        break;
                    }
                }
            } while (!valide);

            return nomUtilisateur;
        }

        /// <summary>
        /// cette methode sert a demander le type d'utilisateur
        /// elle verifie que c'est soit 1 (cuisinier) soit 2 (client)
        /// </summary>
        public static int DemanderTypeUtilisateur(string message)
        {
            int type;
            bool valide = false;

            do
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out type))
                {
                    if (type == 1 || type == 2)
                    {
                        valide = true;
                    }
                    else
                    {
                        Console.WriteLine("choisissez 1 pour cuisinier ou 2 pour client");
                    }
                }
                else
                {
                    Console.WriteLine("entrez un nombre valide");
                }
            } while (!valide);

            return type;
        }
    }
}