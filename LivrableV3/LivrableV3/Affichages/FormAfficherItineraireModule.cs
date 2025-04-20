using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormAfficherItineraireModule : Form
    {
        FormModules module ;
        Graphe<int> graphe;
        GestionnaireItineraire<int> gestionnaireItineraire;
        ChargerFichiers chargerFichiers;

        public FormAfficherItineraireModule(FormModules module, Graphe<int> graphe)
        {
            InitializeComponent();
            this.chargerFichiers = new ChargerFichiers();
            this.module = module;
            this.graphe = graphe;
            this.gestionnaireItineraire = new GestionnaireItineraire<int>(graphe);
            Chargerstations();
        }

        private void Chargerstations()
        {
            comboBoxArrivee.Items.Clear();
            comboBoxDepart.Items.Clear();
            List<string> stations = chargerFichiers.ChargerStation();

            for (int i = 0; i < stations.Count; i++)
            {
                comboBoxDepart.Items.Add(stations[i].ToString());
                comboBoxArrivee.Items.Add(stations[i].ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            this.Close();
            module.Show();
        }

        private void listBoxDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormAfficherItineraire_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            // on recupere les noms des stations
            string nomDepart = comboBoxDepart.Text;
            string nomArrivee = comboBoxArrivee.Text;

            // on cherche les id des stations
            int idDepart = graphe.TrouverIdParNom(nomDepart);
            int idArrivee = graphe.TrouverIdParNom(nomArrivee);

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
                visItineraire.DessinerItineraire(graphe, itineraire, texteItineraire);
                visItineraire.SauvegarderImage("itinerairemodule.png");
                

                using (var stream = new MemoryStream(File.ReadAllBytes("itinerairemodule.png")))
                {
                    pictureBoxItineraire.Image = Image.FromStream(stream);
                }
            }
            else
            {
                MessageBox.Show("pas de chemin trouve", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxrep.Text = "Itineraire de " + nomDepart + " a " + nomArrivee +"\r\n"+ gestionnaireItineraire.detail;

        }

        private void comboBoxDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxArrivee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxItineraire_Click(object sender, EventArgs e)
        {

        }

        private void labelitineraire_Click(object sender, EventArgs e)
        {

        }

    }
}
