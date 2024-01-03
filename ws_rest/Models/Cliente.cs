using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_rest.Models
{
    public class Cliente
    {
        public string kunnr { get; set; }
        public string xcpdk { get; set; }
        public string customer_id { get; set; }
        public string stcd1 { get; set; }
        public string name { get; set; }
        public string brsch { get; set; }
        public string brtxt { get; set; }
        public string telf1 { get; set; }
        public string telf2 { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public string zterm { get; set; }
        public string regular { get; set; }
        //[{  "KUNNR":"0004000101","XCPDK":"X","CUSTOMER_ID":"0006321453","STCD1":"5896145","NAME":"WILLY MENDOZA",
        //    "BRSCH":"Z001","BRTXT":"Automotríz","TELF1":"60935605","TELF2":"3477163","EMAIL":"WMENDOZA@FINILAGER.BO",
        //    "DIRECCION":"BARRIO SORUCO BARBA CALLE C NO.12 CALLE C NO.12 BARDA MELON","ZTERM":"NT00","REGULAR":""}]
    }
}