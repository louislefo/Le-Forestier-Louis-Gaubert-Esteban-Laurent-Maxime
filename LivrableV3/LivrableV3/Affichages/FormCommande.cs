using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormCommande : Form
    {
        private ConnexionBDDClient connexionBDDClient;
        private Authentification authentification;
        private Graphe<int> grapheMetro;
        private SqlCommander sqlCommander;
        private FormClient formClient;

        GestionnaireItineraire<int> gestionnaireItineraire;
        List<Noeud<int>> Chemin;

        private string stationArrivée;
        private string stationDepart;
        public FormCommande(ConnexionBDDClient connexionBDDClient,Authentification authentification, Graphe<int> grapheMetro, FormClient formClient)
        {
            InitializeComponent();
            this.formClient = formClient;
            this.connexionBDDClient = connexionBDDClient;
            this.authentification = authentification;
            RemplirComboBox();
            this.grapheMetro = grapheMetro;
            sqlCommander = new SqlCommander(connexionBDDClient, authentification, grapheMetro);
            gestionnaireItineraire = new GestionnaireItineraire<int>(grapheMetro);
            
        }

        private void btncommander_Click(object sender, EventArgs e)
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

                    string requeteCommande = "INSERT INTO Commande_ VALUES ('" + idCommande + "', '" + authentification.idUtilisateur + "', '" + idCuisinier + "', '" + idPlat + "', '" +
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

        private void FormCommande_Load(object sender, EventArgs e)
        {

        }
        private void RemplirComboBox()
        {
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

        private void comboBoxchoixplat_SelectedIndexChanged(object sender, EventArgs e)
        {

            string platSelectionne = comboBoxchoixplat.SelectedItem.ToString();

            stationArrivée = authentification.stationMetro; 
            stationDepart = sqlCommander.ConnaitreStationCuisinier(platSelectionne); 

            textBoxprix.Text = "  "+sqlCommander.ConnaitrePrix(platSelectionne)+" € ";
            double temps = gestionnaireItineraire.tempsTotal;
            textBoxtemps.Text = " "+temps + " min ";
        }

        private void btnvoiritineraire_Click(object sender, EventArgs e)
        {

            
            try
            {
                this.Chemin = gestionnaireItineraire.RechercherItineraire(stationDepart, stationArrivée);
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }

            FormAfficheritineraire formAfficheritineraire = new FormAfficheritineraire(this, gestionnaireItineraire);
            // on cherche les id des stations
            int idArrivee = grapheMetro.TrouverIdParNom(stationDepart);
            int idDepart = grapheMetro.TrouverIdParNom(stationArrivée);
            

            // on verifie que les stations existent
            if (idDepart == -1 || idArrivee == -1)
            {
                MessageBox.Show("station pas trouvee", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // on cherche l itineraire avec le gestionnaire
            List<Noeud<int>> itineraire = gestionnaireItineraire.RechercherItineraire(idDepart.ToString(), idArrivee.ToString());

            // si on a trouve un itineraire on cree l image
            if (itineraire != null && itineraire.Count > 0)
            {
                VisualisationItineraire visItineraire = new VisualisationItineraire(1200, 800);
                string texteItineraire = "Itineraire de " + itineraire[0].ToString() + " a " + itineraire[itineraire.Count - 1].ToString();
                visItineraire.DessinerItineraire(grapheMetro, itineraire, texteItineraire);
                visItineraire.SauvegarderImage("itineraire.png");

            }
            else
            {
                MessageBox.Show("pas de chemin trouve", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            formAfficheritineraire.Show();
            this.Hide();

        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            this.Close();
            formClient.Show();
        }

        
    }
}
