using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers {
	public class ServiceController : Controller {

		//Returnerar flera tjänster
		public async Task<ActionResult> Index() {
			ServicesViewModel list = null;
			HttpClient client = new HttpClient();

			//Gör ett api-anrop för att hämta flera tjänster
			var result = client.GetAsync("http://localhost:55341/api/GetServices").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsAsync<ServicesViewModel>();
			}

			return View(list);
		}

		//Returnerar tjänster som möjliggör en bokning i ett bokningsystem 
		public async Task<ActionResult> BookService(int inBookingSystemId, int inServiceId) {

			BookingSystemServiceBookingViewModel bsSBVM = null;
			HttpClient client = new HttpClient();

			//Gör ett api-anrop för att hämta en tjänst för ett bokningssystem
			var result = client.GetAsync("http://localhost:55341/api/GetService/" + inBookingSystemId + "/" + inServiceId).Result;

			if (result.IsSuccessStatusCode) {
				bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}
			return View(bsSBVM);
		}

		//Returnerar förslag på tjänster
		public async Task<ActionResult> ServiceSuggestion(int inBookingId, string inServiceName, string inBookingSystemId) {
			ServiceViewModel serviceViewModel = null;
			HttpClient client = new HttpClient();

			//Gör ett api-anrop för att hämta en tjänst 
			var result = client.GetAsync("http://localhost:55341/api/GetService/" +
				inBookingId + "/" + inServiceName + "/" + inBookingSystemId).Result;

			if (result.IsSuccessStatusCode) {
				serviceViewModel = await result.Content.ReadAsAsync<ServiceViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}

			return PartialView(serviceViewModel);
		}
	}
}
