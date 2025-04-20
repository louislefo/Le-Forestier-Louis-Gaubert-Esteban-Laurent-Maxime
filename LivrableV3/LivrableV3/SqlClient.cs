using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public class SqlClient
    {
        public ConnexionBDDClient connexionBDDClient;

        public SqlClient(ConnexionBDDClient connexionBDDClient)
        {
            this.connexionBDDClient = connexionBDDClient;
        }


        public string VoirPlatsDisponibles()
        {
            try
            {
                string requete = "SELECT Plat_.id_plat, Plat_.nom as nom_plat, Plat_.type, Plat_.prix_par_personne, utilisateur.nom as nom_cuisinier, utilisateur.prénom " +
                               "FROM Plat_, cuisinier, utilisateur " +
                               "WHERE Plat_.id_cuisinier = cuisinier.id_cuisinier " +
                               "AND cuisinier.id_utilisateur = utilisateur.id_utilisateur";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();
                string rep = "\nvoici les plats disponibles\r\n";
                Console.WriteLine("----------------------------------");
                rep+= "--------------------------------\r\n";

                while (reader.Read())
                {
                    string idPlat = reader["id_plat"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();
                    string type = reader["type"].ToString();
                    string prix = reader["prix_par_personne"].ToString();
                    string nomCuisinier = reader["nom_cuisinier"].ToString();
                    string prenomCuisinier = reader["prénom"].ToString();

                    rep += "Plat numero " + idPlat + "\r\n";
                    Console.WriteLine("Plat numero " + idPlat);
                    rep += "Nom: " + nomPlat + "\r\n";
                    Console.WriteLine("Nom: " + nomPlat);
                    rep += "Type: " + type + "\r\n";
                    Console.WriteLine("Type: " + type);
                    rep += "Prix: " + prix + " euros\r\n";
                    Console.WriteLine("Prix: " + prix + " euros");
                    rep += "Cuisinier: " + prenomCuisinier + " " + nomCuisinier + "\r\n";
                    Console.WriteLine("Cuisinier: " + prenomCuisinier + " " + nomCuisinier);
                    rep += "--------------------------------\r\n";
                    Console.WriteLine("----------------------------------");
                }

                
                reader.Close();
                commande.Dispose();
                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }


        public string VoirCommandesClient(string idClient)
        {
            try
            {

                string requete = "SELECT Commande_.id_commande, date_commande, prix_total, statut, Plat_.nom as nom_plat " +
                               "FROM Commande_, Plat_ " +
                               "WHERE Commande_.id_plat = Plat_.id_plat " +
                               "AND Commande_.id_client = '" + idClient + "' " +
                               "ORDER BY date_commande DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "\nvoici vos commandes\r\n";
                Console.WriteLine("\nvoici vos commandes");
                rep += "--------------------------------\r\n";
                Console.WriteLine("----------------------------------");

                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string date = reader["date_commande"].ToString();
                    string prix = reader["prix_total"].ToString();
                    string statut = reader["statut"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();

                    rep += "Commande numero " + idCommande + "\r\n";
                    Console.WriteLine("Commande numero " + idCommande);
                    rep += "Date: " + date + "\r\n";
                    Console.WriteLine("Date: " + date);
                    rep += "Plat: " + nomPlat + "\r\n";
                    Console.WriteLine("Plat: " + nomPlat);
                    rep += "Prix: " + prix + " euros\r\n";
                    Console.WriteLine("Prix: " + prix + " euros");
                    rep += "Statut: " + statut + "\r\n";
                    Console.WriteLine("Statut: " + statut);
                    rep += "--------------------------------\r\n";
                    Console.WriteLine("----------------------------------");


                }
                
                reader.Close();
                commande.Dispose();
                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }


        public void PasserCommande(string idClient)
        {
            try
            {
                Console.WriteLine("veuillez entrer l'id du plat que vous voulez commander");
                string idPlat = Console.ReadLine();

                string requetePlat = "SELECT id_cuisinier, prix_par_personne FROM Plat_ WHERE id_plat = '" + idPlat + "'";
                MySqlCommand commandePlat = new MySqlCommand(requetePlat, connexionBDDClient.maConnexionClient);
                MySqlDataReader readerPlat = commandePlat.ExecuteReader();

                if (readerPlat.Read())
                {
                    string idCuisinier = readerPlat["id_cuisinier"].ToString();
                    double prix = Convert.ToDouble(readerPlat["prix_par_personne"]);

                    readerPlat.Close();
                    commandePlat.Dispose();

                    string idCommande = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    string requeteCommande = "INSERT INTO Commande_ VALUES ('" + idCommande + "', '" + idClient + "', '" + idCuisinier + "', '" + idPlat + "', '" +
                                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + prix.ToString().Replace(',', '.') + ", 'En attente')";

                    MySqlCommand commandeInsert = new MySqlCommand(requeteCommande, connexionBDDClient.maConnexionClient);
                    commandeInsert.ExecuteNonQuery();
                    commandeInsert.Dispose();

                    Console.WriteLine("votre commande a ete passee avec succes");
                    Console.WriteLine("numero de commande : " + idCommande);
                }
                else
                {
                    readerPlat.Close();
                    commandePlat.Dispose();
                    Console.WriteLine("ce plat nexiste pas");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }
        public string VoirCuisiniersDisponibles()
        {
            try
            {
                string requete = "SELECT cuisinier.id_cuisinier, utilisateur.nom, utilisateur.prénom, cuisinier.StationMetro, Plat_.nom as nom_plat, Plat_.prix_par_personne " +
                                "FROM cuisinier, utilisateur, Plat_ " +
                                "WHERE cuisinier.id_utilisateur = utilisateur.id_utilisateur " +
                                "AND Plat_.id_cuisinier = cuisinier.id_cuisinier";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();
                string rep = "\nvoici les cuisiniers disponibles\r\n";
                Console.WriteLine("----------------------------------");
                rep += "--------------------------------\r\n";

                while (reader.Read())
                {
                    string idCuisinier = reader["id_cuisinier"].ToString();
                    string nom = reader["nom"].ToString();
                    string prenom = reader["prénom"].ToString();
                    string station = reader["StationMetro"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();
                    string prix = reader["prix_par_personne"].ToString();

                    rep += "Cuisinier numero " + idCuisinier + "\r\n";
                    Console.WriteLine("Cuisinier numero " + idCuisinier);
                    rep += "Nom: " + prenom + " " + nom + "\r\n";
                    Console.WriteLine("Nom: " + prenom + " " + nom);
                    rep += "Station de metro: " + station + "\r\n";
                    Console.WriteLine("Station de metro: " + station);
                    rep += "Plat propose: " + nomPlat + "\r\n";
                    Console.WriteLine("Plat propose: " + nomPlat);
                    rep += "Prix: " + prix + " euros\r\n";
                    Console.WriteLine("Prix: " + prix + " euros");
                    rep += "--------------------------------\r\n";
                    Console.WriteLine("----------------------------------");
                }

                reader.Close();
                commande.Dispose();
                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }

        public string VoirHistoriqueClient(string idUtilisateur)
        {
            try
            {
                string requete = "SELECT Commande_.id_commande, Commande_.date_commande, Commande_.prix_total, Commande_.statut, " +
                               "Plat_.nom as nom_plat, utilisateur.nom as nom_cuisinier, utilisateur.prénom as prenom_cuisinier " +
                               "FROM Commande_, client, Plat_, cuisinier, utilisateur " +
                               "WHERE client.id_utilisateur = '" + idUtilisateur + "' " +
                               "AND Commande_.id_client = client.id_client " +
                               "AND Commande_.id_plat = Plat_.id_plat " +
                               "AND Commande_.id_cuisinier = cuisinier.id_cuisinier " +
                               "AND cuisinier.id_utilisateur = utilisateur.id_utilisateur " +
                               "ORDER BY Commande_.date_commande DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();
                string rep = "\nvoici votre historique de commandes\r\n";
                Console.WriteLine("----------------------------------");
                rep += "--------------------------------\r\n";

                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string date = reader["date_commande"].ToString();
                    string prix = reader["prix_total"].ToString();
                    string statut = reader["statut"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();
                    string nomCuisinier = reader["nom_cuisinier"].ToString();
                    string prenomCuisinier = reader["prenom_cuisinier"].ToString();

                    rep += "Commande numero " + idCommande + "\r\n";
                    Console.WriteLine("Commande numero " + idCommande);
                    rep += "Date: " + date + "\r\n";
                    Console.WriteLine("Date: " + date);
                    rep += "Plat: " + nomPlat + "\r\n";
                    Console.WriteLine("Plat: " + nomPlat);
                    rep += "Cuisinier: " + prenomCuisinier + " " + nomCuisinier + "\r\n";
                    Console.WriteLine("Cuisinier: " + prenomCuisinier + " " + nomCuisinier);
                    rep += "Prix: " + prix + " euros\r\n";
                    Console.WriteLine("Prix: " + prix + " euros");
                    rep += "Statut: " + statut + "\r\n";
                    Console.WriteLine("Statut: " + statut);
                    rep += "--------------------------------\r\n";
                    Console.WriteLine("----------------------------------");
                }

                reader.Close();
                commande.Dispose();
                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }
    }
}
