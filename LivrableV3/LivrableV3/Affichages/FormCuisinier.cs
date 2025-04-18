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

        /// constructeur du formulaire cuisinier
        public FormCuisinier(ConnexionBDDCuisinier connexion, Authentification auth, Graphe<int> graphe)
        {
            InitializeComponent();
            connexionBDDCuisinier = connexion;
            authentification = auth;
            grapheMetro = graphe;
        }

       
        private void InitializeComponent()
        {
            this.btnDeconnexion = new System.Windows.Forms.Button();
            this.labelTitre = new System.Windows.Forms.Label();
            this.labelNom = new System.Windows.Forms.Label();
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
            this.labelNom.Location = new System.Drawing.Point(516, 28);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(67, 20);
            this.labelNom.TabIndex = 2;
            this.labelNom.Text = "Bonjour";
            // 
            // FormCuisinier
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
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

    }
} 