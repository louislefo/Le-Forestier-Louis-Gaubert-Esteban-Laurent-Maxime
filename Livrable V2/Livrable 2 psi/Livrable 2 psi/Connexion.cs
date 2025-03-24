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
    public class Connexion
    {
        /// <summary>
        /// la connexion mysql
        /// </summary>
        public MySqlConnection MaConnection;

        /// <summary>
        /// constructeur qui initialise la connexion
        /// </summary>
        public Connexion()
        {
            try
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=psi;UID=root;PASSWORD=lolote@34F";
                MaConnection = new MySqlConnection(connectionString);
                MaConnection.Open();
                Console.WriteLine("connexion ouverte");
            }
            catch(MySqlException e) 
            {
                Console.WriteLine("erreur connexion : " + e.ToString());
            }
        }

        /// <summary>
        /// ferme la connexion
        /// </summary>
        public void FermerConnexion()
        {
            try
            {
                MaConnection.Close();
                Console.WriteLine("connexion fermee");
            }
            catch(MySqlException e)
            {
                Console.WriteLine("erreur fermeture : " + e.ToString());
            }
        }

        /// <summary>
        /// test simple pour verifier la connexion
        /// </summary>
        public void TestConnexion()
        {
            try
            {
                string requete = "SELECT COUNT(*) FROM clients";
                MySqlCommand cmd = new MySqlCommand(requete, MaConnection);
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
