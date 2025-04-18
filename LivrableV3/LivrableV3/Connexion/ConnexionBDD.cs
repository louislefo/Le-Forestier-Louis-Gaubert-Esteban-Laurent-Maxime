using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    public class ConnexionBDD
    {
        public MySqlConnection maConnexion;
        protected string connectionString;

        /// constructeur de la classe connexionBDD
        public ConnexionBDD()
        {
            connectionString = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=root;PASSWORD=lolote@34F;";
            maConnexion = new MySqlConnection(connectionString);
            OuvrirConnexion();
        }

        /// ouverture de la connexion
        protected void OuvrirConnexion()
        {
            try
            {
                maConnexion.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("erreur lors de l ouverture de la connexion : " + ex.Message);
            }
        }

        /// fermeture de la connexion
        public void FermerConnexion()
        {
            try
            {
                if (maConnexion != null && maConnexion.State == System.Data.ConnectionState.Open)
                {
                    maConnexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("erreur lors de la fermeture de la connexion : " + ex.Message);
            }
        }

        
    }

   
}
