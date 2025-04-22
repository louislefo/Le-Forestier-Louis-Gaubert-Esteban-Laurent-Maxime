namespace LivrableV3
{
    partial class FormAjoutplat
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
            this.btnretour = new System.Windows.Forms.Button();
            this.labeltitre = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxnomplat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxtype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxportion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerfabrication = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerPeremption = new System.Windows.Forms.DateTimePicker();
            this.Prix = new System.Windows.Forms.Label();
            this.textBoxprix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxNationalite = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxregime = new System.Windows.Forms.ComboBox();
            this.btnimage = new System.Windows.Forms.Button();
            this.pictureBoximagepalt = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoximagepalt)).BeginInit();
            this.SuspendLayout();
            // 
            // btnretour
            // 
            this.btnretour.Location = new System.Drawing.Point(15, 13);
            this.btnretour.Name = "btnretour";
            this.btnretour.Size = new System.Drawing.Size(135, 52);
            this.btnretour.TabIndex = 0;
            this.btnretour.Text = "Retour";
            this.btnretour.UseVisualStyleBackColor = true;
            this.btnretour.Click += new System.EventHandler(this.btnretour_Click);
            // 
            // labeltitre
            // 
            this.labeltitre.AutoSize = true;
            this.labeltitre.Location = new System.Drawing.Point(353, 34);
            this.labeltitre.Name = "labeltitre";
            this.labeltitre.Size = new System.Drawing.Size(137, 25);
            this.labeltitre.TabIndex = 1;
            this.labeltitre.Text = "Ajouter un plat";
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(319, 580);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(181, 68);
            this.btnAjouter.TabIndex = 2;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nom du plat :";
            // 
            // textBoxnomplat
            // 
            this.textBoxnomplat.Location = new System.Drawing.Point(297, 92);
            this.textBoxnomplat.Name = "textBoxnomplat";
            this.textBoxnomplat.Size = new System.Drawing.Size(224, 30);
            this.textBoxnomplat.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type :";
            // 
            // comboBoxtype
            // 
            this.comboBoxtype.FormattingEnabled = true;
            this.comboBoxtype.Items.AddRange(new object[] {
            "entree",
            "plat",
            "dessert"});
            this.comboBoxtype.Location = new System.Drawing.Point(297, 144);
            this.comboBoxtype.Name = "comboBoxtype";
            this.comboBoxtype.Size = new System.Drawing.Size(224, 33);
            this.comboBoxtype.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Portion :";
            // 
            // comboBoxportion
            // 
            this.comboBoxportion.FormattingEnabled = true;
            this.comboBoxportion.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBoxportion.Location = new System.Drawing.Point(300, 197);
            this.comboBoxportion.Name = "comboBoxportion";
            this.comboBoxportion.Size = new System.Drawing.Size(223, 33);
            this.comboBoxportion.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Date Fabrication :";
            // 
            // dateTimePickerfabrication
            // 
            this.dateTimePickerfabrication.Location = new System.Drawing.Point(300, 255);
            this.dateTimePickerfabrication.Name = "dateTimePickerfabrication";
            this.dateTimePickerfabrication.Size = new System.Drawing.Size(223, 30);
            this.dateTimePickerfabrication.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Date Peremption :";
            // 
            // dateTimePickerPeremption
            // 
            this.dateTimePickerPeremption.Location = new System.Drawing.Point(297, 308);
            this.dateTimePickerPeremption.Name = "dateTimePickerPeremption";
            this.dateTimePickerPeremption.Size = new System.Drawing.Size(226, 30);
            this.dateTimePickerPeremption.TabIndex = 12;
            // 
            // Prix
            // 
            this.Prix.AutoSize = true;
            this.Prix.Location = new System.Drawing.Point(121, 366);
            this.Prix.Name = "Prix";
            this.Prix.Size = new System.Drawing.Size(146, 25);
            this.Prix.TabIndex = 13;
            this.Prix.Text = "Prix/Personne :";
            // 
            // textBoxprix
            // 
            this.textBoxprix.Location = new System.Drawing.Point(297, 366);
            this.textBoxprix.Name = "textBoxprix";
            this.textBoxprix.Size = new System.Drawing.Size(225, 30);
            this.textBoxprix.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(538, 369);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "€";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 422);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 25);
            this.label7.TabIndex = 16;
            this.label7.Text = "Nationalité :";
            // 
            // textBoxNationalite
            // 
            this.textBoxNationalite.Location = new System.Drawing.Point(296, 419);
            this.textBoxNationalite.Name = "textBoxNationalite";
            this.textBoxNationalite.Size = new System.Drawing.Size(226, 30);
            this.textBoxNationalite.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 475);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 25);
            this.label8.TabIndex = 18;
            this.label8.Text = "Régime :";
            // 
            // comboBoxregime
            // 
            this.comboBoxregime.FormattingEnabled = true;
            this.comboBoxregime.Items.AddRange(new object[] {
            "Normal",
            "vegetarien",
            "vegan"});
            this.comboBoxregime.Location = new System.Drawing.Point(298, 472);
            this.comboBoxregime.Name = "comboBoxregime";
            this.comboBoxregime.Size = new System.Drawing.Size(226, 33);
            this.comboBoxregime.TabIndex = 19;
            // 
            // btnimage
            // 
            this.btnimage.Location = new System.Drawing.Point(300, 520);
            this.btnimage.Name = "btnimage";
            this.btnimage.Size = new System.Drawing.Size(224, 43);
            this.btnimage.TabIndex = 20;
            this.btnimage.Text = "Image Plat";
            this.btnimage.UseVisualStyleBackColor = true;
            this.btnimage.Click += new System.EventHandler(this.btnimage_Click);
            // 
            // pictureBoximagepalt
            // 
            this.pictureBoximagepalt.Location = new System.Drawing.Point(12, 555);
            this.pictureBoximagepalt.Name = "pictureBoximagepalt";
            this.pictureBoximagepalt.Size = new System.Drawing.Size(274, 189);
            this.pictureBoximagepalt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoximagepalt.TabIndex = 21;
            this.pictureBoximagepalt.TabStop = false;
            // 
            // FormAjoutplat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.pictureBoximagepalt);
            this.Controls.Add(this.btnimage);
            this.Controls.Add(this.comboBoxregime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxNationalite);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxprix);
            this.Controls.Add(this.Prix);
            this.Controls.Add(this.dateTimePickerPeremption);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePickerfabrication);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxportion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxtype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxnomplat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.labeltitre);
            this.Controls.Add(this.btnretour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAjoutplat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAjoutplat";
            this.Load += new System.EventHandler(this.FormAjoutplat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoximagepalt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnretour;
        private System.Windows.Forms.Label labeltitre;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxnomplat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxtype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxportion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerfabrication;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerPeremption;
        private System.Windows.Forms.Label Prix;
        private System.Windows.Forms.TextBox textBoxprix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNationalite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxregime;
        private System.Windows.Forms.Button btnimage;
        private System.Windows.Forms.PictureBox pictureBoximagepalt;
    }
}