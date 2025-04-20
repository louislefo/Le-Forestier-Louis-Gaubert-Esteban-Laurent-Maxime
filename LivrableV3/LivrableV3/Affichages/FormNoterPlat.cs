using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LivrableV3.Affichages
{
    public partial class FormNoterPlat : Form
    {
        private ConnexionBDDClient connexionBDDClient;
        private Authentification authentification;
        private FormClient formClient;
        private SqlCommander sqlCommander;
        private Graphe<int> grapheMetro;
        public FormNoterPlat(ConnexionBDDClient connexionBDDClient, Authentification authentification, FormClient formClient)
        {
            InitializeComponent();
            this.connexionBDDClient = connexionBDDClient;
            this.authentification = authentification;
            this.formClient = formClient;
            Rempirelistecommandes();
            Rempirelisteplat();
            this.grapheMetro = formClient.grapheMetro;
            sqlCommander = new SqlCommander(connexionBDDClient, authentification, grapheMetro);
        }

        private void FormNoterPlat_Load(object sender, EventArgs e)
        {

        }

        private void btnretour_Click(object sender, EventArgs e)
        {
            this.Close();
            formClient.Show();
        }

        private void btnenvoyer_Click(object sender, EventArgs e)
        {
            string plat = comboBoxnomplat.Text;
            string idCommande = comboBoxcommande.Text;
            string note = textBoxNote.Text;
            string commentaire = textBoxcommentaire.Text;
            try
            {
                string idPlat = sqlCommander.GetIdPlat(plat);
                string idCuisinier = sqlCommander.ConnaitreIdCuisinier(plat);
                string idClient = sqlCommander.GetIdClientFromUtilisateur(authentification.idUtilisateur); 

                string idAvis = "AVI" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string datePublication = DateTime.Now.ToString("yyyy-MM-dd");

                string requete = "INSERT INTO Avis_ VALUES ('" + idAvis + "', '" + idClient + "', '" + idCuisinier + "', '" + idCommande + "', " +
                                 note.Replace(',', '.') + ", '" + commentaire + "', '" + datePublication + "')";

                MySqlCommand cmd = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                MessageBox.Show("Votre avis a bien été enregistré, merci !", "Avis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi de l'avis : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnretour_Click(sender, e);

        }
        private void Rempirelistecommandes()
        {
            List<string> commandes = listecommandes();
            comboBoxcommande.Items.Clear();
            for (int i = 0; i < commandes.Count; i++)
            {
                string commande = commandes[i];
                if (commande.Contains(" "))
                {
                    commande = commande.Substring(0, commande.IndexOf(" "));
                }
                comboBoxcommande.Items.Add(commande);

            }
        }
        private void Rempirelisteplat()
        {
            comboBoxnomplat.Items.Clear();
            try
            {
                string requete = "SELECT Plat_.nom as NomPlat " +
                               "FROM Plat_, cuisinier, utilisateur " +
                               "WHERE Plat_.id_cuisinier = cuisinier.id_cuisinier " +
                               "AND cuisinier.id_utilisateur = utilisateur.id_utilisateur";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxnomplat.Items.Add(reader["NomPlat"].ToString());
                }

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }


        }
        private List<string> listecommandes()
        {
            List<string> commandes = new List<string>();

            try
            {
                string requete = "SELECT Commande_.id_commande, Commande_.date_commande, Commande_.prix_total, Commande_.statut, " +
                               "Plat_.nom as nom_plat, utilisateur.nom as nom_cuisinier, utilisateur.prénom as prenom_cuisinier " +
                               "FROM Commande_, client, Plat_, cuisinier, utilisateur " +
                               "WHERE client.id_utilisateur = '" + authentification.idUtilisateur + "' " +
                               "AND Commande_.id_client = client.id_client " +
                               "AND Commande_.id_plat = Plat_.id_plat " +
                               "AND Commande_.id_cuisinier = cuisinier.id_cuisinier " +
                               "AND cuisinier.id_utilisateur = utilisateur.id_utilisateur " +
                               "ORDER BY Commande_.date_commande DESC";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDDClient.maConnexionClient);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();
                

                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string nomPlat = reader["nom_plat"].ToString();

                    commandes.Add(idCommande+"");
                }

                reader.Close();
                commande.Dispose();
                return commandes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
                return null;
            }

            
        }
    }
}
