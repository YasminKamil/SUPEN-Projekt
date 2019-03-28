using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SUPEN_Projekt {

	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {
			HttpConfiguration config = GlobalConfiguration.Configuration;
            GlobalConfiguration.Configure(WebApiConfig.Register);
	//		config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });
			AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			Database.SetInitializer(new DatabaseInitializer());

		}

        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
            "CreateBooking",                                              // Route name
            "{controller}/{action}/{Id},{name}, {Id2}",                           // URL with parameters
            new { controller = "Repo", action = "CreateBooking", Id = UrlParameter.Optional, name = UrlParameter.Optional, Id2 = UrlParameter.Optional });

            /* routes.MapRoute(
            "RelevantBookingSystems",                                              // Route name
            "{controller}/{action}/{Id},{name}",                           // URL with parameters
            new { controller = "BookingSystem", action = "RelevantBookingSystems" } // Parameter defaults
        );*/
        }
        
	}
}
