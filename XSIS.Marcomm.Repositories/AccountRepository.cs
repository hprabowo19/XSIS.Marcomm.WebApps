using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.Repositories
{
    public class AccountRepository
    {
        public bool IsValid(string username, string password)
        {
            bool isValid = false;

            using (MarcommEntities db = new MarcommEntities())
            {
                var user = db.m_user.Where(x => x.username == username && x.is_delete == false).SingleOrDefault();

                if (user != null)
                {
                    if (user.password == password)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        public int getRoleId(string username)
        {
            int roleId = 0;

            using (MarcommEntities db = new MarcommEntities())
            {
                var getRoleid = (from role in db.m_role
                                 join user in db.m_user on role.id equals user.m_role_id
                                 join employ in db.m_employee on user.m_employee_id equals employ.id
                                 where user.username == username && user.is_delete == false && 
                                 employ.id_delete == false && role.is_delete == false
                                 select role.id).SingleOrDefault();

                if (getRoleid == 0)
                {
                    roleId = 0;
                }
                else
                {
                    roleId = getRoleid;
                }
            }

            return roleId;
        }

        public string getRoleName(string username)
        {
            string RoleName = string.Empty;

            using (MarcommEntities db = new MarcommEntities())
            {
                var getRoleNm = (from role in db.m_role
                                 join user in db.m_user on role.id equals user.m_role_id
                                 join employ in db.m_employee on user.m_employee_id equals employ.id
                                 where user.username == username && user.is_delete == false &&
                                 employ.id_delete == false && role.is_delete == false
                                 select role.name).SingleOrDefault();

                if (string.IsNullOrEmpty(getRoleNm))
                {
                    RoleName = "Not Assign Role";
                }
                else
                {
                    RoleName = getRoleNm;
                }
            }

            return RoleName;
        }

        public m_user getUserByUsername(string username)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                m_user user = db.m_user.Where(y => y.username == username && y.is_delete == false).SingleOrDefault();
                return user;
            } 
        }

        public m_employee getEmployeeByUserId(int employeeId)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var getEmployee = db.m_employee.Where(x => x.id == employeeId).SingleOrDefault();
                return getEmployee;
            }
        }

        public List<m_role> getRoleByUserId(int userid)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var RoleList = (from role in db.m_role
                                join user in db.m_user on role.id equals user.m_role_id
                                join employ in db.m_employee on user.m_employee_id equals employ.id
                                where user.id == userid && user.is_delete == false &&
                                employ.id_delete == false && role.is_delete == false
                                select role).ToList();
                return RoleList;
            }
        }
    }
}
