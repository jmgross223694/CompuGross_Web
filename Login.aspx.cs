using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace CompuGross_Web
{
    public partial class Login : System.Web.UI.Page
    {
        UsuarioDB uDB = new UsuarioDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            TxtUsuario.Text = "38346656";
            TxtClave.Text = "Admin123";
            BtnIngresar.Focus();
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

            Session["Usuario_Recuperar_Clave"] = null;
        }

        //Comienzo BtnEnviarCodigo_Click
        protected void BtnEnviarCodigo_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Username = TxtRecuperarClave.Text;

            if (usuario.Username != "")
            {
                if (ValidarUsuario(usuario))
                {
                    if (EnviarCodigoRecuperarClave(BuscarMailUsuario(usuario)))
                    {
                        section_recuperar_clave.Style.Add("display", "none");
                        TxtRecuperarClave.Text = "";
                        TxtValidarCodigo.Text = "";
                        TxtCambiarClave.Text = "";
                        section_validar_codigo.Style.Add("display", "block");

                        hfMessage.Value = "Se ha enviado un código a su mail y deberá ingresarlo en el próximo paso";
                    }
                    else
                    {
                        hfError.Value = "No se pudo enviar el código a su mail";
                    }
                }
                else
                {
                    hfError.Value = "El usuario " + usuario.Username;
                }
            }
            else
            {
                hfError.Value = "Usuario vacío";
            }
        }

        private bool ValidarUsuario(Usuario usuario)
        {
            bool validado = false;

            validado = uDB.ValidarUsuario(usuario);

            return validado;
        }

        private Usuario BuscarMailUsuario(Usuario usuario)
        {
            usuario.Mail = uDB.BuscarMail(usuario);

            return usuario;
        }

        private int GenerarNumeroRandom()
        {
            Random numRandom = new Random();

            int random = numRandom.Next(100000, 999999);

            if (random < 0) { random = random * (-1); }

            return random;
        }

        private Usuario GenerarCodigoRecuperarClave(Usuario usuario)
        {
            usuario.Codigo_Recuperar_Clave = GenerarNumeroRandom().ToString();

            uDB.AgregarCodigoRecuperarClave(usuario);

            return usuario;
        }

        private bool EnviarCodigoRecuperarClave(Usuario usuario)
        {
            usuario = GenerarCodigoRecuperarClave(usuario);

            Session["Usuario_Recuperar_Clave"] = usuario;

            return uDB.EnviarCodigoMail(usuario);
        }
        //Fin BtnEnviarCodigo_Click

        //Comienzo BtnValidarCodigo_Click
        protected void BtnValidarCodigo_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            if (Session["Usuario_Recuperar_Clave"] != null)
            {
                usuario = (Usuario)Session["Usuario_Recuperar_Clave"];
                usuario.Codigo_Recuperar_Clave = TxtValidarCodigo.Text;

                if (usuario.Codigo_Recuperar_Clave != "")
                {
                    if (uDB.ValidarCodigoMail(usuario))
                    {
                        section_validar_codigo.Style.Add("display", "none");
                        TxtRecuperarClave.Text = "";
                        TxtValidarCodigo.Text = "";
                        TxtCambiarClave.Text = "";
                        section_cambiar_clave.Style.Add("display", "block");
                        hfMessage.Value = "Código validado exitosamente. En el próximo paso deberá ingresar " +
                                          "una nueva Contraseña";
                    }
                    else
                    {
                        hfError.Value = "El código ingresado no coincide con el que enviamos a su casilla de mail";
                    }
                }
                else
                {
                    hfError.Value = "No ha ingresado ningún código";
                }
            }
            else
            {
                hfError.Value = "No se puede validar el código en este momento, reintente más tarde";
            }
        }
        //Fin BtnValidarCodigo_Click

        //Comienzo BtnCambiarClave_Click
        protected void BtnCambiarClave_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            if (Session["Usuario_Recuperar_Clave"] != null)
            {
                usuario = (Usuario)Session["Usuario_Recuperar_Clave"];
                usuario.Clave = TxtCambiarClave.Text;

                if (usuario.Clave != "")
                {
                    if (ValidarClaveNueva(usuario.Clave))
                    {
                        if (uDB.ActualizarClaveNueva(usuario))
                        {
                            section_cambiar_clave.Style.Add("display", "none");
                            TxtRecuperarClave.Text = "";
                            TxtValidarCodigo.Text = "";
                            TxtCambiarClave.Text = "";

                            Session["Usuario_Recuperar_Clave"] = null;

                            section_login.Style.Add("display", "block");
                            hfMessage.Value = "Su contraseña ha sido actualizada correctamente";
                        }
                        else
                        {
                            hfError.Value = "Se produjo un error al intentar actualizar su contraseña. Reintente más tarde";
                        }
                    }
                    else
                    {
                        hfError.Value = "La contraseña ingresada no cumple con las condiciones impuestas";
                    }
                }
                else
                {
                    hfError.Value = "No ha ingresado ninguna contraseña";
                }
            }
            else
            {
                hfError.Value = "No se puede actualizar la contraseña en este momento, reintente más tarde";
            }
        }

        private bool ValidarClaveNueva(string claveNueva)
        {
            bool validado = false;
            int len = claveNueva.Length;

            bool mayuscula = validarMayusculaClave(claveNueva),
                     minuscula = validarMinusculaClave(claveNueva),
                     numero = validarNumeroClave(claveNueva);

            if (mayuscula && minuscula && numero && len == 8) { validado = true; }

            return validado;
        }

        private bool validarMinusculaClave(string clave)
        {
            bool resultado = false;
            string claveMinuscula = clave.ToLower();
            int minuscula = 0;

            for (int i = 0; i < clave.Length; i++)
            {
                if (!char.IsDigit(clave[i]) && clave[i] == claveMinuscula[i])
                {
                    minuscula++;
                }
            }

            if (minuscula > 0) { resultado = true; }

            return resultado;
        }

        private bool validarMayusculaClave(string clave)
        {
            bool resultado = false;
            string claveMayuscula = clave.ToUpper();
            int mayuscula = 0;

            for (int i = 0; i < clave.Length; i++)
            {
                if (!char.IsDigit(clave[i]) && clave[i] == claveMayuscula[i])
                {
                    mayuscula++;
                }
            }

            if (mayuscula > 0) { resultado = true; }

            return resultado;
        }

        private bool validarNumeroClave(string clave)
        {
            bool resultado = false;
            int numero = 0;

            for (int i = 0; i < clave.Length; i++)
            {
                if (char.IsNumber(clave, i))
                {
                    numero++;
                }
            }

            if (numero > 0) { resultado = true; }

            return resultado;
        }

        //Fin BtnCambiarClave_Click

        //Comienzo BtnIngresar_Click
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Username = TxtUsuario.Text;
            usuario.Clave = TxtClave.Text;

            if (usuario.Username != "" && usuario.Clave != "")
            {
                if (!ValidarCredenciales(usuario))
                {
                    hfError.Value = "Credenciales inválidas";
                }
                else
                {
                    Response.Redirect("Clientes.aspx");
                }
            }
            else
            {
                if (usuario.Username == "")
                {
                    hfError.Value = "Usuario vacío";
                }
                else if (usuario.Clave == "")
                {
                    hfError.Value = "Contraseña vacía";
                }
            }
        }

        private bool ValidarCredenciales(Usuario usuario)
        {
            bool validado = false;

            Usuario usuario_aux = uDB.ValidarCredenciales(usuario);

            if (usuario_aux.Tipo != null && usuario_aux.Nombre != null && usuario_aux.Apellido != null)
            {
                Session["Usuario_Logueado"] = usuario_aux;
                validado = true;                
            }

            return validado;
        }
        //Fin BtnIngresar_Click
    }
}