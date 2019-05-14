using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers {
	public class HomeController : Controller {
		//Returnerar en om oss sida
		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}
	}
}