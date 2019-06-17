using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.Mvc;

namespace XSIS.Marcomm.ViewModels
{
    public class MenuAccessViewModel
    {
        public int id { get; set; }
        public int mRoleId { get; set; }
        public int mMenuId { get; set; }
        public bool isDelete { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updateBy { get; set; }
        public DateTime? updatedDate { get; set; }

        public string mRoleCode { get; set; }
        public string mRoleName { get; set; }

        public List<SelectListItem> availableMenus { get; set; }
        public List<string> selectedMenus { get; set; }
    }
}
