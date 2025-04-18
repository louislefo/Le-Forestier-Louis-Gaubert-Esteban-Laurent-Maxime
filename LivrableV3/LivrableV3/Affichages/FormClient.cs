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
        private Graphe<int> grapheMetro;
        

        /// constructeur du formulaire client
        public FormClient(ConnexionBDDClient connexion, Authentification auth, Graphe<int> graphe)
        {
            InitializeComponent();
            connexionBDDClient = connexion;
            authentification = auth;
            grapheMetro = graphe;
            labelNom.Text = "Bonjour " + authentification.nom;
        }

        private void InitializeComponent()
        {
            this.labelTitre = new System.Windows.Forms.Label();
            this.BtnDeconnexion = new System.Windows.Forms.Button();
            this.labelNom = new System.Windows.Forms.Label();
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
            this.labelNom.Location = new System.Drawing.Point(500, 35);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(84, 25);
            this.labelNom.TabIndex = 2;
            this.labelNom.Text = "Bonjour";
            // 
            // FormClient
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
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
        }

        private void labelTitre_Click(object sender, EventArgs e)
        {

        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }
    }
} 