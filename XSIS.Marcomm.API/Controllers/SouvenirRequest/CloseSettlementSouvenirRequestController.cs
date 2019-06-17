using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XSIS.Marcomm.Repositories;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.API.Controllers.SouvenirRequest
{
    public class CloseSettlementSouvenirRequestController : ApiController
    {
        private SouvenirRequestRepository service = new SouvenirRequestRepository();

        [HttpPut]
        [Route("api/SouvenirRequest/CloseSettlementSouvenirRequest/")]
        public bool Put(SouvenirStockViewModel Souvenir)
        {
            try
            {
                service.CloseSettlementSouvenirRequest(Souvenir);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
