using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Marcomm.ViewModels
{
    public class MenuNavigationViewModel
    {
        public int MenuID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public Nullable<int> ParentID { get; set; }
        public int MenuAccessID { get; set; }
        public int RoleID { get; set; }
    }
}
