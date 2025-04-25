using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;

namespace LivrableV3
{
    public partial class Formmap : Form
    {
        private FormAffichierCarte formModule;
        private ConnexionBDD connexion;
        private ChargerFichiers chargeurFichiers;
        private Dictionary<PointLatLng, string> infoStations;
        private GMapOverlay marqueurs;
        private Dictionary<string, int> nbLignesParStation;

        public Formmap(FormAffichierCarte formModule, ConnexionBDD connexionBDD)
        {
            InitializeComponent();
            this.formModule = formModule;
            this.connexion = connexionBDD;
            this.chargeurFichiers = new ChargerFichiers();
            this.infoStations = new Dictionary<PointLatLng, string>();
            this.nbLignesParStation = new Dictionary<string, int>();
        }

        private string ChargerInfoStation(string nomStation)
        {
            return "Station : " + nomStation + "\n\nInfos chargées à la demande."; // Chargement allégé pour performance
        }

        private void map()
        {
            try
            {
                GMaps.Instance.Mode = AccessMode.ServerAndCache;
                gMapControlmap.MapProvider = GMapProviders.OpenStreetMap;
                gMapControlmap.Position = new PointLatLng(48.8566, 2.3522);
                gMapControlmap.MinZoom = 5;
                gMapControlmap.MaxZoom = 20;
                gMapControlmap.Zoom = 12;
                gMapControlmap.ShowCenter = false;
                gMapControlmap.DragButton = MouseButtons.Left;
                gMapControlmap.CanDragMap = true;
                gMapControlmap.MarkersEnabled = true;

                Dictionary<int, Noeud<int>> stations = chargeurFichiers.ChargerNoeudsMetro("../../Données/MetroParisNoeuds.csv");
                marqueurs = new GMapOverlay("stations");

                foreach (var station in stations.Values)
                {
                    PointLatLng point = new PointLatLng(station.Latitude, station.Longitude);
                    GMarkerGoogle marqueur = new GMarkerGoogle(point, GMarkerGoogleType.red);

                    // Ne charge les infos que sur clic pour accélerer le rendu initial
                    infoStations[point] = station.NomStation;

                    marqueur.ToolTipText = station.NomStation;
                    marqueur.ToolTip.Fill = Brushes.White;
                    marqueur.ToolTip.Foreground = Brushes.Black;
                    marqueur.ToolTip.Stroke = Pens.Black;
                    marqueur.ToolTip.TextPadding = new Size(10, 10);

                    if (!nbLignesParStation.ContainsKey(station.NomStation))
                        nbLignesParStation[station.NomStation] = 1;
                    else
                        nbLignesParStation[station.NomStation]++;

                    if (nbLignesParStation[station.NomStation] > 1)
                    {
                        marqueur.Size = new Size(10, 10);
                    }

                    marqueurs.Markers.Add(marqueur);
                }

                gMapControlmap.Overlays.Add(marqueurs);
                gMapControlmap.OnMarkerClick += new MarkerClick(gMapControlmap_OnMarkerClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de la carte : " + ex.Message);
            }
        }

        private void gMapControlmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            PointLatLng point = item.Position;

            if (infoStations.ContainsKey(point))
            {
                string nomStation = infoStations[point];
                string info = ChargerInfoStation(nomStation); // Chargement dynamique à la demande
                MessageBox.Show(info, "Informations sur la station",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Formmap_Load(object sender, EventArgs e)
        {
            map();
        }

        private void gMapControlmap_Load(object sender, EventArgs e)
        {
            // config déjà faite dans map()
        }

        private void btnretour_Click(object sender, EventArgs e)
        {
            formModule.Show();
            this.Close();
        }
    }
}
