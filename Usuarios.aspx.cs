using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace CompuGross_Web
{
    public partial class Usuarios : System.Web.UI.Page
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

                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario_Logueado"];
                UsuarioDB uDB = new UsuarioDB();
                usuarioLogueado = uDB.BuscarUsuario(usuarioLogueado);
                if (usuarioLogueado.TipoUsuario.Descripcion != "admin")
                {
                    Session["ErrorTipoUsuario"] = "ERROR\n\nUsted no tiene permiso para ingresar a este sitio";
                    Response.Redirect("Error.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        CargarDesplegablesTiposUsuario();
                    }
                    if (Request.QueryString["IdUsuario"] != null && Session["ModificarEliminar"] != null)
                    {
                        Usuario usuario = new Usuario();
                        usuario.ID = Convert.ToInt64(Request.QueryString["IdUsuario"].ToString());
                        string accionUsuario = Request.QueryString["AccionUsuario"].ToString();
                        CargarCamposModificarEliminar(usuario, accionUsuario);
                    }
                }
            }
        }

        private void CargarCamposModificarEliminar(Usuario usuario, string accionUsuario)
        {
            try
            {
                UsuarioDB uDB = new UsuarioDB();
                usuario = uDB.BuscarUsuario(usuario);

                Session["ABM_Usuarios_UsuarioEncontrado"] = usuario;

                hfIdUsuario.Value = usuario.ID.ToString();
                DdlModificarTiposUsuario.SelectedValue = usuario.TipoUsuario.ID.ToString();
                TxtModificarApellido.Text = usuario.Apellido;
                TxtModificarNombre.Text = usuario.Nombre;
                TxtModificarUsername.Text = usuario.Username;
                TxtModificarMail.Text = usuario.Mail;
                TxtModificarClave.Text = "";

                if (accionUsuario == "CargarCamposModificar")
                {
                    VisibilidadSections("modificar");
                }
                if (accionUsuario == "ConfirmarEliminar")
                {
                    VisibilidadSections("eliminar");
                }
            }
            catch
            {
                hfError.Value = "Se produjo un error al buscar el Usuario en el sistema";
            }
        }

        private void CargarDesplegablesTiposUsuario()
        {
            TipoUsuarioDB tipoUsuarioDB = new TipoUsuarioDB();

            DdlAgregarTiposUsuario.Items.Clear();
            DdlAgregarTiposUsuario.Items.Add("Seleccione...");
            DdlAgregarTiposUsuario.DataSource = tipoUsuarioDB.Listar();
            DdlAgregarTiposUsuario.DataMember = "datos";
            DdlAgregarTiposUsuario.DataTextField = "Descripcion";
            DdlAgregarTiposUsuario.DataValueField = "ID";
            DdlAgregarTiposUsuario.DataBind();

            DdlModificarTiposUsuario.Items.Clear();
            DdlModificarTiposUsuario.Items.Add("Seleccione...");
            DdlModificarTiposUsuario.DataSource = tipoUsuarioDB.Listar();
            DdlModificarTiposUsuario.DataMember = "datos";
            DdlModificarTiposUsuario.DataTextField = "Descripcion";
            DdlModificarTiposUsuario.DataValueField = "ID";
            DdlModificarTiposUsuario.DataBind();
        }

        private void CargarListadoUsuarios()
        {
            UsuarioDB usuarioDB = new UsuarioDB();
            
            RepeaterUsuarios.DataSource = usuarioDB.Listar();
            RepeaterUsuarios.DataBind();

            Session["ModificarEliminar"] = true;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            CargarDesplegablesTiposUsuario();
            VisibilidadSections("agregar");
        }

        protected void BtnListar_Click(object sender, EventArgs e)
        {
            CargarListadoUsuarios();
            VisibilidadSections("listar");
        }

        private void VisibilidadSections(string aux)
        {
            section_botones_principales.Style.Add("display", "none");
            LblTitulo.Text = "Usuarios";

            if (aux == "agregar")
            {
                section_agregar.Style.Add("display", "block");
                section_modificar_eliminar.Style.Add("display", "none");
                section_confirmar_eliminar.Style.Add("display", "none");
                section_listar.Style.Add("display", "none");
                LblTitulo.Text = "Nuevo Usuario";
            }

            if (aux == "listar")
            {
                section_botones_principales.Style.Add("display", "block");
                section_agregar.Style.Add("display", "none");
                section_modificar_eliminar.Style.Add("display", "none");
                section_confirmar_eliminar.Style.Add("display", "none");
                section_listar.Style.Add("display", "block");
            }

            if (aux == "modificar")
            {
                section_agregar.Style.Add("display", "none");
                section_modificar_eliminar.Style.Add("display", "block");
                section_confirmar_eliminar.Style.Add("display", "none");
                section_listar.Style.Add("display", "none");
                LblTitulo.Text = "Modificar Usuario";
                HabilitacionCamposModificarEliminar(true);
                BtnConfirmarModificar.Visible = true;
                BtnCancelarModificar.Visible = true;
            }

            if (aux == "eliminar")
            {
                section_agregar.Style.Add("display", "none");
                section_modificar_eliminar.Style.Add("display", "block");
                section_confirmar_eliminar.Style.Add("display", "block");
                section_listar.Style.Add("display", "none");
                LblTitulo.Text = "Eliminar Usuario";
                HabilitacionCamposModificarEliminar(false);
                BtnConfirmarModificar.Visible = false;
                BtnCancelarModificar.Visible = false;
                LblConfirmarEliminar.Text = "¿Seguro que desea Eliminar al Usuario?";
            }

            if (aux == "cancelar agregar")
            {
                section_botones_principales.Style.Add("display", "block");
                section_agregar.Style.Add("display", "none");
                section_modificar_eliminar.Style.Add("display", "none");
                section_confirmar_eliminar.Style.Add("display", "none");
                section_listar.Style.Add("display", "none");
            }
        }

        private void HabilitacionCamposModificarEliminar(bool aux)
        {
            DdlModificarTiposUsuario.Enabled = aux;
            TxtModificarApellido.Enabled = aux;
            TxtModificarNombre.Enabled = aux;
            TxtModificarUsername.Enabled = aux;
            TxtModificarMail.Enabled = aux;
            TxtModificarClave.Enabled = aux;
        }

        private Usuario CompletarPropiedadesUsuarioModificar()
        {
            Usuario usuario = new Usuario();
            TipoUsuario tipoUsuario = new TipoUsuario();
            tipoUsuario.ID = Convert.ToInt64(DdlModificarTiposUsuario.SelectedValue.ToString());
            tipoUsuario.Descripcion = DdlModificarTiposUsuario.SelectedItem.ToString();
            usuario.ID = Convert.ToInt64(hfIdUsuario.Value);
            usuario.TipoUsuario = tipoUsuario;
            usuario.Apellido = TxtModificarApellido.Text;
            usuario.Nombre = TxtModificarNombre.Text;
            usuario.Username = TxtModificarUsername.Text;
            usuario.Mail = TxtModificarMail.Text;
            usuario.Clave = TxtModificarClave.Text;

            return usuario;
        }

        protected void BtnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["ABM_Usuarios_UsuarioEncontrado"];
                UsuarioDB uDB = new UsuarioDB();
                if (uDB.Eliminar(usuario))
                {
                    BorrarCamposModificarEliminar();
                    hfMessage.Value = "El Usuario " + usuario.Username + " ha sido eliminado exitosamente";
                    CargarListadoUsuarios();
                    Session["ModificarEliminar"] = null;
                    VisibilidadSections("listar");
                }
                else
                {
                    hfError.Value = "No se ha podido eliminar al Usuario, debido a un error";
                }
            }
            catch
            {
                hfError.Value = "No se ha podido eliminar al Usuario, debido a un error";
            }
        }

        protected void BtnCancelarEliminar_Click(object sender, EventArgs e)
        {
            BorrarCamposModificarEliminar();
            CargarListadoUsuarios();
            Session["ModificarEliminar"] = null;
            VisibilidadSections("listar");
        }

        protected void BtnConfirmarModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["ABM_Usuarios_UsuarioEncontrado"];
                Usuario usuarioModificado = new Usuario();
                usuarioModificado = CompletarPropiedadesUsuarioModificar();

                UsuarioDB uDB = new UsuarioDB();
                if (ValidarCamposUsuario(usuarioModificado, "modificar"))
                {
                    if (uDB.Modificar(usuarioModificado))
                    {
                        BorrarCamposModificarEliminar();
                        hfMessage.Value = "El Usuario " + usuarioModificado.Username + " se ha modificado exitosamente";
                        CargarListadoUsuarios();
                        Session["ModificarEliminar"] = null;
                        VisibilidadSections("listar");

                        if (usuarioModificado.TipoUsuario.Descripcion != usuario.TipoUsuario.Descripcion)
                        {
                            Session["Usuario_Logueado"] = usuarioModificado;
                        }
                    }
                    else
                    {
                        hfError.Value = "No se ha podido modificar al Usuario, debido a un error";
                    }
                }
                else
                {
                    hfError.Value = "Hay campos inválidos o sin completar";
                }
            }
            catch
            {
                hfError.Value = "No se ha podido modificar al Usuario, debido a un error";
            }
        }

        protected void BtnCancelarModificar_Click(object sender, EventArgs e)
        {
            BorrarCamposModificarEliminar();
            CargarListadoUsuarios();
            Session["ModificarEliminar"] = null;
            VisibilidadSections("listar");
        }

        private bool ValidarCamposUsuario(Usuario usuario, string accion)
        {
            if (accion == "agregar")
            {
                if (usuario.TipoUsuario.Descripcion != "Seleccione..."
                && usuario.Apellido != "" && usuario.Nombre != ""
                && usuario.Username != "" && usuario.Mail != ""
                && usuario.Mail.Contains("@")
                && usuario.Mail.Contains(".com")
                && !usuario.Mail.Contains("@.com")
                && usuario.Clave != "" && usuario.Clave.Length == 8)
                {
                    return true;
                }

                return false;
            }
            if (accion == "modificar")
            {
                if (usuario.TipoUsuario.Descripcion != "Seleccione..."
                && usuario.Apellido != "" && usuario.Nombre != ""
                && usuario.Username != "" && usuario.Mail != ""
                && usuario.Mail.Contains("@")
                && usuario.Mail.Contains(".com")
                && !usuario.Mail.Contains("@.com"))
                {
                    if (usuario.Clave != "" && usuario.Clave.Length != 8)
                    {
                        return false;
                    }
                    return true;
                }

                return false;
            }

            return false;
        }

        protected void BtnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                TipoUsuario tipoUsuario = new TipoUsuario();
                tipoUsuario.Descripcion = DdlAgregarTiposUsuario.SelectedItem.ToString();
                usuario.TipoUsuario = tipoUsuario;
                usuario.Apellido = TxtAgregarApellido.Text;
                usuario.Nombre = TxtAgregarNombre.Text;
                usuario.Username = TxtAgregarUsername.Text;
                usuario.Mail = TxtAgregarMail.Text;
                usuario.Clave = TxtAgregarClave.Text;

                if (ValidarCamposUsuario(usuario, "agregar"))
                {
                    UsuarioDB uDB = new UsuarioDB();
                    if (uDB.Agregar(usuario))
                    {
                        hfMessage.Value = "El nuevo Usuario ha sido agregado exitosamente";
                        BorrarCamposAgregar();
                        VisibilidadSections("cancelar");
                    }
                    else
                    {
                        hfError.Value = "No se pudo agregar al nuevo Usuario, debido a un error";
                    }
                }
                else
                {
                    hfError.Value = "Hay campos vacíos o inválidos";
                }
            }
            catch
            {
                hfError.Value = "No se pudo agregar al nuevo Usuario, debido a un error";
            }
        }

        protected void BtnCancelarAgregar_Click(object sender, EventArgs e)
        {
            BorrarCamposAgregar();
            VisibilidadSections("cancelar agregar");
        }

        private void BorrarCamposAgregar()
        {
            DdlAgregarTiposUsuario.SelectedValue = "Seleccione...";
            TxtAgregarApellido.Text = "";
            TxtAgregarNombre.Text = "";
            TxtAgregarMail.Text = "";
            TxtAgregarUsername.Text = "";
            TxtAgregarClave.Text = "";
        }

        private void BorrarCamposModificarEliminar()
        {
            DdlModificarTiposUsuario.SelectedValue = "Seleccione...";
            TxtModificarApellido.Text = "";
            TxtModificarNombre.Text = "";
            TxtModificarMail.Text = "";
            TxtModificarUsername.Text = "";
            TxtModificarClave.Text = "";
        }
    }
}