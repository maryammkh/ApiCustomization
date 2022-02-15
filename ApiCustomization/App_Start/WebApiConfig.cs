using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiCustomization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services

            //// Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            GlobalConfiguration.Configuration.Routes.MapHttpRoute("API_Default", "ARC_API/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}
