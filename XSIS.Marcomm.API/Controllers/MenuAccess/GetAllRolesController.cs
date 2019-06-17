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
    public class GetAllRolesController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [Route("api/MenuAccess/GetAllRoles/{param1}")]
        [HttpGet]
        public List<RoleViewModel> Get(string param1)
        {
            return service.GetAllRoles(param1);
        }
    }
}
