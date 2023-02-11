using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UnidadOpticaDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<UnidadOptica> Listar()
        {
            List<UnidadOptica> lista = new List<UnidadOptica>();
            string consulta = "select * from UnidadesOpticas where Estado = 1";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    UnidadOptica unidadOptica = new UnidadOptica();

                    unidadOptica.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    unidadOptica.Descripcion = conDB.Lector["Descripcion"].ToString();
                    unidadOptica.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    if (unidadOptica.Estado)
                    {
                        lista.Add(unidadOptica);
                    }
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
