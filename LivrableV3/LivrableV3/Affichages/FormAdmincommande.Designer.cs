namespace LivrableV3
{
    partial class FormAdmincommande
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmincommande));
            this.btnretour = new System.Windows.Forms.Button();
            this.labeltitre = new System.Windows.Forms.Label();
            this.btnajouter = new System.Windows.Forms.Button();
            this.btnmodifier = new System.Windows.Forms.Button();
            this.comboBoxnombre = new System.Windows.Forms.ComboBox();
            this.textBoxcuisinier = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelnombre = new System.Windows.Forms.Label();
            this.textBoxprix = new System.Windows.Forms.TextBox();
            this.labelprix = new System.Windows.Forms.Label();
            this.comboBoxchoixplat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxcommande = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxstatut = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnclear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxclient = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnretour
            // 
            this.btnretour.Location = new System.Drawing.Point(10, 9);
            this.btnretour.Name = "btnretour";
            this.btnretour.Size = new System.Drawing.Size(132, 56);
            this.btnretour.TabIndex = 0;
            this.btnretour.Text = "Retour";
            this.btnretour.UseVisualStyleBackColor = true;
            this.btnretour.Click += new System.EventHandler(this.btnretour_Click);
            // 
            // labeltitre
            // 
            this.labeltitre.AutoSize = true;
            this.labeltitre.Location = new System.Drawing.Point(322, 25);
            this.labeltitre.Name = "labeltitre";
            this.labeltitre.Size = new System.Drawing.Size(170, 25);
            this.labeltitre.TabIndex = 1;
            this.labeltitre.Text = "Admin commande";
            // 
            // btnajouter
            // 
            this.btnajouter.Location = new System.Drawing.Point(309, 644);
            this.btnajouter.Name = "btnajouter";
            this.btnajouter.Size = new System.Drawing.Size(205, 57);
            this.btnajouter.TabIndex = 2;
            this.btnajouter.Text = "Ajouter ";
            this.btnajouter.UseVisualStyleBackColor = true;
            this.btnajouter.Click += new System.EventHandler(this.btnajouter_Click);
            // 
            // btnmodifier
            // 
            this.btnmodifier.Location = new System.Drawing.Point(310, 644);
            this.btnmodifier.Name = "btnmodifier";
            this.btnmodifier.Size = new System.Drawing.Size(204, 55);
            this.btnmodifier.TabIndex = 3;
            this.btnmodifier.Text = "modifier";
            this.btnmodifier.UseVisualStyleBackColor = true;
            this.btnmodifier.Click += new System.EventHandler(this.btnmodifier_Click);
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
            this.comboBoxnombre.Location = new System.Drawing.Point(310, 257);
            this.comboBoxnombre.Name = "comboBoxnombre";
            this.comboBoxnombre.Size = new System.Drawing.Size(205, 33);
            this.comboBoxnombre.TabIndex = 27;
            // 
            // textBoxcuisinier
            // 
            this.textBoxcuisinier.Location = new System.Drawing.Point(310, 466);
            this.textBoxcuisinier.Name = "textBoxcuisinier";
            this.textBoxcuisinier.Size = new System.Drawing.Size(204, 30);
            this.textBoxcuisinier.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 469);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Cuisinier :";
            // 
            // labelnombre
            // 
            this.labelnombre.AutoSize = true;
            this.labelnombre.Location = new System.Drawing.Point(199, 265);
            this.labelnombre.Name = "labelnombre";
            this.labelnombre.Size = new System.Drawing.Size(81, 25);
            this.labelnombre.TabIndex = 23;
            this.labelnombre.Text = "Nombre";
            // 
            // textBoxprix
            // 
            this.textBoxprix.Location = new System.Drawing.Point(310, 540);
            this.textBoxprix.Name = "textBoxprix";
            this.textBoxprix.Size = new System.Drawing.Size(204, 30);
            this.textBoxprix.TabIndex = 19;
            // 
            // labelprix
            // 
            this.labelprix.AutoSize = true;
            this.labelprix.Location = new System.Drawing.Point(224, 545);
            this.labelprix.Name = "labelprix";
            this.labelprix.Size = new System.Drawing.Size(56, 25);
            this.labelprix.TabIndex = 18;
            this.labelprix.Text = "Prix :";
            // 
            // comboBoxchoixplat
            // 
            this.comboBoxchoixplat.FormattingEnabled = true;
            this.comboBoxchoixplat.Location = new System.Drawing.Point(310, 178);
            this.comboBoxchoixplat.Name = "comboBoxchoixplat";
            this.comboBoxchoixplat.Size = new System.Drawing.Size(204, 33);
            this.comboBoxchoixplat.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "plats";
            // 
            // comboBoxcommande
            // 
            this.comboBoxcommande.FormattingEnabled = true;
            this.comboBoxcommande.Location = new System.Drawing.Point(310, 102);
            this.comboBoxcommande.Name = "comboBoxcommande";
            this.comboBoxcommande.Size = new System.Drawing.Size(205, 33);
            this.comboBoxcommande.TabIndex = 28;
            this.comboBoxcommande.SelectedIndexChanged += new System.EventHandler(this.comboBoxcommande_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 25);
            this.label3.TabIndex = 29;
            this.label3.Text = "Commande a modifier";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboBoxstatut
            // 
            this.comboBoxstatut.FormattingEnabled = true;
            this.comboBoxstatut.Items.AddRange(new object[] {
            "En attente",
            "Confirmée",
            "En préparation",
            "Livrée",
            "Annulée"});
            this.comboBoxstatut.Location = new System.Drawing.Point(310, 325);
            this.comboBoxstatut.Name = "comboBoxstatut";
            this.comboBoxstatut.Size = new System.Drawing.Size(204, 33);
            this.comboBoxstatut.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 25);
            this.label4.TabIndex = 31;
            this.label4.Text = "statut";
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(541, 102);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(85, 34);
            this.btnclear.TabIndex = 34;
            this.btnclear.Text = "clear";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 403);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 25);
            this.label6.TabIndex = 35;
            this.label6.Text = "Client";
            // 
            // comboBoxclient
            // 
            this.comboBoxclient.FormattingEnabled = true;
            this.comboBoxclient.Location = new System.Drawing.Point(309, 400);
            this.comboBoxclient.Name = "comboBoxclient";
            this.comboBoxclient.Size = new System.Drawing.Size(205, 33);
            this.comboBoxclient.TabIndex = 36;
            // 
            // FormAdmincommande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.comboBoxclient);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxstatut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxcommande);
            this.Controls.Add(this.comboBoxnombre);
            this.Controls.Add(this.textBoxcuisinier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelnombre);
            this.Controls.Add(this.textBoxprix);
            this.Controls.Add(this.labelprix);
            this.Controls.Add(this.comboBoxchoixplat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnmodifier);
            this.Controls.Add(this.btnajouter);
            this.Controls.Add(this.labeltitre);
            this.Controls.Add(this.btnretour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAdmincommande";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAdmincommande";
            this.Load += new System.EventHandler(this.FormAdmincommande_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnretour;
        private System.Windows.Forms.Label labeltitre;
        private System.Windows.Forms.Button btnajouter;
        private System.Windows.Forms.Button btnmodifier;
        private System.Windows.Forms.ComboBox comboBoxnombre;
        private System.Windows.Forms.TextBox textBoxcuisinier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelnombre;
        private System.Windows.Forms.TextBox textBoxprix;
        private System.Windows.Forms.Label labelprix;
        private System.Windows.Forms.ComboBox comboBoxchoixplat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxcommande;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxstatut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxclient;
    }
}