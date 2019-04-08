using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SUPEN_Projekt.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Data.Entity;

namespace SUPEN_Projekt.Controllers {
	public class ApiBookingSystemController : ApiController {
		IUnitOfWork uw;

		public ApiBookingSystemController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		[HttpGet] //IHttpActionResult - detta kan vi ta bort
		public IEnumerable<BookingSystem> Get() {
			IEnumerable<BookingSystem> list = uw.BookingSystems.GetBookingSystems();
			return list;
		}

		[Route("api/getstr")] //GetSystems både i metod och routen
		[HttpGet] //IHttpActionResult
		public IEnumerable<BookingSystem> GetStr() {

			IEnumerable<BookingSystem> list = uw.BookingSystems.GetAll();
			//   var a=  JsonConvert.SerializeObject(list); 
			return list;
		}
		[Route("api/getRelevant/{bookingSystemId:int}/{serviceId:int}")] //GetRelevantSystem i både route och metod
		[HttpGet] //IHttpActionResult
		public IEnumerable<BookingSystem> GetRelevant(int bookingSystemId, int serviceId) {
			try {
				BookingSystem selectedBookingSystem = uw.BookingSystems.GetBookingSystem(bookingSystemId);
				Service selectedService = uw.Services.Get(serviceId);
				List<BookingSystem> bookingSystemsInRange = uw.BookingSystems.GetBookingSystemsInRange(selectedBookingSystem);
				List<BookingSystem> bookingSystemsInOtherBranches = uw.BookingSystems.GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
				List<BookingSystem> orderedByDistance = uw.BookingSystems.OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
				return orderedByDistance;
			} catch (Exception) {

				throw;
			}
		}
		//testa att lägga till return OK() + byta till IHttpActionResult
		[Route("api/GetBookingSystem/{bookingSystemId:int}")]
		[HttpGet]
		public BookingSystem GetBookingSystem(int bookingSystemId) {
			try {
				BookingSystem selectedBookingSystem = uw.BookingSystems.GetBookingSystem(bookingSystemId);
				return selectedBookingSystem;
			} catch (Exception) {
				throw;
			}
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

		[Route("api/GetBookingSystem/{bookingSystemAId:int}/{bookingSystemBId:int}")]
		[HttpGet]
		public double GetDistanceBetweenBookingSystems(int bookingSystemAId, int BookingSystemBId) {
			try {
				return uw.BookingSystems.GetDistanceTo(uw.BookingSystems.Get(bookingSystemAId), uw.BookingSystems.Get(BookingSystemBId));
			} catch (Exception) {
				throw;
			}
		}



		[Route("api/post")] //Är denna relevant, kommer man behöva skapa nya bokningsystem?
		public IHttpActionResult Post(JObject insystem) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

			BookingSystem system = JsonConvert.DeserializeObject<BookingSystem>(insystem.ToString());

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
