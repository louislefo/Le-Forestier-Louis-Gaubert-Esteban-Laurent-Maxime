using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormAffichierCarte : Form
    {
        private FormModules formModules;
        public FormAffichierCarte(FormModules formModules)
        {
            InitializeComponent();
            this.formModules = formModules; 
        }


        private void btnRetour_Click(object sender, EventArgs e)
        {
            this.Close();
            formModules.Show();
        }

        private void FormAffichierCarte_Load(object sender, EventArgs e)
        {

        }

        public void pictureBoxCarte_Click(object sender, EventArgs e)
        {
            pictureBoxCarte.Image = Image.FromFile("metro.png");
        }
    }
}
