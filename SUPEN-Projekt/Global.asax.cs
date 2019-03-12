﻿using SUPEN_Projekt.Models;
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
			config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });
			AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			Database.SetInitializer(new DatabaseInitializer());
		}
	}
}
