using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegistrarOrdenRetiro.Datos
{
    class HelperDao
    {
        private static HelperDao instancia;
        private SqlConnection cnn;
        private HelperDao()
        {
            cnn = new SqlConnection(Properties.Resources.CadenaConexion);
        }

        public static HelperDao ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }

        public DataTable ConsultaSQL(string spNombre, List<Parametro> values)
        {
            DataTable tabla = new DataTable();

            cnn.Open();

            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (values != null)
            {
                foreach (Parametro oParametro in values)
                {
                    cmd.Parameters.AddWithValue(oParametro.Nombre, oParametro.Valor);
                }
            }
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }

        public SqlConnection obtenerConexion()
        {
            return cnn;
        }
    }
}
