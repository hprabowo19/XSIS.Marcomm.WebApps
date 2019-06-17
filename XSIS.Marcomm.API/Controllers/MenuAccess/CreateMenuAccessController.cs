using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XSIS.Marcomm.Repositories;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.API.Controllers.MenuAccess
{
    public class CreateMenuAccessController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [HttpPost]
        [Route("api/MenuAccess/CreateMenuAccess/")]
        public bool Post(MenuAccessViewModel menuAccess)
        {
            try
            {
                service.CreateMenuAccess(menuAccess);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
