using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    public class BaseDeDonnees
    {
        public List<Utilisateur> Utilisateurs { get; set; }
        public List<Station> Stations { get; set; }
        public List<Client> Clients { get; set; }
        public List<Cuisinier> Cuisiniers { get; set; }
        public List<Plat> Plats { get; set; }
        public List<Commande> Commandes { get; set; }
    }

    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Role { get; set; }
    }

    public class Station
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Client : Utilisateur
    {
        public int StationId { get; set; }
    }

    public class Cuisinier : Utilisateur
    {
        public int StationId { get; set; }
    }

    public class Plat
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }
    }

    public class Commande
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PlatId { get; set; }
        public int Quantite { get; set; }
        public string Statut { get; set; }
    }

    public class ImportXml
    {
        public string cheminFichierXml;
        public ConnexionBDD connexion;

        public ImportXml(ConnexionBDD connexionBase)
        {
            this.cheminFichierXml = "../../Données/Données.Xml";
            this.connexion = connexionBase;
            ExporterToutSqlEnXml();

        }

        public void ExporterToutSqlEnXml()
        {
            /// je vais recuperer les donnees de la base et les mettre dans un xml
            List<Utilisateur> listeUtilisateurs = new List<Utilisateur>();
            List<Station> listeStations = new List<Station>();
            List<Client> listeClients = new List<Client>();
            List<Cuisinier> listeCuisiniers = new List<Cuisinier>();
            List<Plat> listePlats = new List<Plat>();
            List<Commande> listeCommandes = new List<Commande>();

            // utilisateur
            string requete = "SELECT * FROM utilisateur;";
            MySqlCommand commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Utilisateur utilisateur = new Utilisateur();
                int id = 0;
                int.TryParse(reader["id_utilisateur"].ToString(), out id);
                utilisateur.Id = id;
                utilisateur.Nom = reader["nom"].ToString();
                utilisateur.Prenom = reader["prénom"].ToString();
                utilisateur.Email = reader["email"].ToString();
                utilisateur.MotDePasse = reader["mot_de_passe"].ToString();
                utilisateur.Role = "";
                listeUtilisateurs.Add(utilisateur);
            }
            reader.Close();
            commande.Dispose();

            // station (exemple, car pas dans la base, on laisse vide)
            // tu peux remplir avec des fausses stations si tu veux tester

            // client
            requete = "SELECT * FROM client;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Client client = new Client();
                int id = 0;
                int.TryParse(reader["id_client"].ToString(), out id);
                client.Id = id;
                client.Nom = "";
                client.Prenom = "";
                client.Email = "";
                client.MotDePasse = "";
                client.Role = "";
                int stationId = 0;
                int.TryParse(reader["StationMetro"].ToString(), out stationId);
                client.StationId = stationId;
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
                int id = 0;
                int.TryParse(reader["id_cuisinier"].ToString(), out id);
                cuisinier.Id = id;
                cuisinier.Nom = "";
                cuisinier.Prenom = "";
                cuisinier.Email = "";
                cuisinier.MotDePasse = "";
                cuisinier.Role = "";
                int stationId = 0;
                int.TryParse(reader["StationMetro"].ToString(), out stationId);
                cuisinier.StationId = stationId;
                listeCuisiniers.Add(cuisinier);
            }
            reader.Close();
            commande.Dispose();

            // plat
            requete = "SELECT * FROM Plat_;";
            commande = new MySqlCommand(requete, connexion.maConnexion);
            commande.CommandText = requete;
            reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Plat plat = new Plat();
                int id = 0;
                int.TryParse(reader["id_plat"].ToString(), out id);
                plat.Id = id;
                plat.Nom = reader["nom"].ToString();
                double prix = 0;
                double.TryParse(reader["prix_par_personne"].ToString(), out prix);
                plat.Prix = prix;
                listePlats.Add(plat);
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
                int id = 0;
                int.TryParse(reader["id_commande"].ToString(), out id);
                commandeObj.Id = id;
                int clientId = 0;
                int.TryParse(reader["id_client"].ToString(), out clientId);
                commandeObj.ClientId = clientId;
                int platId = 0;
                int.TryParse(reader["id_plat"].ToString(), out platId);
                commandeObj.PlatId = platId;
                commandeObj.Quantite = 1;
                commandeObj.Statut = reader["statut"].ToString();
                listeCommandes.Add(commandeObj);
            }
            reader.Close();
            commande.Dispose();

            // creation de l'objet final
            BaseDeDonnees donnees = new BaseDeDonnees();
            donnees.Utilisateurs = listeUtilisateurs;
            donnees.Stations = listeStations;
            donnees.Clients = listeClients;
            donnees.Cuisiniers = listeCuisiniers;
            donnees.Plats = listePlats;
            donnees.Commandes = listeCommandes;

            XmlSerializer serializer = new XmlSerializer(typeof(BaseDeDonnees));
            using (FileStream fs = new FileStream("../../Données/SQL.xml", FileMode.Create))
            {
                serializer.Serialize(fs, donnees);
            }
            Console.WriteLine("export sql.xml ok");
        }
    }
}

