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
    
    public  partial class FormAfficheritineraire : Form
    {
        private FormCommande Formcom;
        private GestionnaireItineraire<int> gestionnaireItineraire;
        public FormAfficheritineraire(FormCommande formCommande, GestionnaireItineraire<int> gestionnaireItineraire)
        {
            InitializeComponent();
            this.Formcom = formCommande;  

            using (var stream = new MemoryStream(File.ReadAllBytes("itineraire.png")))
            {
                pictureBoxitineraire.Image = Image.FromStream(stream);
            }

            textBoxrep.Text = gestionnaireItineraire.detail;


        }

        private void FormAfficheritineraire_Load(object sender, EventArgs e)
        {

        }

        private void btnretour_Click(object sender, EventArgs e)
        {
            Formcom.Show();
            this.Close();

        }
    }
}
