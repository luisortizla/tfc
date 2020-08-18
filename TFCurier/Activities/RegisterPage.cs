
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using AlertDialog = Android.App.AlertDialog;

namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class RegisterPage : AppCompatActivity
    {
        private MySqlConnection conn;
        Spinner cityspn, bancospn;
        Button finalizar;
        string ciudad, banco, rep;
        bool resultado;
        EditText curiername, emailcurier, contraseñacurier, celcurier, vehiculocurier, descripcionvehiculocurier, cuentabancocurier;
        

        public RegisterPage()
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
            //SetContentView(Resource.Layout.RegisterLayout);
            SetContentView(Resource.Layout.RegisterLayout);

            curiername = FindViewById<EditText>(Resource.Id.curiername);
            emailcurier = FindViewById<EditText>(Resource.Id.emailcurier);
            contraseñacurier = FindViewById<EditText>(Resource.Id.cotraseñacurier);
            celcurier = FindViewById<EditText>(Resource.Id.celcurier);
            vehiculocurier = FindViewById<EditText>(Resource.Id.vehiculocurier);
            descripcionvehiculocurier = FindViewById<EditText>(Resource.Id.descripcionvehiculocurier);
            cuentabancocurier = FindViewById<EditText>(Resource.Id.cuentabancocurier);
             bancospn = FindViewById<Spinner>(Resource.Id.bancospn);
             cityspn = FindViewById<Spinner>(Resource.Id.cityspn);
            finalizar = FindViewById<Button>(Resource.Id.nextbtn);

            finalizar.Click += Finalizar_Click;

            var spinnerbanco = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.Banco, Android.Resource.Layout.SimpleSpinnerItem);
            spinnerbanco.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            bancospn.Adapter = spinnerbanco;

            string sql = string.Format("SELECT NombrePlaza, Ciudad FROM TapFood.Plaza");
            MySqlCommand cty = new MySqlCommand(sql, conn);
            MySqlDataReader plz;
            plz = cty.ExecuteReader();

            var cities = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);

            while (plz.Read())
            {
                cities.Add(" " + plz["Ciudad"].ToString() + "");
            }
            cityspn.Adapter = cities;

            plz.Close();
        }

        private void Finalizar_Click(object sender, EventArgs e)
        {

            ciudad = cityspn.SelectedItem.ToString();
            banco = bancospn.SelectedItem.ToString();

            Random rnd = new Random();
            double r = rnd.Next(10000, 99999);
            string text = new string("TFCP");
            string IdRepartidor = text + r;

            try
            {
                rep = new string(IdRepartidor);
                string sql = string.Format("INSERT INTO `TapFood`.`Repartidor` (`IdRepartidor`, `NombreRepartidor`, `EmailRepartidor`, `ContraseñaRepartidor`, `CiudadRepartidor`, `TelefonoRepartidor`, `TipoDeVehiculo`, `DescripcionVehiculo`, `CuentaDepositoRepartidor`, `Banco`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                                                                                                                         rep, curiername.Text, emailcurier.Text,  contraseñacurier.Text, ciudad, celcurier.Text, vehiculocurier.Text, descripcionvehiculocurier.Text, cuentabancocurier.Text , banco);
                Console.WriteLine(sql);
                MySqlCommand logincmdregister = new MySqlCommand(sql, conn);
                logincmdregister.ExecuteNonQuery();
                resultado = true;

            }
            catch
            {
                Toast.MakeText(this, "Por favor verifica tus datos y vuelve a intentarlo.", ToastLength.Long).Show();
                resultado = false;
            }
            if (resultado == true)
            {

                //CUADRO DE DIALOGO CON EL USUARIO CREADO Y LA CONTRASEñA INGRESADA
                AlertDialog.Builder informacion = new AlertDialog.Builder(this);
                AlertDialog create = informacion.Create();
                create.SetTitle("Datos de acceso:");
                create.SetMessage("Usuario: " + rep +
                    "\nContraseña: " + contraseñacurier.Text);
                create.SetButton("Ok", (create, EventArgs) =>
                {
                    StartActivity(typeof(MainActivity));
                });
                create.Show();


            }
        }
    }
}
