using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {

	//Tänk på properties i Viewmodellerna 
	//Används denna?
	public class ViewModel {
		public IEnumerable<Booking> Bookings { get; set; }
		public DateTime Date { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public IEnumerable<Service> Services { get; set; }
		public int Duration { get; set; }
	}

	//BookingSystemServiceViewModel överflödig 
	//Denna viewmodell kan tas bort och endast använda sig av vm4
	public class ViewModel2 {
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
	}

	//BookingSystemServicesViewModel
	public class ViewModel3 {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public IEnumerable<Service> services { get; set; }
	}

	//BookingSystemServiceBookingViewModel
	public class ViewModel4 {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
	}

	//BookingsWithDistanceViewModel 
	public class ViewModel5 {
		public IEnumerable<BookingSystemAndDistance> BookingsWithDistance { get; set; }
		public BookingSystem SelectedBookingSystem { get; set; }
	}

	public class BookingSystemAndDistance {
		public BookingSystem BookingSystem { get; set; }
		public double Distance { get; set; }
	}

    public class BookingsViewModel {
        public int Id { get; set; }
        public IEnumerable<Booking> bookings { get; set; }
    }

}