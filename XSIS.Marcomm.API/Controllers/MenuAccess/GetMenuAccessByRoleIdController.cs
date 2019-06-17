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
    public class GetMenuAccessByRoleIdController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [Route("api/MenuAccess/GetMenuAccessByRoleId/{param1}")]
        [HttpGet]
        public MenuAccessViewModel Get(int param1)
        {
            return service.GetMenuAccessByRoleId(param1);
        }
    }
}
