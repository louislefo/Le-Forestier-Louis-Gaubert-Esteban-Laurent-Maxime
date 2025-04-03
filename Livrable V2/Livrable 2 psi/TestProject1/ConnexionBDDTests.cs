using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livrable_2_psi;
using MySql.Data.MySqlClient;

namespace TestProject1
{
    public class ConnexionBDDTests
    {
        [Fact]
        public void Constructeur_ConnexionReussie()
        {
            ConnexionBDD connexion = new ConnexionBDD();

            Assert.NotNull(connexion.maConnexion);
            Assert.Equal(System.Data.ConnectionState.Open, connexion.maConnexion.State);
        }

        [Fact]
        public void FermerConnexion_FermeCorrectement()
        {
            ConnexionBDD connexion = new ConnexionBDD();
            
            connexion.FermerConnexion();

            Assert.Equal(System.Data.ConnectionState.Closed, connexion.maConnexion.State);
        }

        [Fact]
        public void Constructeur_MauvaiseConnexion()
        {
            var exception = Assert.Throws<MySqlException>(() =>
            {
                string chaineConnexion = "SERVER=localhost;PORT=3306;DATABASE=BDDInexistante;UID=root;PASSWORD=mauvaismdp";
                MySqlConnection connexion = new MySqlConnection(chaineConnexion);
                connexion.Open();
            });

            Assert.NotNull(exception);
        }

        [Fact]
        public void FermerConnexion_DejaFermee()
        {
            ConnexionBDD connexion = new ConnexionBDD();
            connexion.FermerConnexion();
            
            connexion.FermerConnexion();

            Assert.Equal(System.Data.ConnectionState.Closed, connexion.maConnexion.State);
        }
    }
} 