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
}