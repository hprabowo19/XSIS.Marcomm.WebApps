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
    public class GetRoleByUserIdController : ApiController
    {
        private AccountRepository accountRepo = new AccountRepository();

        public List<RoleViewModel> Get(int param1)
        {
            List<RoleViewModel> ListRoleVM = new List<RoleViewModel>();

            var ListModel = accountRepo.getRoleByUserId(param1);

            if (ListModel != null || ListModel.Count > 0)
            {
                RoleViewModel roleVM = new RoleViewModel();

                foreach (var item in ListModel)
                {
                    roleVM.ID = item.id;
                    roleVM.Code = item.code;
                    roleVM.Name = item.name;
                    roleVM.Description = item.description;
                    roleVM.IsDelete = item.is_delete;
                    roleVM.CreatedBy = item.created_by;
                    roleVM.CreatedDate = item.created_date;
                    roleVM.UpdatedBy = item.updated_by;
                    roleVM.UpdatedDate = item.updated_date;
                }

                ListRoleVM.Add(roleVM);
            }

            return ListRoleVM;
        }
    }
}
