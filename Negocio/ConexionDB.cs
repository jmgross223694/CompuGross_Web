using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class ConexionDB
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public ConexionDB()
        {
            //string strConLocal = "data source=.\\SQLEXPRESS; initial catalog=CompuGross; integrated security=sspi";
            string strConLan = "Server=AMD-FX-8320\\SQLSERVER,1433;DataBase=CompuGross;User Id=compugross;Password=compugross";

            conexion = new SqlConnection(strConLan);
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void EjecutarConsulta()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public DataTable CrearDataTable(string consulta)
        {
            comando.Connection = conexion;
            conexion.Open();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(consulta, conexion);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public SqlDataReader Lector
        {
            get { return lector; }
        }
    }
}
