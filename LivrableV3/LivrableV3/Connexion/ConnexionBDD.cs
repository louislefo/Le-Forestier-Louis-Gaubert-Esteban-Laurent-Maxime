using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    /// <summary>
    /// cette classe gere la connexion a la BDD princiale,
    /// </summary>
    public class ConnexionBDD
    {
        public MySqlConnection maConnexion;
        protected string connectionString;

        public ConnexionBDD()
        {
            connectionString = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=root;PASSWORD=lolote@34F;";
            maConnexion = new MySqlConnection(connectionString);
            OuvrirConnexion();
        }

        /// <summary>
        /// ouvre la connexion a la BDD avecles parametres definis dans le constructeur
        /// elle lance une exception si la connexion echoue
        /// </summary>
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

        /// <summary>
        /// cette methode sert a fermer la connexion a la base de donnees
        /// elle verifie d'abord si la connexion est ouverte
        /// elle lance une exception si la fermeture echoue
        /// </summary>
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
