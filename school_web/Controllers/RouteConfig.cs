using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace school_web.Controllers
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteCollectionExtensions.IgnoreRoute(routes, "{resource}.axd/{*pathInfo}");
            RouteCollectionAttributeRoutingExtensions.MapMvcAttributeRoutes(routes);
            RouteCollectionExtensions.MapRoute(routes, "Default", "{controller}/{action}/{id}", (object)new
            {
                controller = "Auth",
                action = "Index",
                id = UrlParameter.Optional
            });
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Auth", action = "Index", id = UrlParameter.Optional }
            //);

        }
    }
}