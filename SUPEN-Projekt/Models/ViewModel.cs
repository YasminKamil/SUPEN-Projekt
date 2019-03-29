using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {

	public class ViewModel {
		public IEnumerable<Booking> Bookings { get; set; }
		public DateTime Date { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public IEnumerable<Service> Services { get; set; }
		public int Duration { get; set; }
	}

    public class ViewModel2
    {      
        public BookingSystem bookingSystem { get; set; }
        public Service service { get; set; }
              
    }

    public class ViewModel3
    {
        public BookingSystem bookingSystem { get; set; }
        public IEnumerable<Service> services { get; set; }
    }

    public class ViewModel4
    {
        public BookingSystem bookingSystem { get; set; }
        public Service service { get; set; }
        public Booking booking { get; set; }
    }

    public class ViewModel5
    {
        public IEnumerable<BookingSystemAndDistance> BookingsWithDistance { get; set; }
        public BookingSystem SelectedBookingSystem { get; set; }
    }
    public class BookingSystemAndDistance
    {
        public BookingSystem BookingSystem { get; set; }
        public double Distance { get; set; }
    }

}