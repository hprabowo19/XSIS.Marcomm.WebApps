//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XSIS.Marcomm.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class m_menu_access
    {
        public int id { get; set; }
        public int m_role_id { get; set; }
        public int m_menu_id { get; set; }
        public bool is_delete { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
    
        public virtual m_menu m_menu { get; set; }
        public virtual m_role m_role { get; set; }
    }
}
