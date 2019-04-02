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

		[Route("api/getstrbooking")]
		[HttpGet]
		public IEnumerable<Booking> GetStr() {
			IEnumerable<Booking> list = uw.Bookings.GetAll();
			return list;
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

