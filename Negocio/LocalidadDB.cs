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

        public List<Localidad> ListarFiltrado(string filtro)
        {
            List<Localidad> listaLocalidadesFiltrada = new List<Localidad>();

            string consulta = "select * from Localidades where Estado = 1 and Descripcion like '%" + filtro + "%'";

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
                        listaLocalidadesFiltrada.Add(localidad);
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

            return listaLocalidadesFiltrada;
        }

        public bool Agregar(Localidad localidad)
        {
            bool resultado = false;

            string insert = "exec SP_NUEVA_LOCALIDAD '"+ localidad.Descripcion + "'";

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

        public bool Modificar(Localidad localidad)
        {
            bool resultado = false;
            int estado = 1;
            if (!localidad.Estado)
            {
                estado = 0;
            }

            string update = "update Localidades set Descripcion = '" + localidad.Descripcion + "', " +
                                              " Estado = " + estado +
                                              " where ID = " + localidad.ID;

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

        public bool Eliminar(Localidad localidad)
        {
            bool resultado = false;

            string delete = "delete from Localidades where ID = " + localidad.ID;

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

        public bool ValidarExistencia(Localidad localidad)
        {
            bool resultado = true;
            int cantidad = 0;

            string consulta = "select Estado, Count(*) Cantidad from Localidades where Descripcion = '" + localidad.Descripcion + "' group by Estado";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    cantidad = Convert.ToInt32(conDB.Lector["Cantidad"]);
                    bool estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    if (cantidad > 0)
                    {
                        if (!estado)
                        {
                            resultado = false; //ya existe y esta inactiva
                        }
                        else
                        {
                            resultado = true; //ya existe la localidad ingresada
                        }
                    }
                    else
                    {
                        resultado = false; //NO existe la localidad ingresada
                    }
                }
                else
                {
                    if (cantidad == 0)
                    {
                        resultado = false;
                    }
                    else
                    {
                        resultado = true;
                    }
                }
            }
            catch
            {
                resultado = true;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
        }

        public bool VerificarExistenciaLocalidad_RestaurarBackup(Localidad localidad)
        {
            bool resultado = false;
            int localidadEncontrada = 0;

            string consulta = "select count(*) Cantidad from Localidades where Descripcion = '" + localidad.Descripcion + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    localidadEncontrada = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (localidadEncontrada == 1)
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
