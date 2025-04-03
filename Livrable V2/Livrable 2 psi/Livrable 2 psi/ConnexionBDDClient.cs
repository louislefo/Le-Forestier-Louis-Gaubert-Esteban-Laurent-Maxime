using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    
    public class ConnexionBDDClient
    {
        
        public MySqlConnection maConnexionClient;



        //create user if not exists 'superbozo'@'localhost' identified by '123' ;
        //grant all on loueur.* to 'superbozo'@'localhost';
        
        public ConnexionBDDClient(string IDClient, string motDePasse)
        {
            try
            {
                string chaineConnexionClient = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=" + IDClient + ";PASSWORD=" + motDePasse;
                maConnexionClient = new MySqlConnection(chaineConnexionClient);
                maConnexionClient.Open();
                Console.WriteLine("connexion client " + IDClient + " reussie");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("oups erreur connexion client : " + e.Message);
            }
        }

        
        public void FermerConnexionClient()
        {
            try
            {
                maConnexionClient.Close();
                Console.WriteLine("connexion client fermee");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("probleme fermeture client : " + e.Message);
            }
        }

        
    }
}
