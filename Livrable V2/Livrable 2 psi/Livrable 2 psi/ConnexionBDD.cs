using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui gere la connexion a la bdd
    /// </summary>
    public class ConnexionBDD
    {
        /// <summary>
        /// la connexion mysql
        /// </summary>
        public MySqlConnection maConnexion;

        /// <summary>
        /// constructeur par defaut
        /// </summary>
        public ConnexionBDD()
        {
            try
            {
                string chaineConnexion = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=root;PASSWORD=lolote@34F";
                maConnexion = new MySqlConnection(chaineConnexion);
                maConnexion.Open();
                Console.WriteLine(" ## connexion reussie");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur de connexion : " + e.Message);
                throw e;
            }
        }

        /// <summary>
        /// ferme la connexion
        /// </summary>
        public void FermerConnexion()
        {
            try
            {
                maConnexion.Close();
                Console.WriteLine("connexion fermee");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("erreur fermeture : " + e.Message);
            }
        }

        /// <summary>
        /// test simple pour verifier la connexion
        /// </summary>
        public void TestConnexion()
        {
            try
            {
                string requete = "SELECT COUNT(*) FROM client";
                MySqlCommand cmd = new MySqlCommand(requete, maConnexion);
                int nbClients = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine("nombre de clients : " + nbClients);
            }
            catch(MySqlException e)
            {
                Console.WriteLine("erreur test : " + e.ToString());
            }
        }
    }
}
