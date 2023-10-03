using FrmRegistrarOrdenRetiro.Datos.Interfaz;
using FrmRegistrarOrdenRetiro.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Datos.Implementacion
{
    public class OrdenDao : IOrdenDao
    {
        public OrdenDao() 
        { 

        }

        public List<Materiales> ObtenerMateriales()
        {
            List<Materiales> lMateriales = new List<Materiales>();
            DataTable dt = HelperDao.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_MATERIALES", null);

            foreach (DataRow fila in dt.Rows) 
            {
                int id = int.Parse(fila["codigo"].ToString());
                string nom = fila["nombre"].ToString();
                double sck = double.Parse(fila["stock"].ToString());
                Materiales m = new Materiales(id, nom, sck);
                lMateriales.Add(m);
            }
            return lMateriales;
        }

        public int Crear(OrdenRetiro nuevo)
        {
            int resultado = 0;
            SqlConnection cnn = HelperDao.ObtenerInstancia().obtenerConexion();
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INSERTAR_ORDEN";
                cmd.Parameters.AddWithValue("@responsable", nuevo.Responsable);

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@nro";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parametro);
                resultado = cmd.ExecuteNonQuery();

                int nro = (int)parametro.Value;
                int detalleNro = 1;
                SqlCommand cmdDetalle;

                foreach(DetalleOrden detO in nuevo.Detalle)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_orden", nro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@codigo", detO.Material.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detO.Cantidad);
                    int nresu = cmdDetalle.ExecuteNonQuery();
                    resultado = nresu + resultado;
                    detalleNro++;
                }
                t.Commit();
            cnn.Close();
            }
            catch
            {
                if (t != null)
                    t.Rollback();
                resultado = 0;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }

            return resultado;
        }
    }
}
