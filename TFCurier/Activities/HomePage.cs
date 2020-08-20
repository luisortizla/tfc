
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;

using Android.Support.V7.App;

using Android.Widget;
using MySql.Data.MySqlClient;

namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class HomePage : AppCompatActivity
    {
        
        TextView curiername;
        Button feedback, servicios, historial;
        private MySqlConnection conn;
        Random rnd = new Random();

        public HomePage()
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
            SetContentView(Resource.Layout.HomeLayout);
            curiername = FindViewById<TextView>(Resource.Id.namecurierhp);
            feedback = FindViewById<Button>(Resource.Id.feedbakcbtn);
            servicios = FindViewById<Button>(Resource.Id.serviciosbtn);
            historial = FindViewById<Button>(Resource.Id.historialbtn);

            ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(this);
            var jee = preff.GetString("Usuario", "");

            string sql = string.Format("Select NombreRepartidor, CiudadRepartidor,TelefonoRepartidor from TapFood.Repartidor where(IdRepartidor = '{0}')", jee.ToString());
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            string nombre = (string)reader["NombreRepartidor"];
            string ciudad = (string)reader["CiudadRepartidor"];
            double phone = (double)reader["TelefonoRepartidor"];
            string num = phone.ToString();
            ISharedPreferences preff2 = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor edit2 = preff2.Edit();
            edit2.PutString("NombreRepartidor", nombre);
            edit2.PutString("CiudadRepartidor", ciudad);
            edit2.PutString("TelefonoRepartidor", num);
            edit2.Apply();
            int count = nombre.Split(' ').Length;
            if (count == 1)
            {
                curiername.Text = "Hola " + nombre + ",";
            }
            else
            {
                curiername.Text = "Hola " + nombre.Split(' ')[0] + ",";
            }

            feedback.Click += Feedback_Click;
            servicios.Click += Servicios_Click;
            historial.Click += Historial_Click;
        }

        private void Historial_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Servicios_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(TipoServicioPage));
        }

        private void Feedback_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
