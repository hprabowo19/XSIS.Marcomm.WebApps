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
    public class ApprovalSettlementSouvenirRequestController : ApiController
    {
        private SouvenirRequestRepository service = new SouvenirRequestRepository();

        [HttpPut]
        [Route("api/SouvenirRequest/ApprovalSettlementSouvenirRequest/")]
        public bool Put(SouvenirStockViewModel Souvenir)
        {
            try
            {
                service.ApprovalSettlementSouvenirRequest(Souvenir);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
