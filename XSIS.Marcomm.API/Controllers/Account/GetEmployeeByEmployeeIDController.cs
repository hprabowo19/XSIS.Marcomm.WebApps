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
    public class GetEmployeeByEmployeeIDController : ApiController
    {
        private AccountRepository accountRepo = new AccountRepository();

        public EmployeeViewModel Get(int param1)
        {
            EmployeeViewModel empVM = new EmployeeViewModel();
            var model = accountRepo.getEmployeeByUserId(param1);

            if (model != null)
            {
                empVM.ID = model.id;
                empVM.EmployeeNumber = model.employee_number;
                empVM.FirstName = model.first_name;
                empVM.LastName = model.last_name;
                empVM.CompanyID = model.m_company_id;
                empVM.Email = model.email;
                empVM.IdDelete = model.id_delete;
                empVM.CreatedBy = model.created_by;
                empVM.CreatedDate = model.created_date;
                empVM.UpdateBy = model.updated_by;
                empVM.UpdateDate = model.updated_date;
            }

            return empVM;
        }
    }
}
