using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    
    public class ConnexionBDDClient
    {
        
        public MySqlConnection maConnexionClient;



        //create user if not exists 'superbozo'@'localhost' identified by '123' ;
        //grant all on loueur.* to 'superbozo'@'localhost';
        
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
