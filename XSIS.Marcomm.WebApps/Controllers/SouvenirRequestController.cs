using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using XSIS.Marcomm.ViewModels;
using XSIS.Marcomm.WebApps.Common;
using XSIS.Marcomm.WebApps.Models;
using XSIS.Marcomm.Repositories;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using XSIS.Marcomm.Models;
using System.Text;
using System.Net.Http.Headers;

namespace XSIS.Marcomm.WebApps.Controllers
{
    [CustomAuthorizeAttribute(Roles = "Administrator, Staff, Requester")]
    public class SouvenirRequestController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["XSIS.Marcomm.API"];

        [HttpGet]
        public ActionResult Index()
        {
            long UserId = 0;

            ViewBag.SessionLogin = UserId;
            return View();
        }

        [HttpGet]
        public ActionResult ListSouvenirStock(string code, string requestBy, string requestDate, string dueDate, string status, string createdDate, string createdBy)
        {
            string API_END_POINT = API_URL + $"SouvenirRequest/GetAllSouvenirStock" +
                $"?code={code}" +
                $"&requestBy={requestBy}" +
                $"&requestDate={requestDate}" +
                $"&dueDate={dueDate}" +
                $"&status={status}" +
                $"&createdDate={createdDate}" +
                $"&createdBy={createdBy}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var SouvenirStock = JsonConvert.DeserializeObject<List<SouvenirStockViewModel>>(result);
            
            return PartialView("_ListSouvenirRequest", SouvenirStock);
        }

        [HttpGet]
        public ActionResult SettlementSouvenirRequest(int id)
        {
            string API_END_POINT = API_URL + "SouvenirRequest/GetSouvenirStockById/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var SouvenirStock = JsonConvert.DeserializeObject<SouvenirStockViewModel>(result);
            if (Session["DetailUser"] != null)
            {
                var User = (UserViewModel)Session["DetailUser"];
                ViewBag.IdRole = User.RoleID;
            }
            return PartialView("_SettlementSouvenirRequest", SouvenirStock);
        }

        [HttpGet]
        public ActionResult ListSouvenirBySouvenirTransactionId(int id, int status)
        {
            string API_END_POINT = API_URL + "SouvenirRequest/GetListSouvenirBySouvenirTransactionId/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Souvenirs = JsonConvert.DeserializeObject<List<SouvenirRequestViewModel>>(result);
            if (Session["DetailUser"] != null)
            {
                var User = (UserViewModel)Session["DetailUser"];
                ViewBag.IdRole = User.RoleID;
            }
            ViewBag.status = status;
            return PartialView("_ListSouvenirItem", Souvenirs);
        }

        [HttpPost]
        public JsonResult UpdateSettlementSouvenirRequest(List<int> id, List<int> qtySettlement)
        {
            long UserId = 0;

            if (Session["DetailUser"] != null)
            {
                var User = Session["DetailUser"] as UserViewModel;
                UserId = User.ID;
            }

            SouvenirRequestViewModel SouvenirItem = new SouvenirRequestViewModel();
            SouvenirItem.listId = id;
            SouvenirItem.listQtySettlement = qtySettlement;
            SouvenirItem.updatedBy = UserId;

            string json = JsonConvert.SerializeObject(SouvenirItem);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "SouvenirRequest/SubmitSettlementSouvenirRequest/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(API_END_POINT, byteContent).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToString();

            return Json(bool.Parse(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApprovalSettlementSouvenirRequest(int id, string reason)
        {
            long UserId = 0;

            if (Session["DetailUser"] != null)
            {
                var User = Session["DetailUser"] as UserViewModel;
                UserId = User.ID;
            }

            SouvenirStockViewModel Souvenir = new SouvenirStockViewModel();
            Souvenir.id = id;
            if (reason == "")
            {
                Souvenir.status = 5;
                Souvenir.settlementApprovedBy = Convert.ToInt32(UserId);
                Souvenir.settlementApprovedDate = DateTime.Now;
            }
            else
            {
                Souvenir.status = 0;
                Souvenir.rejectReason = reason;
            }
            Souvenir.updateBy = UserId;

            string json = JsonConvert.SerializeObject(Souvenir);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "SouvenirRequest/ApprovalSettlementSouvenirRequest/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(API_END_POINT, byteContent).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToString();

            return Json(bool.Parse(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CloseSettlementSouvenirRequest(int id)
        {
            long UserId = 0;

            if (Session["DetailUser"] != null)
            {
                var User = Session["DetailUser"] as UserViewModel;
                UserId = User.ID;
            }

            SouvenirStockViewModel Souvenir = new SouvenirStockViewModel();
            Souvenir.id = id;
            Souvenir.updateBy = UserId;

            string json = JsonConvert.SerializeObject(Souvenir);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "SouvenirRequest/CloseSettlementSouvenirRequest/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(API_END_POINT, byteContent).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToString();

            return Json(bool.Parse(result), JsonRequestBehavior.AllowGet);
        }
    }
}
