using System;
using System.Data;
using System.Data.SqlClient;

using POS.Librerias.AccesoDatos;
using POS.Librerias.EntidadesNegocio;

using Finilager.General.Librerias.CodigoUsuario;

namespace POS.Librerias.ReglasNegocio
{
    public class brUsuario:brGeneral
    {
        public string validar(enUsuario oenUsuario)
        {
            string rpta = "";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    adUsuario oadUsuario = new adUsuario();
                    rpta = oadUsuario.validar(con,oenUsuario);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return rpta;
        }
    }
}
