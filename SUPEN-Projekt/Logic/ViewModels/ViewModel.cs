using System.Collections.Generic;
using SUPEN_Projekt.Models;

namespace SUPEN_Projekt.Logic {
	public class BookingSystemsViewModel {
		public int Id { get; set; }
		public IEnumerable<BookingSystem> bookingSystems { get; set; }
	}

	//vm3
	public class BookingSystemServicesViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public IEnumerable<Service> services { get; set; }
	}

	//vm4
	public class BookingSystemServiceBookingViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
	}

	//vm5
	public class BookingsWithDistanceViewModel {
		public IEnumerable<BookingSystemAndDistanceViewModel> BookingsWithDistance { get; set; }
		public BookingSystem SelectedBookingSystem { get; set; }
	}

	public class BookingSystemAndDistanceViewModel {
		public BookingSystem BookingSystem { get; set; }
		public double Distance { get; set; }
	}

	public class BookingsViewModel {
		public int Id { get; set; }
		public IEnumerable<Booking> bookings { get; set; }
	}

	public class ServicesViewModel {
		public int Id { get; set; }
		public IEnumerable<Service> services { get; set; }
	}

    public class BookingSystemQuery
    {
        public int firstId { get; set; }
        public int secondId { get; set; }
        public int thirdId { get; set; }
    }
}