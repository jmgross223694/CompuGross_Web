using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace CompuGross_Web
{
    public partial class Servicios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_Logueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session["listadoItems"] = null;

                if (Session["CancelarModificarEliminar"] != null)
                {
                    ListarServiciosCompleto();
                    AlternarVisibilidadSections("btnListar");
                    Session["CancelarModificarEliminar"] = null;
                }
                else
                {
                    if (Session["ElegirClienteModificarServicio"] != null && Request.QueryString["IdClienteSeleccionado"] != null)
                    {
                        if ((bool)Session["ElegirClienteModificarServicio"])
                        {
                            long ID = Convert.ToInt64(Request.QueryString["IdClienteSeleccionado"]);
                            BuscarClienteSeleccionadoModificar(ID);
                            Session["ElegirClienteModificarServicio"] = false;
                        }
                    }
                    if (Request.QueryString["IdClienteSeleccionado"] != null && Session["ServicioAgregar"] != null)
                    {
                        long ID = Convert.ToInt64(Request.QueryString["IdClienteSeleccionado"]);
                        if (ID.ToString() != "_Page")
                        {
                            BuscarClienteSeleccionadoAgregar(ID);
                        }
                    }
                    if (Request.QueryString["IdServicioSeleccionado"] != null && !IsPostBack)
                    {
                        long ID = Convert.ToInt64(Request.QueryString["IdServicioSeleccionado"]);
                        if (ID.ToString() != "_Page")
                        {
                            string accionServicio = Request.QueryString["AccionServicio"].ToString();
                            BuscarServicioSeleccionado(ID, accionServicio);
                        }
                    }
                }
            }
        }

        private void BuscarServicioSeleccionado(long ID, string accionServicio)
        {
            ServicioDB servicioDB = new ServicioDB();
            Servicio servicioSeleccionado = new Servicio();
            servicioSeleccionado = servicioDB.BuscarPorID(ID);
            Session["ServicioSeleccionado"] = servicioSeleccionado;
            CargarDesplegables();

            if (servicioSeleccionado.ID != 0)
            {
                CargarCamposModificarEliminarServicio(servicioSeleccionado);
                VerificarAccionServicio(accionServicio, servicioSeleccionado);
            }
            
            if (accionServicio == "CargarCamposModificar")
            {
                LblTitulo.Text = "Modificar orden de Servicio N°" + servicioSeleccionado.ID;
            }
            if (accionServicio == "ConfirmarEliminar")
            {
                LblTitulo.Text = "Eliminar orden de Servicio N°" + servicioSeleccionado.ID;

                LblConfirmarEliminar.Text = "¿Seguro que desea Eliminar la orden de Servicio?";
            }
        }

        private void CargarCamposModificarEliminarServicio(Servicio servicio)
        {
            HfIdModificarEliminarServicio.Value = servicio.ID.ToString();

            DdlListarModificarTiposEquipo.SelectedValue = servicio.Equipo.Tipo.ID.ToString();
            TxtListarModificarCliente.Text = servicio.Cliente.Apenom;
            DateTime auxFecha = Convert.ToDateTime(servicio.FechaRecepcion);
            TxtListarModificarFechaRecepcion.Text = auxFecha.ToString("yyyy-MM-dd");

            if (servicio.FechaDevolucion != "")
            {
                auxFecha = Convert.ToDateTime(servicio.FechaDevolucion);
                TxtListarModificarFechaDevolucion.Text = auxFecha.ToString("yyyy-MM-dd");

                TxtListarModificarFechaDevolucion.Visible = true;
                CbListarModificarFechaDevolucion1.Visible = false;
                CbListarModificarFechaDevolucion2.Visible = true;
                CbListarModificarFechaDevolucion2.Checked = true;
            }
            else
            {
                TxtListarModificarFechaDevolucion.Visible = false;
                CbListarModificarFechaDevolucion1.Visible = true;
                CbListarModificarFechaDevolucion2.Visible = false;
            }

            DdlListarModificarTiposServicio.SelectedValue = servicio.TipoServicio.ID.ToString();
            TxtListarModificarMarcaModelo.Text = servicio.Equipo.MarcaModelo;
            TxtListarModificarRam.Text = servicio.Equipo.RAM;
            TxtListarModificarMicroprocesador.Text = servicio.Equipo.Microprocesador;
            TxtListarModificarAlmacenamiento.Text = servicio.Equipo.Almacenamiento;
            TxtListarModificarPlacaMadre.Text = servicio.Equipo.PlacaMadre;
            TxtListarModificarNumSerie.Text = servicio.Equipo.NumSerie;
            TxtListarModificarAdicionales.Text = servicio.Equipo.Adicionales;
            TxtListarModificarAlimentacion.Text = servicio.Equipo.Alimentacion;
            DdlListarModificarUnidadOptica.SelectedValue = servicio.Equipo.UnidadOptica.ID.ToString();
            TxtListarModificarDescripcion.Text = servicio.Descripcion;
            TxtListarModificarCostoRepuestos.Text = servicio.CostoRepuestos;
            TxtListarModificarHonorarios.Text = servicio.Honorarios;
            TxtListarModificarCostoTerceros.Text = servicio.CostoTerceros;
        }

        private void VerificarAccionServicio(string accionServicio, Servicio servicio)
        {
            if (accionServicio == "CargarCamposModificar")
            {
                HabilitacionCamposModificarServicio(true, servicio);
                AlternarVisibilidadSections("modificarServicio");
            }
            if (accionServicio == "ConfirmarEliminar")
            {
                HabilitacionCamposModificarServicio(false, servicio);
                AlternarVisibilidadSections("confirmarEliminarServicio");
            }
        }

        private void BuscarClienteSeleccionadoModificar(long ID)
        {
            try
            {
                ClienteDB clienteDB = new ClienteDB();
                Cliente cliente = new Cliente();
                cliente = clienteDB.BuscarCliente(ID);

                Session["ClienteSeleccionado"] = cliente;

                Servicio servicio = new Servicio();
                servicio = (Servicio)Session["ServicioSeleccionado"];
                servicio.Cliente = cliente;

                Session["ServicioSeleccionado"] = servicio;

                CargarDesplegables();
                CargarCamposModificarEliminarServicio(servicio);

                AlternarVisibilidadSections("ModificarClienteSeleccionado");
            }
            catch
            {
                CargarListadoClientes();
                AlternarVisibilidadSections("modificarElegirCliente");
                Session["ElegirClienteModificarServicio"] = true;
                hfError.Value = "Error al seleccionar Cliente";
            }
        }

        private void BuscarClienteSeleccionadoAgregar(long ID)
        {
            try
            {
                ClienteDB clienteDB = new ClienteDB();
                Cliente cliente = new Cliente();
                cliente = clienteDB.BuscarCliente(ID);
                //hfMessage.Value = "Cliente '" + cliente.Apenom + "' seleccionado correctamente";

                Session["ClienteSeleccionado"] = cliente;

                Servicio servicio = new Servicio();
                servicio = (Servicio)Session["ServicioAgregar"];
                servicio.Cliente = cliente;

                Session["ServicioAgregar"] = null;

                CargarDesplegables();
                CargarCamposAgregarServicio(servicio);

                AlternarVisibilidadSections("AgregarClienteSeleccionado");
            }
            catch
            {
                AlternarVisibilidadSections("AgregarElegirCliente");
                hfError.Value = "Error al seleccionar Cliente";
            }
        }

        private void CargarCamposAgregarServicio(Servicio servicio)
        {
            DdlAgregarTiposEquipo.SelectedValue = servicio.Equipo.Tipo.ID.ToString();
            TxtAgregarCliente.Text = servicio.Cliente.Apenom;
            if (servicio.FechaRecepcion != "")
            {
                DateTime auxFecha = Convert.ToDateTime(servicio.FechaRecepcion);
                TxtAgregarFechaRecepcion.Text = auxFecha.ToString("yyyy-MM-dd");
            }
            if (servicio.FechaDevolucion != "")
            {
                DateTime auxFecha = Convert.ToDateTime(servicio.FechaDevolucion);
                TxtAgregarFechaDevolucion.Text = auxFecha.ToString("yyyy-MM-dd");

                if (Session["AgregarFechaDevolucion"] != null)
                {
                    TxtAgregarFechaDevolucion.Enabled = true;
                    TxtAgregarFechaDevolucion.Visible = true;
                    CbAgregarFechaDevolucion1.Enabled = false;
                    CbAgregarFechaDevolucion1.Visible = false;
                    CbAgregarFechaDevolucion2.Enabled = true;
                    CbAgregarFechaDevolucion2.Visible = true;
                    CbAgregarFechaDevolucion2.Checked = true;

                    Session["AgregarFechaDevolucion"] = null;
                }
            }
            DdlAgregarTiposServicio.SelectedValue = servicio.TipoServicio.ID.ToString();
            TxtAgregarMarcaModelo.Text = servicio.Equipo.MarcaModelo;
            TxtAgregarRam.Text = servicio.Equipo.RAM;
            TxtAgregarMicroprocesador.Text = servicio.Equipo.Microprocesador;
            TxtAgregarAlmacenamiento.Text = servicio.Equipo.Almacenamiento;
            TxtAgregarPlacaMadre.Text = servicio.Equipo.PlacaMadre;
            TxtAgregarNumSerie.Text = servicio.Equipo.NumSerie;
            TxtAgregarAdicionales.Text = servicio.Equipo.Adicionales;
            TxtAgregarAlimentacion.Text = servicio.Equipo.Alimentacion;
            DdlAgregarUnidadOptica.SelectedValue = servicio.Equipo.UnidadOptica.ID.ToString();
            TxtAgregarDescripcion.Text = servicio.Descripcion;
            TxtAgregarCostoRepuestos.Text = servicio.CostoRepuestos;
            TxtAgregarHonorarios.Text = servicio.Honorarios;
            TxtAgregarCostoTerceros.Text = servicio.CostoTerceros;
        }

        private void CargarListadoClientes()
        {
            ClienteDB clienteDB = new ClienteDB();
            List<Cliente> listaClientes = new List<Cliente>();

            listaClientes = clienteDB.ListarTodos();

            RepeaterElegirClientes.DataSource = listaClientes;
            RepeaterElegirClientes.DataBind();
        }

        private void CargarListadoClientesFiltrado(string filtro)
        {
            ClienteDB clienteDB = new ClienteDB();
            List<Cliente> listaClientesFiltrada = new List<Cliente>();

            listaClientesFiltrada = clienteDB.ListarFiltrado(filtro);

            RepeaterElegirClientes.DataSource = listaClientesFiltrada;
            RepeaterElegirClientes.DataBind();
        }

        private void CargarDesplegables()
        {
            CargarDesplegablesTiposEquipo();
            CargarDesplegablesTiposServicio();
            CargarDesplegableUnidadOptica();
        }

        private void CargarDesplegablesTiposEquipo()
        {
            TipoEquipoDB tipoEquipoDB = new TipoEquipoDB();
            List<TipoEquipo> lista = new List<TipoEquipo>();

            try
            {
                lista = tipoEquipoDB.Listar();

                DdlAgregarTiposEquipo.Items.Clear();
                DdlAgregarTiposEquipo.Items.Add("-");
                DdlAgregarTiposEquipo.DataSource = lista;
                DdlAgregarTiposEquipo.DataMember = "datos";
                DdlAgregarTiposEquipo.DataTextField = "Descripcion";
                DdlAgregarTiposEquipo.DataValueField = "ID";
                DdlAgregarTiposEquipo.DataBind();

                DdlListarModificarTiposEquipo.Items.Clear();
                DdlListarModificarTiposEquipo.Items.Add("-");
                DdlListarModificarTiposEquipo.DataSource = lista;
                DdlListarModificarTiposEquipo.DataMember = "datos";
                DdlListarModificarTiposEquipo.DataTextField = "Descripcion";
                DdlListarModificarTiposEquipo.DataValueField = "ID";
                DdlListarModificarTiposEquipo.DataBind();
            }
            catch
            {
                hfError.Value = "Se produjo un error al cargar los Tipos de Equipo";
            }
        }

        private void CargarDesplegablesTiposServicio()
        {
            TipoServicioDB tipoServicioDB = new TipoServicioDB();
            List<TipoServicio> lista = new List<TipoServicio>();

            try
            {
                lista = tipoServicioDB.Listar();

                DdlAgregarTiposServicio.Items.Clear();
                DdlAgregarTiposServicio.DataSource = lista;
                DdlAgregarTiposServicio.DataMember = "datos";
                DdlAgregarTiposServicio.DataTextField = "Descripcion";
                DdlAgregarTiposServicio.DataValueField = "ID";
                DdlAgregarTiposServicio.DataBind();

                DdlListarModificarTiposServicio.Items.Clear();
                DdlListarModificarTiposServicio.DataSource = lista;
                DdlListarModificarTiposServicio.DataMember = "datos";
                DdlListarModificarTiposServicio.DataTextField = "Descripcion";
                DdlListarModificarTiposServicio.DataValueField = "ID";
                DdlListarModificarTiposServicio.DataBind();
            }
            catch
            {
                if (hfError.Value == "")
                {
                    hfError.Value = "Se produjo un error al cargar los Tipos de Servicio";
                }
            }
        }

        private void CargarDesplegableUnidadOptica()
        {
            UnidadOpticaDB unidadOpticaDB = new UnidadOpticaDB();
            List<UnidadOptica> lista = new List<UnidadOptica>();

            try
            {
                lista = unidadOpticaDB.Listar();

                DdlAgregarUnidadOptica.Items.Clear();
                DdlAgregarUnidadOptica.DataSource = lista;
                DdlAgregarUnidadOptica.DataMember = "datos";
                DdlAgregarUnidadOptica.DataTextField = "Descripcion";
                DdlAgregarUnidadOptica.DataValueField = "ID";
                DdlAgregarUnidadOptica.DataBind();

                DdlListarModificarUnidadOptica.Items.Clear();
                DdlListarModificarUnidadOptica.DataSource = lista;
                DdlListarModificarUnidadOptica.DataMember = "datos";
                DdlListarModificarUnidadOptica.DataTextField = "Descripcion";
                DdlListarModificarUnidadOptica.DataValueField = "ID";
                DdlListarModificarUnidadOptica.DataBind();
            }
            catch
            {
                if (hfError.Value == "")
                {
                    hfError.Value = "Se produjo un error al cargar los Tipos de Unidad Optica";
                }
            }
        }

        private void AlternarVisibilidadSections(string aux)
        {
            if (aux == "AgregarElegirCliente")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "block");
                LblTitulo.Text = "Seleccionar Cliente";
            }

            if (aux == "BtnAgregarPrincipal")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "block");
                section_agregar_campos.Style.Add("display", "block");
                section_agregar_campos2.Style.Add("display", "none");
                section_agregar_boton.Style.Add("display", "block");
                BtnConfirmarAgregar.Style.Add("display", "none");
                LblTitulo.Text = "Nuevo Servicio";
            }

            if (aux == "AgregarClienteSeleccionado")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "block");
                section_agregar_campos.Style.Add("display", "block");
                section_agregar_campos2.Style.Add("display", "block");
                section_agregar_boton.Style.Add("display", "block");
                BtnConfirmarAgregar.Style.Add("display", "inline-block");
                section_elegir_cliente.Style.Add("display", "none");
                LblTitulo.Text = "Nuevo Servicio";
            }

            if (aux == "btnCancelarAgregar")
            {
                section_principal.Style.Add("display", "block");
                section_agregar.Style.Add("display", "none");
                section_agregar_campos.Style.Add("display", "none");
                section_agregar_campos2.Style.Add("display", "none");
                section_agregar_boton.Style.Add("display", "none");
                BtnConfirmarAgregar.Style.Add("display", "none");
                LblTitulo.Text = "Servicios";
                ResetearCamposAgregar();
            }

            if (aux == "btnListar")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "none");
                section_listar.Style.Add("display", "block");
                section_listar_lista.Style.Add("display", "block");
                section_listar_modificar.Style.Add("display", "none");
                section_listar_eliminar.Style.Add("display", "none");

                LblTitulo.Text = "Lista de Servicios";
            }

            if (aux == "cancelarListar")
            {
                section_principal.Style.Add("display", "block");
                section_agregar.Style.Add("display", "none");
                section_agregar_campos.Style.Add("display", "none");
                section_agregar_campos2.Style.Add("display", "none");
                section_agregar_boton.Style.Add("display", "none");
                section_listar_lista.Style.Add("display", "none");
                section_listar_modificar.Style.Add("display", "none");
                section_listar_modificar_botones.Style.Add("display", "none");
                section_listar_eliminar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "none");
                BtnConfirmarAgregar.Style.Add("display", "none");
                LblTitulo.Text = "Servicios";
                ResetearCamposAgregar();
            }

            if (aux == "modificarServicio")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "none");
                section_listar.Style.Add("display", "block");
                section_listar_lista.Style.Add("display", "none");
                section_listar_modificar.Style.Add("display", "block");
                section_listar_modificar_botones.Style.Add("display", "block");
                section_listar_eliminar.Style.Add("display", "none");
            }

            if (aux == "modificarElegirCliente")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "none");
                section_listar_lista.Style.Add("display", "none");
                section_listar_modificar.Style.Add("display", "none");
                section_listar_modificar_botones.Style.Add("display", "none");
                section_listar_eliminar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "block");
                LblTitulo.Text = "Seleccionar Cliente";
            }

            if (aux == "confirmarEliminarServicio")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "none");
                section_listar.Style.Add("display", "block");
                section_listar_lista.Style.Add("display", "none");
                section_listar_modificar.Style.Add("display", "grid");
                section_listar_modificar_botones.Style.Add("display", "none");
                section_listar_eliminar.Style.Add("display", "block");
            }

            if (aux == "ModificarClienteSeleccionado")
            {
                section_principal.Style.Add("display", "none");
                section_agregar.Style.Add("display", "none");
                section_elegir_cliente.Style.Add("display", "none");
                section_listar.Style.Add("display", "block");
                section_listar_lista.Style.Add("display", "none");
                section_listar_modificar.Style.Add("display", "block");
                section_listar_modificar_botones.Style.Add("display", "block");
                section_listar_eliminar.Style.Add("display", "none");
                LblTitulo.Text = "Modificar Servicio";
            }
        } //ALTERNAR VISIBILIDAD
        
        private void HabilitacionCamposModificarServicio(bool aux, Servicio servicio)
        {
            if (aux)
            {
                DdlListarModificarTiposEquipo.Enabled = true;
                TxtListarModificarCliente.Enabled = true;
                TxtListarModificarFechaRecepcion.Enabled = true;
                TxtListarModificarFechaDevolucion.Enabled = true;
                CbListarModificarFechaDevolucion1.Enabled = true;
                CbListarModificarFechaDevolucion2.Enabled = true;
                DdlListarModificarTiposServicio.Enabled = true;
                TxtListarModificarMarcaModelo.Enabled = true;
                TxtListarModificarRam.Enabled = true;
                TxtListarModificarMicroprocesador.Enabled = true;
                TxtListarModificarAlmacenamiento.Enabled = true;
                TxtListarModificarPlacaMadre.Enabled = true;
                TxtListarModificarNumSerie.Enabled = true;
                TxtListarModificarAdicionales.Enabled = true;
                TxtListarModificarAlimentacion.Enabled = true;
                DdlListarModificarUnidadOptica.Enabled = true;
                TxtListarModificarDescripcion.Enabled = true;
                TxtListarModificarCostoRepuestos.Enabled = true;
                TxtListarModificarHonorarios.Enabled = true;
                TxtListarModificarCostoTerceros.Enabled = true;
            }
            if (!aux)
            {
                DdlListarModificarTiposEquipo.Enabled = false;
                TxtListarModificarCliente.Enabled = false;
                TxtListarModificarFechaRecepcion.Enabled = false;
                TxtListarModificarFechaDevolucion.Enabled = false;
                CbListarModificarFechaDevolucion1.Enabled = false;
                CbListarModificarFechaDevolucion2.Enabled = false;
                DdlListarModificarTiposServicio.Enabled = false;
                TxtListarModificarMarcaModelo.Enabled = false;
                TxtListarModificarRam.Enabled = false;
                TxtListarModificarMicroprocesador.Enabled = false;
                TxtListarModificarAlmacenamiento.Enabled = false;
                TxtListarModificarPlacaMadre.Enabled = false;
                TxtListarModificarNumSerie.Enabled = false;
                TxtListarModificarAdicionales.Enabled = false;
                TxtListarModificarAlimentacion.Enabled = false;
                DdlListarModificarUnidadOptica.Enabled = false;
                TxtListarModificarDescripcion.Enabled = false;
                TxtListarModificarCostoRepuestos.Enabled = false;
                TxtListarModificarHonorarios.Enabled = false;
                TxtListarModificarCostoTerceros.Enabled = false;
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            CargarDesplegables();
            AlternarVisibilidadSections("BtnAgregarPrincipal");
        }

        protected void BtnListar_Click(object sender, EventArgs e)
        {
            ListarServiciosCompleto();
            AlternarVisibilidadSections("btnListar");
        }

        private void ListarServiciosCompleto()
        {
            try
            {
                ServicioDB servicioDB = new ServicioDB();
                List<Servicio> listaServicios = new List<Servicio>();
                listaServicios = servicioDB.ListarTodos();

                Session["ListaServiciosCompleta"] = listaServicios;

                RepeaterListarServicios.DataSource = listaServicios;
                RepeaterListarServicios.DataBind();

                Session["ListarServicios"] = true;
            }
            catch
            {
                hfError.Value = "Se produjo un error al intentar listar todos los Servicios";
            }
        }

        private void ListarServiciosFiltrados(string filtro)
        {
            try
            {
                ServicioDB servicioDB = new ServicioDB();
                List<Servicio> listaServicios = new List<Servicio>();
                listaServicios = servicioDB.ListarFiltrado(filtro);

                Session["ListaServiciosFiltrada"] = listaServicios;

                RepeaterListarServicios.DataSource = listaServicios;
                RepeaterListarServicios.DataBind();

                Session["ListarServicios"] = true;

                //if (listaServicios.Count() > 0)
                //{
                //    Session["ListaServiciosFiltrada"] = listaServicios;

                //    RepeaterListarServicios.DataSource = listaServicios;
                //    RepeaterListarServicios.DataBind();
                //}
                //else
                //{
                //    hfError.Value = "No se hallaron Servicios que coincidan con su búsqueda";
                //    ListarServiciosCompleto();
                //}
            }
            catch
            {
                hfError.Value = "Se produjo un error al intentar listar todos los Servicios";
            }
        }

        protected void DdlAgregarTiposEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlAgregarTiposEquipo.SelectedItem.ToString() != "-")
            {
                TipoEquipo tipoEquipo = new TipoEquipo();

                tipoEquipo.ID = Convert.ToInt64(DdlAgregarTiposEquipo.SelectedValue);
                tipoEquipo.Descripcion = DdlAgregarTiposEquipo.SelectedItem.ToString();
                tipoEquipo.Estado = true;

                OcultarCamposAgregarServicio();

                if (tipoEquipo.Descripcion == "PC de Escritorio"
                    || tipoEquipo.Descripcion == "All in One"
                    || tipoEquipo.Descripcion == "Notebook"
                    || tipoEquipo.Descripcion == "Netbook")
                { MostrarCamposAgregarComputadora(); }

                else if (tipoEquipo.Descripcion == "Impresora")
                { MostrarCamposAgregarImpresora(); }

                else if (tipoEquipo.Descripcion == "Tablet"
                        || tipoEquipo.Descripcion == "Celular")
                { MostrarCamposAgregarTabletCelular(); }

                else if (tipoEquipo.Descripcion == "Televisor"
                        || tipoEquipo.Descripcion == "Monitor")
                { MostrarCamposAgregarTelevisorMonitor(); }

                else if (tipoEquipo.Descripcion == "Consola")
                { MostrarCamposAgregarConsola(); }

                else if (tipoEquipo.Descripcion == "Joystick")
                { MostrarCamposAgregarJoystick(); }

                else if (tipoEquipo.Descripcion == "Cámaras")
                { MostrarCamposAgregarCamaras(); DdlAgregarTiposServicio.SelectedValue = "2"; }

                else if (tipoEquipo.Descripcion == "Unidad de Almacenamiento")
                { MostrarCamposAgregarUnidadDeAlmacenamiento(); }

                if (CbAgregarFechaDevolucion1.Checked && !TxtAgregarFechaDevolucion.Visible)
                {
                    CbAgregarFechaDevolucion1.Checked = false;
                }

                section_agregar_campos2.Style.Add("display", "block");
                BtnConfirmarAgregar.Style.Add("display", "inline-block");
            }
            else
            {
                section_agregar_campos2.Style.Add("display", "none");
                BtnConfirmarAgregar.Style.Add("display", "none");
            }
        }

        private void OcultarCamposAgregarServicio()
        {
            LblAgregarMarcaModelo.Visible = false;
            LblAgregarRam.Visible = false;
            LblAgregarMicroprocesador.Visible = false;
            LblAgregarAlmacenamiento.Visible = false;
            LblAgregarPlacaMadre.Visible = false;
            LblAgregarNumSerie.Visible = false;
            LblAgregarAdicionales.Visible = false;
            LblAgregarAlimentacion.Visible = false;
            LblAgregarUnidadOptica.Visible = false;

            TxtAgregarMarcaModelo.Visible = false;
            TxtAgregarRam.Visible = false;
            TxtAgregarMicroprocesador.Visible = false;
            TxtAgregarAlmacenamiento.Visible = false;
            TxtAgregarPlacaMadre.Visible = false;
            TxtAgregarNumSerie.Visible = false;
            TxtAgregarAdicionales.Visible = false;
            TxtAgregarAlimentacion.Visible = false;
            DdlAgregarUnidadOptica.Visible = false;
        }

        private void MostrarCamposAgregarServicio()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarRam.Visible = true;
            LblAgregarMicroprocesador.Visible = true;
            LblAgregarAlmacenamiento.Visible = true;
            LblAgregarPlacaMadre.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;
            LblAgregarUnidadOptica.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarRam.Visible = true;
            TxtAgregarMicroprocesador.Visible = true;
            TxtAgregarAlmacenamiento.Visible = true;
            TxtAgregarPlacaMadre.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
            DdlAgregarUnidadOptica.Visible = true;
        }

        private void MostrarCamposAgregarUnidadDeAlmacenamiento()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarAlmacenamiento.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarAlmacenamiento.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
        }

        private void MostrarCamposAgregarComputadora()
        {
            MostrarCamposAgregarServicio();
        }

        private void MostrarCamposAgregarImpresora()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
        }

        private void MostrarCamposAgregarTabletCelular()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarRam.Visible = true;
            LblAgregarAlmacenamiento.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarRam.Visible = true;
            TxtAgregarAlmacenamiento.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
        }

        private void MostrarCamposAgregarTelevisorMonitor()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
        }

        private void MostrarCamposAgregarConsola()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarAlmacenamiento.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarAlmacenamiento.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
        }

        private void MostrarCamposAgregarJoystick()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
        }

        private void MostrarCamposAgregarCamaras()
        {
            LblAgregarMarcaModelo.Visible = true;
            LblAgregarNumSerie.Visible = true;
            LblAgregarAdicionales.Visible = true;
            LblAgregarAlimentacion.Visible = true;

            TxtAgregarMarcaModelo.Visible = true;
            TxtAgregarNumSerie.Visible = true;
            TxtAgregarAdicionales.Visible = true;
            TxtAgregarAlimentacion.Visible = true;
        }

        private void OcultarCamposListarModificarServicio()
        {
            LblListarModificarMarcaModelo.Visible = false;
            LblListarModificarRam.Visible = false;
            LblListarModificarMicroprocesador.Visible = false;
            LblListarModificarAlmacenamiento.Visible = false;
            LblListarModificarPlacaMadre.Visible = false;
            LblListarModificarNumSerie.Visible = false;
            LblListarModificarAdicionales.Visible = false;
            LblListarModificarAlimentacion.Visible = false;
            LblListarModificarUnidadOptica.Visible = false;

            TxtListarModificarMarcaModelo.Visible = false;
            TxtListarModificarRam.Visible = false;
            TxtListarModificarMicroprocesador.Visible = false;
            TxtListarModificarAlmacenamiento.Visible = false;
            TxtListarModificarPlacaMadre.Visible = false;
            TxtListarModificarNumSerie.Visible = false;
            TxtListarModificarAdicionales.Visible = false;
            TxtListarModificarAlimentacion.Visible = false;
            DdlListarModificarUnidadOptica.Visible = false;
        }

        private void MostrarCamposListarModificarServicio()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarRam.Visible = true;
            LblListarModificarMicroprocesador.Visible = true;
            LblListarModificarAlmacenamiento.Visible = true;
            LblListarModificarPlacaMadre.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;
            LblListarModificarUnidadOptica.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarRam.Visible = true;
            TxtListarModificarMicroprocesador.Visible = true;
            TxtListarModificarAlmacenamiento.Visible = true;
            TxtListarModificarPlacaMadre.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
            DdlListarModificarUnidadOptica.Visible = true;
        }

        private void MostrarCamposListarModificarComputadora()
        {
            MostrarCamposListarModificarServicio();
        }

        private void MostrarCamposListarModificarImpresora()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
        }

        private void MostrarCamposListarModificarTabletCelular()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarRam.Visible = true;
            LblListarModificarAlmacenamiento.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarRam.Visible = true;
            TxtListarModificarAlmacenamiento.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
        }

        private void MostrarCamposListarModificarTelevisorMonitor()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
        }

        private void MostrarCamposListarModificarConsola()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarAlmacenamiento.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarAlmacenamiento.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
        }

        private void MostrarCamposListarModificarJoystick()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
        }

        private void MostrarCamposListarModificarCamaras()
        {
            LblListarModificarMarcaModelo.Visible = true;
            LblListarModificarNumSerie.Visible = true;
            LblListarModificarAdicionales.Visible = true;
            LblListarModificarAlimentacion.Visible = true;

            TxtListarModificarMarcaModelo.Visible = true;
            TxtListarModificarNumSerie.Visible = true;
            TxtListarModificarAdicionales.Visible = true;
            TxtListarModificarAlimentacion.Visible = true;
        }

        protected void BtnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Servicio servicio = new Servicio();
                servicio = CompletarPropiedadesAgregarServicio();
                if (ValidarCamposServicio(servicio))
                {
                    try
                    {
                        ServicioDB servicioDB = new ServicioDB();
                        if (servicioDB.Agregar(servicio))
                        {
                            hfMessage.Value = "El nuevo Servicio ha sido agregado exitosamente";
                            //ResetearCamposAgregar();
                            AlternarVisibilidadSections("btnCancelarAgregar");
                        }
                        else
                        {
                            hfError.Value = "No se pudo agregar el nuevo Servicio";
                        }
                    }
                    catch
                    {
                        hfError.Value = "No se pudo agregar el nuevo Servicio";
                    }
                }
                else
                {
                    hfError.Value = "Hay datos obligatorios inválidos o sin completar";
                }
            }
            catch
            {
                hfError.Value = "Hay datos obligatorios inválidos o sin completar";
            }
        }

        private bool ValidarCamposServicio(Servicio servicio)
        {
            if (servicio.FechaRecepcion != null && servicio.Cliente != null 
                && servicio.TipoServicio != null && servicio.Equipo != null)
            {
                if (ValidarFechaRecepcion(servicio.FechaRecepcion)
                && ValidarCliente(servicio.Cliente)
                && ValidarTipoServicio(servicio)
                && ValidarTipoEquipo(servicio)
                && ValidarMarcaModelo(servicio.Equipo.MarcaModelo)
                && ValidarDescripcion(servicio.Descripcion)
                && ValidarHonorarios(servicio.Honorarios))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            return false;
        }

        private bool ValidarTipoEquipo(Servicio servicio)
        {
            if (servicio.Equipo.Tipo.Descripcion != "" && servicio.Equipo.Tipo.Descripcion != "-")
            {
                return true;
            }

            return false;
        }

        private bool ValidarTipoServicio(Servicio servicio)
        {
            if (servicio.TipoServicio.Descripcion != "" && servicio.TipoServicio.Descripcion != "-")
            {
                if (servicio.TipoServicio.Descripcion == "Armado de gabinete" && servicio.Equipo.Tipo.Descripcion != "PC de Escritorio")
                {
                    return false;
                }
                if (servicio.TipoServicio.Descripcion == "Cámara" && servicio.Equipo.Tipo.Descripcion != "Cámaras")
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private bool ValidarMarcaModelo(string marcaModelo)
        {
            if (marcaModelo != "")
            {
                return true;
            }

            return false;
        }

        private bool ValidarDescripcion(string descripcion)
        {
            if (descripcion != "")
            {
                return true;
            }

            return false;
        }

        private bool ValidarHonorarios(string honorarios)
        {
            if (honorarios != "" && Convert.ToInt32(honorarios) >= 0)
            {
                return true;
            }

            return false;
        }

        private bool ValidarCliente(Cliente cliente)
        {
            if (cliente.Apenom != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidarFechaRecepcion(string fechaRecepcion)
        {
            DateTime fechaRecep = Convert.ToDateTime(fechaRecepcion);

            if (fechaRecep.Date <= DateTime.Now.Date)
            {
                return true;
            }

            return false;
        }

        protected void BtnCancelarAgregar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("btnCancelarAgregar");
        }

        private void ResetearCamposAgregar()
        {
            DdlAgregarTiposEquipo.SelectedValue = "-";
            TxtAgregarCliente.Text = "";
            TxtAgregarFechaRecepcion.Text = "";
            TxtAgregarFechaDevolucion.Text = "";
            DdlAgregarTiposServicio.SelectedValue = "3";
            TxtAgregarMarcaModelo.Text = "";
            TxtAgregarRam.Text = "";
            TxtAgregarMicroprocesador.Text = "";
            TxtAgregarAlmacenamiento.Text = "";
            TxtAgregarPlacaMadre.Text = "";
            TxtAgregarNumSerie.Text = "";
            TxtAgregarAdicionales.Text = "";
            TxtAgregarAlimentacion.Text = "";
            DdlAgregarUnidadOptica.SelectedValue = "1";
            TxtAgregarDescripcion.Text = "";
            TxtAgregarCostoRepuestos.Text = "";
            TxtAgregarHonorarios.Text = "";
            TxtAgregarCostoTerceros.Text = "";

        }

        protected void LblAgregarCliente_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();
            servicio = CompletarPropiedadesAgregarServicio();
            Session["ServicioAgregar"] = servicio;

            if (TxtAgregarFechaDevolucion.Visible)
            {
                Session["AgregarFechaDevolucion"] = true;
            }

            AlternarVisibilidadSections("AgregarElegirCliente");
            CargarListadoClientes();
        }

        private Servicio CompletarPropiedadesAgregarServicio()
        {
            Cliente cliente = new Cliente();
            Equipo equipo = new Equipo();
            UnidadOptica unidadOptica = new UnidadOptica();
            TipoEquipo tipoEquipo = new TipoEquipo();
            TipoServicio tipoServicio = new TipoServicio();
            Servicio servicio = new Servicio();

            if (TxtAgregarFechaRecepcion.Text != "")
            {
                DateTime fechaRecepcion = Convert.ToDateTime(TxtAgregarFechaRecepcion.Text);
                servicio.FechaRecepcion = fechaRecepcion.Day + "/" + fechaRecepcion.Month + "/" + fechaRecepcion.Year;
            }
            else
            {
                servicio.FechaRecepcion = "";
            }
            if (TxtAgregarFechaDevolucion.Visible && TxtAgregarFechaDevolucion.Text != "")
            {
                DateTime fechaDevolucion = Convert.ToDateTime(TxtAgregarFechaDevolucion.Text);
                servicio.FechaDevolucion = fechaDevolucion.Day + "/" + fechaDevolucion.Month + "/" + fechaDevolucion.Year;
            }
            else
            {
                servicio.FechaDevolucion = "";
            }

            if (Session["ClienteSeleccionado"] != null)
            {
                cliente = (Cliente)Session["ClienteSeleccionado"];
            }
            else
            {
                cliente.Apenom = TxtAgregarCliente.Text;
            }

            servicio.Cliente = cliente;

            tipoEquipo.ID = Convert.ToInt64(DdlAgregarTiposEquipo.SelectedValue);
            tipoEquipo.Descripcion = DdlAgregarTiposEquipo.SelectedItem.ToString();
            tipoEquipo.Estado = true;

            equipo.Tipo = tipoEquipo;
            equipo.MarcaModelo = TxtAgregarMarcaModelo.Text;
            equipo.RAM = TxtAgregarRam.Text;
            equipo.Microprocesador = TxtAgregarMicroprocesador.Text;
            equipo.Almacenamiento = TxtAgregarAlmacenamiento.Text;
            equipo.PlacaMadre = TxtAgregarPlacaMadre.Text;
            equipo.NumSerie = TxtAgregarNumSerie.Text;
            equipo.Adicionales = TxtAgregarAdicionales.Text;
            equipo.Alimentacion = TxtAgregarAlimentacion.Text;

            unidadOptica.ID = Convert.ToInt64(DdlAgregarUnidadOptica.SelectedValue.ToString());
            unidadOptica.Descripcion = DdlAgregarUnidadOptica.SelectedItem.ToString();
            unidadOptica.Estado = true;

            equipo.UnidadOptica = unidadOptica;

            servicio.Equipo = equipo;

            tipoServicio.ID = Convert.ToInt64(DdlAgregarTiposServicio.SelectedValue.ToString());
            tipoServicio.Descripcion = DdlAgregarTiposServicio.SelectedItem.ToString();
            tipoServicio.Estado = true;

            servicio.TipoServicio = tipoServicio;

            servicio.Descripcion = TxtAgregarDescripcion.Text;
            servicio.CostoRepuestos = TxtAgregarCostoRepuestos.Text;
            servicio.Honorarios = TxtAgregarHonorarios.Text;
            servicio.CostoTerceros = TxtAgregarCostoTerceros.Text;

            return servicio;
        }

        protected void CbAgregarFechaDevolucion_CheckedChanged(object sender, EventArgs e)
        {
            if (!TxtAgregarFechaDevolucion.Visible)
            {
                TxtAgregarFechaDevolucion.Enabled = true;
                TxtAgregarFechaDevolucion.Visible = true;
                CbAgregarFechaDevolucion1.Enabled = false;
                CbAgregarFechaDevolucion1.Visible = false;
                CbAgregarFechaDevolucion2.Enabled = true;
                CbAgregarFechaDevolucion2.Visible = true;
                CbAgregarFechaDevolucion2.Checked = true;

            }
            else
            {
                TxtAgregarFechaDevolucion.Enabled = false;
                TxtAgregarFechaDevolucion.Visible = false;
                CbAgregarFechaDevolucion1.Enabled = true;
                CbAgregarFechaDevolucion1.Visible = true;
                CbAgregarFechaDevolucion1.Checked = false;
                CbAgregarFechaDevolucion2.Enabled = false;
                CbAgregarFechaDevolucion2.Visible = false;
            }
        }

        protected void BtnElegirClienteBusqueda_Click(object sender, EventArgs e)
        {
            string filtro = TxtElegirClienteBusqueda.Text;

            if (filtro != "")
            {
                CargarListadoClientesFiltrado(filtro);
            }
            else
            {
                CargarListadoClientes();
            }
        }

        protected void DdlAgregarTiposServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoServicioSeleccionado = DdlAgregarTiposServicio.SelectedItem.ToString();

            if (tipoServicioSeleccionado == "Armado de gabinete")
            {
                DdlAgregarTiposEquipo.SelectedValue = "1";
                OcultarCamposAgregarServicio();
                MostrarCamposAgregarComputadora();
            }

            if (tipoServicioSeleccionado == "Cámaras")
            {
                DdlAgregarTiposEquipo.SelectedValue = "13";
                OcultarCamposAgregarServicio();
                MostrarCamposAgregarCamaras();
            }
        }

        protected void BtnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();
            servicio = CompletarPropiedadesModificarServicio();

            try
            {
                if (ValidarCamposServicio(servicio))
                {
                    ServicioDB servicioDB = new ServicioDB();
                    if (servicioDB.Eliminar(servicio))
                    {
                        ResetearCamposModificarEliminar();
                        Session["ElegirClienteModificarServicio"] = null;
                        Session["CancelarModificar"] = true;
                        ListarServiciosCompleto();
                        AlternarVisibilidadSections("btnListar");

                        hfMessage.Value = "El Servicio N°" + servicio.ID + " se ha eliminado del sistema";
                    }
                    else
                    {
                        hfError.Value = ("No se ha podido eliminar el Servicio N°" + servicio.ID);
                    }
                }
            }
            catch
            {
                hfError.Value = ("No se ha podido eliminar el Servicio N°" + servicio.ID);
            }
        }

        protected void BtnCancelarEliminar_Click(object sender, EventArgs e)
        {
            ResetearCamposModificarEliminar();
            Session["ElegirClienteModificarServicio"] = null;
            Session["ServicioSeleccionado"] = null;
            Session["CancelarModificar"] = true;
            ListarServiciosCompleto();
            AlternarVisibilidadSections("btnListar");
        }

        protected void BtnConfirmarModificar_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();
            servicio = CompletarPropiedadesModificarServicio();

            if (ValidarCamposServicio(servicio))
            {
                try
                {
                    ServicioDB servicioDB = new ServicioDB();
                    if (servicioDB.Modificar(servicio))
                    {
                        ResetearCamposModificarEliminar();
                        Session["ElegirClienteModificarServicio"] = null;
                        Session["CancelarModificar"] = true;
                        ListarServiciosCompleto();
                        AlternarVisibilidadSections("btnListar");

                        hfMessage.Value = "El Servicio N°" + servicio.ID + " se ha modificado exitosamente";
                    }
                    else
                    {
                        hfError.Value = ("No se han podido guardar los cambios para el Servicio N°" + servicio.ID);
                    }
                }
                catch
                {
                    hfError.Value = ("No se han podido guardar los cambios para el Servicio N°" + servicio.ID);
                }
            }
            else
            {
                Session["ServicioSeleccionado"] = servicio;

                hfError.Value = ("Hay campos con valores inválidos o campos obligatorios sin completar");
            }
        }

        protected void BtnCancelarModificar_Click(object sender, EventArgs e)
        {
            ResetearCamposModificarEliminar();
            Session["ElegirClienteModificarServicio"] = null;
            Session["ServicioSeleccionado"] = null;
            Session["CancelarModificar"] = true;
            ListarServiciosCompleto();
            AlternarVisibilidadSections("btnListar");
        }

        private Servicio CompletarPropiedadesModificarServicio()
        {
            Cliente cliente = new Cliente();
            Equipo equipo = new Equipo();
            UnidadOptica unidadOptica = new UnidadOptica();
            TipoEquipo tipoEquipo = new TipoEquipo();
            TipoServicio tipoServicio = new TipoServicio();
            Servicio servicio = new Servicio();

            try
            {
                servicio.ID = Convert.ToInt64(HfIdModificarEliminarServicio.Value);

                if (TxtListarModificarFechaRecepcion.Text != "")
                {
                    DateTime fechaRecepcion = Convert.ToDateTime(TxtListarModificarFechaRecepcion.Text);
                    servicio.FechaRecepcion = fechaRecepcion.Day + "/" + fechaRecepcion.Month + "/" + fechaRecepcion.Year;
                }
                else
                {
                    servicio.FechaRecepcion = "";
                }
                if (TxtListarModificarFechaDevolucion.Visible)
                {
                    DateTime fechaDevolucion = Convert.ToDateTime(TxtListarModificarFechaDevolucion.Text);
                    servicio.FechaDevolucion = fechaDevolucion.Day + "/" + fechaDevolucion.Month + "/" + fechaDevolucion.Year;
                }
                else
                {
                    servicio.FechaDevolucion = "";
                }

                if (Session["ClienteSeleccionado"] != null)
                {
                    cliente = (Cliente)Session["ClienteSeleccionado"];
                }
                else
                {
                    cliente.Apenom = TxtListarModificarCliente.Text;
                }

                servicio.Cliente = cliente;

                tipoEquipo.ID = Convert.ToInt64(DdlListarModificarTiposEquipo.SelectedValue);
                tipoEquipo.Descripcion = DdlListarModificarTiposEquipo.SelectedItem.ToString();
                tipoEquipo.Estado = true;

                equipo.Tipo = tipoEquipo;
                equipo.MarcaModelo = TxtListarModificarMarcaModelo.Text;
                equipo.RAM = TxtListarModificarRam.Text;
                equipo.Microprocesador = TxtListarModificarMicroprocesador.Text;
                equipo.Almacenamiento = TxtListarModificarAlmacenamiento.Text;
                equipo.PlacaMadre = TxtListarModificarPlacaMadre.Text;
                equipo.NumSerie = TxtListarModificarNumSerie.Text;
                equipo.Adicionales = TxtListarModificarAdicionales.Text;
                equipo.Alimentacion = TxtListarModificarAlimentacion.Text;

                unidadOptica.ID = Convert.ToInt64(DdlListarModificarUnidadOptica.SelectedValue.ToString());
                unidadOptica.Descripcion = DdlListarModificarUnidadOptica.SelectedItem.ToString();
                unidadOptica.Estado = true;

                equipo.UnidadOptica = unidadOptica;

                servicio.Equipo = equipo;

                tipoServicio.ID = Convert.ToInt64(DdlListarModificarTiposServicio.SelectedValue.ToString());
                tipoServicio.Descripcion = DdlListarModificarTiposServicio.SelectedItem.ToString();
                tipoServicio.Estado = true;

                servicio.TipoServicio = tipoServicio;

                servicio.Descripcion = TxtListarModificarDescripcion.Text;
                servicio.CostoRepuestos = TxtListarModificarCostoRepuestos.Text;
                servicio.Honorarios = TxtListarModificarHonorarios.Text;
                servicio.CostoTerceros = TxtListarModificarCostoTerceros.Text;
            }
            catch {}

            return servicio;
        }

        private void ResetearCamposModificarEliminar()
        {
            DdlListarModificarTiposEquipo.SelectedValue = "-";
            TxtListarModificarCliente.Text = "";
            TxtListarModificarFechaRecepcion.Text = "";
            TxtListarModificarFechaDevolucion.Text = "";
            DdlListarModificarTiposServicio.SelectedValue = "3";
            TxtListarModificarMarcaModelo.Text = "";
            TxtListarModificarRam.Text = "";
            TxtListarModificarMicroprocesador.Text = "";
            TxtListarModificarAlmacenamiento.Text = "";
            TxtListarModificarPlacaMadre.Text = "";
            TxtListarModificarNumSerie.Text = "";
            TxtListarModificarAdicionales.Text = "";
            TxtListarModificarAlimentacion.Text = "";
            DdlListarModificarUnidadOptica.SelectedValue = "1";
            TxtListarModificarDescripcion.Text = "";
            TxtListarModificarCostoRepuestos.Text = "";
            TxtListarModificarHonorarios.Text = "";
            TxtListarModificarCostoTerceros.Text = "";
        }

        protected void LblListarModificarCliente_Click(object sender, EventArgs e)
        {
            CargarListadoClientes();
            AlternarVisibilidadSections("modificarElegirCliente");
            Session["ElegirClienteModificarServicio"] = true;
        }

        protected void CbListarModificarFechaDevolucion_CheckedChanged(object sender, EventArgs e)
        {
            if (CbListarModificarFechaDevolucion1.Checked)
            {
                TxtListarModificarFechaDevolucion.Enabled = true;
                TxtListarModificarFechaDevolucion.Visible = true;
                CbListarModificarFechaDevolucion1.Enabled = false;
                CbListarModificarFechaDevolucion1.Visible = false;
                CbListarModificarFechaDevolucion1.Checked = false;
                CbListarModificarFechaDevolucion2.Enabled = true;
                CbListarModificarFechaDevolucion2.Visible = true;
                CbListarModificarFechaDevolucion2.Checked = true;
            }
            else
            {
                TxtListarModificarFechaDevolucion.Enabled = false;
                TxtListarModificarFechaDevolucion.Visible = false;
                CbListarModificarFechaDevolucion1.Enabled = true;
                CbListarModificarFechaDevolucion1.Visible = true;
                CbListarModificarFechaDevolucion1.Checked = false;
                CbListarModificarFechaDevolucion2.Enabled = false;
                CbListarModificarFechaDevolucion2.Visible = false;
                CbListarModificarFechaDevolucion2.Checked = false;
            }

            if (Request.QueryString["AccionServicio"] != null)
            {
                string accionServicio = Convert.ToString(Request.QueryString["AccionServicio"]);

                if (accionServicio == "ConfirmarEliminar")
                {
                    TxtListarModificarFechaDevolucion.Enabled = false;
                    CbListarModificarFechaDevolucion1.Enabled = false;
                    CbListarModificarFechaDevolucion2.Enabled = false;
                }
            }
        }

        protected void DdlListarModificarTiposServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoServicioSeleccionado = DdlListarModificarTiposServicio.SelectedItem.ToString();

            if (tipoServicioSeleccionado == "Armado de gabinete")
            {
                DdlListarModificarTiposEquipo.SelectedValue = "1";
                OcultarCamposListarModificarServicio();
                MostrarCamposListarModificarComputadora();
            }

            if (tipoServicioSeleccionado == "Cámaras")
            {
                DdlListarModificarTiposEquipo.SelectedValue = "13";
                OcultarCamposListarModificarServicio();
                MostrarCamposListarModificarCamaras();
            }
        }

        protected void DdlListarModificarTiposEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoEquipo tipoEquipo = new TipoEquipo();

            tipoEquipo.ID = Convert.ToInt64(DdlListarModificarTiposEquipo.SelectedValue);
            tipoEquipo.Descripcion = DdlListarModificarTiposEquipo.SelectedItem.ToString();
            tipoEquipo.Estado = true;

            OcultarCamposListarModificarServicio();

            if (tipoEquipo.Descripcion == "PC de Escritorio"
                || tipoEquipo.Descripcion == "All in One"
                || tipoEquipo.Descripcion == "Notebook"
                || tipoEquipo.Descripcion == "Netbook")
            { MostrarCamposListarModificarComputadora(); }

            else if (tipoEquipo.Descripcion == "Impresora")
            { MostrarCamposListarModificarImpresora(); }

            else if (tipoEquipo.Descripcion == "Tablet"
                    || tipoEquipo.Descripcion == "Celular")
            { MostrarCamposListarModificarTabletCelular(); }

            else if (tipoEquipo.Descripcion == "Televisor"
                    || tipoEquipo.Descripcion == "Monitor")
            { MostrarCamposListarModificarTelevisorMonitor(); }

            else if (tipoEquipo.Descripcion == "Consola")
            { MostrarCamposListarModificarConsola(); }

            else if (tipoEquipo.Descripcion == "Joystick")
            { MostrarCamposListarModificarJoystick(); }

            else if (tipoEquipo.Descripcion == "Cámaras")
            { MostrarCamposListarModificarCamaras(); DdlListarModificarTiposServicio.SelectedValue = "2"; }
        }

        protected void BtnListarBuscar_Click(object sender, EventArgs e)
        {
            string filtro = TxtListarBuscar.Text;

            if (filtro != "")
            {
                ListarServiciosFiltrados(filtro);
            }
            else
            {
                ListarServiciosCompleto();
            }

            AlternarVisibilidadSections("btnListar");
        }

        protected void BtnListarCancelar_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadSections("cancelarListar");
        }

        protected void BtnInformarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                Servicio servicioSeleccionado = new Servicio();
                servicioSeleccionado = (Servicio)Session["ServicioSeleccionado"];
                ClienteDB clienteDB = new ClienteDB();
                servicioSeleccionado.Cliente = clienteDB.BuscarCliente(servicioSeleccionado.Cliente.ID);

                if (servicioSeleccionado.Cliente.Mail != null)
                {
                    if (servicioSeleccionado.Cliente.Mail.Contains("@"))
                    {
                        if (EnviarMailInformarCliente(servicioSeleccionado))
                        {
                            hfMessage.Value = "Se ha enviado el Mail, con la información del Servicio N°" + servicioSeleccionado.ID + ", al Cliente " + servicioSeleccionado.Cliente.Apenom;
                        }
                        else
                        {
                            hfError.Value = "Se ha producido un error al intentar enviar el Mail al Cliente";
                        }
                    }
                    else
                    {
                        hfError.Value = "El Cliente " + servicioSeleccionado.Cliente.Apenom + " no tiene un Mail registrado";
                    }
                }
                else
                {
                    hfError.Value = "Se ha producido un error al intentar enviar el Mail al Cliente";
                }
            }
            catch
            {
                hfError.Value = "Se ha producido un error al intentar enviar el Mail al Cliente";
            }
        }

        private string CargarAsuntoMailInformarCliente(Servicio servicio)
        {
            return "COMPUGROSS - ORDEN DE SERVICIO N°" + servicio.ID;
        }

        private string CargarCuerpoMailInformarCliente(Servicio servicio)
        {
            decimal costoTotalServicio = Convert.ToDecimal(servicio.CostoRepuestos) + Convert.ToDecimal(servicio.CostoTerceros) + Convert.ToDecimal(servicio.Honorarios);

            string cuerpo = "Esperamos se encuentre muy bien Sr/a " + servicio.Cliente.Apenom + ".\n\n" +
                            "A continuación le acercamos los datos actualizados de su orden de servicio N°" + servicio.ID + " realizada con nosotros:\n\n\n" +
                            "- Fecha de recepción de equipo: " + servicio.FechaRecepcion + "\n\n" +
                            "- Fecha de devolución de equipo: " + servicio.FechaDevolucion + "\n\n" +
                            "- Equipo: " + servicio.Equipo.Tipo.Descripcion + " " + servicio.Equipo.MarcaModelo + "\n\n" +
                            "- Detalles de servicio: " + servicio.Descripcion + "\n\n" +
                            "- Costo total del servicio: $" + costoTotalServicio.ToString() +
                            "\n\n\nSaludos cordiales.\n\nCompuGross";

            if (servicio.TipoServicio.Descripcion != "Servicio técnico")
            {
                cuerpo = "Esperamos se encuentre muy bien Sr/a " + servicio.Cliente.Apenom + ".\n\n" +
                         "A continuación le acercamos los datos actualizados de su orden de servicio N°" + servicio.ID + " realizada con nosotros:\n\n\n" +
                         "- Fecha de ejecución del servicio: " + servicio.FechaDevolucion + "\n\n" +
                         "- Equipo: " + servicio.Equipo.Tipo.Descripcion + " " + servicio.Equipo.MarcaModelo + "\n\n" +
                         "- Detalles de servicio: " + servicio.Descripcion + "\n\n" +
                         "- Costo total del servicio: $" + costoTotalServicio.ToString() +
                         "\n\n\nSaludos cordiales.\n\nCompuGross";
            }

            return cuerpo;
        }

        private bool EnviarMailInformarCliente(Servicio servicio)
        {
            try
            {
                EmailService mail = new EmailService();
                mail.armarCorreo(servicio.Cliente.Mail, CargarAsuntoMailInformarCliente(servicio), CargarCuerpoMailInformarCliente(servicio));
                mail.enviarEmail();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}