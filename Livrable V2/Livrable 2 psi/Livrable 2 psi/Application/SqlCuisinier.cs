using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    public class SqlCuisinier
    {
        public ConnexionBDD connexionBDD;

        public SqlCuisinier(ConnexionBDD connexionBDD)
        {
            this.connexionBDD = connexionBDD;
        }

        

        public void AjouterPlat(string idCuisinier)
        {
            try
            {
                Console.WriteLine("\nAjout d'un nouveau plat");
                Console.WriteLine("----------------------");

                Console.Write("nom du plat : ");
                string nomPlat = Console.ReadLine();

                Console.Write("type de plat (entree, plat, dessert) : ");
                string typePlat = Console.ReadLine();

                Console.Write("nombre de portions : ");
                string portions = Console.ReadLine();

                Console.Write("date de fabrication (jj/mm/aaaa) : ");
                DateTime dateFabrication = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("date de peremption (jj/mm/aaaa) : ");
                DateTime datePeremption = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("prix par personne (en euros) : ");
                double prixParPersonne = Convert.ToDouble(Console.ReadLine());

                Console.Write("nationalite du plat : ");
                string nationalite = Console.ReadLine();

                Console.Write("regime (normal, vegetarien, vegan) : ");
                string regime = Console.ReadLine();

                Console.Write("photo (nom du fichier) : ");
                string photo = Console.ReadLine();

                // on cree un id de plat
                string idPlat = "PLT" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // on ajoute le plat
                string requete = "INSERT INTO Plat_ (id_plat, id_cuisinier, nom, type, portions, date_fabrication, " + 
                               "date_peremption, prix_par_personne, nationalite, regime, photo) " +
                               "VALUES ('" + idPlat + "', '" + idCuisinier + "', '" + nomPlat + "', '" + typePlat + "', '" + 
                               portions + "', '" + dateFabrication.ToString("yyyy-MM-dd") + "', '" + 
                               datePeremption.ToString("yyyy-MM-dd") + "', " + prixParPersonne.ToString().Replace(',', '.') + 
                               ", '" + nationalite + "', '" + regime + "', '" + photo + "')";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();

                Console.WriteLine("plat ajoute avec succes");
            }
            catch (Exception ex)
            {
                Console.WriteLine("oups ya une erreur : " + ex.Message);
            }
        }

        /// affiche les plats dun cuisinier
        public void VoirMesPlats(string idCuisinier)
        {
            try
            {
                string requete = "SELECT id_plat, nom, type, portions, date_fabrication, date_peremption, " + 
                               "prix_par_personne, nationalite, regime FROM Plat_ " +
                               "WHERE id_cuisinier = '" + idCuisinier + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nvoici vos plats");
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

                    Console.WriteLine("Plat " + idPlat + " : " + nom);
                    Console.WriteLine("Type : " + type);
                    Console.WriteLine("Portions : " + portions);
                    Console.WriteLine("Fabrication : " + dateFabrication);
                    Console.WriteLine("Peremption : " + datePeremption);
                    Console.WriteLine("Prix : " + prix + " euros");
                    Console.WriteLine("Nationalite : " + nationalite);
                    Console.WriteLine("Regime : " + regime);
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

        /// affiche les commandes en cours dun cuisinier
        public void VoirCommandesEnCours(string idCuisinier)
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

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                Console.WriteLine("\nvoici vos commandes en cours");
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

                    Console.WriteLine("Commande " + idCommande);
                    Console.WriteLine("Date : " + dateCommande);
                    Console.WriteLine("Plat : " + nomPlat);
                    Console.WriteLine("Client : " + prenomClient + " " + nomClient);
                    Console.WriteLine("Prix : " + prix + " euros");
                    Console.WriteLine("Statut : " + statut);
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
    }
}
