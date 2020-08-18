using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;


using Android.Support.V7.App;

using Android.Widget;
using MySql.Data.MySqlClient;
using TFCurier.Activities;

namespace TFCurier
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private MySqlConnection conn;
        EditText user, pass;
        Button login, register;
        

        public MainActivity()
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
            SetContentView(Resource.Layout.content_main);
            user = FindViewById<EditText>(Resource.Id.userlogintxt);
            pass = FindViewById<EditText>(Resource.Id.passlogintxt);
            login = FindViewById<Button>(Resource.Id.loginbtn);
            register = FindViewById<Button>(Resource.Id.registerbtn);
            

            register.Click += Register_Click;
            login.Click += Login_Click;

        }

        private void Login_Click(object sender, EventArgs e)
        {
            try
            {

                string sql1 = string.Format("Select * From TapFood.Repartidor Where (IdRepartidor, ContraseñaRepartidor) =('{0}','{1}')", user.Text, pass.Text);
                MySqlCommand loginverid = new MySqlCommand(sql1, conn);
                MySqlDataReader usr;
                usr = loginverid.ExecuteReader();
                if (usr.HasRows)
                {
                    
                    Intent pis = new Intent(this, typeof(HomePage));
                    pis.PutExtra(HomePage.USER , user.Text);

                    string jee = user.ToString();

                    
                    ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(this);
                    ISharedPreferencesEditor edit = preff.Edit();
                    edit.PutString("Usuario", user.Text.ToString());

                    edit.Apply();

                    StartActivity(pis);

                    Toast.MakeText(this, "Has ingresado!", ToastLength.Long).Show();
                    usr.Close();
                }
                else
                {
                    Toast.MakeText(this, "Tus datos son errones, revisalor por favor.", ToastLength.Long).Show();
                    usr.Close();
                }
            }
            catch
            {
                Toast.MakeText(this, "Error", ToastLength.Long).Show(); ;
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Finish();
            StartActivity(typeof(RegisterPage));
        }
    }
}
