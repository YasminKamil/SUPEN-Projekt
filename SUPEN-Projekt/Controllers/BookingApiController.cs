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
	public class BookingApiController : ApiController {

		//private readonly ApplicationDbContext db;
		IUnitOfWork uw;

		public BookingApiController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		[HttpGet]
		public IEnumerable<Booking> Get() {
			IEnumerable<Booking> list = uw.Bookings.GetAll();
			return list;
		}

		//när denna funktion fungerar i BookingSystemController tas denna bort
		[Route("api/GetSystem/{id}")]
		[HttpGet]
		public IHttpActionResult GetSystem(int id) {

			BookingSystem bookingsystem = uw.BookingSystems.GetBookingSystem(id);
            ViewModel3 vm3 = new ViewModel3();
            vm3.bookingSystem = bookingsystem;
            vm3.services = bookingsystem.Services;
            return Ok(vm3);
		}

		//Byt namn till GetBookings både i route och metod
		//utan IHttpActionResult får man inte med bra statuskoder
		[Route("api/getstrbooking")]
		[HttpGet]
		public IHttpActionResult GetStr() {
			IEnumerable<Booking> bookings = uw.Bookings.GetAll();
            BookingsViewModel list = new BookingsViewModel();
            list.bookings = bookings;
			return Ok(list);
		}

		//Byt namn till PostBooking både i routing och metod
		[Route("api/postBooking")]
		public IHttpActionResult Post(JObject inBookning) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

			Booking booking = JsonConvert.DeserializeObject<Booking>(inBookning.ToString());

			uw.Bookings.UpdateBooking(booking);
			uw.Complete();

			return Ok();
		}

		//denna behöver vi arbeta med bör ju ta in bookingid
		[Route("api/GetBooking/{inBookingSystemId}/{inServiceId}/{inBookingId}")]
		[HttpGet]
		public IHttpActionResult GetBooking(int inBookingSystemId, int inServiceId, int inBookingId) {
			
			BookingSystem bs = uw.BookingSystems.GetBookingSystem(inBookingSystemId);
            ViewModel4 vm4 = new ViewModel4();

            vm4.bookingSystem = bs;
            vm4.service = bs.Services.Single(x => x.ServiceId == inServiceId);
            vm4.booking = vm4.service.Bookings.Single(x => x.BookingId == inBookingId);

            if (vm4 == null)
            {
                return NotFound();
            }

            return Ok(vm4);
		}

	}
}