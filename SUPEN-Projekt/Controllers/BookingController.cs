using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        // GET: Booking
        public ActionResult Index()
        {
			ViewModel myModel = new ViewModel();
			myModel.Bookings = uw.Bookings.GetAllBookings();
		//	myModel.Services = uw.Services.GetAllServices();
            return View(myModel);
        }

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
            if (id != 0)
            {
                Booking booking = uw.Bookings.CreateBooking(id, name);
                uw.Complete();
                return RedirectToAction("Details", new { id = booking.BookingId });
            }
            return RedirectToAction("ABookingSystem", new { id });
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = uw.Bookings.Get(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

    }
}