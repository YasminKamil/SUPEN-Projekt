using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SUPEN_Projekt {
	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			Database.SetInitializer(new DatabaseInitializer());
		}
	}
}
//		SystemName = "boka.se",
//		SystemDescription = "Description...",
//		Email = "boka@boka.se",
//		PhoneNumber = "070 - 000 00 00",
//		Website = "boka.se/SalongFinest",
//		CompanyName = "Salong Finest",
//		ContactEmail = "salongfinest@boka.se",
//		ContactPhone = "070 - 123 56 78",
//		Address = "Köpmangatan 2",
//		Latitude = 1,
//		Longitude = 2,
//		PostalCode = "702 18",
//		City = "Örebro"