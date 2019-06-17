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
    public class GetAllSouvenirStockController : ApiController
    {
        private SouvenirRequestRepository service = new SouvenirRequestRepository();

        [Route("api/SouvenirRequest/GetAllSouvenirStock/")]
        [HttpGet]
        public List<SouvenirStockViewModel> Get([FromUri] SearchTransactionSouvenirViewModel parameters)
        {
            return service.GetAllSouvenirStock(parameters);
        }
    }
}
