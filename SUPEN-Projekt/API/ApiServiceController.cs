using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Controllers {
	public class ApiServiceController : ApiController {
		//IUnitOfWork följer depedency injection. Kommunicerar med repository interfaces
		//Se Repository UnitOfWork för implementation
		IUnitOfWork uw;

		public ApiServiceController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Returnerar alla lagrade tjänster som finns i databasen
		[Route("api/GetServices")]
		[HttpGet]
		public async Task<IHttpActionResult> GetServices() {
			var services = await uw.Services.GetAll();
			ServicesViewModel list = new ServicesViewModel();

			list.services = services.ToList();

			if (list == null) {
				return NotFound();
			}
			return Ok(list);
		}

		//Returnerar en specifik tjänst som finns lagrad i databasen
		[Route("api/GetService/{inBookingSystemId}/{inServiceId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetService(int inBookingSystemId, int inServiceId) {
			//Anropar på GetBookingSystem() från repository för att hämta det specifika bokningssystemet
			var bs = await uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();

			bsSBVM.bookingSystem = bs;
			//Tilldelar service-objektet ett värde på en tjänst som tillhör ett specifikt bokningssystem
			bsSBVM.service = bsSBVM.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);

			if (bsSBVM == null) {
				return NotFound();
			}

			return Ok(bsSBVM);
		}

		//Returnerar förslag på tjänster som användaren har ofta bokat som finns lagrad i databasen
		[Route("api/GetService/{inBookingId}/{inServiceName}/{inBookingSystemId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetServiceSuggestion(int inBookingId, string inServiceName, int inBookingSystemId) {
			ServiceViewModel sVM = new ServiceViewModel();

			var inBooking = await uw.Bookings.Get(inBookingId);
			var bookingSystems = await uw.BookingSystems.GetAll();

			//Tilldelar respektive variabel ett värde genom att anropa på metoder från repository
			var bookingSystem = await uw.BookingSystems.GetBookServiceSuggestion(inBooking, inServiceName, inBookingSystemId);
			var service = await uw.Services.GetServiceSuggestion(bookingSystem);
			var booking = await uw.BookingSystems.GetServiceSuggestionBookings(bookingSystems.ToList(), inBooking);

			sVM.bookingSystemName = bookingSystem.CompanyName;
			sVM.serviceName = service.ServiceName;
			sVM.startTime = booking.StartTime;
			sVM.endTime = booking.EndTime;
			sVM.bookingSystemId = bookingSystem.BookingSystemId;
			sVM.serviceId = service.ServiceId;
			sVM.branchAId = service.Branch.BranchId;
			sVM.bookingId = booking.BookingId;
			sVM.PictureUrl = service.Branch.PictureUrl;

			if (sVM == null) {
				return NotFound();
			}

			return Ok(sVM);
		}
	}
}
