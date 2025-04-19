namespace LivrableV3
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btninscription = new System.Windows.Forms.Button();
            this.btnModule = new System.Windows.Forms.Button();
            this.btnquitter = new System.Windows.Forms.Button();
            this.btnConnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btninscription
            // 
            this.btninscription.Location = new System.Drawing.Point(274, 321);
            this.btninscription.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btninscription.Name = "btninscription";
            this.btninscription.Size = new System.Drawing.Size(250, 63);
            this.btninscription.TabIndex = 1;
            this.btninscription.Text = "S\'inscrire";
            this.btninscription.UseVisualStyleBackColor = true;
            this.btninscription.Click += new System.EventHandler(this.btninscription_Click);
            // 
            // btnModule
            // 
            this.btnModule.AutoSize = true;
            this.btnModule.Location = new System.Drawing.Point(274, 423);
            this.btnModule.Name = "btnModule";
            this.btnModule.Size = new System.Drawing.Size(250, 63);
            this.btnModule.TabIndex = 2;
            this.btnModule.Text = "Admin";
            this.btnModule.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnModule.UseVisualStyleBackColor = true;
            this.btnModule.Click += new System.EventHandler(this.btnModule_Click);
            // 
            // btnquitter
            // 
            this.btnquitter.Location = new System.Drawing.Point(274, 525);
            this.btnquitter.Name = "btnquitter";
            this.btnquitter.Size = new System.Drawing.Size(250, 63);
            this.btnquitter.TabIndex = 3;
            this.btnquitter.Text = "Quitter";
            this.btnquitter.UseVisualStyleBackColor = true;
            this.btnquitter.Click += new System.EventHandler(this.btnquitter_Click);
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(274, 221);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(250, 63);
            this.btnConnection.TabIndex = 4;
            this.btnConnection.Text = "Se Connecter";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(283, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 48);
            this.label1.TabIndex = 5;
            this.label1.Text = "LIV\'IN PARIS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 702);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(404, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "Projet de Maxime, Esteban et Louis";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(782, 759);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this.btnquitter);
            this.Controls.Add(this.btnModule);
            this.Controls.Add(this.btninscription);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liv\'in Paris";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btninscription;
        private System.Windows.Forms.Button btnModule;
        private System.Windows.Forms.Button btnquitter;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

