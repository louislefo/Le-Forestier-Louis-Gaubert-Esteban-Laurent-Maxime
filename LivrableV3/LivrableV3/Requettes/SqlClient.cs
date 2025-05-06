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
                string requete = "SELECT Commande_.id_commande, Commande_.date_commande, Commande_.prix_total, Commande_.statut, " +
                               "Plat_.nom as nom_plat, utilisateur.nom as nom_cuisinier, utilisateur.prénom as prenom_cuisinier " +
                               "FROM Commande_, client, Plat_, cuisinier, utilisateur " +
                               "WHERE client.id_utilisateur = '" + idClient + "' " +
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

        public string VoirCuisiniersDisponibles()
        {
            try
            {
                string requete = "SELECT c.id_cuisinier, u.nom, u.prénom, c.StationMetro, " +
                                 "p.nom AS nom_plat, p.prix_par_personne, " +
                                 "a.note, a.commentaire " +
                                 "FROM cuisinier c " +
                                 "JOIN utilisateur u ON c.id_utilisateur = u.id_utilisateur " +
                                 "JOIN Plat_ p ON p.id_cuisinier = c.id_cuisinier " +
                                 "LEFT JOIN Avis_ a ON a.id_cuisinier = c.id_cuisinier " +
                                 "ORDER BY c.id_cuisinier, p.nom";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "\nVoici les cuisiniers disponibles\r\n";
                rep += "--------------------------------\r\n";

                string dernierId = "";
                while (reader.Read())
                {
                    string idCuisinier = reader["id_cuisinier"].ToString();

                    if (idCuisinier != dernierId)
                    {
                        string nom = reader["nom"].ToString();
                        string prenom = reader["prénom"].ToString();
                        string station = reader["StationMetro"].ToString();

                        rep += "Cuisinier : " + idCuisinier + "\r\n";
                        rep += "Nom : " + prenom + " " + nom + "\r\n";
                        rep += "Station : " + station + "\r\n";
                        dernierId = idCuisinier;
                    }

                    string nomPlat = reader["nom_plat"].ToString();
                    string prix = reader["prix_par_personne"].ToString();
                    rep += "  - Plat : " + nomPlat + " | Prix : " + prix + "€\r\n";

                    string note = reader["note"] != DBNull.Value ? reader["note"].ToString() : "—";
                    string commentaire = reader["commentaire"] != DBNull.Value ? reader["commentaire"].ToString() : "Aucun commentaire";
                    rep += "    > Avis : " + note + "/5 - " + commentaire + "\r\n";
                    rep += "\r\n--------------------------------\r\n";
                }

                reader.Close();
                commande.Dispose();

                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des cuisiniers : " + ex.Message);
                return null;
            }
        }

        public string VoirHistoriqueClient(string idUtilisateur)
        {
            try
            {
                string requete = "SELECT c.id_commande, c.date_commande, c.prix_total, c.statut, " +
                 "p.nom AS nom_plat, u.nom AS nom_cuisinier, u.prénom AS prenom_cuisinier " +
                 "FROM commande_ c " +
                 "JOIN plat_ p ON c.id_plat = p.id_plat " +
                 "JOIN cuisinier cuis ON c.id_cuisinier = cuis.id_cuisinier " +
                 "JOIN utilisateur u ON cuis.id_utilisateur = u.id_utilisateur " +
                 "JOIN client cl ON c.id_client = cl.id_client " +
                 "WHERE cl.id_utilisateur = '" + idUtilisateur + "' " +
                 "AND c.statut = 'Livrée' " +
                 "ORDER BY c.date_commande DESC;";

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
