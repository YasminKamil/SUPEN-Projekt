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

namespace SUPEN_Projekt.Controllers
{
    public class BookingController : Controller
    {
		IUnitOfWork uw;
		public BookingController(IUnitOfWork unitofwork) {
			uw = unitofwork;
		}

		//GET: Booking
		public ActionResult Index() {
			ViewModel myModel = new ViewModel();
			myModel.Bookings = uw.Bookings.GetAllBookings();
			//	myModel.Services = uw.Services.GetAllServices();
			return View(myModel);
		}

		public async Task<ActionResult> Index2() {
			string list = "";
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/getstrbooking").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsStringAsync();
			}

			List<Booking> objects = JsonConvert.DeserializeObject<List<Booking>>(list);
			return View(objects);
		}


		//[HttpPost]
		//public async Task<ActionResult> Create(Booking booking) {
		//	var url = "http://localhost:55341/api/postbooking";
		//	if(await APIContact(url, booking)) {
		//		return RedirectToAction("Index");
		//	}

		//	return View(booking);
		//}

		//public async Task<bool> APIContact(string inUrl, Object inObject) {
		//	bool works = false;
		//	var url = inUrl;
			 
		//	using(var client = new HttpClient()) {
		//		var content = new StringContent(JsonConvert.SerializeObject(inObject), Encoding.UTF8, "application/json");
		//		var result = await client.PostAsync(url, content);

		//		if (result.IsSuccessStatusCode) {
		//			works = true;
		//		}
		//	}

		//	return works;
		//}

		//[HttpPost]
		//public async Task<ActionResult> Create(BookingSystem booking) {
		//	var url = "https://localhost:55341/api/post";
		//	using (var client = new HttpClient()) {
		//		var content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
		//		var result = await client.PostAsync(url, content);
		//		if (result.IsSuccessStatusCode) {
		//			return RedirectToAction("Index");
		//		}
		//		return View(booking);
		//	}
		//}

		[Route("BookingSystem/{id:int}")]
        public ActionResult ABookingSystem(int id)
        {

            BookingSystem bookingSystem = uw.BookingSystems.GetTheBookingSystem(id);

            ViewModel3 vm3 = new ViewModel3();
            vm3.bookingSystem = bookingSystem;
            vm3.services = bookingSystem.Services.ToList();
            ViewBag.Message = vm3.bookingSystem.CompanyName;

            return View(vm3);
        }

        //[Route("BookingSystem/{id:int}")]
        public ActionResult CreateBooking(int id, string name)
        {
            BookingSystem bookingSystem = uw.BookingSystems.GetTheBookingSystem(id);
            ViewModel2 vm2 = new ViewModel2();
            vm2.bookingSystem = bookingSystem;
            vm2.service = bookingSystem.Services.Single(x => x.ServiceName == name);


            return PartialView("CreateBooking", vm2);//en vanlig vy, från början parameter bookingsystem
        }

        [HttpPost, ActionName("CreateBooking")]
        public ActionResult CreateBookingConf(int id, string name)
        {
            var serviceId = uw.Services.Find(x => x.ServiceName == name).Single().ServiceId;
            if (id != 0)
            {
                Booking booking = uw.Bookings.CreateBooking(id, name);
               
                uw.Complete();
                uw.Services.AddBooking(booking, serviceId);
                uw.Complete();
                return RedirectToAction("Details", new { InBookingSystemId = id, inServiceId=serviceId, inBookingId = booking.BookingId });
            }
            return RedirectToAction("ABookingSystem", new { id });
        }

        public ActionResult Details(int inBookingSystemId, int inServiceId, int inBookingId)
        {


            //if (inBookingSystemId == null || inServiceId == null || inBookingSystemId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Booking booking = uw.Bookings.Get(id);


            ViewModel4 vm4 = new ViewModel4();

            vm4.bookingSystem = uw.BookingSystems.GetTheBookingSystem(inBookingSystemId);
            vm4.service = vm4.bookingSystem.Services.Single(x=>x.ServiceId == inServiceId);
            vm4.booking = vm4.service.Bookings.Single(x=>x.BookingId == inBookingId);

            if (vm4.service == null|| vm4.booking == null|| vm4.bookingSystem == null)
            {
                return HttpNotFound();
            }

            return View(vm4);
        }

		public ActionResult Update(int inBookingSystemId, int inServiceId, int inBookingId) {
			

			ViewModel4 vm4 = new ViewModel4();
			vm4.booking= uw.Bookings.Get(inBookingId);
			vm4.bookingSystem = uw.BookingSystems.Get(inBookingSystemId);
			vm4.service = uw.Services.Get(inServiceId);
			return View(vm4);
		}

		[HttpPost, ActionName("UpdateBooking")]
		public ActionResult Update(Booking booking) {
			try {
				if (ModelState.IsValid) {
					uw.Bookings.UpdateBooking(booking);
					return RedirectToAction("Index");
				}
			} catch (DataException) {
				ModelState.AddModelError("", "Unable to save changes, please try again");
			}
			return View(booking);
		}

    }
}