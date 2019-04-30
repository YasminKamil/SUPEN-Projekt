using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers {
	public class ApiServiceController : ApiController {
		IUnitOfWork uw;

		public ApiServiceController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Hämtar alla lagrade tjänster 
		[Route("api/GetServices")]
		[HttpGet]
		public IHttpActionResult GetServices() {
			var services = uw.Services.GetAll();
			ServicesViewModel list = new ServicesViewModel();

			list.services = services;

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

	}
}
