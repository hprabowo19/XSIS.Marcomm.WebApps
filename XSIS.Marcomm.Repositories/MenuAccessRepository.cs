using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.Repositories
{
    public class MenuAccessRepository
    {
        public List<RoleViewModel> GetAllRolesDDL()
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var roles = (from p in db.m_role select p).ToList();
                List<RoleViewModel> Roles = new List<RoleViewModel>();

                foreach (var role in roles)
                {
                    RoleViewModel Role = new RoleViewModel();
                    Role.ID = role.id;
                    Role.Code = role.code;
                    Role.Name = role.name;
                    Role.Description = role.description;
                    Role.IsDelete = role.is_delete;
                    Role.CreatedBy = role.created_by;
                    Role.CreatedDate = role.created_date;
                    Role.UpdatedBy = role.created_by;
                    Role.UpdatedDate = role.created_date;

                    Roles.Add(Role);
                }

                return Roles;
            }
        }

        public List<RoleViewModel> GetAllRoles(string id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                string[] parameter = id.Split('|');
                string RoleCode = parameter[0];
                string RoleName = parameter[1];
                string tempCreatedDate = parameter[2];
                string CreatedBy = parameter[3];

                bool NullRoleCode = string.IsNullOrWhiteSpace(RoleCode);
                bool NullRoleName = string.IsNullOrWhiteSpace(RoleName);
                bool NullCreatedDate = string.IsNullOrWhiteSpace(tempCreatedDate);
                bool NullCreatedBy = string.IsNullOrWhiteSpace(CreatedBy);

                DateTime? CreatedDate = NullCreatedDate ? (DateTime?)null :
                    DateTime.Parse(
                        DateTime.ParseExact(tempCreatedDate, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("MM'/'dd'/'yyyy"));

                List<m_role> roles;
                {
                    if (NullRoleCode && NullRoleName && NullCreatedDate && NullCreatedBy)
                    {
                        roles = (from p in db.m_role select p).ToList();
                    }
                    else if (NullRoleName && NullCreatedDate && NullCreatedBy) // 1
                    {
                        roles = (from p in db.m_role where p.code.Equals(RoleCode) select p).ToList();
                    }
                    else if (NullRoleCode && NullCreatedDate && NullCreatedBy) // 2
                    {
                        roles = (from p in db.m_role where p.name.Equals(RoleName) select p).ToList();
                    }
                    else if (NullRoleCode && NullRoleName && NullCreatedBy) // 3
                    {
                        roles = (from p in db.m_role where (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0) select p).ToList();
                    }
                    else if (NullRoleCode && NullRoleName && NullCreatedDate) // 4
                    {
                        roles = (from p in db.m_role where p.created_by.Contains(CreatedBy) select p).ToList();
                    }
                    else if (NullCreatedDate && NullCreatedBy) // 1 2
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && p.name.Equals(RoleName)) select p).ToList();
                    }
                    else if (NullRoleName && NullCreatedBy) // 1 3
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0)) select p).ToList();
                    }
                    else if (NullRoleName && NullCreatedDate) // 1 4
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                    else if (NullRoleCode && NullCreatedBy) // 2 3
                    {
                        roles = (from p in db.m_role where (p.name.Equals(RoleName) && (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0)) select p).ToList();
                    }
                    else if (NullRoleCode && NullCreatedDate) // 2 4
                    {
                        roles = (from p in db.m_role where (p.name.Equals(RoleName) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                    else if (NullRoleCode && NullRoleName) // 3 4
                    {
                        roles = (from p in db.m_role where ((DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                    else if (NullCreatedBy) // 1 2 3
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && p.name.Equals(RoleName) && (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0)) select p).ToList();
                    }
                    else if (NullCreatedDate) // 1 2 4
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && p.name.Equals(RoleName) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                    else if (NullRoleName) // 1 3 4
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                    else if (NullRoleCode) // 2 3 4
                    {
                        roles = (from p in db.m_role where (p.name.Equals(RoleName) && (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                    else // 1 2 3 4
                    {
                        roles = (from p in db.m_role where (p.code.Equals(RoleCode) && p.name.Equals(RoleName) && (DateTime.Compare(p.created_date, (DateTime)CreatedDate) == 0) && p.created_by.Contains(CreatedBy)) select p).ToList();
                    }
                }

                roles = (from p in roles
                         where (p.m_menu_access.Any(x => x.m_role_id.Equals(p.id) && !x.is_delete) && !p.is_delete)
                         select p).ToList();

                List<RoleViewModel> Roles = new List<RoleViewModel>();

                foreach (var role in roles)
                {
                    RoleViewModel Role = new RoleViewModel();
                    Role.ID = role.id;
                    Role.Code = role.code;
                    Role.Name = role.name;
                    Role.Description = role.description;
                    Role.IsDelete = role.is_delete;
                    Role.CreatedBy = role.created_by;
                    Role.CreatedDate = role.created_date;
                    Role.UpdatedBy = role.created_by;
                    Role.UpdatedDate = role.created_date;

                    Roles.Add(Role);
                }

                return Roles;
            }
        }

        public List<RoleViewModel> GetAllRolesByMenuAccessNotExist()
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                List<m_role> roles = (from p in db.m_role
                                      where (!p.m_menu_access.Any(x => x.m_role_id.Equals(p.id) && !x.is_delete) && !p.is_delete)
                                      select p
                                      ).ToList();
                List<RoleViewModel> Roles = new List<RoleViewModel>();

                foreach (var role in roles)
                {
                    RoleViewModel Role = new RoleViewModel();
                    Role.ID = role.id;
                    Role.Code = role.code;
                    Role.Name = role.name;
                    Role.Description = role.description;
                    Role.IsDelete = role.is_delete;
                    Role.CreatedBy = role.created_by;
                    Role.CreatedDate = role.created_date;
                    Role.UpdatedBy = role.created_by;
                    Role.UpdatedDate = role.created_date;

                    Roles.Add(Role);
                }

                return Roles;
            }
        }

        public MenuAccessViewModel GetMenuAccessByRoleId(int id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var menu_accesses = (from p in db.m_menu_access
                                     where p.m_role_id.Equals(id) && !p.is_delete
                                     select new
                                     {
                                         role_id = p.m_role_id,
                                         role_code = p.m_role.code,
                                         role_name = p.m_role.name,
                                         menu_name = p.m_menu.name,
                                         menu_id = p.m_menu_id
                                     }).ToList();

                MenuAccessViewModel MenuAccess = new MenuAccessViewModel();

                var availableMenus = GetAllMenus();
                foreach (var menu_access in menu_accesses)
                {
                    availableMenus.FirstOrDefault(x => x.Value.Equals(menu_access.menu_id.ToString())).Selected = true;
                }

                MenuAccess.mRoleId = menu_accesses.FirstOrDefault().role_id;
                MenuAccess.mRoleCode = menu_accesses.FirstOrDefault().role_code;
                MenuAccess.mRoleName = menu_accesses.FirstOrDefault().role_name;
                MenuAccess.availableMenus = availableMenus;

                return MenuAccess;
            }
        }

        public List<SelectListItem> GetAllMenus()
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var menus = from p in db.m_menu where !p.is_delete orderby p.name select p;
                var availableMenus = new List<SelectListItem>();
                foreach (var menu in menus)
                {
                    availableMenus.Add(new SelectListItem
                    {
                        Text = menu.name,
                        Value = menu.id.ToString(),
                        Selected = false,
                    });
                }
                return availableMenus;
            }
        }

        public void CreateMenuAccess(MenuAccessViewModel menuAccess)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                m_menu_access menu_access = new m_menu_access();
                menu_access.m_role_id = menuAccess.mRoleId;
                menu_access.is_delete = false;
                menu_access.created_by = menuAccess.createdBy;
                menu_access.created_date = DateTime.Now;

                foreach (var menu_id in menuAccess.selectedMenus)
                {
                    menu_access.m_menu_id = int.Parse(menu_id);
                    db.m_menu_access.Add(menu_access);
                    db.SaveChanges();
                }
            }
        }

        public void EditMenuAccess(MenuAccessViewModel menuAccess)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                m_menu_access menu_access = new m_menu_access();
                menu_access.m_role_id = menuAccess.mRoleId;
                menu_access.is_delete = menuAccess.isDelete;

                var menu_is_already_exist = (from p in db.m_menu_access
                                             where p.m_role_id.Equals(menu_access.m_role_id) && !p.is_delete
                                             select p).ToList();

                foreach (var menu_id in menuAccess.selectedMenus)
                {
                    if (!menu_is_already_exist.Any(x => x.m_menu_id.Equals(int.Parse(menu_id))))
                    {
                        menu_access.m_menu_id = int.Parse(menu_id);
                        menu_access.is_delete = false;
                        menu_access.created_by = menuAccess.createdBy;
                        menu_access.created_date = menuAccess.createdDate;
                        db.m_menu_access.Add(menu_access);
                        db.SaveChanges();
                    }
                    else
                    {
                        menu_access = menu_is_already_exist.Where(x => x.m_menu_id.Equals(int.Parse(menu_id))).SingleOrDefault();
                        menu_access.is_delete = true;
                        menu_access.updated_by = menuAccess.updateBy;
                        menu_access.updated_date = menuAccess.updatedDate;
                        db.Entry(menu_access).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
        }

        public void DeleteMenuAccess(MenuAccessViewModel menuAccess)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var menu_is_already_exist = (from p in db.m_menu_access
                                             where p.m_role_id.Equals(menuAccess.mRoleId) && !p.is_delete
                                             select p).ToList();

                foreach (var menu in menu_is_already_exist)
                {
                    m_menu_access menu_access = menu;
                    menu_access.is_delete = true;
                    menu_access.updated_by = menuAccess.updateBy;
                    menu_access.updated_date = DateTime.Now;
                    db.Entry(menu_access).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
