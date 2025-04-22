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
                string plat = comboBoxchoixplat.SelectedItem.ToString();
                string nombre = comboBoxnombre.Text;
                double nombreint = Convert.ToDouble(comboBoxnombre.Text);
                string nomcuisto = sqlCommander.ConnaitreCuisinier(plat);
                string idPlat = sqlCommander.GetIdPlat(plat);

                // Étape 1 : récupérer id_cuisinier et prix du plat
                string requetePlat = "SELECT id_cuisinier, prix_par_personne FROM Plat_ WHERE id_plat = '" + idPlat + "'";
                MySqlCommand commandePlat = new MySqlCommand(requetePlat, connexionBDDClient.maConnexionClient);
                MySqlDataReader readerPlat = commandePlat.ExecuteReader();

                if (readerPlat.Read())
                {
                    string idCuisinier = readerPlat["id_cuisinier"].ToString();
                    double prix = Convert.ToDouble(readerPlat["prix_par_personne"])*nombreint;
                    
                    readerPlat.Close();
                    commandePlat.Dispose();

                    // Étape 2 : récupérer id_client à partir de l'utilisateur connecté
                    string idClient = null;
                    string requeteClient = "SELECT id_client FROM client WHERE id_utilisateur = '" + authentification.idUtilisateur + "'";
                    MySqlCommand cmdClient = new MySqlCommand(requeteClient, connexionBDDClient.maConnexionClient);
                    MySqlDataReader readerClient = cmdClient.ExecuteReader();
                    if (readerClient.Read())
                    {
                        idClient = readerClient["id_client"].ToString();
                    }
                    readerClient.Close();
                    cmdClient.Dispose();

                    if (idClient == null)
                    {
                        MessageBox.Show("Impossible de trouver le client associé à l'utilisateur connecté.", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Étape 3 : insérer la commande
                    string idCommande = "CMD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    string requeteCommande = "INSERT INTO Commande_ VALUES ('" + idCommande + "', '" + idClient + "', '" + idCuisinier + "', '" + idPlat + "', '" +
                                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + prix.ToString().Replace(',', '.') + ", 'En attente')";

                    MySqlCommand commandeInsert = new MySqlCommand(requeteCommande, connexionBDDClient.maConnexionClient);
                    commandeInsert.ExecuteNonQuery();
                    commandeInsert.Dispose();

                    Console.WriteLine("votre commande a été passée avec succès");
                    MessageBox.Show("votre commande a été passée avec succès", "commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.WriteLine("numéro de commande : " + idCommande);
                    MessageBox.Show("numéro de commande : " + idCommande, "commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    readerPlat.Close();
                    commandePlat.Dispose();
                    MessageBox.Show("Ce plat n'existe pas", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oups, une erreur est survenue : " + ex.Message, "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            btnRetour_Click(sender, e);

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

            this.stationArrivée = authentification.stationMetro;
            this.stationDepart = sqlCommander.ConnaitreStationCuisinier(platSelectionne);

            int idArrivee = grapheMetro.TrouverIdParNom(stationDepart);
            int idDepart = grapheMetro.TrouverIdParNom(stationArrivée);

            List<Noeud<int>> itineraire = gestionnaireItineraire.RechercherItineraire(idDepart.ToString(), idArrivee.ToString());

            textBoxprix.Text = "  " + sqlCommander.ConnaitrePrix(platSelectionne)+ " € ";

            double temps = gestionnaireItineraire.tempsTotal;

            textBoxtemps.Text = " " + temps + " min ";
            textBoxcuisinier.Text = sqlCommander.ConnaitreCuisinier(platSelectionne);

            try
            {
                string imageName = "resto.jpeg"; 

                if (platSelectionne == "Boeuf Bourguignon Maison") imageName = "BoeufBourguignon.jpg";
                else if (platSelectionne == "Risotto Forestier") imageName = "Risotto.jpg";
                else if (platSelectionne == "Paella Royale") imageName = "Paella.jpg";
                else if (platSelectionne == "Tarte Tatin Traditionnelle") imageName = "tarte.jpg";
                else if (platSelectionne == "Curry Vert au Poulet") imageName = "curry.jpg";

                string cheminImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images", imageName);
                cheminImage = Path.GetFullPath(cheminImage); 

                if (File.Exists(cheminImage))
                {
                    using (var stream = new MemoryStream(File.ReadAllBytes(cheminImage)))
                    {
                        pictureBoxphotoplat.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    MessageBox.Show("Image non trouvée : " + cheminImage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnvoiritineraire_Click(object sender, EventArgs e)
        {

            FormAfficheritineraire formAfficheritineraire = new FormAfficheritineraire(this, gestionnaireItineraire);

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

        private void comboBoxnombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            double nombre = Convert.ToDouble(comboBoxnombre.Text);
            string platSelectionne = comboBoxchoixplat.SelectedItem.ToString();
            textBoxprix.Text = "  " + sqlCommander.ConnaitrePrix(platSelectionne) * nombre + " € ";
        }
    }
}
