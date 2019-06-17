using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XSIS.Marcomm.Repositories;
using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.API.Controllers.Navigation
{
    public class GetMenuByRoleIdController : ApiController
    {
        private MenuNavigationRepository menuRepo = new MenuNavigationRepository();

        public List<MenuNavigationViewModel> Get(int param1)
        {
            var ListMenu = menuRepo.GetAllMenu();
            var ListMenuAccess = menuRepo.GetAllMenuAccess();

            var ListMenuByRoleID = (from menu in ListMenu
                                    join menuaccess in ListMenuAccess on menu.id equals menuaccess.m_menu_id
                                    where menuaccess.m_role_id == param1 && menuaccess.is_delete == false
                                    && menu.is_delete == false
                                    select new MenuNavigationViewModel
                                    {
                                        MenuID = menu.id,
                                        Code = menu.code,
                                        Name = menu.name,
                                        Controller = "/" + menu.controller + "/Index",
                                        ParentID = menu.parent_id,
                                        MenuAccessID = menuaccess.id,
                                        RoleID = menuaccess.m_role_id
                                    }).ToList();

            return ListMenuByRoleID;
        }
    }
}
