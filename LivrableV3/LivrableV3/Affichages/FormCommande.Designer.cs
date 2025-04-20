namespace LivrableV3
{
    partial class FormCommande
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
            this.labeltitre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxchoixplat = new System.Windows.Forms.ComboBox();
            this.btncommander = new System.Windows.Forms.Button();
            this.labelprix = new System.Windows.Forms.Label();
            this.textBoxprix = new System.Windows.Forms.TextBox();
            this.textBoxtemps = new System.Windows.Forms.TextBox();
            this.labeltemps = new System.Windows.Forms.Label();
            this.btnvoiritineraire = new System.Windows.Forms.Button();
            this.labelnombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxcuisinier = new System.Windows.Forms.TextBox();
            this.pictureBoxphotoplat = new System.Windows.Forms.PictureBox();
            this.comboBoxnombre = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxphotoplat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRetour
            // 
            this.btnRetour.Location = new System.Drawing.Point(12, 12);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(93, 44);
            this.btnRetour.TabIndex = 0;
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = true;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);
            // 
            // labeltitre
            // 
            this.labeltitre.AutoSize = true;
            this.labeltitre.Location = new System.Drawing.Point(313, 31);
            this.labeltitre.Name = "labeltitre";
            this.labeltitre.Size = new System.Drawing.Size(120, 25);
            this.labeltitre.TabIndex = 1;
            this.labeltitre.Text = "Commander";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "plats";
            // 
            // comboBoxchoixplat
            // 
            this.comboBoxchoixplat.FormattingEnabled = true;
            this.comboBoxchoixplat.Location = new System.Drawing.Point(285, 115);
            this.comboBoxchoixplat.Name = "comboBoxchoixplat";
            this.comboBoxchoixplat.Size = new System.Drawing.Size(187, 33);
            this.comboBoxchoixplat.TabIndex = 3;
            this.comboBoxchoixplat.SelectedIndexChanged += new System.EventHandler(this.comboBoxchoixplat_SelectedIndexChanged);
            // 
            // btncommander
            // 
            this.btncommander.Location = new System.Drawing.Point(309, 648);
            this.btncommander.Name = "btncommander";
            this.btncommander.Size = new System.Drawing.Size(145, 34);
            this.btncommander.TabIndex = 4;
            this.btncommander.Text = "Commander";
            this.btncommander.UseVisualStyleBackColor = true;
            this.btncommander.Click += new System.EventHandler(this.btncommander_Click);
            // 
            // labelprix
            // 
            this.labelprix.AutoSize = true;
            this.labelprix.Location = new System.Drawing.Point(221, 596);
            this.labelprix.Name = "labelprix";
            this.labelprix.Size = new System.Drawing.Size(56, 25);
            this.labelprix.TabIndex = 5;
            this.labelprix.Text = "Prix :";
            // 
            // textBoxprix
            // 
            this.textBoxprix.Location = new System.Drawing.Point(283, 596);
            this.textBoxprix.Name = "textBoxprix";
            this.textBoxprix.Size = new System.Drawing.Size(207, 30);
            this.textBoxprix.TabIndex = 6;
            // 
            // textBoxtemps
            // 
            this.textBoxtemps.Location = new System.Drawing.Point(285, 540);
            this.textBoxtemps.Name = "textBoxtemps";
            this.textBoxtemps.Size = new System.Drawing.Size(204, 30);
            this.textBoxtemps.TabIndex = 7;
            // 
            // labeltemps
            // 
            this.labeltemps.AutoSize = true;
            this.labeltemps.Location = new System.Drawing.Point(89, 540);
            this.labeltemps.Name = "labeltemps";
            this.labeltemps.Size = new System.Drawing.Size(187, 25);
            this.labeltemps.TabIndex = 8;
            this.labeltemps.Text = "Temps de livraison :";
            // 
            // btnvoiritineraire
            // 
            this.btnvoiritineraire.Location = new System.Drawing.Point(530, 530);
            this.btnvoiritineraire.Name = "btnvoiritineraire";
            this.btnvoiritineraire.Size = new System.Drawing.Size(173, 51);
            this.btnvoiritineraire.TabIndex = 9;
            this.btnvoiritineraire.Text = "Itineraire";
            this.btnvoiritineraire.UseVisualStyleBackColor = true;
            this.btnvoiritineraire.Click += new System.EventHandler(this.btnvoiritineraire_Click);
            // 
            // labelnombre
            // 
            this.labelnombre.AutoSize = true;
            this.labelnombre.Location = new System.Drawing.Point(151, 184);
            this.labelnombre.Name = "labelnombre";
            this.labelnombre.Size = new System.Drawing.Size(81, 25);
            this.labelnombre.TabIndex = 10;
            this.labelnombre.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cuisinier :";
            // 
            // textBoxcuisinier
            // 
            this.textBoxcuisinier.Location = new System.Drawing.Point(285, 491);
            this.textBoxcuisinier.Name = "textBoxcuisinier";
            this.textBoxcuisinier.Size = new System.Drawing.Size(204, 30);
            this.textBoxcuisinier.TabIndex = 13;
            // 
            // pictureBoxphotoplat
            // 
            this.pictureBoxphotoplat.Image = global::LivrableV3.Properties.Resources.resto;
            this.pictureBoxphotoplat.Location = new System.Drawing.Point(142, 243);
            this.pictureBoxphotoplat.Name = "pictureBoxphotoplat";
            this.pictureBoxphotoplat.Size = new System.Drawing.Size(506, 208);
            this.pictureBoxphotoplat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxphotoplat.TabIndex = 14;
            this.pictureBoxphotoplat.TabStop = false;
            // 
            // comboBoxnombre
            // 
            this.comboBoxnombre.FormattingEnabled = true;
            this.comboBoxnombre.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBoxnombre.Location = new System.Drawing.Point(285, 176);
            this.comboBoxnombre.Name = "comboBoxnombre";
            this.comboBoxnombre.Size = new System.Drawing.Size(187, 33);
            this.comboBoxnombre.TabIndex = 15;
            this.comboBoxnombre.SelectedIndexChanged += new System.EventHandler(this.comboBoxnombre_SelectedIndexChanged);
            // 
            // FormCommande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.comboBoxnombre);
            this.Controls.Add(this.pictureBoxphotoplat);
            this.Controls.Add(this.textBoxcuisinier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelnombre);
            this.Controls.Add(this.btnvoiritineraire);
            this.Controls.Add(this.labeltemps);
            this.Controls.Add(this.textBoxtemps);
            this.Controls.Add(this.textBoxprix);
            this.Controls.Add(this.labelprix);
            this.Controls.Add(this.btncommander);
            this.Controls.Add(this.comboBoxchoixplat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labeltitre);
            this.Controls.Add(this.btnRetour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormCommande";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCommande";
            this.Load += new System.EventHandler(this.FormCommande_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxphotoplat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRetour;
        private System.Windows.Forms.Label labeltitre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxchoixplat;
        private System.Windows.Forms.Button btncommander;
        private System.Windows.Forms.Label labelprix;
        private System.Windows.Forms.TextBox textBoxprix;
        private System.Windows.Forms.TextBox textBoxtemps;
        private System.Windows.Forms.Label labeltemps;
        private System.Windows.Forms.Button btnvoiritineraire;
        private System.Windows.Forms.Label labelnombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxcuisinier;
        private System.Windows.Forms.PictureBox pictureBoxphotoplat;
        private System.Windows.Forms.ComboBox comboBoxnombre;
    }
}