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
    public class GetAllRolesByMenuAccessNotExistController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [Route("api/MenuAccess/GetAllRolesByMenuAccessNotExist/")]
        [HttpGet]
        public List<RoleViewModel> Get()
        {
            return service.GetAllRolesByMenuAccessNotExist();
        }
    }
}
