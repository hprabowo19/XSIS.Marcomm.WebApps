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
    public class GetListSouvenirBySouvenirTransactionIdController : ApiController
    {
        private SouvenirRequestRepository service = new SouvenirRequestRepository();

        [Route("api/SouvenirRequest/GetListSouvenirBySouvenirTransactionId/{param1}")]
        [HttpGet]
        public List<SouvenirRequestViewModel> Get(int param1)
        {
            return service.GetListSouvenirBySouvenirTransactionId(param1);
        }
    }
}
