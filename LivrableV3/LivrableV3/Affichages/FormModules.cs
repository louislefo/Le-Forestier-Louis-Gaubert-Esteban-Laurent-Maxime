using System;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormModules : Form
    {
        private ConnexionBDD connexionBDD;
        private TabControl tabModule;
        private TabPage tabModuleClient;
        private TabPage tabModuleCuisinier;
        private Button btnRetourMenu;
        private TabPage tabModuleCommande;
        private TabPage tabModuleStatistiques;
        private TabPage tabModuleGraphe;
        private Button btnAfficherMetro;
        private Graphe<int> grapheMetro;

        public FormModules(ConnexionBDD connexion, Graphe<int> graphe)
        {
            InitializeComponent();
            connexionBDD = connexion;
            grapheMetro = graphe;
        }

        private void InitializeComponent()
        {
            this.tabModule = new System.Windows.Forms.TabControl();
            this.tabModuleClient = new System.Windows.Forms.TabPage();
            this.tabModuleCuisinier = new System.Windows.Forms.TabPage();
            this.tabModuleCommande = new System.Windows.Forms.TabPage();
            this.tabModuleStatistiques = new System.Windows.Forms.TabPage();
            this.tabModuleGraphe = new System.Windows.Forms.TabPage();
            this.btnAfficherMetro = new System.Windows.Forms.Button();
            this.btnRetourMenu = new System.Windows.Forms.Button();
            this.tabModule.SuspendLayout();
            this.tabModuleGraphe.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabModule
            // 
            this.tabModule.Controls.Add(this.tabModuleClient);
            this.tabModule.Controls.Add(this.tabModuleCuisinier);
            this.tabModule.Controls.Add(this.tabModuleCommande);
            this.tabModule.Controls.Add(this.tabModuleStatistiques);
            this.tabModule.Controls.Add(this.tabModuleGraphe);
            this.tabModule.Location = new System.Drawing.Point(53, 55);
            this.tabModule.Name = "tabModule";
            this.tabModule.SelectedIndex = 0;
            this.tabModule.Size = new System.Drawing.Size(710, 611);
            this.tabModule.TabIndex = 0;
            // 
            // tabModuleClient
            // 
            this.tabModuleClient.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleClient.Location = new System.Drawing.Point(4, 25);
            this.tabModuleClient.Name = "tabModuleClient";
            this.tabModuleClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleClient.Size = new System.Drawing.Size(702, 582);
            this.tabModuleClient.TabIndex = 0;
            this.tabModuleClient.Text = "moduleClient";
            // 
            // tabModuleCuisinier
            // 
            this.tabModuleCuisinier.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleCuisinier.Location = new System.Drawing.Point(4, 25);
            this.tabModuleCuisinier.Name = "tabModuleCuisinier";
            this.tabModuleCuisinier.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleCuisinier.Size = new System.Drawing.Size(702, 582);
            this.tabModuleCuisinier.TabIndex = 1;
            this.tabModuleCuisinier.Text = "ModuleCuisinier";
            // 
            // tabModuleCommande
            // 
            this.tabModuleCommande.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleCommande.Location = new System.Drawing.Point(4, 25);
            this.tabModuleCommande.Name = "tabModuleCommande";
            this.tabModuleCommande.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleCommande.Size = new System.Drawing.Size(702, 582);
            this.tabModuleCommande.TabIndex = 2;
            this.tabModuleCommande.Text = "ModuleCommande";
            // 
            // tabModuleStatistiques
            // 
            this.tabModuleStatistiques.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleStatistiques.Location = new System.Drawing.Point(4, 25);
            this.tabModuleStatistiques.Name = "tabModuleStatistiques";
            this.tabModuleStatistiques.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleStatistiques.Size = new System.Drawing.Size(702, 582);
            this.tabModuleStatistiques.TabIndex = 3;
            this.tabModuleStatistiques.Text = "tabModuleStatistiques";
            // 
            // tabModuleGraphe
            // 
            this.tabModuleGraphe.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleGraphe.Controls.Add(this.btnAfficherMetro);
            this.tabModuleGraphe.Location = new System.Drawing.Point(4, 25);
            this.tabModuleGraphe.Name = "tabModuleGraphe";
            this.tabModuleGraphe.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleGraphe.Size = new System.Drawing.Size(702, 582);
            this.tabModuleGraphe.TabIndex = 4;
            this.tabModuleGraphe.Text = "tabModuleGraphe";
            // 
            // btnAfficherMetro
            // 
            this.btnAfficherMetro.BackColor = System.Drawing.Color.Gray;
            this.btnAfficherMetro.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAfficherMetro.Location = new System.Drawing.Point(35, 35);
            this.btnAfficherMetro.Name = "btnAfficherMetro";
            this.btnAfficherMetro.Size = new System.Drawing.Size(133, 42);
            this.btnAfficherMetro.TabIndex = 0;
            this.btnAfficherMetro.Text = "Afficher Métro";
            this.btnAfficherMetro.UseVisualStyleBackColor = false;
            this.btnAfficherMetro.Click += new System.EventHandler(this.btnAfficherMetro_Click);
            // 
            // btnRetourMenu
            // 
            this.btnRetourMenu.Location = new System.Drawing.Point(53, 682);
            this.btnRetourMenu.Name = "btnRetourMenu";
            this.btnRetourMenu.Size = new System.Drawing.Size(710, 42);
            this.btnRetourMenu.TabIndex = 1;
            this.btnRetourMenu.Text = "RETOUR AU MENU";
            this.btnRetourMenu.UseVisualStyleBackColor = true;
            this.btnRetourMenu.Click += new System.EventHandler(this.btnRetourMenu_Click);
            // 
            // FormModules
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.btnRetourMenu);
            this.Controls.Add(this.tabModule);
            this.Name = "FormModules";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormModules_Load);
            this.tabModule.ResumeLayout(false);
            this.tabModuleGraphe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void FormModules_Load(object sender, EventArgs e)
        {

        }

        private void btnRetourMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAfficherMetro_Click(object sender, EventArgs e)
        {

        }
    }
} 