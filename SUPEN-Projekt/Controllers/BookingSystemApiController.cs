using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Device.Location;
using SUPEN_Projekt.Repositories;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemApiController : ApiController{
        
        IUnitOfWork uw;

        public BookingSystemApiController(IUnitOfWork unitofwork)
        {
            uw = unitofwork;
        }

        [HttpGet]
		public IEnumerable<BookingSystem> Get() {

            IEnumerable<BookingSystem> list = uw.BookingSystems.GetAll();
            return list;
		}

		[HttpPost]
		public BookingSystem Create(BookingSystem system) {
			if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			uw.BookingSystems.Add(system);
			uw.Complete();
			return system;
		}

        public double getDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB)
        {
            var aCoord = new GeoCoordinate(bookingSystemA.Latitude, bookingSystemA.Longitude);
            var bCoord = new GeoCoordinate(bookingSystemB.Latitude, bookingSystemB.Longitude);
            return aCoord.GetDistanceTo(bCoord);
        }

        public void Update(int id, BookingSystem system)
            {

            if (!ModelState.IsValid) {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

            BookingSystem bookingSystem = uw.BookingSystems.Get(id);

            if (bookingSystem == null) {
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

            uw.Complete();
		}

		[HttpDelete]
		public void Delete(int id)
        {
			BookingSystem system = uw.BookingSystems.Get(id);

			if(system == null)
            {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			uw.BookingSystems.Remove(system);
			uw.Complete();
		}

    }
}
