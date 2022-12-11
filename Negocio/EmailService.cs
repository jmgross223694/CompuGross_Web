using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            ConexionDB conDB = new ConexionDB();

            string selectCredencialesMail = "select mail, pass from credencialesMail";

            string user = "";
            string pass = "";

            try
            {
                conDB.SetearConsulta(selectCredencialesMail);
                conDB.EjecutarConsulta();

                if (conDB.Lector.Read() == true)
                {
                    user = conDB.Lector["mail"].ToString();
                    pass = conDB.Lector["pass"].ToString();
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            try
            {
                server = new SmtpClient();
                server.Credentials = new NetworkCredential(user, pass);
                server.EnableSsl = true;
                server.Port = 587;
                server.Host = "smtp.gmail.com";
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }

        public void armarCorreo(string mailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@compugrossok.com", "CompuGross");
            email.To.Add(mailDestino);
            email.Subject = asunto;
            email.Body = cuerpo;
        }

        public bool enviarEmail()
        {
            bool mailEnviado = false;

            try
            {
                server.Send(email);
                mailEnviado = true;
            }
            catch
            {
                mailEnviado = false;
            }

            return mailEnviado;
        }
    }
}
