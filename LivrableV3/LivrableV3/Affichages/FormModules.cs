using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Linq;

namespace LivrableV3
{
    public partial class FormModules : Form
    {
        #region variables

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
        private Button btnitineraire;
        private Label labelClientrep;
        private Button btnClientparachat;
        private Button btnclientparrue;
        private Button btnclienttrialpha;
        private TextBox textBoxclientrep;
        private Button btnClientSuppClient;
        private Button btnClientModifclient;
        private Button btnClientAjoutClient;
        private Button btnstatlivparcuisinier;
        private Button btnstatcomdpartype;
        private Button btnstatmoyennecompteclient;
        private Button btnstatmoyenneprix;
        private Button btnstatcomparper;
        private Label labelstatrep;
        private TextBox textBoxstatrep;
        private Label label1;
        private DateTimePicker dateTimePickerstatFin;
        private DateTimePicker dateTimePickerstatdebut;
        private Label labelstatdatefin;
        private Button btncommandecreer;
        private Button btncommandeitineraire;
        private Button btncommandeprix;
        private Button btncommandemodif;
        private Button btngrapheinfoligne;
        private Button btnGrapheinfometro;
        private TextBox textBoxGrapheRep;
        private ComboBox comboBoxgrapheligne;
        private MainForm main;

        #endregion

        public FormModules(ConnexionBDD connexion, Graphe<int> graphe, MainForm main)
        {
            InitializeComponent();
            connexionBDD = connexion;
            grapheMetro = graphe;
            this.main = main;
        }

        private void InitializeComponent()
        {
            this.tabModule = new System.Windows.Forms.TabControl();
            this.tabModuleClient = new System.Windows.Forms.TabPage();
            this.btnClientSuppClient = new System.Windows.Forms.Button();
            this.btnClientModifclient = new System.Windows.Forms.Button();
            this.btnClientAjoutClient = new System.Windows.Forms.Button();
            this.textBoxclientrep = new System.Windows.Forms.TextBox();
            this.labelClientrep = new System.Windows.Forms.Label();
            this.btnClientparachat = new System.Windows.Forms.Button();
            this.btnclientparrue = new System.Windows.Forms.Button();
            this.btnclienttrialpha = new System.Windows.Forms.Button();
            this.tabModuleCuisinier = new System.Windows.Forms.TabPage();
            this.tabModuleCommande = new System.Windows.Forms.TabPage();
            this.btncommandeitineraire = new System.Windows.Forms.Button();
            this.btncommandeprix = new System.Windows.Forms.Button();
            this.btncommandemodif = new System.Windows.Forms.Button();
            this.btncommandecreer = new System.Windows.Forms.Button();
            this.tabModuleStatistiques = new System.Windows.Forms.TabPage();
            this.labelstatdatefin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerstatFin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerstatdebut = new System.Windows.Forms.DateTimePicker();
            this.labelstatrep = new System.Windows.Forms.Label();
            this.textBoxstatrep = new System.Windows.Forms.TextBox();
            this.btnstatcomdpartype = new System.Windows.Forms.Button();
            this.btnstatmoyennecompteclient = new System.Windows.Forms.Button();
            this.btnstatmoyenneprix = new System.Windows.Forms.Button();
            this.btnstatcomparper = new System.Windows.Forms.Button();
            this.btnstatlivparcuisinier = new System.Windows.Forms.Button();
            this.tabModuleGraphe = new System.Windows.Forms.TabPage();
            this.btnitineraire = new System.Windows.Forms.Button();
            this.btnAfficherMetro = new System.Windows.Forms.Button();
            this.btnRetourMenu = new System.Windows.Forms.Button();
            this.btnGrapheinfometro = new System.Windows.Forms.Button();
            this.btngrapheinfoligne = new System.Windows.Forms.Button();
            this.textBoxGrapheRep = new System.Windows.Forms.TextBox();
            this.comboBoxgrapheligne = new System.Windows.Forms.ComboBox();
            this.tabModule.SuspendLayout();
            this.tabModuleClient.SuspendLayout();
            this.tabModuleCommande.SuspendLayout();
            this.tabModuleStatistiques.SuspendLayout();
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
            this.tabModule.Location = new System.Drawing.Point(39, 64);
            this.tabModule.Name = "tabModule";
            this.tabModule.SelectedIndex = 0;
            this.tabModule.Size = new System.Drawing.Size(710, 611);
            this.tabModule.TabIndex = 0;
            // 
            // tabModuleClient
            // 
            this.tabModuleClient.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleClient.Controls.Add(this.btnClientSuppClient);
            this.tabModuleClient.Controls.Add(this.btnClientModifclient);
            this.tabModuleClient.Controls.Add(this.btnClientAjoutClient);
            this.tabModuleClient.Controls.Add(this.textBoxclientrep);
            this.tabModuleClient.Controls.Add(this.labelClientrep);
            this.tabModuleClient.Controls.Add(this.btnClientparachat);
            this.tabModuleClient.Controls.Add(this.btnclientparrue);
            this.tabModuleClient.Controls.Add(this.btnclienttrialpha);
            this.tabModuleClient.Location = new System.Drawing.Point(4, 25);
            this.tabModuleClient.Name = "tabModuleClient";
            this.tabModuleClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleClient.Size = new System.Drawing.Size(702, 582);
            this.tabModuleClient.TabIndex = 0;
            this.tabModuleClient.Text = "moduleClient";
            this.tabModuleClient.Click += new System.EventHandler(this.tabModuleClient_Click);
            // 
            // btnClientSuppClient
            // 
            this.btnClientSuppClient.Location = new System.Drawing.Point(303, 102);
            this.btnClientSuppClient.Name = "btnClientSuppClient";
            this.btnClientSuppClient.Size = new System.Drawing.Size(146, 60);
            this.btnClientSuppClient.TabIndex = 7;
            this.btnClientSuppClient.Text = "Supprimer un client";
            this.btnClientSuppClient.UseVisualStyleBackColor = true;
            this.btnClientSuppClient.Click += new System.EventHandler(this.btnClientSuppClient_Click);
            // 
            // btnClientModifclient
            // 
            this.btnClientModifclient.Location = new System.Drawing.Point(46, 166);
            this.btnClientModifclient.Name = "btnClientModifclient";
            this.btnClientModifclient.Size = new System.Drawing.Size(132, 48);
            this.btnClientModifclient.TabIndex = 6;
            this.btnClientModifclient.Text = "Modifier un client";
            this.btnClientModifclient.UseVisualStyleBackColor = true;
            this.btnClientModifclient.Click += new System.EventHandler(this.btnClientModifclient_Click);
            // 
            // btnClientAjoutClient
            // 
            this.btnClientAjoutClient.Location = new System.Drawing.Point(46, 52);
            this.btnClientAjoutClient.Name = "btnClientAjoutClient";
            this.btnClientAjoutClient.Size = new System.Drawing.Size(137, 49);
            this.btnClientAjoutClient.TabIndex = 5;
            this.btnClientAjoutClient.Text = "Ajouter un client";
            this.btnClientAjoutClient.UseVisualStyleBackColor = true;
            this.btnClientAjoutClient.Click += new System.EventHandler(this.btnClientAjoutClient_Click);
            // 
            // textBoxclientrep
            // 
            this.textBoxclientrep.Location = new System.Drawing.Point(198, 316);
            this.textBoxclientrep.Multiline = true;
            this.textBoxclientrep.Name = "textBoxclientrep";
            this.textBoxclientrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxclientrep.Size = new System.Drawing.Size(448, 242);
            this.textBoxclientrep.TabIndex = 4;
            // 
            // labelClientrep
            // 
            this.labelClientrep.AutoSize = true;
            this.labelClientrep.Location = new System.Drawing.Point(195, 283);
            this.labelClientrep.Name = "labelClientrep";
            this.labelClientrep.Size = new System.Drawing.Size(128, 16);
            this.labelClientrep.TabIndex = 3;
            this.labelClientrep.Text = "Affichage Réponse :";
            this.labelClientrep.Click += new System.EventHandler(this.labelClientrep_Click);
            // 
            // btnClientparachat
            // 
            this.btnClientparachat.Location = new System.Drawing.Point(37, 494);
            this.btnClientparachat.Name = "btnClientparachat";
            this.btnClientparachat.Size = new System.Drawing.Size(124, 64);
            this.btnClientparachat.TabIndex = 2;
            this.btnClientparachat.Text = "Afficher les clients par montant des achats";
            this.btnClientparachat.UseVisualStyleBackColor = true;
            this.btnClientparachat.Click += new System.EventHandler(this.btnClientparachat_Click);
            // 
            // btnclientparrue
            // 
            this.btnclientparrue.Location = new System.Drawing.Point(37, 390);
            this.btnclientparrue.Name = "btnclientparrue";
            this.btnclientparrue.Size = new System.Drawing.Size(128, 74);
            this.btnclientparrue.TabIndex = 1;
            this.btnclientparrue.Text = "Afficher les clients par rue";
            this.btnclientparrue.UseVisualStyleBackColor = true;
            this.btnclientparrue.Click += new System.EventHandler(this.btnclientparrue_Click);
            // 
            // btnclienttrialpha
            // 
            this.btnclienttrialpha.Location = new System.Drawing.Point(37, 283);
            this.btnclienttrialpha.Name = "btnclienttrialpha";
            this.btnclienttrialpha.Size = new System.Drawing.Size(128, 67);
            this.btnclienttrialpha.TabIndex = 0;
            this.btnclienttrialpha.Text = "Afficher les clients par ordre alphabetique";
            this.btnclienttrialpha.UseVisualStyleBackColor = true;
            this.btnclienttrialpha.Click += new System.EventHandler(this.btnclienttrialpha_Click);
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
            this.tabModuleCommande.Controls.Add(this.btncommandeitineraire);
            this.tabModuleCommande.Controls.Add(this.btncommandeprix);
            this.tabModuleCommande.Controls.Add(this.btncommandemodif);
            this.tabModuleCommande.Controls.Add(this.btncommandecreer);
            this.tabModuleCommande.Location = new System.Drawing.Point(4, 25);
            this.tabModuleCommande.Name = "tabModuleCommande";
            this.tabModuleCommande.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleCommande.Size = new System.Drawing.Size(702, 582);
            this.tabModuleCommande.TabIndex = 2;
            this.tabModuleCommande.Text = "ModuleCommande";
            this.tabModuleCommande.Click += new System.EventHandler(this.tabModuleCommande_Click);
            // 
            // btncommandeitineraire
            // 
            this.btncommandeitineraire.Location = new System.Drawing.Point(38, 435);
            this.btncommandeitineraire.Name = "btncommandeitineraire";
            this.btncommandeitineraire.Size = new System.Drawing.Size(152, 65);
            this.btncommandeitineraire.TabIndex = 3;
            this.btncommandeitineraire.Text = "Determiner le chemin de livraison";
            this.btncommandeitineraire.UseVisualStyleBackColor = true;
            this.btncommandeitineraire.Click += new System.EventHandler(this.btncommandeitineraire_Click);
            // 
            // btncommandeprix
            // 
            this.btncommandeprix.Location = new System.Drawing.Point(38, 308);
            this.btncommandeprix.Name = "btncommandeprix";
            this.btncommandeprix.Size = new System.Drawing.Size(152, 65);
            this.btncommandeprix.TabIndex = 2;
            this.btncommandeprix.Text = "Calculer le prix d\'une commande";
            this.btncommandeprix.UseVisualStyleBackColor = true;
            this.btncommandeprix.Click += new System.EventHandler(this.btncommandeprix_Click);
            // 
            // btncommandemodif
            // 
            this.btncommandemodif.Location = new System.Drawing.Point(38, 192);
            this.btncommandemodif.Name = "btncommandemodif";
            this.btncommandemodif.Size = new System.Drawing.Size(152, 65);
            this.btncommandemodif.TabIndex = 1;
            this.btncommandemodif.Text = "Modifier une commande";
            this.btncommandemodif.UseVisualStyleBackColor = true;
            this.btncommandemodif.Click += new System.EventHandler(this.btncommandemodif_Click);
            // 
            // btncommandecreer
            // 
            this.btncommandecreer.Location = new System.Drawing.Point(38, 87);
            this.btncommandecreer.Name = "btncommandecreer";
            this.btncommandecreer.Size = new System.Drawing.Size(152, 65);
            this.btncommandecreer.TabIndex = 0;
            this.btncommandecreer.Text = "Créer une commande";
            this.btncommandecreer.UseVisualStyleBackColor = true;
            this.btncommandecreer.Click += new System.EventHandler(this.btncommandecreer_Click);
            // 
            // tabModuleStatistiques
            // 
            this.tabModuleStatistiques.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleStatistiques.Controls.Add(this.labelstatdatefin);
            this.tabModuleStatistiques.Controls.Add(this.label1);
            this.tabModuleStatistiques.Controls.Add(this.dateTimePickerstatFin);
            this.tabModuleStatistiques.Controls.Add(this.dateTimePickerstatdebut);
            this.tabModuleStatistiques.Controls.Add(this.labelstatrep);
            this.tabModuleStatistiques.Controls.Add(this.textBoxstatrep);
            this.tabModuleStatistiques.Controls.Add(this.btnstatcomdpartype);
            this.tabModuleStatistiques.Controls.Add(this.btnstatmoyennecompteclient);
            this.tabModuleStatistiques.Controls.Add(this.btnstatmoyenneprix);
            this.tabModuleStatistiques.Controls.Add(this.btnstatcomparper);
            this.tabModuleStatistiques.Controls.Add(this.btnstatlivparcuisinier);
            this.tabModuleStatistiques.Location = new System.Drawing.Point(4, 25);
            this.tabModuleStatistiques.Name = "tabModuleStatistiques";
            this.tabModuleStatistiques.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleStatistiques.Size = new System.Drawing.Size(702, 582);
            this.tabModuleStatistiques.TabIndex = 3;
            this.tabModuleStatistiques.Text = "tabModuleStatistiques";
            this.tabModuleStatistiques.Click += new System.EventHandler(this.tabModuleStatistiques_Click);
            // 
            // labelstatdatefin
            // 
            this.labelstatdatefin.AutoSize = true;
            this.labelstatdatefin.Location = new System.Drawing.Point(533, 417);
            this.labelstatdatefin.Name = "labelstatdatefin";
            this.labelstatdatefin.Size = new System.Drawing.Size(76, 16);
            this.labelstatdatefin.TabIndex = 10;
            this.labelstatdatefin.Text = "Date de Fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Date de début";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dateTimePickerstatFin
            // 
            this.dateTimePickerstatFin.Location = new System.Drawing.Point(488, 468);
            this.dateTimePickerstatFin.Name = "dateTimePickerstatFin";
            this.dateTimePickerstatFin.Size = new System.Drawing.Size(171, 22);
            this.dateTimePickerstatFin.TabIndex = 8;
            // 
            // dateTimePickerstatdebut
            // 
            this.dateTimePickerstatdebut.Location = new System.Drawing.Point(237, 468);
            this.dateTimePickerstatdebut.Name = "dateTimePickerstatdebut";
            this.dateTimePickerstatdebut.Size = new System.Drawing.Size(170, 22);
            this.dateTimePickerstatdebut.TabIndex = 7;
            // 
            // labelstatrep
            // 
            this.labelstatrep.AutoSize = true;
            this.labelstatrep.Location = new System.Drawing.Point(195, 21);
            this.labelstatrep.Name = "labelstatrep";
            this.labelstatrep.Size = new System.Drawing.Size(148, 16);
            this.labelstatrep.TabIndex = 6;
            this.labelstatrep.Text = "Affichage des resultats :";
            // 
            // textBoxstatrep
            // 
            this.textBoxstatrep.Location = new System.Drawing.Point(198, 42);
            this.textBoxstatrep.Multiline = true;
            this.textBoxstatrep.Name = "textBoxstatrep";
            this.textBoxstatrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxstatrep.Size = new System.Drawing.Size(480, 328);
            this.textBoxstatrep.TabIndex = 5;
            // 
            // btnstatcomdpartype
            // 
            this.btnstatcomdpartype.Location = new System.Drawing.Point(30, 479);
            this.btnstatcomdpartype.Name = "btnstatcomdpartype";
            this.btnstatcomdpartype.Size = new System.Drawing.Size(147, 65);
            this.btnstatcomdpartype.TabIndex = 4;
            this.btnstatcomdpartype.Text = "Afficher les commandes par type de plat";
            this.btnstatcomdpartype.UseVisualStyleBackColor = true;
            this.btnstatcomdpartype.Click += new System.EventHandler(this.btnstatcomdpartype_Click);
            // 
            // btnstatmoyennecompteclient
            // 
            this.btnstatmoyennecompteclient.Location = new System.Drawing.Point(30, 42);
            this.btnstatmoyennecompteclient.Name = "btnstatmoyennecompteclient";
            this.btnstatmoyennecompteclient.Size = new System.Drawing.Size(147, 68);
            this.btnstatmoyennecompteclient.TabIndex = 3;
            this.btnstatmoyennecompteclient.Text = "Afficher la moyenne des comptes clients";
            this.btnstatmoyennecompteclient.UseVisualStyleBackColor = true;
            this.btnstatmoyennecompteclient.Click += new System.EventHandler(this.btnstatmoyennecompteclient_Click);
            // 
            // btnstatmoyenneprix
            // 
            this.btnstatmoyenneprix.Location = new System.Drawing.Point(30, 268);
            this.btnstatmoyenneprix.Name = "btnstatmoyenneprix";
            this.btnstatmoyenneprix.Size = new System.Drawing.Size(147, 63);
            this.btnstatmoyenneprix.TabIndex = 2;
            this.btnstatmoyenneprix.Text = "Afficher la moyenne des prix des commandes";
            this.btnstatmoyenneprix.UseVisualStyleBackColor = true;
            this.btnstatmoyenneprix.Click += new System.EventHandler(this.btnstatmoyenneprix_Click);
            // 
            // btnstatcomparper
            // 
            this.btnstatcomparper.Location = new System.Drawing.Point(30, 383);
            this.btnstatcomparper.Name = "btnstatcomparper";
            this.btnstatcomparper.Size = new System.Drawing.Size(147, 63);
            this.btnstatcomparper.TabIndex = 1;
            this.btnstatcomparper.Text = "Afficher les commandes par periode";
            this.btnstatcomparper.UseVisualStyleBackColor = true;
            this.btnstatcomparper.Click += new System.EventHandler(this.btnstatcomparper_Click);
            // 
            // btnstatlivparcuisinier
            // 
            this.btnstatlivparcuisinier.Location = new System.Drawing.Point(30, 150);
            this.btnstatlivparcuisinier.Name = "btnstatlivparcuisinier";
            this.btnstatlivparcuisinier.Size = new System.Drawing.Size(147, 67);
            this.btnstatlivparcuisinier.TabIndex = 0;
            this.btnstatlivparcuisinier.Text = "Afficher les livraisons par cuisinier";
            this.btnstatlivparcuisinier.UseVisualStyleBackColor = true;
            this.btnstatlivparcuisinier.Click += new System.EventHandler(this.btnstatlivparcuisinier_Click);
            // 
            // tabModuleGraphe
            // 
            this.tabModuleGraphe.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabModuleGraphe.Controls.Add(this.comboBoxgrapheligne);
            this.tabModuleGraphe.Controls.Add(this.textBoxGrapheRep);
            this.tabModuleGraphe.Controls.Add(this.btngrapheinfoligne);
            this.tabModuleGraphe.Controls.Add(this.btnGrapheinfometro);
            this.tabModuleGraphe.Controls.Add(this.btnitineraire);
            this.tabModuleGraphe.Controls.Add(this.btnAfficherMetro);
            this.tabModuleGraphe.Location = new System.Drawing.Point(4, 25);
            this.tabModuleGraphe.Name = "tabModuleGraphe";
            this.tabModuleGraphe.Padding = new System.Windows.Forms.Padding(3);
            this.tabModuleGraphe.Size = new System.Drawing.Size(702, 582);
            this.tabModuleGraphe.TabIndex = 4;
            this.tabModuleGraphe.Text = "tabModuleGraphe";
            this.tabModuleGraphe.Click += new System.EventHandler(this.tabModuleGraphe_Click);
            // 
            // btnitineraire
            // 
            this.btnitineraire.Location = new System.Drawing.Point(198, 39);
            this.btnitineraire.Name = "btnitineraire";
            this.btnitineraire.Size = new System.Drawing.Size(128, 61);
            this.btnitineraire.TabIndex = 1;
            this.btnitineraire.Text = "Afficher Itineraire";
            this.btnitineraire.UseVisualStyleBackColor = true;
            this.btnitineraire.Click += new System.EventHandler(this.btnitineraire_Click);
            // 
            // btnAfficherMetro
            // 
            this.btnAfficherMetro.BackColor = System.Drawing.Color.Gray;
            this.btnAfficherMetro.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAfficherMetro.Location = new System.Drawing.Point(44, 39);
            this.btnAfficherMetro.Name = "btnAfficherMetro";
            this.btnAfficherMetro.Size = new System.Drawing.Size(128, 61);
            this.btnAfficherMetro.TabIndex = 0;
            this.btnAfficherMetro.Text = "Afficher Métro";
            this.btnAfficherMetro.UseVisualStyleBackColor = false;
            this.btnAfficherMetro.Click += new System.EventHandler(this.btnAfficherMetro_Click);
            // 
            // btnRetourMenu
            // 
            this.btnRetourMenu.Location = new System.Drawing.Point(12, 8);
            this.btnRetourMenu.Name = "btnRetourMenu";
            this.btnRetourMenu.Size = new System.Drawing.Size(98, 41);
            this.btnRetourMenu.TabIndex = 1;
            this.btnRetourMenu.Text = "Retour";
            this.btnRetourMenu.UseVisualStyleBackColor = true;
            this.btnRetourMenu.Click += new System.EventHandler(this.btnRetourMenu_Click);
            // 
            // btnGrapheinfometro
            // 
            this.btnGrapheinfometro.Location = new System.Drawing.Point(24, 371);
            this.btnGrapheinfometro.Name = "btnGrapheinfometro";
            this.btnGrapheinfometro.Size = new System.Drawing.Size(128, 61);
            this.btnGrapheinfometro.TabIndex = 2;
            this.btnGrapheinfometro.Text = "Afficher les informations du metro";
            this.btnGrapheinfometro.UseVisualStyleBackColor = true;
            this.btnGrapheinfometro.Click += new System.EventHandler(this.btnGrapheinfometro_Click);
            // 
            // btngrapheinfoligne
            // 
            this.btngrapheinfoligne.Location = new System.Drawing.Point(24, 452);
            this.btngrapheinfoligne.Name = "btngrapheinfoligne";
            this.btngrapheinfoligne.Size = new System.Drawing.Size(128, 61);
            this.btngrapheinfoligne.TabIndex = 3;
            this.btngrapheinfoligne.Text = "Afficher les stations d\'une ligne de metro";
            this.btngrapheinfoligne.UseVisualStyleBackColor = true;
            this.btngrapheinfoligne.Click += new System.EventHandler(this.btngrapheinfoligne_Click);
            // 
            // textBoxGrapheRep
            // 
            this.textBoxGrapheRep.Location = new System.Drawing.Point(194, 373);
            this.textBoxGrapheRep.Multiline = true;
            this.textBoxGrapheRep.Name = "textBoxGrapheRep";
            this.textBoxGrapheRep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxGrapheRep.Size = new System.Drawing.Size(490, 192);
            this.textBoxGrapheRep.TabIndex = 4;
            // 
            // comboBoxgrapheligne
            // 
            this.comboBoxgrapheligne.FormattingEnabled = true;
            this.comboBoxgrapheligne.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "3bis",
            "4",
            "5",
            "6",
            "7",
            "7bis",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14"});
            this.comboBoxgrapheligne.Location = new System.Drawing.Point(30, 532);
            this.comboBoxgrapheligne.Name = "comboBoxgrapheligne";
            this.comboBoxgrapheligne.Size = new System.Drawing.Size(121, 24);
            this.comboBoxgrapheligne.TabIndex = 5;
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
            this.tabModuleClient.ResumeLayout(false);
            this.tabModuleClient.PerformLayout();
            this.tabModuleCommande.ResumeLayout(false);
            this.tabModuleStatistiques.ResumeLayout(false);
            this.tabModuleStatistiques.PerformLayout();
            this.tabModuleGraphe.ResumeLayout(false);
            this.tabModuleGraphe.PerformLayout();
            this.ResumeLayout(false);

        }

        private void FormModules_Load(object sender, EventArgs e)
        {

        }

        private void btnRetourMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            main.Show();
            
        }

        #region Graphe
        private void btnAfficherMetro_Click(object sender, EventArgs e)
        {
            FormAffichierCarte formAffichage = new FormAffichierCarte(this, grapheMetro);

            int largeur = formAffichage.pictureBoxCarte.Width;
            int hauteur = formAffichage.pictureBoxCarte.Height;

            VisualisationCarte visMetro = new VisualisationCarte(largeur, hauteur);
            visMetro.DessinerGraphe(grapheMetro);
            visMetro.SauvegarderImage("metro.png");

            

            using (var stream = new MemoryStream(File.ReadAllBytes("metro.png")))
            {
                formAffichage.pictureBoxCarte.Image = Image.FromStream(stream);
            }
            this.Hide();
            formAffichage.Show();
        }

        private void tabModuleGraphe_Click(object sender, EventArgs e)
        {

        }
        private void btnitineraire_Click(object sender, EventArgs e)
        {
            FormAfficherItineraireModule formAfficherItineraire = new FormAfficherItineraireModule(this, grapheMetro);
            this.Hide();
            formAfficherItineraire.Show();
        }
        private void btnGrapheinfometro_Click(object sender, EventArgs e)
        {
            string rep = "Informations du metro :\r\n";

            // on cree des dictionnaires pour compter les stations
            Dictionary<string, int> stationsParLigne = new Dictionary<string, int>();
            Dictionary<string, List<string>> nomsStationsParLigne = new Dictionary<string, List<string>>();

            // on cree une liste pour toutes les stations uniques
            List<string> toutesLesStations = new List<string>();

            // on parcourt tous les noeuds du graphe
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                // on ajoute la ligne si elle existe pas
                if (!stationsParLigne.ContainsKey(noeud.NumeroLigne))
                {
                    stationsParLigne[noeud.NumeroLigne] = 0;
                    nomsStationsParLigne[noeud.NumeroLigne] = new List<string>();
                }

                // on ajoute la station si elle existe pas dans la ligne
                if (!nomsStationsParLigne[noeud.NumeroLigne].Contains(noeud.NomStation))
                {
                    stationsParLigne[noeud.NumeroLigne]++;
                    nomsStationsParLigne[noeud.NumeroLigne].Add(noeud.NomStation);
                }

                // on ajoute la station a la liste totale si elle existe pas
                if (!toutesLesStations.Contains(noeud.NomStation))
                {
                    toutesLesStations.Add(noeud.NomStation);
                }
            }

            // on affiche les stats
            rep += "Nombre total de stations uniques : " + toutesLesStations.Count + "\r\n";
            Console.WriteLine("Nombre total de stations uniques : " + toutesLesStations.Count);
            rep += "Nombre total de noeuds (avec doublons) : " + grapheMetro.Noeuds.Count + "\r\n";
            Console.WriteLine("Nombre total de noeuds (avec doublons) : " + grapheMetro.Noeuds.Count);
            rep += "Nombre total de liens : " + grapheMetro.Liens.Count + "\r\n";
            Console.WriteLine("Nombre total de liens : " + grapheMetro.Liens.Count);
            rep += "Nombre total de lignes : " + stationsParLigne.Count + "\r\n";
            Console.WriteLine("\nNombre de stations par ligne :");
            rep += "Nombre total de lignes : " + stationsParLigne.Count + "\r\n";
            Console.WriteLine("Nombre total de lignes : " + stationsParLigne.Count);

            // on affiche le nombre de stations par ligne
            foreach (KeyValuePair<string, int> ligne in stationsParLigne.OrderBy(l => l.Key))
            {
                rep += "Ligne " + ligne.Key + " : " + ligne.Value + " stations\r\n";
            }
            textBoxGrapheRep.Text = rep;

        }
        private void btngrapheinfoligne_Click(object sender, EventArgs e)
        {
            // Corrected the issue by using SelectedItem or SelectedValue instead of Value  
            string ligneChoisie = comboBoxgrapheligne.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ligneChoisie))
            {
                MessageBox.Show("Veuillez sélectionner une ligne de métro.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create a list for the stations of the selected line  
            string rep = "Stations de la ligne " + ligneChoisie + " :\r\n";
            List<string> stationsDeLigne = new List<string>();

            // Add all stations of the selected line  
            foreach (Noeud<int> noeud in grapheMetro.Noeuds.Values)
            {
                if (noeud.NumeroLigne == ligneChoisie)
                {
                    stationsDeLigne.Add(noeud.NomStation);
                }
            }

            // Display the stations in order  
            foreach (string station in stationsDeLigne.OrderBy(s => s))
            {
                rep += "- " + station + "\r\n";
            }
            rep += "\r\n";
            rep += "Nombre total de stations : " + stationsDeLigne.Count + "\r\n";

            textBoxGrapheRep.Text = rep;
        }

        #endregion

        #region Client

        private void btnclienttrialpha_Click(object sender, EventArgs e)
        {
            try
            {
                string requete = "SELECT u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro FROM utilisateur u, client c WHERE u.id_utilisateur = c.id_utilisateur ORDER BY u.nom ASC, u.prénom ASC";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "Liste des clients par ordre alphabetique :\r\n";
                rep += "----------------------------------------\r\n";

                while (reader.Read())
                {
                    rep += "ID : " + reader["id_utilisateur"] + "\r\n";
                    rep += "Nom : " + reader["nom"] + "\r\n";
                    rep += "Prenom : " + reader["prénom"] + "\r\n";
                    rep += "Adresse : " + reader["adresse"] + "\r\n";
                    rep += "Station Metro : " + reader["StationMetro"] + "\r\n";
                    rep += "\r\n"; 
                }

                textBoxclientrep.Text = rep;

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage des clients : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabModuleClient_Click(object sender, EventArgs e)
        {

        }

        private void labelClientrep_Click(object sender, EventArgs e)
        {

        }

        private void btnclientparrue_Click(object sender, EventArgs e)
        {
            try
            {
                string requete = "SELECT u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro FROM utilisateur u, client c WHERE u.id_utilisateur = c.id_utilisateur ORDER BY u.adresse ASC";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "Liste des clients par rue :\r\n";
                rep += "----------------------------------------\r\n";

                while (reader.Read())
                {
                    rep += "ID : " + reader["id_utilisateur"] + "\r\n";
                    rep += "Nom : " + reader["nom"] + "\r\n";
                    rep += "Prenom : " + reader["prénom"] + "\r\n";
                    rep += "Adresse : " + reader["adresse"] + "\r\n";
                    rep += "Station Metro : " + reader["StationMetro"] + "\r\n";
                    rep += "\r\n";
                }
                textBoxclientrep.Text = rep;

                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage des clients : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClientparachat_Click(object sender, EventArgs e)
        {
            try
            {
                string requete = "SELECT u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro, SUM(co.prix_total) as total FROM utilisateur u, client c, Commande_ co WHERE u.id_utilisateur = c.id_utilisateur AND c.id_client = co.id_client GROUP BY u.id_utilisateur, u.nom, u.prénom, u.adresse, c.StationMetro ORDER BY total DESC";
                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande.CommandText = requete;

                MySqlDataReader reader = commande.ExecuteReader();

                string rep = "Liste des clients par montant des achats :\r\n";

                rep += "----------------------------------------\r\n";
                while (reader.Read())
                {
                    rep += "ID : " + reader["id_utilisateur"] + "\r\n";
                    rep += "Nom : " + reader["nom"] + "\r\n";
                    rep += "Prenom : " + reader["prénom"] + "\r\n";
                    rep += "Adresse : " + reader["adresse"] + "\r\n";
                    rep += "Station Metro : " + reader["StationMetro"] + "\r\n";
                    rep += "Total des achats : " + reader["total"] + " euros\r\n";
                    rep += "\r\n";
                }
                textBoxclientrep.Text = rep;
                reader.Close();
                commande.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage des clients : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClientAjoutClient_Click(object sender, EventArgs e)
        {
            // a faire
            MessageBox.Show("Ajout d'un client à faire");
        }

        private void btnClientModifclient_Click(object sender, EventArgs e)
        {
            // a faire
            MessageBox.Show("Modification d'un client à faire");
        }

        private void btnClientSuppClient_Click(object sender, EventArgs e)
        {
            // a faire
            MessageBox.Show("Suppression d'un client à faire");
        }

        #endregion

        #region Statistiques
        private void btnstatlivparcuisinier_Click(object sender, EventArgs e)
        {
            try
            {
                // on fait une requete pour avoir les livraisons par cuisinier
                string requete = "SELECT nom, prénom, nombre_livraisons FROM cuisinier, utilisateur " +
                               "WHERE cuisinier.id_utilisateur = utilisateur.id_utilisateur";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();

                string rep = "Liste des cuisiniers et leurs livraisons :\r\n";
                rep += "----------------------------------\r\n";

                // on affiche chaque cuisinier avec son nombre de livraisons
                while (reader.Read())
                {
                    string nom = reader["nom"].ToString();

                    string prenom = reader["prénom"].ToString();
                    string nbLivraisons = reader["nombre_livraisons"].ToString();
                    rep += "Cuisinier : " + prenom + " " + nom + "\r\n";
                    rep+= "\r\n";
                }
                textBoxstatrep.Text = rep;
                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
        }
        private void btnstatcomparper_Click(object sender, EventArgs e)
        {

            string dateDebut = dateTimePickerstatdebut.Value.ToString("yyyy-MM-dd");
            string dateFin = dateTimePickerstatFin.Value.ToString("yyyy-MM-dd");

            
            try
            {
                // on fait une requete pour avoir les commandes entre les deux dates
                string requete = "SELECT id_commande, date_commande, prix_total FROM Commande_ " +
                               "WHERE date_commande >= '" + dateDebut + "' " +
                               "AND date_commande <= '" + dateFin + "'";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();
                string rep = "\nvoici les commande entre " + dateDebut + " et " + dateFin + "\r\n";
                
                rep+= "--------------------------------\r\n";

                // on affiche chaque commande avec ses infos
                while (reader.Read())
                {
                    string idCommande = reader["id_commande"].ToString();
                    string date = reader["date_commande"].ToString();
                    string prix = reader["prix_total"].ToString();
                    rep += "commande numero " + idCommande + " faite le " + date + " pour " + prix + " euro\r\n";
                }
                rep += "----------------------------------\r\n";
                textBoxstatrep.Text = rep;
                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }


        }

        private void btnstatmoyenneprix_Click(object sender, EventArgs e)
        {
            try
            {
                // on fait une requete pour avoir la moyenne des prix
                string requete = "SELECT AVG(prix_total) as moyenne FROM Commande_";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();
                string rep = "";
                // on affiche la moyenne arrondie a 2 chiffres
                while (reader.Read())
                {
                    double moyenne = Convert.ToDouble(reader["moyenne"]);
                    // on arrondi a 2 chiffres apres la virgule
                    moyenne = Math.Round(moyenne, 2);
                    rep = "La moyenne des prix des commandes est de : " + moyenne + " euros\r\n";
                }
                textBoxstatrep.Text = rep;

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
        }

        private void btnstatmoyennecompteclient_Click(object sender, EventArgs e)
        {
            try
            {
                // on fait une requete pour avoir le total depense par client
                string requete = "SELECT nom, prénom, SUM(montant) as total FROM Transaction_, Commande_, client, utilisateur " +
                               "WHERE Transaction_.id_commande = Commande_.id_commande " +
                               "AND Commande_.id_client = client.id_client " +
                               "AND client.id_utilisateur = utilisateur.id_utilisateur " +
                               "GROUP BY nom, prénom";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();
                string rep = "\nvoila combien les client on depense";
                rep += "\r\n----------------------------------\r\n";

                while (reader.Read())
                {
                    string nom = reader["nom"].ToString();
                    string prenom = reader["prénom"].ToString();
                    double total = Convert.ToDouble(reader["total"]);
                    total = Math.Round(total, 2);
                    rep += "Client : " + prenom + " " + nom + "\r\n";
                    rep += "Total depense : " + total + " euros\r\n";
                    rep += "\r\n";
                }
                rep += "----------------------------------\r\n";
                textBoxstatrep.Text = rep;

                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
        }

        private void btnstatcomdpartype_Click(object sender, EventArgs e)
        {


            string dateDebut = dateTimePickerstatdebut.Value.ToString("yyyy-MM-dd");
            string dateFin = dateTimePickerstatFin.Value.ToString("yyyy-MM-dd");
            

            try
            {
                // on fait une requete pour avoir le nombre de commandes par type de plat
                string requete = "SELECT type as type_plat, COUNT(*) as nombre FROM Plat_, Commande_ " +
                               "WHERE Plat_.id_plat = Commande_.id_plat " +
                               "AND date_commande >= '" + dateDebut + "' " +
                               "AND date_commande <= '" + dateFin + "' " +
                               "GROUP BY type";

                MySqlCommand commande0 = new MySqlCommand(requete, connexionBDD.maConnexion);
                commande0.CommandText = requete;

                MySqlDataReader reader = commande0.ExecuteReader();
                string rep = "\nvoici les commande entre " + dateDebut + " et " + dateFin + "\r\n";
                rep += "----------------------------------\r\n";

                // on affiche chaque type de plat avec son nombre de commandes
                while (reader.Read())
                {
                    string type = reader["type_plat"].ToString();
                    string nombre = reader["nombre"].ToString();
                    rep += "Type de plat : " + type + "\r\n";
                    rep+= "\r\n";
                }
                rep += "----------------------------------\r\n";
                textBoxstatrep.Text = rep;
                reader.Close();
                commande0.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oups ya une erreur : " + ex.Message);
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabModuleStatistiques_Click(object sender, EventArgs e)
        {

        }



        #endregion

        #region Commandes
        private void btncommandecreer_Click(object sender, EventArgs e)
        {

        }

        

        private void tabModuleCommande_Click(object sender, EventArgs e)
        {

        }

        private void btncommandeitineraire_Click(object sender, EventArgs e)
        {

        }

        private void btncommandemodif_Click(object sender, EventArgs e)
        {

        }

        private void btncommandeprix_Click(object sender, EventArgs e)
        {

        }

        
    }
#endregion
}