using SUPEN_Projekt.Models;


namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingSystemAndDistanceViewModel {
		public BookingSystem BookingSystem { get; set; }
		public double Distance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
	}
}