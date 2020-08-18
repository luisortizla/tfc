using System;
namespace TFCurier.Entidades
{
    public class Cuenta
    {
        public string IdPedido { get; set; }

        public float VentaTotal { get; set; }

        public float CostoServicio { get; set; }

        public float Propina { get; set; }

        public float Descuento { get; set; }

        public float TotalAPagar { get; set; }

    }
}
