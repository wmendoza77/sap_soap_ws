using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;


namespace Finilager.General.Librerias.CodigoUsuario
{
    public class cuCorreo
    {
        public string NombreMostrar { get; set; }
        public string EmailRemite { get; set; }
        public string Clave { get; set; }
        public string ServidorSalida { get; set; }
        public int PuertoSalida { get; set; }
        public bool ConSSL { get; set; }
        public string Asunto { get; set; }

        public string EmailDestino { get; set; }
        public string NomDestinatario { get; set; }

        static bool mailSent = false;

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;
            Exception ex = new Exception();
            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
                Log.grabar(ex);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
                Log.grabar(e.Error);
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        public void EnviarMail(string algo = "")
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                MailAddress from = new MailAddress(EmailRemite, NombreMostrar, System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(EmailDestino);
                MailMessage mensaje = new MailMessage(from, to);
                mensaje.IsBodyHtml = true;
                mensaje.Body = "MENSAJE DE PRUEBA DE MANERA ASÍNCRONA " + algo;

                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.Subject = Asunto;
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                //message.Attachments.Add(new Attachment(AdjuntoPDFStream(NroPedido.ToString(), nombre), "Prueba.pdf"));

                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                smtp.Host = ServidorSalida;
                smtp.Port = PuertoSalida;
                smtp.EnableSsl = ConSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(EmailRemite, Clave);
                //smtp.Send(mensaje);
                smtp.SendAsync(mensaje,"Ricardo");
                mensaje.Dispose();

            }
            catch (SmtpException ex)
            {
                Log.grabar(ex);
            }
            catch (Exception ex)
            {
                Log.grabar(ex);
            }
        }

        public void cargarDatos()
        {
            NombreMostrar = ConfigurationManager.AppSettings["NombreMostrar"];
            EmailRemite = ConfigurationManager.AppSettings["EmailRemite"];
            Clave = ConfigurationManager.AppSettings["Clave"];
            ServidorSalida = ConfigurationManager.AppSettings["ServidorSalida"];
            PuertoSalida = int.Parse(ConfigurationManager.AppSettings["PuertoSalida"]);
            ConSSL = bool.Parse( ConfigurationManager.AppSettings["ConSSL"]);
            Asunto = ConfigurationManager.AppSettings["Asunto"];
            /*EmailDestino = ConfigurationManager.AppSettings["EmailDestino"];
            NomDestinatario = ConfigurationManager.AppSettings["NomDestinatario"];*/
        }
    }
}
