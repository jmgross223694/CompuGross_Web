using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoUsuarioDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<TipoUsuario> Listar()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            string consulta = "select * from TiposUsuario";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    TipoUsuario tipoUsuario = new TipoUsuario();

                    tipoUsuario.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    tipoUsuario.Descripcion = conDB.Lector["Tipo"].ToString();

                    lista.Add(tipoUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return lista;
        }
    }
}
