using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Finilager.General.Librerias.CodigoUsuario
{
    public class ucSerializador
    {
        public static string Serializar<T>(List<T> lista, string separadorCampo, string separadorRegistro, bool incluirCabecera)
        {

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            string tipo;
            if (incluirCabecera)
            {
                for (int i = 0; i < propiedades.Length; i++)
                {
                    sb.Append(propiedades[i].Name);
                    if (i < propiedades.Length - 1) sb.Append(separadorCampo);
                }
                sb.Append(separadorRegistro);
            }
            for(int j=0; j < lista.Count;j++)
            {
                propiedades=lista[j].GetType().GetProperties();
                for (int i = 0; i < propiedades.Length; i++)
                {
                    tipo = propiedades[i].PropertyType.ToString();
                    if (propiedades[i].GetValue(lista[j], null) != null)
                    {
                        if (tipo.Contains("Byte[]"))
                        {
                            byte[] buffer = (byte[])propiedades[i].GetValue(lista[j], null);
                            sb.Append(Convert.ToBase64String(buffer));
                        }
                        else sb.Append(propiedades[i].GetValue(lista[j], null).ToString());
                    }
                    else sb.Append("");
                    if (i < propiedades.Length - 1) sb.Append(separadorCampo);
                }
                if (j < lista.Count - 1) sb.Append(separadorRegistro);
            }
            return sb.ToString();
        }

        public static string SerializarObjeto<T>(T obj, string separadorCampo)
        {

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = obj.GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                sb.Append(propiedades[i].Name);
                if (i < propiedades.Length - 1) sb.Append(separadorCampo);
            }
            return sb.ToString();
        }
    }

    public class CSV
    {
        public static string SerializarLista<T>(List<T> lista, char separadorCampo = '|', char separadorRegistro = ';', bool tieneCabeceras = true)
        {
            StringBuilder sb = new StringBuilder();
            if (lista != null && lista.Count > 0)
            {
                PropertyInfo[] propiedades;
                if (tieneCabeceras)
                {
                    propiedades = lista[0].GetType().GetProperties();
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        sb.Append(propiedad.Name);
                        sb.Append(separadorCampo);
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append(separadorRegistro);
                }
                int nRegistros = lista.Count;
                for (int i = 0; i < nRegistros; i++)
                {
                    propiedades = lista[i].GetType().GetProperties();
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        sb.Append(propiedad.GetValue(lista[i], null));
                        sb.Append(separadorCampo);
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append(separadorRegistro);
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
