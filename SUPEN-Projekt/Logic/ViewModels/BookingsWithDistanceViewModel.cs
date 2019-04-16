using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingsWithDistanceViewModel {
		public IEnumerable<BookingSystemAndDistanceViewModel> BookingsWithDistance { get; set; }
		public BookingSystem SelectedBookingSystem { get; set; }
	}
}