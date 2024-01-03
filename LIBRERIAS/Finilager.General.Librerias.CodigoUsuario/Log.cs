using System;
using System.Web;
using System.Configuration;
using Finilager.General.Librerias.EntidadesNegocio;

using System.IO;

namespace Finilager.General.Librerias.CodigoUsuario
{
    public class Log
    {
        
        public static void grabar(Exception ex)
        {
            enLog oenLog=new enLog();
            oenLog.FechaHora=DateTime.Now;
            oenLog.TipoError = ex.GetType().ToString();
            /*enLogin oenLogin=(enLogin)HttpContext.Current.Session["Cliente"];
            oenLog.Aplicacion = oenLogin.Aplicacion;
            oenLog.Usuario = oenLogin.Usuario;*/
            oenLog.MensajeError = ex.Message;
            oenLog.DetalleError = ex.StackTrace;
            string rutaLog = ConfigurationManager.AppSettings["rutaLog"];
            //string rutaLog = Server.MapPath("~/ARCHIVOS/L");
            DateTime fecha = DateTime.Now;
            string nombre = String.Format("{0}LogError_{1}_{2}_{3}.txt", rutaLog, fecha.Year, 
                fecha.Month.ToString().PadLeft(2, '0'), 
                fecha.Day.ToString().PadLeft(2, '0'));
            Objeto.grabar(oenLog, nombre);
        }

        public static void grabar(Exception ex, bool SinSesion)
        {
            enLog oenLog = new enLog();
            oenLog.FechaHora = DateTime.Now;
            enLogin oenLogin = new enLogin();
            oenLog.Aplicacion = oenLogin.Aplicacion;
            oenLog.Usuario = "ADM";
            oenLog.MensajeError = ex.Message;
            oenLog.DetalleError = ex.StackTrace;
            string rutaLog = ConfigurationManager.AppSettings["rutaLog"];
            DateTime fecha = DateTime.Now;
            string nombre = String.Format("{0}LogError_{1}_{2}_{3}.txt", rutaLog, fecha.Year,
                fecha.Month.ToString().PadLeft(2, '0'),
                fecha.Day.ToString().PadLeft(2, '0'));
            Objeto.grabar(oenLog, nombre);
        }

        public static void grabar(Exception ex, string rutaLog)
        {
            enLog oenLog = new enLog();
            oenLog.FechaHora = DateTime.Now;
            oenLog.MensajeError = ex.Message;
            oenLog.DetalleError = ex.StackTrace;
            DateTime fecha = DateTime.Now;
            string nombre = String.Format("{0}LogError_{1}_{2}_{3}.txt", rutaLog, fecha.Year,
                fecha.Month.ToString().PadLeft(2, '0'),
                fecha.Day.ToString().PadLeft(2, '0'));
            Objeto.grabar(oenLog, nombre);
        }

    }
}
