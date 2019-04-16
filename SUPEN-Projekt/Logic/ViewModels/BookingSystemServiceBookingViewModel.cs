using SUPEN_Projekt.Models;


namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingSystemServiceBookingViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
	}
}