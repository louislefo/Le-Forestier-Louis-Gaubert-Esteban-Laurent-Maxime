using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormInscription : Form
    {
        #region Variables
        private Label labelInscription;
        public TextBox textBoxNom;
        private Label labelNom;
        private Label labelPrenom;
        public TextBox textBoxPrenom;
        private Label labelemail;
        public TextBox textBoxEmail;
        private Label labelTelephone;
        private TextBox textBoxTel;
        private Label labelAdresse;
        private TextBox textBoxAdresse;
        private Label labelMotDePasse;
        private TextBox textBoxMotdepasse;
        private Button btninscription;
        private ListBox listBoxType;
        private Label labelType;
        private Label labelStationdeMetro;
        private Button btnRetourMenu;
        private Authentification authentification;
        private ConnexionBDD connexionBDD;
        private Graphe<int> grapheMetro;
        private ComboBox comboBoxstation;
        private MainForm mainForm;
        private ChargerFichiers chargerFichiers;
        #endregion

        /// constructeur du formulaire d inscription
        public FormInscription(Authentification auth, ConnexionBDD connexionBDD,Graphe<int> graphemetro, MainForm mainForm)
        {
            this.chargerFichiers = new ChargerFichiers();
            InitializeComponent();
            authentification = auth;
            this.connexionBDD = connexionBDD;
            this.grapheMetro = graphemetro;
            this.mainForm = mainForm;
            Chargerstations();
        }



        private void InitializeComponent()
        {
            this.labelInscription = new System.Windows.Forms.Label();
            this.textBoxNom = new System.Windows.Forms.TextBox();
            this.labelNom = new System.Windows.Forms.Label();
            this.labelPrenom = new System.Windows.Forms.Label();
            this.textBoxPrenom = new System.Windows.Forms.TextBox();
            this.labelemail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelTelephone = new System.Windows.Forms.Label();
            this.textBoxTel = new System.Windows.Forms.TextBox();
            this.labelAdresse = new System.Windows.Forms.Label();
            this.textBoxAdresse = new System.Windows.Forms.TextBox();
            this.labelMotDePasse = new System.Windows.Forms.Label();
            this.textBoxMotdepasse = new System.Windows.Forms.TextBox();
            this.btninscription = new System.Windows.Forms.Button();
            this.listBoxType = new System.Windows.Forms.ListBox();
            this.labelType = new System.Windows.Forms.Label();
            this.labelStationdeMetro = new System.Windows.Forms.Label();
            this.btnRetourMenu = new System.Windows.Forms.Button();
            this.comboBoxstation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelInscription
            // 
            this.labelInscription.AutoSize = true;
            this.labelInscription.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInscription.Location = new System.Drawing.Point(304, 37);
            this.labelInscription.Name = "labelInscription";
            this.labelInscription.Size = new System.Drawing.Size(144, 32);
            this.labelInscription.TabIndex = 0;
            this.labelInscription.Text = "Inscription";
            // 
            // textBoxNom
            // 
            this.textBoxNom.Location = new System.Drawing.Point(374, 87);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.Size = new System.Drawing.Size(168, 30);
            this.textBoxNom.TabIndex = 1;
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Location = new System.Drawing.Point(207, 90);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(53, 25);
            this.labelNom.TabIndex = 2;
            this.labelNom.Text = "Nom";
            // 
            // labelPrenom
            // 
            this.labelPrenom.AutoSize = true;
            this.labelPrenom.Location = new System.Drawing.Point(208, 144);
            this.labelPrenom.Name = "labelPrenom";
            this.labelPrenom.Size = new System.Drawing.Size(80, 25);
            this.labelPrenom.TabIndex = 3;
            this.labelPrenom.Text = "Prenom";
            // 
            // textBoxPrenom
            // 
            this.textBoxPrenom.Location = new System.Drawing.Point(374, 141);
            this.textBoxPrenom.Name = "textBoxPrenom";
            this.textBoxPrenom.Size = new System.Drawing.Size(168, 30);
            this.textBoxPrenom.TabIndex = 4;
            // 
            // labelemail
            // 
            this.labelemail.AutoSize = true;
            this.labelemail.Location = new System.Drawing.Point(208, 197);
            this.labelemail.Name = "labelemail";
            this.labelemail.Size = new System.Drawing.Size(60, 25);
            this.labelemail.TabIndex = 5;
            this.labelemail.Text = "Email";
            this.labelemail.Click += new System.EventHandler(this.labelemail_Click);
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(374, 194);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(168, 30);
            this.textBoxEmail.TabIndex = 6;
            // 
            // labelTelephone
            // 
            this.labelTelephone.AutoSize = true;
            this.labelTelephone.Location = new System.Drawing.Point(207, 255);
            this.labelTelephone.Name = "labelTelephone";
            this.labelTelephone.Size = new System.Drawing.Size(106, 25);
            this.labelTelephone.TabIndex = 7;
            this.labelTelephone.Text = "Telephone";
            // 
            // textBoxTel
            // 
            this.textBoxTel.Location = new System.Drawing.Point(374, 250);
            this.textBoxTel.Name = "textBoxTel";
            this.textBoxTel.Size = new System.Drawing.Size(168, 30);
            this.textBoxTel.TabIndex = 8;
            // 
            // labelAdresse
            // 
            this.labelAdresse.AutoSize = true;
            this.labelAdresse.Location = new System.Drawing.Point(208, 307);
            this.labelAdresse.Name = "labelAdresse";
            this.labelAdresse.Size = new System.Drawing.Size(85, 25);
            this.labelAdresse.TabIndex = 9;
            this.labelAdresse.Text = "Adresse";
            // 
            // textBoxAdresse
            // 
            this.textBoxAdresse.Location = new System.Drawing.Point(374, 304);
            this.textBoxAdresse.Name = "textBoxAdresse";
            this.textBoxAdresse.Size = new System.Drawing.Size(168, 30);
            this.textBoxAdresse.TabIndex = 10;
            // 
            // labelMotDePasse
            // 
            this.labelMotDePasse.AutoSize = true;
            this.labelMotDePasse.Location = new System.Drawing.Point(207, 365);
            this.labelMotDePasse.Name = "labelMotDePasse";
            this.labelMotDePasse.Size = new System.Drawing.Size(135, 25);
            this.labelMotDePasse.TabIndex = 11;
            this.labelMotDePasse.Text = "Mot De Passe";
            this.labelMotDePasse.Click += new System.EventHandler(this.labelMotDePasse_Click);
            // 
            // textBoxMotdepasse
            // 
            this.textBoxMotdepasse.Location = new System.Drawing.Point(376, 365);
            this.textBoxMotdepasse.Name = "textBoxMotdepasse";
            this.textBoxMotdepasse.Size = new System.Drawing.Size(166, 30);
            this.textBoxMotdepasse.TabIndex = 12;
            this.textBoxMotdepasse.UseSystemPasswordChar = true;
            // 
            // btninscription
            // 
            this.btninscription.Location = new System.Drawing.Point(243, 645);
            this.btninscription.Name = "btninscription";
            this.btninscription.Size = new System.Drawing.Size(275, 45);
            this.btninscription.TabIndex = 13;
            this.btninscription.Text = "S\'inscrire";
            this.btninscription.UseVisualStyleBackColor = true;
            this.btninscription.Click += new System.EventHandler(this.btninscription_Click);
            // 
            // listBoxType
            // 
            this.listBoxType.FormattingEnabled = true;
            this.listBoxType.ItemHeight = 25;
            this.listBoxType.Items.AddRange(new object[] {
            "Client",
            "Cuisinier",
            "Les deux"});
            this.listBoxType.Location = new System.Drawing.Point(376, 420);
            this.listBoxType.Name = "listBoxType";
            this.listBoxType.Size = new System.Drawing.Size(166, 29);
            this.listBoxType.TabIndex = 14;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(208, 420);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(57, 25);
            this.labelType.TabIndex = 15;
            this.labelType.Text = "Type";
            // 
            // labelStationdeMetro
            // 
            this.labelStationdeMetro.AutoSize = true;
            this.labelStationdeMetro.Location = new System.Drawing.Point(207, 481);
            this.labelStationdeMetro.Name = "labelStationdeMetro";
            this.labelStationdeMetro.Size = new System.Drawing.Size(154, 25);
            this.labelStationdeMetro.TabIndex = 16;
            this.labelStationdeMetro.Text = "Station de metro";
            this.labelStationdeMetro.Click += new System.EventHandler(this.labelStationdeMetro_Click);
            // 
            // btnRetourMenu
            // 
            this.btnRetourMenu.Location = new System.Drawing.Point(19, 15);
            this.btnRetourMenu.Name = "btnRetourMenu";
            this.btnRetourMenu.Size = new System.Drawing.Size(98, 42);
            this.btnRetourMenu.TabIndex = 18;
            this.btnRetourMenu.Text = "Retour";
            this.btnRetourMenu.UseVisualStyleBackColor = true;
            this.btnRetourMenu.Click += new System.EventHandler(this.btnRetourMenu_Click);
            // 
            // comboBoxstation
            // 
            this.comboBoxstation.FormattingEnabled = true;
            this.comboBoxstation.Location = new System.Drawing.Point(374, 481);
            this.comboBoxstation.Name = "comboBoxstation";
            this.comboBoxstation.Size = new System.Drawing.Size(168, 33);
            this.comboBoxstation.TabIndex = 19;
            // 
            // FormInscription
            // 
            this.AcceptButton = this.btninscription;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.comboBoxstation);
            this.Controls.Add(this.btnRetourMenu);
            this.Controls.Add(this.labelStationdeMetro);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.listBoxType);
            this.Controls.Add(this.btninscription);
            this.Controls.Add(this.textBoxMotdepasse);
            this.Controls.Add(this.labelMotDePasse);
            this.Controls.Add(this.textBoxAdresse);
            this.Controls.Add(this.labelAdresse);
            this.Controls.Add(this.textBoxTel);
            this.Controls.Add(this.labelTelephone);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelemail);
            this.Controls.Add(this.textBoxPrenom);
            this.Controls.Add(this.labelPrenom);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.textBoxNom);
            this.Controls.Add(this.labelInscription);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormInscription";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormInscription_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Chargerstations()
        {
            List<string> stations = chargerFichiers.ChargerStation();

            for (int i = 0; i < stations.Count; i++)
            {
                comboBoxstation.Items.Add(stations[i].ToString());
            }
        }
        private string GenererIdCuisinier()
        {
            try
            {

                string sql = "SELECT id_cuisinier FROM cuisinier WHERE id_cuisinier LIKE 'CUI%' ORDER BY id_cuisinier DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    return "CUI001";
                }

                string dernierId = result.ToString();

                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;


                return "CUI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur lors de la generation de l'id cuisinier : " + ex.Message);
                return "CUI" + DateTime.Now.Ticks;
            }
        }
        private string GenererIdClient()
        {
            try
            {

                string sql = "SELECT id_client FROM client WHERE id_client LIKE 'CLI%' ORDER BY id_client DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, connexionBDD.maConnexion);
                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    return "CLI001";
                }

                string dernierId = result.ToString();
                string numeroStr = dernierId.Substring(3);
                int numero = int.Parse(numeroStr) + 1;
                return "CLI" + numero.ToString("D3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur lors de la generation de l'id client : " + ex.Message);
                return "CLI" + DateTime.Now.Ticks;
            }
        }
        private void FormInscription_Load(object sender, EventArgs e)
        {

        }

        private void labelemail_Click(object sender, EventArgs e)
        {

        }

        private void labelMotDePasse_Click(object sender, EventArgs e)
        {

        }

        private void labelStationdeMetro_Click(object sender, EventArgs e)
        {

        }

        private void btnRetourMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void btninscription_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationRequette validation = new ValidationRequette(grapheMetro);

                string nomUtilisateur = textBoxNom.Text;
                string prenom = textBoxPrenom.Text;
                string email = textBoxEmail.Text;
                string telephone = textBoxTel.Text;
                string adresse = textBoxAdresse.Text;
                string motDePasse = textBoxMotdepasse.Text;

                string stationMetro = comboBoxstation.Text;

                // on valide les donnees
                nomUtilisateur = validation.DemanderNom(nomUtilisateur);
                if (nomUtilisateur == "") return;

                prenom = validation.DemanderPrenom(prenom);
                if (prenom == "") return;

                email = validation.DemanderEmail(email);
                if (email == "") return;

                telephone = validation.DemanderTelephone(telephone);
                if (telephone == "") return;

                adresse = validation.DemanderAdresse(adresse);
                if (adresse == "") return;

                motDePasse = validation.DemanderMotDePasse(motDePasse);
                if (motDePasse == "") return;

                if (string.IsNullOrEmpty(stationMetro))
                {
                    MessageBox.Show("Veuillez selectionner une station de metro");
                    return;
                }
                stationMetro = validation.DemanderStationMetro(stationMetro);
                if (stationMetro == "") return;

                if (listBoxType.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez selectionner un type d'utilisateur");
                    return;
                }

                /// verifier si l'email existe deja
                string sqlVerifEmail = "SELECT COUNT(*) FROM utilisateur WHERE email = '" + email + "'";
                MySqlCommand cmdVerifEmail = new MySqlCommand(sqlVerifEmail, authentification.connexionBDD.maConnexion);
                int countEmail = Convert.ToInt32(cmdVerifEmail.ExecuteScalar());

                if (countEmail > 0)
                {
                    MessageBox.Show("cet email est deja utilise");
                    return;
                }

                /// generer id utilisateur
                string sqlUtilisateur = "SELECT id_utilisateur FROM utilisateur WHERE id_utilisateur LIKE 'USR%' ORDER BY id_utilisateur DESC LIMIT 1";
                MySqlCommand cmdUtilisateur = new MySqlCommand(sqlUtilisateur, authentification.connexionBDD.maConnexion);
                object result = cmdUtilisateur.ExecuteScalar();
                string idUtilisateur;
                if (result == null)
                {
                    idUtilisateur = "USR001";
                }
                else
                {
                    string dernierId = result.ToString();
                    string numeroStr = dernierId.Substring(3);
                    int numero = int.Parse(numeroStr) + 1;
                    idUtilisateur = "USR" + numero.ToString("D3");
                }

                /// inserer utilisateur
                string sqlInsertUtilisateur = "INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES ('" +
                    idUtilisateur + "', '" + nomUtilisateur + "', '" + prenom + "', '" + email + "', '" + adresse + "', '" + telephone + "', '" + motDePasse + "')";
                MySqlCommand cmdInsertUtilisateur = new MySqlCommand(sqlInsertUtilisateur, authentification.connexionBDD.maConnexion);
                cmdInsertUtilisateur.ExecuteNonQuery();

                int typeChoisi = listBoxType.SelectedIndex;

                switch (typeChoisi)
                {
                    case 0: /// client
                        string idClient = GenererIdClient();
                        string requeteClient = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" +
                            idClient + "', '" + idUtilisateur + "', '" + stationMetro + "', NULL, NULL)";
                        MySqlCommand cmdClient = new MySqlCommand(requeteClient, authentification.connexionBDD.maConnexion);
                        cmdClient.ExecuteNonQuery();

                        /// creation compte acces BDD client
                        CreerCompteBDDClient(nomUtilisateur, motDePasse);
                        try
                        {
                            ConnexionBDDClient connexionBDDClient = new ConnexionBDDClient(nomUtilisateur, motDePasse);
                            FormClient formClient = new FormClient(connexionBDDClient, authentification, grapheMetro, mainForm);
                            this.Close();
                            formClient.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        break;

                    case 1: /// cuisinier
                        string idCuisinier = GenererIdCuisinier();
                        string requeteCuisinier = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" +
                            idCuisinier + "', '" + idUtilisateur + "', '" + stationMetro + "', NULL, 0, 0)";
                        MySqlCommand cmdCuisinier = new MySqlCommand(requeteCuisinier, authentification.connexionBDD.maConnexion);
                        cmdCuisinier.ExecuteNonQuery();

                        /// creation compte acces BDD cuisinier
                        CreerCompteBDDCuisinier(nomUtilisateur, motDePasse);
                        try
                        {
                            ConnexionBDDCuisinier connexionBDDCuisinier = new ConnexionBDDCuisinier(nomUtilisateur, motDePasse);
                            FormCuisinier formCuisinier = new FormCuisinier(connexionBDDCuisinier, authentification, grapheMetro,mainForm);
                            this.Close();
                            formCuisinier.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        

                        break;

                    case 2: /// les deux
                        string idClient2 = GenererIdClient();
                        string requeteClient2 = "INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES ('" +
                            idClient2 + "', '" + idUtilisateur + "', '" + stationMetro + "', NULL, NULL)";
                        MySqlCommand cmdClient2 = new MySqlCommand(requeteClient2, authentification.connexionBDD.maConnexion);
                        cmdClient2.ExecuteNonQuery();

                        string idCuisinier2 = GenererIdCuisinier();
                        string requeteCuisinier2 = "INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES ('" +
                            idCuisinier2 + "', '" + idUtilisateur + "', '" + stationMetro + "', NULL, 0, 0)";
                        MySqlCommand cmdCuisinier2 = new MySqlCommand(requeteCuisinier2, authentification.connexionBDD.maConnexion);
                        cmdCuisinier2.ExecuteNonQuery();

                        /// creation compte acces BDD
                        CreerCompteBDDClient(nomUtilisateur, motDePasse);
                        CreerCompteBDDCuisinier(nomUtilisateur, motDePasse);

                        // demander ou aller
                        try
                        {
                            ConnexionBDDCuisinier connexionBDDCuisinier = new ConnexionBDDCuisinier(nomUtilisateur, motDePasse);
                            FormCuisinier formCuisinier = new FormCuisinier(connexionBDDCuisinier, authentification, grapheMetro, mainForm);
                            this.Close();
                            formCuisinier.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        try
                        {
                            ConnexionBDDClient connexionBDDClient = new ConnexionBDDClient(nomUtilisateur, motDePasse);
                            FormClient formClient = new FormClient(connexionBDDClient, authentification, grapheMetro, mainForm);
                            this.Close();
                            formClient.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        break;
                }

                MessageBox.Show("Inscription reussie");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreerCompteBDDClient(string nomUtilisateur, string motDePasse)
        {
            try
            {
                string CreateBDDClient = "CREATE USER IF NOT EXISTS '" + nomUtilisateur + "'@'localhost' IDENTIFIED BY '" + motDePasse + "';";
                MySqlCommand cmdCreateBDDClient = new MySqlCommand(CreateBDDClient, connexionBDD.maConnexion);
                cmdCreateBDDClient.ExecuteNonQuery();

                string GrantBDDClient = "GRANT SELECT, INSERT, UPDATE ON PSI_LoMaEs.*  TO '" + nomUtilisateur + "'@'localhost';";
                MySqlCommand cmdGrantBDDClient = new MySqlCommand(GrantBDDClient, connexionBDD.maConnexion);
                cmdGrantBDDClient.ExecuteNonQuery();

                cmdCreateBDDClient.Dispose();
                cmdGrantBDDClient.Dispose();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("erreur lors de la creation du compte acces BDD client : " + e.Message);
            }
        }
        private void CreerCompteBDDCuisinier(string nomUtilisateur, string motDePasse)
        {
            try
            {
                string CreateBDDCuisinier = "CREATE USER IF NOT EXISTS '" + nomUtilisateur + "'@'localhost' IDENTIFIED BY '" + motDePasse + "';";
                MySqlCommand cmdCreateBDDCuisinier = new MySqlCommand(CreateBDDCuisinier, connexionBDD.maConnexion);
                cmdCreateBDDCuisinier.ExecuteNonQuery();


                string GrantBDDCuisinier = "GRANT SELECT, INSERT, UPDATE ON PSI_LoMaEs.*  TO '" + nomUtilisateur + "'@'localhost';";
                MySqlCommand cmdGrantBDDCuisinier = new MySqlCommand(GrantBDDCuisinier, connexionBDD.maConnexion);
                cmdGrantBDDCuisinier.ExecuteNonQuery();

                cmdCreateBDDCuisinier.Dispose();
                cmdGrantBDDCuisinier.Dispose();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("erreur lors de la creation du compte acces BDD cuisinier : " + e.Message);
            }
        }
    }
} 