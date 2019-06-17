using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.WebApps.Controllers
{
    public class AccountController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Marcomm.API"];

        [AllowAnonymous]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountViewModel loginViewModel)
        {
            string apiEndpoint = ApiUrl + "Account/IsValid/" + loginViewModel.UserName + "/" + loginViewModel.Password;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndpoint).Result;

            var result = bool.Parse(response.Content.ReadAsStringAsync().Result);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(loginViewModel.UserName, false);

                string apiEndpoint3 = ApiUrl + "Account/GetUserByUsername/" + loginViewModel.UserName;
                HttpClient client3 = new HttpClient();
                HttpResponseMessage response3 = client3.GetAsync(apiEndpoint3).Result;

                string result2 = response3.Content.ReadAsStringAsync().Result.ToString();
                var detailUser =  JsonConvert.DeserializeObject<UserViewModel>(result2);

                string apiEndpoint4 = ApiUrl + "Account/GetRoleName/" + loginViewModel.UserName;
                HttpClient client4 = new HttpClient();
                HttpResponseMessage response4 = client4.GetAsync(apiEndpoint4).Result;

                string RoleName = response4.Content.ReadAsStringAsync().Result.ToString();

                string apiEndpoint2 = ApiUrl + "Account/GetEmployeeByEmployeeID/" + detailUser.EmployeeID;
                HttpClient client2 = new HttpClient();
                HttpResponseMessage response2 = client2.GetAsync(apiEndpoint2).Result;

                string result1 = response2.Content.ReadAsStringAsync().Result.ToString();
                var detailEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(result1);

                string apiEndpoint5 = ApiUrl + "Account/GetRoleId/" + loginViewModel.UserName;
                HttpClient client5 = new HttpClient();
                HttpResponseMessage response5 = client5.GetAsync(apiEndpoint5).Result;

                int RoleId = int.Parse(response5.Content.ReadAsStringAsync().Result.ToString());

                string apiEndpoint6 = ApiUrl + "Navigation/GetMenuByRoleId/" + RoleId;
                HttpClient client6 = new HttpClient();
                HttpResponseMessage response6 = client6.GetAsync(apiEndpoint6).Result;

                string result3 = response6.Content.ReadAsStringAsync().Result.ToString();
                var detailMenu = JsonConvert.DeserializeObject<List<MenuNavigationViewModel>>(result3);

                Session["DetailUser"] = detailUser;
                Session["RoleName"] = RoleName;
                Session["DetailEmployee"] = detailEmployee;
                Session["DetailMenu"] = detailMenu;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login failed.");
                return View(loginViewModel);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Json(new { result = true, message = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}