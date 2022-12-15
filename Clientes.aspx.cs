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

            if (cliente.CuitDni == "") { cliente.CuitDni = "-"; }
            if (cliente.Direccion == "") { cliente.Direccion = "-"; }
            if (cliente.Localidad == "Seleccione") { cliente.Localidad = "-"; }
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
            DdlNuevoClienteLocalidad.SelectedValue = "Seleccione";
            TxtNuevoClienteTelefono.Text = "";
            TxtNuevoClienteMail.Text = "";
        }
    }
}