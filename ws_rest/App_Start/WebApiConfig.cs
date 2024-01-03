using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ws_rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "fl/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
