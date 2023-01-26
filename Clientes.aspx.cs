using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace CompuGross_Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_Logueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["ModificarEliminar"] != null)
                {
                    if (Convert.ToBoolean(Session["ModificarEliminar"]))
                    {
                        FuncionModificarEliminar();
                    }
                }
                else
                {
                    FuncionModificarEliminar2();
                }
            }
        }

        private void FuncionModificarEliminar()
        {
            if (Request.QueryString["MostrarListadoClientes"] != null)
            {
                FuncionModificarEliminarCliente();
            }

            if (Request.QueryString["MostrarListadoLocalidades"] != null)
            {
                FuncionModificarEliminarLocalidad();
            }

            Session["ModificarEliminar"] = null;
        }

        private void FuncionModificarEliminar2()
        {
            if (Request.QueryString["MostrarListadoClientes"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["MostrarListadoClientes"]);

                if (valor)
                {
                    CargarListadoClientes();
                    AlternarVisibilidadSections("btnModificar");
                }
            }

            if (Request.QueryString["MostrarListadoLocalidades"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["MostrarListadoLocalidades"]);

                if (valor)
                {
                    CargarLocalidades();
                    AlternarVisibilidadSections("MostrarListadoLocalidades");
                }
            }

            if (Request.QueryString["MostrarSeccionPrincipalLocalidades"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["MostrarSeccionPrincipalLocalidades"]);

                if (valor)
                {
                    AlternarVisibilidadSections("btnLocalidades");
                }
            }

            if (Request.QueryString["IdLocalidad"] != null) //Click en modificar o eliminar alguna localidad del listado
            {
                long ID = Convert.ToInt64(Request.QueryString["IdLocalidad"]);
                BuscarLocalidadSeleccionada(ID);
            }

            if (Request.QueryString["IdCliente"] != null) //Click en modificar o eliminar algún cliente del listado
            {
                CargarLocalidades();
                long ID = Convert.ToInt64(Request.QueryString["IdCliente"]);
                BuscarClienteSeleccionado(ID);
            }
        }

        private void FuncionModificarEliminarCliente()
        {
            Cliente cliente = new Cliente();

            if (Session["ClienteModificarEliminar"] != null)
            {
                cliente = (Cliente)Session["ClienteModificarEliminar"];
            }

            if (Request.QueryString["Modificar"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["Modificar"]);

                if (valor)
                {
                    CargarListadoClientes();
                    AlternarVisibilidadSections("btnModificar");
                    hfMessage.Value = "Cliente '" + cliente.Apenom + "' modificado exitosamente"; //NO MUESTRA MENSAJE
                }
            }

            if (Request.QueryString["Eliminar"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["Eliminar"]);

                if (valor)
                {
                    CargarListadoClientes();
                    AlternarVisibilidadSections("btnModificar");
                    hfMessage.Value = "Cliente '" + cliente.Apenom + "' eliminado exitosamente"; //NO MUESTRA MENSAJE
                }
            }
        }

        private void FuncionModificarEliminarLocalidad()
        {
            Localidad localidad = new Localidad();

            if (Session["LocalidadModificarEliminar"] != null)
            {
                localidad = (Localidad)Session["LocalidadModificarEliminar"];
            }

            if (Request.QueryString["Modificar"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["Modificar"]);

                if (valor)
                {
                    hfMessage.Value = "Localidad '" + localidad.Descripcion + "' modificada exitosamente";
                    CargarLocalidades();
                    AlternarVisibilidadSections("MostrarListadoLocalidades");
                }
            }

            if (Request.QueryString["Eliminar"] != null)
            {
                bool valor = Convert.ToBoolean(Request.QueryString["Eliminar"]);

                if (valor)
                {
                    hfMessage.Value = "Localidad '" + localidad.Descripcion + "' eliminada exitosamente";
                    CargarLocalidades();
                    AlternarVisibilidadSections("MostrarListadoLocalidades");
                }
            }
        }

        private void CargarLocalidades()
        {
            LocalidadDB locDB = new LocalidadDB();

            List<Localidad> listaLocalidades = new List<Localidad>();
            listaLocalidades = locDB.ListarTodas();

            Session["ListaLocalidades"] = listaLocalidades;

            CargarListaLocalidadesNuevoCliente(listaLocalidades);
            CargarListaLocalidadesModificarCliente(listaLocalidades);
            CargarListaLocalidades_ABM_Localidades(listaLocalidades);
        }

        private void CargarListaLocalidadesNuevoCliente(List<Localidad> lista)
        {
            DdlNuevoClienteLocalidad.DataSource = lista;
            DdlNuevoClienteLocalidad.DataMember = "datos";
            DdlNuevoClienteLocalidad.DataTextField = "Descripcion";
            DdlNuevoClienteLocalidad.DataValueField = "ID";
            DdlNuevoClienteLocalidad.DataBind();
        }

        private void CargarListaLocalidadesModificarCliente(List<Localidad> lista)
        {
            DdlModificarClienteLocalidad.DataSource = lista;
            DdlModificarClienteLocalidad.DataMember = "datos";
            DdlModificarClienteLocalidad.DataTextField = "Descripcion";
            DdlModificarClienteLocalidad.DataValueField = "ID";
            DdlModificarClienteLocalidad.DataBind();
        }

        private void CargarListaLocalidades_ABM_Localidades(List<Localidad> lista)
        {
            Localidad localidadRemover = new Localidad();

            foreach(Localidad localidad in lista)
            {
                if (localidad.Descripcion == "-")
                {
                    localidadRemover = localidad;
                }
            }

            lista.Remove(localidadRemover);

            RepeaterLocalidades.DataSource = lista;
            RepeaterLocalidades.DataBind();
        }

        private void BuscarLocalidadSeleccionada(long ID)
        {
            Localidad localidadSeleccionada = new Localidad();
            List<Localidad> listaLocalidades = new List<Localidad>();
            bool localidadEncontrada = false;

            if (Session["ListaLocalidades"] != null)
            {
                listaLocalidades = (List<Localidad>)Session["ListaLocalidades"];

                foreach (Localidad localidad in listaLocalidades)
                {
                    if (localidad.ID == ID && !localidadEncontrada)
                    {
                        localidadSeleccionada.ID = localidad.ID;
                        localidadSeleccionada.Descripcion = localidad.Descripcion;
                        localidadSeleccionada.Estado = localidad.Estado;

                        localidadEncontrada = true;
                    }
                }
            }

            VerificarAccionLocalidad(localidadSeleccionada);
        }

        private void VerificarAccionLocalidad(Localidad localidadSeleccionada)
        {
            if (Request.QueryString["AccionLocalidad"] != null)
            {
                string accionLocalidad = Request.QueryString["AccionLocalidad"].ToString();

                if (accionLocalidad == "CargarCamposModificar")
                {
                    CargarCamposModificarLocalidad(localidadSeleccionada);
                }

                if (accionLocalidad == "ConfirmarEliminar")
                {
                    CargarCamposEliminarLocalidad(localidadSeleccionada);
                }
            }
        }
        
        private void CargarCamposModificarLocalidad(Localidad localidad)
        {
            AlternarVisibilidadSections("ModificarLocalidad");

            if (HfIdLocalidadModificar.Value == "0")
            {
                HfIdLocalidadModificar.Value = localidad.ID.ToString();
                TxtLocalidadesModificarDescripcion.Text = localidad.Descripcion;
                DdlLocalidadesModificarEstado.SelectedValue = "1";
                if (!localidad.Estado)
                {
                    DdlModificarClienteEstado.SelectedValue = "0";
                }

                Session["LocalidadModificar"] = localidad;
            }
        }

        private void CargarCamposEliminarLocalidad(Localidad localidad)
        {
            AlternarVisibilidadSections("EliminarLocalidad");

            if (HfIdLocalidadModificar.Value == "0")
            {
                HfIdLocalidadModificar.Value = localidad.ID.ToString();
                TxtLocalidadesModificarDescripcion.Text = localidad.Descripcion;
                DdlLocalidadesModificarEstado.SelectedValue = "1";
                if (!localidad.Estado)
                {
                    DdlModificarClienteEstado.SelectedValue = "0";
                }

                Session["LocalidadEliminar"] = localidad;
            }

            LblLocalidadesConfirmarEliminar.Text = "¿ Seguro que desea eliminar la Localidad '" + localidad.Descripcion + "' ?";

            InhabilitarCamposEliminarLocalidad();
        }

        private void InhabilitarCamposEliminarLocalidad()
        {
            TxtLocalidadesModificarDescripcion.Enabled = false;
            DdlLocalidadesModificarEstado.Enabled = false;
            BtnLocalidadesConfirmarModificar.Style.Add("display", "none");
            BtnLocalidadesCancelarModificar.Style.Add("display", "none");
        }

        private void BuscarClienteSeleccionado(long ID)
        {
            Cliente clienteSeleccionado = new Cliente();
            List<Cliente> listaClientes = new List<Cliente>();
            bool clienteEncontrado = false;

            if (Session["listadoClientes"] != null)
            {
                listaClientes = (List<Cliente>)Session["listadoClientes"];

                foreach (Cliente cliente in listaClientes)
                {
                    if (cliente.ID == ID && !clienteEncontrado)
                    {
                        clienteSeleccionado.ID = cliente.ID;
                        clienteSeleccionado.CuitDni = cliente.CuitDni;
                        clienteSeleccionado.Apenom = cliente.Apenom;
                        clienteSeleccionado.Direccion = cliente.Direccion;
                        clienteSeleccionado.Localidad = cliente.Localidad;
                        clienteSeleccionado.Telefono = cliente.Telefono;
                        clienteSeleccionado.Mail = cliente.Mail;
                        clienteSeleccionado.FechaAlta = cliente.FechaAlta;
                        clienteSeleccionado.Estado = cliente.Estado;

                        clienteEncontrado = true;
                    }
                }
            }

            VerificarAccionCliente(clienteSeleccionado);
        }

        private void VerificarAccionCliente(Cliente clienteSeleccionado)
        {
            if (Request.QueryString["AccionCliente"] != null)
            {
                string accionCliente = Request.QueryString["AccionCliente"].ToString();

                if (accionCliente == "CargarCamposModificar")
                {
                    CargarCamposModificarCliente(clienteSeleccionado);
                }

                if (accionCliente == "ConfirmarEliminar")
                {
                    CargarCamposEliminarCliente(clienteSeleccionado);
                }
            }
        }

        private void CargarCamposEliminarCliente(Cliente cliente)
        {
            AlternarVisibilidadSections("EliminarCliente");

            if (hfIdCliente.Value == "0")
            {
                hfIdCliente.Value = cliente.ID.ToString();
                TxtModificarClienteCuitDni.Text = cliente.CuitDni;
                TxtModificarClienteApenom.Text = cliente.Apenom;
                TxtModificarClienteDireccion.Text = cliente.Direccion;
                DdlModificarClienteLocalidad.SelectedValue = cliente.Localidad.ID.ToString();
                TxtModificarClienteTelefono.Text = cliente.Telefono;
                TxtModificarClienteMail.Text = cliente.Mail;
                DdlModificarClienteEstado.SelectedValue = "1";
                if (!cliente.Estado)
                {
                    DdlModificarClienteEstado.SelectedValue = "0";
                }

                Session["ClienteEliminar"] = cliente;
            }

            LblConfirmarEliminarCliente.Text = "¿ Seguro que desea eliminar al cliente '" + cliente.Apenom + "' ?";

            LblModificarClienteTitulo.Text = "Eliminar Cliente";

            InhabilitarCamposEliminarCliente();
        }

        private void InhabilitarCamposEliminarCliente()
        {
            TxtModificarClienteCuitDni.Enabled = false;
            TxtModificarClienteApenom.Enabled = false;
            TxtModificarClienteDireccion.Enabled = false;
            DdlModificarClienteLocalidad.Enabled = false;
            TxtModificarClienteTelefono.Enabled = false;
            TxtModificarClienteMail.Enabled = false;
            DdlModificarClienteEstado.Enabled = false;
            BtnModificarClienteConfirmar.Enabled = false;
            BtnModificarClienteConfirmar.Style.Add("display", "none");
            BtnModificarClienteCancelarEdicion.Enabled = false;
            BtnModificarClienteCancelarEdicion.Style.Add("display", "none");
        }

        private void CargarCamposModificarCliente(Cliente cliente)
        {
            AlternarVisibilidadSections("ModificarCliente");

            if (hfIdCliente.Value == "0")
            {
                hfIdCliente.Value = cliente.ID.ToString();
                TxtModificarClienteCuitDni.Text = cliente.CuitDni;
                TxtModificarClienteApenom.Text = cliente.Apenom;
                TxtModificarClienteDireccion.Text = cliente.Direccion;
                DdlModificarClienteLocalidad.SelectedValue = cliente.Localidad.ID.ToString();
                TxtModificarClienteTelefono.Text = cliente.Telefono;
                TxtModificarClienteMail.Text = cliente.Mail;
                DdlModificarClienteEstado.SelectedValue = "1";
                if (!cliente.Estado)
                {
                    DdlModificarClienteEstado.SelectedValue = "0";
                }
            }

            LblModificarClienteTitulo.Text = "Modificar Cliente";

            HabilitarCamposModificar();
        }

        private void HabilitarCamposModificar()
        {
            TxtModificarClienteCuitDni.Enabled = true;
            TxtModificarClienteApenom.Enabled = true;
            TxtModificarClienteDireccion.Enabled = true;
            DdlModificarClienteLocalidad.Enabled = true;
            TxtModificarClienteTelefono.Enabled = true;
            TxtModificarClienteMail.Enabled = true;
            DdlModificarClienteEstado.Enabled = true;
            BtnModificarClienteConfirmar.Enabled = true;
            BtnModificarClienteConfirmar.Style.Add("display", "block");
            BtnModificarClienteCancelarEdicion.Enabled = true;
            BtnModificarClienteCancelarEdicion.Style.Add("display", "block");
        }

        protected void BtnNuevoClienteConfirmar_Click(object sender, EventArgs e)
        {
            Localidad localidad = new Localidad();
            Cliente cliente = new Cliente();

            cliente.CuitDni = TxtNuevoClienteCuitDni.Text;
            cliente.Apenom = TxtNuevoClienteApenom.Text;
            cliente.Direccion = TxtNuevoClienteDireccion.Text;

            localidad.ID = Convert.ToInt64(DdlNuevoClienteLocalidad.SelectedValue.ToString());
            localidad.Descripcion = DdlNuevoClienteLocalidad.SelectedItem.ToString();
            localidad.Estado = true;

            cliente.Localidad = localidad;
            cliente.Telefono = TxtNuevoClienteTelefono.Text;
            cliente.Mail = TxtNuevoClienteMail.Text;

            if (cliente.CuitDni == "") { cliente.CuitDni = "-"; }
            if (cliente.Direccion == "") { cliente.Direccion = "-"; }
            if (cliente.Mail == "") { cliente.Mail = "-"; }

            if (ValidarCamposCliente(cliente))
            {
                ClienteDB cDB = new ClienteDB();

                if (!cDB.VerificarExistenciaCliente(cliente))
                {
                    if (cDB.AgregarCliente(cliente))
                    {
                        ResetearCamposNuevoCliente();
                        hfMessage.Value = "Cliente '" + cliente.Apenom + "' agregado exitosamente";
                        CargarListadoClientes();
                    }
                    else
                    {
                        hfError.Value = "Se produjo un error al intentar agregar al nuevo Cliente";
                    }
                }
                else
                {
                    hfError.Value = "El Cliente '" + cliente.Apenom + "' ya existe en el sistema";
                }
            }
            else
            {
                hfError.Value = "Hay datos obligatorios inválidos o sin completar";
            }
        }

        private bool ValidarCamposCliente(Cliente cliente)
        {
            if (ValidarCuitDni(cliente.CuitDni) && ValidarApeNom(cliente.Apenom)
                && ValidarTelefono(cliente.Telefono) && ValidarMail(cliente.Mail))
            {
                return true;
            }

            return false;
        }

        private bool ValidarCuitDni(string cuitDni)
        {
            if (cuitDni != "")
            {
                if (cuitDni == "-")
                {
                    return true;
                }
                else if (cuitDni.Length == 10 || cuitDni.Length == 9 || cuitDni.Length < 7)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidarApeNom(string apeNom)
        {
            if (apeNom == "")
            {
                return false;
            }

            return true;
        }

        private bool ValidarTelefono(string telefono)
        {
            if (telefono == "")
            {
                return false;
            }

            return true;
        }

        private bool ValidarMail(string mail)
        {
            if (mail != "")
            {
                if (mail == "-")
                {
                    return true;
                }
                else if (mail.Contains("@") && mail.Contains(".com") && !mail.Contains("@.com"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private void ResetearCamposNuevoCliente()
        {
            TxtNuevoClienteCuitDni.Text = "";
            TxtNuevoClienteApenom.Text = "";
            TxtNuevoClienteDireccion.Text = "";
            DdlNuevoClienteLocalidad.SelectedValue = "1";
            TxtNuevoClienteTelefono.Text = "";
            TxtNuevoClienteMail.Text = "";
        }

        private void ResetearCamposModificarCliente()
        {
            hfIdCliente.Value = "0";
            TxtModificarClienteCuitDni.Text = "";
            TxtModificarClienteApenom.Text = "";
            TxtModificarClienteDireccion.Text = "";
            DdlModificarClienteLocalidad.SelectedValue = "1";
            TxtModificarClienteTelefono.Text = "";
            TxtModificarClienteMail.Text = "";
            DdlModificarClienteEstado.SelectedValue = "0";
        }

        private void ResetearCamposEliminarCliente()
        {
            //hfIdCliente.Value = "0";
            TxtModificarClienteCuitDni.Text = "";
            TxtModificarClienteApenom.Text = "";
            TxtModificarClienteDireccion.Text = "";
            DdlModificarClienteLocalidad.SelectedValue = "1";
            TxtModificarClienteTelefono.Text = "";
            TxtModificarClienteMail.Text = "";
            DdlModificarClienteEstado.SelectedValue = "0";
        }

        private void CargarListadoClientes()
        {
            ClienteDB cDB = new ClienteDB();

            List<Cliente> listaClientes = new List<Cliente>();

            listaClientes = cDB.ListarTodos();

            Session["listadoClientes"] = listaClientes;

            RepeaterListadoClientes.DataSource = listaClientes;
            RepeaterListadoClientes.DataBind();
        }

        private void CargarListadoClientesFiltrado(string filtro)
        {
            ClienteDB cDB = new ClienteDB();

            List<Cliente> listaClientesFiltrada = new List<Cliente>();

            listaClientesFiltrada = cDB.ListarFiltrado(filtro);

            Session["listadoClientes"] = listaClientesFiltrada;

            RepeaterListadoClientes.DataSource = listaClientesFiltrada;
            RepeaterListadoClientes.DataBind();
        }

        protected void BtnModificarClienteBusqueda_Click(object sender, EventArgs e)
        {
            string filtro = TxtModificarClienteBusqueda.Text;

            if (filtro != "")
            {
                CargarListadoClientesFiltrado(filtro);
            }
            else
            {
                CargarListadoClientes();
            }
        }

        protected void BtnModificarClienteConfirmar_Click(object sender, EventArgs e)
        {
            Localidad localidad = new Localidad();
            Cliente cliente = new Cliente();

            cliente.ID = Convert.ToInt64(hfIdCliente.Value);
            cliente.CuitDni = TxtModificarClienteCuitDni.Text;
            cliente.Apenom = TxtModificarClienteApenom.Text;
            cliente.Direccion = TxtModificarClienteDireccion.Text;

            localidad.ID = Convert.ToInt64(DdlModificarClienteLocalidad.SelectedValue.ToString());
            localidad.Descripcion = DdlModificarClienteLocalidad.SelectedItem.ToString();
            localidad.Estado = true;

            cliente.Localidad = localidad;
            cliente.Telefono = TxtModificarClienteTelefono.Text;
            cliente.Mail = TxtModificarClienteMail.Text;
            cliente.Estado = true;
            if (DdlModificarClienteEstado.SelectedValue.ToString() == "0")
            {
                cliente.Estado = false;
            }

            if (cliente.CuitDni == "") { cliente.CuitDni = "-"; }
            if (cliente.Direccion == "") { cliente.Direccion = "-"; }
            if (cliente.Mail == "") { cliente.Mail = "-"; }

            if (ValidarCamposCliente(cliente))
            {
                ClienteDB cDB = new ClienteDB();

                if (!cDB.VerificarExistenciaCliente(cliente))
                {
                    if (cDB.ModificarCliente(cliente))
                    {
                        ResetearCamposModificarCliente();
                        CargarLocalidades();
                        CargarListadoClientes();

                        Session["ModificarEliminar"] = true;
                        Session["ClienteModificarEliminar"] = cliente;
                        Response.Redirect("Clientes.aspx?MostrarListadoClientes=true&Modificar=true");
                    }
                    else
                    {
                        hfError.Value = "Se produjo un error al intentar modificar Cliente";
                    }
                }
                else
                {
                    hfError.Value = "Ya existe otro cliente con el Apellido y Nombre ingresado";
                }
            }
        }

        protected void BtnBotonPrincipalNuevoCliente_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("btnAgregar");
            CargarLocalidades();
        }

        protected void BtnBotonPrincipalModificarCliente_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("btnModificar");
            CargarLocalidades();
            CargarListadoClientes();
            LblModificarClienteTitulo.Text = "Listado de Clientes";
        }

        protected void BtnBotonPrincipalLocalidades_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("btnLocalidades");
            CargarLocalidades();
        }

        protected void BtnNuevoClienteCancelar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelar");
        }

        protected void BtnModificarClienteCancelar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelar");
        }

        protected void BtnModificarClienteCancelarEdicion_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelarModificar");
            CargarLocalidades();
            CargarListadoClientes();
        }

        protected void BtnConfirmarEliminarCliente_Click(object sender, EventArgs e)
        {
            ClienteDB cDB = new ClienteDB();
            Cliente cliente = new Cliente();

            if (Session["ClienteEliminar"] != null)
            {
                cliente = (Cliente)Session["ClienteEliminar"];
                if (cDB.EliminarCliente(cliente))
                {
                    ResetearCamposEliminarCliente();
                    CargarLocalidades();
                    CargarListadoClientes();

                    Session["ModificarEliminar"] = true;
                    Session["ClienteModificarEliminar"] = cliente;
                    Response.Redirect("Clientes.aspx?MostrarListadoClientes=true&Eliminar=true");
                }
            }
            else
            {
                hfError.Value = "No se pudo eliminar al Cliente '" + TxtModificarClienteApenom.Text + "'";
            }
        }

        protected void BtnCancelarEliminarCliente_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelarEliminar");
            CargarLocalidades();
            CargarListadoClientes();
        }

        protected void BtnLocalidadesAgregar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("AgregarLocalidad");
        }

        protected void BtnLocalidadesListar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("ListarLocalidades");
            CargarLocalidades();
            LblLocalidadesTitulo.Text = "Modificar Localidades";
        }

        protected void BtnLocalidadesCancelar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelar");
        }

        protected void BtnLocalidadesCancelarAgregar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("CancelarAgregarLocalidad");
            TxtLocalidadesAgregarDescripcion.Text = "";
        }

        protected void BtnLocalidadesListarCancelar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("CancelarListarLocalidades");
            TxtLocalidadesBuscar.Text = "";
        }

        private void AlternarVisibilidadSections(string botonOprimido)
        {
            if (botonOprimido == "AgregarLocalidad")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_localidades_principal.Style.Add("display", "none");
                section_localidades_busqueda.Style.Add("display", "none");
                section_localidades_listado.Style.Add("display", "none");
                section_agregar_localidad.Style.Add("display", "block");
                LblLocalidadesTitulo.Text = "Nueva Localidad";
            }

            if (botonOprimido == "btnLocalidadesConfirmarAgregar")
            {
                section_localidades_principal.Style.Add("display", "block");
                section_localidades_busqueda.Style.Add("display", "none");
                section_localidades_listado.Style.Add("display", "none");
                section_agregar_localidad.Style.Add("display", "none");
                section_modificar_localidad.Style.Add("display", "none");
                BtnLocalidadesConfirmarModificar.Style.Add("display", "none");
                BtnLocalidadesCancelarModificar.Style.Add("display", "none");
                section_eliminar_localidad.Style.Add("display", "none");
                LblLocalidadesTitulo.Text = "Localidades";
            }

            if (botonOprimido == "CancelarAgregarLocalidad")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_botones_principales.Style.Add("display", "none");
                section_localidades_principal.Style.Add("display", "block");
                section_localidades_busqueda.Style.Add("display", "none");
                section_localidades_listado.Style.Add("display", "none");
                section_agregar_localidad.Style.Add("display", "none");
            }

            if (botonOprimido == "ModificarLocalidad")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_localidades_principal.Style.Add("display", "none");
                section_localidades_busqueda.Style.Add("display", "none");
                section_localidades_listado.Style.Add("display", "none");
                section_agregar_localidad.Style.Add("display", "none");
                section_modificar_localidad.Style.Add("display", "block");
                BtnLocalidadesConfirmarModificar.Style.Add("display", "block");
                BtnLocalidadesCancelarModificar.Style.Add("display", "block");
                section_eliminar_localidad.Style.Add("display", "none");
                LblLocalidadesTitulo.Text = "Modificar Localidad";
            }

            if (botonOprimido == "CancelarModificarEliminar")
            {
                Response.Redirect("Clientes.aspx?MostrarListadoLocalidades=true");
            }

            if (botonOprimido == "MostrarListadoLocalidades")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_botones_principales.Style.Add("display", "none");
                section_localidades_principal.Style.Add("display", "none");
                section_localidades_busqueda.Style.Add("display", "block");
                section_localidades_listado.Style.Add("display", "block");
                section_agregar_localidad.Style.Add("display", "none");
                section_modificar_localidad.Style.Add("display", "none");
                BtnLocalidadesConfirmarModificar.Style.Add("display", "none");
                BtnLocalidadesCancelarModificar.Style.Add("display", "none");
                section_eliminar_localidad.Style.Add("display", "none");
                LblLocalidadesTitulo.Text = "Localidades";
            }

            if (botonOprimido == "EliminarLocalidad")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_localidades_principal.Style.Add("display", "none");
                section_localidades_busqueda.Style.Add("display", "none");
                section_localidades_listado.Style.Add("display", "none");
                section_agregar_localidad.Style.Add("display", "none");
                section_modificar_localidad.Style.Add("display", "block");
                section_eliminar_localidad.Style.Add("display", "block");
                LblLocalidadesTitulo.Text = "Modificar Localidad";
            }

            if (botonOprimido == "ListarLocalidades")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_botones_principales.Style.Add("display", "none");
                section_localidades_principal.Style.Add("display", "none");
                section_localidades_busqueda.Style.Add("display", "block");
                section_localidades_listado.Style.Add("display", "block");
                section_agregar_localidad.Style.Add("display", "none");
            }

            if (botonOprimido == "CancelarListarLocalidades")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
                section_botones_principales.Style.Add("display", "none");
                section_localidades_principal.Style.Add("display", "block");
                section_localidades_busqueda.Style.Add("display", "none");
                section_localidades_listado.Style.Add("display", "none");
                section_agregar_localidad.Style.Add("display", "none");
                section_modificar_localidad.Style.Add("display", "none");
                section_eliminar_localidad.Style.Add("display", "none");
                LblLocalidadesTitulo.Text = "Localidades";
                Response.Redirect("Clientes.aspx?MostrarSeccionPrincipalLocalidades=true");
            }

            if (botonOprimido == "btnAgregar")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "block");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "block");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
            }

            if (botonOprimido == "btnModificar")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "block");
                section_localidades.Style.Add("display", "none");
                section_listado_modificar_cliente.Style.Add("display", "block");
                section_campos_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
                LblModificarClienteTitulo.Text = "Listado de Clientes";
            }

            if (botonOprimido == "btnLocalidades")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "block");
            }

            if (botonOprimido == "cancelar")
            {
                section_botones_principales.Style.Add("display", "block");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
                Response.Redirect("Clientes.aspx");
            }

            if (botonOprimido == "cancelarModificar")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "block");
                section_listado_modificar_cliente.Style.Add("display", "block");
                section_campos_modificar_cliente.Style.Add("display", "none");
                section_confirmar_eliminar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
                LblModificarClienteTitulo.Text = "Listado de Clientes";
                Response.Redirect("Clientes.aspx?MostrarListadoClientes=true");
            }

            if (botonOprimido == "cancelarEliminar")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "block");
                section_listado_modificar_cliente.Style.Add("display", "block");
                section_campos_modificar_cliente.Style.Add("display", "none");
                section_confirmar_eliminar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
                LblModificarClienteTitulo.Text = "Listado de Clientes";
                Response.Redirect("Clientes.aspx?MostrarListadoClientes=true");
            }

            if (botonOprimido == "ModificarCliente")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "block");
                section_listado_modificar_cliente.Style.Add("display", "none");
                section_campos_modificar_cliente.Style.Add("display", "block");
                section_confirmar_eliminar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
            }

            if (botonOprimido == "EliminarCliente")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "block");
                section_listado_modificar_cliente.Style.Add("display", "none");
                section_campos_modificar_cliente.Style.Add("display", "block");
                section_confirmar_eliminar_cliente.Style.Add("display", "block");
                section_localidades.Style.Add("display", "none");
            }
        }

        private void CargarListadoLocalidadesFiltrado(string filtro)
        {
            LocalidadDB lDB = new LocalidadDB();

            List<Localidad> listaLocalidadesFiltrada = new List<Localidad>();

            listaLocalidadesFiltrada = lDB.ListarFiltrado(filtro);

            Session["ListadoLocalidades"] = listaLocalidadesFiltrada;

            RepeaterLocalidades.DataSource = listaLocalidadesFiltrada;
            RepeaterLocalidades.DataBind();
        }

        protected void BtnLocalidadesBuscar_Click(object sender, EventArgs e)
        {
            string filtro = TxtLocalidadesBuscar.Text;

            if (filtro != "")
            {
                CargarListadoLocalidadesFiltrado(filtro);
            }
            else
            {
                CargarLocalidades();
            }
        }

        protected void BtnLocalidadesCancelarModificar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("CancelarModificarEliminar");
            CargarLocalidades();
        }

        private void ResetearCamposLocalidades()
        {
            TxtLocalidadesBuscar.Text = "";
            TxtLocalidadesAgregarDescripcion.Text = "";
            TxtLocalidadesModificarDescripcion.Text = "";
            HfIdLocalidadModificar.Value = "0";
            DdlLocalidadesModificarEstado.SelectedValue = "1";
        }

        protected void BtnLocalidadesConfirmarAgregar_Click(object sender, EventArgs e)
        {
            Localidad localidad = new Localidad();
                
            localidad.Descripcion = TxtLocalidadesAgregarDescripcion.Text;

            if (localidad.Descripcion != "")
            {
                if (!ValidarExistenciaLocalidad(localidad))
                {
                    try
                    {
                        LocalidadDB lDB = new LocalidadDB();
                        if (lDB.Agregar(localidad))
                        {
                            ResetearCamposLocalidades();

                            hfMessage.Value = "La Localidad '" + localidad.Descripcion + "' ha sido agregada exitosamente";

                            CargarLocalidades();
                            AlternarVisibilidadSections("btnLocalidades");
                            AlternarVisibilidadSections("btnLocalidadesConfirmarAgregar");
                        }
                        else
                        {
                            hfError.Value = "No ha sido posible agregar la Localidad '" + localidad.Descripcion + "'";
                        }
                    }
                    catch
                    {
                        hfError.Value = "No ha sido posible agregar la Localidad '" + localidad.Descripcion + "'";
                    }
                }
                else
                {
                    hfError.Value = "La Localidad '" + localidad.Descripcion + "' ya existe en el sistema";
                }
            }
            else
            {
                hfError.Value = "Debe ingresar una descripción para la nueva Localidad";
            }
        }

        protected void BtnLocalidadesConfirmarModificar_Click(object sender, EventArgs e)
        {
            Localidad localidad = new Localidad();

            localidad.ID = Convert.ToInt64(HfIdLocalidadModificar.Value);
            localidad.Descripcion = TxtLocalidadesModificarDescripcion.Text;
            localidad.Estado = true;
            if (DdlLocalidadesModificarEstado.SelectedValue == "0")
            {
                localidad.Estado = false;
            }

            if (localidad.Descripcion != "")
            {
                if (!ValidarExistenciaLocalidad(localidad))
                {
                    try
                    {
                        LocalidadDB lDB = new LocalidadDB();
                        if (lDB.Modificar(localidad))
                        {
                            ResetearCamposLocalidades();

                            hfMessage.Value = "La Localidad '" + localidad.Descripcion + "' se ha modificado exitosamente";

                            CargarLocalidades();
                            AlternarVisibilidadSections("MostrarListadoLocalidades");

                            Session["ModificarEliminar"] = true;
                            Session["LocalidadModificarEliminar"] = localidad;
                            Response.Redirect("Clientes.aspx?MostrarListadoLocalidades=true&Modificar=true");
                        }
                        else
                        {
                            hfError.Value = "Se ha producido un error al intentar guardar los cambios";
                        }
                    }
                    catch
                    {
                        hfError.Value = "Se ha producido un error al intentar guardar los cambios";
                    }
                }
                else
                {
                    hfError.Value = "La Localidad '" + localidad.Descripcion + "' ya existe en el sistema";
                }
            }
            else
            {
                hfError.Value = "La descripción de la Localidad no puede quedar vacía";
            }
        }

        private bool ValidarExistenciaLocalidad(Localidad localidad)
        {
            bool resultado = true;

            LocalidadDB lDB = new LocalidadDB();

            resultado = lDB.ValidarExistencia(localidad);

            return resultado;
        }

        protected void BtnLocalidadesConfirmarEliminar_Click(object sender, EventArgs e)
        {
            Localidad localidad = new Localidad();

            localidad.ID = Convert.ToInt64(HfIdLocalidadModificar.Value);

            try
            {
                LocalidadDB lDB = new LocalidadDB();
                if (lDB.Eliminar(localidad))
                {
                    ResetearCamposLocalidades();

                    hfMessage.Value = "La Localidad '" + localidad.Descripcion + "' se ha eliminado exitosamente";

                    CargarLocalidades();
                    AlternarVisibilidadSections("MostrarListadoLocalidades");

                    Session["ModificarEliminar"] = true;
                    Session["LocalidadModificarEliminar"] = localidad;
                    Response.Redirect("Clientes.aspx?MostrarListadoLocalidades=true&Eliminar=true");
                }
                else
                {
                    hfError.Value = "No ha sido posible eliminar la Localidad '" + localidad.Descripcion + "'";
                }
            }
            catch
            {
                hfError.Value = "No ha sido posible eliminar la Localidad '" + localidad.Descripcion + "'";
            }
        }

        protected void BtnLocalidadesCancelarEliminar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("CancelarModificarEliminar");
            CargarLocalidades();
        }
    }
}