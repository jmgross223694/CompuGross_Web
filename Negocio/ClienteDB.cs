using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ClienteDB
    {
        ConexionDB conDB = new ConexionDB();

        public Cliente BuscarCliente(Cliente clienteBuscado) 
        {
            Cliente clienteEncontrado = new Cliente();

            return clienteEncontrado;
        }

        public bool VerificarExistenciaCliente(Cliente cliente)
        {
            bool resultado = false;
            long ID = 0;

            string consulta = "select ID, count(*) Cantidad from Clientes where Estado = 1 and Nombres = '" + cliente.Apenom + "' group by ID";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    int clienteEncontrado = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (clienteEncontrado == 1)
                    {
                        ID = Convert.ToInt64(conDB.Lector["ID"]);
                        if (cliente.ID == ID)
                        {
                            return false; //El cliente encontrado es el que está siendo modificado
                        }
                        else
                        {
                            resultado = true; //Cliente ya existe en sistema
                        }
                    }
                    else
                    {
                        resultado = false; //Cliente no existe en sistema
                    }
                }
                else
                {
                    resultado = false;
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

            return resultado;
        }

        public bool AgregarCliente(Cliente cliente)
        {
            bool resultado = false;

            string insert = "exec SP_NUEVO_CLIENTE '" + cliente.CuitDni + "', '" 
                                                      + cliente.Apenom + "', '" 
                                                      + cliente.Direccion + "', '" 
                                                      + cliente.Localidad + "', '" 
                                                      + cliente.Telefono + "', '" 
                                                      + cliente.Mail + "'";

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

        public bool ModificarCliente(Cliente cliente)
        {
            bool resultado = false;
            int Estado = 1;
            if (!cliente.Estado)
            {
                Estado = 0;
            }

            string update = "exec SP_MODIFICAR_CLIENTE " + cliente.ID + ", '"
                                                         + cliente.CuitDni + "', '"
                                                         + cliente.Apenom + "', '"
                                                         + cliente.Direccion + "', '"
                                                         + cliente.Localidad + "', '"
                                                         + cliente.Telefono + "', '"
                                                         + cliente.Mail + "', "
                                                         + Estado;

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

        public bool EliminarCliente(Cliente cliente)
        {
            bool resultado = false;

            string delete = "delete from Clientes where ID = " + cliente.ID;

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

        public List<Cliente> ListarTodos()
        {
            List <Cliente> listaClientes = new List<Cliente>();

            string consulta = "select * from ExportClientes where Estado = 1 order by Cliente asc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while(conDB.Lector.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    cliente.CuitDni = conDB.Lector["DNI"].ToString();
                    cliente.Apenom = conDB.Lector["Cliente"].ToString();
                    cliente.Direccion = conDB.Lector["Direccion"].ToString();
                    cliente.IdLocalidad = Convert.ToInt64(conDB.Lector["IdLocalidad"]);
                    cliente.Localidad = conDB.Lector["Localidad"].ToString();
                    cliente.Telefono = conDB.Lector["Telefono"].ToString();
                    cliente.Mail = conDB.Lector["Mail"].ToString();
                    DateTime FechaAltaAux = Convert.ToDateTime(conDB.Lector["FechaAlta"]);
                    cliente.FechaAlta = FechaAltaAux.Day + "-" + FechaAltaAux.Month + "-" + FechaAltaAux.Year;
                    cliente.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    if (cliente.Estado)
                    {
                        listaClientes.Add(cliente);
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

            return listaClientes;
        }

        public List<Cliente> ListarFiltrado(string filtro)
        {
            List<Cliente> listaClientesFiltrada = new List<Cliente>();

            string consulta = "select * from ExportClientes where " +
                              "Cliente like '%" + filtro + "%' " +
                              "or DNI like '%" + filtro + "%' " +
                              "or Telefono like '%" + filtro + "%' " +
                              "or Mail like '%" + filtro + "%' " +
                              "or Localidad like '%" + filtro + "%' " +
                              "or Direccion like '%" + filtro + "%' " +
                              " order by Cliente asc";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    cliente.CuitDni = conDB.Lector["DNI"].ToString();
                    cliente.Apenom = conDB.Lector["Cliente"].ToString();
                    cliente.Direccion = conDB.Lector["Direccion"].ToString();
                    cliente.IdLocalidad = Convert.ToInt64(conDB.Lector["IdLocalidad"]);
                    cliente.Localidad = conDB.Lector["Localidad"].ToString();
                    cliente.Telefono = conDB.Lector["Telefono"].ToString();
                    cliente.Mail = conDB.Lector["Mail"].ToString();
                    DateTime FechaAltaAux = Convert.ToDateTime(conDB.Lector["FechaAlta"]);
                    cliente.FechaAlta = FechaAltaAux.Day + "-" + FechaAltaAux.Month + "-" + FechaAltaAux.Year;
                    cliente.Estado = Convert.ToBoolean(conDB.Lector["Estado"]);

                    if (cliente.Estado)
                    {
                        listaClientesFiltrada.Add(cliente);
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

            return listaClientesFiltrada;
        }
    }
}
