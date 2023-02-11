using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TipoServicioDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<TipoServicio> Listar()
        {
            List<TipoServicio> lista = new List<TipoServicio>();

            string consulta = "select * from TiposServicio where Estado = 1 order by Descripcion desc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    TipoServicio tipoServicio = new TipoServicio();

                    tipoServicio.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    tipoServicio.Descripcion = conDB.Lector["Descripcion"].ToString();
                    tipoServicio.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    lista.Add(tipoServicio);
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

        public bool Agregar(TipoServicio tipoServicio)
        {
            bool resultado = false;
            string insert = "insert into TiposServicio(Descripcion) values('" + tipoServicio.Descripcion + "')";

            try
            {
                conDB.SetearConsulta(insert);
                conDB.EjecutarConsulta();

                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
        }

        public bool VerificarExistenciaTipoServicio_RestaurarBackup(TipoServicio tipoServicio)
        {
            bool resultado = false;
            int tipoServicioEncontrado = 0;

            string consulta = "select count(*) Cantidad from TiposServicio where Descripcion = '" + tipoServicio.Descripcion + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    tipoServicioEncontrado = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (tipoServicioEncontrado == 1)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
        }
    }
}
