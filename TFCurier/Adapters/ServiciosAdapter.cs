using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using TFCurier.Activities;
using TFCurier.Entidades;
using Xamarin.Essentials;

namespace TFCurier.Adapters
{
    public class ServiciosAdapter : BaseAdapter<Pedidoadap>
    {
        public List<Pedidoadap> servicios;
        Activity context;


        public ServiciosAdapter(Activity context, List<Pedidoadap> servicios)
        {
            this.context = context;
            this.servicios = servicios;
        }

        public override Pedidoadap this[int position]
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
            view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ServicioEnCursoLayout, parent, false);
            var prod = servicios[position];
            var idservicio = view.FindViewById<TextView>(Resource.Id.idservicioaceptado);
            var cliente = view.FindViewById<TextView>(Resource.Id.clienteservicioaceptado);
            var origen = view.FindViewById<TextView>(Resource.Id.origenservicioaceptado);
            var destino = view.FindViewById<TextView>(Resource.Id.destinoservicioaceptado);
            var boton = view.FindViewById<Button>(Resource.Id.infoservicioaceptado);
            idservicio.Text = prod.IdPedido;
            string name = prod.NombreUsuario;
            cliente.Text = name.Split(' ')[0];
            origen.Text = prod.NombrePlaza;
            destino.Text = prod.Direccion;
            double lat = prod.LatitudUsuario;
            double lng = prod.LongitudUsuario;
            string gol = prod.Creada.ToString();
            boton.Click += delegate
            {
                //string hora = prod.Creada.ToString();
                Intent intent = new Intent(context,typeof(PedidoDatailPage));
                intent.PutExtra(PedidoDatailPage.IDPEDIDO, prod.IdPedido);
                intent.PutExtra(PedidoDatailPage.CLIENTE, prod.NombreUsuario);
                intent.PutExtra(PedidoDatailPage.LATITUDDESTINO, prod.LatitudUsuario);
                intent.PutExtra(PedidoDatailPage.LONGITUDDESTINO, prod.LongitudUsuario);
                intent.PutExtra(PedidoDatailPage.HORACREADA, prod.Creada);
                
                context.StartActivity(intent);
            };

            return view;

        }

    }
}
