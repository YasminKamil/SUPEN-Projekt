using SUPEN_Projekt.Models;

namespace SUPEN_Projekt.Logic.ViewModels {
	/*En vymodell som hanterar ett bokningssystem och distansen till systemet*/
	public class BookingSystemAndDistanceViewModel {
		public BookingSystem BookingSystem { get; set; }
		public double Distance { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string CompanyName { get; set; }
		public int Id { get; set; }
	}
}