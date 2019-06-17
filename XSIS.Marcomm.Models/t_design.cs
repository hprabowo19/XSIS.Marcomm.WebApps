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
    
    public partial class t_design
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_design()
        {
            this.t_design_item = new HashSet<t_design_item>();
            this.t_promotion = new HashSet<t_promotion>();
        }
    
        public int id { get; set; }
        public string code { get; set; }
        public int t_event_id { get; set; }
        public string title_header { get; set; }
        public int request_by { get; set; }
        public System.DateTime request_date { get; set; }
        public Nullable<int> approved_by { get; set; }
        public Nullable<System.DateTime> approved_date { get; set; }
        public Nullable<int> assign_to { get; set; }
        public Nullable<System.DateTime> closed_date { get; set; }
        public string note { get; set; }
        public Nullable<int> status { get; set; }
        public string reject_reason { get; set; }
        public Nullable<bool> is_delete { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
    
        public virtual m_employee m_employee { get; set; }
        public virtual m_employee m_employee1 { get; set; }
        public virtual m_employee m_employee2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_design_item> t_design_item { get; set; }
        public virtual t_event t_event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_promotion> t_promotion { get; set; }
    }
}
