using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers
{
    public class ApiBookingController : ApiController
    {

        //IUnitOfWork följer indpendancy injection. Kommunicerar med repository interfaces
        //Se Repository UnitOfWork för implementation
        IUnitOfWork uw;

        public ApiBookingController(IUnitOfWork unitOfWork)
        {
            uw = unitOfWork;
        }

        //Hämtar alla lagrade bokningar
        [Route("api/GetBookings")]
        [HttpGet]
        public IHttpActionResult GetBookings()
        {
            var bookings = uw.Bookings.GetAll();
            BookingsViewModel list = new BookingsViewModel();
            list.bookings = bookings;
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [Route("api/GetBooking/{inBookingId}")]
        [HttpGet]
        public IHttpActionResult GetBooking(int inBookingId)
        {
            BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
            bsSBVM.booking = uw.Bookings.Get(inBookingId);
            bsSBVM.bookings = uw.Bookings.GetAll();

            if (bsSBVM == null)
            {
                return NotFound();
            }

            return Ok(bsSBVM);
        }

        [Route("api/GetBooking/{inBookingSystemId}/{inServiceId}")]
        [HttpGet]
        public IHttpActionResult GetBooking(int inBookingSystemId, int inServiceId)
        {
            var bs = uw.BookingSystems.GetBookingSystem(inBookingSystemId);
            BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
            bsSBVM.bookingSystem = bs;
            bsSBVM.service = bs.Services.Single(x => x.ServiceId == inServiceId);
            //bsSBVM.booking = bsSBVM.service.Bookings.Single(x => x.BookingId == inBookingId);
            //bsSBVM.booking = uw.Bookings.GetBookings().Single(x=>x.BookingId == inBookingId);

            if (bsSBVM == null)
            {
                return NotFound();
            }
            return Ok(bsSBVM);
        }

        [Route("api/GetBooking/GetMaxId")]
        [HttpGet]
        public IHttpActionResult GetMaxId()
        {
            BookingSystemServiceBookingViewModel model = new BookingSystemServiceBookingViewModel();
            var maxId = uw.Bookings.GetBookings();
            model.booking = maxId.OrderByDescending(i => i.BookingId).Take(1).Single();          
            return Ok(model);
        }

        [Route("api/PostBooking")]
        [HttpPost]
        public IHttpActionResult PostBooking(BookingSystemServiceBookingViewModel inBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var booking = inBooking.booking;
            booking.StartTime = inBooking.startTime;
            booking.EndTime = inBooking.startTime.AddMinutes(inBooking.service.Duration);

            uw.Services.AddBooking(uw.Bookings.CreateBooking(booking), inBooking.service.ServiceId);
            uw.Complete();

            return Ok();
        }

    }
}






