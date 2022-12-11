using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioDB
    {
        ConexionDB conDB = new ConexionDB();
        EmailService envioMail = new EmailService();

        public Usuario ValidarCredenciales(Usuario usuario)
        {
            string consulta = "select (select TU.Tipo from TiposUsuario TU where ID = IdTipo) Tipo, " +
                    "Nombre, Apellido, Count(*) as Cantidad " +
                    "from Usuarios where Username = '" + usuario.Username + "' " +
                    "and PWDCOMPARE('" + usuario.Clave + "', Clave)=1 " +
                    "group by IdTipo, Nombre, Apellido";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    int cantidad = Convert.ToInt32(conDB.Lector["Cantidad"]);
                    
                    if (cantidad == 1)
                    {
                        usuario.Tipo = conDB.Lector["Tipo"].ToString();
                        usuario.Nombre = conDB.Lector["Nombre"].ToString();
                        usuario.Apellido = conDB.Lector["Apellido"].ToString();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return usuario;
        }

        public bool ValidarUsuario(Usuario usuario)
        {
            bool validado = false;
            int cantidad = 0;

            string consulta = "select Count(*) Cantidad from Usuarios where Username = '" + usuario.Username + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    cantidad = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (cantidad == 1)
                    {
                        validado = true;
                    }
                    else
                    {
                        validado = false;
                    }
                }
                else
                {
                    validado = false;
                }
            }
            catch (Exception)
            {
                validado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return validado;
        }

        public string BuscarMail(Usuario usuario)
        {
            string mail = "";

            string consulta = "select Mail from Usuarios where Username = '" + usuario.Username + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    mail = conDB.Lector["Mail"].ToString();
                }
                else
                {
                    mail = "-1";
                }
            }
            catch (Exception)
            {
                mail = "-1";
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return mail;
        }

        public bool AgregarCodigoRecuperarClave(Usuario usuario)
        {
            bool codigoAgregado = false;

            string updateCodigo = "update Usuarios set CodigoRecuperarClave = '" + usuario.Codigo_Recuperar_Clave + 
                                  "' where Username = '" + usuario.Username + "'";

            try
            {
                conDB.SetearConsulta(updateCodigo);
                conDB.EjecutarConsulta();
                codigoAgregado = true;
            }
            catch
            {
                codigoAgregado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return codigoAgregado;
        }

        public bool EnviarCodigoMail(Usuario usuario)
        {
            bool mailEnviado = false;

            string asunto = "COMPUGROSS - RECUPERAR CONTRASEÑA (no-reply)",
                   cuerpo = "Este mail ha sido enviado debido a que solicitaste un cambio de clave.\n\n" +
                            "Si no has sido tú, ponte en contacto con nosotros de manera inmediata.\n\n" +
                            "Debes ingresar este código para poder crear una nueva clave: '" + 
                            usuario.Codigo_Recuperar_Clave + "'\n\n\n" +
                            "Saludos cordiales.\n\nCompuGross";

            try
            {
                envioMail.armarCorreo(usuario.Mail, asunto, cuerpo);
                mailEnviado = envioMail.enviarEmail();
            }
            catch
            {
                mailEnviado = false;
            }

            return mailEnviado;
        }

        public bool ValidarCodigoMail(Usuario usuario)
        {
            bool validado = false;

            string consulta = "select CodigoRecuperarClave Codigo from Usuarios where " +
                              "Username = '" + usuario.Username + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    string CodigoRecuperarClaveDB = conDB.Lector["Codigo"].ToString();

                    if (CodigoRecuperarClaveDB == usuario.Codigo_Recuperar_Clave)
                    {
                        validado = true;
                    }
                    else
                    {
                        validado = false;
                    }    
                }
            }
            catch
            {
                validado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return validado;
        }

        public bool ActualizarClaveNueva(Usuario usuario)
        {
            bool claveActualizada = false;

            string updateClave = "update Usuarios set CodigoRecuperarClave = 0, " +
                        "Clave = PWDENCRYPT('" + usuario.Clave + "') where Username = '" + usuario.Username + "'";

            try
            {
                conDB.SetearConsulta(updateClave);
                conDB.EjecutarConsulta();

                claveActualizada = true;
            }
            catch
            {
                claveActualizada = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return claveActualizada;
        }
    }
}
