using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Negocio
{
    public class NegocioMessenger
    {
        bool estado = true;
        string merror;
        public void SenderEmail(string sender, string name, string pass, List<string> destinatarios, string subject, string mensaje)
        {
            try
            {
                MailMessage correo = new MailMessage();
                SmtpClient protocolo = new SmtpClient();
                correo.From = new MailAddress(sender, name, Encoding.UTF8);
                foreach(string item in destinatarios)
                {
                    correo.To.Add(item);
                }
                correo.Subject = subject;
                correo.SubjectEncoding = Encoding.UTF8;
                correo.Body = mensaje;
                correo.BodyEncoding = Encoding.UTF8;
                protocolo.Credentials = new NetworkCredential(sender,pass);
                protocolo.Port = 587;
                protocolo.Host = "smtp.gmail.com";
                protocolo.EnableSsl = true;
                protocolo.Send(correo);
            }
            catch (SmtpException ex)
            {
                estado = false;
                merror = ex.Message.ToString();
            }
        }
        public bool Estado
        {
            get { return estado; }
        }
        public string LogError
        {
            get { return merror; }
        }
    }
}
