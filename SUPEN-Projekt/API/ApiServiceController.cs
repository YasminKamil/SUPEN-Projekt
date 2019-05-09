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
		public IHttpActionResult GetService(int inBookingSystemId, int inServiceId) {

			var bs = uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();

			bsSBVM.bookingSystem = bs;
			bsSBVM.service = bsSBVM.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);
           
			if (bsSBVM == null) {
				return NotFound();
			}

			return Ok(bsSBVM);
		}

        [Route("api/GetService/{inBookingId}/{inServiceName}/{inBookingSystemName}")]
        [HttpGet]
        public IHttpActionResult GetServiceSuggestion(int inBookingId, string inServiceName, string inBookingSystemName)
        {

            ServiceViewModel serviceViewModel = new ServiceViewModel();
            var inBooking = uw.Bookings.Get(inBookingId);
            var service = uw.BookingSystems.GetBookServiceSuggestion(inBooking, inServiceName);
            serviceViewModel.serviceName = service.ServiceName;
            //serviceViewModel.booking = service.Bookings.Where(x => x.Available && x.StartTime > inBooking.EndTime).Single();
            if (serviceViewModel == null)
            {
                return NotFound();
            }

            return Ok(serviceViewModel);

        }

    }
}
