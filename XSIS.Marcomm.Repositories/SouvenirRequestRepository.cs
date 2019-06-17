using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.Repositories
{
    public class SouvenirRequestRepository
    {
        public List<SouvenirStockViewModel> GetAllSouvenirStock(SearchTransactionSouvenirViewModel parameters)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                bool NullCode = string.IsNullOrWhiteSpace(parameters.code);
                bool NullRequestBy = string.IsNullOrWhiteSpace(parameters.requestBy);
                bool NullRequestDate = string.IsNullOrWhiteSpace(parameters.requestDate);
                bool NullDueDate = string.IsNullOrWhiteSpace(parameters.dueDate);
                bool NullStatus = string.IsNullOrWhiteSpace(parameters.status);
                bool NullCreatedDate = string.IsNullOrWhiteSpace(parameters.createdDate);
                bool NullCreatedBy = string.IsNullOrWhiteSpace(parameters.createdBy);

                DateTime? requestDate = NullRequestDate ? (DateTime?)null :
                    DateTime.Parse(
                        DateTime.ParseExact(parameters.requestDate, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("MM'/'dd'/'yyyy"));
                DateTime? dueDate = NullDueDate ? (DateTime?)null :
                    DateTime.Parse(
                        DateTime.ParseExact(parameters.dueDate, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("MM'/'dd'/'yyyy"));
                DateTime? createdDate = NullCreatedDate ? (DateTime?)null :
                    DateTime.Parse(
                        DateTime.ParseExact(parameters.createdDate, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("MM'/'dd'/'yyyy"));

                var souvenir_stock = (from p in db.t_souvenir where p.is_delete == false select p).ToList();

                souvenir_stock = !NullCode ? (from p in souvenir_stock where p.code.Contains(parameters.code) select p).ToList() : souvenir_stock;
                souvenir_stock = !NullRequestDate ? (from p in souvenir_stock where DateTime.Compare((DateTime)p.request_date, (DateTime)requestDate) == 0 select p).ToList() : souvenir_stock;
                souvenir_stock = !NullDueDate ? (from p in souvenir_stock where DateTime.Compare((DateTime)p.request_due_date, (DateTime)dueDate) == 0 select p).ToList() : souvenir_stock;
                souvenir_stock = !NullCreatedDate ? (from p in souvenir_stock where DateTime.Compare((DateTime)p.created_date, (DateTime)createdDate) == 0 select p).ToList() : souvenir_stock;
                souvenir_stock = !NullRequestBy ? (from p in souvenir_stock where p.request_by.Equals(
                    (from q in db.m_employee where !q.id_delete && (q.first_name + " " + q.last_name).Contains(parameters.requestBy) select q.id).SingleOrDefault()) select p).ToList() : souvenir_stock;
                souvenir_stock = !NullCreatedBy ? (from p in souvenir_stock where p.created_by.Equals(Convert.ToInt64(
                    (from q in db.m_employee where !q.id_delete && (q.first_name + " " + q.last_name).Contains(parameters.createdBy) select q.id).SingleOrDefault())) select p).ToList() : souvenir_stock;
                souvenir_stock = !NullStatus ? (from p in souvenir_stock where GetStatusId(parameters.status.ToLower()).Contains((int)p.status) select p).ToList() : souvenir_stock;

                List<SouvenirStockViewModel> SouvenirStock = new List<SouvenirStockViewModel>();
                foreach (var souvenir in souvenir_stock)
                {
                    SouvenirStockViewModel Souvenir = new SouvenirStockViewModel();
                    Souvenir.id = souvenir.id;
                    Souvenir.code = souvenir.code;
                    Souvenir.requestBy = souvenir.request_by;
                    Souvenir.requestDate = souvenir.request_date;
                    Souvenir.requestDueDate = souvenir.request_due_date;
                    Souvenir.status = souvenir.status;
                    Souvenir.createdDate = souvenir.created_date;
                    Souvenir.createdBy = souvenir.created_by;

                    Souvenir.requestByToFullName = GetFullNameFromIdEmployee(souvenir.request_by);
                    Souvenir.createdByToFullName = GetFullNameFromIdEmployee(Convert.ToInt32(souvenir.created_by));

                    SouvenirStock.Add(Souvenir);
                }
                return SouvenirStock;
            }
        }

        public SouvenirStockViewModel GetSouvenirStockById(int id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var souvenir = (from p in db.t_souvenir
                                where p.id.Equals(id) && p.is_delete == false
                                select p).SingleOrDefault();

                SouvenirStockViewModel Souvenir = new SouvenirStockViewModel();
                Souvenir.id = souvenir.id;
                Souvenir.code = souvenir.code;
                Souvenir.tEventId = souvenir.t_event_id;
                Souvenir.requestBy = souvenir.request_by;
                Souvenir.requestDate = souvenir.request_date;
                Souvenir.requestDueDate = souvenir.request_due_date;
                Souvenir.note = souvenir.note;
                Souvenir.status = souvenir.status;

                Souvenir.requestByToFullName = GetFullNameFromIdEmployee(souvenir.request_by);
                Souvenir.createdByToFullName = GetFullNameFromIdEmployee(Convert.ToInt32(souvenir.created_by));
                Souvenir.eventIdToEventCode = GetEventCodeFromEventId(souvenir.t_event_id);

                return Souvenir;
            }
        }

        public List<SouvenirRequestViewModel> GetListSouvenirBySouvenirTransactionId(int id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var souvenir_items = (from p in db.t_souvenir
                                      where p.id.Equals(id) && p.is_delete == false
                                      select p.t_souvenir_item.ToList()
                                      ).SingleOrDefault();
                List<SouvenirRequestViewModel> souvenirItems = new List<SouvenirRequestViewModel>();
                foreach (var souvenir_item in souvenir_items)
                {
                    SouvenirRequestViewModel souvenirItem = new SouvenirRequestViewModel();
                    souvenirItem.id = souvenir_item.id;
                    souvenirItem.mSouvenirId = souvenir_item.m_souvenir_id;
                    souvenirItem.qty = souvenir_item.qty;
                    souvenirItem.qtySettlement = souvenir_item.qty_settlement;
                    souvenirItem.note = souvenir_item.note;

                    souvenirItem.souvenirIdToSouvenirName = GetSouvenirNameFromSouvenirId(souvenir_item.m_souvenir_id);

                    souvenirItems.Add(souvenirItem);
                }
                return souvenirItems;
            }
        }

        public void UpdateSettlementSouvenirRequest(SouvenirRequestViewModel souvenirItem)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var dictSouvenirItem = souvenirItem.listId.Zip(souvenirItem.listQtySettlement, (id, qty) => new { id, qty })
                    .ToDictionary(x => x.id, x => x.qty);

                var souvenir_items = (from p in db.t_souvenir_item
                                      where souvenirItem.listId.ToList().Contains(p.id) && !p.is_delete
                                      select p)
                                      .ToList();
                
                foreach (var souvenir_item in souvenir_items)
                {
                    souvenir_item.qty_settlement = dictSouvenirItem[souvenir_item.id];
                    souvenir_item.updated_by = souvenirItem.updatedBy;
                    souvenir_item.updated_date = DateTime.Now;

                    souvenir_item.t_souvenir.status = 4;
                    souvenir_item.t_souvenir.settlement_by = Convert.ToInt32(souvenirItem.updatedBy);
                    souvenir_item.t_souvenir.settlement_date = DateTime.Now;
                    souvenir_item.t_souvenir.update_by = souvenirItem.updatedBy;
                    souvenir_item.t_souvenir.update_date = DateTime.Now;

                    db.Entry(souvenir_item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void ApprovalSettlementSouvenirRequest(SouvenirStockViewModel Souvenir)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var souvenir = (from p in db.t_souvenir where p.id.Equals(Souvenir.id) select p).SingleOrDefault();

                souvenir.status = Souvenir.status;
                souvenir.settlement_approved_by = Souvenir.settlementApprovedBy;
                souvenir.settlement_approved_date = Souvenir.settlementApprovedDate;
                souvenir.reject_reason = Souvenir.rejectReason;
                souvenir.update_by = Souvenir.updateBy;
                souvenir.update_date = DateTime.Now;
                db.Entry(souvenir).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void CloseSettlementSouvenirRequest(SouvenirStockViewModel Souvenir)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                var souvenir = (from p in db.t_souvenir where p.id.Equals(Souvenir.id) select p).SingleOrDefault();

                souvenir.status = 6;
                souvenir.update_by = Souvenir.updateBy;
                souvenir.update_date = DateTime.Now;
                db.Entry(souvenir).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private string GetFullNameFromIdEmployee(int id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                return (from p in db.m_employee where p.id.Equals(id) && !p.id_delete select p.first_name + " " + p.last_name).SingleOrDefault();
            }
        }

        private string GetEventCodeFromEventId(int id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                return (from p in db.t_event where p.id.Equals(id) && p.is_delete == false select p.code).SingleOrDefault();
            }
        }

        private string GetSouvenirNameFromSouvenirId(int id)
        {
            using (MarcommEntities db = new MarcommEntities())
            {
                return (from p in db.m_souvenir where p.id.Equals(id) && !p.is_delete select p.name).SingleOrDefault();
            }
        }

        private List<int> GetStatusId(string value)
        {
            Dictionary<int, string> Status = new Dictionary<int, string>()
            {
                { 0, "rejected" },
                { 1, "submitted" },
                { 2, "in progress" },
                { 3, "received by requester" },
                { 4, "settlement" },
                { 5, "approved settlement" },
                { 6, "done request" }
            };
            return Status.Where(p => p.Value.Contains(value)).Select(p => p.Key).ToList();
        }
    }
}
