using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	public interface IBookingSystemRepository : IRepository<BookingSystem> {
		List<string> GetBranchesInBookingSystem(BookingSystem bookingSystem);
		List<BookingSystem> GetBookingSystemsInOtherBranches(List<BookingSystem> inBookingSystems, Service selectedService);
		double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB);
		List<BookingSystem> OrderByDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem);
		// string GetBrachesCount(List<BookingSystem> inBookingSystems);
		bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance);
		IEnumerable<BookingSystem> GetBookingSystems();//tas bort?
		List<BookingSystem> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem);
		//void AddBookingSystem(BookingSystem bookingsystem);
		void EditBookingSystem(BookingSystem bookingSystem);
		void RemoveBookingSystem(int id);
		//void AddBooking(Booking booking, int id);
		Service GetBookingSystemService(int id, int ServiceId);
		void AddService(Service service, int id);
		BookingSystem GetBookingSystem(int id);
		Service GetService(int BookingSystemId, int serviceId);
     
        List<BookingSystem> GetBookingSystemsWithAvailableBooking(List<BookingSystem> inBookingSystems, Booking inSelectedBooking);


    }
}
