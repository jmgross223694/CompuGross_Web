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
                //Usuario usuarioLogueado = new Usuario();
                //usuarioLogueado = (Usuario)Session["Usuario_Logueado"];
                
                if (Request.QueryString["IdCliente"] != null) //Click en modificar o eliminar algún cliente del listado
                {
                    CargarLocalidades();
                    long ID = Convert.ToInt64(Request.QueryString["IdCliente"]);
                    BuscarClienteSeleccionado(ID);
                }
            }
        }

        private void CargarLocalidades()
        {
            LocalidadDB locDB = new LocalidadDB();

            List<Localidad> listaLocalidades = new List<Localidad>();
            listaLocalidades = locDB.ListarTodas();

            CargarListaLocalidadesNuevoCliente(listaLocalidades);
            CargarListaLocalidadesModificarCliente(listaLocalidades);
            //CargarListaLocalidades_ABM_Localidades(listaLocalidades);
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
                        clienteSeleccionado.IdLocalidad = cliente.IdLocalidad;
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
                DdlModificarClienteLocalidad.SelectedValue = cliente.IdLocalidad.ToString();
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

            InhabilitarCamposEliminar();
        }

        private void InhabilitarCamposEliminar()
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
                DdlModificarClienteLocalidad.SelectedValue = cliente.IdLocalidad.ToString();
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
            Cliente cliente = new Cliente();

            cliente.CuitDni = TxtNuevoClienteCuitDni.Text;
            cliente.Apenom = TxtNuevoClienteApenom.Text;
            cliente.Direccion = TxtNuevoClienteDireccion.Text;
            cliente.Localidad = DdlNuevoClienteLocalidad.SelectedValue.ToString();
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
                hfError.Value = "Verifique los datos ingresados";
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
            Cliente cliente = new Cliente();

            cliente.ID = Convert.ToInt64(hfIdCliente.Value);
            cliente.CuitDni = TxtModificarClienteCuitDni.Text;
            cliente.Apenom = TxtModificarClienteApenom.Text;
            cliente.Direccion = TxtModificarClienteDireccion.Text;
            cliente.Localidad = DdlModificarClienteLocalidad.SelectedItem.ToString();
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
                        AlternarVisibilidadSections("cancelarModificar");
                        hfMessage.Value = "Cliente '" + cliente.Apenom + "' modificado exitosamente";
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

        private void AlternarVisibilidadSections(string botonOprimido)
        {
            if (botonOprimido == "btnAgregar")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "block");
                section_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
            }

            if (botonOprimido == "btnModificar")
            {
                section_botones_principales.Style.Add("display", "none");
                section_agregar_cliente.Style.Add("display", "none");
                section_modificar_cliente.Style.Add("display", "block");
                section_listado_modificar_cliente.Style.Add("display", "block");
                section_campos_modificar_cliente.Style.Add("display", "none");
                section_localidades.Style.Add("display", "none");
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
                LblModificarClienteTitulo.Text = "Modificar Cliente";
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
                LblModificarClienteTitulo.Text = "Modificar Cliente";
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
            LblModificarClienteTitulo.Text = "Modificar Cliente";
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

        protected void BtnLocalidadesCancelar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelar");
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
                    AlternarVisibilidadSections("cancelarEliminar");
                    hfMessage.Value = "Cliente '" + cliente.Apenom + "' eliminado exitosamente";
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
    }
}