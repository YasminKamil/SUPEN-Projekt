using System;
using System.Web.Http;
using SUPEN_Projekt.Logic;
using SUPEN_Projekt.Repositories;
using SUPEN_Projekt.Logic.ViewModels;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Controllers {
	public class ApiBookingSystemController : ApiController {
		//IUnitOfWork följer depedency injection. Kommunicerar med repository interfaces
		//Se Repository UnitOfWork för implementation
		IUnitOfWork uw;

		public ApiBookingSystemController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Hämtar alla lagrade bokningsystem som finns i databasen
		[Route("api/GetBookingSystems")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBookingSystems() {
			BookingSystemsViewModel list = new BookingSystemsViewModel();
			//Anropar GetAll() metoden från repository för att hämta alla bokningssytem
			list.bookingSystems = await uw.BookingSystems.GetAll();
			if (list == null) {
				return NotFound();
			}
			return Ok(list);
		}


		//Hämtar ut det valda bokningsystemet
		[Route("api/GetBookingSystem/{bookingSystemId:int}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBookingSystem(int bookingSystemId) {
			var bs = await uw.BookingSystems.GetBookingSystem(bookingSystemId);
			BookingSystemServicesViewModel bssvm = new BookingSystemServicesViewModel();
			bssvm.bookingSystem = bs;
			bssvm.services = bs.Services;
			if (bssvm == null) {
				return NotFound();
			}
			return Ok(bssvm);
		}

		//Returnerar avståndet mellan bokningssystem A och bokningssystem B
		[Route("api/GetBookingSystem/{bookingSystemAId:int}/{bookingSystemBId:int}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetDistanceBetweenBookingSystems(int bookingSystemAId, int BookingSystemBId) {
			try {
				//Anropar GetDistanceTo() metoden från repository som tar in parametervärden på bokningssystem A och bokningssystem B
				double gDBBS = await uw.BookingSystems.GetDistanceTo(await uw.BookingSystems.Get(bookingSystemAId), await uw.BookingSystems.Get(BookingSystemBId));
				return Ok(gDBBS);

			} catch (Exception) {
				throw;
			}
		}

		//Hämtar relevant bokningssystem som finns lagrad i databasen
		[Route("api/GetRelevantBookingSystem/{bookingSystemId:int}/{serviceId:int}/{bookingId:int}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetRelevantBookingSystem(int bookingSystemId, int serviceId, int bookingId) {
			try {
				/*Tilldelar variabeln ett värde på bokningssystem med relevanta bokningssystem med endast tillgängliga tider
				 genom att anropa på metoden GetRelevantBookingSystemOnlyWithAvailableTimes() från repository*/
				var onlyWithAvailableTimes = await uw.BookingSystems.GetRelevantBookingSystemOnlyWithAvailableTimes(bookingSystemId, serviceId, bookingId);
				BookingSystemsViewModel bsVM = new BookingSystemsViewModel();
				bsVM.bookingSystems = onlyWithAvailableTimes;
				return Ok(bsVM);
			} catch (Exception) {

				throw;
			}
		}
	}
}
