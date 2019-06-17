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
    public class IsValidController : ApiController
    {
        private AccountRepository accountRepo = new AccountRepository();

        public bool Get(string param1, string param2)
        {
            return accountRepo.IsValid(param1, param2);
        }
    }
}
