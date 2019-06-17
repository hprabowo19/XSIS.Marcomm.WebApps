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
    public class MenuAccessController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["XSIS.Marcomm.API"];
        
        [HttpGet]
        public ActionResult Index()
        {
            string API_END_POINT = API_URL + "MenuAccess/GetAllRolesDDL/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(result);
            
            ViewBag.RoleCode = new SelectList(Roles, "Code", "Code");
            ViewBag.RoleName = new SelectList(Roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string RoleCode, string RoleName, string CreatedDate, string CreatedBy)
        {
            string parameter = RoleCode + '|' + RoleName + '|' + CreatedDate + '|' + CreatedBy;
            string API_END_POINT = API_URL + "MenuAccess/GetAllRoles/" + parameter;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(result);
            return PartialView("_ListMenuAccess", Roles);
        }
        
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "MenuAccess/GetMenuAccessByRoleId/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var MenuAccess = JsonConvert.DeserializeObject<MenuAccessViewModel>(result);
                        
            if (MenuAccess == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", MenuAccess);
        }
        
        [HttpGet]
        public ActionResult Create()
        {

            string API_END_POINT = API_URL + "MenuAccess/GetAllRolesByMenuAccessNotExist/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(result);

            API_END_POINT = API_URL + "MenuAccess/GetAllMenus/";
            client = new HttpClient();
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var Menus = JsonConvert.DeserializeObject<List<SelectListItem>>(result);

            if (Menus == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleName = new SelectList(Roles, "ID", "Name");
            ViewBag.Menus = Menus;
            return PartialView("_Create");
        }

        [HttpPost]
        public JsonResult Create(int RoleId, List<string> MenusId)
        {
            string UserName = "Anonymous";

            if (Session["DetailUser"] != null)
            {
                var User = Session["DetailUser"] as UserViewModel;
                UserName = User.UserName;
            }

            MenuAccessViewModel menuAccess = new MenuAccessViewModel();
            menuAccess.mRoleId = RoleId;
            menuAccess.selectedMenus = MenusId;
            menuAccess.createdBy = UserName;

            string json = JsonConvert.SerializeObject(menuAccess);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "MenuAccess/CreateMenuAccess/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsync(API_END_POINT, byteContent).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToString();

            return Json(bool.Parse(result), JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "MenuAccess/GetMenuAccessByRoleId/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var MenuAccess = JsonConvert.DeserializeObject<MenuAccessViewModel>(result);

            if (MenuAccess == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit", MenuAccess);
        }

        [HttpPost]
        public JsonResult Edit(int RoleId, List<string> MenusId)
        {
            string UserName = "Anonymous";

            if (Session["DetailUser"] != null)
            {
                var User = Session["DetailUser"] as UserViewModel;
                UserName = User.UserName;
            }

            MenuAccessViewModel menuAccess = new MenuAccessViewModel();
            menuAccess.mRoleId = RoleId;
            menuAccess.selectedMenus = MenusId;
            menuAccess.updateBy = menuAccess.createdBy = UserName;
            menuAccess.updatedDate = menuAccess.createdDate = DateTime.Now;

            string json = JsonConvert.SerializeObject(menuAccess);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "MenuAccess/EditMenuAccess/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(API_END_POINT, byteContent).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToString();

            return Json(bool.Parse(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int RoleId)
        {
            string UserName = "Anonymous";

            if (Session["DetailUser"] != null)
            {
                var User = Session["DetailUser"] as UserViewModel;
                UserName = User.UserName;
            }

            MenuAccessViewModel menuAccess = new MenuAccessViewModel();
            menuAccess.mRoleId = RoleId;
            menuAccess.updateBy = UserName;

            string json = JsonConvert.SerializeObject(menuAccess);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "MenuAccess/DeleteMenuAccess/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(API_END_POINT, byteContent).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToString();

            return Json(bool.Parse(result), JsonRequestBehavior.AllowGet);
        }
    }
}
