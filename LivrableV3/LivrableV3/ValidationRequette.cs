using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;


namespace LivrableV3
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
        public string DemanderNom(string nom)
        {
            // on verifie si le nom est vide
            if (string.IsNullOrEmpty(nom))
            {
                MessageBox.Show("le nom ne peut pas etre vide");
                return "";
            }

            // on verifie que le nom est assez long
            if (nom.Length < 2)
            {
                MessageBox.Show("le nom doit contenir au moins 2 caracteres");
                return "";
            }

            // on verifie que le nom a que des lettres et des espaces
            for (int i = 0; i < nom.Length; i++)
            {
                if (!char.IsLetter(nom[i]) && nom[i] != ' ' && nom[i] != '-')
                {
                    MessageBox.Show("le nom ne peut contenir que des lettres espaces et tirets");
                    return "";
                }
            }

            return nom;
        }

        /// <summary>
        /// cette methode sert a demander un nom et verifier qu'il est bon
        /// elle verifie que le nom a au moins 2 lettres et que c'est que des lettres
        /// </summary>
        public string DemanderPrenom(string prenom)
        {
            // on verifie si le prenom est vide
            if (string.IsNullOrEmpty(prenom))
            {
                MessageBox.Show("le prenom ne peut pas etre vide");
                return "";
            }

            // on verifie que le prenom est assez long
            if (prenom.Length < 2)
            {
                MessageBox.Show("le prenom doit contenir au moins 2 caracteres");
                return "";
            }

            // on verifie que le prenom a que des lettres et des espaces
            for (int i = 0; i < prenom.Length; i++)
            {
                if (!char.IsLetter(prenom[i]) && prenom[i] != ' ' && prenom[i] != '-')
                {
                    MessageBox.Show("le prenom ne peut contenir que des lettres espaces et tirets");
                    return "";
                }
            }

            return prenom;
        }

        /// <summary>
        /// cette methode sert a demander une adresse et verifier qu'elle est bonne
        /// elle verifie que l'adresse commence par un numero et a au moins 5 caracteres
        /// </summary>
        public string DemanderAdresse(string adresse)
        {
            // on verifie si l'adresse est vide
            if (string.IsNullOrEmpty(adresse))
            {
                MessageBox.Show("l'adresse ne peut pas etre vide");
                return "";
            }

            if (adresse.Length < 5)
            {
                MessageBox.Show("l'adresse doit avoir au moins 5 caracteres");
                return "";
            }

            // on verifie que l'adresse commence par un numero
            if (!char.IsDigit(adresse[0]))
            {
                MessageBox.Show("l'adresse doit commencer par un numero");
                return "";
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
                MessageBox.Show("l'adresse doit contenir au moins une lettre");
                return "";
            }

            return adresse;
        }

        /// <summary>
        /// cette methode sert a demander une station de metro et verifier qu'elle existe
        /// elle regarde dans le graphe du metro si la station existe
        /// </summary>
        public string DemanderStationMetro(string station)
        {
            try
            {
                if(noeuds == null || noeuds.Count == 0)
                {
                    MessageBox.Show("le graphe n'est pas charge");
                    return "";
                }

                if (string.IsNullOrEmpty(station))
                {
                    MessageBox.Show("la station ne peut pas etre vide");
                    return "";
                }

                if (station.Length < 2)
                {
                    MessageBox.Show("la station doit avoir au moins 2 caracteres");
                    return "";
                }

                // on met en minuscule pour comparer
                station = station.ToLower();

                // on verifie si la station existe dans le metro
                bool stationExiste = false;
                for (int i = 0; i < noeuds.Count; i++)
                {
                    if (noeuds[i+1].NomStation.ToLower() == station)
                    {
                        stationExiste = true;
                        break;
                    }
                }

                if (!stationExiste)
                {
                    MessageBox.Show("cette station n'existe pas dans le metro");
                    return "";
                }

                return station;
            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur avec la station : " + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// cette methode sert a verifier qu'un email est bien forme
        /// elle verifie qu'il y a un @ et un point apres
        /// </summary>
        public  bool ValiderEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("l'email ne peut pas etre vide");
                return false;
            }

            // on verifie qu'il y a un @
            if (!email.Contains("@"))
            {
                MessageBox.Show("l'email doit contenir un @");
                return false;
            }

            // on separe l'email en deux parties
            string[] parties = email.Split('@');
            if (parties.Length != 2)
            {
                MessageBox.Show("format d'email invalide");
                return false;
            }

            // on verifie la partie avant le @
            if (parties[0].Length < 1)
            {
                MessageBox.Show("la partie locale de l'email ne peut pas etre vide");
                return false;
            }

            // on verifie la partie apres le @
            if (!parties[1].Contains("."))
            {
                MessageBox.Show("l'email doit contenir un point");
                return false;
            }

            string[] domaine = parties[1].Split('.');
            if (domaine.Length < 2 || domaine[domaine.Length - 1].Length < 2)
            {
                MessageBox.Show("l'email doit avoir une extension valide");
                return false;
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un numero de telephone est bon
        /// elle verifie qu'il commence par 0 ou +33 et a 10 chiffres
        /// </summary>
        public  bool ValiderTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone))
            {
                MessageBox.Show("le numero de telephone ne peut pas etre vide");
                return false;
            }

            // on enleve les espaces
            telephone = telephone.Replace(" ", "");

            // on verifie la longueur
            if (telephone.Length != 10 && telephone.Length != 11)
            {
                MessageBox.Show("le numero doit avoir 10 chiffres");
                return false;
            }

            // on verifie que ca commence par 0 ou +33
            if (telephone[0] != '0' && !telephone.StartsWith("+33"))
            {
                MessageBox.Show("le numero doit commencer par 0 ou +33");
                return false;
            }

            // on verifie que c'est que des chiffres (sauf le +)
            for (int i = 0; i < telephone.Length; i++)
            {
                if (telephone[i] != '+' && !char.IsDigit(telephone[i]))
                {
                    MessageBox.Show("le numero ne peut contenir que des chiffres");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un mot de passe est assez fort
        /// elle verifie qu'il a au moins 6 caracteres, une majuscule, une minuscule et un chiffre
        /// </summary>
        public  bool ValiderMotDePasse(string motDePasse)
        {
            if (string.IsNullOrEmpty(motDePasse))
            {
                MessageBox.Show("le mot de passe ne peut pas etre vide");
                return false;
            }

            if (motDePasse.Length < 6)
            {
                MessageBox.Show("le mot de passe doit avoir au moins 6 caracteres");
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
                MessageBox.Show("le mot de passe doit contenir au moins une majuscule");
                return false;
            }

            if (!contientMinuscule)
            {
                MessageBox.Show("le mot de passe doit contenir au moins une minuscule");
                return false;
            }

            if (!contientChiffre)
            {
                MessageBox.Show("le mot de passe doit contenir au moins un chiffre");
                return false;
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a verifier qu'un nom d'utilisateur est bon
        /// elle verifie qu'il a au moins 3 caracteres et que c'est que des lettres, chiffres ou _
        /// </summary>
        public  bool ValiderNomUtilisateur(string nomUtilisateur)
        {
            if (string.IsNullOrEmpty(nomUtilisateur))
            {
                MessageBox.Show("le nom d'utilisateur ne peut pas etre vide");
                return false;
            }

            if (nomUtilisateur.Length < 3)
            {
                MessageBox.Show("le nom d'utilisateur doit etre superieur ou egal a 3 caracteres");
                return false;
            }

            for (int i = 0; i < nomUtilisateur.Length; i++)
            {
                if (!char.IsLetterOrDigit(nomUtilisateur[i]) && nomUtilisateur[i] != '_')
                {
                    MessageBox.Show("le nom d'utilisateur ne peut contenir que des lettres, chiffres et des tirets");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// cette methode sert a demander un email et verifier qu'il est bon
        /// elle utilise la methode ValiderEmail pour verifier
        /// </summary>
        public  string DemanderEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("l'email ne peut pas etre vide");
                return "";
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("l'email doit contenir un @");
                return "";
            }

            string[] parties = email.Split('@');
            if (parties.Length != 2)
            {
                MessageBox.Show("format de mail invalide");
                return "";
            }

            if (parties[0].Length < 1)
            {
                MessageBox.Show("la partie devant le @ ne peut pas etre vide");
                return "";
            }

            if (!parties[1].Contains("."))
            {
                MessageBox.Show("l'email doit contenir un point");
                return "";
            }

            return email;
        }

        /// <summary>
        /// cette methode sert a demander un telephone et verifier qu'il est bon
        /// elle utilise la methode ValiderTelephone pour verifier
        /// </summary>
        public  string DemanderTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone))
            {
                MessageBox.Show("le telephone ne peut pas etre vide");
                return "";
            }

            telephone = telephone.Replace(" ", "");

            if (telephone.Length != 10 && telephone.Length != 11)
            {
                MessageBox.Show("le numero doit avoir 10 chiffres");
                return "";
            }

            if (telephone[0] != '0' && !telephone.StartsWith("+33"))
            {
                MessageBox.Show("le numero doit commencer par 0 ou +33");
                return "";
            }

            for (int i = 0; i < telephone.Length; i++)
            {
                if (telephone[i] != '+' && !char.IsDigit(telephone[i]))
                {
                    MessageBox.Show("le numero ne peut contenir que des chiffres");
                    return "";
                }
            }

            return telephone;
        }

        /// <summary>
        /// cette methode sert a demander un mot de passe et verifier qu'il est assez fort
        /// elle utilise la methode ValiderMotDePasse pour verifier
        /// </summary>
        public  string DemanderMotDePasse(string motDePasse)
        {
            if (string.IsNullOrEmpty(motDePasse))
            {
                MessageBox.Show("le mot de passe ne peut pas etre vide");
                return "";
            }

            if (motDePasse.Length < 6)
            {
                MessageBox.Show("le mot de passe doit avoir au moins 6 caracteres");
                return "";
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
                MessageBox.Show("le mot de passe doit contenir au moins une majuscule");
                return "";
            }

            if (!contientMinuscule)
            {
                MessageBox.Show("le mot de passe doit contenir au moins une minuscule");
                return "";
            }

            if (!contientChiffre)
            {
                MessageBox.Show("le mot de passe doit contenir au moins un chiffre");
                return "";
            }

            return motDePasse;
        }

        /// <summary>
        /// cette methode sert a demander un nom d'utilisateur et verifier qu'il est bon
        /// elle utilise la methode ValiderNomUtilisateur pour verifier
        /// </summary>
        public string DemanderNomUtilisateur(string nomUtilisateur)
        {
            if (string.IsNullOrEmpty(nomUtilisateur))
            {
                MessageBox.Show("le nom d'utilisateur ne peut pas etre vide");
                return "";
            }

            if (nomUtilisateur.Length < 3)
            {
                MessageBox.Show("le nom d'utilisateur doit avoir au moins 3 caracteres");
                return "";
            }

            for (int i = 0; i < nomUtilisateur.Length; i++)
            {
                if (!char.IsLetterOrDigit(nomUtilisateur[i]) && nomUtilisateur[i] != '_')
                {
                    MessageBox.Show("le nom d'utilisateur ne peut contenir que des lettres, chiffres et underscores");
                    return "";
                }
            }

            return nomUtilisateur;
        }

        /// <summary>
        /// cette methode sert a demander le type d'utilisateur
        /// elle verifie que c'est soit 1 (cuisinier) soit 2 (client)
        /// </summary>
        public  int DemanderTypeUtilisateur(string message)
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
                        MessageBox.Show("choisissez 1 pour cuisinier ou 2 pour client");
                    }
                }
                else
                {
                    MessageBox.Show("entrez un nombre valide");
                }
            } while (!valide);

            return type;
        }
    }
}