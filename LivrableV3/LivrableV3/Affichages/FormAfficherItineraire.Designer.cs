namespace LivrableV3
{
    partial class FormAfficheritineraire
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
            this.pictureBoxitineraire = new System.Windows.Forms.PictureBox();
            this.textBoxrep = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxitineraire)).BeginInit();
            this.SuspendLayout();
            // 
            // btnretour
            // 
            this.btnretour.Location = new System.Drawing.Point(14, 12);
            this.btnretour.Name = "btnretour";
            this.btnretour.Size = new System.Drawing.Size(118, 50);
            this.btnretour.TabIndex = 0;
            this.btnretour.Text = "Retour";
            this.btnretour.UseVisualStyleBackColor = true;
            this.btnretour.Click += new System.EventHandler(this.btnretour_Click);
            // 
            // labeltitre
            // 
            this.labeltitre.AutoSize = true;
            this.labeltitre.Location = new System.Drawing.Point(274, 32);
            this.labeltitre.Name = "labeltitre";
            this.labeltitre.Size = new System.Drawing.Size(164, 25);
            this.labeltitre.TabIndex = 1;
            this.labeltitre.Text = "Afficher l\'itineraire";
            // 
            // pictureBoxitineraire
            // 
            this.pictureBoxitineraire.Location = new System.Drawing.Point(22, 104);
            this.pictureBoxitineraire.Name = "pictureBoxitineraire";
            this.pictureBoxitineraire.Size = new System.Drawing.Size(739, 493);
            this.pictureBoxitineraire.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxitineraire.TabIndex = 2;
            this.pictureBoxitineraire.TabStop = false;
            // 
            // textBoxrep
            // 
            this.textBoxrep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxrep.Location = new System.Drawing.Point(28, 616);
            this.textBoxrep.Multiline = true;
            this.textBoxrep.Name = "textBoxrep";
            this.textBoxrep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxrep.Size = new System.Drawing.Size(719, 118);
            this.textBoxrep.TabIndex = 3;
            // 
            // FormAfficheritineraire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.textBoxrep);
            this.Controls.Add(this.pictureBoxitineraire);
            this.Controls.Add(this.labeltitre);
            this.Controls.Add(this.btnretour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAfficheritineraire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAfficheritineraire";
            this.Load += new System.EventHandler(this.FormAfficheritineraire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxitineraire)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnretour;
        private System.Windows.Forms.Label labeltitre;
        private System.Windows.Forms.PictureBox pictureBoxitineraire;
        private System.Windows.Forms.TextBox textBoxrep;
    }
}