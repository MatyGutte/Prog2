using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Entidades
{
    public class Materiales
    {
        public int Codigo { get; set; }

        public string Nombre { get; set; }

        public double Stock { get; set; }

        public Materiales() 
        { 
            Codigo = 0;
            Nombre = string.Empty;
            Stock = 0;
        }

        public Materiales(int codigo, string nombre, double stock)
        {
            Codigo = codigo;
            Nombre = nombre;
            Stock = stock;
        }

        public override string ToString()
        {
            return Codigo + " - " + Nombre;
        }
    }
}
