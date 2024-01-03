using System;
using System.Data;
using System.Data.SqlClient;

using POS.Librerias.EntidadesNegocio;

namespace POS.Librerias.AccesoDatos
{
    public class adUsuario
    {
        public string validar(SqlConnection con, enUsuario oenUsuario)
        {
            string rpta = "";

            SqlCommand cmd = new SqlCommand("sp_UsuarioValidar",con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p0 = cmd.Parameters.Add("@CodUsuario", SqlDbType.TinyInt);
            p0.Direction = ParameterDirection.Input;
            p0.Value = oenUsuario.CodUsuario;

            SqlParameter p1 = cmd.Parameters.Add("@Clave", SqlDbType.NVarChar);
            p1.Direction = ParameterDirection.Input;
            p1.Value = oenUsuario.Clave;

            object data = cmd.ExecuteScalar();
            if (data != null) rpta = data.ToString();
            return rpta;
        }

        public string validar1(SqlConnection con, enUsuario oenUsuario)
        {
            string rpta = "";

            SqlCommand cmd = new SqlCommand("sp_UsuarioValidar", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p0 = new SqlParameter();
            p0.Value = oenUsuario.CodUsuario;
            cmd.Parameters.Add(p0);

            SqlParameter p1 = new SqlParameter();
            p1.Value = oenUsuario.Clave;
            cmd.Parameters.Add(p1);

            object data = cmd.ExecuteScalar();
            if (data != null) rpta = data.ToString();
            return rpta;
        }

    }
}
