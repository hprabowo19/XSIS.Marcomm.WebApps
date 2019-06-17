using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
using System.Net.Http;

using XSIS.Marcomm.ViewModels;
using Newtonsoft.Json;

namespace XSIS.Marcomm.WebApps.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Marcomm.API"];

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string cookiesName = FormsAuthentication.FormsCookieName;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                filterContext.HttpContext.Request.Cookies == null ||
                filterContext.HttpContext.Request.Cookies[cookiesName] == null)
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var uri = Url.Action("Login", "Account");
                filterContext.Result = new RedirectResult(uri);
                return;
            }

            var authCookie = filterContext.HttpContext.Request.Cookies[cookiesName];
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            string apiEndpoint = ApiUrl + "Account/GetUserByUsername/" + authTicket.Name;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndpoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var user =  JsonConvert.DeserializeObject<UserViewModel>(result);

            string apiEndpoint2 = ApiUrl + "Account/GetRoleByUserId/" + user.ID;
            HttpClient client2 = new HttpClient();
            HttpResponseMessage response2 = client2.GetAsync(apiEndpoint2).Result;

            string result2 = response2.Content.ReadAsStringAsync().Result.ToString();
            var roleList =  JsonConvert.DeserializeObject<List<RoleViewModel>>(result2);

            string[] roleArray = new string[roleList.Count()];
            for (int i = 0; i < roleList.Count(); i++)
            {
                roleArray[i] = roleList.ElementAt(i).Name;
            }

            var userIdentity = new GenericIdentity(authTicket.Name);
            var userPrincipal = new GenericPrincipal(userIdentity, roleArray);

            filterContext.HttpContext.User = userPrincipal;
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var uri = Url.Action("Unauthorized", "Account");
                filterContext.Result = new RedirectResult(uri);
                return;
            }
        }
    }
}