﻿using SUPEN_Projekt.Models;
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
		public IHttpActionResult GetStr() {

			IEnumerable<BookingSystem> bookingsystems = uw.BookingSystems.GetAll();
            BookingSystemsViewModel list = new BookingSystemsViewModel();
            list.bookingSystems = bookingsystems;
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
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

			ViewModel4 system = JsonConvert.DeserializeObject<ViewModel4>(insystem.ToString());

			uw.BookingSystems.Add(new BookingSystem() {
				BookingSystemId = system.bookingSystem.BookingSystemId,
				Address = system.bookingSystem.Address,
				City = system.bookingSystem.City,
				CompanyName = system.bookingSystem.CompanyName,
				ContactEmail = system.bookingSystem.ContactEmail,
				ContactPhone = system.bookingSystem.ContactPhone,
				Email = system.bookingSystem.Email,
				Latitude = system.bookingSystem.Latitude,
				Longitude = system.bookingSystem.Longitude,
				PhoneNumber = system.bookingSystem.PhoneNumber,
				PostalCode = system.bookingSystem.PostalCode,
				SystemDescription = system.bookingSystem.SystemDescription,
				SystemName = system.bookingSystem.SystemName,
				Website = system.bookingSystem.Website
			});
			uw.Complete();
			return Ok();
		}
	}
}
