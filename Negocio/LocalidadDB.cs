using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class LocalidadDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<Localidad> ListarTodas()
        {
            List<Localidad> listaLocalidades = new List<Localidad>();

            string consulta = "select * from Localidades where Estado = 1";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    Localidad localidad = new Localidad();

                    localidad.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    localidad.Descripcion = conDB.Lector["Descripcion"].ToString();
                    localidad.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    if (localidad.Estado)
                    {
                        listaLocalidades.Add(localidad);
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

            return listaLocalidades;
        }
    }
}
