using System;
using System.Configuration;

using Finilager.General.Librerias.CodigoUsuario;

namespace POS.Librerias.ReglasNegocio
{
    public class brGeneral
    {
        public string CadenaConexion { get; set; }

        public brGeneral()
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["conPOS"].ConnectionString;
            }
            catch (Exception ex)
            {
                Log.grabar(ex);
            }
        }
    }
}