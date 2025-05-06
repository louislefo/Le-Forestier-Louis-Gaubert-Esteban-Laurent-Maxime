using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public class SqlCuisinier
    {
        public ConnexionBDDCuisinier connexionBDD;
        private Authentification authentification;
        public List<string> listecommande;

        public SqlCuisinier(ConnexionBDDCuisinier connexionBDD,Authentification authentification)
        {
            this.connexionBDD = connexionBDD;
            this.authentification = authentification;
        }

        public string GetIdCuisinierFromUtilisateur(string idUtilisateur)
        {
            try
            {
                string requete = "SELECT id_cuisinier FROM cuisinier WHERE id_utilisateur = '" + idUtilisateur + "'";
                MySqlCommand cmd = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                MySqlDataReader reader = cmd.ExecuteReader();

                string idcuistot = null;

                if (reader.Read())
                {
                    idcuistot = reader["id_cuisinier"].ToString();
                }

                reader.Close();
                cmd.Dispose();

                return idcuistot;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération de l'identifiant cuisinier : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string VoirMesPlats(string idCuisinier)
        {
            try
            {
                string requete = "SELECT id_plat, nom, type, portions, date_fabrication, date_peremption, " +
                               "prix_par_personne, nationalite, regime FROM Plat_ " +
                               "WHERE id_cuisinier = '" + idCuisinier + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "\nvoici vos plats\r\n";

                Console.WriteLine("\nvoici vos plats");
                rep += "----------------------------------\r\n";
                Console.WriteLine("----------------------------------");

                while (reader.Read())
                {
                    string idPlat = reader["id_plat"].ToString();
                    string nom = reader["nom"].ToString();
                    string type = reader["type"].ToString();
                    string portions = reader["portions"].ToString();
                    string dateFabrication = Convert.ToDateTime(reader["date_fabrication"]).ToShortDateString();
                    string datePeremption = Convert.ToDateTime(reader["date_peremption"]).ToShortDateString();
                    double prix = Convert.ToDouble(reader["prix_par_personne"]);
                    string nationalite = reader["nationalite"].ToString();
                    string regime = reader["regime"].ToString();

                    rep += "Plat " + idPlat + " : " + nom + "\r\n";
                    Console.WriteLine("Plat " + idPlat + " : " + nom);
                    rep += "Type : " + type + "\r\n";
                    Console.WriteLine("Type : " + type);
                    rep += "Portions : " + portions + "\r\n";
                    Console.WriteLine("Portions : " + portions);
                    rep += "Fabrication : " + dateFabrication + "\r\n";
                    Console.WriteLine("Fabrication : " + dateFabrication);
                    rep += "Peremption : " + datePeremption + "\r\n";
                    Console.WriteLine("Peremption : " + datePeremption);
                    rep += "Prix : " + prix + " euros\r\n";
                    Console.WriteLine("Prix : " + prix + " euros");
                    rep += "Nationalite : " + nationalite + "\r\n";
                    Console.WriteLine("Nationalite : " + nationalite);
                    rep += "Regime : " + regime + "\r\n";
                    Console.WriteLine("Regime : " + regime);
                    rep += "----------------------------------\r\n";
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

        public List<string> VoiridCommandes(string idCuisinier)
        {
            try
            {
                List<string> listecommande = new List<string>();

                string requete = "SELECT c.id_commande, c.date_commande, c.prix_total, c.statut, " +
                                 "p.nom as nom_plat, u.nom as nom_client, u.prénom as prenom_client " +
                                 "FROM Commande_ c " +
                                 "JOIN Plat_ p ON c.id_plat = p.id_plat " +
                                 "JOIN client cl ON c.id_client = cl.id_client " +
                                 "JOIN utilisateur u ON cl.id_utilisateur = u.id_utilisateur " +
                                 "WHERE c.id_cuisinier = '" + idCuisinier + "' " +
                                 "AND c.statut != 'Terminée'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string dateCommande = Convert.ToDateTime(reader["date_commande"]).ToString();
                    double prix = Convert.ToDouble(reader["prix_total"]);
                    string statut = reader["statut"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();
                    string nomClient = reader["nom_client"].ToString();
                    string prenomClient = reader["prenom_client"].ToString();

                    Console.WriteLine("Commande " + idCommande);
                    Console.WriteLine("Date : " + dateCommande);
                    Console.WriteLine("Plat : " + nomPlat);
                    Console.WriteLine("Client : " + prenomClient + " " + nomClient);
                    Console.WriteLine("Prix : " + prix + " euros");
                    Console.WriteLine("Statut : " + statut);
                    Console.WriteLine("----------------------------------");

                    listecommande.Add(idCommande);
                }

                reader.Close();
                commande.Dispose();
                return listecommande;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }

        public string VoirCommandesEnCours(string idCuisinier)
        {
            try
            {
                string requete = "SELECT c.id_commande, c.date_commande, c.prix_total, c.statut, " +
                               "p.nom as nom_plat, u.nom as nom_client, u.prénom as prenom_client " +
                               "FROM Commande_ c " +
                               "JOIN Plat_ p ON c.id_plat = p.id_plat " +
                               "JOIN client cl ON c.id_client = cl.id_client " +
                               "JOIN utilisateur u ON cl.id_utilisateur = u.id_utilisateur " +
                               "WHERE c.id_cuisinier = '" + idCuisinier + "' " +
                               "AND c.statut != 'Terminée'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "\nvoici vos commandes en cours\r\n";

                Console.WriteLine("\nvoici vos commandes en cours");
                rep += "----------------------------------\r\n";
                Console.WriteLine("----------------------------------");

                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string dateCommande = Convert.ToDateTime(reader["date_commande"]).ToString();
                    double prix = Convert.ToDouble(reader["prix_total"]);
                    string statut = reader["statut"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();
                    string nomClient = reader["nom_client"].ToString();
                    string prenomClient = reader["prenom_client"].ToString();

                    rep += "Commande " + idCommande + "\r\n";
                    Console.WriteLine("Commande " + idCommande);
                    rep += "Date : " + dateCommande + "\r\n";
                    Console.WriteLine("Date : " + dateCommande);
                    rep += "Plat : " + nomPlat + "\r\n";
                    Console.WriteLine("Plat : " + nomPlat);
                    rep += "Client : " + prenomClient + " " + nomClient + "\r\n";
                    Console.WriteLine("Client : " + prenomClient + " " + nomClient);
                    rep += "Prix : " + prix + " euros\r\n";
                    Console.WriteLine("Prix : " + prix + " euros");
                    rep += "Statut : " + statut + "\r\n";
                    Console.WriteLine("Statut : " + statut);
                    rep += "----------------------------------\r\n";
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

        public string ModifierStatutCommande(string idcommande, string statut)
        {
            try
            {
                string requete = "UPDATE Commande_ SET statut = '" + statut + "' WHERE id_commande = '" + idcommande + "'";

                MySqlCommand cmd = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                int lignesAffectees = cmd.ExecuteNonQuery();
                cmd.Dispose();

                if (lignesAffectees > 0)
                {
                    return "Statut de la commande modifié avec succès.";
                }
                else
                {
                    return "Aucune commande trouvée avec cet identifiant.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification du statut : " + ex.Message);
                return null;
            }
        }

        public string Voirmesnotes(string idcuisinier)
        {
            try
            {
                string requete = "SELECT note, commentaire, date_publication " +
                                 "FROM Avis_ " +
                                 "WHERE id_cuisinier = '" + idcuisinier + "' " +
                                 "AND note IS NOT NULL " +
                                 "ORDER BY date_publication DESC";

                MySqlCommand cmd = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                MySqlDataReader reader = cmd.ExecuteReader();

                string rep = "\nVoici vos avis :\r\n";
                rep += "---------------------------\r\n";

                bool hasAvis = false;

                while (reader.Read())
                {
                    hasAvis = true;

                    string note = reader["note"].ToString();
                    string commentaire = reader["commentaire"] != DBNull.Value ? reader["commentaire"].ToString() : "Aucun commentaire";
                    string date = Convert.ToDateTime(reader["date_publication"]).ToString("dd/MM/yyyy");

                    rep += $"Note : {note}/5\r\n";
                    rep += $"Commentaire : {commentaire}\r\n";
                    rep += $"Date : {date}\r\n";
                    rep += "---------------------------\r\n";
                }

                reader.Close();
                cmd.Dispose();

                if (!hasAvis)
                {
                    rep += "Aucun avis pour le moment.";
                }

                return rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des avis : " + ex.Message);
                return null;
            }
        }

        public string ConnaitreStationClient(string idcommande)
        {
            try
            {
                string requete = "SELECT cl.StationMetro " +
                                 "FROM Commande_ c " +
                                 "JOIN client cl ON c.id_client = cl.id_client " +
                                 "WHERE c.id_commande = '" + idcommande + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                MySqlDataReader reader = commande.ExecuteReader();

                string station = null;

                if (reader.Read())
                {
                    station = reader["StationMetro"].ToString();
                }

                reader.Close();
                commande.Dispose();
                return station;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération de la station du client : " + ex.Message);
                return null;
            }
        }
    }
}
