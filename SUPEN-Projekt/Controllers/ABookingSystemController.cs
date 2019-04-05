using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers {
	public class ABookingSystemController : Controller {
		UnitOfWork uw;

		public ABookingSystemController(UnitOfWork unityOfWork) {
			uw = unityOfWork;
		}

		// GET: ABookingSystem
		[Route("BookingSystem/{id:int}")]
		public ActionResult Index(int id) {
			BookingSystem bookingSystem = uw.BookingSystems.Get(id);
			ViewBag.Message = bookingSystem.CompanyName;
			return View(bookingSystem);
		}
	}
}