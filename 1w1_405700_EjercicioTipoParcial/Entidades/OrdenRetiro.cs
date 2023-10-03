using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Entidades
{
    public class OrdenRetiro
    {
        public int nroOrden { get; set; }

        public DateTime Fecha { get; set; }

        public string Responsable { get; set; }

        public List<DetalleOrden> Detalle { get; set; }

        public OrdenRetiro()
        {
            nroOrden = 0;
            Fecha = DateTime.Now;
            Responsable = string.Empty;
            Detalle = new List<DetalleOrden>();
        }

        public void AgregarDetalle(DetalleOrden detalle)
        {
            Detalle.Add(detalle);
        }
        public void QuitarDetalle(int posicion)
        {
            Detalle.RemoveAt(posicion);
        }
    }
}
