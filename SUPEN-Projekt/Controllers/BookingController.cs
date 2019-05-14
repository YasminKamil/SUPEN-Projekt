using Newtonsoft.Json;
using SUPEN_Projekt.Logic.ViewModels;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers {
	public class BookingController : Controller {
		//Returnerar alla bokningar med ett api-anrop
		public async Task<ActionResult> Index() {
			BookingsViewModel list = null;
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetBookings").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsAsync<BookingsViewModel>();
			}
			return View(list);
		}

		//Hämtar information om bokningen som användaren försöker boka
		[HttpGet]
		public async Task<ActionResult> BookService(int inBookingSystemId, int inServiceId, string inStartTime, int? branchAId) {
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
			HttpClient client = new HttpClient();

			//Gör ett API-anrop för att hämta information om bokningen som tillhör till ett specifikt bokningssystem och tjänst
			var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + inBookingSystemId + "/" + inServiceId).Result;
			if (result.IsSuccessStatusCode) {
				bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}
			bsSBVM.branchAId = branchAId ?? default(int);
			bsSBVM.startTime = Convert.ToDateTime(inStartTime);
			return View(bsSBVM);
		}

		//Skapar en ny bokning som användaren har valt att boka
		[HttpPost, ActionName("BookService")]
		public async Task<ActionResult> BookServiceConfirmed(int inBookingSystemId, int inServiceId, BookingSystemServiceBookingViewModel model) {
			try {
				BookingSystemServiceBookingViewModel getService = new BookingSystemServiceBookingViewModel();
				HttpClient client = new HttpClient();

				//Hämtar servicen med ett api-anrop för bokningen som tillhör ett bokningssystem och service id:et
				var result = client.GetAsync("http://localhost:55341/api/GetService/" + inBookingSystemId + "/" + inServiceId).Result;
				if (result.IsSuccessStatusCode) {
					getService = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
				} else {
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
				}

				//Tilldelar parametervärdet-model den tjänsten som väljs i bokningsflödet
				model.service = getService.service;

				BookingSystemServiceBookingViewModel getSystem = null;
				HttpClient client1 = new HttpClient();

				//Hämtar bokningssystemet med ett api-anrop för bokningen som tillhör ett specifikt bokningssystem
				var result1 = client1.GetAsync("http://localhost:55341/api/GetBookingSystem/" + inBookingSystemId).Result;
				if (result1.IsSuccessStatusCode) {
					getSystem = await result1.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
				} else {
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}

				//Tilldelar parametervärdet-model det bokningssystemet som väljs i bokningsflödet.
				model.bookingSystem = getSystem.bookingSystem;

				//Om man nyligen har bokat en tjänst läggs det till ett klick på relationen eller så skapas den
				if (model.branchAId != 0 && model.branchAId != null) {
					await UpdateBranchRelationAsync(model.branchAId ?? default(int), model.service.Branch.BranchId);
				}

				//Tilldelar parametervärdet-model branchA Id:et som den tillhör
				model.branchAId = model.service.Branch.BranchId;
				var url = "http://localhost:55341/api/PostBooking";

				//Gör ett metodanrop till APIContact-metoden som kontrollerar url och model-värden som passeras in.
				if (await APIContact(url, model)) {
					//Returnerar bokningsinformation genom att anropa på metoden Details
					return RedirectToAction("Details",
						new { inBookingSystemId, inServiceId });
				}
			} catch (DataException) {
				ModelState.AddModelError("", "Unable to save changes, please try again");
			}
			return View(model);
		}

		//Returnerar bokningsbekräftelse på bokningen
		public async Task<ActionResult> Details(int inBookingSystemId, int inServiceId) {
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
			HttpClient client = new HttpClient();

			BookingSystemServiceBookingViewModel getSystem = null;
			HttpClient client1 = new HttpClient();

			//Gör ett api-anrop för att hämta det valda bokningssystemet i bokningsflödet
			var result1 = client1.GetAsync("http://localhost:55341/api/GetBookingSystem/" + inBookingSystemId).Result;
			if (result1.IsSuccessStatusCode) {
				getSystem = await result1.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}

			//Tilldelar objektet värdet på bokningsystemet som är valt.
			bsSBVM.bookingSystem = getSystem.bookingSystem;

			BookingSystemServiceBookingViewModel getService = null;
			HttpClient client2 = new HttpClient();

			//Gör ett api-anrop för att hämta tjänsten som är valt i bokningsflödet för bokningssystemet
			var result2 = client2.GetAsync("http://localhost:55341/api/GetService/" + inBookingSystemId + "/" + inServiceId).Result;
			if (result2.IsSuccessStatusCode) {
				getService = await result2.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}

			//Tilldelar objektet värdet på tjänsten som är valt på bokningssystemet
			bsSBVM.service = getService.service;

			BookingSystemServiceBookingViewModel getBookingWithMaxId = null;
			HttpClient client3 = new HttpClient();

			//Gör ett api-anrop för att hämta det högsta id:et för bokningarna
			var result3 = client3.GetAsync("http://localhost:55341/api/GetBooking/GetMaxId").Result;
			if (result3.IsSuccessStatusCode) {
				getBookingWithMaxId = await result3.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}

			//Gör ett api-anrop och hämtar bokningen med det högsta id:et
			var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + getBookingWithMaxId.booking.BookingId).Result;
			if (result.IsSuccessStatusCode) {
				getBookingWithMaxId = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			bsSBVM.booking = getBookingWithMaxId.booking;
			bsSBVM.bookings = getBookingWithMaxId.bookings;
			bsSBVM.bookingSystems = getBookingWithMaxId.bookingSystems;

			return View(bsSBVM);
		}

		//Uppdaterar relationen mellan branch A och branchB genom att göra ett api-anrop till en post metod i API:et
		[HttpPost]
		public async Task UpdateBranchRelationAsync(int inBranchA, int inBranchB) {
			object branchId = new { branchA = inBranchA, branchB = inBranchB };
			var url = "http://localhost:55341/api/UpdateBranchRelation/" + inBranchA.ToString() + "/" + inBranchB.ToString();
			await APIContact(url, branchId);
		}

		/*Metoden används för att göra interna metodanrop, som tar in url:en som används för att kontrollera url:en som används
		 för att göra api-anrop och objekten som ska tas in i api-anropet*/
		public async Task<bool> APIContact(string inUrl, Object inObject) {
			bool works = false;
			var url = inUrl;
			using (var client = new HttpClient()) {

				//Serialiserar objekten som tas in till JSON-objekt
				var content = new StringContent(JsonConvert.SerializeObject(inObject), Encoding.UTF8, "application/json");
				var result = await client.PostAsync(url, content);
				if (result.IsSuccessStatusCode) {
					works = true;
				}
			}
			return works;
		}
	}
}