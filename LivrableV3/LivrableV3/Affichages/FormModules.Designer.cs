using System;
using System.Drawing;
using System.Windows.Forms;

namespace LivrableV3.Affichages
{
    public partial class FormModules : Form
    {
        private System.Windows.Forms.Button btnColorationMetro;

        public FormModules()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnColorationMetro = new System.Windows.Forms.Button();

            // btnColorationMetro
            this.btnColorationMetro.Location = new System.Drawing.Point(12, 200);
            this.btnColorationMetro.Name = "btnColorationMetro";
            this.btnColorationMetro.Size = new System.Drawing.Size(128, 61);
            this.btnColorationMetro.TabIndex = 4;
            this.btnColorationMetro.Text = "Colorer le Metro";
            this.btnColorationMetro.UseVisualStyleBackColor = true;
            this.btnColorationMetro.Click += new System.EventHandler(this.btnColorationMetro_Click);

            // tabModuleGraphe
            this.tabModuleGraphe.Controls.Add(this.btnColorationMetro);
        }

        private void btnColorationMetro_Click(object sender, EventArgs e)
        {
            // Implementation of the button click event
        }
    }
} 