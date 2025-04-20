namespace LivrableV3.Affichages
{
    partial class FormNoterPlat
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnretour = new System.Windows.Forms.Button();
            this.labellequel = new System.Windows.Forms.Label();
            this.comboBoxcommande = new System.Windows.Forms.ComboBox();
            this.labelnote = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxcommentaire = new System.Windows.Forms.TextBox();
            this.btnenvoyer = new System.Windows.Forms.Button();
            this.labelplat = new System.Windows.Forms.Label();
            this.comboBoxnomplat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(412, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Noter le plat";
            // 
            // btnretour
            // 
            this.btnretour.Location = new System.Drawing.Point(17, 16);
            this.btnretour.Name = "btnretour";
            this.btnretour.Size = new System.Drawing.Size(118, 56);
            this.btnretour.TabIndex = 1;
            this.btnretour.Text = "Retour";
            this.btnretour.UseVisualStyleBackColor = true;
            this.btnretour.Click += new System.EventHandler(this.btnretour_Click);
            // 
            // labellequel
            // 
            this.labellequel.AutoSize = true;
            this.labellequel.Location = new System.Drawing.Point(279, 112);
            this.labellequel.Name = "labellequel";
            this.labellequel.Size = new System.Drawing.Size(82, 25);
            this.labellequel.TabIndex = 2;
            this.labellequel.Text = "Lequel :";
            // 
            // comboBoxcommande
            // 
            this.comboBoxcommande.FormattingEnabled = true;
            this.comboBoxcommande.Location = new System.Drawing.Point(384, 109);
            this.comboBoxcommande.Name = "comboBoxcommande";
            this.comboBoxcommande.Size = new System.Drawing.Size(163, 33);
            this.comboBoxcommande.TabIndex = 3;
            // 
            // labelnote
            // 
            this.labelnote.AutoSize = true;
            this.labelnote.Location = new System.Drawing.Point(260, 223);
            this.labelnote.Name = "labelnote";
            this.labelnote.Size = new System.Drawing.Size(91, 25);
            this.labelnote.TabIndex = 4;
            this.labelnote.Text = "Note / 5 :";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(384, 220);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(163, 30);
            this.textBoxNote.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(553, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "/5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Commentaire :";
            // 
            // textBoxcommentaire
            // 
            this.textBoxcommentaire.Location = new System.Drawing.Point(248, 312);
            this.textBoxcommentaire.Multiline = true;
            this.textBoxcommentaire.Name = "textBoxcommentaire";
            this.textBoxcommentaire.Size = new System.Drawing.Size(473, 306);
            this.textBoxcommentaire.TabIndex = 8;
            // 
            // btnenvoyer
            // 
            this.btnenvoyer.Location = new System.Drawing.Point(359, 652);
            this.btnenvoyer.Name = "btnenvoyer";
            this.btnenvoyer.Size = new System.Drawing.Size(232, 61);
            this.btnenvoyer.TabIndex = 9;
            this.btnenvoyer.Text = "Envoyer";
            this.btnenvoyer.UseVisualStyleBackColor = true;
            this.btnenvoyer.Click += new System.EventHandler(this.btnenvoyer_Click);
            // 
            // labelplat
            // 
            this.labelplat.AutoSize = true;
            this.labelplat.Location = new System.Drawing.Point(284, 164);
            this.labelplat.Name = "labelplat";
            this.labelplat.Size = new System.Drawing.Size(69, 31);
            this.labelplat.TabIndex = 10;
            this.labelplat.Text = "Plats";
            // 
            // comboBoxnomplat
            // 
            this.comboBoxnomplat.FormattingEnabled = true;
            this.comboBoxnomplat.Location = new System.Drawing.Point(384, 161);
            this.comboBoxnomplat.Name = "comboBoxnomplat";
            this.comboBoxnomplat.Size = new System.Drawing.Size(163, 33);
            this.comboBoxnomplat.TabIndex = 11;
            // 
            // FormNoterPlat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(982, 953);
            this.Controls.Add(this.comboBoxnomplat);
            this.Controls.Add(this.labelplat);
            this.Controls.Add(this.btnenvoyer);
            this.Controls.Add(this.textBoxcommentaire);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.labelnote);
            this.Controls.Add(this.comboBoxcommande);
            this.Controls.Add(this.labellequel);
            this.Controls.Add(this.btnretour);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormNoterPlat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormNoterPlat";
            this.Load += new System.EventHandler(this.FormNoterPlat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnretour;
        private System.Windows.Forms.Label labellequel;
        private System.Windows.Forms.ComboBox comboBoxcommande;
        private System.Windows.Forms.Label labelnote;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxcommentaire;
        private System.Windows.Forms.Button btnenvoyer;
        private System.Windows.Forms.Label labelplat;
        private System.Windows.Forms.ComboBox comboBoxnomplat;
    }
}