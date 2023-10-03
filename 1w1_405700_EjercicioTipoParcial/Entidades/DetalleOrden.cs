using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Entidades
{
    public class DetalleOrden
    {
        public Materiales Material { get; set; }

        public int Cantidad { get; set; }

        public DetalleOrden()
        {
            Material = new Materiales();
            Cantidad = 0;
        }

        public DetalleOrden(Materiales material, int cant)
        {
            Material = material;
            Cantidad = cant;
        }
    }
}
