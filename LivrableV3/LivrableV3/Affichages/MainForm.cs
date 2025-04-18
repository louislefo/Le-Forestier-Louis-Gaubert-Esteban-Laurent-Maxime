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
            FormConnexion formConnexion = new FormConnexion(authentification);
            formConnexion.Show();

        }

        private void btnquitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnModule_Click(object sender, EventArgs e)
        {
            //Ajouter mdp
            FormModules formModules = new FormModules(connexionBDD,grapheMetro);
            formModules.Show();
        }

        private void btninscription_Click(object sender, EventArgs e)
        {
            FormInscription formInscription = new FormInscription(authentification,connexionBDD,grapheMetro);
            formInscription.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
