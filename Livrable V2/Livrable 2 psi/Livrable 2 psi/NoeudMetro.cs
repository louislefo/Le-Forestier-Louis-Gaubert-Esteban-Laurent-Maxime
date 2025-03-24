namespace Livrable_2_psi
{
    /// <summary>
    /// classe qui represente une station de metro
    /// </summary>
    public class NoeudMetro : Noeud<int>
    {
        /// <summary>
        /// recupere ou modifie le nom de la station
        /// </summary>
        public string NomStation { get; set; }

        /// <summary>
        /// recupere ou modifie la longitude de la station
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// recupere ou modifie la latitude de la station
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// recupere ou modifie le numero de la ligne
        /// </summary>
        public string NumeroLigne { get; set; }

        /// <summary>
        /// cree un nouveau noeud metro
        /// </summary>
        public NoeudMetro(int id, string nomStation, double longitude, double latitude, string numeroLigne) 
            : base(id)
        {
            NomStation = nomStation;
            Longitude = longitude;
            Latitude = latitude;
            NumeroLigne = numeroLigne;
        }
    }
} 