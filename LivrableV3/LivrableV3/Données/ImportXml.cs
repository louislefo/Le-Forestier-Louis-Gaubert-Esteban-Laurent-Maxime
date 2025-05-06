using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    public class ImportXml
    {
        public string cheminFichierXml;
        public ConnexionBDD connexion;

        public ImportXml(ConnexionBDD connexionBase)
        {
            this.cheminFichierXml = @"../../Données/Données.Xml";
            this.connexion = connexionBase;
            ImporterTout();
        }

        public void ImporterTout()
        {
            BaseDeDonnees donnees;

            XmlSerializer serializer = new XmlSerializer(typeof(BaseDeDonnees));
            using (FileStream fs = new FileStream(cheminFichierXml, FileMode.Open))
            {
                donnees = (BaseDeDonnees)serializer.Deserialize(fs);
            }

            DonneesImportees donneesImportees = new DonneesImportees
            {
                utilisateurs = new List<Utilisateur>(donnees.utilisateurs.utilisateur),
                clients = new List<Client>(donnees.clients.client),
                cuisiniers = new List<Cuisinier>(donnees.cuisiniers.cuisinier),
                commandes = new List<Commande>(donnees.commandes.commande),
                livraisons = new List<Livraison>(donnees.livraisons.livraison)
            };

            for (int i = 0; i < donneesImportees.utilisateurs.Count; i++)
            {
                Utilisateur utilisateur = donneesImportees.utilisateurs[i];
                string requete = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" + 
                    utilisateur.id_utilisateur + "', '" + utilisateur.nom + "', '" + utilisateur.prenom + "', '" + 
                    utilisateur.email + "', '" + utilisateur.adresse + "', '" + utilisateur.telephone + "', '" + 
                    utilisateur.mot_de_passe + "');";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            for (int i = 0; i < donneesImportees.clients.Count; i++)
            {
                Client client = donneesImportees.clients[i];
                string entrepriseNom = string.IsNullOrEmpty(client.entreprise_nom) ? "NULL" : "'" + client.entreprise_nom + "'";
                string referent = string.IsNullOrEmpty(client.referent) ? "NULL" : "'" + client.referent + "'";
                
                string requete = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" + 
                    client.id_client + "', '" + client.id_utilisateur + "', '" + client.StationMetro + "', " + 
                    entrepriseNom + ", " + referent + ");";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            for (int i = 0; i < donneesImportees.cuisiniers.Count; i++)
            {
                Cuisinier cuisinier = donneesImportees.cuisiniers[i];
                string requete = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" + 
                    cuisinier.id_cuisinier + "', '" + cuisinier.id_utilisateur + "', '" + cuisinier.StationMetro + "', '" + 
                    cuisinier.zones_livraison + "', " + cuisinier.note_moyenne + ", " + cuisinier.nombre_livraisons + ");";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.ExecuteNonQuery();
                commande.Dispose();
            }

            for (int i = 0; i < donneesImportees.commandes.Count; i++)
            {
                Commande commande = donneesImportees.commandes[i];
                string requete = "INSERT INTO Commande_ (id_commande, id_client, id_cuisinier, id_plat, date_commande, prix_total, statut) VALUES ('" + 
                    commande.id_commande + "', '" + commande.id_client + "', '" + commande.id_cuisinier + "', '" + 
                    commande.id_plat + "', '" + commande.date_commande + "', " + commande.prix_total + ", '" + 
                    commande.statut + "');";
                MySqlCommand cmd = new MySqlCommand(requete, connexion.maConnexion);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            for (int i = 0; i < donneesImportees.livraisons.Count; i++)
            {
                Livraison livraison = donneesImportees.livraisons[i];
                string requete = "INSERT INTO Livraison (id_livraison, id_commande, date_livraison, trajet) VALUES ('" + 
                    livraison.id_livraison + "', '" + livraison.id_commande + "', '" + livraison.date_livraison + "', '" + 
                    livraison.trajet + "');";
                MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
                commande.ExecuteNonQuery();
                commande.Dispose();
            }
        }
    }

    [XmlRoot("baseDeDonnees")]
    public class BaseDeDonnees
    {
        [XmlElement("utilisateurs")]
        public Utilisateurs utilisateurs { get; set; }
        [XmlElement("clients")]
        public Clients clients { get; set; }
        [XmlElement("cuisiniers")]
        public Cuisiniers cuisiniers { get; set; }
        [XmlElement("commandes")]
        public Commandes commandes { get; set; }
        [XmlElement("livraisons")]
        public Livraisons livraisons { get; set; }
    }

    public class Utilisateurs
    {
        [XmlElement("utilisateur")]
        public List<Utilisateur> utilisateur { get; set; }
    }

    public class Clients
    {
        [XmlElement("client")]
        public List<Client> client { get; set; }
    }

    public class Cuisiniers
    {
        [XmlElement("cuisinier")]
        public List<Cuisinier> cuisinier { get; set; }
    }

    public class Commandes
    {
        [XmlElement("commande")]
        public List<Commande> commande { get; set; }
    }

    public class Livraisons
    {
        [XmlElement("livraison")]
        public List<Livraison> livraison { get; set; }
    }
}

