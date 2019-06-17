using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

using XSIS.Marcomm.Repositories;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.API.Controllers.MenuAccess
{
    public class GetAllMenusController : ApiController
    {
        private MenuAccessRepository service = new MenuAccessRepository();

        [System.Web.Http.Route("api/MenuAccess/GetAllMenus/")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> Get()
        {
            return service.GetAllMenus();
        }
    }
}
