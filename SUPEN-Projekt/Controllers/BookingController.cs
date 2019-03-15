using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}