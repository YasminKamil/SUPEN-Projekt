﻿using System;
using System.Web.Http;
using SUPEN_Projekt.Logic;
using SUPEN_Projekt.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

			var bookingsystems = uw.BookingSystems.GetAll();
			BookingSystemsViewModel list = new BookingSystemsViewModel();
			list.bookingSystems = bookingsystems;
			if (list == null) {
				return NotFound();
			}
			return Ok(list);
		}

		//Hämtar det relevanta bokningsystemet inom ett visst område och inom andra branscher ordnat efter avståndet
        //bytt till var
		[Route("api/GetRelevantBookingSystem/{bookingSystemId:int}/{serviceId:int}")]
		[HttpGet]
		public IHttpActionResult GetRelevantBookingSystem(int bookingSystemId, int serviceId) {
			try {
				var selectedBookingSystem = uw.BookingSystems.GetBookingSystem(bookingSystemId);
				var selectedService = uw.Services.Get(serviceId);

				var bookingSystemsInRange = uw.BookingSystems.GetBookingSystemsInRange(selectedBookingSystem);
				var bookingSystemsInOtherBranches = uw.BookingSystems.GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
				var orderedByDistance = uw.BookingSystems.OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
                var onlyWithAvailableTimes = uw.BookingSystems.GetBookingSystemsWithAvailableBooking(orderedByDistance);
                BookingSystemsViewModel bookingsystemsvm = new BookingSystemsViewModel();
                bookingsystemsvm.bookingSystems = onlyWithAvailableTimes;
                return Ok(bookingsystemsvm);
			} catch (Exception) {

				throw;
			}
		}

		//Hämtar ut det valda bokningsystemet
		[Route("api/GetBookingSystem/{bookingSystemId:int}")]
		[HttpGet]
		public IHttpActionResult GetBookingSystem(int bookingSystemId) {
			var bs = uw.BookingSystems.GetBookingSystem(bookingSystemId);
            BookingSystemServicesViewModel bssvm = new BookingSystemServicesViewModel();
            bssvm.bookingSystem = bs;
			return Ok(bssvm);

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

			var bookingsystem = uw.BookingSystems.GetBookingSystem(id);
			BookingSystemServicesViewModel bsSVM = new BookingSystemServicesViewModel();
			bsSVM.bookingSystem = bookingsystem;
			bsSVM.services = bookingsystem.Services;
			return Ok(bsSVM);
		}

        [Route("api/GetSystem/")]
        [HttpGet]
        public IHttpActionResult GetSystem()
        {

            //BookingSystem bookingsystem = uw.BookingSystems.GetBookingSystem(id);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
            //vm4.bookingSystem = bookingsystem;
            //vm4.services = bookingsystem.Services;
            return Ok(bsSBVM);
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

			BookingSystemServiceBookingViewModel bsSBVM = JsonConvert.DeserializeObject<BookingSystemServiceBookingViewModel>(insystem.ToString());
            uw.BookingSystems.AddBookingSystem(bsSBVM.bookingSystem);
			uw.Complete();
			return Ok();
		}
	}
}
