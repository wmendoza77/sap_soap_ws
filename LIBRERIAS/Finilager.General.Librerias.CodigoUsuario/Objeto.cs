using System;
using System.IO;
using System.Reflection;

namespace Finilager.General.Librerias.CodigoUsuario
{
    public class Objeto
    {
        public static void grabar<T>(T obj,string archivo)
        {
            PropertyInfo[] propiedades = obj.GetType().GetProperties();
            using (StreamWriter sw = new StreamWriter(archivo, true))
            {
                try
                {
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        sw.Write("{0} = {1}\r\n", propiedad.Name, propiedad.GetValue(obj, null));
                    }
                    sw.Write("\r\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrio un error: {0}", ex.Message);
                }
            }
        }
    }
}
