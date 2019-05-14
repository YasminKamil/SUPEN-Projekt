using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Controllers {
	public class ApiBookingController : ApiController {

		//IUnitOfWork följer depedency injection. Kommunicerar med repository interfaces
		//Se Repository UnitOfWork för implementation
		IUnitOfWork uw;

		public ApiBookingController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Returnerar alla lagrade bokningar som finns i databasen
		[Route("api/GetBookings")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBookings() {
			//Hämtar bokningar genom att anropa på GetAll() metoden som används i repository
			var bookings = await uw.Bookings.GetAll();
			BookingsViewModel list = new BookingsViewModel();
			list.bookings = bookings;
			if (list == null) {
				return NotFound();
			}
			return Ok(list);
		}

		//Returnerar en specifik bokning som finns lagrad i databasen
		[Route("api/GetBooking/{inBookingId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBooking(int inBookingId) {
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
			bsSBVM.booking = await uw.Bookings.Get(inBookingId);
			bsSBVM.bookingSystems = await uw.BookingSystems.GetAll();
			bsSBVM.bookings = await uw.Bookings.GetAll();

			if (bsSBVM == null) {
				return NotFound();
			}
			return Ok(bsSBVM);
		}

		//Returnerar en specifik bokning som tillhör en specifik tjänst som finns lagrad i databasen
		[Route("api/GetBooking/{inBookingSystemId}/{inServiceId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBooking(int inBookingSystemId, int inServiceId) {
			//Hämtar bokningssystemet genom att anropa GetBookingSystem() metoden från repository
			var bs = await uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
			bsSBVM.bookingSystem = bs;

			//Hämtar den specifika tjänsten som tillhör bokningssystemet
			bsSBVM.service = bs.Services.Single(x => x.ServiceId == inServiceId);

			if (bsSBVM == null) {
				return NotFound();
			}
			return Ok(bsSBVM);
		}

		//Hämtar det högsta id:et för bokningar som finns lagrad i databasen
		[Route("api/GetBooking/GetMaxId")]
		[HttpGet]
		public async Task<IHttpActionResult> GetMaxId() {
			BookingSystemServiceBookingViewModel model = new BookingSystemServiceBookingViewModel();
			var maxId = await uw.Bookings.GetBookings();
			model.booking = maxId.OrderByDescending(i => i.BookingId).Take(1).Single();
			return Ok(model);
		}

		//Skapar en ny post av en bokning som lagras i databasen
		[Route("api/PostBooking")]
		[HttpPost]
		public async Task<IHttpActionResult> PostBooking(BookingSystemServiceBookingViewModel inBooking) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

			var booking = inBooking.booking;
			booking.StartTime = inBooking.startTime;
			booking.EndTime = inBooking.startTime.AddMinutes(inBooking.service.Duration);

			/*Gör först ett anrop till AddBooking i repository som kräver en bokning och service id som parameter.
			 Tar in CreateBooking() metoden från repository som första parametervärde för bokningen och sedan service objektet
			 som andra parametervärde*/
			uw.Services.AddBooking(await uw.Bookings.CreateBooking(booking), inBooking.service.ServiceId);
			uw.Complete();

			return Ok();
		}
	}
}

