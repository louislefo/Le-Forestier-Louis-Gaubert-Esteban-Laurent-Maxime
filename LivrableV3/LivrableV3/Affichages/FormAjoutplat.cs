using MySql.Data.MySqlClient;
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
    public partial class FormAjoutplat : Form
    {
        private ConnexionBDDCuisinier connexionBDD;
        private Authentification authentification;
        private string nomimage;
        private FormCuisinier formCuisinier;
        public FormAjoutplat(ConnexionBDDCuisinier connexionBDD,Authentification authentification,FormCuisinier formCuisinier)
        {
            InitializeComponent();
            this.connexionBDD = connexionBDD;
            this.authentification = authentification;
            this.nomimage = "";
            this.formCuisinier = formCuisinier;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {

            try
            {
                string nomPlat = textBoxnomplat.Text;


                string typePlat = comboBoxtype.Text;

                string portions = comboBoxportion.Text;

                string dateFabricationString = dateTimePickerfabrication.Value.ToString("yyyy-MM-dd");

                string datePeremptionString = dateTimePickerPeremption.Value.ToString("yyyy-MM-dd");

                DateTime dateFabrication = dateTimePickerfabrication.Value;
                DateTime datePeremption = dateTimePickerPeremption.Value;

                if (dateFabrication < datePeremption)
                {
                    MessageBox.Show("La date de fabrication doit être antérieure à la date de péremption.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double prixParPersonne = Convert.ToDouble(textBoxprix.Text);

                string nationalite = textBoxNationalite.Text;

                string regime = comboBoxregime.Text;

                string photo = this.nomimage;

                string idPlat = "PLT" + DateTime.Now.ToString("yyyyMMddHHmmss");

                string requete = "INSERT INTO Plat_ VALUES ('" + idPlat + "', '" + authentification.idUtilisateur + "', '" +
                               nomPlat + "', '" + typePlat + "', '" + portions + "', '" +
                               dateFabricationString + "', '" +
                               datePeremptionString + "', " +
                               prixParPersonne.ToString().Replace(',', '.') + ", '" +
                               nationalite + "', '" + regime + "', '" + photo + "')";

                MySqlCommand commande = new MySqlCommand(requete, connexionBDD.maConnexionCuisinier);
                commande.CommandText = requete;
                commande.ExecuteNonQuery();
                commande.Dispose();

                MessageBox.Show("Plat ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnretour_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du plat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnimage_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Choisir une image";
                openFileDialog.Filter = "Fichiers image (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fichierSource = openFileDialog.FileName;
                    this.nomimage = fichierSource;

                    string nomFichier = Path.GetFileName(fichierSource);


                    string dossierImages = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Images"));


                    string fichierDestination = Path.Combine(dossierImages, nomFichier);

                    if (!File.Exists(fichierDestination))
                    {
                        File.Copy(fichierSource, fichierDestination);
                        MessageBox.Show("Image copiée dans : " + fichierDestination, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("L'image existe déjà dans le dossier Images.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la copie de l'image : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                string dossierImages = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Images"));
                string nomFichier = Path.GetFileName(nomimage);
                string fichierDestination = Path.Combine(dossierImages, nomFichier);
                using (var stream = new MemoryStream(File.ReadAllBytes(fichierDestination)))
                {
                    pictureBoximagepalt.Image = Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage de l'image : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void btnretour_Click(object sender, EventArgs e)
        {
            formCuisinier.Show();
            this.Close();
            
        }

        private void FormAjoutplat_Load(object sender, EventArgs e)
        {

        }
    }
}
