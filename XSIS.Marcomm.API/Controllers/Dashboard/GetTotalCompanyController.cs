using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XSIS.Marcomm.Repositories;
using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.API.Controllers.Dashboard
{
    public class GetTotalCompanyController : ApiController
    {
        private DashboardRepository dashboardRepo = new DashboardRepository();

        public int Get()
        {
            return dashboardRepo.TotalCompany();
        }
    }
}
