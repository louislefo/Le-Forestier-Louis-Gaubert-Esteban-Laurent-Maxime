namespace LivrableV3
{
    partial class FormAfficherItineraireModule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRetour = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDepart = new System.Windows.Forms.ComboBox();
            this.comboBoxArrivee = new System.Windows.Forms.ComboBox();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.pictureBoxItineraire = new System.Windows.Forms.PictureBox();
            this.textBoxrep = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItineraire)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRetour
            // 
            this.btnRetour.Location = new System.Drawing.Point(12, 12);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(102, 43);
            this.btnRetour.TabIndex = 0;
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = true;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Afficher itineraire";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "De";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "A";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboBoxDepart
            // 
            this.comboBoxDepart.FormattingEnabled = true;
            this.comboBoxDepart.Items.AddRange(new object[] {
            "Abbesses",
            "Al�sia",
            "Alexandre Dumas",
            "Alma - Marceau",
            "Anvers",
            "Argentine",
            "Arts et M�tiers",
            "Assembl�e Nationale",
            "Avenue Emile Zola",
            "Avron",
            "Balard",
            "Barb�s - Rochechouart",
            "Bastille",
            "Bel-Air",
            "Belleville",
            "Bercy",
            "Biblioth�que Fran�ois Mitterrand",
            "Bir-Hakeim",
            "Blanche",
            "Boissi�re",
            "Bolivar",
            "Bonne Nouvelle",
            "Botzaris",
            "Boucicaut",
            "Bourse",
            "Br�guet-Sabin",
            "Brochant",
            "Buttes Chaumont",
            "Buzenval",
            "Cadet",
            "Cambronne",
            "Campo-Formio",
            "Cardinal Lemoine",
            "Censier - Daubenton",
            "Champs-Elys�es - Clemenceau",
            "Chardon Lagache",
            "Charles de Gaulle - Etoile",
            "Charles Michels",
            "Charonne",
            "Ch�teau de Vincennes",
            "Ch�teau d\'Eau",
            "Ch�teau Landon",
            "Ch�teau Rouge",
            "Ch�telet",
            "Chauss�e d\'Antin - La Fayette",
            "Chemin Vert",
            "Chevaleret",
            "Cit�",
            "Cluny - La Sorbonne",
            "Colonel Fabien",
            "Commerce",
            "Concorde",
            "Convention",
            "Corentin Cariou",
            "Corvisart",
            "Cour Saint-Emilion",
            "Courcelles",
            "Couronnes",
            "Crim�e",
            "Danube",
            "Daumesnil",
            "Denfert-Rochereau",
            "Dugommier",
            "Dupleix",
            "Duroc",
            "Ecole Militaire",
            "Edgar Quinet",
            "Eglise d\'Auteuil",
            "Etienne Marcel",
            "Europe",
            "Exelmans",
            "Faidherbe - Chaligny",
            "Falgui�re",
            "F�lix Faure",
            "Filles du Calvaire",
            "Franklin D. Roosevelt",
            "Ga�t�",
            "Gambetta",
            "Gare d\'Austerlitz",
            "Gare de l\'Est",
            "Gare de Lyon",
            "Gare du Nord",
            "George V",
            "Glaci�re",
            "Goncourt",
            "Grands Boulevards",
            "Guy M�quet",
            "Havre-Caumartin",
            "H�tel de Ville",
            "I�na",
            "Invalides",
            "Jacques Bonsergent",
            "Jasmin",
            "Jaur�s",
            "Javel - Andr� Citro�n",
            "Jourdain",
            "Jules Joffrin",
            "Jussieu",
            "Kl�ber",
            "La Chapelle",
            "La Fourche",
            "La Motte-Picquet - Grenelle",
            "La Muette",
            "La Tour-Maubourg",
            "Lamarck - Caulaincourt",
            "Laumi�re",
            "Le Peletier",
            "Ledru-Rollin",
            "Les Gobelins",
            "Les Halles",
            "Li�ge",
            "Louis Blanc",
            "Lourmel",
            "Louvre - Rivoli",
            "Mabillon",
            "Madeleine",
            "Maison Blanche",
            "Malesherbes",
            "Mara�chers",
            "Marcadet - Poissonniers",
            "Marx Dormoy",
            "Maubert - Mutualit�",
            "M�nilmontant",
            "Michel Bizot",
            "Michel-Ange - Auteuil",
            "Michel-Ange - Molitor",
            "Mirabeau",
            "Miromesnil",
            "Monceau",
            "Montgallet",
            "Montparnasse Bienvenue",
            "Mouton-Duvernet",
            "Nation",
            "Nationale",
            "Notre-Dame des Champs",
            "Notre-Dame-de-Lorette",
            "Oberkampf",
            "Od�on",
            "Olympiades",
            "Op�ra",
            "Ourcq",
            "Palais Royal - Mus�e du Louvre",
            "Parmentier",
            "Passy",
            "Pasteur",
            "Pelleport",
            "P�re Lachaise",
            "Pereire",
            "Pernety",
            "Philippe Auguste",
            "Picpus",
            "Pigalle",
            "Place de Clichy",
            "Place des Fêtes",
            "Place des F�tes",
            "Place d\'Italie",
            "Place Monge",
            "Plaisance",
            "Poissonni�re",
            "Pont Cardinet",
            "Pont Marie (Cit� des Arts)",
            "Pont Neuf",
            "Porte Dauphine",
            "Porte d\'Auteuil",
            "Porte de Bagnolet",
            "Porte de Champerret",
            "Porte de Charenton",
            "Porte de Choisy",
            "Porte de Clichy",
            "Porte de Clignancourt",
            "Porte de la Chapelle",
            "Porte de la Villette",
            "Porte de Montreuil",
            "Porte de Pantin",
            "Porte de Saint-Cloud",
            "Porte de Saint-Ouen",
            "Porte de Vanves",
            "Porte de Versailles",
            "Porte de Vincennes",
            "Porte des Lilas",
            "Porte d\'Italie",
            "Porte d\'Ivry",
            "Porte Dor�e",
            "Porte d\'Orl�ans",
            "Porte Maillot",
            "Pr�-Saint-Gervais",
            "Pyramides",
            "Pyr�n�es",
            "Quai de la Gare",
            "Quai de la Rap�e",
            "Quatre Septembre",
            "Rambuteau",
            "Ranelagh",
            "Raspail",
            "R�aumur - S�bastopol",
            "Rennes",
            "R�publique",
            "Reuilly - Diderot",
            "Richard-Lenoir",
            "Richelieu - Drouot",
            "Riquet",
            "Rome",
            "Rue de la Pompe",
            "Rue des Boulets",
            "Rue du Bac",
            "Rue Saint-Maur",
            "Saint-Ambroise",
            "Saint-Augustin",
            "Saint-Fargeau",
            "Saint-Fran�ois-Xavier",
            "Saint-Georges",
            "Saint-Germain-des-Pr�s",
            "Saint-Jacques",
            "Saint-Lazare",
            "Saint-Marcel",
            "Saint-Michel",
            "Saint-Paul (Le Marais)",
            "Saint-Philippe du Roule",
            "Saint-Placide",
            "Saint-S�bastien - Froissart",
            "Saint-Sulpice",
            "S�gur",
            "Sentier",
            "S�vres - Babylone",
            "S�vres-Lecourbe",
            "Simplon",
            "Solf�rino",
            "Stalingrad",
            "Strasbourg - Saint-Denis",
            "Sully - Morland",
            "T�l�graphe",
            "Temple",
            "Ternes",
            "Tolbiac",
            "Trinit� - d\'Estienne d\'Orves",
            "Trocad�ro",
            "Tuileries",
            "Vaneau",
            "Varenne",
            "Vaugirard",
            "Vavin",
            "Victor Hugo",
            "Villiers",
            "Volontaires",
            "Voltaire",
            "Wagram"});
            this.comboBoxDepart.Location = new System.Drawing.Point(94, 98);
            this.comboBoxDepart.Name = "comboBoxDepart";
            this.comboBoxDepart.Size = new System.Drawing.Size(200, 33);
            this.comboBoxDepart.TabIndex = 6;
            this.comboBoxDepart.SelectedIndexChanged += new System.EventHandler(this.comboBoxDepart_SelectedIndexChanged);
            // 
            // comboBoxArrivee
            // 
            this.comboBoxArrivee.FormattingEnabled = true;
            this.comboBoxArrivee.Items.AddRange(new object[] {
            "Abbesses",
            "Al�sia",
            "Alexandre Dumas",
            "Alma - Marceau",
            "Anvers",
            "Argentine",
            "Arts et M�tiers",
            "Assembl�e Nationale",
            "Avenue Emile Zola",
            "Avron",
            "Balard",
            "Barb�s - Rochechouart",
            "Bastille",
            "Bel-Air",
            "Belleville",
            "Bercy",
            "Biblioth�que Fran�ois Mitterrand",
            "Bir-Hakeim",
            "Blanche",
            "Boissi�re",
            "Bolivar",
            "Bonne Nouvelle",
            "Botzaris",
            "Boucicaut",
            "Bourse",
            "Br�guet-Sabin",
            "Brochant",
            "Buttes Chaumont",
            "Buzenval",
            "Cadet",
            "Cambronne",
            "Campo-Formio",
            "Cardinal Lemoine",
            "Censier - Daubenton",
            "Champs-Elys�es - Clemenceau",
            "Chardon Lagache",
            "Charles de Gaulle - Etoile",
            "Charles Michels",
            "Charonne",
            "Ch�teau de Vincennes",
            "Ch�teau d\'Eau",
            "Ch�teau Landon",
            "Ch�teau Rouge",
            "Ch�telet",
            "Chauss�e d\'Antin - La Fayette",
            "Chemin Vert",
            "Chevaleret",
            "Cit�",
            "Cluny - La Sorbonne",
            "Colonel Fabien",
            "Commerce",
            "Concorde",
            "Convention",
            "Corentin Cariou",
            "Corvisart",
            "Cour Saint-Emilion",
            "Courcelles",
            "Couronnes",
            "Crim�e",
            "Danube",
            "Daumesnil",
            "Denfert-Rochereau",
            "Dugommier",
            "Dupleix",
            "Duroc",
            "Ecole Militaire",
            "Edgar Quinet",
            "Eglise d\'Auteuil",
            "Etienne Marcel",
            "Europe",
            "Exelmans",
            "Faidherbe - Chaligny",
            "Falgui�re",
            "F�lix Faure",
            "Filles du Calvaire",
            "Franklin D. Roosevelt",
            "Ga�t�",
            "Gambetta",
            "Gare d\'Austerlitz",
            "Gare de l\'Est",
            "Gare de Lyon",
            "Gare du Nord",
            "George V",
            "Glaci�re",
            "Goncourt",
            "Grands Boulevards",
            "Guy M�quet",
            "Havre-Caumartin",
            "H�tel de Ville",
            "I�na",
            "Invalides",
            "Jacques Bonsergent",
            "Jasmin",
            "Jaur�s",
            "Javel - Andr� Citro�n",
            "Jourdain",
            "Jules Joffrin",
            "Jussieu",
            "Kl�ber",
            "La Chapelle",
            "La Fourche",
            "La Motte-Picquet - Grenelle",
            "La Muette",
            "La Tour-Maubourg",
            "Lamarck - Caulaincourt",
            "Laumi�re",
            "Le Peletier",
            "Ledru-Rollin",
            "Les Gobelins",
            "Les Halles",
            "Li�ge",
            "Louis Blanc",
            "Lourmel",
            "Louvre - Rivoli",
            "Mabillon",
            "Madeleine",
            "Maison Blanche",
            "Malesherbes",
            "Mara�chers",
            "Marcadet - Poissonniers",
            "Marx Dormoy",
            "Maubert - Mutualit�",
            "M�nilmontant",
            "Michel Bizot",
            "Michel-Ange - Auteuil",
            "Michel-Ange - Molitor",
            "Mirabeau",
            "Miromesnil",
            "Monceau",
            "Montgallet",
            "Montparnasse Bienvenue",
            "Mouton-Duvernet",
            "Nation",
            "Nationale",
            "Notre-Dame des Champs",
            "Notre-Dame-de-Lorette",
            "Oberkampf",
            "Od�on",
            "Olympiades",
            "Op�ra",
            "Ourcq",
            "Palais Royal - Mus�e du Louvre",
            "Parmentier",
            "Passy",
            "Pasteur",
            "Pelleport",
            "P�re Lachaise",
            "Pereire",
            "Pernety",
            "Philippe Auguste",
            "Picpus",
            "Pigalle",
            "Place de Clichy",
            "Place des Fêtes",
            "Place des F�tes",
            "Place d\'Italie",
            "Place Monge",
            "Plaisance",
            "Poissonni�re",
            "Pont Cardinet",
            "Pont Marie (Cit� des Arts)",
            "Pont Neuf",
            "Porte Dauphine",
            "Porte d\'Auteuil",
            "Porte de Bagnolet",
            "Porte de Champerret",
            "Porte de Charenton",
            "Porte de Choisy",
            "Porte de Clichy",
            "Porte de Clignancourt",
            "Porte de la Chapelle",
            "Porte de la Villette",
            "Porte de Montreuil",
            "Porte de Pantin",
            "Porte de Saint-Cloud",
            "Porte de Saint-Ouen",
            "Porte de Vanves",
            "Porte de Versailles",
            "Porte de Vincennes",
            "Porte des Lilas",
            "Porte d\'Italie",
            "Porte d\'Ivry",
            "Porte Dor�e",
            "Porte d\'Orl�ans",
            "Porte Maillot",
            "Pr�-Saint-Gervais",
            "Pyramides",
            "Pyr�n�es",
            "Quai de la Gare",
            "Quai de la Rap�e",
            "Quatre Septembre",
            "Rambuteau",
            "Ranelagh",
            "Raspail",
            "R�aumur - S�bastopol",
            "Rennes",
            "R�publique",
            "Reuilly - Diderot",
            "Richard-Lenoir",
            "Richelieu - Drouot",
            "Riquet",
            "Rome",
            "Rue de la Pompe",
            "Rue des Boulets",
            "Rue du Bac",
            "Rue Saint-Maur",
            "Saint-Ambroise",
            "Saint-Augustin",
            "Saint-Fargeau",
            "Saint-Fran�ois-Xavier",
            "Saint-Georges",
            "Saint-Germain-des-Pr�s",
            "Saint-Jacques",
            "Saint-Lazare",
            "Saint-Marcel",
            "Saint-Michel",
            "Saint-Paul (Le Marais)",
            "Saint-Philippe du Roule",
            "Saint-Placide",
            "Saint-S�bastien - Froissart",
            "Saint-Sulpice",
            "S�gur",
            "Sentier",
            "S�vres - Babylone",
            "S�vres-Lecourbe",
            "Simplon",
            "Solf�rino",
            "Stalingrad",
            "Strasbourg - Saint-Denis",
            "Sully - Morland",
            "T�l�graphe",
            "Temple",
            "Ternes",
            "Tolbiac",
            "Trinit� - d\'Estienne d\'Orves",
            "Trocad�ro",
            "Tuileries",
            "Vaneau",
            "Varenne",
            "Vaugirard",
            "Vavin",
            "Victor Hugo",
            "Villiers",
            "Volontaires",
            "Voltaire",
            "Wagram"});
            this.comboBoxArrivee.Location = new System.Drawing.Point(349, 98);
            this.comboBoxArrivee.Name = "comboBoxArrivee";
            this.comboBoxArrivee.Size = new System.Drawing.Size(200, 33);
            this.comboBoxArrivee.TabIndex = 7;
            this.comboBoxArrivee.SelectedIndexChanged += new System.EventHandler(this.comboBoxArrivee_SelectedIndexChanged);
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(589, 89);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(154, 49);
            this.btnRechercher.TabIndex = 8;
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            // 
            // pictureBoxItineraire
            // 
            this.pictureBoxItineraire.Location = new System.Drawing.Point(42, 170);
            this.pictureBoxItineraire.Name = "pictureBoxItineraire";
            this.pictureBoxItineraire.Size = new System.Drawing.Size(700, 380);
            this.pictureBoxItineraire.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxItineraire.TabIndex = 9;
            this.pictureBoxItineraire.TabStop = false;
            this.pictureBoxItineraire.Click += new System.EventHandler(this.pictureBoxItineraire_Click);
            // 
            // textBoxrep
            // 
            this.textBoxrep.Location = new System.Drawing.Point(41, 578);
            this.textBoxrep.Multiline = true;
            this.textBoxrep.Name = "textBoxrep";
            this.textBoxrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxrep.Size = new System.Drawing.Size(701, 154);
            this.textBoxrep.TabIndex = 10;
            // 
            // FormAfficherItineraireModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.textBoxrep);
            this.Controls.Add(this.pictureBoxItineraire);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.comboBoxArrivee);
            this.Controls.Add(this.comboBoxDepart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRetour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAfficherItineraireModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAfficherItineraireModule";
            this.Load += new System.EventHandler(this.FormAfficherItineraire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItineraire)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRetour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxDepart;
        private System.Windows.Forms.ComboBox comboBoxArrivee;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.PictureBox pictureBoxItineraire;
        private System.Windows.Forms.TextBox textBoxrep;
    }
}