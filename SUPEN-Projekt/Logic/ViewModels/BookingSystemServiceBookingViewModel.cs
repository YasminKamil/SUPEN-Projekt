using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	/*En vymodell som håller en lista av bokningssystem och en lista av bokningar som tar ut en service*/
	public class BookingSystemServiceBookingViewModel {

		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
		public DateTime startTime { get; set; }
		public Branch branch { get; set; }
		public int? branchAId { get; set; }
		public IEnumerable<Booking> bookings { get; set; }
		public IEnumerable<BookingSystem> bookingSystems { get; set; }

	}
}