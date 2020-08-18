/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.CustomTabs;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using TFCurier.Adapters;
using TFCurier.Entidades;
using Xamarin.Essentials;
using ActionBar = Android.App.ActionBar;

namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ServiciosPage : AppCompatActivity
    {
        private MySqlConnection conn;
        ListView servicioslist;
        List<Pedidoadap> servicios = new List<Pedidoadap>();
        List<PedidosDisponibles> servicios1 = new List<PedidosDisponibles>();
        

        public ServiciosPage()
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.ServicioLayout);
            servicioslist = FindViewById<ListView>(Resource.Id.servicioslist);
            ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(this);
            var id = preff.GetString("Usuario", "");
            var city = preff.GetString("CiudadRepartidor", "");
            

        }   

        private void GetServiciosEnCurso(ListView list )
        {
            ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(this);
            var id = preff.GetString("Usuario", "");
            var city = preff.GetString("CiudadRepartidor", "");
            string sql = string.Format("Select distinct IdPedido, NombrePlaza, NombreUsuario, LongitudUsuario, LatitudUsuario, Creada from TapFood.Pedido Where (Ciudad='{0}'and Recolectada='SI' and IdRepartidor='{1}')", city,id);
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
                servicios.Add(servicio);
            }
            reader.Close();
            list.Adapter = new ServiciosAdapter(this, servicios);
        }

        private void GetServiciosDisponibles(ListView list)
        {
            ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(this);
            var id = preff.GetString("Usuario", "");
            var city = preff.GetString("CiudadRepartidor", "");
            string sql = string.Format("Select distinct IdPedido, NombrePlaza, Creada from TapFood.Pedido Where (Ciudad='{0}'and Recolectada='NO')", city);
            MySqlCommand exe = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            reader = exe.ExecuteReader();
            while (reader.Read())
            {
                PedidosDisponibles servicio = new PedidosDisponibles();
                servicio.IdPedido = reader["IdPedido"].ToString();
                servicio.NombrePlaza = reader["NombrePlaza"].ToString();
                servicio.HoraEstimada = reader["Creada"].ToString();
                servicios1.Add(servicio);
            }
            reader.Close();
            list.Adapter = new ServiciosDisponiblesAdapter(this, servicios1);
        }
    }
}*/
