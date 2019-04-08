using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SUPEN_Projekt.Controllers {
	public class ApiBookingController : ApiController {

		//private readonly ApplicationDbContext db;
		IUnitOfWork uw;

		public ApiBookingController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Hämtar alla lagrade bokningar
		[Route("api/GetBookings")]
		[HttpGet]
		public IHttpActionResult GetBookings() {
			IEnumerable<Booking> bookings = uw.Bookings.GetAll();
			BookingsViewModel list = new BookingsViewModel();
			list.bookings = bookings;
			return Ok(list);
		}

		//Skapar en ny bokning i datakällan
		[Route("api/PostBooking")]
		public IHttpActionResult PostBooking(JObject inBookning) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

			Booking booking = JsonConvert.DeserializeObject<Booking>(inBookning.ToString());

			uw.Bookings.UpdateBooking(booking);
			uw.Complete();

			return Ok();
		}

		//Returnerar den specifika bokningen som har skapats
		[Route("api/GetBooking/{inBookingSystemId}/{inServiceId}/{inBookingId}")]
		[HttpGet]
		public IHttpActionResult GetBooking(int inBookingSystemId, int inServiceId, int inBookingId) {

			BookingSystem bs = uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();

			bsSBVM.bookingSystem = bs;
			bsSBVM.service = bs.Services.Single(x => x.ServiceId == inServiceId);
			bsSBVM.booking = bsSBVM.service.Bookings.Single(x => x.BookingId == inBookingId);

			if (bsSBVM == null) {
				return NotFound();
			}

			return Ok(bsSBVM);
		}

	}
}