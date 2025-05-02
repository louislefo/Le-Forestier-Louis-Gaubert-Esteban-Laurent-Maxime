using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormCuisinier : Form
    {
        private ConnexionBDDCuisinier connexionBDDCuisinier;
        private Authentification authentification;
        private Graphe<int> grapheMetro;
        private TabControl tabControl;
        private TabPage tabCommandes;
        private TabPage tabPlats;
        private Button btnDeconnexion;
        private Label labelTitre;
        private Label labelNom;
        private TabPage tabProfil;
        private Button btnvoircommandes;
        private Button btnvoirmenu;
        private Button btnajouterplat;
        private Button btnvoiritineraire;
        private TextBox textBoxrep;
        private Button btnvoirnotes;
        private ComboBox comboBoxnumcommande;
        private Label label1;
        private MainForm mainForm;
        private FormAjoutplat formAjouterPlat;
        private SqlCuisinier sqlCuisinier;
        private SqlCommander sqlCommander;
        private Button btnvalider;
        private ComboBox comboBoxcomandes;
        private ComboBox comboBoxstatut;
        private Label label2;
        private Label label3;
        private GestionnaireItineraire<int> gestionnaireItineraire;

        public FormCuisinier(ConnexionBDDCuisinier connexion, Authentification auth, Graphe<int> graphe,MainForm main)
        {
            InitializeComponent();
            connexionBDDCuisinier = connexion;
            authentification = auth;
            grapheMetro = graphe;
            this.gestionnaireItineraire = new GestionnaireItineraire<int>(grapheMetro);
            labelNom.Text = "Bonjour " + authentification.nom + " " + authentification.prenom;
            this.mainForm = main;
            this.sqlCuisinier = new SqlCuisinier(connexionBDDCuisinier,authentification);
            RemplirBox(sqlCuisinier.VoiridCommandes(sqlCuisinier.GetIdCuisinierFromUtilisateur(authentification.idUtilisateur)));
        }

       
        private void InitializeComponent()
        {
            this.btnDeconnexion = new System.Windows.Forms.Button();
            this.labelTitre = new System.Windows.Forms.Label();
            this.labelNom = new System.Windows.Forms.Label();
            this.btnvoircommandes = new System.Windows.Forms.Button();
            this.btnvoirmenu = new System.Windows.Forms.Button();
            this.btnajouterplat = new System.Windows.Forms.Button();
            this.btnvoiritineraire = new System.Windows.Forms.Button();
            this.textBoxrep = new System.Windows.Forms.TextBox();
            this.btnvoirnotes = new System.Windows.Forms.Button();
            this.comboBoxnumcommande = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnvalider = new System.Windows.Forms.Button();
            this.comboBoxcomandes = new System.Windows.Forms.ComboBox();
            this.comboBoxstatut = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDeconnexion
            // 
            this.btnDeconnexion.Location = new System.Drawing.Point(20, 15);
            this.btnDeconnexion.Name = "btnDeconnexion";
            this.btnDeconnexion.Size = new System.Drawing.Size(158, 43);
            this.btnDeconnexion.TabIndex = 0;
            this.btnDeconnexion.Text = "Deconnexion";
            this.btnDeconnexion.UseVisualStyleBackColor = true;
            this.btnDeconnexion.Click += new System.EventHandler(this.btnDeconnexion_Click);
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Location = new System.Drawing.Point(297, 41);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(138, 25);
            this.labelTitre.TabIndex = 1;
            this.labelTitre.Text = "MenuCuisinier";
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNom.Location = new System.Drawing.Point(522, 45);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(67, 20);
            this.labelNom.TabIndex = 2;
            this.labelNom.Text = "Bonjour";
            this.labelNom.Click += new System.EventHandler(this.labelNom_Click);
            // 
            // btnvoircommandes
            // 
            this.btnvoircommandes.Location = new System.Drawing.Point(43, 265);
            this.btnvoircommandes.Name = "btnvoircommandes";
            this.btnvoircommandes.Size = new System.Drawing.Size(179, 73);
            this.btnvoircommandes.TabIndex = 3;
            this.btnvoircommandes.Text = "Voir mes commandes";
            this.btnvoircommandes.UseVisualStyleBackColor = true;
            this.btnvoircommandes.Click += new System.EventHandler(this.btnvoircommandes_Click);
            // 
            // btnvoirmenu
            // 
            this.btnvoirmenu.Location = new System.Drawing.Point(43, 547);
            this.btnvoirmenu.Name = "btnvoirmenu";
            this.btnvoirmenu.Size = new System.Drawing.Size(179, 66);
            this.btnvoirmenu.TabIndex = 4;
            this.btnvoirmenu.Text = "Voir mon menu";
            this.btnvoirmenu.UseVisualStyleBackColor = true;
            this.btnvoirmenu.Click += new System.EventHandler(this.btnvoirmenu_Click);
            // 
            // btnajouterplat
            // 
            this.btnajouterplat.Location = new System.Drawing.Point(42, 168);
            this.btnajouterplat.Name = "btnajouterplat";
            this.btnajouterplat.Size = new System.Drawing.Size(179, 65);
            this.btnajouterplat.TabIndex = 5;
            this.btnajouterplat.Text = "Ajouter un plat";
            this.btnajouterplat.UseVisualStyleBackColor = true;
            this.btnajouterplat.Click += new System.EventHandler(this.btnajouterplat_Click);
            // 
            // btnvoiritineraire
            // 
            this.btnvoiritineraire.Location = new System.Drawing.Point(43, 445);
            this.btnvoiritineraire.Name = "btnvoiritineraire";
            this.btnvoiritineraire.Size = new System.Drawing.Size(179, 71);
            this.btnvoiritineraire.TabIndex = 6;
            this.btnvoiritineraire.Text = "Voir mes itineraires";
            this.btnvoiritineraire.UseVisualStyleBackColor = true;
            this.btnvoiritineraire.Click += new System.EventHandler(this.btnvoiritineraire_Click);
            // 
            // textBoxrep
            // 
            this.textBoxrep.Location = new System.Drawing.Point(257, 265);
            this.textBoxrep.Multiline = true;
            this.textBoxrep.Name = "textBoxrep";
            this.textBoxrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxrep.Size = new System.Drawing.Size(497, 448);
            this.textBoxrep.TabIndex = 7;
            // 
            // btnvoirnotes
            // 
            this.btnvoirnotes.Location = new System.Drawing.Point(43, 648);
            this.btnvoirnotes.Name = "btnvoirnotes";
            this.btnvoirnotes.Size = new System.Drawing.Size(179, 65);
            this.btnvoirnotes.TabIndex = 8;
            this.btnvoirnotes.Text = "Voir mes notes";
            this.btnvoirnotes.UseVisualStyleBackColor = true;
            this.btnvoirnotes.Click += new System.EventHandler(this.btnvoirnotes_Click);
            // 
            // comboBoxnumcommande
            // 
            this.comboBoxnumcommande.FormattingEnabled = true;
            this.comboBoxnumcommande.Location = new System.Drawing.Point(43, 391);
            this.comboBoxnumcommande.Name = "comboBoxnumcommande";
            this.comboBoxnumcommande.Size = new System.Drawing.Size(178, 33);
            this.comboBoxnumcommande.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "commande :";
            // 
            // btnvalider
            // 
            this.btnvalider.Location = new System.Drawing.Point(584, 128);
            this.btnvalider.Name = "btnvalider";
            this.btnvalider.Size = new System.Drawing.Size(180, 65);
            this.btnvalider.TabIndex = 11;
            this.btnvalider.Text = "valider";
            this.btnvalider.UseVisualStyleBackColor = true;
            this.btnvalider.Click += new System.EventHandler(this.btnvalider_Click);
            // 
            // comboBoxcomandes
            // 
            this.comboBoxcomandes.FormattingEnabled = true;
            this.comboBoxcomandes.Location = new System.Drawing.Point(353, 128);
            this.comboBoxcomandes.Name = "comboBoxcomandes";
            this.comboBoxcomandes.Size = new System.Drawing.Size(198, 33);
            this.comboBoxcomandes.TabIndex = 12;
            this.comboBoxcomandes.SelectedIndexChanged += new System.EventHandler(this.comboBoxcomandes_SelectedIndexChanged);
            // 
            // comboBoxstatut
            // 
            this.comboBoxstatut.FormattingEnabled = true;
            this.comboBoxstatut.Items.AddRange(new object[] {
            "En attente",
            "Validé",
            "En préparation",
            "livrée",
            "Annulée"});
            this.comboBoxstatut.Location = new System.Drawing.Point(352, 167);
            this.comboBoxstatut.Name = "comboBoxstatut";
            this.comboBoxstatut.Size = new System.Drawing.Size(199, 33);
            this.comboBoxstatut.TabIndex = 13;
            this.comboBoxstatut.SelectedIndexChanged += new System.EventHandler(this.comboBoxstatut_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 14;
            this.label2.Text = "commandes :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "statut :";
            // 
            // FormCuisinier
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxstatut);
            this.Controls.Add(this.comboBoxcomandes);
            this.Controls.Add(this.btnvalider);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxnumcommande);
            this.Controls.Add(this.btnvoirnotes);
            this.Controls.Add(this.textBoxrep);
            this.Controls.Add(this.btnvoiritineraire);
            this.Controls.Add(this.btnajouterplat);
            this.Controls.Add(this.btnvoirmenu);
            this.Controls.Add(this.btnvoircommandes);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.labelTitre);
            this.Controls.Add(this.btnDeconnexion);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormCuisinier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormCuisinier_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormCuisinier_Load(object sender, EventArgs e)
        {

        }
        private void RemplirBox(List<string> listecommande)
        {
            comboBoxnumcommande.Items.Clear();
            comboBoxcomandes.Items.Clear();
            if (listecommande == null || listecommande.Count == 0)
            {
                textBoxrep.Text = "pas de commande en cours";
                return;
            }
            else
            {
                for (int i = 0; i < listecommande.Count; i++)
                {
                    comboBoxnumcommande.Items.Add(listecommande[i]);
                    comboBoxcomandes.Items.Add(listecommande[i]);
                }
            }
            
        }

        private void btnDeconnexion_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void btnvoircommandes_Click(object sender, EventArgs e)
        {
            string idcuisto = sqlCuisinier.GetIdCuisinierFromUtilisateur( authentification.idUtilisateur );
            textBoxrep.Text = sqlCuisinier.VoirCommandesEnCours(idcuisto);
        }
        

        private void btnvoirmenu_Click(object sender, EventArgs e)
        {
            textBoxrep.Text = sqlCuisinier.VoirMesPlats(sqlCuisinier.GetIdCuisinierFromUtilisateur(authentification.idUtilisateur));
        }

        private void btnvoiritineraire_Click(object sender, EventArgs e)
        {

            FormItineraireCuisinier formAfficheritineraire = new FormItineraireCuisinier(this , gestionnaireItineraire);


            string stationDepart = authentification.stationMetro;
            string sationArrivée = sqlCuisinier.ConnaitreStationClient(comboBoxnumcommande.Text);

            int idArrivee = grapheMetro.TrouverIdParNom(stationDepart);
            int idDepart = grapheMetro.TrouverIdParNom(sationArrivée);


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
                visItineraire.SauvegarderImage("itinerairecuisinier.png");

            }
            else
            {
                MessageBox.Show("pas de chemin trouve", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            formAfficheritineraire.Show();
            this.Hide();
        }

        private void btnajouterplat_Click(object sender, EventArgs e)
        {
            FormAjoutplat formAjouterPlat = new FormAjoutplat(connexionBDDCuisinier, authentification, this);
            formAjouterPlat.Show();
            this.Hide();

        }

        private void labelNom_Click(object sender, EventArgs e)
        {

        }

        private void btnvoirnotes_Click(object sender, EventArgs e)
        {
            textBoxrep.Text = sqlCuisinier.Voirmesnotes(sqlCuisinier.GetIdCuisinierFromUtilisateur(authentification.idUtilisateur));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxcomandes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnvalider_Click(object sender, EventArgs e)
        {
            try
            {
                string idcommande = comboBoxcomandes.Text;
                string statut = comboBoxstatut.Text;
                if (string.IsNullOrEmpty(idcommande) || string.IsNullOrEmpty(statut))
                {
                    MessageBox.Show("Veuillez sélectionner une commande et un statut.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sqlCuisinier.ModifierStatutCommande(idcommande, statut);
                MessageBox.Show("Statut de la commande modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification du statut : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxstatut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
} 