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
    
    public partial class m_menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public m_menu()
        {
            this.m_menu_access = new HashSet<m_menu_access>();
        }
    
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string controller { get; set; }
        public Nullable<int> parent_id { get; set; }
        public bool is_delete { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<m_menu_access> m_menu_access { get; set; }
    }
}
