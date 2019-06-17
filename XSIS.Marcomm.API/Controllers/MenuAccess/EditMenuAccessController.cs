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
    public class EditMenuAccessController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [HttpPut]
        [Route("api/MenuAccess/EditMenuAccess/")]
        public bool Put(MenuAccessViewModel menuAccess)
        {
            try
            {
                service.EditMenuAccess(menuAccess);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
