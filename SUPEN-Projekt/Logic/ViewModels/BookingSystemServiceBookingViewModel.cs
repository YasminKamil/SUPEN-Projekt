using SUPEN_Projekt.Models;
using System;

namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingSystemServiceBookingViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
        public DateTime startTime { get; set; }
        public Branch branch { get; set; }
        public int? branchAId { get; set; }
    }
}