using functions.App_Code;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ws_rest.Models;

namespace ws_rest.Controllers
{
    [RoutePrefix("fl/cliente")]
    public class ClienteController : ApiController
    {
        rfc RFCFunc = new rfc();

        [Route("fl/cliente/{gv_vkorg}/{gv_vtweg}/{gv_spart}/{gv_kunnr}/{gv_stcd1}/{gv_name1}")]
        public IEnumerable<Cliente> GetAllCliente(string gv_vkorg, string gv_vtweg, string gv_spart , string gv_kunnr = null, string gv_stcd1 = null, string gv_name1 = null ) {
            RfcDestination sap = RFCFunc.GetCnx();
            try
            {
                RfcRepository repo = sap.Repository;

                IRfcFunction sd = repo.CreateFunction("ZRFC_BUSCAR_CLIENTE");
                if ((gv_kunnr == "") || (gv_kunnr == null))
                { sd.SetValue("GV_KUNNR", "");  }
                else { sd.SetValue("GV_KUNNR", Utiles.LlenarCadena(gv_kunnr, 10, '0')); }
                sd.SetValue("GV_STCD1", gv_stcd1);
                if ((gv_name1 == null)) { sd.SetValue("GV_NAME1", ""); }
                else { sd.SetValue("GV_NAME1", gv_name1.ToUpper()); }
                sd.SetValue("GV_VKORG", gv_vkorg);
                sd.SetValue("GV_VTWEG", gv_vtweg);
                sd.SetValue("GV_SPART", gv_spart);

                RfcSessionManager.BeginContext(sap);
                sd.Invoke(sap);

                IRfcTable ret = sd.GetTable("RESULTADO");

                RfcSessionManager.EndContext(sap);

                //DataTable dt = new DataTable();
                //dt = Utiles.ConvertRFCTableToDataTable(ret);
                List<Cliente> cl = new List<Cliente>();
                cl = Utiles.TableMapParallel<Cliente>(ret);
                //cl = Utiles.ConvertDataTableToList<Cliente>(dt);
                
                return cl;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
    }
}
