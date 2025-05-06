using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    public partial class MainForm : Form
    {
        public ConnexionBDD connexionBDD;
        public Authentification authentification;
        public Graphe<int> grapheMetro;
        private ChargerFichiers chargeur;

        private ImportJson  import;
        private ImportXml importXml;
        private string cheminFichierMetro = @"../../Données/MetroParisNoeuds.csv";
        private string cheminFichierArcs = @"../../Données/MetroParisArcs.csv";

        /// constructeur de la fenetre principale
        public MainForm()
        {
            InitializeComponent();
            InitialiserApplication();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// initialisation des elements de l'application
        private void InitialiserApplication()
        {
            try
            {
                connexionBDD = new ConnexionBDD();
                grapheMetro = new Graphe<int>();
                chargeur = new ChargerFichiers();
                ChargerDonneesMetro();
                authentification = new Authentification(connexionBDD, grapheMetro);
                this.import = new ImportJson(connexionBDD);
                this.importXml = new ImportXml(connexionBDD);


            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur lors de l initialisation : " + ex.Message);
            }
        }

        /// chargement des donnees du metro
        private void ChargerDonneesMetro()
        {
            Dictionary<int, Noeud<int>> noeudsMetro = chargeur.ChargerNoeudsMetro(cheminFichierMetro);

            foreach (int id in noeudsMetro.Keys)
            {
                grapheMetro.Noeuds[id] = noeudsMetro[id];
            }

            chargeur.ChargerArcsMetro(grapheMetro, cheminFichierArcs);
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            FormConnexion formConnexion = new FormConnexion(authentification,this);
            formConnexion.Show();
            this.Hide();

        }

        private void btnquitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnModule_Click(object sender, EventArgs e)
        {
            bool demanderMotDePasse = DemanderMotDePasseAdmin(); 
            
            if (demanderMotDePasse)
            {
                FormModules formModules = new FormModules(connexionBDD, grapheMetro, this);
                formModules.Show();
                this.Hide();

            }
            
        }

        private void btninscription_Click(object sender, EventArgs e)
        {
            FormInscription formInscription = new FormInscription(authentification,connexionBDD,grapheMetro,this);
            formInscription.Show();
            this.Hide();    

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnColoration_Click(object sender, EventArgs e)
        {
            try
            {
                // on teste la coloration sur le graphe des clients et cuisiniers avec les données de la base
                TestColorationClientsMetro.TesterColorationClientsMetro(connexionBDD, grapheMetro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage du graphe : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool DemanderMotDePasseAdmin()
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Mot de passe requis",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label label = new Label() { Left = 20, Top = 20, Text = "Entrez le mot de passe :", Width = 280 };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 280, PasswordChar = '*' };

            Button btnOK = new Button() { Text = "Valider", Left = 200, Width = 100, Top = 85, DialogResult = DialogResult.OK };
            btnOK.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(label);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(btnOK);
            prompt.AcceptButton = btnOK;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                if (inputBox.Text == "Admin")
                {
                    return true; 
                }
                else
                {
                    MessageBox.Show("Mot de passe incorrect !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return false; 
        }

        private void btncolo_Click(object sender, EventArgs e)
        {
            try
            {
                // on teste la coloration sur le graphe des clients et cuisiniers
                TestColorationClientsMetro.TesterColorationClientsMetro(connexionBDD, grapheMetro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage du graphe : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
