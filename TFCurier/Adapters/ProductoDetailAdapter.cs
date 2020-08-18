using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using TFCurier.Entidades;

namespace TFCurier.Adapters
{
    public class ProductoDetailAdapter:BaseAdapter<DetailProductos>
    {
        public List<DetailProductos> productos;

        Activity context;

        public ProductoDetailAdapter(Activity context, List<DetailProductos> productos)
        {
            this.context = context;

            this.productos = productos;
        }

        public override DetailProductos this[int position]
        {
            get { return productos[position]; }
        }

        public override int Count
        {
            get { return productos.Count; } 
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterServiciosDetailLayout, parent, false);

            var prod = productos[position];

            var producto = view.FindViewById<TextView>(Resource.Id.producto);

            var restaurante = view.FindViewById<TextView>(Resource.Id.restaurante);

            var cantidad = view.FindViewById<TextView>(Resource.Id.cantidadproducto);

            producto.Text = prod.NombreProducto;

            restaurante.Text = prod.NombreRestaurante;

            cantidad.Text = prod.Cantidad;

            return view;
        }
    }
}
