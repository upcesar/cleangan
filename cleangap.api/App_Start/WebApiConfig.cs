using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace cleangap.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Enable CORS security
            EnableCors(config);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void EnableCors(HttpConfiguration config)
        {
            var UrlOrigin = ConfigurationManager.AppSettings["WebUrl"].ToString();
            var cors = new EnableCorsAttribute(UrlOrigin, "*", "*");
            
            config.EnableCors(cors);
        }
    }
}
