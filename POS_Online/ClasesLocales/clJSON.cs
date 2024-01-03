using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace POS_Online.ClasesLocales
{
    public class clJSON
    {
        public object DesSerializar<T>(T obj, string json)
        {
            object S = new object();
            S = JsonConvert.DeserializeObject<T>(json);
            return S;
        }

        public List<T> DesSerializar<T>(List<T> lista, string json)
        {
            List<T> listaR = new List<T>();
            listaR = JsonConvert.DeserializeObject<List<T>>(json);
            return listaR;
        }
    }
}