using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // router -? MvcRequestHandler -> new {ConcreteControllerClass} -> wykona metodę {action}, 
            // która zwróci ActionResult, ktorego metoda ExecuteResult beędzie wykonana
            // a na końcu handler z kontrolera wywoła metodę Dispose() jeśli ona tam jest
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", // dowolnie dużo segmentów (tenant/site/subsite/subsubsite/page)
                defaults: new { controller = "Items", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
