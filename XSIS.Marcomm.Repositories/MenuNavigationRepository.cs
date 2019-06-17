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
    public class MenuNavigationRepository
    {
        public List<m_menu> GetAllMenu()
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var ListMenu = db.m_menu.Where(x => x.is_delete == false).ToList();
                return ListMenu;
            }
        }

        public List<m_menu_access> GetAllMenuAccess()
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var ListMenuAccess = db.m_menu_access.Where(x => x.is_delete == false).ToList();
                return ListMenuAccess;
            }
        }
    }
}
