using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using TFCurier.Entidades;

namespace TFCurier.Adapters
{
    public class ServiciosDisponiblesAdapter:BaseAdapter<PedidosDisponibles>
    {
        public List<PedidosDisponibles> servicios;
        Activity context;
        private MySqlConnection conn;
        

        public ServiciosDisponiblesAdapter(Activity context, List<PedidosDisponibles> servicios)
        {
            this.context = context;
            this.servicios = servicios;
            MySqlConnectionStringBuilder con = new MySqlConnectionStringBuilder();
            con.Server = "mysql-10951-0.cloudclusters.net";
            con.Port = 10951;
            con.Database = "TapFood";
            con.UserID = "curecu";
            con.Password = "curecu123";

            conn = new MySqlConnection(con.ToString());

            conn.Open();
        }

        public override PedidosDisponibles this[int position]
        {
            get { return servicios[position]; }
        }

        public override int Count
        {
            get { return servicios.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ServicioDisponibleLayout, parent, false);

            var prod = servicios[position];
            var serviciodisponible = view.FindViewById<TextView>(Resource.Id.seriviciodisponible);
            var lugarrecolectad = view.FindViewById<TextView>(Resource.Id.sitiorecolectadisponible);
            var hestimada = view.FindViewById<TextView>(Resource.Id.horaestimadaserviciodisponible);
            var boton = view.FindViewById<Button>(Resource.Id.aceptarservicio);
            serviciodisponible.Text = prod.IdPedido;
            lugarrecolectad.Text = prod.NombrePlaza;
            string hora = prod.HoraEstimada;
            DateTime hijole = Convert.ToDateTime(hora);
            int x = 45;
            DateTime puede = hijole.AddMinutes(x);
            var se = puede.ToString("hh:mm");
            hestimada.Text = se;

            boton.Click+=delegate {

                try
                {
                    ISharedPreferences preff = PreferenceManager.GetDefaultSharedPreferences(context);
                    var id = preff.GetString("Usuario", "");
                    var name = preff.GetString("NombreRepartidor", "");
                    var phone = preff.GetString("TelefonoRepartidor", "");
                    string sql3 = string.Format("Select irrelevante from TapFood.Pedido where (IdPedido = '{0}' and Recolectada ='NO')", prod.IdPedido);
                    Console.WriteLine( sql3);
                    MySqlCommand cty3 = new MySqlCommand(sql3, conn);
                    MySqlDataReader plz3;
                    plz3 = cty3.ExecuteReader();
                    List<int> irr = new List<int>();
                    while (plz3.Read())
                    {
                        int y;
                        y = (int)plz3["irrelevante"];
                        irr.Add(y);
                        
                    }
                    plz3.Close();
                    Console.WriteLine(irr);
                    for (int i=0; i<irr.Count;i++)
                    {
                        
                        //int please = (int)plz3["Irrelevante"];
                        string rec = "En proceso";
                        string sql4 = string.Format("UPDATE `TapFood`.`Pedido` SET `IdRepartidor` = '{0}', `NombreRepartidor` = '{1}', `TelefonoRepartidor` = '{2}', `Recolectada`='{3}' WHERE (`irrelevante` = '{4}')", id, name, phone,rec, irr.ElementAt(i).ToString()); ;
                        Console.WriteLine(sql4);
                        MySqlCommand cmd = new MySqlCommand(sql4, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                {
                    Toast.MakeText(context, "El servicio ya no esta disponible", ToastLength.Long).Show();
                }
	};

            return view;
        }
    }
}
