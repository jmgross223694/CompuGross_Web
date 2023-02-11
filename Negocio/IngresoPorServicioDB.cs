using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class IngresoPorServicioDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<IngresoPorServicio> Listar()
        {
            List<IngresoPorServicio> listaIngresosPorServicio = new List<IngresoPorServicio>();

            TipoServicioDB tipoServicioDB = new TipoServicioDB();
            List<TipoServicio> listaTiposServicio = new List<TipoServicio>();
            listaTiposServicio = tipoServicioDB.Listar();

            foreach(TipoServicio ts in listaTiposServicio)
            {
                IngresoPorServicio ingresoPorServicio = new IngresoPorServicio();

                string consulta = "select COUNT(*) CantidadTotal, SUM(Ganancia) IngresoTotal from OrdenesTrabajo " +
                    "where IdTipoServicio = (select ID from TiposServicio where Descripcion = '" + ts.Descripcion + "')";

                try
                {
                    conDB.SetearConsulta(consulta);
                    conDB.EjecutarConsulta();
                    if (conDB.Lector.Read())
                    {
                        ingresoPorServicio.Cantidad = Convert.ToInt32(conDB.Lector["CantidadTotal"]);
                        ingresoPorServicio.TipoServicio = ts;
                        ingresoPorServicio.Ganancia = Convert.ToDecimal(Convert.ToInt32(conDB.Lector["IngresoTotal"]).ToString("N", new CultureInfo("es-AR")));
                    }

                    listaIngresosPorServicio.Add(ingresoPorServicio);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conDB.CerrarConexion();
                }
            }

            return listaIngresosPorServicio;
        }
    }
}
