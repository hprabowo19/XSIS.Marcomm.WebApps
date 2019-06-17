using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace XSIS.Marcomm.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.MapHttpAttributeRoutes();

            // Web API routes Account
            config.Routes.MapHttpRoute(
                name: "Account1ParamApi",
                routeTemplate: "api/Account/{controller}/{param1}",
                defaults: new { param1 = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Account2ParamApi",
                routeTemplate: "api/Account/{controller}/{param1}/{param2}",
                defaults: new { param1 = RouteParameter.Optional, param2 = RouteParameter.Optional }
            );

            // Web API routes Dashboard
            config.Routes.MapHttpRoute(
                name: "DashboardApi",
                routeTemplate: "api/Dashboard/{controller}/{param1}",
                defaults: new { param1 = RouteParameter.Optional }
            );

            // Web API routes Navigation
            config.Routes.MapHttpRoute(
                name: "NavigationApi",
                routeTemplate: "api/Navigation/{controller}/{param1}",
                defaults: new { param1 = RouteParameter.Optional }
            );

            // Web API routes Menu Access
            config.Routes.MapHttpRoute(
                name: "MenuAccessApi",
                routeTemplate: "api/MenuAccess/{controller}/{param1}",
                defaults: new { param1 = RouteParameter.Optional }
            );

            // Web API routes Default
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
