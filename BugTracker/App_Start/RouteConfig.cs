using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BugTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            //can I add if user signed in as not demo then redirect to normal index page????


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "/*Index*/LP", id = UrlParameter.Optional }
            );
        }
    }
}
