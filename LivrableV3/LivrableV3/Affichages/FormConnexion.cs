using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormConnexion : Form
    {
        private Button btnRetour;
        private Label label1;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelMotDePasse;
        private TextBox textBoxMotDePasse;
        private Button btnConnexion;
        private Authentification authentification;

        /// constructeur du formulaire de connexion
        public FormConnexion(Authentification auth)
        {
            InitializeComponent();
            authentification = auth;
        }

       

        private void FormConnexion_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.btnRetour = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelMotDePasse = new System.Windows.Forms.Label();
            this.textBoxMotDePasse = new System.Windows.Forms.TextBox();
            this.btnConnexion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRetour
            // 
            this.btnRetour.Location = new System.Drawing.Point(12, 12);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(100, 49);
            this.btnRetour.TabIndex = 0;
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = true;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(343, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connexion";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(231, 207);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(60, 25);
            this.labelEmail.TabIndex = 2;
            this.labelEmail.Text = "Email";
            this.labelEmail.Click += new System.EventHandler(this.labelEmail_Click);
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(383, 202);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(194, 30);
            this.textBoxEmail.TabIndex = 3;
            // 
            // labelMotDePasse
            // 
            this.labelMotDePasse.AutoSize = true;
            this.labelMotDePasse.Location = new System.Drawing.Point(231, 281);
            this.labelMotDePasse.Name = "labelMotDePasse";
            this.labelMotDePasse.Size = new System.Drawing.Size(132, 25);
            this.labelMotDePasse.TabIndex = 4;
            this.labelMotDePasse.Text = "Mot de Passe";
            // 
            // textBoxMotDePasse
            // 
            this.textBoxMotDePasse.Location = new System.Drawing.Point(383, 276);
            this.textBoxMotDePasse.Name = "textBoxMotDePasse";
            this.textBoxMotDePasse.Size = new System.Drawing.Size(194, 30);
            this.textBoxMotDePasse.TabIndex = 5;
            // 
            // btnConnexion
            // 
            this.btnConnexion.Location = new System.Drawing.Point(297, 494);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(209, 40);
            this.btnConnexion.TabIndex = 6;
            this.btnConnexion.Text = "Se Connecter";
            this.btnConnexion.UseVisualStyleBackColor = true;
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);
            this.AcceptButton = this.btnConnexion;
            // 
            // FormConnexion
            // 
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.btnConnexion);
            this.Controls.Add(this.textBoxMotDePasse);
            this.Controls.Add(this.labelMotDePasse);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRetour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormConnexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormConnexion_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelEmail_Click(object sender, EventArgs e)
        {

        }

        private void FormConnexion_Load_1(object sender, EventArgs e)
        {
            

        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            try
            {
                string email = textBoxEmail.Text;
                string motDePasse = textBoxMotDePasse.Text;

                /// requete pour verifier si l'utilisateur existe
                string requete = "SELECT * FROM utilisateur WHERE email='" + email + "'";
                MySqlCommand commande = new MySqlCommand(requete, authentification.connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                if (reader.Read())
                {
                    string mdpBDD = reader.GetString("mot_de_passe");

                    if (mdpBDD == motDePasse)
                    {
                        authentification.estConnecte = true;
                        authentification.idUtilisateur = reader.GetString("id_utilisateur");
                        authentification.nomUtilisateur = reader.GetString("nom");
                        authentification.nom = reader.GetString("nom");
                        authentification.prenom = reader.GetString("prénom");
                        authentification.telephone = reader.GetString("telephone");
                        authentification.adresse = reader.GetString("adresse");
                        authentification.email = email;
                        authentification.motDePasse = motDePasse;

                        reader.Close();
                        commande.Dispose();

                        /// verifier si c'est un client
                        string requeteClient = "SELECT StationMetro FROM client WHERE id_utilisateur='" + authentification.idUtilisateur + "'";
                        MySqlCommand commandeClient = new MySqlCommand(requeteClient, authentification.connexionBDD.maConnexion);
                        commandeClient.CommandText = requeteClient;
                        MySqlDataReader readerClient = commandeClient.ExecuteReader();

                        if (readerClient.Read())
                        {
                            authentification.estClient = true;
                            authentification.estCuisinier = false;
                            authentification.stationMetro = readerClient.GetString("StationMetro");
                            readerClient.Close();
                            commandeClient.Dispose();

                            /// redirection vers le form client
                            ConnexionBDDClient connexionBDDClient = new ConnexionBDDClient(authentification.nomUtilisateur, authentification.motDePasse);
                            FormClient formClient = new FormClient(connexionBDDClient, authentification, authentification.GrapheMetro);
                            this.Hide();
                            formClient.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            readerClient.Close();
                            commandeClient.Dispose();

                            /// verifier si c'est un cuisinier
                            string requeteCuisinier = "SELECT StationMetro FROM cuisinier WHERE id_utilisateur='" + authentification.idUtilisateur + "'";
                            MySqlCommand commandeCuisinier = new MySqlCommand(requeteCuisinier, authentification.connexionBDD.maConnexion);
                            commandeCuisinier.CommandText = requeteCuisinier;
                            MySqlDataReader readerCuisinier = commandeCuisinier.ExecuteReader();

                            if (readerCuisinier.Read())
                            {
                                authentification.estCuisinier = true;
                                authentification.estClient = false;
                                authentification.stationMetro = readerCuisinier.GetString("StationMetro");
                                readerCuisinier.Close();
                                commandeCuisinier.Dispose();

                                /// redirection vers le form cuisinier
                                ConnexionBDDCuisinier connexionBDDCuisinier = new ConnexionBDDCuisinier(authentification.nomUtilisateur, authentification.motDePasse);
                                FormCuisinier formCuisinier = new FormCuisinier(connexionBDDCuisinier, authentification, authentification.GrapheMetro);
                                this.Hide();
                                formCuisinier.ShowDialog();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("mot de passe incorrect");
                        reader.Close();
                        commande.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("utilisateur non trouve");
                    reader.Close();
                    commande.Dispose();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("erreur lors de la connexion : " + ex.Message);
            }
        }
    }
} 