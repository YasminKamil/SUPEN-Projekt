using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Controllers {
	public class ApiServiceController : ApiController {
		IUnitOfWork uw;

		public ApiServiceController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Hämtar alla lagrade tjänster 
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

		//Hämtar den specifika tjänsten i bokningsystemet som är lagrad
		[Route("api/GetService/{inBookingSystemId}/{inServiceId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetService(int inBookingSystemId, int inServiceId) {

			var bs = await uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();

			bsSBVM.bookingSystem = bs;
			bsSBVM.service = bsSBVM.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);

			if (bsSBVM == null) {
				return NotFound();
			}

			return Ok(bsSBVM);
		}

		[Route("api/GetService/{inBookingId}/{inServiceName}/{inBookingSystemId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetServiceSuggestion(int inBookingId, string inServiceName, int inBookingSystemId) {
			ServiceViewModel serviceViewModel = new ServiceViewModel();

			var inBooking = await uw.Bookings.Get(inBookingId);
			var bookingSystems = await uw.BookingSystems.GetAll();

            var bookingSystem = await uw.BookingSystems.GetBookServiceSuggestion(inBooking, inServiceName, inBookingSystemId);
            var service = await uw.Services.GetServiceSuggestion(bookingSystem);
            var booking = await uw.BookingSystems.GetServiceSuggestionBookings(bookingSystems.ToList(), inBooking);
            
            serviceViewModel.bookingSystemName = bookingSystem.CompanyName;
            serviceViewModel.serviceName = service.ServiceName;
            serviceViewModel.startTime = booking.StartTime;
            serviceViewModel.endTime = booking.EndTime;
            serviceViewModel.bookingSystemId = bookingSystem.BookingSystemId;
            serviceViewModel.serviceId = service.ServiceId;
            serviceViewModel.branchAId = service.Branch.BranchId;
            serviceViewModel.bookingId = booking.BookingId;

			if (serviceViewModel == null) {
				return NotFound();
			}

			return Ok(serviceViewModel);
		}
	}
}
