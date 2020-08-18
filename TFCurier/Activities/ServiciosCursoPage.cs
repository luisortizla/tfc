
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using TFCurier.Adapters;
using TFCurier.Entidades;
using Xamarin.Essentials;

namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ServiciosCursoPage : AppCompatActivity
    {
        private MySqlConnection conn;
        ListView servicioslist;
        TextView tiposervicios;
        List<Pedidoadap> servicios = new List<Pedidoadap>();

        public ServiciosCursoPage()
        {
            MySqlConnectionStringBuilder con = new MySqlConnectionStringBuilder();
            con.Server = "mysql-10951-0.cloudclusters.net";
            con.Port = 10951;
            con.Database = "TapFood";
            con.UserID = "curecu";
            con.Password = "curecu123";

            conn = new MySqlConnection(con.ToString());

            conn.Open();
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ServicioLayout);
            servicioslist = FindViewById<ListView>(Resource.Id.servicioslist);
            tiposervicios = FindViewById<TextView>(Resource.Id.tiposervicio);
            ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(this);
            var id = preff.GetString("Usuario", "");
            var city = preff.GetString("CiudadRepartidor", "");
            string si = city.Trim();
            string sql = string.Format("Select distinct IdPedido, NombrePlaza, NombreUsuario, LongitudUsuario, LatitudUsuario, Creada from TapFood.Pedido Where (Ciudad='{0}'and Recolectada='En proceso' and IdRepartidor='{1}')", si, id);
            Console.WriteLine(sql);
            MySqlCommand exe = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            reader = exe.ExecuteReader();
            while (reader.Read())
            {
                Pedidoadap servicio = new Pedidoadap();
                servicio.IdPedido = reader["IdPedido"].ToString();
                servicio.NombrePlaza = reader["NombrePlaza"].ToString();
                servicio.NombreUsuario = reader["NombreUsuario"].ToString();
                servicio.Creada = reader["Creada"].ToString();
                servicio.LatitudUsuario = Convert.ToDouble(reader["LatitudUsuario"].ToString());
                servicio.LongitudUsuario = Convert.ToDouble(reader["LongitudUsuario"].ToString());
                var placemarks = await Geocoding.GetPlacemarksAsync(servicio.LatitudUsuario,servicio.LongitudUsuario);
                var placemark = placemarks?.FirstOrDefault();
                var geocodeAddress = placemark.Thoroughfare + ", " + placemark.SubThoroughfare + ", " + placemark.SubLocality;
                servicio.Direccion = geocodeAddress;
                servicios.Add(servicio);
            }
            reader.Close();
            tiposervicios.Text = "Servicios en curso";
            servicioslist.Adapter = new ServiciosAdapter(this, servicios);
        }
    }
}
