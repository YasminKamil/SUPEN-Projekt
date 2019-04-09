using System;
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
		[Route("api/GetRelevantBookingSystem/{bookingSystemId:int}/{serviceId:int}/{bookingId:int}")]
		[HttpGet]
		public IHttpActionResult GetRelevantBookingSystem(int bookingSystemId, int serviceId, int bookingId) {
			try {
				BookingSystem selectedBookingSystem = uw.BookingSystems.GetBookingSystem(bookingSystemId);
				Service selectedService = uw.Services.Get(serviceId);
				List<BookingSystem> bookingSystemsInRange = uw.BookingSystems.GetBookingSystemsInRange(selectedBookingSystem);
				List<BookingSystem> bookingSystemsInOtherBranches = uw.BookingSystems.GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
				List<BookingSystem> orderedByDistance = uw.BookingSystems.OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
                List<BookingSystem> onlyWithAvailableTimes=uw.BookingSystems.GetBookingSystemsWithAvailableBooking(orderedByDistance,uw.Bookings.Get(bookingId));
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
            bssvm.services = bs.Services;
            if (bssvm == null)
            {
                return NotFound();
            }
            return Ok(bssvm);
		}

        [Route("api/GetBookingSystem/")]
        [HttpGet]
        public IHttpActionResult GetSystem()
        {
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
            return Ok(bsSBVM);
        }

        //Hämtar och beräknar avståndet mellan befintliga bokningsystem
        [Route("api/GetBookingSystem/{bookingSystemAId:int}/{bookingSystemBId:int}")]
		[HttpGet]
		public IHttpActionResult GetDistanceBetweenBookingSystems(int bookingSystemAId, int BookingSystemBId) {
			try {
                double gdbb = uw.BookingSystems.GetDistanceTo(uw.BookingSystems.Get(bookingSystemAId), uw.BookingSystems.Get(BookingSystemBId));
                return Ok(gdbb);

			} catch (Exception) {
				throw;
			}
		}

        //Hämtar det relevanta bokningsystemet inom ett visst område och inom andra branscher ordnat efter avståndet
        //bytt till var
        [Route("api/GetRelevantBookingSystem/{bookingSystemId:int}/{serviceId:int}")]
        [HttpGet]
        public IHttpActionResult GetRelevantBookingSystem(int bookingSystemId, int serviceId)
        {
            try
            {
                var selectedBookingSystem = uw.BookingSystems.GetBookingSystem(bookingSystemId);
                var selectedService = uw.Services.Get(serviceId);

                var bookingSystemsInRange = uw.BookingSystems.GetBookingSystemsInRange(selectedBookingSystem);
                var bookingSystemsInOtherBranches = uw.BookingSystems.GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
                var orderedByDistance = uw.BookingSystems.OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
                var onlyWithAvailableTimes = uw.BookingSystems.GetBookingSystemsWithAvailableBooking(orderedByDistance);
                BookingSystemsViewModel bookingsystemsvm = new BookingSystemsViewModel();
                bookingsystemsvm.bookingSystems = onlyWithAvailableTimes;
                return Ok(bookingsystemsvm);
            }
            catch (Exception)
            {

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
