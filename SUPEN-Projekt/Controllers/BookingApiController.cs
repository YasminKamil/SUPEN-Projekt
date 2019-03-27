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


		//[Route("api/postbooking")]
		//public IHttpActionResult Post(JObject inbooking) {
		//	if (!ModelState.IsValid) {
		//		return BadRequest("Invalid data");
		//	}

		//	//Booking booking = uw.Bookings.Include(x => x.ServiceId);

		//	Booking booking = JsonConvert.DeserializeObject<Booking>(inbooking.ToString());


		//	uw.Bookings.Add(new Booking() {
		//		BookingId = booking.BookingId,
		//		UserName = booking.UserName,
		//		UserMobile = booking.UserMobile,
		//		Subject = booking.Subject,
		//		//StartTime = booking.StartTime,
		//		//EndTime = booking.EndTime,
		//		Price = booking.Price,
		//		//ServiceId = booking.ServiceId,
		//		//BookingSystemId = booking.BookingSystemId,
		//		//Services= new List<Service> { uw.Services.Get(1) }
		//	});


			//var bookings = uw.Bookings.Include(x => x.Services);
			//foreach (Booking b in bookings) {
			//	foreach(Service s in b.Services) {
			//		uw.Bookings.Add(b.BookingId + b.UserName + b.UserMobile + b.Subject + b.StartTime + b.EndTime + b.Price + s.ServiceId + s.ServiceName);
			//	}
			//}







		//	uw.Complete();
		//	return Ok();
		//}

  //      /*[Route("api/post")]
  //      public IHttpActionResult PostBooking(Booking booking)
  //      {
		//	if (!ModelState.IsValid)
  //          {
		//		throw new HttpResponseException(HttpStatusCode.BadRequest);
		//	}

		//	uw.Bookings.Add(booking);
		//	uw.Complete();

		//	return Created(booking);
		//}*/

		//public void Update(int id, Booking booking) {
		//	if (!ModelState.IsValid) {
		//		throw new HttpResponseException(HttpStatusCode.BadRequest);
		//	}

		//	Booking bookings = uw.Bookings.Get(id);
		//	if(bookings == null) {
		//		throw new HttpResponseException(HttpStatusCode.NotFound);
		//	}

		//	bookings.UserName = booking.UserName;
		//	bookings.UserMobile = booking.UserMobile;
		//	bookings.UserMail = booking.UserMail;
		//	bookings.Subject = booking.Subject;
		//	bookings.StartTime = booking.StartTime;
		//	bookings.EndTime = booking.EndTime;
		//	bookings.Price = booking.Price;

		//	uw.Complete();
		//}

		//[HttpDelete]
		//public void Delete(int id) {
  //          Booking booking = uw.Bookings.Get(id);// SingleOrDefault(x => x.BookingId == id);
		//	if(booking == null) {
		//		throw new HttpResponseException(HttpStatusCode.NotFound);
		//	}

		//	uw.Bookings.Remove(booking);
		//	uw.Complete();
		//}
		}
    }

