using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class PrecioDB
    {
        ConexionDB conDB = new ConexionDB();

        public List<Precio> Listar()
        {
            List<Precio> lista = new List<Precio>();

            string consulta = "select * from ListaPrecios where Estado = 1 order by Codigo asc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    Precio precio = new Precio();

                    precio.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    precio.Codigo = conDB.Lector["Codigo"].ToString();
                    precio.Descripcion = conDB.Lector["Descripcion"].ToString();
                    precio.Aclaraciones = conDB.Lector["Aclaraciones"].ToString();
                    precio.Dolares = Convert.ToDecimal(Convert.ToDouble(Math.Truncate((decimal)conDB.Lector["Precio_Dolares"] * 100) / 100));
                    
                    precio.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    lista.Add(precio);
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

        public Precio BuscarPorID(long ID)
        {
            Precio precio = new Precio();

            string consulta = "select * from ListaPrecios where Estado = 1 and ID = " + ID;

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    precio.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    precio.Codigo = conDB.Lector["Codigo"].ToString();
                    precio.Descripcion = conDB.Lector["Descripcion"].ToString();
                    precio.Aclaraciones = conDB.Lector["Aclaraciones"].ToString();
                    precio.Dolares = Convert.ToDecimal(Convert.ToDouble(Math.Truncate((decimal)conDB.Lector["Precio_Dolares"] * 100) / 100));

                    precio.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);
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

            return precio;
        }

        public bool Agregar(Precio precio)
        {
            bool resultado = false;
            string auxPrecioDolar = precio.Dolares.ToString().Replace(",", ".");
            string insert = "insert into ListaPrecios(Codigo, Descripcion, Aclaraciones, Precio_Dolares) " +
                            "values('" + precio.Codigo + "', '" + precio.Descripcion + "', '" + precio.Aclaraciones + "', " + auxPrecioDolar + ")";

            if (precio.Aclaraciones == "")
            {
                insert = "insert into ListaPrecios(Codigo, Descripcion, Precio_Dolares) " +
                            "values('" + precio.Codigo + "', '" + precio.Descripcion + "', " + auxPrecioDolar + ")";
            }

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

        public bool Modificar(Precio precio)
        {
            bool resultado = false;
            string update = "update ListaPrecios set Codigo = '" + precio.Codigo + "', " +
                                                    "Descripcion = '" + precio.Descripcion + "', " +
                                                    "Aclaraciones = '" + precio.Aclaraciones + "', " +
                                                    "Precio_Dolares = " + precio.Dolares +
                                                    " where ID = " + precio.ID;

            try
            {
                conDB.SetearConsulta(update);
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

        public bool Eliminar(Precio precio)
        {
            bool resultado = false;
            string delete = "delete from ListaPrecios where ID = " + precio.ID;

            try
            {
                conDB.SetearConsulta(delete);
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

        public bool VerificarExistenciaPrecio_RestaurarBackup(Precio precio)
        {
            bool resultado = false;
            int precioEncontrado = 0;

            string consulta = "select count(*) Cantidad from ListaPrecios where Codigo = '" + precio.Codigo + "' and Descripcion = '" + precio.Descripcion + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    precioEncontrado = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (precioEncontrado == 1)
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
