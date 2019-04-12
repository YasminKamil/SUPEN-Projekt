using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic;

namespace SUPEN_Projekt.Controllers {
	public class ApiBookingController : ApiController {

		//IUnitOfWork följer indpendancy injection. Kommunicerar med repository interfaces
        //Se Repository UnitOfWork för implementation
		IUnitOfWork uw;

		public ApiBookingController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Hämtar alla lagrade bokningar
		[Route("api/GetBookings")]
		[HttpGet]
		public IHttpActionResult GetBookings() {
			var bookings = uw.Bookings.GetAll();
			BookingsViewModel list = new BookingsViewModel();
			list.bookings = bookings;
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
		}

		//Skapar en ny bokning i datakällan
		[Route("api/PostBooking")]
		public IHttpActionResult PostBooking(BookingSystemServiceBookingViewModel inBooking) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

            var booking = inBooking.booking;
			uw.Bookings.UpdateBooking(booking);
			uw.Complete();

			return Ok();
		}

		//Returnerar den specifika bokningen som har skapats
		[Route("api/GetBooking/{inBookingSystemId}/{inServiceId}/{inBookingId}")]
		[HttpGet]
		public IHttpActionResult GetBooking(int inBookingSystemId, int inServiceId, int inBookingId) {

			var bs = uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();

			bsSBVM.bookingSystem = bs;
			bsSBVM.service = bs.Services.Single(x => x.ServiceId == inServiceId);
			bsSBVM.booking = bsSBVM.service.Bookings.Single(x => x.BookingId == inBookingId);

			if (bsSBVM == null) {
				return NotFound();
			}

			return Ok(bsSBVM);
		}

	}
}