using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemApiController : ApiController{
		private readonly ApplicationDbContext db;

		public BookingSystemApiController() {
			db = new ApplicationDbContext();
		}

		[HttpGet]
		public IEnumerable<BookingSystem> Get() {
			return db.BookingSystems.ToList();
		}

		[HttpPost]
		public BookingSystem Create(BookingSystem system) {
			if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			db.BookingSystems.Add(system);
			db.SaveChanges();
			return system;
		}

		public void Update(int id, BookingSystem system) {
			if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			var bookingSystem = db.BookingSystems.SingleOrDefault(x => x.BookingSystemId == id);
			if(bookingSystem == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			bookingSystem.Address = system.Address;
			bookingSystem.City = system.City;
			bookingSystem.CompanyName = system.CompanyName;
			bookingSystem.ContactEmail = system.ContactEmail;
			bookingSystem.ContactPhone = system.ContactPhone;
			bookingSystem.Email = system.Email;
			bookingSystem.Latitude = system.Latitude;
			bookingSystem.Longitude = system.Longitude;
			bookingSystem.PhoneNumber = system.PhoneNumber;
			bookingSystem.PostalCode = system.PostalCode;
			bookingSystem.SystemDescription = system.SystemDescription;
			bookingSystem.SystemName = system.SystemName;
			bookingSystem.Website = system.Website;

			db.SaveChanges();
		}

		[HttpDelete]
		public void Delete(int id) {
			var system = db.BookingSystems.SingleOrDefault(x => x.BookingSystemId == id);

			if(system == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			db.BookingSystems.Remove(system);
			db.SaveChanges();
		}

    }
}
