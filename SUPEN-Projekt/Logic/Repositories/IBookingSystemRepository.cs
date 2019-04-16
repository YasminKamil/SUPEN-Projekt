using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	public interface IBookingSystemRepository : IRepository<BookingSystem> {

		double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB);
		IEnumerable<BookingSystem> GetBookingSystems();
		void AddBookingSystem(BookingSystem bookingsystem);
		void EditBookingSystem(BookingSystem bookingSystem);
		void AddService(Service service, int id);
		BookingSystem GetBookingSystem(int id);
		List<BookingSystem> GetRelevantBookingSystemOnlyWithAvailableTimes(int bookingSystemId, int serviceId, int bookingId);

		//List<BookingSystem> OrderByDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem);	
		//List<string> GetBranchesInBookingSystem(BookingSystem bookingSystem);
		//List<BookingSystem> GetBookingSystemsInOtherBranches(List<BookingSystem> inBookingSystems, Service selectedService);
		//string GetBrachesCount(List<BookingSystem> inBookingSystems);
		//bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance);
		//List<BookingSystem> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem);
		//void RemoveBookingSystem(int id);       
		//List<BookingSystem> GetBookingSystemsWithAvailableBooking(List<BookingSystem> inBookingSystems, Booking inSelectedBooking);
		//public Service GetBookingSystemService(int id, int ServiceId)
	}
}
