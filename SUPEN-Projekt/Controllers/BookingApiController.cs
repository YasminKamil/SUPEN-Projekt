using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SUPEN_Projekt.Controllers
{
    public class BookingApiController : ApiController{
		private readonly ApplicationDbContext db;

		public BookingApiController() {
			db = new ApplicationDbContext();
		}
			[HttpGet]
			public IEnumerable<Booking> Get() {
			return db.Bookings.ToList();
		}

		[HttpPost]
		public Booking Create(Booking bookings) {
			if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			db.Bookings.Add(bookings);
			db.SaveChanges();
			return bookings;
		}

		public void Update(int id, Booking booking) {
			if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			var bookings = db.Bookings.SingleOrDefault(x => x.BookingId == id);
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

			db.SaveChanges();
		}

		[HttpDelete]
		public void Delete(int id) {
			var booking = db.Bookings.SingleOrDefault(x => x.BookingId == id);
			if(booking == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			db.Bookings.Remove(booking);
			db.SaveChanges();
		}
		}
    }

