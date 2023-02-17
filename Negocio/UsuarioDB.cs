using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
            string consulta = "select ID, (select TU.Tipo from TiposUsuario TU where ID = IdTipo) Tipo, " +
                    "Nombre, Apellido, Count(*) as Cantidad " +
                    "from Usuarios where Username = '" + usuario.Username + "' " +
                    "and PWDCOMPARE('" + usuario.Clave + "', Clave)=1 " +
                    "group by ID, IdTipo, Nombre, Apellido";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    int cantidad = Convert.ToInt32(conDB.Lector["Cantidad"]);
                    
                    if (cantidad == 1)
                    {
                        TipoUsuario tipoUsuario = new TipoUsuario();

                        usuario.ID = Convert.ToInt64(conDB.Lector["ID"]);
                        tipoUsuario.Descripcion = conDB.Lector["Tipo"].ToString();
                        usuario.TipoUsuario = tipoUsuario;
                        usuario.Nombre = conDB.Lector["Nombre"].ToString();
                        usuario.Apellido = conDB.Lector["Apellido"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return usuario;
        }

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            string consulta = "select * from ExportUsuarios";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                while (conDB.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    TipoUsuario tipoUsuario = new TipoUsuario();

                    usuario.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    usuario.Apellido = conDB.Lector["Apellido"].ToString();
                    usuario.Nombre = conDB.Lector["Nombre"].ToString();
                    usuario.Username = conDB.Lector["Username"].ToString();
                    usuario.Mail = conDB.Lector["Mail"].ToString();
                    tipoUsuario.ID = Convert.ToInt64(conDB.Lector["IdTipo"]);
                    tipoUsuario.Descripcion = conDB.Lector["Tipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;

                    lista.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return lista;
        }

        public Usuario BuscarUsuario(Usuario usuario)
        {
            string consulta = "select * from ExportUsuarios where ID = " + usuario.ID;

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    TipoUsuario tipoUsuario = new TipoUsuario();

                    usuario.ID = Convert.ToInt64(conDB.Lector["ID"]);
                    usuario.Apellido = conDB.Lector["Apellido"].ToString();
                    usuario.Nombre = conDB.Lector["Nombre"].ToString();
                    usuario.Username = conDB.Lector["Username"].ToString();
                    usuario.Mail = conDB.Lector["Mail"].ToString();
                    tipoUsuario.ID = Convert.ToInt64(conDB.Lector["IdTipo"]);
                    tipoUsuario.Descripcion = conDB.Lector["Tipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return usuario;
        }

        public bool Agregar(Usuario usuario)
        {
            bool resultado = false;
            string insert = "insert into Usuarios(IdTipo, Apellido, Nombre, Username, Mail, Clave)" +
                            "values((select ID from TiposUsuario where Tipo = '" + usuario.TipoUsuario.Descripcion + "'), '" +
                            usuario.Apellido + "', '" + usuario.Nombre + "', '" + usuario.Username + "', '" + 
                            usuario.Mail + "', pwdencrypt('" + usuario.Clave + "'))";

            try
            {
                conDB.SetearConsulta(insert);
                conDB.EjecutarConsulta();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
        }

        public bool Modificar(Usuario usuario)
        {
            bool resultado = false;
            string update = "update Usuarios set IdTipo = " + usuario.TipoUsuario.ID + ", " +
                                                "Apellido = '" + usuario.Apellido + "', " +
                                                "Nombre = '" + usuario.Nombre + "', " +
                                                "Username = '" + usuario.Username + "', " +
                                                "Mail = '" + usuario.Mail + "'";

            if (usuario.Clave != "")
            {
                update = "update Usuarios set IdTipo = " + usuario.TipoUsuario.ID + ", " +
                                             "Apellido = '" + usuario.Apellido + "', " +
                                             "Nombre = '" + usuario.Nombre + "', " +
                                             "Username = '" + usuario.Username + "', " +
                                             "Mail = '" + usuario.Mail + "', " +
                                             "Clave = PWDENCRYPT('" + usuario.Clave + "')";
            }

            try
            {
                conDB.SetearConsulta(update);
                conDB.EjecutarConsulta();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
        }

        public bool Eliminar(Usuario usuario)
        {
            bool resultado = false;
            string delete = "delete from Usuarios where ID = " + usuario.ID;

            try
            {
                conDB.SetearConsulta(delete);
                conDB.EjecutarConsulta();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
        }

        public bool VerificarExistenciaUsuario_RestaurarBackup(Usuario usuario)
        {
            bool resultado = false;
            int usuarioEncontrado = 0;

            string consulta = "select count(*) Cantidad from Usuarios where Username = '" + usuario.Username + 
                              "' and Apellido = '" + usuario.Apellido + "', and Nombre = '" + usuario.Nombre + "'";

            try
            {
                conDB.SetearConsulta(consulta);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read())
                {
                    usuarioEncontrado = Convert.ToInt32(conDB.Lector["Cantidad"]);

                    if (usuarioEncontrado == 1)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                conDB.CerrarConexion();
            }

            return resultado;
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
