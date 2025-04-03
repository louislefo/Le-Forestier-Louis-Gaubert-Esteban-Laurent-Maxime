using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Livrable_2_psi
{
    /// classe qui gere la connexion a la bdd pour les clients
    /// pas optimise mais ca marche
    public class ConnexionBDDClient
    {
        /// la connexion mysql pour les clients
        public MySqlConnection maConnexionClient;

        /// constructeur qui prend le nom et mdp du client
        public ConnexionBDDClient(string nomClient, string motDePasse)
        {
            try
            {
                string chaineConnexionClient = "SERVER=localhost;PORT=3306;DATABASE=PSI_LoMaEs;UID=" + nomClient + ";PASSWORD=" + motDePasse;
                maConnexionClient = new MySqlConnection(chaineConnexionClient);
                maConnexionClient.Open();
                Console.WriteLine("connexion client " + nomClient + " reussie");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("oups erreur connexion client : " + e.Message);
            }
        }

        /// ferme la connexion des clients
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

        /// test pour voir si ca marche
        public void TestConnexionClient()
        {
            try
            {
                string requete = "SELECT COUNT(*) FROM client";
                MySqlCommand commande = new MySqlCommand(requete, maConnexionClient);
                int nbClients = Convert.ToInt32(commande.ExecuteScalar());
                Console.WriteLine("ya " + nbClients + " clients");
            }
            catch(MySqlException e)
            {
                Console.WriteLine("oups erreur test client : " + e.ToString());
            }
        }
    }
}
