using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using Livrable_2_psi;

namespace Livrable_2_psi
{
    public class ValidationRequette
    {
        public Graphe<int> GrapheMetro;
        public Dictionary<int, Noeud<int>> noeuds;

        public ValidationRequette(Graphe<int> GrapheMetro)
        {
            this.GrapheMetro = GrapheMetro;
            this.noeuds = GrapheMetro.Noeuds;
        }
        
        /// demande et valide un nombre entier
        public static int DemanderNombreEntier(string message)
        {
            int nombre;
            bool valide = false;

            do
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out nombre))
                {
                    if (nombre > 0)
                    {
                        valide = true;
                    }
                    else
                    {
                        Console.WriteLine("le nombre doit etre positif");
                    }
                }
                else
                {
                    Console.WriteLine("entrez un nombre valide");
                }
            } while (!valide);

            return nombre;
        }

        /// demande et valide un nombre decimal
        public static double DemanderNombreDecimal(string message)
        {
            double nombre;
            bool valide = false;

            do
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out nombre))
                {
                    if (nombre > 0)
                    {
                        valide = true;
                    }
                    else
                    {
                        Console.WriteLine("le nombre doit etre positif");
                    }
                }
                else
                {
                    Console.WriteLine("entrez un nombre valide");
                }
            } while (!valide);

            return nombre;
        }

        /// demande et valide une date
        public static DateTime DemanderDate(string message)
        {
            DateTime date;
            bool valide = false;

            do
            {
                Console.Write(message + " (format: yyyy-mm-dd) : ");
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    if (date <= DateTime.Now)
                    {
                        valide = true;
                    }
                    else
                    {
                        Console.WriteLine("la date ne peut pas etre dans le futur");
                    }
                }
                else
                {
                    Console.WriteLine("entrez une date valide");
                }
            } while (!valide);

            return date;
        }

        /// demande et valide un nom
        public static string DemanderNom(string message)
        {
            string nom;
            bool valide = false;

            do
            {
                Console.Write(message);
                nom = Console.ReadLine();
                
                // verifie si le nom est vide
                if (string.IsNullOrEmpty(nom))
                {
                    Console.WriteLine("le nom ne peut pas etre vide");
                    continue;
                }

                // verifie la longueur minimale
                if (nom.Length < 2)
                {
                    Console.WriteLine("le nom doit contenir au moins 2 caracteres");
                    continue;
                }

                // verifie que le nom ne contient que des lettres, espaces et tirets
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

        /// demande et valide une adresse
        public static string DemanderAdresse(string message)
        {
            string adresse;
            bool valide = false;

            do
            {
                Console.Write(message);
                adresse = Console.ReadLine();

                // verifie si l'adresse est vide
                if (string.IsNullOrEmpty(adresse))
                {
                    Console.WriteLine("l'adresse ne peut pas etre vide");
                    continue;
                }

                // verifie la longueur minimale
                if (adresse.Length < 5)
                {
                    Console.WriteLine("l'adresse doit contenir au moins 5 caracteres");
                    continue;
                }

                // verifie que l'adresse commence par un chiffre
                if (!char.IsDigit(adresse[0]))
                {
                    Console.WriteLine("l'adresse doit commencer par un numero");
                    continue;
                }

                // verifie que l'adresse contient au moins une lettre
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

        /// demande et valide une station de metro
        public string DemanderStationMetro(string message)
        {
            string station;
            bool valide = false;

            // utiliser le dictionnaire noeuds du graphe
            try
            {
                // verifie si le graphe est charge
                if(noeuds == null || noeuds.Count == 0)
                {
                    Console.WriteLine("le graphe n'est pas charge");
                    return "";
                }

                do
                {
                    Console.Write(message);
                    station = Console.ReadLine().ToLower();

                    // verifie si la station est vide
                    if (string.IsNullOrEmpty(station))
                    {
                        Console.WriteLine("la station ne peut pas etre vide");
                        continue;
                    }

                    // verifie la longueur minimale
                    if (station.Length < 2)
                    {
                        Console.WriteLine("la station doit contenir au moins 2 caracteres");
                        continue;
                    }

                    // verifie que la station ne contient que des lettres, espaces et tirets
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

                    // verifie si la station existe dans le graphe
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

        /// demande et valide un chemin de fichier
        public static string DemanderCheminFichier(string message)
        {
            string chemin;
            bool valide = false;

            do
            {
                Console.Write(message);
                chemin = Console.ReadLine();
                if (System.IO.File.Exists(chemin))
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("le fichier n'existe pas");
                }
            } while (!valide);

            return chemin;
        }

        /// demande une confirmation
        public static bool DemanderConfirmation(string message)
        {
            string reponse;
            bool valide = false;

            do
            {
                Console.Write(message + " (o/n) : ");
                reponse = Console.ReadLine().ToLower();
                if (reponse == "o" || reponse == "n")
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("repondez par o (oui) ou n (non)");
                }
            } while (!valide);

            return reponse == "o";
        }

        /// valide un email
        public static bool ValiderEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("l'email ne peut pas etre vide");
                return false;
            }

            // verifie si l'email contient un @
            if (!email.Contains("@"))
            {
                Console.WriteLine("l'email doit contenir un @");
                return false;
            }

            // separe l'email en deux parties
            string[] parties = email.Split('@');
            if (parties.Length != 2)
            {
                Console.WriteLine("format d'email invalide");
                return false;
            }

            // verifie la partie locale
            if (parties[0].Length < 1)
            {
                Console.WriteLine("la partie locale de l'email ne peut pas etre vide");
                return false;
            }

            // verifie le domaine
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

        /// valide un numero de telephone
        public static bool ValiderTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone))
            {
                Console.WriteLine("le numero de telephone ne peut pas etre vide");
                return false;
            }

            // enleve les espaces
            telephone = telephone.Replace(" ", "");

            // verifie la longueur
            if (telephone.Length != 10 && telephone.Length != 11)
            {
                Console.WriteLine("le numero doit avoir 10 chiffres");
                return false;
            }

            // verifie si le numero commence par 0 ou +33
            if (telephone[0] != '0' && !telephone.StartsWith("+33"))
            {
                Console.WriteLine("le numero doit commencer par 0 ou +33");
                return false;
            }

            // verifie que tous les caracteres sont des chiffres (sauf le +)
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

        /// valide un mot de passe
        public static bool ValiderMotDePasse(string motDePasse)
        {
            if (string.IsNullOrEmpty(motDePasse))
            {
                Console.WriteLine("le mot de passe ne peut pas etre vide");
                return false;
            }

            if (motDePasse.Length < 6)
            {
                Console.WriteLine("le mot de passe doit contenir au moins 8 caracteres");
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

        /// valide un nom d'utilisateur
        public static bool ValiderNomUtilisateur(string nomUtilisateur)
        {
            if (string.IsNullOrEmpty(nomUtilisateur))
            {
                Console.WriteLine("le nom d'utilisateur ne peut pas etre vide");
                return false;
            }

            if (nomUtilisateur.Length < 3)
            {
                Console.WriteLine("le nom d'utilisateur doit contenir au moins 3 caracteres");
                return false;
            }

            for (int i = 0; i < nomUtilisateur.Length; i++)
            {
                if (!char.IsLetterOrDigit(nomUtilisateur[i]) && nomUtilisateur[i] != '_')
                {
                    Console.WriteLine("le nom d'utilisateur ne peut contenir que des lettres, chiffres et underscores");
                    return false;
                }
            }

            return true;
        }

        /// demande et valide un email
        public static string DemanderEmail(string message)
        {
            string email;
            bool valide = false;

            do
            {
                Console.Write(message);
                email = Console.ReadLine();
                valide = ValiderEmail(email);
            } while (!valide);

            return email;
        }

        /// demande et valide un numero de telephone
        public static string DemanderTelephone(string message)
        {
            string telephone;
            bool valide = false;

            do
            {
                Console.Write(message);
                telephone = Console.ReadLine();
                valide = ValiderTelephone(telephone);
            } while (!valide);

            return telephone;
        }

        /// demande et valide un mot de passe
        public static string DemanderMotDePasse(string message)
        {
            string motDePasse;
            bool valide = false;

            do
            {
                Console.Write(message);
                motDePasse = Console.ReadLine();
                valide = ValiderMotDePasse(motDePasse);
            } while (!valide);

            return motDePasse;
        }

        /// demande et valide un nom d'utilisateur
        public static string DemanderNomUtilisateur(string message)
        {
            string nomUtilisateur;
            bool valide = false;

            do
            {
                Console.Write(message);
                nomUtilisateur = Console.ReadLine();
                valide = ValiderNomUtilisateur(nomUtilisateur);
            } while (!valide);

            return nomUtilisateur;
        }

        /// demande et valide un type d'utilisateur
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

        /// verifie si un fichier existe et est accessible
        public static bool VerifierFichierMetro(string cheminFichier)
        {
            try
            {
                if (!File.Exists(cheminFichier))
                {
                    Console.WriteLine("le fichier " + cheminFichier + " n'existe pas");
                    return false;
                }

                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    string premiereLigne = sr.ReadLine();
                    if (string.IsNullOrEmpty(premiereLigne))
                    {
                        Console.WriteLine("le fichier " + cheminFichier + " est vide");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur lors de la lecture du fichier : " + ex.Message);
                return false;
            }
        }

        /// verifie le format d'une ligne de fichier metro
        public static bool ValiderLigneMetro(string ligne)
        {
            string[] colonnes = ligne.Split(',');
            if (colonnes.Length < 2)
            {
                return false;
            }

            // verifie que les colonnes sont des nombres
            return int.TryParse(colonnes[0], out _) && int.TryParse(colonnes[1], out _);
        }
    }
}