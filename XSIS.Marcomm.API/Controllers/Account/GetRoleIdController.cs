using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XSIS.Marcomm.Repositories;
using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.API.Controllers.Account
{
    public class GetRoleIdController : ApiController
    {
        private AccountRepository accountRepo = new AccountRepository();

        public int Get(string param1)
        {
            return accountRepo.getRoleId(param1);
        }
    }
}
