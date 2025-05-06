using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Net.Cache;

namespace LivrableV3
{
    public class ImportJson
    {
        public string cheminFichierJson;
        public ConnexionBDD connexion;

        public ImportJson(ConnexionBDD connexionBase)
        {
            this.cheminFichierJson = @"../../Données/Donnee.json";
            this.connexion = connexionBase;
            ImporterTout();
            LectureJson(cheminFichierJson);
            AfficherPrettyJson(cheminFichierJson);
            EcritureFichierJson();
        }

        public void ImporterTout()
        {
            
            List<Dictionary<string, string>> listeUtilisateurs = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeClients = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeCuisiniers = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listePlats = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeCommandes = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeLivraisons = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeTransactions = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeAvis = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeRecettes = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeInspire = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> listeIngredients = new List<Dictionary<string, string>>();

            
            string requete = "SELECT * FROM utilisateur;";
            MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> utilisateur = new Dictionary<string, string>();
                utilisateur["id_utilisateur"] = reader["id_utilisateur"].ToString();
                utilisateur["nom"] = reader["nom"].ToString();
                utilisateur["prénom"] = reader["prénom"].ToString();
                utilisateur["email"] = reader["email"].ToString();
                utilisateur["adresse"] = reader["adresse"].ToString();
                utilisateur["telephone"] = reader["telephone"].ToString();
                utilisateur["mot_de_passe"] = reader["mot_de_passe"].ToString();
                listeUtilisateurs.Add(utilisateur);
            }
            reader.Close();
            commande.Dispose();

            
            requete = "SELECT * FROM client;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> client = new Dictionary<string, string>();
                client["id_client"] = reader["id_client"].ToString();
                client["id_utilisateur"] = reader["id_utilisateur"].ToString();
                client["StationMetro"] = reader["StationMetro"].ToString();
                client["entreprise_nom"] = reader["entreprise_nom"].ToString();
                client["referent"] = reader["referent"].ToString();
                listeClients.Add(client);
            }
            reader.Close();
            commande.Dispose();

            
            requete = "SELECT * FROM cuisinier;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> cuisinier = new Dictionary<string, string>();
                cuisinier["id_cuisinier"] = reader["id_cuisinier"].ToString();
                cuisinier["id_utilisateur"] = reader["id_utilisateur"].ToString();
                cuisinier["StationMetro"] = reader["StationMetro"].ToString();
                cuisinier["zones_livraison"] = reader["zones_livraison"].ToString();
                cuisinier["note_moyenne"] = reader["note_moyenne"].ToString();
                cuisinier["nombre_livraisons"] = reader["nombre_livraisons"].ToString();
                listeCuisiniers.Add(cuisinier);
            }
            reader.Close();
            commande.Dispose();

            
            requete = "SELECT * FROM Plat_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> plat = new Dictionary<string, string>();
                plat["id_plat"] = reader["id_plat"].ToString();
                plat["id_cuisinier"] = reader["id_cuisinier"].ToString();
                plat["nom"] = reader["nom"].ToString();
                plat["type"] = reader["type"].ToString();
                plat["portions"] = reader["portions"].ToString();
                plat["date_fabrication"] = reader["date_fabrication"].ToString();
                plat["date_peremption"] = reader["date_peremption"].ToString();
                plat["prix_par_personne"] = reader["prix_par_personne"].ToString();
                plat["nationalite"] = reader["nationalite"].ToString();
                plat["regime"] = reader["regime"].ToString();
                plat["photo"] = reader["photo"].ToString();
                listePlats.Add(plat);
            }
            reader.Close();
            commande.Dispose();

            
            requete = "SELECT * FROM Commande_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> commandeDico = new Dictionary<string, string>();
                commandeDico["id_commande"] = reader["id_commande"].ToString();
                commandeDico["id_client"] = reader["id_client"].ToString();
                commandeDico["id_cuisinier"] = reader["id_cuisinier"].ToString();
                commandeDico["id_plat"] = reader["id_plat"].ToString();
                commandeDico["date_commande"] = reader["date_commande"].ToString();
                commandeDico["prix_total"] = reader["prix_total"].ToString();
                commandeDico["statut"] = reader["statut"].ToString();
                listeCommandes.Add(commandeDico);
            }
            reader.Close();
            commande.Dispose();

            
            requete = "SELECT * FROM Livraison;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> livraison = new Dictionary<string, string>();
                livraison["id_livraison"] = reader["id_livraison"].ToString();
                livraison["id_commande"] = reader["id_commande"].ToString();
                livraison["date_livraison"] = reader["date_livraison"].ToString();
                livraison["trajet"] = reader["trajet"].ToString();
                listeLivraisons.Add(livraison);
            }
            reader.Close();
            commande.Dispose();

          
            requete = "SELECT * FROM Transaction_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> transaction = new Dictionary<string, string>();
                transaction["id_transaction"] = reader["id_transaction"].ToString();
                transaction["id_commande"] = reader["id_commande"].ToString();
                transaction["montant"] = reader["montant"].ToString();
                transaction["date_paiement"] = reader["date_paiement"].ToString();
                transaction["statut"] = reader["statut"].ToString();
                listeTransactions.Add(transaction);
            }
            reader.Close();
            commande.Dispose();

           
            requete = "SELECT * FROM Avis_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> avis = new Dictionary<string, string>();
                avis["id_avis"] = reader["id_avis"].ToString();
                avis["id_client"] = reader["id_client"].ToString();
                avis["id_cuisinier"] = reader["id_cuisinier"].ToString();
                avis["id_commande"] = reader["id_commande"].ToString();
                avis["note"] = reader["note"].ToString();
                avis["commentaire"] = reader["commentaire"].ToString();
                avis["date_publication"] = reader["date_publication"].ToString();
                listeAvis.Add(avis);
            }
            reader.Close();
            commande.Dispose();

            requete = "SELECT * FROM Recette_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> recette = new Dictionary<string, string>();
                recette["id_recette"] = reader["id_recette"].ToString();
                recette["nom"] = reader["nom"].ToString();
                recette["description"] = reader["description"].ToString();
                recette["origine"] = reader["origine"].ToString();
                listeRecettes.Add(recette);
            }
            reader.Close();
            commande.Dispose();

            requete = "SELECT * FROM s_inspire_de;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> inspire = new Dictionary<string, string>();
                inspire["id_plat"] = reader["id_plat"].ToString();
                inspire["id_recette"] = reader["id_recette"].ToString();
                listeInspire.Add(inspire);
            }
            reader.Close();
            commande.Dispose();

            requete = "SELECT * FROM Ingrédients;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, string> ingredient = new Dictionary<string, string>();
                ingredient["id_ingredient"] = reader["id_ingredient"].ToString();
                ingredient["id_plat"] = reader["id_plat"].ToString();
                ingredient["nom"] = reader["nom"].ToString();
                listeIngredients.Add(ingredient);
            }
            reader.Close();
            commande.Dispose();

            var objetFinal = new
            {
                utilisateurs = listeUtilisateurs,
                clients = listeClients,
                cuisiniers = listeCuisiniers,
                plats = listePlats,
                commandes = listeCommandes,
                livraisons = listeLivraisons,
                transactions = listeTransactions,
                avis = listeAvis,
                recettes = listeRecettes,
                inspire = listeInspire,
                ingredients = listeIngredients
            };

            string json = JsonConvert.SerializeObject(objetFinal, Formatting.Indented);
            File.WriteAllText("../../Données/SQL.json", json);
            MessageBox.Show("export sql.json ok");
        }

        public void LectureJson(string chemin)
        {
            string rep = "";
            using (StreamReader lecteur = new StreamReader(chemin))
            using (JsonTextReader lecteurJson = new JsonTextReader(lecteur))
            {
                int compteur = 1;
                while (lecteurJson.Read())
                {
                    rep += "token n " + (compteur++) + " => \r\n";
                    Console.Write("token n " + (compteur++) + " => ");
                    if (lecteurJson.Value != null)
                    {
                        rep += lecteurJson.TokenType + " : " + lecteurJson.Value + "\r\n";
                        Console.WriteLine(lecteurJson.TokenType + " : " + lecteurJson.Value);
                    }
                    else
                    {
                        rep += lecteurJson.TokenType + " : \r\n";
                        Console.WriteLine(lecteurJson.TokenType + " : ");
                    }
                }
            }
            MessageBox.Show(rep);
        }

        public void AfficherPrettyJson(string chemin)
        {
            using (StreamReader lecteur = new StreamReader(chemin))
            using (JsonTextReader lecteurJson = new JsonTextReader(lecteur))
            {
                string reponse = "";
                reponse += "debut de AfficherPrettyJson\r\n";
                Console.WriteLine("debut de AfficherPrettyJson");
                reponse += "---------------------------\r\n";
                Console.WriteLine("---------------------------");


                while (lecteurJson.Read())
                {
                    switch (lecteurJson.TokenType)
                    {
                        case JsonToken.StartObject:
                            reponse += lecteurJson.Path + " : nouvel objet\r\n";
                            Console.WriteLine("nouvel objet");
                            reponse += "------------\r\n";
                            Console.WriteLine("------------");
                            break;
                        case JsonToken.EndObject:
                            reponse += "------------\r\n";
                            Console.WriteLine("------------\n");
                            break;
                        case JsonToken.StartArray:
                            reponse += lecteurJson.Path + " : liste\r\n";
                            Console.WriteLine(lecteurJson.Path + " : liste\n");
                            break;
                        case JsonToken.PropertyName:
                            string nomProp = lecteurJson.Value != null ? lecteurJson.Value.ToString() : "";
                            if (lecteurJson.Read())
                            {
                                if (lecteurJson.TokenType == JsonToken.StartObject || lecteurJson.TokenType == JsonToken.StartArray)
                                {
                                    continue;
                                }
                                Console.WriteLine(nomProp + " : " + lecteurJson.Value);
                                reponse += nomProp + " : " + lecteurJson.Value + "\r\n";
                            }
                            break;
                    }
                }

                MessageBox.Show(reponse);
            }

        }

        public void EcritureFichierJson()
        {
            string chemin = @"../../Données/Donnee.json";
            
            string texte = File.ReadAllText(chemin);
            
            dynamic objetJson = JsonConvert.DeserializeObject(texte);

            
            Dictionary<string, string> nouvelUtilisateur = new Dictionary<string, string>();
            nouvelUtilisateur.Add("nom", "Paul");
            nouvelUtilisateur.Add("email", "paul.toto@gmail.com");
            nouvelUtilisateur.Add("station", "République");

            
            if (objetJson.utilisateurs == null)
            {
                objetJson.utilisateurs = new List<object>();
            }
            objetJson.utilisateurs.Add(nouvelUtilisateur);

            
            string nouveauJson = JsonConvert.SerializeObject(objetJson, Formatting.Indented);
            File.WriteAllText(chemin, nouveauJson);

            MessageBox.Show("un utilisateur a ete ajoute dans le json\n");
            AfficherPrettyJson(chemin);
        }
    }
}