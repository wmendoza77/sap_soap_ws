using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using SAP.Middleware.Connector;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel;

namespace functions.App_Code
{
    public static class Utiles
    {
        /// <summary>
        /// Rellena un string a la cantidad de caracteres ingresados, con el caracter de parámetro.
        /// </summary>
        /// <param name="txt">String a rellenar</param>
        /// <param name="cant">Logintud de la salida</param>
        /// <param name="caracter">Carácter de relleno</param>
        /// <returns></returns>
        public static string LlenarCadena(string txt, int cant, char caracter) {
            string sal = txt;
            int l = txt.Length;
            for (int i = 0; (i <= ((cant - l) - 1)); i++)
            {
                sal = (caracter + sal);
            }
            return sal;
        }

        /// <summary>
        /// Convierte un string JSON a un DataTable
        /// </summary>
        /// <param name="jsonString">String JSON</param>
        /// <returns></returns>
        public static DataTable ConvertJSONToDataTable(string jsonString) {
            DataTable dt = new DataTable();

            string[] stringSeparators = new string[] { "},{" };
            string[] jsonParts = jsonString.Replace("[", "").Replace("]", "").Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            List<string> dtColumns = new List<string>();

            foreach (string jp in jsonParts)
            {
                //only loop thru once to get column names
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");

                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1);
                        string v = rowData.Substring(idx + 1);
                        if (!dtColumns.Contains(n))
                        {
                            dtColumns.Add(n.Replace("\"", ""));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                    }

                }
                break; // TODO: might not be correct. Was : Exit For
            }

            //build dt
            foreach (string c in dtColumns)
            {
                dt.Columns.Add(c);
            }
            //get table data
            foreach (string jp in jsonParts)
            {
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string v = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[n] = v;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }

                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        /// <summary>
        /// Devuelve un String con formato JSON con un dataTable de parámetro
        /// </summary>
        /// <param name="dt">DataTable a serializar</param>
        /// <returns></returns>
        public static string GetJson(DataTable dt) {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> packet = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.Trim(), dr[col]);
                }
                packet.Add(row);
            }
            return serializer.Serialize(packet);
        }

        /// <summary>
        /// Convierte un RFCTable a un dataTable
        /// </summary>
        /// <param name="rfcTbl">RFCTable a convertir</param>
        /// <returns></returns>
        public static DataTable ConvertRFCTableToDataTable(IRfcTable rfcTbl)
        {
            DataTable dt = new DataTable();
            for (int ic = 0; (ic
                        <= (rfcTbl.ElementCount - 1)); ic++)
            {
                RfcElementMetadata metadata;
                metadata = rfcTbl.GetElementMetadata(ic);
                dt.Columns.Add(metadata.Name);
            }

            foreach (IRfcStructure row in rfcTbl)
            {
                DataRow dr;
                dr = dt.NewRow();
                for (int i = 0; (i
                            <= (rfcTbl.ElementCount - 1)); i++)
                {
                    RfcElementMetadata metadata;
                    metadata = rfcTbl.GetElementMetadata(i);
                    dr[metadata.Name] = row.GetString(metadata.Name);
                }

                dt.Rows.Add(dr);
            }

            return dt;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myrefcTable"></param>
        /// <returns></returns>
        public static DataTable ConvertRFCStructureToDataTable(IRfcStructure myrefcTable)  //Convert RFCStructure to datatable

        {

            DataTable rowTable = new DataTable();

            for (int i = 0; i <= myrefcTable.ElementCount - 1; i++)

            {

                rowTable.Columns.Add(myrefcTable.GetElementMetadata(i).Name);

            }

            DataRow row = rowTable.NewRow();

            for (int j = 0; j <= myrefcTable.ElementCount - 1; j++)

            {

                row[j] = myrefcTable.GetValue(j);

            }

            rowTable.Rows.Add(row);

            return rowTable;

        }

        public static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();
            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }
            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }
            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }
            return table;
        }
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }

    public class txt_head
    {
        public string OPERATION { get; set; }
        public string SD_DOC { get; set; }
        public string ITM_NUMBER { get; set; }
        public string APPLOBJECT { get; set; }
        public string TEXT_NAME { get; set; }
        public string TEXT_ID { get; set; }
        public string LANGU { get; set; }
        public string LANGU_ISO { get; set; }
        public string TITLE { get; set; }
        public string FORM { get; set; }
        public string STYLE { get; set; }
        public string VERSION { get; set; }
        public string CREATEUSER { get; set; }
        public string CREATEREL { get; set; }
        public string CREATEDATE { get; set; }
        public string CREATETIME { get; set; }
        public string CHANGEUSER { get; set; }
        public string CHANGEREL { get; set; }
        public string CHANGEDATE { get; set; }
        public string CHANGETIME { get; set; }
        public string LINESIZE { get; set; }
        public string LINEAMOUNT { get; set; }
        public string HYPHENAT { get; set; }
        public string ORGSPRAS { get; set; }
        public string ORGSPRAISO { get; set; }
        public string TRANSTAT { get; set; }
        public string MATCHCOD1 { get; set; }
        public string MATCHCOD2 { get; set; }
        public string REFOBJECT { get; set; }
        public string REFNAME { get; set; }
        public string REFID { get; set; }
        public string TEXTTYPE { get; set; }
        public string COMPRESSTD { get; set; }
        public string OBJ_CLASS { get; set; }
    }

    public class txt_line
    {
        public string OPERATION { get; set; }
        public string APPLOBJECT { get; set; }
        public string TEXT_NAME { get; set; }
        public string TEXT_ID { get; set; }
        public string LANGU { get; set; }
        public string LANGU_ISO { get; set; }
        public string LINE_CNT { get; set; }
        public string LINE { get; set; }
        public string FORMAT_COL { get; set; }
    }

    public class conditions
    {
        public string OPERATION { get; set; }
        public string SD_DOC { get; set; }
        public string CONDIT_NO { get; set; }
        public string ITM_NUMBER { get; set; }
        public string COND_ST_NO { get; set; }
        public string COND_COUNT { get; set; } 
        public string APPLICATIO { get; set; }
        public string COND_TYPE { get; set; }
        public string CONPRICDAT { get; set; }
        public string CALCTYPCON { get; set; }
        public string CONBASEVAL { get; set; }
        public string SDCURRENCY { get; set; }
        public string CURREN_ISO { get; set; }
        public string COND_VALUE { get; set; }
        public string CURRENCY { get; set; }
        public string CURRENCISO { get; set; }
        public string CONEXCHRAT { get; set; }
        public string COND_P_UNT { get; set; }
        public string COND_D_UNT { get; set; }
        public string T_UNIT_ISO { get; set; }
        public string NUMCONVERT { get; set; }
        public string DENOMINATO { get; set; }
        public string CONDTYPE { get; set; }
        public string STAT_CON { get; set; }
        public string SCALETYPE { get; set; }
        public string ACCRUALS { get; set; }
        public string CONINVOLST { get; set; }
        public string CONDORIGIN { get; set; }
        public string GROUPCOND { get; set; }
        public string COND_UPDAT { get; set; }
        public string ACCESS_SEQ { get; set; }
        public string COND_NO { get; set; }
        public string CONDCOUNT { get; set; }
        public string ACCOUNTKEY { get; set; }
        public string GL_ACCOUNT { get; set; }
        public string TAX_CODE { get; set; }
        public string ACCOUNT_KE { get; set; }
        public string GLACCOUNT { get; set; }
        public string WITHTAXCOD { get; set; }
        public string VENDOR_NO { get; set; }
        public string CUSTNO_RR { get; set; }
        public string ROUNDOFFDI { get; set; }
        public string CONDVALUE { get; set; }
        public string CONDCNTRL { get; set; }
        public string CONDISACTI { get; set; }
        public string CONDCLASS { get; set; }
        public string CONDCOINHD { get; set; }
        public string INDIBASVAL { get; set; }
        public string INDICONVAL { get; set; }
        public string FACTBASVAL { get; set; }
        public string STRUCTCOND { get; set; }
        public string FACTCONBAS { get; set; }
        public string PRICELEVEL { get; set; }
        public string CONDFORMUL { get; set; }
        public string SCALETYP { get; set; }
        public string INCREMSCAL { get; set; }
        public string INCREASCAL { get; set; }
        public string INDEX_NO { get; set; }
        public string INDEXNO { get; set; }
        public string CONDITIDX { get; set; }
        public string PRINT_ID { get; set; }
        public string FROREFSTEP { get; set; }
        public string FROMREFSTE { get; set; }
        public string CONDSUBTOT { get; set; }
        public string COND_FORM { get; set; }
        public string COND_FORM1 { get; set; }
        public string MAKMANENTR { get; set; }
        public string ROUNDRULE { get; set; }
        public string PMSIGNAMOU { get; set; }
        public string CURRCONVER { get; set; }
        public string CONDISMAND { get; set; }
        public string RATEOFCHA1 { get; set; }
        public string RATEOFCHA2 { get; set; }
        public string RATEOFCHA3 { get; set; }
        public string RATEOFCHA4 { get; set; }
        public string RATEOFCHA5 { get; set; }
        public string RATEOFCHA6 { get; set; }
        public string TERMSOFPAY { get; set; }
        public string MESSAGENO { get; set; }
        public string INDIUPDATE { get; set; }
        public string SELECTION { get; set; }
        public string SCALEBASIN { get; set; }
        public string SCALBASVAL { get; set; }
        public string UNITMEASUR { get; set; }
        public string TUNITISO { get; set; }
        public string CURRENCKEY { get; set; }
        public string CORRENISO { get; set; }
        public string CONDCURREN { get; set; }
        public string CORR_ISO { get; set; }
        public string CONBASVAL { get; set; }
        public string CONDIVALUE { get; set; }
        public string CONDINCOMP { get; set; }
        public string CONDCONFIG { get; set; }
        public string CONDCHAMAN { get; set; }
        public string PRICESOURC { get; set; }
        public string VARIANCOND { get; set; }
        public string LEVEL_BOM { get; set; } 
        public string PATH_BOM { get; set; }
        public string STATOFAGRE { get; set; }
        public string REB_RETROA { get; set; }
        public string INDIDELETE { get; set; }
        public string AGREE_COND { get; set; }
        public string CALCULBASE { get; set; }
        public string SH_MAT_TYP { get; set; }
        public string ROUNDDIFCO { get; set; }
        public string QTYCONVERS { get; set; }
        public string CONCBUFFER { get; set; } 
        public string RELACCASSI { get; set; }
        public string INDIMATMAI { get; set; }
        public string TAXJURISDI { get; set; }
        public string CONEXCHRAT_V { get; set; }
    }
}