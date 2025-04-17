using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_PSI
{
    
    public class SqlClient
    {
        public ConnexionBDDClient connexionBDDClient;

        public SqlClient(ConnexionBDDClient connexionBDDClient)
        {
            this.connexionBDDClient = connexionBDDClient;
        }

        
        public void VoirPlatsDisponibles()
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

                Console.WriteLine("\nvoici les plats disponibles");
                Console.WriteLine("----------------------------------");

                while (reader.Read())
                {
                    string idPlat = reader["id_plat"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();
                    string type = reader["type"].ToString();
                    string prix = reader["prix_par_personne"].ToString();
                    string nomCuisinier = reader["nom_cuisinier"].ToString();
                    string prenomCuisinier = reader["prénom"].ToString();

                    Console.WriteLine("Plat numero " + idPlat);
                    Console.WriteLine("Nom: " + nomPlat);
                    Console.WriteLine("Type: " + type);
                    Console.WriteLine("Prix: " + prix + " euros");
                    Console.WriteLine("Cuisinier: " + prenomCuisinier + " " + nomCuisinier);
                    Console.WriteLine("----------------------------------");
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }

       
        public void VoirCommandesClient(string idClient)
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

                Console.WriteLine("\nvoici vos commandes");
                Console.WriteLine("----------------------------------");

                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string date = reader["date_commande"].ToString();
                    string prix = reader["prix_total"].ToString();
                    string statut = reader["statut"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();

                    Console.WriteLine("Commande numero " + idCommande);
                    Console.WriteLine("Date: " + date);
                    Console.WriteLine("Plat: " + nomPlat);
                    Console.WriteLine("Prix: " + prix + " euros");
                    Console.WriteLine("Statut: " + statut);
                    Console.WriteLine("----------------------------------");
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
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
    }
}
