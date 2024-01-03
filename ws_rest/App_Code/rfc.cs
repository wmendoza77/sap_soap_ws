using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;

 namespace functions.App_Code
{
    public class rfc{

        public RfcDestination GetCnx() {
            RfcConfigParameters Conexion = new RfcConfigParameters();
            //Dim Conexion As RfcConfigParameters = New RfcConfigParameters()
            Conexion.Add(RfcConfigParameters.Name, System.Configuration.ConfigurationManager.AppSettings["NAME"].ToString());
            Conexion.Add(RfcConfigParameters.AppServerHost, System.Configuration.ConfigurationManager.AppSettings["SERVER"].ToString());
            Conexion.Add(RfcConfigParameters.SystemNumber, System.Configuration.ConfigurationManager.AppSettings["SYSTEMNO"].ToString());
            Conexion.Add(RfcConfigParameters.User, System.Configuration.ConfigurationManager.AppSettings["USER"].ToString());
            Conexion.Add(RfcConfigParameters.Password, System.Configuration.ConfigurationManager.AppSettings["PASS"].ToString());
            Conexion.Add(RfcConfigParameters.Client, System.Configuration.ConfigurationManager.AppSettings["CLIENT"].ToString());
            Conexion.Add(RfcConfigParameters.Language, System.Configuration.ConfigurationManager.AppSettings["LANG"].ToString());
            Conexion.Add(RfcConfigParameters.PoolSize, "10");
            Conexion.Add(RfcConfigParameters.IdleTimeout, "10");

            RfcDestination ConxSap = RfcDestinationManager.GetDestination(Conexion);
        
           return ConxSap;
        }

    }
}