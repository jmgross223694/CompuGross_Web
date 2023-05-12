using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ServicioDB
    {
        ConexionDB conDB = new ConexionDB();

        public bool Agregar(Servicio servicio)
        {
            bool resultado = false;
            string insert = "exec SP_INSERT_ORDEN_TRABAJO '" + servicio.Cliente.Apenom + "', " +
                                                         "'" + servicio.FechaRecepcion + "', " +
                                                         "'" + servicio.Equipo.Tipo.Descripcion + "', " +
                                                         "'" + servicio.Equipo.RAM + "', " +
                                                         "'" + servicio.Equipo.PlacaMadre + "', " +
                                                         "'" + servicio.Equipo.MarcaModelo + "', " +
                                                         "'" + servicio.Equipo.Microprocesador + "', " +
                                                         "'" + servicio.Equipo.Almacenamiento + "', " +
                                                         "'" + servicio.Equipo.UnidadOptica.Descripcion + "', " +
                                                         "'" + servicio.Equipo.Alimentacion + "', " +
                                                         "'" + servicio.Equipo.Adicionales + "', " +
                                                         "'" + servicio.Equipo.NumSerie + "', " +
                                                         "'" + servicio.TipoServicio.Descripcion + "', " +
                                                         "'" + servicio.Descripcion + "', " +
                                                         "'" + servicio.CostoRepuestos + "', " +
                                                         "'" + servicio.CostoTerceros + "', " +
                                                         "'" + servicio.Honorarios + "', " +
                                                         "'" + servicio.FechaDevolucion + "'";

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

        public bool Modificar(Servicio servicio)
        {
            bool resultado = false;

            string update = "exec SP_UPDATE_ORDEN_TRABAJO " + servicio.ID + ", " +
                                                         "'" + servicio.Cliente.Apenom + "', " +
                                                         "'" + servicio.FechaRecepcion + "', " +
                                                         "'" + servicio.Equipo.Tipo.Descripcion + "', " +
                                                         "'" + servicio.Equipo.RAM + "', " +
                                                         "'" + servicio.Equipo.PlacaMadre + "', " +
                                                         "'" + servicio.Equipo.MarcaModelo + "', " +
                                                         "'" + servicio.Equipo.Microprocesador + "', " +
                                                         "'" + servicio.Equipo.Almacenamiento + "', " +
                                                         "'" + servicio.Equipo.UnidadOptica.Descripcion + "', " +
                                                         "'" + servicio.Equipo.Alimentacion + "', " +
                                                         "'" + servicio.Equipo.Adicionales + "', " +
                                                         "'" + servicio.Equipo.NumSerie + "', " +
                                                         "'" + servicio.TipoServicio.Descripcion + "', " +
                                                         "'" + servicio.Descripcion + "', " +
                                                         "'" + servicio.CostoRepuestos + "', " +
                                                         "'" + servicio.CostoTerceros + "', " +
                                                         "'" + servicio.Honorarios + "', " +
                                                         "'" + servicio.FechaDevolucion + "'";

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

        public bool Eliminar(Servicio servicio)
        {
            bool resultado = false;
            string delete = "delete from OrdenesTrabajo where ID = " + servicio.ID;

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

        public List<Servicio> ListarTodos()
        {
            List<Servicio> lista = new List<Servicio>();

            string consulta = "select * from ExportModificarOrdenTrabajo where Estado = 1 order by FechaRecepcion desc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    Cliente cliente = new Cliente();
                    Equipo equipo = new Equipo();
                    UnidadOptica unidadOptica = new UnidadOptica();
                    TipoEquipo tipoEquipo = new TipoEquipo();
                    TipoServicio tipoServicio = new TipoServicio();
                    Servicio servicio = new Servicio();

                    servicio.ID = Convert.ToInt64(conDB.Lector["ID"]);

                    DateTime aux = Convert.ToDateTime(conDB.Lector["FechaRecepcion"].ToString());
                    servicio.FechaRecepcion = aux.Day + "/" + aux.Month + "/" + aux.Year;

                    aux = Convert.ToDateTime(conDB.Lector["FechaDevolucion"].ToString());
                    string diaAux, mesAux = "";
                    if (aux.Day < 10)
                    {
                        diaAux = "0" + aux.Day;
                    }
                    else
                    {
                        diaAux = aux.Day.ToString();
                    }
                    if (aux.Month < 10)
                    {
                        mesAux = "0" + aux.Month;
                    }
                    else
                    {
                        mesAux = aux.Month.ToString();
                    }
                    servicio.FechaDevolucion = diaAux + "/" + mesAux + "/" + aux.Year;

                    if (servicio.FechaDevolucion == "1/1/1900")
                    {
                        servicio.FechaDevolucion = "-";
                    }

                    tipoServicio.ID = Convert.ToInt64(conDB.Lector["IdTipoServicio"]);
                    tipoServicio.Descripcion = conDB.Lector["TipoServicio"].ToString();

                    servicio.TipoServicio = tipoServicio;

                    cliente.ID = Convert.ToInt64(conDB.Lector["IdCliente"]);
                    cliente.Apenom = conDB.Lector["Cliente"].ToString();
                    
                    servicio.Cliente = cliente;

                    unidadOptica.ID = Convert.ToInt64(conDB.Lector["IdUnidadOptica"]);
                    unidadOptica.Descripcion = conDB.Lector["UnidadOptica"].ToString();

                    equipo.UnidadOptica = unidadOptica;

                    tipoEquipo.ID = Convert.ToInt64(conDB.Lector["IdTipoEquipo"]);
                    tipoEquipo.Descripcion = conDB.Lector["TipoEquipo"].ToString();

                    equipo.Tipo = tipoEquipo;

                    equipo.RAM = conDB.Lector["RAM"].ToString();
                    equipo.PlacaMadre = conDB.Lector["PlacaMadre"].ToString();
                    equipo.MarcaModelo = conDB.Lector["MarcaModelo"].ToString();
                    equipo.Microprocesador = conDB.Lector["Microprocesador"].ToString();
                    equipo.Almacenamiento = conDB.Lector["Almacenamiento"].ToString();
                    equipo.Alimentacion = conDB.Lector["Alimentacion"].ToString();
                    equipo.Adicionales = conDB.Lector["Adicionales"].ToString();
                    equipo.NumSerie = conDB.Lector["NumSerie"].ToString();

                    servicio.Equipo = equipo;

                    servicio.Descripcion = conDB.Lector["Descripcion"].ToString();
                    servicio.CostoRepuestos = conDB.Lector["CostoRepuestos"].ToString();
                    servicio.Honorarios = conDB.Lector["Honorarios"].ToString();
                    servicio.CostoTerceros = conDB.Lector["CostoTerceros"].ToString();
                    servicio.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    int costoRepuestos = Convert.ToInt32(servicio.CostoRepuestos);
                    int honorarios = Convert.ToInt32(servicio.Honorarios);
                    int costoTerceros = Convert.ToInt32(servicio.CostoTerceros);
                    int subTotal = costoRepuestos + honorarios + costoTerceros;

                    servicio.CostoTotal = subTotal.ToString("N", new CultureInfo("es-AR"));

                    lista.Add(servicio);
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

        public List<Servicio> ListarFiltrado(string filtro)
        {
            List<Servicio> lista = new List<Servicio>();

            string consulta = "select * from ExportModificarOrdenTrabajo where Estado = 1 order by FechaRecepcion desc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    Cliente cliente = new Cliente();
                    Equipo equipo = new Equipo();
                    UnidadOptica unidadOptica = new UnidadOptica();
                    TipoEquipo tipoEquipo = new TipoEquipo();
                    TipoServicio tipoServicio = new TipoServicio();
                    Servicio servicio = new Servicio();

                    servicio.ID = Convert.ToInt64(conDB.Lector["ID"]);

                    DateTime aux = Convert.ToDateTime(conDB.Lector["FechaRecepcion"].ToString());
                    servicio.FechaRecepcion = aux.Day + "/" + aux.Month + "/" + aux.Year;

                    aux = Convert.ToDateTime(conDB.Lector["FechaDevolucion"].ToString());
                    servicio.FechaDevolucion = aux.Day + "/" + aux.Month + "/" + aux.Year;

                    if (servicio.FechaDevolucion == "1/1/1900")
                    {
                        servicio.FechaDevolucion = "-";
                    }

                    tipoServicio.ID = Convert.ToInt64(conDB.Lector["IdTipoServicio"]);
                    tipoServicio.Descripcion = conDB.Lector["TipoServicio"].ToString();

                    servicio.TipoServicio = tipoServicio;

                    cliente.ID = Convert.ToInt64(conDB.Lector["IdCliente"]);
                    cliente.Apenom = conDB.Lector["Cliente"].ToString();

                    servicio.Cliente = cliente;

                    unidadOptica.ID = Convert.ToInt64(conDB.Lector["IdUnidadOptica"]);
                    unidadOptica.Descripcion = conDB.Lector["UnidadOptica"].ToString();

                    equipo.UnidadOptica = unidadOptica;

                    tipoEquipo.ID = Convert.ToInt64(conDB.Lector["IdTipoEquipo"]);
                    tipoEquipo.Descripcion = conDB.Lector["TipoEquipo"].ToString();

                    equipo.Tipo = tipoEquipo;

                    equipo.RAM = conDB.Lector["RAM"].ToString();
                    equipo.PlacaMadre = conDB.Lector["PlacaMadre"].ToString();
                    equipo.MarcaModelo = conDB.Lector["MarcaModelo"].ToString();
                    equipo.Microprocesador = conDB.Lector["Microprocesador"].ToString();
                    equipo.Almacenamiento = conDB.Lector["Almacenamiento"].ToString();
                    equipo.Alimentacion = conDB.Lector["Alimentacion"].ToString();
                    equipo.Adicionales = conDB.Lector["Adicionales"].ToString();
                    equipo.NumSerie = conDB.Lector["NumSerie"].ToString();

                    servicio.Equipo = equipo;

                    servicio.Descripcion = conDB.Lector["Descripcion"].ToString();
                    servicio.CostoRepuestos = conDB.Lector["CostoRepuestos"].ToString();
                    servicio.Honorarios = conDB.Lector["Honorarios"].ToString();
                    servicio.CostoTerceros = conDB.Lector["CostoTerceros"].ToString();
                    servicio.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    int costoRepuestos = Convert.ToInt32(servicio.CostoRepuestos);
                    int honorarios = Convert.ToInt32(servicio.Honorarios);
                    int costoTerceros = Convert.ToInt32(servicio.CostoTerceros);
                    int subTotal = costoRepuestos + honorarios + costoTerceros;

                    servicio.CostoTotal = subTotal.ToString("N", new CultureInfo("es-AR"));

                    filtro = filtro.ToUpper();

                    if (servicio.ID.ToString().Contains(filtro)
                        || servicio.TipoServicio.Descripcion.ToUpper().Contains(filtro)
                        || servicio.Equipo.Tipo.Descripcion.ToUpper().Contains(filtro)
                        || servicio.Cliente.Apenom.ToUpper().Contains(filtro)
                        || servicio.FechaRecepcion.Contains(filtro)
                        || servicio.FechaRecepcion.Replace('/', '-').Contains(filtro)
                        || servicio.FechaDevolucion.Contains(filtro)
                        || servicio.FechaDevolucion.Replace('/', '-').Contains(filtro)
                        || servicio.Equipo.MarcaModelo.ToUpper().Contains(filtro)
                        || servicio.CostoTotal == filtro)
                    {
                        lista.Add(servicio);
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

        public Servicio BuscarPorID (long ID)
        {
            Servicio servicio = new Servicio();

            string consulta = "select * from ExportModificarOrdenTrabajo where ID = " + ID;

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    Cliente cliente = new Cliente();
                    Equipo equipo = new Equipo();
                    UnidadOptica unidadOptica = new UnidadOptica();
                    TipoEquipo tipoEquipo = new TipoEquipo();
                    TipoServicio tipoServicio = new TipoServicio();
                    
                    servicio.ID = Convert.ToInt64(conDB.Lector["ID"]);

                    DateTime aux = Convert.ToDateTime(conDB.Lector["FechaRecepcion"].ToString());
                    servicio.FechaRecepcion = aux.Day + "/" + aux.Month + "/" + aux.Year;

                    aux = Convert.ToDateTime(conDB.Lector["FechaDevolucion"].ToString());
                    servicio.FechaDevolucion = aux.Day + "/" + aux.Month + "/" + aux.Year;

                    if (servicio.FechaDevolucion == "1/1/1900")
                    {
                        servicio.FechaDevolucion = "";
                    }

                    tipoServicio.ID = Convert.ToInt64(conDB.Lector["IdTipoServicio"]);
                    tipoServicio.Descripcion = conDB.Lector["TipoServicio"].ToString();

                    servicio.TipoServicio = tipoServicio;

                    cliente.ID = Convert.ToInt64(conDB.Lector["IdCliente"]);
                    cliente.Apenom = conDB.Lector["Cliente"].ToString();

                    servicio.Cliente = cliente;

                    unidadOptica.ID = Convert.ToInt64(conDB.Lector["IdUnidadOptica"]);
                    unidadOptica.Descripcion = conDB.Lector["UnidadOptica"].ToString();

                    equipo.UnidadOptica = unidadOptica;

                    tipoEquipo.ID = Convert.ToInt64(conDB.Lector["IdTipoEquipo"]);
                    tipoEquipo.Descripcion = conDB.Lector["TipoEquipo"].ToString();

                    equipo.Tipo = tipoEquipo;

                    equipo.RAM = conDB.Lector["RAM"].ToString();
                    equipo.PlacaMadre = conDB.Lector["PlacaMadre"].ToString();
                    equipo.MarcaModelo = conDB.Lector["MarcaModelo"].ToString();
                    equipo.Microprocesador = conDB.Lector["MicroProcesador"].ToString();
                    equipo.Almacenamiento = conDB.Lector["Almacenamiento"].ToString();
                    equipo.Alimentacion = conDB.Lector["Alimentacion"].ToString();
                    equipo.Adicionales = conDB.Lector["Adicionales"].ToString();
                    equipo.NumSerie = conDB.Lector["NumSerie"].ToString();

                    servicio.Equipo = equipo;

                    servicio.Descripcion = conDB.Lector["Descripcion"].ToString();
                    servicio.CostoRepuestos = conDB.Lector["CostoRepuestos"].ToString();
                    servicio.Honorarios = conDB.Lector["Honorarios"].ToString();
                    servicio.CostoTerceros = conDB.Lector["CostoTerceros"].ToString();
                    servicio.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    int costoRepuestos = Convert.ToInt32(servicio.CostoRepuestos);
                    int honorarios = Convert.ToInt32(servicio.Honorarios);
                    int costoTerceros = Convert.ToInt32(servicio.CostoTerceros);
                    int subTotal = costoRepuestos + honorarios + costoTerceros;

                    servicio.CostoTotal = subTotal.ToString("N", new CultureInfo("es-AR"));
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

            return servicio;
        }

        public bool VerificarExistenciaServicio_RestaurarBackup(Servicio servicio)
        {
            bool resultado = false;
            int servicioEncontrado = 0;
            int ganancia = Convert.ToInt32(servicio.Honorarios);

            string consulta = "select count(*) Cantidad from ExportListarOrdenTrabajo where FechaRecepcion = '" + servicio.FechaRecepcion + "' and " +
                                                                                            "FechaDevolucion = '" + servicio.FechaDevolucion + "' and " +
                                                                                            "Cliente = '" + servicio.Cliente.Apenom + "' and " +
                                                                                            "TipoEquipo = '" + servicio.Equipo.Tipo.Descripcion + "' and " +
                                                                                            "MarcaModelo = '" + servicio.Equipo.MarcaModelo + "' and " +
                                                                                            "TipoServicio = '" + servicio.TipoServicio.Descripcion + "' and " +
                                                                                            "Descripcion = '" + servicio.Descripcion + "' and " +
                                                                                            "Ganancia = " + ganancia;

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    servicioEncontrado = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (servicioEncontrado == 1)
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
