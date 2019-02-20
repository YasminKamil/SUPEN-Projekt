using System.Web;
using System.Web.Mvc;

namespace SUPEN_Projekt {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
