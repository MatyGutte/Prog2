using FrmRegistrarOrdenRetiro.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Datos.Interfaz
{
    public interface IOrdenDao
    {
        int Crear(OrdenRetiro nuevo);
        List<Materiales> ObtenerMateriales();
    }
}
