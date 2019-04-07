using Newtonsoft.Json;
using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers {
	public class BookingController : Controller {
		IUnitOfWork uw;
		public BookingController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//GET: Booking detat kan tas bort
		public ActionResult Index() {
			ViewModel myModel = new ViewModel();
			myModel.Bookings = uw.Bookings.GetAllBookings();
			//	myModel.Services = uw.Services.GetAllServices();
			return View(myModel);
		}

		//döpas om till Index när ovanstående är borta
		public async Task<ActionResult> Index2() {
			string list = "";
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/getstrbooking").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsStringAsync();
			}

			//objects till bookings
			List<Booking> objects = JsonConvert.DeserializeObject<List<Booking>>(list);
			return View(objects);
		}

		//Denna ska flyttas till bookingsystemcontroller och namnsättas till BookingSystem
		public async Task<ActionResult> ABookingSystem(int id) {
			string bookingsystem = "";
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetSystem/" + id).Result;
			if (result.IsSuccessStatusCode) {
				bookingsystem = await result.Content.ReadAsStringAsync();
			}

			var bs = JsonConvert.DeserializeObject<BookingSystem>(bookingsystem);
			ViewModel3 vm3 = new ViewModel3();
			vm3.bookingSystem = bs;
			vm3.services = bs.Services;
			return View(vm3);
		}

		//[Route("BookingSystem/{id:int}")]
		public ActionResult CreateBooking(int id, string name) {
			BookingSystem bookingSystem = uw.BookingSystems.GetTheBookingSystem(id);
			ViewModel4 vm4 = new ViewModel4();
			vm4.bookingSystem = bookingSystem;
			vm4.service = bookingSystem.Services.Single(x => x.ServiceName == name);


			return View("CreateBooking", vm4);//en vanlig vy, från början parameter bookingsystem
		}

		[HttpPost, ActionName("CreateBooking")]
		public ActionResult CreateBookingConfirmed(int id, string name, Booking inBooking) {
			var serviceId = uw.Services.Find(x => x.ServiceName == name).Single().ServiceId;
			if (id != 0) {
				Booking booking = uw.Bookings.CreateBooking(inBooking);
				uw.Services.AddBooking(booking, serviceId);
				uw.Complete();
				return RedirectToAction("Details", new { InBookingSystemId = id, inServiceId = serviceId, inBookingId = booking.BookingId });
			}
			return RedirectToAction("ABookingSystem", new { id });//när abookingsystem är flyttad ändrad, ändra här också
		}

		public ActionResult Details(int inBookingSystemId, int inServiceId, int inBookingId) {


			//if (inBookingSystemId == null || inServiceId == null || inBookingSystemId == null)
			//{
			//    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//}
			//Booking booking = uw.Bookings.Get(id);


			ViewModel4 vm4 = new ViewModel4();//

			vm4.bookingSystem = uw.BookingSystems.GetTheBookingSystem(inBookingSystemId);
			vm4.service = vm4.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);
			vm4.booking = vm4.service.Bookings.Single(x => x.BookingId == inBookingId);

			if (vm4.service == null || vm4.booking == null || vm4.bookingSystem == null) {
				return HttpNotFound();
			}

			return View(vm4);
		}

		[HttpGet]//BookService
		public async Task<ActionResult> Update(int inBookingSystemId, int inServiceId, int inBookingId) {
			ViewModel4 vm4 = null;

            HttpClient client = new HttpClient();

			var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + inBookingSystemId + "/" + inServiceId + "/" + inBookingId).Result;

			if (result.IsSuccessStatusCode) {
				vm4 = await result.Content.ReadAsAsync<ViewModel4>();
			}

			//BookingSystem bs = JsonConvert.DeserializeObject<BookingSystem>(list);
			//ViewModel4 vm4 = new ViewModel4();

			//vm4.bookingSystem = bs;
			//vm4.service = bs.Services.Single(x => x.ServiceId == inServiceId);
			//vm4.booking = vm4.service.Bookings.Single(x => x.BookingId == inBookingId);

			return View(vm4);

		}

		[HttpPost, ActionName("Update")]//BookService alternativt BookServiceConfirmed
		public async Task<ActionResult> UpdateBooking(int inBookingSystemId, int inServiceId, int inBookingId, Booking booking) {
			try {
				var url = "http://localhost:55341/api/postBooking";

				if (await APIContact(url, booking)) {
					return RedirectToAction("Details",
						new { inBookingSystemId, inServiceId, inBookingId });
				}

			} catch (DataException) {
				ModelState.AddModelError("", "Unable to save changes, please try again");
			}
			return View(booking);
		}

		public async Task<bool> APIContact(string inUrl, Object inObject) {
			bool works = false;
			var url = inUrl;


			//HttpClient client = new HttpClient();
			using (var client = new HttpClient()) {
				var content = new StringContent(JsonConvert.SerializeObject(inObject), Encoding.UTF8, "application/json");
				var result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode) {
					works = true;
				}

				return works;
			}
		}
	}
}