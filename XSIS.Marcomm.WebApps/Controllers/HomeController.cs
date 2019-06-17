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
using XSIS.Marcomm.WebApps.Common;

namespace XSIS.Marcomm.WebApps.Controllers
{
    [CustomAuthorizeAttribute(Roles = "Administrator, Staff, Requester")]
    public class HomeController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Marcomm.API"];

        public ActionResult Index()
        {
            string apiEndpoint = ApiUrl + "Dashboard/GetTotalCompany/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndpoint).Result;

            int TotalCompany = int.Parse(response.Content.ReadAsStringAsync().Result);

            string apiEndpoint1 = ApiUrl + "Dashboard/GetTotalEmployee/";
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = client1.GetAsync(apiEndpoint1).Result;

            int TotalEmployee = int.Parse(response1.Content.ReadAsStringAsync().Result);

            string apiEndpoint2 = ApiUrl + "Dashboard/GetTotalUser/";
            HttpClient client2 = new HttpClient();
            HttpResponseMessage response2 = client2.GetAsync(apiEndpoint2).Result;

            int TotalUser = int.Parse(response2.Content.ReadAsStringAsync().Result);

            string apiEndpoint3 = ApiUrl + "Dashboard/GetTotalProduct/";
            HttpClient client3 = new HttpClient();
            HttpResponseMessage response3 = client3.GetAsync(apiEndpoint3).Result;

            int TotalProduct = int.Parse(response3.Content.ReadAsStringAsync().Result);

            ViewBag.TotalCompany = TotalCompany;
            ViewBag.TotalEmployee = TotalEmployee;
            ViewBag.TotalUser = TotalUser;
            ViewBag.TotalProduct = TotalProduct;

            return View();
        }
    }
}