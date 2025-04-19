using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LivrableV3
{
    public partial class FormAffichierCarte : Form
    {
        private FormModules formModules;
        private Graphe<int> graphe;
        public FormAffichierCarte(FormModules formModules,Graphe<int> graphe)
        {
            InitializeComponent();
            this.formModules = formModules;
            this.graphe = graphe;
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


        private void btnsatelite_Click(object sender, EventArgs e)
        {
            int largeur = pictureBoxCarte.Width;
            int hauteur = pictureBoxCarte.Height;
            AfficherCarteOSM visMetro2 = new AfficherCarteOSM(largeur, hauteur);
            visMetro2.DessinerGraphe(graphe);
            visMetro2.SauvegarderImage("satelite.png");

            using (var stream = new MemoryStream(File.ReadAllBytes("satelite.png")))
            {
                pictureBoxCarte.Image = Image.FromStream(stream);
            }

        }

        private void btnbasic_Click(object sender, EventArgs e)
        {
            pictureBoxCarte.Image = Image.FromFile("metro.png");
        }
    }
}
