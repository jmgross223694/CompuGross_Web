using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace CompuGross_Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_Logueado"] != null)
            {
                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario_Logueado"];
                LblUsuarioLogueado.Text = "Usuario actual: " + usuarioLogueado.Nombre + " " 
                                          + usuarioLogueado.Apellido 
                                          + " (" + usuarioLogueado.Tipo + ")";
            }
        }
    }
}