using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using XSIS.Marcomm.Models;

namespace XSIS.Marcomm.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "*Username is mandatory.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*Password is mandatory.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public m_user m_user { get; set; }
    }
}
