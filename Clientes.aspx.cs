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
            }
        }

        protected void BtnNuevoClienteConfirmar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();

            cliente.CuitDni = TxtNuevoClienteCuitDni.Text;
            cliente.Apenom = TxtNuevoClienteApenom.Text;
            cliente.Direccion = TxtNuevoClienteDireccion.Text;
            cliente.Localidad = DdlNuevoClienteLocalidad.SelectedItem.ToString();
            cliente.Telefono = TxtNuevoClienteTelefono.Text;
            cliente.Mail = TxtNuevoClienteMail.Text;

            //Validar campos cliente
            //Validar existencia en DB
            //Ingresar a DB

            hfMessage.Value = "Cliente '" + cliente.Apenom + "' agregado exitosamente";
        }
    }
}