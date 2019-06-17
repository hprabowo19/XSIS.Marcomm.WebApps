using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Marcomm.ViewModels
{
    public class SearchTransactionSouvenirViewModel
    {
        public string code { get; set; }
        public string requestBy { get; set; }
        public string requestDate { get; set; }
        public string dueDate { get; set; }
        public string status { get; set; }
        public string createdDate { get; set; }
        public string createdBy { get; set; }
    }
}
