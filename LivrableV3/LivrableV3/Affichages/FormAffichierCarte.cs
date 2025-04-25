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
        private ConnexionBDD connexion;
        public FormAffichierCarte(FormModules formModules,Graphe<int> graphe, ConnexionBDD connexion)
        {
            InitializeComponent();
            this.formModules = formModules;
            this.graphe = graphe;
            this.connexion = connexion;
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
            
        }


        private void btnsatelite_Click(object sender, EventArgs e)
        {


            if (File.Exists("satelite.png"))
            {
                
                using (var stream = new MemoryStream(File.ReadAllBytes("satelite.png")))
                {
                    pictureBoxCarte.Image = Image.FromStream(stream);
                }
                return;
            }
            else
            {
                
                AfficherCarteOSM visMetro2 = new AfficherCarteOSM(1200, 800);
                visMetro2.DessinerGraphe(graphe);
                visMetro2.SauvegarderImage("satelite.png");

                using (var stream = new MemoryStream(File.ReadAllBytes("satelite.png")))
                {
                    pictureBoxCarte.Image = Image.FromStream(stream);
                }

            }
            

            

        }

        private void btnbasic_Click(object sender, EventArgs e)
        {
            pictureBoxCarte.Image = Image.FromFile("metro.png");
        }

        private void btnmap_Click(object sender, EventArgs e)
        {
            Formmap formMap = new Formmap(this, connexion );
            formMap.Show();
            this.Hide();
        }
    }
}
