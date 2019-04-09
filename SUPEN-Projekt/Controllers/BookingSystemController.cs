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

		//Returnerar det valda bokningsystemets tjänster
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

		//Ett API-anrop till ApiBooking som serialiserar det använda objektet till JSON
		public async Task<bool> APIContact(string inUrl, Object inObject) {
			bool works = false;
			var url = inUrl;

			using (var client = new HttpClient()) {
				var content = new StringContent(JsonConvert.SerializeObject(inObject), Encoding.UTF8, "application/json");
				var result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode) {
					works = true;
				}
			}

			return works;
		}

		// GET: BookingSystem/Create
		public async Task<ActionResult> Create() {
            BookingSystemServiceBookingViewModel bsSBVM = null;
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:55341/api/GetBookingSystem/").Result;
            if (result.IsSuccessStatusCode)
            {
                bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(bsSBVM);
            //return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(BookingSystemServiceBookingViewModel system) {
			var url = "http://localhost:55341/api/PostBookingSystem";

			if (await APIContact(url, system)) {
                return RedirectToAction("Index");
			}

			return View(system);

		}

        //Returnerar vilka bokningsystem som finns i närområdet efter att man har bokat en tjänst
        public async Task<ActionResult> RelevantBookingSystems(int bookingSystemId, int serviceId)
        {

            BookingSystemServicesViewModel bssvm = null;
            HttpClient client1 = new HttpClient();
            string url1 = "http://localhost:55341/api/GetBookingSystem/" + bookingSystemId.ToString();
            var result1 = client1.GetAsync(url1).Result;
            if (result1.IsSuccessStatusCode)
            {
                bssvm = await result1.Content.ReadAsAsync<BookingSystemServicesViewModel>();
            }
            var selectedBookingSystem = bssvm.bookingSystem;

            if (selectedBookingSystem == null || !selectedBookingSystem.Services.Any(x => x.ServiceId == serviceId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookingSystemsViewModel bookingsystemsvm = null;
            HttpClient client = new HttpClient();
            string url = "http://localhost:55341/api/GetRelevantBookingSystem/" + bookingSystemId.ToString() + "/" + serviceId.ToString();
            var result = client.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
            {
                bookingsystemsvm = await result.Content.ReadAsAsync<BookingSystemsViewModel>();
            }
            var orderedByDistance = bookingsystemsvm.bookingSystems;

            BookingsWithDistanceViewModel bWDVM = new BookingsWithDistanceViewModel();
            List<BookingSystemAndDistanceViewModel> listOfBookingSystems = new List<BookingSystemAndDistanceViewModel>();
            foreach (var item in orderedByDistance)
            {
                BookingSystemAndDistanceViewModel pairedObject = new BookingSystemAndDistanceViewModel();
                pairedObject.BookingSystem = item;


                string list3 = "";
                HttpClient client3 = new HttpClient();
                var result3 = client3.GetAsync("http://localhost:55341/api/GetBookingSystem/" + selectedBookingSystem.BookingSystemId + "/" + item.BookingSystemId).Result;
                if (result3.IsSuccessStatusCode)
                {
                    list3 = await result3.Content.ReadAsStringAsync();
                }

                list3 = list3.Replace('.', ',');

                double distance = double.Parse(list3);

                pairedObject.Distance = Math.Round(distance);
                listOfBookingSystems.Add(pairedObject);
            }

            bWDVM.SelectedBookingSystem = selectedBookingSystem;
            bWDVM.BookingsWithDistance = listOfBookingSystems;
            return PartialView(bWDVM);
        }

    }


}
