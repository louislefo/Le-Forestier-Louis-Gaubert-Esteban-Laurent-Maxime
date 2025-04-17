using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_PSI
{
    public class ConnexionBDDCuisinier
    {
        public MySqlConnection maConnexionCuisinier;

        // pour crer un utilisateur pour le cuistot
        //create user if not exists 'IDCuisinier'@'localhost' identified by '123' ;
        //grant all * to 'superbozo'@'localhost';

        public ConnexionBDDCuisinier(string IDCuisinier, string motDePasse)
        {
            try
            {
                string chaineConnexionCuisinier = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=" + IDCuisinier + ";PASSWORD=" + motDePasse;
                maConnexionCuisinier = new MySqlConnection(chaineConnexionCuisinier);
                maConnexionCuisinier.Open();
                Console.WriteLine("connexion cuisinier " + IDCuisinier + " reussie");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur connexion cuisinier : " + e.Message);
            }
        }

        public void FermerConnexionCuisinier()
        {
            try
            {
                maConnexionCuisinier.Close();
                Console.WriteLine("connexion cuisinier ferme");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("probleme fermeture cuisinier : " + e.Message);
            }
        }


    }
}
