using SUPEN_Projekt.Models;
using System.Collections.Generic;


namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingSystemServicesViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public IEnumerable<Service> services { get; set; }
	}
}