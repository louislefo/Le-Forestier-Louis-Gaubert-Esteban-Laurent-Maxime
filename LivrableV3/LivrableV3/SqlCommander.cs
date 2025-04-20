using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace LivrableV3
{
    public class SqlCommander
    {
        private ConnexionBDDClient connexionBDDClient;
        private Authentification authentificationclient;
        private Graphe<int> grapheMetro;
        private string stationArrivée;
        private string stationDepart;

        public SqlCommander(ConnexionBDDClient connexionBDDClient, Authentification authentification, Graphe<int> graphe)
        {
            this.connexionBDDClient = connexionBDDClient;
            this.authentificationclient = authentification;
            this.grapheMetro = graphe;
        }

        /// recupere le prix dun plat
        public double ConnaitrePrix(string plat)
        {
            try
            {
                string idplat = GetIdPlat(plat);
                if (idplat == null)
                {
                    return -1;
                }

                string requete = "SELECT prix_par_personne FROM Plat_ WHERE id_plat = '" + idplat + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();
                double prix = -1;

                if (reader.Read())
                {
                    prix = Convert.ToDouble(reader["prix_par_personne"]);
                }

                reader.Close();
                commande.Dispose();
                return prix;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return -1;
            }
        }

        

        /// trouve lid du plat avec son nom
        private string GetIdPlat(string plat)
        {
            try
            {
                string requete = "SELECT id_plat FROM Plat_ WHERE nom = '" + plat + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();
                string idPlat = null;

                if (reader.Read())
                {
                    idPlat = reader["id_plat"].ToString();
                }

                reader.Close();
                commande.Dispose();
                return idPlat;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }
        /// trouve la station du cuisinier qui fait le plat
        public string ConnaitreStationCuisinier(string platSelectionne)
        {
            try
            {
                string idplat = GetIdPlat(platSelectionne);
                if (idplat == null)
                {
                    return null;
                }

                string requete = "SELECT cuisinier.StationMetro " +
                               "FROM Plat_, cuisinier " +
                               "WHERE Plat_.id_cuisinier = cuisinier.id_cuisinier " +
                               "AND Plat_.id_plat = '" + idplat + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

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
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }
    }
}
