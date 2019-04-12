using System;
using System.Web.Http;
using SUPEN_Projekt.Logic;
using SUPEN_Projekt.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SUPEN_Projekt.Controllers
{
    public class ApiBookingSystemController : ApiController
    {
        IUnitOfWork uw;

        public ApiBookingSystemController(IUnitOfWork unitOfWork)
        {
            uw = unitOfWork;
        }

        //Hämtar alla lagrade bokningsystem
        [Route("api/GetSystems")]
        [HttpGet]
        public IHttpActionResult GetSystems()
        {

            var bookingsystems = uw.BookingSystems.GetAll();
            BookingSystemsViewModel list = new BookingSystemsViewModel();
            list.bookingSystems = bookingsystems;
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }


        //Hämtar ut det valda bokningsystemet
        [Route("api/GetBookingSystem/{bookingSystemId:int}")]
        [HttpGet]
        public IHttpActionResult GetBookingSystem(int bookingSystemId)
        {
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
        public IHttpActionResult GetDistanceBetweenBookingSystems(int bookingSystemAId, int BookingSystemBId)
        {
            try
            {
                double gdbb = uw.BookingSystems
                    .GetDistanceTo(uw.BookingSystems.Get(bookingSystemAId), uw.BookingSystems.Get(BookingSystemBId));
                return Ok(gdbb);

            }
            catch (Exception)
            {
                throw;
            }
        }

        //Hämtar det relevanta bokningsystemet inom ett visst område och inom andra branscher ordnat efter avståndet
        //bytt till var
        [Route("api/GetRelevantBookingSystem/{bookingSystemId:int}/{serviceId:int}/{bookingId:int}")]
        [HttpGet]
        public IHttpActionResult GetRelevantBookingSystem(int bookingSystemId, int serviceId, int bookingId)
        {
            try
            {
                var onlyWithAvailableTimes = uw.BookingSystems
                    .GetRelevantBookingSystemOnlyWithAvailableTimes(bookingSystemId, serviceId, bookingId);
                BookingSystemsViewModel bookingsystemsvm = new BookingSystemsViewModel();
                bookingsystemsvm.bookingSystems = onlyWithAvailableTimes;
                return Ok(bookingsystemsvm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Skapar ett nytt bokningssystem i datakällan
        [Route("api/PostBookingSystem")]
        public IHttpActionResult PostBookingSystem(JObject insystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            BookingSystemServiceBookingViewModel bsSBVM = JsonConvert
                .DeserializeObject<BookingSystemServiceBookingViewModel>(insystem.ToString());
            uw.BookingSystems.AddBookingSystem(bsSBVM.bookingSystem);
            uw.Complete();
            return Ok();
        }
    }
}
