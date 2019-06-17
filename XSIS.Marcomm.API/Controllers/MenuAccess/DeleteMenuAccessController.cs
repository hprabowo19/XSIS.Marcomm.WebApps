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
    public class DeleteMenuAccessController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [HttpPut]
        [Route("api/MenuAccess/DeleteMenuAccess/")]
        public bool Put(MenuAccessViewModel menuAccess)
        {
            try
            {
                service.DeleteMenuAccess(menuAccess);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
