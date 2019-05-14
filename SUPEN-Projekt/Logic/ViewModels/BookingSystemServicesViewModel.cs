using SUPEN_Projekt.Models;
using System.Collections.Generic;


namespace SUPEN_Projekt.Logic.ViewModels {
	//En vymodell med ett bokningssystem och en lista av services
	public class BookingSystemServicesViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public IEnumerable<Service> services { get; set; }
	}
}