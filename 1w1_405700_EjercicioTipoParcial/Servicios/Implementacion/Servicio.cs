using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrmRegistrarOrdenRetiro.Datos.Implementacion;
using FrmRegistrarOrdenRetiro.Datos.Interfaz;
using FrmRegistrarOrdenRetiro.Entidades;
using FrmRegistrarOrdenRetiro.Servicios.Interfaz;

namespace FrmRegistrarOrdenRetiro.Servicios.Implementacion
{
    internal class Servicio : IServicio
    {
        private IOrdenDao dao;

        public Servicio()
        { 
            dao = new OrdenDao();
        }

        public int CrearOrden(OrdenRetiro nuevo)
        {
            return dao.Crear(nuevo);
        }

        public List<Materiales> TraerMateriales()
        {
            return dao.ObtenerMateriales();
        }
    }
}
