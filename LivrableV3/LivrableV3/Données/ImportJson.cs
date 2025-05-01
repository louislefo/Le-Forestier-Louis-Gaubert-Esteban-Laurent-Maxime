using System; 
using System.Collections.Generic; 
using System.IO; 
using System.Text.Json; 
using MySql.Data.MySqlClient;

namespace LivrableV3
{

    public class ImportJson
    {
        public string cheminFichierJson;
        public ConnexionBDD connexion;

        /// c la classe pour importer json
        public ImportJson(ConnexionBDD connexionBase)
        {
            this.cheminFichierJson = @"../../Données/Donnee.json";
            this.connexion = connexionBase;
            ImporterTout();
        }

        /// pour importer tout
        public void ImporterTout()
        {
            string texteJson = File.ReadAllText(cheminFichierJson);
            DonneesImportees donnees = JsonSerializer.Deserialize<DonneesImportees>(texteJson);

            // utilisateur
            for (int i = 0; i < donnees.utilisateurs.Count; i++)
            {
                Utilisateur utilisateur = donnees.utilisateurs[i];
                string requete = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + utilisateur.id_utilisateur + "', '" + utilisateur.nom + "', '" + utilisateur.prenom + "', '" + utilisateur.email + "', '" + utilisateur.adresse + "', '" + utilisateur.telephone + "', '" + utilisateur.mot_de_passe + "');";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            // client
            for (int i = 0; i < donnees.clients.Count; i++)
            {
                Client client = donnees.clients[i];
                string requete = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + client.id_client + "', '" + client.id_utilisateur + "', '" + client.StationMetro + "', '" + client.entreprise_nom + "', '" + client.referent + "');";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            // cuisinier
            for (int i = 0; i < donnees.cuisiniers.Count; i++)
            {
                Cuisinier cuisinier = donnees.cuisiniers[i];
                string requete = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" + cuisinier.id_cuisinier + "', '" + cuisinier.id_utilisateur + "', '" + cuisinier.StationMetro + "', '" + cuisinier.zones_livraison + "', " + cuisinier.note_moyenne + ", " + cuisinier.nombre_livraisons + ");";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            // commande
            for (int i = 0; i < donnees.commandes.Count; i++)
            {
                Commande commandeObj = donnees.commandes[i];
                string requete = "INSERT INTO Commande_ (id_commande, id_client, id_cuisinier, id_plat, date_commande, prix_total, statut) VALUES ('" + commandeObj.id_commande + "', '" + commandeObj.id_client + "', '" + commandeObj.id_cuisinier + "', '" + commandeObj.id_plat + "', '" + commandeObj.date_commande + "', " + commandeObj.prix_total + ", '" + commandeObj.statut + "');";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            // livraison
            for (int i = 0; i < donnees.livraisons.Count; i++)
            {
                Livraison livraison = donnees.livraisons[i];
                string requete = "INSERT INTO Livraison (id_livraison, id_commande, date_livraison, trajet) VALUES ('" + livraison.id_livraison + "', '" + livraison.id_commande + "', '" + livraison.date_livraison + "', '" + livraison.trajet + "');";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();
            }
        }
    }

    public class DonneesImportees
    {
        public List<Utilisateur> utilisateurs;
        public List<Client> clients;
        public List<Cuisinier> cuisiniers;
        public List<Commande> commandes;
        public List<Livraison> livraisons;
    }

    public class Utilisateur
    {
        public string id_utilisateur;
        public string nom;
        public string prenom;
        public string email;
        public string adresse;
        public string telephone;
        public string mot_de_passe;
    }

    public class Client
    {
        public string id_client;
        public string id_utilisateur;
        public string StationMetro;
        public string entreprise_nom;
        public string referent;
    }

    public class Cuisinier
    {
        public string id_cuisinier;
        public string id_utilisateur;
        public string StationMetro;
        public string zones_livraison;
        public double note_moyenne;
        public int nombre_livraisons;
    }

    public class Commande
    {
        public string id_commande;
        public string id_client;
        public string id_cuisinier;
        public string id_plat;
        public string date_commande;
        public double prix_total;
        public string statut;
    }

    public class Livraison
    {
        public string id_livraison;
        public string id_commande;
        public string date_livraison;
        public string trajet;
    }

}