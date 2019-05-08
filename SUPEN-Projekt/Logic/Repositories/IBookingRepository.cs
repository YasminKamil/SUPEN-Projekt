using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Repositories {
	//Interface för BookingRepository för att kunna återanvända metoderna i presentationslagret/API:er
	public interface IBookingRepository : IRepository<Booking> {
		Booking CreateBooking(Booking inBooking);
		IEnumerable<Booking> GetBookings();
	}
}
