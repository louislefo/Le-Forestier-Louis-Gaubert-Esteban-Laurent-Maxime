using System;
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
        private MainForm mainForm;

        /// constructeur du formulaire cuisinier
        public FormCuisinier(ConnexionBDDCuisinier connexion, Authentification auth, Graphe<int> graphe,MainForm main)
        {
            InitializeComponent();
            connexionBDDCuisinier = connexion;
            authentification = auth;
            grapheMetro = graphe;
            labelNom.Text = "Bonjour " + authentification.nom + " " + authentification.prenom;
            this.mainForm = main;
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
            this.btnvoircommandes.Location = new System.Drawing.Point(43, 338);
            this.btnvoircommandes.Name = "btnvoircommandes";
            this.btnvoircommandes.Size = new System.Drawing.Size(179, 73);
            this.btnvoircommandes.TabIndex = 3;
            this.btnvoircommandes.Text = "Voir mes commandes";
            this.btnvoircommandes.UseVisualStyleBackColor = true;
            this.btnvoircommandes.Click += new System.EventHandler(this.btnvoircommandes_Click);
            // 
            // btnvoirmenu
            // 
            this.btnvoirmenu.Location = new System.Drawing.Point(43, 448);
            this.btnvoirmenu.Name = "btnvoirmenu";
            this.btnvoirmenu.Size = new System.Drawing.Size(179, 66);
            this.btnvoirmenu.TabIndex = 4;
            this.btnvoirmenu.Text = "Voir mon menu";
            this.btnvoirmenu.UseVisualStyleBackColor = true;
            this.btnvoirmenu.Click += new System.EventHandler(this.btnvoirmenu_Click);
            // 
            // btnajouterplat
            // 
            this.btnajouterplat.Location = new System.Drawing.Point(289, 155);
            this.btnajouterplat.Name = "btnajouterplat";
            this.btnajouterplat.Size = new System.Drawing.Size(179, 65);
            this.btnajouterplat.TabIndex = 5;
            this.btnajouterplat.Text = "Ajouter un plat";
            this.btnajouterplat.UseVisualStyleBackColor = true;
            this.btnajouterplat.Click += new System.EventHandler(this.btnajouterplat_Click);
            // 
            // btnvoiritineraire
            // 
            this.btnvoiritineraire.Location = new System.Drawing.Point(43, 544);
            this.btnvoiritineraire.Name = "btnvoiritineraire";
            this.btnvoiritineraire.Size = new System.Drawing.Size(179, 71);
            this.btnvoiritineraire.TabIndex = 6;
            this.btnvoiritineraire.Text = "Voir mes itineraires";
            this.btnvoiritineraire.UseVisualStyleBackColor = true;
            this.btnvoiritineraire.Click += new System.EventHandler(this.btnvoiritineraire_Click);
            // 
            // textBoxrep
            // 
            this.textBoxrep.Location = new System.Drawing.Point(257, 338);
            this.textBoxrep.Multiline = true;
            this.textBoxrep.Name = "textBoxrep";
            this.textBoxrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxrep.Size = new System.Drawing.Size(497, 375);
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
            // 
            // FormCuisinier
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
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

        private void btnDeconnexion_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void btnvoircommandes_Click(object sender, EventArgs e)
        {

        }

        private void btnvoirmenu_Click(object sender, EventArgs e)
        {

        }

        private void btnvoiritineraire_Click(object sender, EventArgs e)
        {

        }

        private void btnajouterplat_Click(object sender, EventArgs e)
        {

        }

        private void labelNom_Click(object sender, EventArgs e)
        {

        }
    }
} 