using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompuGross_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkLabelRecuperarClave_Click(object sender, EventArgs e)
        {
            section_login.Style.Add("display", "none");
            section_recuperar_clave.Style.Add("display", "block");
            TxtUsuario.Text = "";
            TxtClave.Text = "";
            TxtRecuperarClave.Text = "";
            TxtValidarCodigo.Text = "";
            TxtCambiarClave.Text = "";
        }

        protected void BtnCancelarRecuperarClave_Click(object sender, EventArgs e)
        {
            section_login.Style.Add("display", "block");
            section_recuperar_clave.Style.Add("display", "none");
            section_validar_codigo.Style.Add("display", "none");
            section_validar_codigo.Style.Add("display", "none");
            TxtRecuperarClave.Text = "";
            TxtValidarCodigo.Text = "";
            TxtCambiarClave.Text = "";
        }

        protected void BtnEnviarCodigo_Click(object sender, EventArgs e)
        {
            string usuario = TxtRecuperarClave.Text;

            if (usuario != "")
            {
                //comprobar usuario y buscar mail
                //enviar codigo al mail
                section_recuperar_clave.Style.Add("display", "none");
                TxtRecuperarClave.Text = "";
                TxtValidarCodigo.Text = "";
                TxtCambiarClave.Text = "";
                section_validar_codigo.Style.Add("display", "block");
                //Codigo enviado exitosamente
            }
            else
            {
                //usuario vacio
            }
        }

        protected void BtnValidarCodigo_Click(object sender, EventArgs e)
        {
            string codigoMail = TxtValidarCodigo.Text;

            if (codigoMail != "")
            {
                //Validar Codigo
                section_validar_codigo.Style.Add("display", "none");
                TxtRecuperarClave.Text = "";
                TxtValidarCodigo.Text = "";
                TxtCambiarClave.Text = "";
                section_cambiar_clave.Style.Add("display", "block");
                //Codigo validado exitosamente
            }
            else
            {
                //codigoMail vacio
            }
        }

        protected void BtnCambiarClave_Click(object sender, EventArgs e)
        {
            string claveNueva = TxtCambiarClave.Text;

            if (claveNueva != "")
            {
                //validar clave nueva
                //actualizar clave en DB
                section_cambiar_clave.Style.Add("display", "none");
                TxtRecuperarClave.Text = "";
                TxtValidarCodigo.Text = "";
                TxtCambiarClave.Text = "";
                section_login.Style.Add("display", "block");
                //Clave actualizada exitosamente
            }
            else
            {
                //claveNueva vacio
            }
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = TxtUsuario.Text,
                   clave = TxtClave.Text;

            if (usuario != "" && clave != "")
            {
                //validar en DB
                //si esta OK permitir ingreso
                //Si no indicar con cartel
            }
            else
            {
                //usuario y/o clave vacios
            }
        }
    }
}