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

		//Hämtar alla lagrade bokningsystem
		[Route("api/GetSystems")] 
		[HttpGet] 
		public IHttpActionResult GetSystems() {

			IEnumerable<BookingSystem> bookingsystems = uw.BookingSystems.GetAll();
            BookingSystemsViewModel list = new BookingSystemsViewModel();
            list.bookingSystems = bookingsystems;
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
		}

		//Hämtar det relevanta bokningsystemet inom ett visst område och inom andra branscher ordnat efter avståndet
		[Route("api/GetRelevantBookingSystem/{bookingSystemId:int}/{serviceId:int}")] 
		[HttpGet] 
		public IEnumerable<BookingSystem> GetRelevantBookingSystem(int bookingSystemId, int serviceId) {
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
		
		//Hämtar ut det valda bokningsystemet
		[Route("api/GetBookingSystem/{bookingSystemId:int}")]
		[HttpGet]
		public IHttpActionResult GetBookingSystem(int bookingSystemId) {
			BookingSystem bs = uw.BookingSystems.GetBookingSystem(bookingSystemId);
			return Ok(bs);
			
			//try {
			//	BookingSystem selectedBookingSystem = uw.BookingSystems.GetBookingSystem(bookingSystemId);
			//	return selectedBookingSystem;
			//} catch (Exception) {
			//	throw;
			//}
		}

		//Hämtar bokningsystemet och tjänsten för bokningsystemet
		[Route("api/GetSystem/{id}")]
		[HttpGet]
		public IHttpActionResult GetSystem(int id) {

			BookingSystem bookingsystem = uw.BookingSystems.GetBookingSystem(id);
			ViewModel3 vm3 = new ViewModel3();
			vm3.bookingSystem = bookingsystem;
			vm3.services = bookingsystem.Services;
			return Ok(vm3);
		}

        [Route("api/GetSystem/")]
        [HttpGet]
        public IHttpActionResult GetSystem()
        {

            //BookingSystem bookingsystem = uw.BookingSystems.GetBookingSystem(id);
            ViewModel4 vm4 = new ViewModel4();
            //vm4.bookingSystem = bookingsystem;
            //vm4.services = bookingsystem.Services;
            return Ok(vm4);
        }


        //Hämtar och beräknar avståndet mellan befintliga bokningsystem
        [Route("api/GetBookingSystem/{bookingSystemAId:int}/{bookingSystemBId:int}")]
		[HttpGet]
		public double GetDistanceBetweenBookingSystems(int bookingSystemAId, int BookingSystemBId) {
			try {
				return uw.BookingSystems.GetDistanceTo(uw.BookingSystems.Get(bookingSystemAId), uw.BookingSystems.Get(BookingSystemBId));
			} catch (Exception) {
				throw;
			}
		}

	
		//Skapar ett nytt bokningssystem i datakällan - denna metod behöver utvecklas vidare så att den anropas via repository
		[Route("api/PostBookingSystem")] 
		public IHttpActionResult PostBookingSystem(JObject insystem) {
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
