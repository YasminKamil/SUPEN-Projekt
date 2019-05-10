using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Repositories {
	//Interface för BookingRepository för att kunna återanvända metoderna i presentationslagret/API:er
	public interface IBookingSystemRepository : IRepository<BookingSystem> {
		double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB);
		IEnumerable<BookingSystem> GetBookingSystems();
		BookingSystem GetBookingSystem(int id);
		List<BookingSystem> GetRelevantBookingSystemOnlyWithAvailableTimes(int bookingSystemId, int serviceId, int bookingId);
		BookingSystem GetBookServiceSuggestion(Booking inbooking, string inServiceName, int inBookingSystemId);
        Booking GetServiceSuggestionBookings(List<BookingSystem> inBookingSystem, Booking inBooking);

    }
}
