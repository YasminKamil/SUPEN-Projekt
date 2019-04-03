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

namespace SUPEN_Projekt.Controllers
{
    public class BookingApiController : ApiController{

        //private readonly ApplicationDbContext db;
        IUnitOfWork uw;

        public BookingApiController(IUnitOfWork unitofwork)
        {
            uw = unitofwork;
        }

		[HttpGet]
		public IEnumerable<Booking> Get()
        {
			IEnumerable<Booking> list = uw.Bookings.GetAll();
			return list;
		}

        [Route("api/GetSystem/{id}")]
        [HttpGet]
        public IHttpActionResult GetSystem(int id)
        {

            BookingSystem bookingSystem = uw.BookingSystems.GetTheBookingSystem(id);
            //Service service = uw.BookingSystems.Get(Servic);
			//IEnumerable<Service> listServcies = uw.BookingSystems.GetAllServices(id);
            //

            //ViewModel3 vm3 = new ViewModel3();
            //vm3.bookingSystem = bookingSystem;
            //vm3.services = listServices;
            //ViewBag.Message = vm3.bookingSystem.CompanyName;

            return Ok(bookingSystem);
        }

        [Route("api/getstrbooking")]
		[HttpGet]//utan IHttpActionResult får man inte med bra statuskoder
		public IHttpActionResult GetStr() {
			IEnumerable<Booking> list = uw.Bookings.GetAll();
			return Ok(list);
		}

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
	}
}

