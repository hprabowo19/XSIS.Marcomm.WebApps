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
    public class GetAllRolesDDLController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [HttpGet]
        public List<RoleViewModel> Get()
        {
            return service.GetAllRolesDDL();
        }
    }
}
