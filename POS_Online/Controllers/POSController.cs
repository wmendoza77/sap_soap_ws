using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Newtonsoft.Json;

using POS.Librerias.EntidadesNegocio;
using POS.Librerias.ReglasNegocio;
using Finilager.General.Librerias.CodigoUsuario;
using Finilager.General.Librerias.EntidadesNegocio;

namespace POS_Online.Controllers
{
    public class POSController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cotizacion()
        {
            return View();
        }

        public ActionResult Pedido()
        {
            return View();
        }

        #region VALIDACIONES
        public string validarUsuario(short Usuario, string Password)
        {
            string rpta = "";
            enUsuario oenUsuario = new enUsuario();
            oenUsuario.CodUsuario = Usuario;
            oenUsuario.Clave = Password;
            brUsuario obrUsuario = new brUsuario();
            rpta = obrUsuario.validar(oenUsuario);
            return rpta;
        }
        #endregion


        #region PEDIDO

        /*public static brUsuario DesSerializar(string json)
        {
            brUsuario deserializedUser = new brUsuario();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as brUsuario;
            ms.Close();
            return deserializedUser;  
        }*/

        /*public static enCliente DesSerializar(string json)
        {
            enCliente u = JsonConvert.DeserializeObject<enCliente>(json);
            return u;
        }*/

        /*public object DesSerializar<T>(T obj, string json)
        {
            object S = new object();
            S = JsonConvert.DeserializeObject<T>(json);
            return S;
        }*/

        public object DesSerializar<T>(ref T obj, string json)
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
        public string pruebaJson()
        {
            //string json = "{'KUNNR':'0000100004','XCPDK':'','CUSTOMER_ID':'0000000000','STCD1':'1028779020','NAME':'SOFIA LTDA.','BRSCH':'Z004','BRTXT':'Industrial','TELF1':'3462717','TELF2':'','EMAIL':'','DIRECCION':'','ZTERM':'NT00','REGULAR':''}";
            string json = "[{'KUNNR':'0000100004','XCPDK':'','CUSTOMER_ID':'0000000000','STCD1':'1028779020','NAME':'SOFIA LTDA.','BRSCH':'Z004','BRTXT':'Industrial','TELF1':'3462717','TELF2':'','EMAIL':'','DIRECCION':'','ZTERM':'NT00','REGULAR':''},{'KUNNR':'0000100082','XCPDK':'','CUSTOMER_ID':'0000000000','STCD1':'5993496012','NAME':'SOFIA VIVIANA ANTEZANA DE BLACUTT','BRSCH':'Z003','BRTXT':'Distribuidor','TELF1':'38522795','TELF2':'','EMAIL':'','DIRECCION':'','ZTERM':'NT90','REGULAR':''}]";
            ucSerializador CS = new ucSerializador();
            List<enCliente> lUsuario = new List<enCliente>();;
            lUsuario = DesSerializar(lUsuario, json);
            string y="";
            y = CSV.SerializarLista(lUsuario, '¬', '¥', false);
            //y = ucSerializador.SerializarObjeto(R, "|");
            return y;
        }

        #endregion

    }
}
