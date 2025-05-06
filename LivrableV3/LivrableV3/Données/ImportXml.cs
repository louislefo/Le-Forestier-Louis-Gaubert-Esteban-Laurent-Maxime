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

        public void ExporterToutSqlEnXml()
        {
            /// je vais recuperer les donnees de la base et les mettre dans un xml
            List<Utilisateur> listeUtilisateurs = new List<Utilisateur>();
            List<Client> listeClients = new List<Client>();
            List<Cuisinier> listeCuisiniers = new List<Cuisinier>();
            List<Commande> listeCommandes = new List<Commande>();
            List<Livraison> listeLivraisons = new List<Livraison>();

            // utilisateur
            string requete = "SELECT * FROM utilisateur;";
            MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Utilisateur utilisateur = new Utilisateur();
                utilisateur.id_utilisateur = reader["id_utilisateur"].ToString();
                utilisateur.nom = reader["nom"].ToString();
                utilisateur.prenom = reader["prénom"].ToString();
                utilisateur.email = reader["email"].ToString();
                utilisateur.adresse = reader["adresse"].ToString();
                utilisateur.telephone = reader["telephone"].ToString();
                utilisateur.mot_de_passe = reader["mot_de_passe"].ToString();
                listeUtilisateurs.Add(utilisateur);
            }
            reader.Close();
            commande.Dispose();

            // client
            requete = "SELECT * FROM client;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Client client = new Client();
                client.id_client = reader["id_client"].ToString();
                client.id_utilisateur = reader["id_utilisateur"].ToString();
                client.StationMetro = reader["StationMetro"].ToString();
                client.entreprise_nom = reader["entreprise_nom"].ToString();
                client.referent = reader["referent"].ToString();
                listeClients.Add(client);
            }
            reader.Close();
            commande.Dispose();

            // cuisinier
            requete = "SELECT * FROM cuisinier;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Cuisinier cuisinier = new Cuisinier();
                cuisinier.id_cuisinier = reader["id_cuisinier"].ToString();
                cuisinier.id_utilisateur = reader["id_utilisateur"].ToString();
                cuisinier.StationMetro = reader["StationMetro"].ToString();
                cuisinier.zones_livraison = reader["zones_livraison"].ToString();
                cuisinier.note_moyenne = Convert.ToDouble(reader["note_moyenne"].ToString());
                cuisinier.nombre_livraisons = Convert.ToInt32(reader["nombre_livraisons"].ToString());
                listeCuisiniers.Add(cuisinier);
            }
            reader.Close();
            commande.Dispose();

            // commande
            requete = "SELECT * FROM Commande_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Commande commandeObj = new Commande();
                commandeObj.id_commande = reader["id_commande"].ToString();
                commandeObj.id_client = reader["id_client"].ToString();
                commandeObj.id_cuisinier = reader["id_cuisinier"].ToString();
                commandeObj.id_plat = reader["id_plat"].ToString();
                commandeObj.date_commande = reader["date_commande"].ToString();
                commandeObj.prix_total = Convert.ToDouble(reader["prix_total"].ToString());
                commandeObj.statut = reader["statut"].ToString();
                listeCommandes.Add(commandeObj);
            }
            reader.Close();
            commande.Dispose();

            // livraison
            requete = "SELECT * FROM Livraison;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Livraison livraison = new Livraison();
                livraison.id_livraison = reader["id_livraison"].ToString();
                livraison.id_commande = reader["id_commande"].ToString();
                livraison.date_livraison = reader["date_livraison"].ToString();
                livraison.trajet = reader["trajet"].ToString();
                listeLivraisons.Add(livraison);
            }
            reader.Close();
            commande.Dispose();

            // creation de l'objet final
            DonneesImportees donnees = new DonneesImportees();
            donnees.utilisateurs = listeUtilisateurs;
            donnees.clients = listeClients;
            donnees.cuisiniers = listeCuisiniers;
            donnees.commandes = listeCommandes;
            donnees.livraisons = listeLivraisons;

            XmlSerializer serializer = new XmlSerializer(typeof(DonneesImportees));
            using (FileStream fs = new FileStream("../../Données/SQL.xml", FileMode.Create))
            {
                serializer.Serialize(fs, donnees);
            }
            Console.WriteLine("export sql.xml ok");
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

