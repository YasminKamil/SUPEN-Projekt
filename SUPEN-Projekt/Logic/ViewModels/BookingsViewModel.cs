using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingsViewModel {
		public int Id { get; set; }
		public IEnumerable<Booking> bookings { get; set; }
	}
}