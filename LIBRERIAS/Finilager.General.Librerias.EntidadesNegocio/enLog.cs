using System;
namespace Finilager.General.Librerias.EntidadesNegocio
{
    public class enLog
    {
        public DateTime FechaHora { get; set; }
        public string Aplicacion { get; set; }
        public string Usuario { get; set; }
        public string MensajeError { get; set; }
        public string DetalleError { get; set; }
        public string TipoError { get; set; }
    }
}
