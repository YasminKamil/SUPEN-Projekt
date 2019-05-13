using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	//Ett repository för metoder som hanterar bokningar.
	public class BookingRepository : Repository<Booking>, IBookingRepository {

		//Konstruktor med ApplicationDbContext som parametervärde
		public BookingRepository(ApplicationDbContext context) : base(context) {
		}

		//Gör ett anrop till ApplicationDbContext och returnerar contexten
		public ApplicationDbContext ApplicationDbContext {
			get { return Context as ApplicationDbContext; }
		}

		//Returnerar alla bokningar som är lagrade i databasen
		public async Task<IEnumerable<Booking>> GetBookings() {
			return await GetAll();
		}

		//Skapar ett nytt bokningsobjekt
		public async Task<Booking> CreateBooking(Booking inBooking) {
			//Instansierar ett bokningsobjekt
			Booking booking = new Booking();

			//Tilldelar varje property i bokningen ett värde
			booking.BookingId = ApplicationDbContext.Bookings.Count();
			booking.UserName = inBooking.UserName;
			booking.UserMail = inBooking.UserMail;
			booking.UserMobile = inBooking.UserMobile;
			booking.Available = false;
			booking.StartTime = inBooking.StartTime;
			booking.EndTime = inBooking.EndTime;
			booking.Date = inBooking.Date;


			//Sparar bokningen i databasen
			Add(booking);
			return await Task.FromResult(booking);
		}

		public async Task<Booking> GetServiceSuggestionBookings(Service inService, Booking inBooking) {
			List<Booking> availableBookings = new List<Booking>();
			Booking serviceSuggestionBooking = new Booking();

			if (inService.Bookings.Count > 0) {
				foreach (var booking in inService.Bookings) {
					if (booking.Available == true)
					//booking.StartTime > inBooking.EndTime.AddMinutes(20) && 
					//booking.EndTime.AddMinutes(20) < inBooking.StartTime)

					{
						availableBookings.Add(booking);
					}

				}

				serviceSuggestionBooking = availableBookings.First();
				//serviceSuggestion = bookingSystem.Services.Where(x => x.Bookings.Count == mostBookings.Max()).First();
			}

			return await Task.FromResult(serviceSuggestionBooking);
		}
	}
}