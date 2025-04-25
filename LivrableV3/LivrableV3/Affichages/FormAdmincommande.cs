using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormAdmincommande : Form
    {
        private ConnexionBDD connexionBDD;
        private FormModules formModules;
        private ChargerFichiers chargerFichiers;
        public FormAdmincommande(FormModules formModules, ConnexionBDD connexionBDD)
        {
            InitializeComponent();
            this.connexionBDD = connexionBDD;
            this.formModules = formModules;
            this.chargerFichiers = new ChargerFichiers();


            RemplirComboBoxplat();
            RemplirComboBoxcommande();
            RemplirComboBoxclient();
            btnmodifier.Hide();
        }

        private void FormAdmincommande_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxchoixplat_SelectedIndexChanged(object sender, EventArgs e)
        {

            string platSelectionne = comboBoxchoixplat.SelectedItem.ToString();
            textBoxcuisinier.Text = ConnaitreCuisinier(platSelectionne);

            
        }
        private void RemplirComboBoxplat()
        {
            try
            {
                string requete = "SELECT Plat_.nom as NomPlat " +
                               "FROM Plat_, cuisinier, utilisateur " +
                               "WHERE Plat_.id_cuisinier = cuisinier.id_cuisinier " +
                               "AND cuisinier.id_utilisateur = utilisateur.id_utilisateur";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxchoixplat.Items.Add(reader["NomPlat"].ToString());
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
        }
        private void RemplirComboBoxclient()
        {
            
            try
            {
                string requete = "SELECT id_client from client ; ";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxclient.Items.Add(reader["id_client"].ToString());
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
        }

        private void RemplirComboBoxcommande()
        {
            try
            {
                string requete = "SELECT id_commande " +
                               "FROM Commande_ ;";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxcommande.Items.Add(reader["id_commande"].ToString());
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
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
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
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
        public string GetIdPlat(string plat)
        {
            try
            {
                string requete = "SELECT id_plat FROM Plat_ WHERE nom = '" + plat + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
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

        public string ConnaitreCuisinier(string platSelectionne)
        {
            try
            {
                string cuisinier = null;
                string idplat = GetIdPlat(platSelectionne);

                if (idplat != null)
                {
                    string requete = "SELECT u.nom, u.prénom " +
                                     "FROM Plat_ p " +
                                     "JOIN cuisinier c ON p.id_cuisinier = c.id_cuisinier " +
                                     "JOIN utilisateur u ON c.id_utilisateur = u.id_utilisateur " +
                                     "WHERE p.id_plat = '" + idplat + "';";

                    MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                    MySqlDataReader reader = commande.ExecuteReader();

                    if (reader.Read())
                    {
                        string nom = reader["nom"].ToString();
                        string prenom = reader["prénom"].ToString();
                        cuisinier = nom + " " + prenom;
                    }

                    reader.Close();
                    commande.Dispose();
                }

                return cuisinier;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oups, une erreur est survenue : " + ex.Message);
                return null;
            }

        }

        public string ConnaitreIdCuisinier(string platSelectionne)
        {
            try
            {
                string idplat = GetIdPlat(platSelectionne);
                if (idplat == null)
                {
                    return null;
                }
                string requete = "SELECT id_cuisinier FROM Plat_ WHERE id_plat = '" + idplat + "'";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;
                MySqlDataReader reader = commande.ExecuteReader();
                string idCuisinier = null;
                if (reader.Read())
                {
                    idCuisinier = reader["id_cuisinier"].ToString();
                }
                reader.Close();
                commande.Dispose();
                return idCuisinier;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }
        }
        public string GetIdClientFromUtilisateur(string idUtilisateur)
        {
            try
            {
                string requete = "SELECT id_client FROM client WHERE id_utilisateur = '" + idUtilisateur + "'";
                MySqlCommand cmd = new MySqlCommand(requete, connexionBDD.maConnexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                string idClient = null;

                if (reader.Read())
                {
                    idClient = reader["id_client"].ToString();
                }

                reader.Close();
                cmd.Dispose();

                return idClient;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération de l'identifiant client : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public string NomPlatDepuisId(string idPlat)
        {
            string nomPlat = "";

            try
            {
                string requete = "SELECT nom FROM Plat_ WHERE id_plat = '" + idPlat + "'";
                MySqlCommand cmd = new MySqlCommand(requete, connexionBDD.maConnexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    nomPlat = reader["nom"].ToString();
                }

                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération du nom du plat : " + ex.Message);
            }

            return nomPlat;
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

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
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
                MessageBox.Show("oups ya une erreur je ne trouve pas la station cuisto: " + ex.Message);
                return null;
            }
        }

        private void btnretour_Click(object sender, EventArgs e)
        {
            this.Close();
            formModules.Show();
             
        }

        private void comboBoxcommande_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnajouter.Hide();
            btnmodifier.Show();

            string idcomande = comboBoxcommande.SelectedItem.ToString();
            try
            {
                string idCommande = comboBoxcommande.SelectedItem.ToString();
                string requete = "SELECT * FROM Commande_ WHERE id_commande = '" + idCommande + "'";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                MySqlDataReader reader = commande.ExecuteReader();

                if (reader.Read())
                {
                    string idClient = reader["id_client"].ToString();
                    string idCuisinier = reader["id_cuisinier"].ToString();
                    string idPlat = reader["id_plat"].ToString();
                    string dateCommande = reader["date_commande"].ToString();
                    string prix = reader["prix_total"].ToString();
                    string statut = reader["statut"].ToString();

                    comboBoxclient.SelectedItem = idClient;
                    comboBoxchoixplat.SelectedItem = NomPlatDepuisId(idPlat); 
                    comboBoxstatut.Text = statut;
                    textBoxprix.Text = prix;
                    textBoxcuisinier.Text = ConnaitreCuisinier(NomPlatDepuisId(idPlat));
                    comboBoxstatut.Text = statut;

                }

                reader.Close();
                commande.Dispose();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }

            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            comboBoxcommande.SelectedIndex = -1;
            btnajouter.Show();
            btnmodifier.Hide();
        }

        private void btnmodifier_Click(object sender, EventArgs e)
        {

            // on recupere les infos de la commande
            string idCommande = comboBoxcommande.SelectedItem.ToString();
            string idClient = comboBoxclient.SelectedItem.ToString();
            string idCuisinier = ConnaitreIdCuisinier(comboBoxchoixplat.SelectedItem.ToString());
            string idPlat = GetIdPlat(comboBoxchoixplat.SelectedItem.ToString());
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            double prix = ConnaitrePrix(comboBoxchoixplat.SelectedItem.ToString());
            string statut = comboBoxstatut.Text;

            
            try
            {
                try
                {

                    string requeteUpdateCommande = "UPDATE Commande_ SET IdClient = '" + idClient + "', IdCuisinier = '" + idCuisinier + "', IdPlat = '" + idPlat + "', DateCommande = '" + date + "', Prix = " + prix + ", Statut = '" + statut + "' WHERE IdCommande = '" + idCommande + "'";

                    MySqlCommand commande = new MySqlCommand(requeteUpdateCommande, connexionBDD.maConnexion);
                    commande.CommandText = requeteUpdateCommande;

                    MySqlDataReader reader = commande.ExecuteReader();

                    reader.Close();
                    commande.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("oups ya une erreur : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }

        }

        private void btnajouter_Click(object sender, EventArgs e)
        {
            // on recupere les infos de la commande
            string idCommande = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string idClient = comboBoxclient.SelectedItem.ToString();
            string idCuisinier = ConnaitreIdCuisinier(comboBoxchoixplat.SelectedItem.ToString());
            string idPlat = GetIdPlat(comboBoxchoixplat.SelectedItem.ToString());
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            double prix = ConnaitrePrix(comboBoxchoixplat.SelectedItem.ToString());
            string statut = comboBoxstatut.Text;

            try
            {
                
                string requeteCommande = "INSERT INTO Commande_ VALUES ('" + idCommande + "', '" + idClient + "', '" + idCuisinier + "', '" + idPlat + "', '" +
                                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + prix.ToString().Replace(',', '.') + ", '"+statut+"')";

                MySqlCommand commande = new MySqlCommand(requeteCommande, connexionBDD.maConnexion);
                commande.CommandText = requeteCommande;

                MySqlDataReader reader = commande.ExecuteReader();
                
                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }

        }
    }
}
