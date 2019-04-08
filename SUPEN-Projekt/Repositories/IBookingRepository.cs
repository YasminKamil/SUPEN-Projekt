using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	public interface IBookingRepository : IRepository<Booking> {
		//metoden GetBookings är redundant eftersom Irepository redan har en metod för get all.
		//fungerar som testmetod tills vidare.
		Booking CreateBooking(Booking inBooking);
		IEnumerable<Booking> GetBookings();
		void UpdateBooking(Booking booking);

	}
}
