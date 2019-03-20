using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SUPEN_Projekt.Repositories;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemApiController : ApiController{
       IUnitOfWork uw;

        public BookingSystemApiController(IUnitOfWork unitofwork) {
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


		[HttpPut]
		public void Update(int id, BookingSystem system) {
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

		public IHttpActionResult Post(BookingSystem system) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

			uw.BookingSystems.Add(new BookingSystem() {
				BookingSystemId = system.BookingSystemId,
				Address = system.Address,
				City = system.City,
				CompanyName = system.CompanyName,
				ContactEmail = system.ContactEmail,
				ContactPhone = system.ContactPhone,
				Email = system.Email,
				Latitude = system.Latitude,
				Longitude = system.Longitude,
				PhoneNumber = system.PhoneNumber,
				PostalCode = system.PostalCode,
				SystemDescription = system.SystemDescription,
				SystemName = system.SystemName,
				Website = system.Website
		});
			uw.Complete();
			return Ok();
		}
    }
}
