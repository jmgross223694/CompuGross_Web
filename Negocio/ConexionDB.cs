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
            string strConLan = "Server=AMD-FX-8320\\SQLEXPRESS,1433;DataBase=CompuGross;User Id=compugross;Password=compugross";

            conexion = new SqlConnection(strConLan);
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
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

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        internal void EjetutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public DataSet DSET(string consulta)
        {
            DataSet ds = new DataSet();

            if (Conectar())
            {
                try
                {
                    SqlDataAdapter SDA = new SqlDataAdapter(consulta, conexion);
                    SDA.Fill(ds, "datos");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds;
        }

        public int IUD(string consulta)
        {
            int res = -1;

            if (Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    res = 0;
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return res;
        }

        public bool Conectar()
        {
            try
            {
                string datos = "";
                SqlConnectionStringBuilder db = new SqlConnectionStringBuilder();
                db.DataSource = "AMD-FX-8320\\SQLEXPRESS,1433";
                //db.DataSource = ".\\SQLEXPRESS";
                db.InitialCatalog = "CompuGross";
                db.IntegratedSecurity = true;
                datos = db.ToString();
                conexion = new SqlConnection(datos);
                conexion.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int IUDquery(string consulta)
        {
            int res = -1;

            if (Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.ExecuteScalar();
                }
                catch (SqlException mise)
                {
                    int error = Convert.ToInt32(mise);
                }
            }

            return res;
        }
    }
}
