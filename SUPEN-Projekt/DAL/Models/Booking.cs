using System;
using System.ComponentModel.DataAnnotations;

namespace SUPEN_Projekt.Models {

	//En modell för bokningar med sina egenskaper, som representerar en tabell i databasen
	public class Booking {
		[Key]
		public int BookingId { get; set; }
		public string UserName { get; set; }
		public string UserMail { get; set; }
		public string UserMobile { get; set; }
		public bool Available { get; set; }

		[Required, DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}")]
		public DateTime StartTime { get; set; } = DateTime.Now;
		[Required, DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}")]
		public DateTime EndTime { get; set; } = DateTime.Now;
		[Required, DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime Date { get; set; } = DateTime.Now;
	}
}