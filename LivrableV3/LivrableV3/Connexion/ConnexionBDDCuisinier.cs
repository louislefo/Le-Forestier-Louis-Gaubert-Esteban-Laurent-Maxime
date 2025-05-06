using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    /// <summary>
    /// cette classe permet d'ouvrir et fermer la connexion avec les droits cuisinier
    /// </summary>
    public class ConnexionBDDCuisinier
    {
        public MySqlConnection maConnexionCuisinier;

        // pour crer un utilisateur pour le cuistot
        //create user if not exists 'IDCuisinier'@'localhost' identified by '123' ;
        //grant all * to 'superbozo'@'localhost';

        public ConnexionBDDCuisinier(string nomCuisinier, string motDePasse)
        {
            try
            {
                string chaineConnexionCuisinier = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=" + nomCuisinier + ";PASSWORD=" + motDePasse;
                maConnexionCuisinier = new MySqlConnection(chaineConnexionCuisinier);
                maConnexionCuisinier.Open();
                MessageBox.Show("connexion cuisinier " + nomCuisinier + " reussie");
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Erreur de connexion : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FermerConnexionCuisinier()
        {
            try
            {
                maConnexionCuisinier.Close();
                MessageBox.Show("connexion cuisinier fermee");
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Erreur de fermeture de connexion : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
