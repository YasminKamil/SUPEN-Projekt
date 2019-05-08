using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Linq;

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
		public IEnumerable<Booking> GetBookings() {
			return GetAll(); ;
		}

		//Skapar ett nytt bokningsobjekt
		public Booking CreateBooking(Booking inBooking) {
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
			return booking;
		}
	}
}