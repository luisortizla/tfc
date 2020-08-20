
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using TFCurier.Adapters;
using TFCurier.Entidades;
using AlertDialog = Android.App.AlertDialog;

namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PedidoDatailPage : AppCompatActivity
    {
        public static readonly string IDPEDIDO = "ID";
        public static readonly string CLIENTE = "CLIENTE";
        public static readonly string LATITUDDESTINO = "LATITUD";
        public static readonly string LONGITUDDESTINO = "LONGITUD";
        public static readonly string HORACREADA = "HORA";
        private MySqlConnection conn;
        TextView pedido,nombrecliente,efectivo,dinero;
        Button navegacion, finalizar, pedidorecolectado;
        ListView productoslist;
        List<DetailProductos> productos = new  List<DetailProductos>();

        public PedidoDatailPage()
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
            SetContentView(Resource.Layout.DetailServicioLayout);
            pedido = FindViewById<TextView>(Resource.Id.pedido);
            nombrecliente = FindViewById<TextView>(Resource.Id.pedidocliente);
            efectivo = FindViewById<TextView>(Resource.Id.efectivo);
            dinero = FindViewById<TextView>(Resource.Id.dinero);
            navegacion = FindViewById<Button>(Resource.Id.navegacionbtn);
            finalizar = FindViewById<Button>(Resource.Id.finalizarpedido);
            productoslist = FindViewById<ListView>(Resource.Id.listpedidos);
            pedidorecolectado = FindViewById<Button>(Resource.Id.pedidorecolectado);

            var id = Intent.GetStringExtra(IDPEDIDO);
            var cliente = Intent.GetStringExtra(CLIENTE);
            var lat = Intent.GetStringExtra(LATITUDDESTINO);
            var lng = Intent.GetStringExtra(LONGITUDDESTINO);
            var horad = Intent.GetStringExtra(HORACREADA);
            Console.WriteLine(id,cliente,lat,lng,horad);
            pedido.Text = id;
            nombrecliente.Text = cliente;

            string latt = lat;
            string longg = lng;

            string sql = string.Format("Select irrelevante, NombreProducto,NombreRestaurante,Cantidad from TapFood.Pedido where(IdPedido='{0}')", id);
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rd;
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DetailProductos producto = new DetailProductos();
                producto.irrelevante = (int)rd["irrelevante"];
                producto.NombreProducto = (string)rd["NombreProducto"];
                producto.NombreRestaurante = (string)rd["NombreRestaurante"];
                producto.Cantidad =  Convert.ToInt32(rd["Cantidad"]).ToString();
                productos.Add(producto);
            }
            rd.Close();
            productoslist.Adapter = new ProductoDetailAdapter(this,productos);
            pedidorecolectado.Click+= async delegate {
                string sql1 = string.Format("Select Creada from TapFood.Pedido where(IdPedido='{0}') limit 1", id);
                MySqlCommand cm = new MySqlCommand(sql1, conn);
                MySqlDataReader re;
                re = cm.ExecuteReader();
                re.Read();
                string dato = re["Creada"].ToString() ;
                string format = "MM/dd/yyyy hh:mm:ss";
                bool result = DateTime.TryParseExact(dato, format, null, DateTimeStyles.None, out DateTime dt);
                if (result)
                {
                    re.Close();
                    for (int i = 0; i < productos.Count; i++)
                    {
                        var location = await Geolocation.GetLastKnownLocationAsync();
                        string sql2 = string.Format("UPDATE `TapFood`.`Pedido` SET `LongitudRepartidor` = '{0}', `LatitudRepartidor` = '{1}', `Recolectada` = '{2}' WHERE (`irrelevante` = '{3}';",location.Longitude,location.Latitude,DateTime.Now,productos.ElementAt(i).irrelevante);
                        MySqlCommand insert = new MySqlCommand(sql2, conn);
                        insert.ExecuteNonQuery();
                    }
                }
                else
                {
                    re.Close();
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("El resturant aun no ha confirmado la entrega, ¿estas seguro que ya recolectaste el pedido?");
                    builder.SetCancelable(false)
                        .SetPositiveButton("Seguro", async (c, ev) =>
                        {
                            for (int i = 0; i < productos.Count; i++)
                            {
                                var location = await Geolocation.GetLastKnownLocationAsync();
                                string sql2 = string.Format("UPDATE `TapFood`.`Pedido` SET `LongitudRepartidor` = '{0}', `LatitudRepartidor` = '{1}', `Recolectada` = '{2}' WHERE (`irrelevante` = '{3}')", location.Longitude, location.Latitude, DateTime.Now, productos.ElementAt(i).irrelevante);
                                Console.WriteLine(sql2);
                                MySqlCommand insert = new MySqlCommand(sql2, conn);
                                insert.ExecuteNonQuery();
                            }
                        })
                    .SetNegativeButton("Cancelar", (c, ev) =>
                    {
                        builder.Dispose();
                    });
                    AlertDialog lala = builder.Create();
                    lala.Show();
                }
	};
            navegacion.Click+=delegate {

                Intent intent = new Intent(this, typeof(MapaNavegacionPage));
                //intent.PutExtra(MapaNavegacionPage.LATUSR, latt);
                //intent.PutExtra(MapaNavegacionPage.LONGUSR, longg);
                StartActivity(intent);

	};
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
