using FrmRegistrarOrdenRetiro.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Servicios.Interfaz
{
    public interface IServicio
    {
        int CrearOrden(OrdenRetiro nuevo);
        List<Materiales> TraerMateriales();
    }
}
