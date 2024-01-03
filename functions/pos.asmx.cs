using functions.App_Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace functions
{
    /// <summary>
    /// Descripción breve de pos
    /// </summary>
    [WebService(Namespace = "http://localhost:8083/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class pos : System.Web.Services.WebService
    {
        rfc RFCFunc = new rfc();

        [WebMethod]
        public string ArticulosNuevos(string dias, string meses, string anios, string canal, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user,pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ARTICULOS_NUEVOS");
                sd.SetValue("GV_DIAS", dias);
                sd.SetValue("GV_MESES", meses);
                sd.SetValue("GV_ANIOS", anios);
                sd.SetValue("GV_VTWEG", canal);

                RfcSessionManager.BeginContext(sap);

                sd.Invoke(sap);

                RfcSessionManager.EndContext(sap);

                IRfcTable resultado = sd.GetTable("GT_ARTICULOS");

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(resultado)).ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string BuscarMaterial(string gv_matnr, string gv_zz_descripcion, string gv_groes, string gv_zz_aplicacion, string gv_zeinr, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_BUSCAR_MATERIAL");
                if (!(gv_matnr == "")) { sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0')); }
                else { sd.SetValue("GV_MATNR", ""); }
                sd.SetValue("GV_ZZ_DESCRIPCION", (gv_zz_descripcion.ToUpper()));
                sd.SetValue("GV_GROES", gv_groes.ToUpper());
                sd.SetValue("GV_ZZ_APLICACION", gv_zz_aplicacion.ToUpper());
                sd.SetValue("GV_ZEINR", gv_zeinr.ToUpper());

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("RESULTADO");

                RfcSessionManager.EndContext(sap);

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret)).ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string EtiquetaMaterial(string gv_ean, string gv_matnr, string gv_zz_descripcion, string gv_groes, string gv_zz_aplicacion, string gv_zeinr, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ETIQUETA_MATERIAL");
                sd.SetValue("GV_EAN", gv_ean);
                if (!(gv_matnr == "")) { sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0')); }
                else { sd.SetValue("GV_MATNR", ""); }
                sd.SetValue("GV_ZZ_DESCRIPCION", (gv_zz_descripcion.ToUpper()));
                sd.SetValue("GV_GROES", gv_groes.ToUpper());
                sd.SetValue("GV_ZZ_APLICACION", gv_zz_aplicacion.ToUpper());
                sd.SetValue("GV_ZEINR", gv_zeinr.ToUpper());

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("RESULTADO");

                RfcSessionManager.EndContext(sap);

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret)).ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string BuscarCliente(string gv_kunnr, string gv_stcd1, string gv_name1, string gv_vkorg, string gv_vtweg, string gv_spart, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_BUSCAR_CLIENTE");
                if (!(gv_kunnr == "")) { sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0')); }
                else { sd.SetValue("GV_KUNNR", ""); }
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_NAME1", gv_name1.ToUpper());
                sd.SetValue("GV_VKORG", gv_vkorg);
                sd.SetValue("GV_VTWEG", gv_vtweg);
                sd.SetValue("GV_SPART", gv_spart);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("RESULTADO");

                RfcSessionManager.EndContext(sap);

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret)).ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod] 
        public string ActualizarDataCliente(string gv_kunnr, string gv_stcd1, string gv_type, string gv_value, string user, string pass)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_EDITAR_DATA_CLIENTE");

                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_TYPE", gv_type);
                sd.SetValue("GV_VALUE", gv_value);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string ret = sd.GetValue("EV_RETURN").ToString();

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, string> row = null;

                if (ret == "X") { jsonResp.TIPO = "OK"; } else { jsonResp.TIPO = "ERROR"; };
                jsonResp.MSG = ret;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string DsctoCampania(string gv_stcd1, string user, string pass)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_DSCTO_2DA_COMPRA");

                sd.SetValue("IV_NIT", gv_stcd1);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                int ret = Convert.ToInt32(sd.GetValue("EV_DSCTO"));

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, string> row = null;

                jsonResp.TIPO = "OK";
                jsonResp.MSG = ret.ToString();
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string DatosEtiquetaPacking(string gv_vbeln)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx();
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_DATOS_ETIQUETA");
               
                sd.SetValue("I_VBELN", Utiles.LlenarCadena(gv_vbeln, 10, '0'));
                
                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure ret = sd.GetStructure("EV_DATA");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                for (int liElement = 0; liElement < ret.ElementCount; liElement++)
                {
                    //row = new Dictionary<string, string>();
                    RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
                    //row.Add(metadata.Name, ret.GetString(metadata.Name));
                    json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
                    if ((liElement + 1) == ret.ElementCount)
                    { json += "}"; }
                    else { json += ","; }
                    //packet.Add(row);
                }

                //json = serializer.Serialize(json);

               
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = json;
                    lstResp.Add(jsonResp);
              

                RfcSessionManager.EndContext(sap);
          
            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string EtiquetaProducto(string gv_matnr)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx();
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("Z_MM_FUNC_RFC_MAT");

                sd.SetValue("G_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string textoBreve = sd.GetValue("G_TXTBRV").ToString();
                string ean = sd.GetValue("G_EAN").ToString();
                string fecha = sd.GetValue("G_FECHA").ToString();

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                if (textoBreve == "")
                {
                    //jsonResp.TIPO = "ERROR";
                    //jsonResp.MSG = "Compensación no efectuada.";
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Datos no encontrados." });
                    //jsonResp.Clear();
                }
                else
                {
                    //jsonResp.TIPO = "OK";
                    //jsonResp.MSG = no_compensacion;
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = "{\"TEXTO_BREVE\":\"" + textoBreve + "\", \"EAN\":\"" + ean + "\", \"FECHA\":\"" + fecha + "\"}" });
                    //jsonResp.Clear();
                }

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string RegistrarPuntosAcumulados(string gv_kunnr, string gv_nit, string gv_monto)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx();
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_ACUM_PUNTOS");

                sd.SetValue("IV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("IV_NIT", gv_nit);
                sd.SetValue("IV_MONTO", gv_monto);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure ret = sd.GetStructure("ES_RESULTADO");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                for (int liElement = 0; liElement < ret.ElementCount; liElement++)
                {
                    //row = new Dictionary<string, string>();
                    RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
                    //row.Add(metadata.Name, ret.GetString(metadata.Name));
                    json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
                    if ((liElement + 1) == ret.ElementCount)
                    { json += "}"; }
                    else { json += ","; }
                    //packet.Add(row);
                }

                //json = serializer.Serialize(json);


                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string PuntosClubMecanico(string gv_kunnr)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx();
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CONS_CLUB_MECA");

                sd.SetValue("IV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure ret = sd.GetStructure("ES_RESP");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                //Dictionary<string, string> row = null;

                for (int liElement = 0; liElement < ret.ElementCount; liElement++)
                {
                    //row = new Dictionary<string, string>();
                    RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
                    //row.Add(metadata.Name, ret.GetString(metadata.Name));
                    json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
                    if ((liElement + 1) == ret.ElementCount)
                    { json += "}"; }
                    else { json += ","; }
                    //packet.Add(row);
                }

                //json = serializer.Serialize(json);


                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                json = ex.ToString();
                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string RegistrarPuntosClubMecanico(string gv_kunnr, string gv_nit, string gv_monto, string user, string pass)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_CLUB_MECA_PUNTOS");

                sd.SetValue("IV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("IV_NIT", gv_nit);
                sd.SetValue("IV_MONTO", gv_monto);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure ret = sd.GetStructure("ES_RESULTADO");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                for (int liElement = 0; liElement < ret.ElementCount; liElement++)
                {
                    //row = new Dictionary<string, string>();
                    RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
                    //row.Add(metadata.Name, ret.GetString(metadata.Name));
                    json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
                    if ((liElement + 1) == ret.ElementCount)
                    { json += "}"; }
                    else { json += ","; }
                    //packet.Add(row);
                }

                //json = serializer.Serialize(json);


                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string VerificarPromo2daCompra(string user, string pass)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_DSCTO_2DA_COMPRA_ACT");

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure ret = sd.GetStructure("ES_DATA");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                for (int liElement = 0; liElement < ret.ElementCount; liElement++)
                {
                    //row = new Dictionary<string, string>();
                    RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
                    //row.Add(metadata.Name, ret.GetString(metadata.Name));
                    json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
                    if ((liElement + 1) == ret.ElementCount)
                    { json += "}"; }
                    else { json += ","; }
                    //packet.Add(row);
                }

                //json = serializer.Serialize(json);


                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string Verificar2daCompraCliente(string gv_nit, string user, string pass)
        {
            var json = "{";
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_DSCTO_2DA_COMPRA");

                sd.SetValue("IV_NIT", gv_nit);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure ret = sd.GetStructure("ES_DATA");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                for (int liElement = 0; liElement < ret.ElementCount; liElement++)
                {
                    //row = new Dictionary<string, string>();
                    RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
                    //row.Add(metadata.Name, ret.GetString(metadata.Name));
                    json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
                    if ((liElement + 1) == ret.ElementCount)
                    { json += "}"; }
                    else { json += ","; }
                    //packet.Add(row);
                }

                //json = serializer.Serialize(json);


                jsonResp.TIPO = "OK";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = json;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string TicketsPromocion(string iv_promo, string iv_ticket, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_DATA_PROMOCIONES");
                sd.SetValue("IV_PROMO", iv_promo);
                sd.SetValue("IV_TICKET", iv_ticket);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string resul = sd.GetValue("EV_RETURN").ToString();

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                int firstCommaIndex = resul.IndexOf('&');


                jsonResp.TIPO = firstCommaIndex >= 0 ? resul.Substring(0, firstCommaIndex) : resul;

                jsonResp.MSG = resul.Substring(firstCommaIndex);
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.Message;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string PromocionesABM(string iv_type, string iv_promo, string iv_descripcion, string iv_finicio, string iv_ffin, string iv_monto_base, string user, string pass)
        {
            
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_ABM_PROMOCIONES");
                sd.SetValue("IV_TYPE", iv_type);
                sd.SetValue("IV_PROMO", iv_promo);
                sd.SetValue("IV_DESCRIPCION", iv_descripcion);
                sd.SetValue("IV_INICIO", iv_finicio);
                sd.SetValue("IV_FIN", iv_ffin);
                sd.SetValue("IV_MONTO_BASE", iv_monto_base);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string resul = sd.GetValue("EV_RETURN").ToString();
                IRfcTable ret = sd.GetTable("ET_DATA");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                int firstCommaIndex = resul.IndexOf('&');

              
                jsonResp.TIPO = firstCommaIndex >= 0 ?  resul.Substring(0, firstCommaIndex) : resul;
       
                jsonResp.MSG = Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret)).ToString(); ;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.Message;
                lstResp.Add(jsonResp);
                throw;
            }

            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string ImpresorasDivisionAlmacenes(string user, string pass)
        {
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_PRT_DIV_ALM");

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("ES_RESULTADO");

                RfcSessionManager.EndContext(sap);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                
                jsonResp.TIPO = "OK";
                jsonResp.MSG = Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret)).ToString(); ;
                lstResp.Add(jsonResp);


                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.Message;
                lstResp.Add(jsonResp);
                throw; ;
            }

            return JsonConvert.SerializeObject(lstResp);
        }
        //[WebMethod]
        //public string  PuntosClubMecanico(string gv_kunnr)
        //{
        //    var json = "{";
        //    RfcDestination sap = RFCFunc.GetCnx();
        //    json_Respuesta jsonResp = new json_Respuesta();
        //    List<json_Respuesta> lstResp = new List<json_Respuesta>();
        //    try
        //    {
        //        RfcRepository repo = sap.Repository;

        //        IRfcFunction sd = repo.CreateFunction("ZRFC_CONS_CLUB_MECA");

        //        sd.SetValue("IV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));

        //        RfcSessionManager.BeginContext(sap);
        //        sd.Invoke(sap);

        //        IRfcStructure ret = sd.GetStructure("ES_RESP");

        //        RfcSessionManager.EndContext(sap);

        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        List<Dictionary<string, string>> packet = new List<Dictionary<string, string>>();
        //        Dictionary<string, string> row = null;

        //        for (int liElement = 0; liElement < ret.ElementCount; liElement++)
        //        {
        //            //row = new Dictionary<string, string>();
        //            RfcElementMetadata metadata = ret.GetElementMetadata(liElement);
        //            //row.Add(metadata.Name, ret.GetString(metadata.Name));
        //            json += "\"" + metadata.Name + "\":\"" + ret.GetString(metadata.Name) + "\"";
        //            if ((liElement + 1) == ret.ElementCount)
        //            { json += "}"; }
        //            else { json += ","; }
        //            //packet.Add(row);
        //        }

        //        //json = serializer.Serialize(json);


        //        jsonResp.TIPO = "OK";
        //        jsonResp.MSG = json;
        //        lstResp.Add(jsonResp);


        //        RfcSessionManager.EndContext(sap);

        //    }
        //    catch (Exception ex)
        //    {
        //        jsonResp.TIPO = "OK";
        //        jsonResp.MSG = json;
        //        lstResp.Add(jsonResp);
        //        throw;
        //    }

        //    return JsonConvert.SerializeObject(lstResp);
        //}

        [WebMethod]
        public string CreaTicket(string gv_ticket, string gv_oferta, string gv_pedido, string gv_caja, string gv_oficina, string gv_centro, string gv_almacen, string gv_cliente, string gv_vendor, string gv_validez_f, string gv_validez_t, string gv_cliente_pos, string gt_items, string user, string pass)    
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_TICKET");
                if (!(gv_ticket is null)) { sd.SetValue("GV_TICKET", Utiles.LlenarCadena(gv_ticket, 10, '0')); }
                else { sd.SetValue("GV_TICKET", gv_ticket); }
                sd.SetValue("GV_OFERTA", gv_oferta);
                sd.SetValue("GV_PEDIDO", gv_pedido);
                sd.SetValue("GV_CAJA", Utiles.LlenarCadena(gv_caja, 6, '0'));
                sd.SetValue("GV_OFICINA", gv_oficina);
                sd.SetValue("GV_CENTRO", gv_centro);
                sd.SetValue("GV_CLIENTE", Utiles.LlenarCadena(gv_cliente, 10, '0'));
                sd.SetValue("GV_VENDOR", Utiles.LlenarCadena(gv_vendor, 8, '0'));
                sd.SetValue("GV_VALIDEZ_F", gv_validez_f);
                sd.SetValue("GV_VALIDEZ_T", gv_validez_t);
                if (!(gv_cliente_pos is null)) { sd.SetValue("GV_CLIENTE_POS", Utiles.LlenarCadena(gv_cliente_pos, 10, '0')); }
                else { sd.SetValue("GV_CLIENTE_POS", gv_cliente_pos); }

                IRfcTable tbMatnr = sd.GetTable("GT_ITEMS");
                DataTable dt = new DataTable();
                gt_items = gt_items.Replace(@"\", "");
                dt = Utiles.ConvertJSONToDataTable(gt_items);
                if (!(dt.Rows.Count == 0))
                {
                    IRfcTable gt = sd.GetTable("GT_ITEMS");

                    for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                    {
                        gt.Append();
                        gt.SetValue("TRX_ID", "");
                        gt.SetValue("TRX_POS", i + 1);
                        gt.SetValue("MATNR", Utiles.LlenarCadena(dt.Rows[i]["MATERIAL"].ToString(), 18, '0'));
                        gt.SetValue("WERKS", gv_centro);
                        gt.SetValue("LGORT", gv_almacen);
                        gt.SetValue("MENGE_V", dt.Rows[i]["CANTIDAD"]);
                        gt.SetValue("MENGE_B", dt.Rows[i]["CANTIDAD"]);
                        gt.SetValue("UNIT_PRICE", dt.Rows[i]["PRECIO_UNIT"]);
                        gt.SetValue("UNIT_PRICE_D", dt.Rows[i]["PRECIO_UNIT"]);
                        gt.SetValue("DISC_PRICE", Convert.ToDouble(dt.Rows[i]["DSCTO"]) * -1);
                        if (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) > 0)
                        {
                            gt.SetValue("MDISCOUNTP", (Math.Round((Convert.ToDouble(dt.Rows[i]["DSCTO"]) * 100) / (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])), 0)) * -1);
                            gt.SetValue("BASE_PRICE", (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])).ToString());
                            gt.SetValue("BASE_PRICE_D", (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])).ToString());
                            gt.SetValue("TAX", ((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.13).ToString());
                            gt.SetValue("NETPR", ((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.87).ToString());
                            gt.SetValue("MDISC_PRICE", dt.Rows[i]["DSCTO"].ToString());
                            gt.SetValue("GROSS_PRICE", (((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.87) + ((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.13)).ToString());
                        }
                        gt.SetValue("WAERS", "BOB");
                        gt.SetValue("TAX_PERC", "130.00");
                        gt.SetValue("VGPOS", (i + 1) * 10);
                    }
                }

                RfcSessionManager.BeginContext(sap);

                sd.Invoke(sap);

                RfcSessionManager.EndContext(sap);

                string resultado = sd.GetValue("RESULTADO").ToString();

                return resultado;

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string CrearEntrega(string gv_vbeln, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_ENTREGA");
                sd.SetValue("IV_VBELN", Utiles.LlenarCadena(gv_vbeln, 10, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("EV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("ET_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                if (vbeln == "")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Trim() == "E")
                        {
                            jsonResp.TIPO = "ERROR";
                            jsonResp.MSG = row[3].ToString();
                            lstResp.Add(jsonResp);
                        }
                    }
                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CrearFactura(string gv_vbeln, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_FACTURA");
                sd.SetValue("IV_VBELN", Utiles.LlenarCadena(gv_vbeln, 10, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("EV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("ET_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                if (vbeln == "")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Trim() == "E")
                        {
                            jsonResp.TIPO = "ERROR";
                            jsonResp.MSG = row[3].ToString();
                            lstResp.Add(jsonResp);
                        }
                    }
                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CrearConsulta(string gv_kunnr, string gs_stcd1, string gv_pernr, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_CONSULTA");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_PERNR", gv_pernr);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);
                Llenar_Gt_Address_Partner(sd, gv_kunnr, gs_stcd1);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                if (vbeln == "")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Trim() == "E")
                        {
                            jsonResp.TIPO = "ERROR";
                            jsonResp.MSG = row[1].ToString() + "|" + row[2].ToString() + "|" + row[3].ToString() + "|" + row[4].ToString() + "|" + row[5].ToString() + "|" + row[6].ToString() + "|" + row[7].ToString() + "|" + row[8].ToString() + "|" + row[9].ToString() + "|" + row[10].ToString();
                            lstResp.Add(jsonResp);
                        }
                    }
                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CrearConsultaV2(string gv_kunnr, string gv_pernr, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_CONSULTA_V2");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_PERNR", gv_pernr);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);
                //Llenar_Gt_Address_Partner(sd, gv_kunnr, gs_stcd1);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                if (vbeln == "")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Trim() == "E")
                        {
                            jsonResp.TIPO = "ERROR";
                            jsonResp.MSG = row[3].ToString();
                            lstResp.Add(jsonResp);
                        }
                    }
                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CrearOferta(string gv_kunnr, string gs_stcd1, string gv_pernr, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_OFERTA");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_PERNR", gv_pernr);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);
                Llenar_Gt_Address_Partner(sd, gv_kunnr, gs_stcd1);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                if (vbeln == "")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Trim() == "E")
                        {
                            jsonResp.TIPO = "ERROR";
                            jsonResp.MSG = row[3].ToString();
                            lstResp.Add(jsonResp);
                        }
                    }
                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CrearPedido(string gv_kunnr, string gv_stcd1, string gv_pernr, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_PEDIDO");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_PERNR", gv_pernr);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);
                Llenar_Gt_Address_Partner(sd, gv_kunnr, gv_stcd1);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                if (vbeln == "")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Trim() == "E")
                        {
                            jsonResp.TIPO = "ERROR";
                            jsonResp.MSG = row[3].ToString();
                            lstResp.Add(jsonResp);
                        }
                    }
                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string ClientePOS(string gv_operacion, string gv_stcd1, string gv_name, string gv_title, string gv_tipo_doc, string gv_brsch, string gv_phone, string gv_email, string gv_mob_phone, string gv_street, string gv_str_suppl3, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ABM_CLIENTE");
                sd.SetValue("GV_OPERACION", gv_operacion);
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_NAME", gv_name);
                sd.SetValue("GV_TITLE", gv_title);
                sd.SetValue("GV_TIPO_DOC", gv_tipo_doc);
                sd.SetValue("GV_BRSCH", gv_brsch);
                sd.SetValue("GV_PHONE", gv_phone);
                sd.SetValue("GV_EMAIL", gv_email);
                sd.SetValue("GV_MOB_PHONE", gv_mob_phone);
                sd.SetValue("GV_STREET", gv_street);
                sd.SetValue("GV_STR_SUPPL3", gv_str_suppl3);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("GV_RESULTADO").ToString();

                RfcSessionManager.EndContext(sap);

                if (gv_resultado == "")
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Cliente no creado." });
                }
                else
                {
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = gv_resultado });
                }
                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string CrearDocumentoInvWM(string gv_lgnum, string gv_lgtyp, string gv_lgpla, string gv_contador, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_WM_CREAR_DOC_INVE");
                sd.SetValue("G_LGNUM", gv_lgnum);
                sd.SetValue("G_LGTYP", gv_lgtyp);
                sd.SetValue("G_LGPLA", gv_lgpla);
                sd.SetValue("G_CONTADOR", gv_contador);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                IRfcTable gt_resultado = sd.GetTable("IT_SALIDA");

                RfcSessionManager.EndContext(sap);

                DataTable tblResp = Utiles.ConvertRFCTableToDataTable(gt_resultado);
                string msg = "";
                if (tblResp.Rows.Count > 0)
                {               
                    msg = Utiles.GetJson(tblResp).ToString();
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = msg });
                } else
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = msg });
                }

                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string RegistrarConteoInvWM(string gv_lgnum, string gv_ivnum, string gv_matnr, string gv_contador, string gv_cantidad, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_WM_CARGAR_CONTADOS");
                sd.SetValue("G_LGNUM", gv_lgnum);
                sd.SetValue("G_IVNUM", gv_ivnum);
                sd.SetValue("G_MATNR", gv_matnr);
                sd.SetValue("G_CONTADOR", gv_contador);
                sd.SetValue("G_CANTC", gv_cantidad);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("G_ESTADO").ToString();

                RfcSessionManager.EndContext(sap);
                
                string msg = "";
                if (gv_resultado == "0")
                {
                    msg = "";
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = msg });
                }
                else
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = msg });
                }

                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string Actividades(string gv_operacion, string gv_idactividad, string gv_tipo_actividad, string gv_username, string gv_cliente,
            string gv_nit, string gv_tipo_documento, string gv_documento, string gv_id_seguimiento,  string gv_asunto, string gv_para, 
            string gv_cc, string gv_body, string gv_fecha, string gv_hora, string gv_fecha_inicio, string gv_hora_inicio, string gv_fecha_fin, 
            string gv_hora_fin, string gv_periodica, string gv_fecha_aviso, string gv_hora_aviso, string gv_id_graph, string gv_images, 
            string gv_latitud, string gv_longitud, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ACTIVIDADES");
                sd.SetValue("GV_OPERACION", gv_operacion);
                sd.SetValue("GV_IDACTIVIDAD", gv_idactividad);
                sd.SetValue("GV_TIPO_ACTIVIDAD", gv_tipo_actividad);
                sd.SetValue("GV_USERNAME", gv_username);
                sd.SetValue("GV_CLIENTE", gv_cliente);
                sd.SetValue("GV_NIT", gv_nit);
                sd.SetValue("GV_TIPO_DOCUMENTO", gv_tipo_documento);
                sd.SetValue("GV_DOCUMENTO", gv_documento);
                sd.SetValue("GV_ID_SEGUIMIENTO", gv_id_seguimiento);
                sd.SetValue("GV_ASUNTO", gv_asunto);
                sd.SetValue("GV_PARA", gv_para);
                sd.SetValue("GV_CC", gv_cc);
                sd.SetValue("GV_BODY", gv_body);
                sd.SetValue("GV_FECHA", gv_fecha);
                sd.SetValue("GV_HORA", gv_hora);
                sd.SetValue("GV_FECHA_INICIO", gv_fecha_inicio);
                sd.SetValue("GV_HORA_INICIO", gv_hora_inicio);
                sd.SetValue("GV_FECHA_FIN", gv_fecha_fin);
                sd.SetValue("GV_HORA_FIN", gv_hora_fin);
                sd.SetValue("GV_PERIODICA", gv_periodica);
                sd.SetValue("GV_FECHA_AVISO", gv_fecha_aviso);
                sd.SetValue("GV_HORA_AVISO", gv_hora_aviso);
                sd.SetValue("GV_ID_GRAPH", gv_id_graph);
                sd.SetValue("GV_LATITUD", gv_latitud);
                sd.SetValue("GV_LONGITUD", gv_longitud);
                sd.SetValue("GV_IMAGENES", gv_images);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("GV_RESULTADO").ToString();
                IRfcTable gt_resultado = sd.GetTable("GT_RESULTADO");

                RfcSessionManager.EndContext(sap);

                if (gv_resultado == "0")
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "'Error en la operacion.'" });
                }
                else
                {
                  
                    JToken msg = Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(gt_resultado));
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = msg.ToString() });
                }
                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string LeadABM(string gv_operacion, string gv_stcd1, string gv_name, string gv_title, string gv_tipo_doc, 
            string gv_brsch, string gv_phone, string gv_email, string gv_direccion, string gv_geolocalizacion,
            string gv_observacion, string gv_kunnr, string gv_contacto, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ABM_LEAD_APP");
                sd.SetValue("GV_OPERACION", gv_operacion);
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_NAME", gv_name.ToUpper());
                sd.SetValue("GV_TITLE", gv_title);
                sd.SetValue("GV_TIPO_DOC", gv_tipo_doc);
                sd.SetValue("GV_BRSCH", gv_brsch);
                sd.SetValue("GV_PHONE", gv_phone);
                sd.SetValue("GV_EMAIL", gv_email);
                sd.SetValue("GV_DIRECCION", gv_direccion);
                sd.SetValue("GV_GEO_LOC", gv_geolocalizacion);
                sd.SetValue("GV_OBSERVACION", gv_observacion);
                //sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr,10,'0'));
                if (gv_kunnr.ToString() == "") { sd.SetValue("GV_KUNNR", ""); }
                else { sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0')); }
                sd.SetValue("GV_CONTACTO", gv_contacto.ToUpper());

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("GV_RESULTADO").ToString();
                IRfcTable gt_resultado = sd.GetTable("GT_RESULTADO");
                RfcSessionManager.EndContext(sap);

                if (gv_resultado == "0")
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "'Error en la operacion.'" });
                }
                else
                {
                    string msg = Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(gt_resultado)).ToString();
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = msg });
                }
                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string LeadDocs(string gv_operacion, string gv_vbeln, string gv_vbtyp, string gv_auart, string gv_vkbur, string gv_id_lead,
            string gv_name, string gv_nit, string gv_fecha_f, string gv_fecha_t, string gv_estado, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ABM_LEAD_DOCS");
                sd.SetValue("GV_OPERACION", gv_operacion);
                if (!(gv_vbeln == "")) { sd.SetValue("GV_VBELN", Utiles.LlenarCadena(gv_vbeln, 10, '0')); }  
                else { sd.SetValue("GV_VBELN", gv_vbeln);  }              
                sd.SetValue("GV_VBTYP", gv_vbtyp);
                sd.SetValue("GV_AUART", gv_auart);
                sd.SetValue("GV_VKBUR", gv_vkbur);
                sd.SetValue("GV_ID_LEAD", gv_id_lead);
                sd.SetValue("GV_NAME", gv_name.ToUpper());
                sd.SetValue("GV_NIT", gv_nit);                
                sd.SetValue("GV_FECHA_F", gv_fecha_f);
                sd.SetValue("GV_FECHA_T", gv_fecha_t);
                sd.SetValue("GV_ESTADO", gv_estado);
                //sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr,10,'0'));


                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("GV_RESULTADO").ToString();
                IRfcTable gt_resultado = sd.GetTable("GT_RESULTADO");
                RfcSessionManager.EndContext(sap);

                if (gv_resultado == "0")
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "'Error en la operacion.'" });
                }
                else
                {
                    string msg = Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(gt_resultado)).ToString();
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = msg });
                }
                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string LogAccesoApp(string gv_operacion, string gv_usuario, string gv_fecha_f, string gv_fecha_t, string gv_hora, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ABM_LOG_APP");
                sd.SetValue("GV_OPERACION", gv_operacion);               
                sd.SetValue("GV_USUARIO", gv_usuario);
                sd.SetValue("GV_FECHA_T", gv_fecha_f);
                sd.SetValue("GV_FECHA_F", gv_fecha_t);
                sd.SetValue("GV_HORA", gv_hora);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("GV_RESULTADO").ToString();
                IRfcTable gt_resultado = sd.GetTable("GT_RESULTADO");
                RfcSessionManager.EndContext(sap);

                if (gv_resultado == "0")
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "'Error en la operacion.'" });
                }
                else
                {
                    string msg = Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(gt_resultado)).ToString();
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = msg });
                }
                return JsonConvert.SerializeObject(lstResp);

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string FacturarPedidoWeb(string iv_vbeln, float iv_wrbtr, string iv_id_externo, string iv_ref_externa, string iv_account_comp)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            RfcRepository repo = sap.Repository;
            //json_Respuesta jsonResp = new json_Respuesta();
            //json_Respuesta jsonResp1 = new json_Respuesta();
            //json_Respuesta jsonResp2 = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            //Función para consultas SQL a tablas
            IRfcFunction sd = repo.CreateFunction("ZRFC_FACTURACION_ZPWB");
            sd.SetValue("IV_VBELN", iv_vbeln);
            sd.SetValue("IV_WRBTR", iv_wrbtr);
            sd.SetValue("IV_ID_EXTERNO", iv_id_externo);
            sd.SetValue("IV_REF_EXTERNA", iv_ref_externa);
            sd.SetValue("IV_ACCOUNT_COMP", iv_account_comp);

            RfcSessionManager.BeginContext(sap);
            sd.Invoke(sap);

            string no_factura = sd.GetValue("EV_VBELN").ToString();
            string no_compensacion = sd.GetValue("EV_BELNR").ToString();
            IRfcTable DATA = sd.GetTable("GT_MESSAGE");
            RfcSessionManager.EndContext(sap);

            DataTable dt = Utiles.ConvertRFCTableToDataTable(DATA);

            if (no_factura == "")
            {
                //jsonResp.TIPO = "ERROR";
                //jsonResp.MSG = "Factura no creada.";
                lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Factura no creada." });
                //jsonResp.Clear();
            }
            else
            {
                //jsonResp.TIPO = "OK";
                //jsonResp.MSG = no_factura;
                lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = no_factura });
                //jsonResp.Clear();
            }
            if (no_compensacion == "")
            {
                //jsonResp.TIPO = "ERROR";
                //jsonResp.MSG = "Compensación no efectuada.";
                lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Compensación no efectuada." });
                //jsonResp.Clear();
            }
            else
            {
                //jsonResp.TIPO = "OK";
                //jsonResp.MSG = no_compensacion;
                lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = no_compensacion });
                //jsonResp.Clear();
            }
            //jsonResp.TIPO = "LOG";
            //jsonResp.MSG = Utiles.DataTableToJson(dt);
            lstResp.Add(new json_Respuesta { TIPO = "LOG", MSG = Utiles.DataTableToJson(dt) });
            //jsonResp.Clear();
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CompensacionFacturaSAP(string iv_no_factura, string iv_division, string iv_cta_contable, string iv_forma_pago, string iv_referencia, string iv_txt_cab_doc, string iv_txt_compensacion, string iv_texto, string iv_asignacion, string iv_bldat, string user, string pass)
        {
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            RfcRepository repo = sap.Repository;          
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            //Función para consultas SQL a tablas
            IRfcFunction sd = repo.CreateFunction("ZWS_COMPENSACION_SD");
            sd.SetValue("IV_REFERENCIA", iv_referencia);
            sd.SetValue("IV_TXT_CAB_DOC", iv_txt_cab_doc);
            sd.SetValue("IV_TXT_COMPENSACION", iv_txt_compensacion);
            sd.SetValue("IV_CTA_CONTABLE", iv_cta_contable);
            sd.SetValue("IV_DIVISION", iv_division);
            sd.SetValue("IV_TEXTO", iv_texto);
            sd.SetValue("IV_ASIGNACION", iv_asignacion);
            sd.SetValue("IV_NO_FACTURA", iv_no_factura);
            sd.SetValue("IV_FORMA_PAGO", iv_forma_pago);
            sd.SetValue("IV_BLDAT", iv_bldat);

            RfcSessionManager.BeginContext(sap);
            sd.Invoke(sap);

            string no_compensacion = sd.GetValue("EV_BELNR").ToString();
            IRfcTable DATA = sd.GetTable("ET_MESSAGE");
            RfcSessionManager.EndContext(sap);

            DataTable dt = Utiles.ConvertRFCTableToDataTable(DATA);

            if (no_compensacion == "")
            {
                //jsonResp.TIPO = "ERROR";
                //jsonResp.MSG = "Compensación no efectuada.";
                lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Compensación no efectuada." });
                //jsonResp.Clear();
            }
            else
            {
                //jsonResp.TIPO = "OK";
                //jsonResp.MSG = no_compensacion;
                lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = no_compensacion });
                //jsonResp.Clear();
            }
            //jsonResp.TIPO = "LOG";
            //jsonResp.MSG = Utiles.DataTableToJson(dt);
            lstResp.Add(new json_Respuesta { TIPO = "LOG", MSG = Utiles.DataTableToJson(dt) });
            //jsonResp.Clear();
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string DatosFacturaSAP(string iv_vbeln, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            RfcRepository repo = sap.Repository;
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            //Función para consultas SQL a tablas
            IRfcFunction sd = repo.CreateFunction("ZRFC_DATOS_FACTURA");
            sd.SetValue("IV_VBELN", iv_vbeln);

            RfcSessionManager.BeginContext(sap);
            sd.Invoke(sap);

            string codCliente = sd.GetValue("EV_KUNNR").ToString();
            string nit = sd.GetValue("EV_STCD1").ToString();
            string razonSocial = sd.GetValue("EV_NAME").ToString();
            string monto = sd.GetValue("EV_NETWR").ToString();
            
            RfcSessionManager.EndContext(sap);

            if (codCliente == "")
            {
                //jsonResp.TIPO = "ERROR";
                //jsonResp.MSG = "Compensación no efectuada.";
                lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Datos no encontrados." });
                //jsonResp.Clear();
            }
            else
            {
                //jsonResp.TIPO = "OK";
                //jsonResp.MSG = no_compensacion;
                lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = "{KUNNR:'" + codCliente + "', STCD1:'" + nit + "', NAME:'" + razonSocial + "', NETWR: '" + monto + "'}" });
                //jsonResp.Clear();
            }
            
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string GuardarTexto(string tipo, string doc, string linea, string texto)
        {
            int cont = texto.Length / 72; ;
            //if (texto.Length > 132) { cont = texto.Length / 132; } else { cont = 1; }
            int pos = 0;
            string objeto = "VBBK";
            string id_obj = "0001";
            string nom_obj = doc;
            if (!(tipo == "C"))
            {
                objeto = "VBBP";
                nom_obj = nom_obj + Utiles.LlenarCadena(linea, 6, '0');
            }

            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_SAVE_TEXT");


                IRfcTable gt = sd.GetTable("TEXT_LINES");

                for (int i = 0; (i <= (cont)); i++)
                {
                    gt.Append();
                    gt.SetValue("TDOBJECT", objeto);
                    gt.SetValue("TDNAME", nom_obj);
                    gt.SetValue("TDID", id_obj);
                    gt.SetValue("TDSPRAS", "S");
                    if (texto.Length > 72)
                    {
                        if (i == cont) { gt.SetValue("TDLINE", texto.Substring(pos)); }
                        else { gt.SetValue("TDLINE", texto.Substring(pos, 72)); }
                    }
                    else { gt.SetValue("TDLINE", texto); }
                    pos += 72;
                }

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                //IRfcStructure mat_stock = sd.GetStructure("MRP_STOCK_DETAIL");

                RfcSessionManager.EndContext(sap);

                return "";


            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string Login(string username, string password)
        {
            json_Respuesta jsonResp = new json_Respuesta();
            try
            {
                RfcDestination conSAP = RFCFunc.GetCnx(username, password);
                RfcRepository repo = conSAP.Repository;
                conSAP.Ping();
              
                //Función para consultas SQL a tablas
                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_DATOS_USUARIO");
                sd.SetValue("GV_USER", username.ToUpper());

                RfcSessionManager.BeginContext(conSAP);
                sd.Invoke(conSAP);

                string data = sd.GetValue("GV_DATA").ToString();

                RfcSessionManager.EndContext(conSAP);
                jsonResp.TIPO = "OK";
                jsonResp.MSG = data;

            }
            catch (Exception ex)
            {
                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
            }

            return JsonConvert.SerializeObject(jsonResp);
           
        }

        [WebMethod]
        public string ListarDocumentos(string clase, string vkbur, string vtweg, string spart, string vboff, string kunnr, string matnr, string vbeln, string audat, string pernr, string ernam, string auart, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_LISTAR_SD_DOCS");
                sd.SetValue("CLASE", clase);
                sd.SetValue("VKBUR", vkbur);
                sd.SetValue("VTWEG", vtweg);
                sd.SetValue("SPART", spart);
                sd.SetValue("VBOFF", vboff);

                if (!(matnr.Trim() == ""))
                {
                    IRfcTable tbMatnr = sd.GetTable("RG_MATNR");
                    LlenarTablaRango(matnr, 18, tbMatnr);
                }
                if (!(kunnr.Trim() == ""))
                {
                    IRfcTable tbKunnr = sd.GetTable("RG_KUNNR");
                    LlenarTablaRango(kunnr, 10, tbKunnr);
                }
                if (!(vbeln.Trim() == ""))
                {
                    IRfcTable tbVbeln = sd.GetTable("RG_VBELN");
                    LlenarTablaRango(vbeln, 10, tbVbeln);
                }
                if (!(audat.Trim() == ""))
                {
                    IRfcTable tbAudat = sd.GetTable("RG_AUDAT");
                    LlenarTablaRango(audat, 8, tbAudat);
                }
                if (!(pernr.Trim() == ""))
                {
                    IRfcTable tbPernr = sd.GetTable("RG_PERNR");
                    LlenarTablaRango(pernr, 8, tbPernr);
                }
                if (!(ernam.Trim() == ""))
                {
                    IRfcTable tbErnam = sd.GetTable("RG_ERNAM");
                    LlenarTablaRango(ernam, 12, tbErnam);
                }
                if (!(auart.Trim() == ""))
                {
                    IRfcTable tbAuart = sd.GetTable("RG_AUART");
                    LlenarTablaRango(auart, 0, tbAuart);
                }

                RfcSessionManager.BeginContext(sap);

                sd.Invoke(sap);

                RfcSessionManager.EndContext(sap);

                IRfcTable resultado = sd.GetTable("RESULTADO");

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(resultado)).ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string ModificaConsulta(string gv_vbeln, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_MODIFICA_CONSULTA");
                sd.SetValue("GV_VBELN", gv_vbeln);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);

                var cont = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString().Trim() == "E")
                    {
                        cont += 1;
                        jsonResp.TIPO = "ERROR";
                        jsonResp.MSG = row[3].ToString();
                        lstResp.Add(jsonResp);
                    }
                }
                if (!(cont > 0))
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string ModificaOferta(string gv_vbeln, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            //RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_MODIFICA_OFERTA");
                sd.SetValue("GV_VBELN", gv_vbeln);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);

                var cont = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString().Trim() == "E")
                    {
                        cont += 1;
                        jsonResp.TIPO = "ERROR";
                        jsonResp.MSG = row[3].ToString();
                        lstResp.Add(jsonResp);
                    }
                }
                if (!(cont > 0))
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string ModificaPedido(string gv_vbeln, string gs_header, string gt_items, string gt_schedules, string gt_conditions, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_MODIFICA_PEDIDO");
                sd.SetValue("GV_VBELN", gv_vbeln);
                Llenar_Gs_Header(sd, gs_header);
                Llenar_Gt_Items(sd, gt_items);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);

                var cont = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString().Trim() == "E")
                    {
                        cont += 1;
                        jsonResp.TIPO = "ERROR";
                        jsonResp.MSG = row[3].ToString();
                        lstResp.Add(jsonResp);
                    }
                }
                if (!(cont > 0))
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = vbeln;
                    lstResp.Add(jsonResp);
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string CrossSelling(string gv_matnr, string gv_vkorg, string gv_vtweg, string gv_spart, string gv_auart, string gv_kunnr, string gv_werks, string gv_lgort, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ART_CROSS_SELLING");
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_VKORG", gv_vkorg);
                sd.SetValue("GV_VTWEG", gv_vtweg);
                sd.SetValue("GV_SPART", gv_spart);
                sd.SetValue("GV_AUART", gv_auart);
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_WERKS", gv_werks);
                sd.SetValue("GV_LGORT", gv_lgort);
                

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("RESULTADO");

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);

                //var cont = dt.Rows.Count;
                if (dt.Rows.Count == 0)
                {
                    lstResp.Add(new json_Respuesta { TIPO = "ERROR", MSG = "Datos no encontrados." });
                }
                else
                {
                    lstResp.Add(new json_Respuesta { TIPO = "OK", MSG = Utiles.DataTableToJson(dt) });
                }
                RfcSessionManager.EndContext(sap);        

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                lstResp.Add(jsonResp);
                throw;
            }
            return JsonConvert.SerializeObject(lstResp);
        }

        [WebMethod]
        public string MotivosRechazo(string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            RfcRepository repo = sap.Repository;
            string result = "";
            //Función para consultas SQL a tablas
            IRfcFunction sd = repo.CreateFunction("RFC_READ_TABLE");
            sd.SetValue("QUERY_TABLE", "TVAGT"); //'Tabla sobre la que queremos buscar
            sd.SetValue("DELIMITER", ";"); //'Delimitador de valores

            IRfcTable tblFields = sd.GetTable("FIELDS");
            tblFields.Append();
            tblFields.SetValue("FIELDNAME", "ABGRU");
            tblFields.Append();
            tblFields.SetValue("FIELDNAME", "BEZEI");

            //'Condición WHERE para realizar la busqueda
            IRfcTable tblOptions = sd.GetTable("OPTIONS");
            tblOptions.Append();
            tblOptions.SetValue("TEXT", "SPRAS EQ 'S'");

            RfcSessionManager.BeginContext(sap);
            sd.Invoke(sap);

            IRfcTable DATA = sd.GetTable("DATA");
            RfcSessionManager.EndContext(sap);

            DataTable dt = Utiles.ConvertRFCTableToDataTable(DATA);


            return Utiles.DataTableToJson(dt);
        }

        [WebMethod]
        public string ObtenerVendedor(string nroVendedor)
        {
            string rpta = Utiles.obtenerVendedor(nroVendedor);
            return rpta;
        }

        [WebMethod]
        public string PrecioDeVenta(string gv_vkorg, string gv_vtweg, string gv_spart, string gv_kunnr, string gv_auart, string gv_matnr, string gv_werks, string gv_prsdt, string gv_menge, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CONDICIONES_PRECIO_P");
                sd.SetValue("GV_VKORG", gv_vkorg);
                sd.SetValue("GV_VTWEG", gv_vtweg);
                sd.SetValue("GV_SPART", gv_spart);
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_AUART", gv_auart.ToUpper());
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_WERKS", gv_werks);
                sd.SetValue("GV_PRSDT", gv_prsdt);
                sd.SetValue("GV_MENGE", gv_menge);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure st_komp = sd.GetStructure("GS_KOMP");
                IRfcTable tb_komv = sd.GetTable("GT_KOMV");

                RfcSessionManager.EndContext(sap);
                string zred = "";

                foreach (IRfcStructure row in tb_komv)
                {
                    if (row["KSCHL"].GetValue().ToString() == "ZRED")
                    {
                        zred = "X";
                    }
                }

                decimal precio_venta = Convert.ToDecimal(st_komp["KZWI5"].GetValue());
                decimal descuento = Convert.ToDecimal(st_komp["KZWI6"].GetValue());

                DataTable dt = new DataTable();
                dt.Columns.Add("MATERIAL");
                dt.Columns.Add("PRECIO_VENTA");
                dt.Columns.Add("DESCUENTO");
                dt.Columns.Add("EXCENTO");

                dt.Rows.Add(gv_matnr, precio_venta, descuento, zred);

                return Utiles.GetJson(dt).ToString();


            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string NecesidadesAlmacen(string gv_vbeln, string gv_posnr, string gv_werks, string gv_lgort, string gv_matnr, string gv_maktx, string gv_znece, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();
            List<json_Respuesta> lstResp = new List<json_Respuesta>();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_PED_A_ALMAC");
                sd.SetValue("GV_VBELN", gv_vbeln);
                sd.SetValue("GV_POSNR", Utiles.LlenarCadena(gv_posnr, 6, '0'));
                sd.SetValue("GV_WERKS", gv_werks);
                sd.SetValue("GV_LGORT", gv_lgort);
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_MAKTX", gv_maktx);
                sd.SetValue("GV_ZNECE", gv_znece);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure et_pedido = sd.GetStructure("ET_PEDIDO");

                RfcSessionManager.EndContext(sap);

                string evbeln = et_pedido["VBELN"].GetValue().ToString();

                if (gv_vbeln == evbeln)
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = et_pedido["VBELN"].GetValue().ToString();
                    lstResp.Add(jsonResp);
                } else
                {
                    jsonResp.TIPO = "ERROR";
                    jsonResp.MSG = "Error";
                    lstResp.Add(jsonResp);
                    
                }
                return JsonConvert.SerializeObject(lstResp);
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string StockPorCentro(string gv_matnr, string gv_werks, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                /*JArray materials = JArray.Parse(gv_matnr);*/
                String[] materials = Echovoice.JSON.JSONDecoders.DecodeJSONArray(gv_matnr);
                DataTable dt = new DataTable();
                dt.Columns.Add("MATERIAL");
                dt.Columns.Add("LIBRE_UTILIZACION");
                dt.Columns.Add("COMPROMETIDO");

                if (!(materials.Length <= 0))
                {
                    foreach (string value in materials)
                    {
                        RfcRepository repo = sap.Repository;

                        IRfcFunction sd = repo.CreateFunction("BAPI_MATERIAL_STOCK_REQ_LIST");
                        sd.SetValue("MATERIAL", Utiles.LlenarCadena(value.ToString(), 18, '0'));
                        sd.SetValue("PLANT", gv_werks);

                        RfcSessionManager.BeginContext(sap);
                        sd.Invoke(sap);

                        IRfcStructure mat_stock = sd.GetStructure("MRP_STOCK_DETAIL");

                        RfcSessionManager.EndContext(sap);

                        decimal libre_Utilizacion = Convert.ToDecimal(mat_stock["UNRESTRICTED_STCK"].GetValue());
                        decimal comprometido_sd = Convert.ToDecimal(mat_stock["SALES_REQS"].GetValue());
                        decimal comprometido_mm = Convert.ToDecimal(mat_stock["STK_TRNF_REL"].GetValue());

                        decimal comprometido = comprometido_sd + comprometido_mm;

                        dt.Rows.Add(value.ToString(), libre_Utilizacion, comprometido);
                    }
                }
                return Utiles.GetJson(dt).ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string StockPorCentroPorAlmacen(string gt_matnr, string gt_werks, string gt_lgort, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_STOCK_DISPONIBLE_BK");

                if (!(gt_matnr.Trim() == ""))
                {
                    IRfcTable tbMatnr = sd.GetTable("GT_MATNR");
                    LlenarTablaRango(gt_matnr, 18, tbMatnr);
                }
                if (!(gt_werks.Trim() == ""))
                {
                    IRfcTable tbWerks = sd.GetTable("GT_WERKS");
                    LlenarTablaRango(gt_werks, 4, tbWerks);
                }
                if (!(gt_lgort.Trim() == ""))
                {
                    IRfcTable tbLgort = sd.GetTable("GT_LGORT");
                    LlenarTablaRango(gt_lgort, 4, tbLgort);
                }

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable data = sd.GetTable("RETURN");

                RfcSessionManager.EndContext(sap);

                DataTable dt = Utiles.ConvertRFCTableToDataTable(data);

                return Utiles.DataTableToJson(dt);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public string TraerVendedor(int pernr, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_TRAER_VENDEDOR");
                sd.SetValue("I_PERNR", Utiles.LlenarCadena(pernr.ToString(), 8, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string ename = sd.GetValue("E_ENAME").ToString();

                if (ename == "")
                {
                    jsonResp.TIPO = "ERROR";
                    jsonResp.MSG = "Vendedor no encontrado!!!";

                }
                else
                {
                    jsonResp.TIPO = "OK";
                    jsonResp.MSG = ename;
                }

                RfcSessionManager.EndContext(sap);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                throw;
            }
            return JsonConvert.SerializeObject(jsonResp);
        }

        [WebMethod]
        public string DatosUsuario(string username, string password)
        {

            RfcDestination sap = RFCFunc.GetCnx(username, password);
            json_Respuesta jsonResp = new json_Respuesta();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZWS_RFC_DATOS_USUARIO");
                sd.SetValue("IV_USER", username);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string ename = sd.GetValue("EV_DATA").ToString();

                RfcSessionManager.EndContext(sap);

                return JsonConvert.SerializeObject(ename);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string ComparacionDeQs(string gv_kunnr, string gv_fechai, string gv_fechaf, string gv_nroges, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            json_Respuesta jsonResp = new json_Respuesta();

            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_OBT_DETALLE_FACT");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_FECHAI", gv_fechai);
                sd.SetValue("GV_FECHAF", gv_fechaf);
                sd.SetValue("GV_NROGES", gv_nroges);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                IRfcTable tbLP = sd.GetTable("RET_DOC1");
                IRfcTable tbLC = sd.GetTable("RET_DOC2");

                DataTable dtP = Utiles.ConvertRFCTableToDataTable(tbLP);
                DataTable dtC = Utiles.ConvertRFCTableToDataTable(tbLC);

                RfcSessionManager.EndContext(sap);
                string retorno = "" + Utiles.DataTableToJson(dtP) +  "|" + Utiles.DataTableToJson(dtC) + "";

                jsonResp.TIPO = "OK";
                jsonResp.MSG = retorno;

                return JsonConvert.SerializeObject(jsonResp);

            }
            catch (Exception ex)
            {

                jsonResp.TIPO = "ERROR";
                jsonResp.MSG = ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string WMTraerUbicaciones(string iv_almacen, string user, string pass)
        {
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                DataTable dt = new DataTable();
                DataColumn c_lgnum = new DataColumn();
                c_lgnum.ColumnName = "LGNUM";
                DataColumn c_lgtyp = new DataColumn();
                c_lgtyp.ColumnName = "LGTYP";
                DataColumn c_lgpla = new DataColumn();
                c_lgpla.ColumnName = "LGPLA";
                dt.Columns.Add(c_lgnum);
                dt.Columns.Add(c_lgtyp);
                dt.Columns.Add(c_lgpla);

                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("YY_WM_TRAER_UBICACIONES")    ;
                sd.SetValue("IV_ALMACEN", Utiles.LlenarCadena(iv_almacen, 3, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("RESULTADO");

                RfcSessionManager.EndContext(sap);

                foreach (IRfcStructure row in ret)
                {
                    dt.Rows.Add(row["LGNUM"].GetValue(), row["LGTYP"].GetValue(), row["LGPLA"].GetValue());
                }

                return Utiles.GetJson(dt).ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }

        }

        [WebMethod]
        public string AppConfig(string modo)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Datos = new
            {
                urlSOAP = System.Configuration.ConfigurationManager.AppSettings["URL_SOAP"].ToString(),
                ipZebraPrint = System.Configuration.ConfigurationManager.AppSettings["ZEBRA_PRINT"].ToString(),
                ipZebraPrintCdd = System.Configuration.ConfigurationManager.AppSettings["ZEBRA_PRINT_CDD"].ToString(),
                zpl = System.Configuration.ConfigurationManager.AppSettings["ZPL"].ToString(),
                zplPrd = System.Configuration.ConfigurationManager.AppSettings["ZPLPrd"].ToString()
            };

            return serializer.Serialize(Datos);
        }

        [WebMethod]
        public string TablaDsctosPOS(string usuario, string oficina, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                DataTable dt = new DataTable();
                DataColumn c_percent = new DataColumn();
                c_percent.ColumnName = "PORCENTAJE";
                DataColumn c_monto = new DataColumn();
                c_monto.ColumnName = "MONTO_LIMITE";
                dt.Columns.Add(c_percent);
                dt.Columns.Add(c_monto);

                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_DSCTOS_POS");
                sd.SetValue("USUARIO", usuario);
                sd.SetValue("OFICINA", Utiles.LlenarCadena(oficina, 6, '0'));

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("DSCTOS");

                RfcSessionManager.EndContext(sap);

                foreach (IRfcStructure row in ret)
                {
                    dt.Rows.Add(row["PERCENT"].GetValue(), row["NETWR"].GetValue());
                }


                return Utiles.GetJson(dt).ToString();


            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }
        
        [WebMethod]
        public byte[] TraerPDFPedidoAlmacen(string gv_vendedor, string gv_vbeln, string user, string pass)
        {
            try { 
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_SMARTFORM_NECESIDAD_ALM");
                sd.SetValue("IV_VENDEDOR", gv_vendedor); //'Delimitador de valores
                sd.SetValue("IV_PEDIDO", gv_vbeln); //'Tabla sobre la que queremos buscar

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string bin = sd.GetValue("EV_PDF_BASE_64").ToString();
                byte[] pdf = Convert.FromBase64String(bin);
                RfcSessionManager.EndContext(sap);

                return pdf;
            }
            catch (Exception ex) 
            {
                string error = ex.ToString();
                return null;
                throw;
            }
        }

        [WebMethod]
        public byte[] TraerPDFFactura(string gv_vbeln, string formato, string user, string pass)
        {
            try
            {
                RfcDestination sap = RFCFunc.GetCnx(user, pass);
                RfcRepository repo = sap.Repository;
                string name = "";
                //Función para consultas SQL a tablas
                if (formato == "CARTA") { name = "ZSD_FAC_CORPORATIVA"; }
                else { name = "ZSF_YBAA_SDINV_LR_EJ_IVA_WEB"; }  //ZSF_YBAA_SDINV_LR_EJ_IVA

                IRfcFunction sd = repo.CreateFunction("ZRFC_SMARTFORM_FACTURA");
                sd.SetValue("GV_VBELN", gv_vbeln); //'Tabla sobre la que queremos buscar
                sd.SetValue("GV_FORM_NAME", name); //'Delimitador de valores

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string bin = sd.GetValue("EV_PDF_BASE_64").ToString();
                byte[] pdf = Convert.FromBase64String(bin);
                RfcSessionManager.EndContext(sap);

                return pdf;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
                throw;
            }
        }

        [WebMethod]
        public byte[] TraerPDFOferta(string gv_vbeln, string user, string pass)
        {
            try { 
            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            RfcRepository repo = sap.Repository;
                //Función para consultas SQL a tablas
                IRfcFunction sd = repo.CreateFunction("ZRFC_SMARTFORM_OFERTA");
                sd.SetValue("GV_VBELN", gv_vbeln); //'Tabla sobre la que queremos buscar
                sd.SetValue("GV_FORM_NAME", "Z32_SF_YBAA_SDQUO_NSF"); //'Delimitador de valores

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string bin = sd.GetValue("EV_PDF_BASE_64").ToString();
                byte[] pdf = Convert.FromBase64String(bin);
                RfcSessionManager.EndContext(sap);

                return pdf;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
                throw;
            }
        }

        [WebMethod]
        public byte[] TraerPDFEtiqueta(string gv_vbeln, string gv_page, string gv_cant)
        {
            try
            {
                RfcDestination sap = RFCFunc.GetCnx();
                RfcRepository repo = sap.Repository;
                //Función para consultas SQL a tablas
                IRfcFunction sd = repo.CreateFunction("ZRFC_SMARTFORM_ETIQUETA");
                sd.SetValue("I_VBELN", gv_vbeln); //'Tabla sobre la que queremos buscar
                sd.SetValue("I_PAGE", gv_page); //'Tabla sobre la que queremos buscar
                sd.SetValue("I_CANT", gv_cant); //'Tabla sobre la que queremos buscar

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                string bin = sd.GetValue("EV_PDF_BASE_64").ToString();
                byte[] pdf = Convert.FromBase64String(bin);
                RfcSessionManager.EndContext(sap);

                return pdf;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
                throw;
            }
        }

        [WebMethod]
        public string CuentasPorCobrar(string gv_kunnr, string gv_vkorg, string gv_date, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;
                string result = "";
                IRfcFunction sd = repo.CreateFunction("ZRFC_OBT_CXC_X_CLIENTE_FBL5N"); //ZRFC_OBT_CXC_X_CLIENTE_FBL5N
                sd.SetValue("IV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("IV_BUKRS", gv_vkorg);
                sd.SetValue("IV_DATE", gv_date);
                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                //IRfcTable ret = sd.GetTable("GT_SALIDA");
                IRfcTable ret = sd.GetTable("GT_SALIDA");

                RfcSessionManager.EndContext(sap);

                DataTable dt = Utiles.ConvertRFCTableToDataTable(ret);


                return Utiles.DataTableToJson(dt);
                //return gv_resultado;

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string TraerDocumentoSD(string gv_vbeln, string user, string pass)
        {

            RfcDestination sap = RFCFunc.GetCnx(user, pass);
            try
            {
                RfcRepository repo = sap.Repository;
                string result = "";
                IRfcFunction sd = repo.CreateFunction("BAPISDORDER_GETDETAILEDLIST");
                //Función para consultas SQL a tablas
                IRfcFunction sd2 = repo.CreateFunction("RFC_READ_TABLE");

                IRfcStructure st_header = sd.GetStructure("I_BAPI_VIEW");
                st_header.SetValue("HEADER", 'X');
                st_header.SetValue("ITEM", 'X');
                st_header.SetValue("SDSCHEDULE", 'X');
                st_header.SetValue("BUSINESS", 'X');
                st_header.SetValue("PARTNER", 'X');
                st_header.SetValue("ADDRESS", 'X');
                st_header.SetValue("STATUS_H", 'X');
                st_header.SetValue("STATUS_I", 'X');
                st_header.SetValue("SDCOND", 'X');
                st_header.SetValue("SDCOND_ADD", 'X');
                st_header.SetValue("CONTRACT", 'X');
                st_header.SetValue("TEXT", 'X');
                st_header.SetValue("FLOW", 'X');
                st_header.SetValue("BILLPLAN", 'X');
                st_header.SetValue("CONFIGURE", 'X');
                st_header.SetValue("CREDCARD", 'X');
                st_header.SetValue("INCOMP_LOG", 'X');
                sd.SetValue("I_BAPI_VIEW", st_header);
                sd.SetValue("I_MEMORY_READ", 'X');
                sd.SetValue("I_WITH_HEADER_CONDITIONS", 'X');

                IRfcTable docs = sd.GetTable("SALES_DOCUMENTS");
                docs.Append();
                docs.SetValue("VBELN", Utiles.LlenarCadena(gv_vbeln, 10, '0'));
                sd.SetValue("SALES_DOCUMENTS", docs);

                sd2.SetValue("QUERY_TABLE", "VBPA3"); //'Tabla sobre la que queremos buscar
                sd2.SetValue("DELIMITER", ";"); //'Delimitador de valores

                //'Campos que queremos mostrar
                IRfcTable tblFields = sd2.GetTable("FIELDS");
                tblFields.Append();
                tblFields.SetValue("FIELDNAME", "STCD1");

                //'Condición WHERE para realizar la busqueda
                IRfcTable tblOptions = sd2.GetTable("OPTIONS");
                tblOptions.Append();
                tblOptions.SetValue("TEXT", "VBELN EQ '" + Utiles.LlenarCadena(gv_vbeln, 10, '0') + "'");
                tblOptions.Append();
                tblOptions.SetValue("TEXT", "AND PARVW EQ 'AG'");

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                sd2.Invoke(sap);

                IRfcTable r_header = sd.GetTable("ORDER_HEADERS_OUT");
                IRfcTable r_items = sd.GetTable("ORDER_ITEMS_OUT");
                IRfcTable r_sched = sd.GetTable("ORDER_SCHEDULES_OUT");
                IRfcTable r_busines = sd.GetTable("ORDER_BUSINESS_OUT");
                IRfcTable r_partners = sd.GetTable("ORDER_PARTNERS_OUT");
                IRfcTable r_address = sd.GetTable("ORDER_ADDRESS_OUT");
                IRfcTable r_status_header = sd.GetTable("ORDER_STATUSHEADERS_OUT");
                IRfcTable r_status_items = sd.GetTable("ORDER_STATUSITEMS_OUT");
                IRfcTable r_conditiones = sd.GetTable("ORDER_CONDITIONS_OUT");
                IRfcTable r_cond_head = sd.GetTable("ORDER_COND_HEAD");
                IRfcTable r_cond_item = sd.GetTable("ORDER_COND_ITEM");
                IRfcTable r_text_head = sd.GetTable("ORDER_TEXTHEADERS_OUT");
                IRfcTable r_text_lines = sd.GetTable("ORDER_TEXTLINES_OUT");

                IRfcTable r_nit = sd2.GetTable("DATA");

                RfcSessionManager.EndContext(sap);

                if (r_header.RowCount > 0)
                {

                    IRfcStructure st_nit = r_nit.FirstOrDefault();

                    if (!(st_nit is null)) { result = gv_vbeln + "&" + Convert.ToInt64(st_nit.GetValue("WA")).ToString() + "&"; }
                    else { result = gv_vbeln + "&0&"; }
                    string header = "{";
                    string items = "[";
                    string txt_conditiones = "&[";
                    //Codigo de Cliente
                    IRfcStructure row_header = r_header.FirstOrDefault();
                    result += row_header["SOLD_TO"].GetValue().ToString() + "&";
                    header += "\"DOC_TYPE\":\"" + row_header["DOC_TYPE"].GetValue().ToString() + "\",";
                    header += "\"SALES_ORG\":" + row_header["SALES_ORG"].GetValue().ToString() + ",";
                    header += "\"DISTR_CHAN\":" + row_header["DISTR_CHAN"].GetValue().ToString() + ",";
                    header += "\"CANAL\":\"" + row_header["DIVISION"].GetValue().ToString() + "\",";
                    header += "\"SALES_GRP\":" + row_header["SALES_GRP"].GetValue().ToString() + ",";
                    header += "\"SALES_OFF\":\"" + row_header["SALES_OFF"].GetValue().ToString() + "\",";
                    header += "\"PURCH_DATE\":\"" + row_header["PURCH_DATE"].GetValue().ToString() + "\",";
                    header += "\"PMNTTRMS\":\"\",";
                    header += "\"QT_VALID_F\":\"" + row_header["QT_VALID_F"].GetValue().ToString() + "\",";
                    header += "\"QT_VALID_T\":\"" + row_header["QT_VALID_T"].GetValue().ToString() + "\",";
                    header += "\"DOC_DATE\":\"" + row_header["DOC_DATE"].GetValue().ToString() + "\",";
                    header += "\"CURRENCY\":\"" + row_header["CURRENCY"].GetValue().ToString() + "\",";
                    header += "\"REF_DOC\":\"" + row_header["REF_DOC"].GetValue().ToString() + "\",";
                    header += "\"REFDOC_CAT\":\"" + row_header["DOC_CAT_SD"].GetValue().ToString() + "\",";
                    header += "\"PURCH_NO\":\"" + row_header["PURCH_NO"].GetValue().ToString() + "\",";
                    header += "\"VERSION\":\"" + row_header["VERSION"].GetValue().ToString() + "\"}&";
                    //Codigo de Vendedor

                    foreach (IRfcStructure row in r_partners)
                    {
                        if (row["PARTN_ROLE"].GetValue().ToString() == "VE")
                        {
                            result += Convert.ToInt16(row["PERSON_NO"].GetValue()).ToString() + "&";
                        }
                    }
                    //Linea de Cabecera
                    result += header;
                    //Registros de linea
                    IRfcStructure last = r_items.Last();

                    DataTable dt_conditiones = Utiles.ConvertRFCTableToDataTable(r_conditiones);
                    //List<conditions> lst_conditiones = Utiles.ConvertDataTableToList<conditions>(dt_conditiones);
                    List<conditions> lst_conditiones = Utiles.ConvertTo<conditions>(dt_conditiones);

                    foreach (IRfcStructure row in r_items)
                    {
                        int posl = Convert.ToInt32(row["ITM_NUMBER"].GetValue().ToString());
                        items += "{\"ITM_NUMBER\":" + posl.ToString() + ",";
                        items += "\"MATERIAL\":\"" + Convert.ToInt64(row["MATERIAL"].GetValue()).ToString() + "\",";
                        items += "\"PLANT\":\"" + row["PLANT"].GetValue().ToString() + "\",";
                        items += "\"STORE_LOC\":\"" + row["STGE_LOC"].GetValue().ToString() + "\",";
                        items += "\"TARGET_QTY\":" + row["REQ_QTY"].GetValue().ToString().Replace(",", ".") + ",";
                        items += "\"SHORT_TEXT\":\"" + row["SHORT_TEXT"].GetValue().ToString() + "\",";
                        items += "\"PROFIT_CTR\":\"" + Convert.ToInt32(row["PROFIT_CTR"].GetValue()).ToString() + "\",";
                        items += "\"REASON_REJ\":\"" + row["REA_FOR_RE"].GetValue().ToString() + "\",";
                        items += "\"REF_DOC\":\"" + row["REF_DOC"].GetValue().ToString() + "\",";
                        items += "\"REF_DOC_IT\":\"" + row["POSNR_VOR"].GetValue().ToString() + "\",";
                        items += "\"REF_DOC_CA\":\"" + row["DOC_CAT_SD"].GetValue().ToString() + "\"}";
                        if (!(row.Equals(last)))
                        {
                            items += ",";
                        }
                        //buscar condiciones de venta
                        foreach (conditions rowc in lst_conditiones.Where(p => p.ITM_NUMBER == row["ITM_NUMBER"].GetValue().ToString()))
                        {
                            if (((rowc.COND_TYPE == "ZPR0") || (rowc.COND_TYPE == "ZP01") || (rowc.COND_TYPE == "ZPBA") ||
                                ((!(string.IsNullOrEmpty(rowc.COND_TYPE))) &&
                                (Convert.ToInt32(rowc.COND_ST_NO) >= 26 && Convert.ToInt32(rowc.COND_ST_NO) <= 39))) &&
                                (string.IsNullOrEmpty(rowc.CONDISACTI)) && (!(Convert.ToDouble(rowc.COND_VALUE) == 0)))
                            {
                                int pos = Convert.ToInt32(rowc.ITM_NUMBER.ToString());
                                double total, condbase, cond_value;
                                txt_conditiones += "{\"ITM_NUMBER\":" + pos.ToString() + ",\"COND_TYPE\":\"" + rowc.COND_TYPE.ToString() + "\",\"COND_VALUE\":";
                                condbase = Convert.ToDouble(rowc.CONBASEVAL);
                                cond_value = Convert.ToDouble(rowc.COND_VALUE);
                                if (rowc.CALCTYPCON == "A")
                                {
                                    total = condbase * (cond_value / 100);
                                }
                                else { total = condbase * cond_value; }
                                txt_conditiones += total.ToString().Replace(",", ".") + ",\"CURRENCY\":\"BOB\"},";
                            }
                        }
                    }
                    items += "]";
                    result += items;
                    //Registros de Condiciones
                    txt_conditiones += "]";
                    if (txt_conditiones.Substring((txt_conditiones.Length - 2), 1) == ",")
                    { txt_conditiones = txt_conditiones.Remove((txt_conditiones.Length - 2), 1); }
                    else { txt_conditiones = txt_conditiones.Substring((txt_conditiones.Length - 2), 1); }
                    if (!(txt_conditiones == "["))
                    {
                        result += txt_conditiones;
                    }                    
                    //Textos
                    DataTable dt_txt_head = Utiles.ConvertRFCTableToDataTable(r_text_head);
                    List<txt_head> lst_txt_head = Utiles.ConvertDataTableToList<txt_head>(dt_txt_head);
                    DataTable dt_txt_line = Utiles.ConvertRFCTableToDataTable(r_text_lines);
                    List<txt_line> lst_txt_line = Utiles.ConvertDataTableToList<txt_line>(dt_txt_line);
                    string textos = "&[";
                    if (r_text_head.Count > 0)
                    {
                        string tipo = "C";
                        string linea = "00";
                        string texto = "";
                        if (lst_txt_head.Where(n => n.APPLOBJECT == "VBBK").Count() > 0)
                        {
                            foreach (txt_head t in lst_txt_head.Where(n => n.APPLOBJECT == "VBBK"))
                            {
                                texto += "{\"tipo\":\"" + tipo + "\",\"linea\":\"" + linea + "\", \"texto\":\"";
                                foreach (txt_line l in lst_txt_line.Where(n => n.APPLOBJECT == "VBBK"))
                                {
                                    texto += l.LINE;
                                }
                                texto += "\"},";
                            }
                            textos += texto;
                        }
                        if (lst_txt_head.Where(n => n.APPLOBJECT == "VBBP").Count() > 0)
                        {
                            foreach (txt_head t in lst_txt_head.Where(n => n.APPLOBJECT == "VBBP"))
                            {
                                tipo = "L";
                                linea = Convert.ToInt32(t.ITM_NUMBER).ToString();
                                texto = "{\"tipo\":\"" + tipo + "\",\"linea\":\"" + linea + "\", \"texto\":\"";
                                foreach (txt_line l in lst_txt_line.Where(n => n.TEXT_NAME == (t.SD_DOC + t.ITM_NUMBER)))
                                {
                                    texto += l.LINE;
                                }
                                textos += texto + "\"},";
                            }
                        }
                        if (textos.Substring((textos.Length - 1), 1) == ",")
                        { textos = textos.Remove((textos.Length - 1), 1); }
                        else
                        {
                            textos = textos.Substring((textos.Length - 1), 1);
                        }
                    }
                    textos += "]";
                    result += textos;
                }

                return result;

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string WMUpdMaxMin(string gv_matnr, string gv_lgnum, string gv_lgtyp, string gv_lgpla, Int32 gv_lpmax, Int32 gv_lpmin, Int32 gv_nsmng)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_WM_UPD_STG_MAX_MIN");
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_LGNUM", gv_lgnum);
                sd.SetValue("GV_LGTYP", gv_lgtyp);
                sd.SetValue("GV_LGPLA", gv_lgpla);
                sd.SetValue("GV_LPMAX", gv_lpmax);
                sd.SetValue("GV_LPMIN", gv_lpmin);
                sd.SetValue("GV_NSMNG", gv_nsmng);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = sd.GetValue("RETURN").ToString();

                RfcSessionManager.EndContext(sap);
                return gv_resultado;
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]//zrfc_wm_upd_weight
        public string WMUpdEAN(string gv_matnr, string gv_meins, string gv_ean11)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_WM_UPD_EAN");
                if (gv_meins == "UN") { gv_meins = "ST"; }
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_MEINS", gv_meins);
                sd.SetValue("GV_EAN11", gv_ean11);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = "1";
                gv_resultado = sd.GetValue("RETURN").ToString();

                RfcSessionManager.EndContext(sap);
                return gv_resultado;
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]//zrfc_wm_upd_weight
        public string WMUpdPeso(string gv_matnr, string gv_meins, string gv_brgew)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_WM_UPD_WEIGHT");
                if (gv_meins == "UN") { gv_meins = "ST"; }
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_MEINS", gv_meins);
                sd.SetValue("GV_BRGEW", gv_brgew);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string gv_resultado = "1";
                gv_resultado = sd.GetValue("RETURN").ToString();

                RfcSessionManager.EndContext(sap);
                return gv_resultado;
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        void LlenarTablaRango(string cad, int relleno, IRfcTable tb) {
            DataTable dt = new DataTable();
            dt = Utiles.ConvertJSONToDataTable(cad);
            foreach (DataRow oRow in dt.Rows)
            {
                tb.Append();
                tb.SetValue("SIGN", oRow["SIGN"].ToString());
                tb.SetValue("OPTION", oRow["OPTION"].ToString());
                tb.SetValue("LOW", Utiles.LlenarCadena(oRow["LOW"].ToString(), relleno, '0'));
                if (!(oRow["HIGH"].ToString().Trim() == ""))
                { tb.SetValue("HIGH",  Utiles.LlenarCadena(oRow["HIGH"].ToString(), relleno, '0')); }
            }
        }

        void Llenar_Gs_Header(IRfcFunction sd, string header)
        {
            string error;
            try
            {
                
                DataTable dt = new DataTable();
                header = header.Replace(@"\", "");
                dt = Utiles.ConvertJSONToDataTable(header);
                if (!(dt.Rows.Count == 0))
                {
                    IRfcStructure st_header = sd.GetStructure("GS_HEADER");
                    st_header.SetValue("DOC_TYPE", dt.Rows[0]["DOC_TYPE"].ToString());
                    st_header.SetValue("SALES_ORG", dt.Rows[0]["SALES_ORG"].ToString());
                    st_header.SetValue("DISTR_CHAN", dt.Rows[0]["DISTR_CHAN"].ToString());
                    st_header.SetValue("DIVISION", dt.Rows[0]["DIVISION"].ToString());
                    st_header.SetValue("SALES_GRP", dt.Rows[0]["SALES_GRP"].ToString());
                    st_header.SetValue("SALES_OFF", dt.Rows[0]["SALES_OFF"].ToString());
                    st_header.SetValue("PURCH_DATE", dt.Rows[0]["PURCH_DATE"].ToString());
                    st_header.SetValue("PMNTTRMS", dt.Rows[0]["PMNTTRMS"].ToString());
                    st_header.SetValue("QT_VALID_F", dt.Rows[0]["QT_VALID_F"].ToString());
                    st_header.SetValue("QT_VALID_T", dt.Rows[0]["QT_VALID_T"].ToString());
                    st_header.SetValue("DOC_DATE", dt.Rows[0]["DOC_DATE"].ToString());
                    st_header.SetValue("CURRENCY", dt.Rows[0]["CURRENCY"].ToString());
                    if (!(dt.Rows[0]["REF_DOC"].ToString() == "")) { st_header.SetValue("REF_DOC", Utiles.LlenarCadena(dt.Rows[0]["REF_DOC"].ToString(),10,'0')); }
                    else { st_header.SetValue("REF_DOC", dt.Rows[0]["REF_DOC"].ToString()); }
                    st_header.SetValue("REFDOC_CAT", dt.Rows[0]["REFDOC_CAT"].ToString());
                    st_header.SetValue("PURCH_NO_C", dt.Rows[0]["PURCH_NO_C"].ToString());
                    st_header.SetValue("VERSION", dt.Rows[0]["VERSION"].ToString());
                    sd.SetValue("GS_HEADER", st_header);
                }
            }
            catch (ArgumentOutOfRangeException aex)
            {
                error = aex.ToString();
                throw;
            }
            catch (IndexOutOfRangeException iex)
            {
                error = iex.ToString();
                throw;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                throw;
            }
        }

        void Llenar_Gt_Address_Partner(IRfcFunction sd, string kunnr, string address)
        {
            try
            {
                
                DataTable dt = new DataTable();
                address = address.Replace(@"\", "");
                dt = Utiles.ConvertJSONToDataTable(address);
                if (!(dt.Rows.Count == 0))
                {
                    //            IRfcTable gt_partner = sd.GetTable("INQUIRY_PARTNERS");
                    //            IRfcTable gt_address = sd.GetTable("PARTNERADDRESSES");

                    //            gt_partner.Append();
                    //            gt_partner.SetValue("PARTN_ROLE", "AG");
                    //            gt_partner.SetValue("PARTN_NUMB", Utiles.LlenarCadena(kunnr, 10, '0'));
                    //            gt_partner.SetValue("ADDR_LINK", Utiles.LlenarCadena(kunnr, 10, '0'));
                    //            gt_partner.SetValue("REFOBJTYPE", "BAPIPARNR");

                    //            gt_partner.Append();
                    //            gt_partner.SetValue("PARTN_ROLE", "WE");

                    //            gt_partner.Append();
                    //            gt_partner.SetValue("PARTN_ROLE", "VE");
                    //            gt_partner.SetValue("PARTN_NUMB", pernr);
                    //            gt_partner.SetValue("ADDR_LINK", "");
                    //            gt_partner.SetValue("REFOBJTYPE", "BAPIPARNR");
                    IRfcStructure st_address = sd.GetStructure("GS_ADDRESS");
                    st_address.SetValue("ADDR_NO", Utiles.LlenarCadena(kunnr, 10, '0'));
                    st_address.SetValue("NAME", dt.Rows[0]["NAME"].ToString());
                    st_address.SetValue("NAME_2", dt.Rows[0]["NAME2"].ToString());
                    st_address.SetValue("NAME_3", dt.Rows[0]["NAME3"].ToString());
                    st_address.SetValue("NAME_4", dt.Rows[0]["NAME4"].ToString());
                    st_address.SetValue("STREET", dt.Rows[0]["STREET"].ToString());
                    st_address.SetValue("STR_SUPPL1", dt.Rows[0]["STR_SUPPL1"].ToString());
                    st_address.SetValue("STR_SUPPL2", dt.Rows[0]["STR_SUPPL2"].ToString());
                    st_address.SetValue("LOCATION", dt.Rows[0]["LOCATION"].ToString());
                    st_address.SetValue("STR_SUPPL3", dt.Rows[0]["STR_SUPPL3"].ToString());
                    st_address.SetValue("COUNTRY", dt.Rows[0]["COUNTRY"].ToString());
                    st_address.SetValue("CITY", dt.Rows[0]["CITY"].ToString());
                    st_address.SetValue("REGION", dt.Rows[0]["REGION"].ToString());
                    st_address.SetValue("SORT1", dt.Rows[0]["SORT1"].ToString());
                    st_address.SetValue("TEL1_NUMBR", dt.Rows[0]["TEL1_NUMBR"].ToString());
                    st_address.SetValue("E_MAIL", dt.Rows[0]["E_MAIL"].ToString());
                    sd.SetValue("GS_ADDRESS", st_address);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void Llenar_Gt_Items(IRfcFunction sd, string items)
        {
            try
            {
                DataTable dt = new DataTable();
                items = items.Replace(@"\", "");
                dt = Utiles.ConvertJSONToDataTable(items);
                if (!(dt.Rows.Count == 0))
                {
                    IRfcTable gt = sd.GetTable("GT_ITEMS");

                    for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                    {
                        gt.Append();
                        gt.SetValue("ITM_NUMBER", dt.Rows[i]["ITM_NUMBER"].ToString());
                        gt.SetValue("MATERIAL", Utiles.LlenarCadena(dt.Rows[i]["MATERIAL"].ToString(), 18, '0'));
                        gt.SetValue("PLANT", dt.Rows[i]["PLANT"].ToString());
                        gt.SetValue("STORE_LOC", dt.Rows[i]["STORE_LOC"].ToString());
                        gt.SetValue("TARGET_QTY", Convert.ToDouble(dt.Rows[i]["TARGET_QTY"].ToString()));
                        gt.SetValue("SHORT_TEXT", dt.Rows[i]["SHORT_TEXT"].ToString());
                        gt.SetValue("REFOBJTYPE", "BAPISDITM");
                        gt.SetValue("PROFIT_CTR", Utiles.LlenarCadena(dt.Rows[i]["PROFIT_CTR"].ToString(), 10, '0'));
                        gt.SetValue("REASON_REJ", dt.Rows[i]["REASON_REJ"].ToString()); //CAMPO PARA RECHAZO DE LÍNEA
                        if (!(dt.Rows[i]["REF_DOC"].ToString() == "")) { gt.SetValue("REF_DOC", Utiles.LlenarCadena(dt.Rows[i]["REF_DOC"].ToString(), 10, '0')); }
                        else { gt.SetValue("REF_DOC", dt.Rows[i]["REF_DOC"].ToString()); }
                        //gt.SetValue("REF_DOC", Utiles.LlenarCadena(dt.Rows[i][8].ToString(), 10, '0'));
                        gt.SetValue("REF_DOC_IT", dt.Rows[i]["REF_DOC_IT"].ToString());
                        gt.SetValue("REF_DOC_CA", dt.Rows[i]["REF_DOC_CA"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
    
        }

        void Llenar_Gt_Schedules(IRfcFunction sd, string schedules)
        {
            DataTable dt = new DataTable();
            schedules = schedules.Replace(@"\", "");
            dt = Utiles.ConvertJSONToDataTable(schedules);
            if (!(dt.Rows.Count == 0))
            {
                IRfcTable gt = sd.GetTable("GT_SCHEDULES");

                for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                {
                    gt.Append();
                    gt.SetValue("ITM_NUMBER", dt.Rows[i][0].ToString());
                    gt.SetValue("REQ_DATE", dt.Rows[i][1].ToString());
                    gt.SetValue("REQ_TIME", "12:00:00");
                    gt.SetValue("REQ_QTY", Convert.ToDouble(dt.Rows[i][2].ToString()));
                    gt.SetValue("TP_TIME", "12:00:00");
                    gt.SetValue("MS_TIME", "12:00:00");
                    gt.SetValue("LOAD_TIME", "12:00:00");
                    gt.SetValue("GI_TIME", "12:00:00");
                    gt.SetValue("REFOBJTYPE", "BAPISCHDL");
                    gt.SetValue("DLV_TIME", "12:00:00");
                }
            }
        }

        void Llenar_Gt_Conditions(IRfcFunction sd, string conditions)
        {
            DataTable dt = new DataTable();
            conditions = conditions.Replace(@"\", "");
            dt = Utiles.ConvertJSONToDataTable(conditions);
            if (!(dt.Rows.Count == 0))
            {
                IRfcTable gt = sd.GetTable("GT_CONDITIONS");

                for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                {
                    gt.Append();
                    gt.SetValue("ITM_NUMBER", dt.Rows[i][0].ToString());
                    gt.SetValue("COND_TYPE", dt.Rows[i][1].ToString());
                    gt.SetValue("COND_VALUE", Convert.ToDouble(dt.Rows[i][2].ToString()));
                    gt.SetValue("CURRENCY","BOB");
                    gt.SetValue("REFOBJTYPE", "BAPICOND");
                }
            }
        }
    }
}