namespace LivrableV3
{
    partial class FormItineraireCuisinier
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
            this.pictureBoxitinerairecuisinier = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxdetail = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxitinerairecuisinier)).BeginInit();
            this.SuspendLayout();
            // 
            // btnretour
            // 
            this.btnretour.Location = new System.Drawing.Point(18, 14);
            this.btnretour.Name = "btnretour";
            this.btnretour.Size = new System.Drawing.Size(78, 44);
            this.btnretour.TabIndex = 0;
            this.btnretour.Text = "Retour";
            this.btnretour.UseVisualStyleBackColor = true;
            this.btnretour.Click += new System.EventHandler(this.btnretour_Click);
            // 
            // pictureBoxitinerairecuisinier
            // 
            this.pictureBoxitinerairecuisinier.Location = new System.Drawing.Point(18, 75);
            this.pictureBoxitinerairecuisinier.Name = "pictureBoxitinerairecuisinier";
            this.pictureBoxitinerairecuisinier.Size = new System.Drawing.Size(760, 444);
            this.pictureBoxitinerairecuisinier.TabIndex = 1;
            this.pictureBoxitinerairecuisinier.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Itineraire a suivre :";
            // 
            // textBoxdetail
            // 
            this.textBoxdetail.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxdetail.Location = new System.Drawing.Point(19, 530);
            this.textBoxdetail.Multiline = true;
            this.textBoxdetail.Name = "textBoxdetail";
            this.textBoxdetail.Size = new System.Drawing.Size(758, 219);
            this.textBoxdetail.TabIndex = 3;
            // 
            // FormItineraireCuisinier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(982, 953);
            this.Controls.Add(this.textBoxdetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxitinerairecuisinier);
            this.Controls.Add(this.btnretour);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormItineraireCuisinier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormItineraireCuisinier";
            this.Load += new System.EventHandler(this.FormItineraireCuisinier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxitinerairecuisinier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnretour;
        private System.Windows.Forms.PictureBox pictureBoxitinerairecuisinier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxdetail;
    }
}