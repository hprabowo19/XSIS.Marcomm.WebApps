using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Marcomm.ViewModels
{
    public class SouvenirStockViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public int tEventId { get; set; }
        public int requestBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? requestDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? requestDueDate { get; set; }
        public int? approvedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? approvedDate { get; set; }
        public int? receivedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? receivedDate { get; set; }
        public int? settlementBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? settlementDate { get; set; }
        public int? settlementApprovedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? settlementApprovedDate { get; set; }
        public int? status { get; set; }
        public string note { get; set; }
        public string rejectReason { get; set; }
        public bool? isDelete { get; set; }
        public long? createdBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? createdDate { get; set; }
        public long? updateBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? updateDate { get; set; }

        public string requestByToFullName { get; set; }
        public string createdByToFullName { get; set; }
        public string eventIdToEventCode { get; set; }
        public string statusText { get {
                return
                    status == 0 ? "Rejected" :
                    status == 1 ? "Submitted" :
                    status == 2 ? "In Progress" :
                    status == 3 ? "Received by Requester" :
                    status == 4 ? "Settlement" :
                    status == 5 ? "Approved Settlement" :
                    status == 6 ? "Done Request" : ""; 
            }
        }

    }
}
