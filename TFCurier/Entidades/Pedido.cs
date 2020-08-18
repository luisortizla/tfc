using System;
namespace TFCurier.Entidades
{
    public class Pedido
    {
        public int irrelevante { get; set; }

        public string IdPedido { get; set; }

        public string NombrePlaza { get; set; }

        public string Ciudad { get; set; }

        public string IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string IdRestaurante { get; set; }

        public string NombreRestaurante { get; set; }

        public string IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public float PrecioProducto { get; set; }

        public int Cantidad { get; set; }

        public string IdRepartidor { get; set; }

        public string NombreRepartidor { get; set; }

        public double TelefonoRepartidor { get; set; }

        public string TipoDePago { get; set; }

        public double LongitudPlaza { get; set; }

        public double LatitudPlaza { get; set; }

        public double LongitudUsuario { get; set; }

        public double LatitudUsuario { get; set; }

        public double LongitudRepartidor { get; set; }

        public double LatitudRepartidor { get; set; }

        public string Creada { get; set; }

        public string Confirmada { get; set; }

        public string Recolectada { get; set; }

        public string Entregada { get; set; }

    }
}
