using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using SUPEN_Projekt.Logic;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers {
	public class BookingSystemController : Controller {
		//Returnerar alla bokningsystem med ett api-anrop
		public async Task<ActionResult> Index() {
			BookingSystemsViewModel bsVM = null;
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetSystems").Result;
			if (result.IsSuccessStatusCode) {
				bsVM = await result.Content.ReadAsAsync<BookingSystemsViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}

			return View(bsVM);
		}

		//Returnerar ett bokningssystem med ett api-anrop
		public async Task<ActionResult> BookingSystem(int id) {
			BookingSystemServicesViewModel bsSVM = null;
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetBookingSystem/" + id).Result;
			if (result.IsSuccessStatusCode) {
				bsSVM = await result.Content.ReadAsAsync<BookingSystemServicesViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			return View(bsSVM);
		}

		//Returnerar vilka bokningsystem som finns i närområdet efter att man har bokat en tjänst
		public async Task<ActionResult> RelevantBookingSystems(int bookingSystemId, int serviceId, int bookingId, int? branchAId) {

			BookingSystemServicesViewModel bsSVM = null;
			HttpClient client1 = new HttpClient();

			//Gör ett api-anrop för att hämta bokningssystemet
			string url1 = "http://localhost:55341/api/GetBookingSystem/" + bookingSystemId.ToString();
			var result1 = client1.GetAsync(url1).Result;
			if (result1.IsSuccessStatusCode) {
				bsSVM = await result1.Content.ReadAsAsync<BookingSystemServicesViewModel>();
			}
			//Tilldelar variabeln värdet på det valda bokningssystemet
			var selectedBookingSystem = bsSVM.bookingSystem;
			if (selectedBookingSystem == null || !selectedBookingSystem.Services.Any(x => x.ServiceId == serviceId)) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			BookingSystemsViewModel bsVM = null;
			HttpClient client = new HttpClient();

			//Gör ett api-anrop för att hämta relevantabokningssystem
			string url = "http://localhost:55341/api/GetRelevantBookingSystem/" + bookingSystemId.ToString() + "/" + serviceId.ToString() + "/" + bookingId.ToString();
			var result = client.GetAsync(url).Result;
			if (result.IsSuccessStatusCode) {
				bsVM = await result.Content.ReadAsAsync<BookingSystemsViewModel>();
			}

			var orderedByDistance = bsVM.bookingSystems;

			BookingsWithDistanceViewModel bWDVM = new BookingsWithDistanceViewModel();
			//Om man nyligen har bokat en tjänst läggs det till ett klick på relationen eller så skapas den
			if (branchAId != null) {
				bWDVM.branchAId = branchAId ?? default(int);
			} else {
				bWDVM.branchAId = selectedBookingSystem.Services.Single(x => x.ServiceId == serviceId).Branch.BranchId;
			}

			//Instansierar en lista på bokningssystem
			List<BookingSystemAndDistanceViewModel> listOfBookingSystems = new List<BookingSystemAndDistanceViewModel>();
			foreach (var item in orderedByDistance) {
				BookingSystemAndDistanceViewModel pairedObject = new BookingSystemAndDistanceViewModel();
				pairedObject.BookingSystem = item;
				string list3 = "";
				HttpClient client3 = new HttpClient();

				//Gör ett api-anrop för att hämta det först valda bokningssystemet och bokningssystem i närområdet.
				var result3 = client3.GetAsync("http://localhost:55341/api/GetBookingSystem/" + selectedBookingSystem.BookingSystemId + "/" + item.BookingSystemId).Result;
				if (result3.IsSuccessStatusCode) {
					list3 = await result3.Content.ReadAsStringAsync();
				}

				//Beräknar distansen mellan bokningsystemen
				list3 = list3.Replace('.', ',');
				double distance = double.Parse(list3);
				pairedObject.Distance = Math.Round(distance);
				pairedObject.Latitude = pairedObject.BookingSystem.Latitude;
				pairedObject.Longitude = pairedObject.BookingSystem.Longitude;
				pairedObject.CompanyName = pairedObject.BookingSystem.CompanyName;
				pairedObject.Id = pairedObject.BookingSystem.BookingSystemId;
				listOfBookingSystems.Add(pairedObject);
			}
			bWDVM.SelectedBookingSystem = selectedBookingSystem;
			bWDVM.BookingsWithDistance = listOfBookingSystems;
			return PartialView(bWDVM);
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