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
    public partial class FormItineraireCuisinier : Form
    {

        FormCuisinier formCuisinier;
        GestionnaireItineraire<int> gestionnaireItineraire;
        public FormItineraireCuisinier(FormCuisinier formCuisinier,GestionnaireItineraire<int> gestionnaire)
        {
            InitializeComponent();
            this.formCuisinier = formCuisinier;
            this.gestionnaireItineraire = gestionnaire;

            textBoxdetail.Text = gestionnaireItineraire.detail;

            using (var stream = new MemoryStream(File.ReadAllBytes("itinerairecuisinier.png")))
            {
                pictureBoxitinerairecuisinier.Image = Image.FromStream(stream);
            }



        }

        private void btnretour_Click(object sender, EventArgs e)
        {
            this.Close();
            formCuisinier.Show();
        }

        private void FormItineraireCuisinier_Load(object sender, EventArgs e)
        {

        }
    }
}
