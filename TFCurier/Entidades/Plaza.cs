using System;
namespace TFCurier.Entidades
{
    public class Plaza
    {
        public int IdPlaza { get; set; }

        public string NombrePlaza { get; set; }

        public string Ciudad { get; set; }

        public byte[] LogoPlaza { get; set; }

        public double LatitudPlaza { get; set; }

        public double LongitudPlaza { get; set; }
     }
}
