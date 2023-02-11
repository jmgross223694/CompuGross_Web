using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TipoEquipoDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<TipoEquipo> Listar()
        {
            List<TipoEquipo> lista = new List<TipoEquipo>();

            string consulta = "select * from TiposEquipo where Estado = 1 order by Descripcion asc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    TipoEquipo tipoEquipo = new TipoEquipo();

                    tipoEquipo.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    tipoEquipo.Descripcion = conDB.Lector["Descripcion"].ToString();
                    tipoEquipo.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    lista.Add(tipoEquipo);
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

        public bool Agregar(TipoEquipo tipoEquipo)
        {
            bool resultado = false;
            string insert = "insert into TiposEquipo(Descripcion) values('" + tipoEquipo.Descripcion + "')";

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

        public bool VerificarExistenciaTipoEquipo_RestaurarBackup(TipoEquipo tipoEquipo)
        {
            bool resultado = false;
            int tipoEquipoEncontrado = 0;

            string consulta = "select count(*) Cantidad from TiposEquipo where Descripcion = '" + tipoEquipo.Descripcion + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    tipoEquipoEncontrado = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (tipoEquipoEncontrado == 1)
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
