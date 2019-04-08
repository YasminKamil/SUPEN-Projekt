using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;

namespace SUPEN_Projekt.Controllers {
	public class BookingSystemController : Controller {

		//IUnitOfWork uw;
		//public BookingSystemController(IUnitOfWork unitofwork)
		//{
		//    uw = unitofwork;
		//}

		//Returnerar alla bokningsystem med ett api-anrop
		public async Task<ActionResult> Index() {
			BookingSystemsViewModel bsVM = null;
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetSystems").Result; 
			if (result.IsSuccessStatusCode) {
				bsVM = await result.Content.ReadAsAsync<BookingSystemsViewModel>();
			}
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
 
            return View(bsVM);
		}

		//Returnerar det valda bokningsystemets tjänster
		public async Task<ActionResult> BookingSystem(int id) {
			//string bookingsystem = "";
			ViewModel3 vm3 = null;
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetSystem/" + id).Result;
			if (result.IsSuccessStatusCode) {
				vm3 = await result.Content.ReadAsAsync<ViewModel3>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			return View(vm3);
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
		public ActionResult Create() {
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(BookingSystem system) {
			var url = "http://localhost:55341/api/post";

			if (await APIContact(url, system)) {
				return RedirectToAction("Index");
			}
			return View(system);

		}

		//Returnerar vilka bokningsystem som finns i närområdet efter att man har bokat en tjänst
		public async Task<ActionResult> RelevantBookingSystems(int bookingSystemId, int serviceId) {

			string list1 = "";
			HttpClient client1 = new HttpClient();
			string url1 = "http://localhost:55341/api/GetBookingSystem/" + bookingSystemId.ToString();
			var result1 = client1.GetAsync(url1).Result;
			if (result1.IsSuccessStatusCode) {
				list1 = await result1.Content.ReadAsStringAsync();
			}
			BookingSystem selectedBookingSystem = JsonConvert.DeserializeObject<BookingSystem>(list1);


			if (selectedBookingSystem == null || !selectedBookingSystem.Services.Any(x => x.ServiceId == serviceId)) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			List<BookingSystem> orderedByDistance = new List<BookingSystem>();

			string list = "";
			HttpClient client = new HttpClient();
			string url = "http://localhost:55341/api/GetRelevantBookingSystem/" + bookingSystemId.ToString() + "/" + serviceId.ToString();
			var result = client.GetAsync(url).Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsStringAsync();
			}
			orderedByDistance = JsonConvert.DeserializeObject<List<BookingSystem>>(list);

			ViewModel5 vm5 = new ViewModel5();
			List<BookingSystemAndDistance> listOfBookingSystems = new List<BookingSystemAndDistance>();
			foreach (var item in orderedByDistance) {
				BookingSystemAndDistance pairedObject = new BookingSystemAndDistance();
				pairedObject.BookingSystem = item;


				string list3 = "";
				HttpClient client3 = new HttpClient();
				var result3 = client3.GetAsync("http://localhost:55341/api/GetBookingSystem/" + selectedBookingSystem.BookingSystemId + "/" + item.BookingSystemId).Result;
				if (result3.IsSuccessStatusCode) {
					list3 = await result3.Content.ReadAsStringAsync();
				}

				list3 = list3.Replace('.', ',');

				double distance = double.Parse(list3);


				pairedObject.Distance = Math.Round(distance);
				listOfBookingSystems.Add(pairedObject);

			}
			vm5.SelectedBookingSystem = selectedBookingSystem;
			vm5.BookingsWithDistance = listOfBookingSystems;
			return PartialView(vm5);
		}


	}


}
