namespace LivrableV3
{
    partial class FormAffichierCarte
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
            this.pictureBoxCarte = new System.Windows.Forms.PictureBox();
            this.btnsatelite = new System.Windows.Forms.Button();
            this.btnbasic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCarte)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRetour
            // 
            this.btnRetour.Location = new System.Drawing.Point(13, 14);
            this.btnRetour.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(122, 54);
            this.btnRetour.TabIndex = 0;
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = true;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);
            // 
            // pictureBoxCarte
            // 
            this.pictureBoxCarte.Location = new System.Drawing.Point(28, 94);
            this.pictureBoxCarte.Name = "pictureBoxCarte";
            this.pictureBoxCarte.Size = new System.Drawing.Size(725, 615);
            this.pictureBoxCarte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCarte.TabIndex = 1;
            this.pictureBoxCarte.TabStop = false;
            this.pictureBoxCarte.Click += new System.EventHandler(this.pictureBoxCarte_Click);
            // 
            // btnsatelite
            // 
            this.btnsatelite.Location = new System.Drawing.Point(609, 33);
            this.btnsatelite.Name = "btnsatelite";
            this.btnsatelite.Size = new System.Drawing.Size(144, 45);
            this.btnsatelite.TabIndex = 2;
            this.btnsatelite.Text = "Satelite";
            this.btnsatelite.UseVisualStyleBackColor = true;
            this.btnsatelite.Click += new System.EventHandler(this.btnsatelite_Click);
            // 
            // btnbasic
            // 
            this.btnbasic.Location = new System.Drawing.Point(437, 33);
            this.btnbasic.Name = "btnbasic";
            this.btnbasic.Size = new System.Drawing.Size(144, 45);
            this.btnbasic.TabIndex = 3;
            this.btnbasic.Text = "Basic";
            this.btnbasic.UseVisualStyleBackColor = true;
            this.btnbasic.Click += new System.EventHandler(this.btnbasic_Click);
            // 
            // FormAffichierCarte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.btnbasic);
            this.Controls.Add(this.btnsatelite);
            this.Controls.Add(this.pictureBoxCarte);
            this.Controls.Add(this.btnRetour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAffichierCarte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAffichierCarte";
            this.Load += new System.EventHandler(this.FormAffichierCarte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCarte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRetour;
        public System.Windows.Forms.PictureBox pictureBoxCarte;
        private System.Windows.Forms.Button btnsatelite;
        private System.Windows.Forms.Button btnbasic;
    }
}