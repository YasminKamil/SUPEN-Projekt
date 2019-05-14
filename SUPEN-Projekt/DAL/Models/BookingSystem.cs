using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUPEN_Projekt.Models {

	/*En modell för bokningssystem med sina egenskaper, som representerar en tabell i databasen. 
	 Med ett till många samband till services*/
	public class BookingSystem {

		public BookingSystem() {
			Services = new HashSet<Service>();
		}

		[Key]
		public int BookingSystemId { get; set; }
		public string SystemName { get; set; }
		public string SystemDescription { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Website { get; set; }

		public string CompanyName { get; set; }
		public string ContactEmail { get; set; }
		public string ContactPhone { get; set; }

		public string Address { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }

		public virtual ICollection<Service> Services { get; set; }

	}
}