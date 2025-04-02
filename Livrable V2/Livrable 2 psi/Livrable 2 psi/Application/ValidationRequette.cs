using System;
using System.Text.RegularExpressions;
using System.IO;

namespace Livrable_2_psi
{
    public class ValidationRequette
    {
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
            Regex regex = new Regex(@"^[a-zA-ZÀ-ÿ\s-]+$");

            do
            {
                Console.Write(message);
                nom = Console.ReadLine();
                if (regex.IsMatch(nom) && nom.Length >= 2)
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("le nom doit contenir uniquement des lettres, espaces et tirets");
                }
            } while (!valide);

            return nom;
        }

        /// demande et valide une adresse
        public static string DemanderAdresse(string message)
        {
            string adresse;
            bool valide = false;
            Regex regex = new Regex(@"^[0-9]+[a-zA-ZÀ-ÿ\s,.-]+$");

            do
            {
                Console.Write(message);
                adresse = Console.ReadLine();
                if (regex.IsMatch(adresse) && adresse.Length >= 5)
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("l'adresse doit commencer par un numero et contenir uniquement des lettres, chiffres, espaces et caracteres speciaux autorises");
                }
            } while (!valide);

            return adresse;
        }

        /// demande et valide une station de metro
        public static string DemanderStationMetro(string message)
        {
            string station;
            bool valide = false;
            Regex regex = new Regex(@"^[a-zA-ZÀ-ÿ\s-]+$");

            do
            {
                Console.Write(message);
                station = Console.ReadLine();
                if (regex.IsMatch(station) && station.Length >= 2)
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("la station doit contenir uniquement des lettres, espaces et tirets");
                }
            } while (!valide);

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

        /// <summary>
        /// valide un email
        /// </summary>
        public static bool ValiderEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("l'email ne peut pas etre vide");
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, pattern))
            {
                Console.WriteLine("format d'email invalide");
                return false;
            }

            return true;
        }

        /// <summary>
        /// valide un numero de telephone
        /// </summary>
        public static bool ValiderTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone))
            {
                Console.WriteLine("le numero de telephone ne peut pas etre vide");
                return false;
            }

            string pattern = @"^(\+33|0)[1-9](\d{2}){4}$";
            if (!Regex.IsMatch(telephone, pattern))
            {
                Console.WriteLine("format de telephone invalide");
                return false;
            }

            return true;
        }

        /// <summary>
        /// valide un mot de passe
        /// </summary>
        public static bool ValiderMotDePasse(string motDePasse)
        {
            if (string.IsNullOrEmpty(motDePasse))
            {
                Console.WriteLine("le mot de passe ne peut pas etre vide");
                return false;
            }

            if (motDePasse.Length < 8)
            {
                Console.WriteLine("le mot de passe doit contenir au moins 8 caracteres");
                return false;
            }

            if (!Regex.IsMatch(motDePasse, @"[A-Z]"))
            {
                Console.WriteLine("le mot de passe doit contenir au moins une majuscule");
                return false;
            }

            if (!Regex.IsMatch(motDePasse, @"[a-z]"))
            {
                Console.WriteLine("le mot de passe doit contenir au moins une minuscule");
                return false;
            }

            if (!Regex.IsMatch(motDePasse, @"[0-9]"))
            {
                Console.WriteLine("le mot de passe doit contenir au moins un chiffre");
                return false;
            }

            return true;
        }

        /// <summary>
        /// valide un nom d'utilisateur
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
                Console.WriteLine("le nom d'utilisateur doit contenir au moins 3 caracteres");
                return false;
            }

            string pattern = @"^[a-zA-Z0-9_]+$";
            if (!Regex.IsMatch(nomUtilisateur, pattern))
            {
                Console.WriteLine("le nom d'utilisateur ne peut contenir que des lettres, chiffres et underscores");
                return false;
            }

            return true;
        }

        /// <summary>
        /// demande et valide un email
        /// </summary>
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

        /// <summary>
        /// demande et valide un numero de telephone
        /// </summary>
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

        /// <summary>
        /// demande et valide un mot de passe
        /// </summary>
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

        /// <summary>
        /// demande et valide un nom d'utilisateur
        /// </summary>
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