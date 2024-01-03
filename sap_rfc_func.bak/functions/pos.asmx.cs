using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SAP.Middleware.Connector;
using functions.App_Code;
using System.Data;

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
        public string CrearOferta(string gv_kunnr, string gv_stcd1, string gv_pernr, string gs_header, string gt_items, string gt_schedules, string gt_conditions)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_OFERTA");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr,10,'0'));
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_PERNR", gv_pernr);
                Llenar_Gs_Header(sd, gs_header, "");
                Llenar_Gt_Items(sd, gt_items, "");
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                RfcSessionManager.EndContext(sap);
                return vbeln;

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }            
        }

        [WebMethod]
        public string CrearPedido(string gv_vbeln, string gv_kunnr, string gv_stcd1, string gv_pernr, string gs_header, string gt_items, string gt_schedules, string gt_conditions)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_PEDIDO");
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_PERNR", gv_pernr);
                Llenar_Gs_Header(sd, gs_header, gv_vbeln);
                Llenar_Gt_Items(sd, gt_items, gv_vbeln);
                Llenar_Gt_Schedules(sd, gt_schedules);
                Llenar_Gt_Conditions(sd, gt_conditions);
                //Llenar_Gt_PartnerAdd(sd, gt_partneradd);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);
                string vbeln = sd.GetValue("GV_VBELN").ToString();
                IRfcTable ret = sd.GetTable("GT_RETURN");

                RfcSessionManager.EndContext(sap);
                return vbeln;

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string BuscarMaterial(string gv_matnr, string gv_zz_descripcion, string gv_groes, string gv_zz_aplicacion, string gv_zeinr)
        {
            RfcDestination sap = RFCFunc.GetCnx();
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

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret));

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string BuscarCliente(string gv_kunnr, string gv_stcd1, string gv_name1, string gv_vkorg, string gv_vtweg, string gv_spart)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_BUSCAR_CLIENTE");
                if (!(gv_kunnr == "")){sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));}
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

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(ret));

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string StockPorCentro(string gv_matnr, string gv_werks)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("BAPI_MATERIAL_STOCK_REQ_LIST");
                sd.SetValue("MATERIAL", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("PLANT", gv_werks);
                
                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure mat_stock= sd.GetStructure("MRP_STOCK_DETAIL");

                RfcSessionManager.EndContext(sap);

                decimal libre_Utilesizacion = Convert.ToDecimal(mat_stock["UNRESTRICTED_STCK"].GetValue());
                decimal comprometido = Convert.ToDecimal(mat_stock["SALES_REQS"].GetValue());

                DataTable dt = new DataTable();
                dt.Columns.Add("MATERIAL");
                dt.Columns.Add("LIBRE_UTILIZACION");
                dt.Columns.Add("COMPROMETIDO");

                dt.Rows.Add(gv_matnr, libre_Utilesizacion, comprometido);

                return Utiles.GetJson(dt);
             

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string PrecioDeVenta(string gv_vkorg, string gv_vtweg, string gv_spart, string gv_kunnr, string gv_auart, string gv_matnr, string gv_werks, string gv_prsdt)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CONDICIONES_PRECIO");
                sd.SetValue("GV_VKORG", gv_vkorg);
                sd.SetValue("GV_VTWEG", gv_vtweg);
                sd.SetValue("GV_SPART", gv_spart);
                sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0'));
                sd.SetValue("GV_AUART", gv_auart.ToUpper());
                sd.SetValue("GV_MATNR", Utiles.LlenarCadena(gv_matnr, 18, '0'));
                sd.SetValue("GV_WERKS", gv_werks);
                sd.SetValue("GV_PRSDT", gv_prsdt);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcStructure st_komp = sd.GetStructure("GS_KOMP");
                IRfcTable tb_komv = sd.GetTable("GT_KOMV");

                RfcSessionManager.EndContext(sap);
                string zred = "";

                foreach (IRfcStructure row in tb_komv)
                {
                    if (row["KSCHL"].GetValue().ToString() == "ZRED") {
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

                return Utiles.GetJson(dt);


            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string ClientePOS(string gv_operacion, string gv_stcd1, string gv_name, string gv_brsch, string gv_phone, string gv_email, string gv_mob_phone, string gv_street, string gv_str_suppl3)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_ABM_CLIENTE");
                sd.SetValue("GV_OPERACION", gv_operacion);
                sd.SetValue("GV_STCD1", gv_stcd1);
                sd.SetValue("GV_NAME", gv_name);
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
                return gv_resultado;


            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
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
            if (!(tipo == "C")) {
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
                        if (texto.Length > 72) {
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
        public string TablaDsctosPOS(string usuario, string oficina)
        {
            RfcDestination sap = RFCFunc.GetCnx();
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


                return Utiles.GetJson(dt);


            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string ListarDocumentoSD(string clase, string vkbur, string kunnr, string matnr, string vbeln, string audat, string pernr)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_LISTAR_DOCS_SD");
                sd.SetValue("CLASE", clase);
                sd.SetValue("VKBUR", vkbur);

                IRfcTable tbMatnr = sd.GetTable("RG_MATNR");
                LlenarTablaRango(matnr, 18, tbMatnr);
                IRfcTable tbKunnr = sd.GetTable("RG_KUNNR");
                LlenarTablaRango(kunnr, 10, tbKunnr);
                IRfcTable tbVbeln = sd.GetTable("RG_VBELN");
                LlenarTablaRango(vbeln, 10, tbVbeln);
                IRfcTable tbAudat = sd.GetTable("RG_AUDAT");
                LlenarTablaRango(audat, 8, tbAudat);
                IRfcTable tbPernr = sd.GetTable("RG_PERNR");
                LlenarTablaRango(pernr, 8, tbPernr);

                RfcSessionManager.BeginContext(sap);

                sd.Invoke(sap);

                RfcSessionManager.EndContext(sap);

                IRfcTable resultado = sd.GetTable("RESULTADO");
                
                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(resultado));

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }

        [WebMethod]
        public string TraerDocumentoSD(string gv_vbeln)
        {
            RfcDestination sap = RFCFunc.GetCnx();
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
                docs.SetValue("VBELN", gv_vbeln);
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
                tblOptions.SetValue("TEXT", "VBELN EQ '" + gv_vbeln + "'");
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

                if (r_header.RowCount > 0) {

                    IRfcStructure st_nit = r_nit.FirstOrDefault();

                    if (!(st_nit is null)) { result = gv_vbeln + "&" + Convert.ToInt32(st_nit.GetValue("WA")).ToString() + "&"; }
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
                    header += "\"CURRENCY\":\"" + row_header["CURRENCY"].GetValue().ToString() + "\"}&";
                  
                    //Codigo de Vendedor
                    
                    foreach (IRfcStructure row in r_partners)
                    {
                        if (row["PARTN_ROLE"].GetValue().ToString() == "VE") {
                            result += Convert.ToInt16(row["PERSON_NO"].GetValue()).ToString() + "&";
                        } 
                    }
                    //Linea de Cabecera
                    result += header;
                    //Registros de linea
                    IRfcStructure last = r_items.Last();
                    
                    DataTable dt_conditiones = Utiles.ConvertRFCTableToDataTable(r_conditiones);
                    List<conditions> lst_conditiones = Utiles.ConvertDataTableToList<conditions>(dt_conditiones);
                    foreach (IRfcStructure row in r_items)
                    {
                        int posl = Convert.ToInt32(row["ITM_NUMBER"].GetValue().ToString());
                        items += "{\"ITM_NUMBER\":" + posl.ToString() + ",";
                        items += "\"MATERIAL\":\"" + Convert.ToInt64(row["MATERIAL"].GetValue()).ToString() + "\",";
                        items += "\"PLANT\":\"" + row["PLANT"].GetValue().ToString() + "\",";
                        items += "\"STORE_LOC\":\"" + row["STGE_LOC"].GetValue().ToString() + "\",";
                        items += "\"TARGET_QTY\":" + row["REQ_QTY"].GetValue().ToString().Replace(",",".") + ",";
                        items += "\"SHORT_TEXT\":\"" + row["SHORT_TEXT"].GetValue().ToString() + "\",";
                        items += "\"PROFIT_CTR\":\"" + Convert.ToInt32(row["PROFIT_CTR"].GetValue()).ToString() + "\",";
                        items += "\"REASON_REJ\":\"" + row["REA_FOR_RE"].GetValue().ToString() + "\"}";
                        if (!(row.Equals(last))) {
                            items += ",";
                        }
                        //buscar condiciones de venta
                        foreach (conditions rowc in lst_conditiones.Where(p => p.ITM_NUMBER == row["ITM_NUMBER"].GetValue().ToString()))
                        {
                            if (((rowc.COND_TYPE == "ZP01") || (Convert.ToInt32(rowc.COND_ST_NO) >= 30 && Convert.ToInt32(rowc.COND_ST_NO) <= 34)) && (rowc.CONDISACTI == ""))
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
                                txt_conditiones += total.ToString().Replace(",",".") + ",\"CURRENCY\":\"BOB\"},";
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
                    result += txt_conditiones;
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
        public string CreaTicket(string gv_ticket, string gv_oferta, string gv_pedido, string gv_caja, string gv_oficina, string gv_centro, string gv_cliente, string gv_vendor, string gv_validez_f, string gv_validez_t, string gv_cliente_pos, string gt_items)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_CREA_TICKET");
                if (!(gv_ticket is null)) { sd.SetValue("GV_TICKET", Utiles.LlenarCadena(gv_ticket, 10, '0')); }
                else { sd.SetValue("GV_TICKET", gv_ticket); }
                sd.SetValue("GV_OFERTA", gv_oferta);
                sd.SetValue("GV_PEDIDO", gv_pedido);
                sd.SetValue("GV_CAJA", Utiles.LlenarCadena(gv_caja,6,'0'));
                sd.SetValue("GV_OFICINA", gv_oficina);
                sd.SetValue("GV_CENTRO", gv_centro);
                sd.SetValue("GV_CLIENTE", Utiles.LlenarCadena(gv_cliente, 10, '0'));
                sd.SetValue("GV_VENDOR", Utiles.LlenarCadena(gv_vendor,8,'0'));
                sd.SetValue("GV_VALIDEZ_F", gv_validez_f);
                sd.SetValue("GV_VALIDEZ_T", gv_validez_t);
                if (!(gv_cliente_pos is null)) { sd.SetValue("GV_CLIENTE_POS", Utiles.LlenarCadena(gv_cliente_pos, 10, '0')); }
                else { sd.SetValue("GV_CLIENTE_POS", gv_cliente_pos); }

                IRfcTable tbMatnr = sd.GetTable("GT_ITEMS");
                DataTable dt = new DataTable();
                dt = Utiles.ConvertJSONToDataTable(gt_items);
                if (!(dt.Rows.Count == 0))
                {
                    IRfcTable gt = sd.GetTable("GT_ITEMS");

                    for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                    {
                        gt.Append();
                        gt.SetValue("TRX_ID", "");
                        gt.SetValue("TRX_POS", i + 1);
                        gt.SetValue("MATNR", Utiles.LlenarCadena(dt.Rows[i]["MATERIAL"].ToString(),18,'0'));
                        gt.SetValue("WERKS",   gv_centro);
                        gt.SetValue("LGORT", gv_centro);
                        gt.SetValue("MENGE_V", dt.Rows[i]["CANTIDAD"]);
                        gt.SetValue("MENGE_B", dt.Rows[i]["CANTIDAD"]);
                        gt.SetValue("UNIT_PRICE", dt.Rows[i]["PRECIO_UNIT"]);
                        gt.SetValue("UNIT_PRICE_D", dt.Rows[i]["PRECIO_UNIT"]);
                        gt.SetValue("DISC_PRICE", dt.Rows[i]["DSCTO"]);
                        if (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) > 0) {
                            gt.SetValue("MDISCOUNTP", (Math.Round((Convert.ToInt32(dt.Rows[i]["DSCTO"]) * 100) / (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])), 0)));
                            gt.SetValue("BASE_PRICE", (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])).ToString());
                            gt.SetValue("BASE_PRICE_D", (Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])).ToString());
                            gt.SetValue("TAX", ((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.13).ToString());
                            gt.SetValue("NETPR", ((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.87).ToString());
                            gt.SetValue("MDISC_PRICE", dt.Rows[i]["DSCTO"].ToString());
                            gt.SetValue("GROSS_PRICE", (((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.87) + ((Convert.ToDouble(dt.Rows[i]["PRECIO_UNIT"]) * Convert.ToInt32(dt.Rows[i]["CANTIDAD"])) * 0.13)).ToString());
                        }
                        gt.SetValue("WAERS", "BOB");
                        gt.SetValue("TAX_PERC", "130.00");
                        gt.SetValue("VGPOS", (i + 1) * 10 );
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
        public string BuscarOfertas(string gv_ticket, string gv_caja, string gv_nit, string gv_pernr, string gt_audat)
        {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_BUSCAR_OFERTAS");
                if (!(gv_ticket is null)) { sd.SetValue("GV_TICKET", Utiles.LlenarCadena(gv_ticket, 10, '0')); }
                else { sd.SetValue("GV_TICKET", gv_ticket); }
                sd.SetValue("GV_CAJA", Utiles.LlenarCadena(gv_caja, 6, '0'));
                sd.SetValue("GV_NIT", gv_nit);
                if (!(gv_pernr is null)) { sd.SetValue("GV_PERNR", Utiles.LlenarCadena(gv_pernr, 8, '0')); }
                else { sd.SetValue("GV_PERNR", gv_pernr); }

                IRfcTable tbAudat = sd.GetTable("RG_AUDAT");
                LlenarTablaRango(gt_audat, 8, tbAudat);

                RfcSessionManager.BeginContext(sap);

                sd.Invoke(sap);

                RfcSessionManager.EndContext(sap);

                IRfcTable resultado = sd.GetTable("GT_OFERTAS");

                return Utiles.GetJson(Utiles.ConvertRFCTableToDataTable(resultado));

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
                { tb.SetValue("HIGH", Utiles.LlenarCadena(oRow["HIGH"].ToString(), relleno, '0')); }
            }
        }

        void Llenar_Gs_Header(IRfcFunction sd, string header, string vbeln)
        {
            DataTable dt = new DataTable();
            dt = Utiles.ConvertJSONToDataTable(header);
            if (!(dt.Rows.Count == 0))
            {
                IRfcStructure st_header = sd.GetStructure("GS_HEADER");
                st_header.SetValue("DOC_TYPE", dt.Rows[0][0].ToString());
                st_header.SetValue("SALES_ORG", Convert.ToInt16(dt.Rows[0][1].ToString()));
                st_header.SetValue("DISTR_CHAN", Convert.ToInt16(dt.Rows[0][2].ToString()));
                st_header.SetValue("DIVISION", dt.Rows[0][3].ToString());
                st_header.SetValue("SALES_GRP", Convert.ToInt16(dt.Rows[0][4].ToString()));
                st_header.SetValue("SALES_OFF", dt.Rows[0][5].ToString());
                st_header.SetValue("PURCH_DATE", dt.Rows[0][6].ToString());
                st_header.SetValue("PMNTTRMS", dt.Rows[0][7].ToString());
                st_header.SetValue("QT_VALID_F", dt.Rows[0][8].ToString());
                st_header.SetValue("QT_VALID_T", dt.Rows[0][9].ToString());
                st_header.SetValue("DOC_DATE", dt.Rows[0][10].ToString());
                st_header.SetValue("CURRENCY", dt.Rows[0][11].ToString());
                
                if (!(vbeln == "")){
                    st_header.SetValue("DOC_TYPE", "ZPOS");
                    st_header.SetValue("PRICE_GRP", "01");
                    st_header.SetValue("PURCH_NO_C", "POS");
                    st_header.SetValue("DLVSCHDUSE", "M");
                    st_header.SetValue("REF_DOC", vbeln);
                    st_header.SetValue("REFDOC_CAT", "B");
                }

                    sd.SetValue("GS_HEADER", st_header);
                
            }
        }

        void Llenar_Gt_Items(IRfcFunction sd, string items, string vbeln)
        {
            DataTable dt = new DataTable();
            dt = Utiles.ConvertJSONToDataTable(items);
            if (!(dt.Rows.Count == 0))
            {
                IRfcTable gt = sd.GetTable("GT_ITEMS");

                for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                {
                    gt.Append();
                    gt.SetValue("ITM_NUMBER", dt.Rows[i][0].ToString());
                    gt.SetValue("MATERIAL", Utiles.LlenarCadena(dt.Rows[i][1].ToString(), 18, '0'));
                    gt.SetValue("PLANT", dt.Rows[i][2].ToString());
                    gt.SetValue("STORE_LOC", dt.Rows[i][3].ToString());
                    gt.SetValue("TARGET_QTY", Convert.ToDouble(dt.Rows[i][4].ToString()));
                    gt.SetValue("SHORT_TEXT", dt.Rows[i][5].ToString());
                    gt.SetValue("REFOBJTYPE", "BAPISDITM");
                    gt.SetValue("PROFIT_CTR", Utiles.LlenarCadena(dt.Rows[i][6].ToString(), 10, '0'));
                    gt.SetValue("REASON_REJ", dt.Rows[i][7].ToString()); //CAMPO PARA RECHAZO DE LÍNEA

                    if (!(vbeln == ""))
                    {
                        gt.SetValue("PRICE_GRP", "01");
                        gt.SetValue("CUST_GROUP", "Z1");
                        gt.SetValue("REF_DOC", vbeln);
                        gt.SetValue("REF_DOC_IT", dt.Rows[i][0].ToString());
                        gt.SetValue("REF_DOC_CA", "B");
                    }
                }
            }
        }

        void Llenar_Gt_Schedules(IRfcFunction sd, string schedules)
        {
            DataTable dt = new DataTable();
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