using System.Collections.Generic;

namespace SUPEN_Projekt.Models {
	/*En modell för services med sina egenskaper, som representerar en tabell i databasen. 
	 Med ett till många samband till bokningar*/
	public class Service {

		public virtual int ServiceId { get; set; }
		public virtual string ServiceName { get; set; }
		public virtual int Duration { get; set; }
		public virtual double Price { get; set; }

		public virtual Branch Branch { get; set; }
		public virtual ICollection<Booking> Bookings { get; set; }
	}
}