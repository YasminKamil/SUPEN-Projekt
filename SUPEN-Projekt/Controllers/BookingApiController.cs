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

        /*[Route("api/post")]
        public IHttpActionResult PostBooking(Booking booking)
        {
			if (!ModelState.IsValid)
            {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			uw.Bookings.Add(booking);
			uw.Complete();

			return Created(booking);
		}*/

		public void Update(int id, Booking booking) {
			if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			Booking bookings = uw.Bookings.Get(id);
			if(bookings == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			bookings.UserName = booking.UserName;
			bookings.UserMobile = booking.UserMobile;
			bookings.UserMail = booking.UserMail;
			bookings.Subject = booking.Subject;
			bookings.StartTime = booking.StartTime;
			bookings.EndTime = booking.EndTime;
			bookings.Price = booking.Price;

			uw.Complete();
		}

		[HttpDelete]
		public void Delete(int id) {
            Booking booking = uw.Bookings.Get(id);// SingleOrDefault(x => x.BookingId == id);
			if(booking == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			uw.Bookings.Remove(booking);
			uw.Complete();
		}
		}
    }

