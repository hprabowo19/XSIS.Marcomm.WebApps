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
    public class GetUserByUsernameController : ApiController
    {
        private AccountRepository accountRepo = new AccountRepository();

        public UserViewModel Get(string param1)
        {
            UserViewModel userVM = new UserViewModel();
            var model = accountRepo.getUserByUsername(param1);

            if (model != null)
            {
                userVM.ID = model.id;
                userVM.UserName = model.username;
                userVM.Password = model.password;
                userVM.RoleID = model.m_role_id;
                userVM.EmployeeID = model.m_employee_id;
                userVM.IsDelete = model.is_delete;
                userVM.CreatedBy = model.created_by;
                userVM.CreatedDate = model.created_date;
                userVM.UpdateBy = model.updated_by;
                userVM.UpdateDate = model.updated_date;
            }

            return userVM;
        }
    }
}
