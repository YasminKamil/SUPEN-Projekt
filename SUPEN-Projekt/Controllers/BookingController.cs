﻿using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SUPEN_Projekt.Logic;


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

		//Returnerar information om bokningen
		public async Task<ActionResult> Details(int inBookingSystemId, int inServiceId, int inBookingId) {

			BookingSystemServiceBookingViewModel bsSBVM = null;
			HttpClient client = new HttpClient();

			var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + inBookingSystemId +
				"/" + inServiceId + "/" + inBookingId).Result;

			if (result.IsSuccessStatusCode) {
				bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}

			return View(bsSBVM);
		}

		//Hämtar information för en bokning innan det går att genomföra en bokning
		[HttpGet]
		public async Task<ActionResult> BookService(int inBookingSystemId, int inServiceId, int inBookingId) {

			BookingSystemServiceBookingViewModel bsSBVM = null;
			HttpClient client = new HttpClient();

			var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + inBookingSystemId +
				"/" + inServiceId + "/" + inBookingId).Result;

			if (result.IsSuccessStatusCode) {
				bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}

			return View(bsSBVM);
		}

		//Bokar en tillgänglig tjänst
		[HttpPost, ActionName("BookService")]
		public async Task<ActionResult> BookServiceConfirmed(int inBookingSystemId, int inServiceId, int inBookingId, BookingSystemServiceBookingViewModel model) {
			try {
				var url = "http://localhost:55341/api/PostBooking";

				if (await APIContact(url, model)) {
					return RedirectToAction("Details",
						new { inBookingSystemId, inServiceId, inBookingId });
				}

			} catch (DataException) {
				ModelState.AddModelError("", "Unable to save changes, please try again");
			}
			return View(model);
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

				return works;
			}
		}

		////[Route("BookingSystem/{id:int}")]
		//public ActionResult CreateBooking(int id, string name) {
		//	BookingSystem bookingSystem = uw.BookingSystems.GetBookingSystem(id);
		//	BookingSystemServiceBookingViewModel vm4 = new BookingSystemServiceBookingViewModel();
		//	vm4.bookingSystem = bookingSystem;
		//	vm4.service = bookingSystem.Services.Single(x => x.ServiceName == name);


		//	return View("CreateBooking", vm4);//en vanlig vy, från början parameter bookingsystem
		//}

		//[HttpPost, ActionName("CreateBooking")]
		//public ActionResult CreateBookingConfirmed(int id, string name, Booking inBooking) {
		//	var serviceId = uw.Services.Find(x => x.ServiceName == name).Single().ServiceId;
		//	if (id != 0) {
		//		Booking booking = uw.Bookings.CreateBooking(inBooking);
		//		uw.Services.AddBooking(booking, serviceId);
		//		uw.Complete();
		//		return RedirectToAction("Details", new { InBookingSystemId = id, inServiceId = serviceId, inBookingId = booking.BookingId });
		//	}
		//	return RedirectToAction("ABookingSystem", new { id });//när abookingsystem är flyttad ändrad, ändra här också
		//}
	}
}