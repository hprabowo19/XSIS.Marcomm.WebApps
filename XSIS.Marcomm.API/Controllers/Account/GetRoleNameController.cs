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
    public class GetRoleNameController : ApiController
    {
        private AccountRepository accountRepo = new AccountRepository();

        public string Get(string param1)
        {
            return accountRepo.getRoleName(param1);
        }
    }
}
