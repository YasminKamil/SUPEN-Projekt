﻿using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	//Interface för BookingRepository för att kunna återanvända metoderna i presentationslagret/API:er
	public interface IBookingSystemRepository : IRepository<BookingSystem> {
		Task<double> GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB);
		Task<IEnumerable<BookingSystem>> GetBookingSystems();
		Task<BookingSystem> GetBookingSystem(int id);
		//BookingSystem GetBookingSystem(int id);
		Task<List<BookingSystem>> GetRelevantBookingSystemOnlyWithAvailableTimes(int bookingSystemId, int serviceId, int bookingId);
		//BookingSystem GetBookServiceSuggestion(Booking inbooking, string inServiceName, int inBookingSystemId);
		Task<BookingSystem> GetBookServiceSuggestion(Booking inBooking, string inServiceName, int inBookingSystemId);
		Task<Booking> GetServiceSuggestionBookings(List<BookingSystem> inBookingSystem, Booking inBooking);

    }
}
