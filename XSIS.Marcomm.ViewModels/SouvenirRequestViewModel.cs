using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Marcomm.ViewModels
{
    public class SouvenirRequestViewModel
    {
        public int id { get; set; }
        public int tSouvenirId { get; set; }
        public int mSouvenirId { get; set; }
        public long? qty { get; set; }
        public long? qtySettlement { get; set; }
        public string note { get; set; }
        public bool isDelete { get; set; }
        public long? createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public long? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }

        public string souvenirIdToSouvenirName { get; set; }
        public List<int> listId { get; set; }
        public List<int> listQtySettlement { get; set; }
    }
}
