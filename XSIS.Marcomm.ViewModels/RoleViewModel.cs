using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Marcomm.ViewModels
{
    public class RoleViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Role Code")]
        public string Code { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
