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
    public class GetSouvenirStockByIdController : ApiController
    {
        private SouvenirRequestRepository service = new SouvenirRequestRepository();

        [Route("api/SouvenirRequest/GetSouvenirStockById/{param1}")]
        [HttpGet]
        public SouvenirStockViewModel Get(int param1)
        {
            return service.GetSouvenirStockById(param1);
        }
    }
}
