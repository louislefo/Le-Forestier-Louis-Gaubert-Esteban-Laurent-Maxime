using LivrableV3.Affichages;
using System;
using System.Windows.Forms;


namespace LivrableV3
{
    public partial class FormClient : Form
    {
        private ConnexionBDDClient connexionBDDClient;
        private Authentification authentification;
        private Label labelTitre;
        private Button BtnDeconnexion;
        private Label labelNom;
        public Graphe<int> grapheMetro;
        private Button btncommander;
        private Button btnVoircomandes;
        private Button btnnoterplat;
        private Button btnhistorique;
        private TextBox textBoxrep;
        private Button btnvoircuisiniers;
        private MainForm mainForm;
        private Button btnvoirplats;
        private SqlClient sqlClient;
        private FormNoterPlat formNoterPlat;


        /// constructeur du formulaire client
        public FormClient(ConnexionBDDClient connexion, Authentification auth, Graphe<int> graphe, MainForm main)
        {
            InitializeComponent();
            connexionBDDClient = connexion;
            authentification = auth;
            grapheMetro = graphe;
            labelNom.Text = "Bonjour " + authentification.nom+ " "+ authentification.prenom;
            this.mainForm = main;
            sqlClient = new SqlClient(connexionBDDClient);
        }

        private void InitializeComponent()
        {
            this.labelTitre = new System.Windows.Forms.Label();
            this.BtnDeconnexion = new System.Windows.Forms.Button();
            this.labelNom = new System.Windows.Forms.Label();
            this.btncommander = new System.Windows.Forms.Button();
            this.btnVoircomandes = new System.Windows.Forms.Button();
            this.btnnoterplat = new System.Windows.Forms.Button();
            this.btnhistorique = new System.Windows.Forms.Button();
            this.textBoxrep = new System.Windows.Forms.TextBox();
            this.btnvoircuisiniers = new System.Windows.Forms.Button();
            this.btnvoirplats = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Location = new System.Drawing.Point(297, 42);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(112, 25);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "MenuClient";
            this.labelTitre.Click += new System.EventHandler(this.labelTitre_Click);
            // 
            // BtnDeconnexion
            // 
            this.BtnDeconnexion.Location = new System.Drawing.Point(19, 25);
            this.BtnDeconnexion.Name = "BtnDeconnexion";
            this.BtnDeconnexion.Size = new System.Drawing.Size(139, 42);
            this.BtnDeconnexion.TabIndex = 1;
            this.BtnDeconnexion.Text = "Deconnexion";
            this.BtnDeconnexion.UseVisualStyleBackColor = true;
            this.BtnDeconnexion.Click += new System.EventHandler(this.BtnDeconnexion_Click);
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNom.Location = new System.Drawing.Point(509, 47);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(67, 20);
            this.labelNom.TabIndex = 2;
            this.labelNom.Text = "Bonjour";
            this.labelNom.Click += new System.EventHandler(this.labelNom_Click);
            // 
            // btncommander
            // 
            this.btncommander.Location = new System.Drawing.Point(302, 156);
            this.btncommander.Name = "btncommander";
            this.btncommander.Size = new System.Drawing.Size(146, 59);
            this.btncommander.TabIndex = 3;
            this.btncommander.Text = "Commander";
            this.btncommander.UseVisualStyleBackColor = true;
            this.btncommander.Click += new System.EventHandler(this.btncommander_Click);
            // 
            // btnVoircomandes
            // 
            this.btnVoircomandes.Location = new System.Drawing.Point(39, 364);
            this.btnVoircomandes.Name = "btnVoircomandes";
            this.btnVoircomandes.Size = new System.Drawing.Size(144, 80);
            this.btnVoircomandes.TabIndex = 4;
            this.btnVoircomandes.Text = "Voir mes commandes";
            this.btnVoircomandes.UseVisualStyleBackColor = true;
            this.btnVoircomandes.Click += new System.EventHandler(this.btnVoircomandes_Click);
            // 
            // btnnoterplat
            // 
            this.btnnoterplat.Location = new System.Drawing.Point(39, 474);
            this.btnnoterplat.Name = "btnnoterplat";
            this.btnnoterplat.Size = new System.Drawing.Size(144, 66);
            this.btnnoterplat.TabIndex = 5;
            this.btnnoterplat.Text = "Noter le plat";
            this.btnnoterplat.UseVisualStyleBackColor = true;
            this.btnnoterplat.Click += new System.EventHandler(this.btnnoterplat_Click);
            // 
            // btnhistorique
            // 
            this.btnhistorique.Location = new System.Drawing.Point(39, 570);
            this.btnhistorique.Name = "btnhistorique";
            this.btnhistorique.Size = new System.Drawing.Size(144, 67);
            this.btnhistorique.TabIndex = 6;
            this.btnhistorique.Text = "Voir l\'historique";
            this.btnhistorique.UseVisualStyleBackColor = true;
            this.btnhistorique.Click += new System.EventHandler(this.btnhistorique_Click);
            // 
            // textBoxrep
            // 
            this.textBoxrep.Location = new System.Drawing.Point(216, 268);
            this.textBoxrep.Multiline = true;
            this.textBoxrep.Name = "textBoxrep";
            this.textBoxrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxrep.Size = new System.Drawing.Size(530, 451);
            this.textBoxrep.TabIndex = 7;
            // 
            // btnvoircuisiniers
            // 
            this.btnvoircuisiniers.Location = new System.Drawing.Point(39, 652);
            this.btnvoircuisiniers.Name = "btnvoircuisiniers";
            this.btnvoircuisiniers.Size = new System.Drawing.Size(144, 67);
            this.btnvoircuisiniers.TabIndex = 8;
            this.btnvoircuisiniers.Text = "Voir les cuisiniers";
            this.btnvoircuisiniers.UseVisualStyleBackColor = true;
            this.btnvoircuisiniers.Click += new System.EventHandler(this.btnvoircuisiniers_Click);
            // 
            // btnvoirplats
            // 
            this.btnvoirplats.Location = new System.Drawing.Point(39, 268);
            this.btnvoirplats.Name = "btnvoirplats";
            this.btnvoirplats.Size = new System.Drawing.Size(144, 65);
            this.btnvoirplats.TabIndex = 9;
            this.btnvoirplats.Text = "Voir tous les plats";
            this.btnvoirplats.UseVisualStyleBackColor = true;
            this.btnvoirplats.Click += new System.EventHandler(this.btnvoirplats_Click);
            // 
            // FormClient
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.btnvoirplats);
            this.Controls.Add(this.btnvoircuisiniers);
            this.Controls.Add(this.textBoxrep);
            this.Controls.Add(this.btnhistorique);
            this.Controls.Add(this.btnnoterplat);
            this.Controls.Add(this.btnVoircomandes);
            this.Controls.Add(this.btncommander);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.BtnDeconnexion);
            this.Controls.Add(this.labelTitre);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnDeconnexion_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void labelTitre_Click(object sender, EventArgs e)
        {
            labelTitre.Text = "tu as trouvé l'easter egg";
        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }

        private void labelNom_Click(object sender, EventArgs e)
        {

        }

        private void btnhistorique_Click(object sender, EventArgs e)
        {
            textBoxrep.Text = sqlClient.VoirHistoriqueClient(authentification.idUtilisateur);
        }

        private void btnVoircomandes_Click(object sender, EventArgs e)
        {
            textBoxrep.Text = sqlClient.VoirCommandesClient(authentification.idUtilisateur);
        }

        private void btnvoirplats_Click(object sender, EventArgs e)
        {
           textBoxrep.Text =  sqlClient.VoirPlatsDisponibles();
        }

        private void btnvoircuisiniers_Click(object sender, EventArgs e)
        {
            textBoxrep.Text = sqlClient.VoirCuisiniersDisponibles();
        }

        private void btncommander_Click(object sender, EventArgs e)
        {
            FormCommande formCommande = new FormCommande(connexionBDDClient, authentification, grapheMetro, this);
            formCommande.Show();
            this.Hide();
        }

        private void btnnoterplat_Click(object sender, EventArgs e)
        {
            FormNoterPlat formNoterPlat = new FormNoterPlat(connexionBDDClient, authentification, this);
            formNoterPlat.Show();
            this.Hide();

        }
    }
} 