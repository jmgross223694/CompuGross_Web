using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using System.Net;

namespace CompuGross_Web
{
    public partial class Precios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_Logueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Request.QueryString["IdPrecio"] != null && Session["ModificarEliminarPrecio"] != null)
                {
                    try
                    {
                        PrecioDB precioDB = new PrecioDB();
                        Precio precio = new Precio();
                        precio = precioDB.BuscarPorID(Convert.ToInt64(Request.QueryString["IdPrecio"]));
                        Modificar_Eliminar_Precio(precio, Request.QueryString["AccionPrecio"].ToString());
                    }
                    catch
                    {
                        hfError.Value = "Error al cargar los datos del Precio seleccionado";
                    }
                }
            }
        }

        private void CargarListado()
        {
            PrecioDB precioDB = new PrecioDB();
            List<Precio> lista = new List<Precio>();
            lista = precioDB.Listar();
            string precioDolar = TxtPrecioDolar.Text;

            Session["listadoPrecios"] = lista;

            if (precioDolar != "")
            {
                precioDolar = precioDolar.Replace(".", ",");

                foreach (Precio precio in lista)
                {
                    precio.Pesos = precio.Dolares * Convert.ToDecimal(precioDolar);
                }
            }
            else
            {
                foreach (Precio precio in lista)
                {
                    precio.Pesos = 0;
                }
            }

            section_listado.Style.Add("display", "block");
            section_precio_dolar.Style.Add("display", "block");
            section_agregar.Style.Add("display", "none");
            section_modificar.Style.Add("display", "none");
            section_confirmar_eliminar.Style.Add("display", "none");

            Session["ModificarEliminarPrecio"] = true;

            LblTitulo.Text = "Listado de Precios";

            RepeaterPrecios.DataSource = lista;
            RepeaterPrecios.DataBind();
        }

        protected void BtnCargarPrecios_Click(object sender, EventArgs e)
        {
            if (TxtPrecioDolar.Text != "")
            {
                try
                {
                    CargarListado();
                }
                catch
                {
                    hfError.Value = "No se pudieron cargar los precios de la DB";
                }
            }
            else
            {
                hfError.Value = "Por favor ingrese el precio del dolar";
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            //TxtPrecioDolar.Text = "";
            LblTitulo.Text = "Nuevo Precio";
            ResetearCamposAgregar();
            section_listado.Style.Add("display", "none");
            section_precio_dolar.Style.Add("display", "none");
            section_agregar.Style.Add("display", "block");
            section_modificar.Style.Add("display", "none");
            section_confirmar_eliminar.Style.Add("display", "none");
            TxtAgregarCodigo.Focus();
        }

        private void Modificar_Eliminar_Precio(Precio precio, string accion)
        {
            //TxtPrecioDolar.Text = "";
            VisibilidadPanelesModificarEliminar(accion);
            CargarCamposModificarEliminar(precio);
        }

        private void CargarCamposModificarEliminar(Precio precio)
        {
            HfIdPrecio.Value = precio.ID.ToString();
            TxtModificarCodigo.Text = precio.Codigo;
            TxtModificarDescripcion.Text = precio.Descripcion;
            TxtModificarAclaraciones.Text = precio.Aclaraciones;
            TxtModificarPrecio.Text = precio.Dolares.ToString();
        }

        private void VisibilidadPanelesModificarEliminar (string accion)
        {
            ResetearCamposModificar();
            section_precio_dolar.Style.Add("display", "none");
            section_listado.Style.Add("display", "none");
            section_agregar.Style.Add("display", "none");
            section_modificar.Style.Add("display", "block");

            if (accion == "Modificar")
            {
                section_confirmar_eliminar.Style.Add("display", "none");
                HabilitarCamposModificar(true);
                LblTitulo.Text = "Modificar Precio";
                TxtModificarCodigo.Focus();
            }
            if (accion == "Eliminar")
            {
                section_confirmar_eliminar.Style.Add("display", "block");
                HabilitarCamposModificar(false);
                LblTitulo.Text = "Eliminar Precio";
                BtnCancelarEliminar.Focus();
            }
        }

        private void HabilitarCamposModificar(bool accion)
        {
            TxtModificarCodigo.Enabled = accion;
            TxtModificarDescripcion.Enabled = accion;
            TxtModificarAclaraciones.Enabled = accion;
            TxtModificarPrecio.Enabled = accion;
            BtnConfirmarModificar.Enabled = accion;
            BtnConfirmarModificar.Visible = accion;
            BtnCancelarModificar.Enabled = accion;
            BtnCancelarModificar.Visible = accion;
        }

        private void ResetearCamposAgregar()
        {
            TxtAgregarCodigo.Text = "";
            TxtAgregarDescripcion.Text = "";
            TxtAgregarAclaraciones.Text = "";
            TxtAgregarPrecio.Text = "";
        }

        private void ResetearCamposModificar()
        {
            HfIdPrecio.Value = "";
            TxtModificarCodigo.Text = "";
            TxtModificarDescripcion.Text = "";
            TxtModificarAclaraciones.Text = "";
            TxtModificarPrecio.Text = "";
        }

        private void VisibilidadSections()
        {
            section_precio_dolar.Style.Add("display", "block");
            section_listado.Style.Add("display", "none");
            section_agregar.Style.Add("display", "none");
            section_modificar.Style.Add("display", "none");
            section_confirmar_eliminar.Style.Add("display", "none");
            LblTitulo.Text = "Listado de Precios";
        }

        protected void BtnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposAgregar())
            {
                try
                {
                    PrecioDB precioDB = new PrecioDB();
                    if (precioDB.Agregar(CargarCamposAgregarPrecio()))
                    {
                        hfMessage.Value = "Precio Agregado exitosamente";
                        ResetearCamposAgregar();
                        VisibilidadSections();
                    }
                    else
                    {
                        hfError.Value = "Se produjo un Error y no se pudo agregar el Precio ingresado";
                    }
                }
                catch
                {
                    hfError.Value = "Se produjo un Error y no se pudo agregar el Precio ingresado";
                }
            }
            else
            {
                hfError.Value = "Hay campos obligatorios que están vacíos";
            }
        }

        protected void BtnCancelarAgregar_Click(object sender, EventArgs e)
        {
            ResetearCamposAgregar();
            VisibilidadSections();
        }

        private Precio CargarCamposAgregarPrecio()
        {
            Precio precio = new Precio();
            precio.Codigo = TxtAgregarCodigo.Text;
            precio.Descripcion = TxtAgregarDescripcion.Text;
            precio.Aclaraciones = TxtAgregarAclaraciones.Text;
            precio.Dolares = Convert.ToDecimal(TxtAgregarPrecio.Text.Replace(".", ","));

            return precio;
        }

        private bool ValidarCamposAgregar()
        {
            if (TxtAgregarCodigo.Text != "" && TxtAgregarDescripcion.Text != "" && TxtAgregarPrecio.Text != "")
            {
                return true;
            }

            return false;
        }

        protected void BtnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposModificar())
            {
                try
                {
                    PrecioDB precioDB = new PrecioDB();
                    if (precioDB.Eliminar(CargarCamposPrecioModificado()))
                    {
                        hfMessage.Value = "Precio Eliminado exitosamente";
                        ResetearCamposModificar();
                        VisibilidadSections();
                        Session["ModificarEliminarPrecio"] = null;
                    }
                    else
                    {
                        hfError.Value = "Se produjo un Error y no se eliminó el Precio";
                    }
                }
                catch
                {
                    hfError.Value = "Se produjo un Error y no se eliminó el Precio";
                }
            }
            else
            {
                hfError.Value = "Hay campos obligatorios que están vacíos";
            }
        }

        protected void BtnCancelarEliminar_Click(object sender, EventArgs e)
        {
            ResetearCamposModificar();
            VisibilidadSections();
            Session["ModificarEliminarPrecio"] = null;
        }

        protected void BtnConfirmarModificar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposModificar())
            {
                try
                {
                    PrecioDB precioDB = new PrecioDB();
                    if (precioDB.Modificar(CargarCamposPrecioModificado()))
                    {
                        hfMessage.Value = "Precio Modificado exitosamente";
                        ResetearCamposModificar();
                        VisibilidadSections();
                        Session["ModificarEliminarPrecio"] = null;
                    }
                    else
                    {
                        hfError.Value = "Se produjo un Error y no se pudo modificar el Precio";
                    }
                }
                catch
                {
                    hfError.Value = "Se produjo un Error y no se pudo modificar el Precio";
                }
            }
            else
            {
                hfError.Value = "Hay campos obligatorios que están vacíos";
            }
        }

        protected void BtnCancelarModificar_Click(object sender, EventArgs e)
        {
            ResetearCamposModificar();
            VisibilidadSections();
            Session["ModificarEliminarPrecio"] = null;
        }

        private Precio CargarCamposPrecioModificado()
        {
            Precio precio = new Precio();
            precio.ID = Convert.ToInt64(HfIdPrecio.Value);
            precio.Codigo = TxtModificarCodigo.Text;
            precio.Descripcion = TxtModificarDescripcion.Text;
            precio.Aclaraciones = TxtModificarAclaraciones.Text;
            precio.Dolares = Convert.ToDecimal(TxtModificarPrecio.Text.Replace(".", ","));

            return precio;
        }

        private bool ValidarCamposModificar()
        {
            if (HfIdPrecio.Value != "" && TxtModificarCodigo.Text != "" && TxtModificarDescripcion.Text != "" && TxtModificarPrecio.Text != "")
            {
                return true;
            }

            return false;
        }
    }
}