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
    /// cette classe gere la connexion a la BDD client
    /// </summary>
    public class ConnexionBDDClient
    {
        
        public MySqlConnection maConnexionClient;
        
        public ConnexionBDDClient(string nomClient, string motDePasse)
        {
            try
            {
                string chaineConnexionClient = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=" + nomClient + ";PASSWORD=" + motDePasse;
                maConnexionClient = new MySqlConnection(chaineConnexionClient);
                maConnexionClient.Open();
                MessageBox.Show("connexion client " + nomClient + " reussie");
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Erreur de connexion : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// <summary>
        /// cette methode sert a fermer la connexion du client
        /// elle affiche un message de confirmation ou d'erreur
        /// </summary>
        public void FermerConnexionClient()
        {
            try
            {
                maConnexionClient.Close();
                MessageBox.Show("connexion client fermee");
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Erreur de fermeture de connexion : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       


    }
}
