using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {

	public class Booking {
		[Key]
		public int BookingId { get; set; }
		public string UserName { get; set; }
		public string UserMail { get; set; }
		public string UserMobile { get; set; }
		public bool Available { get; set; }

		//public string Subject { get; set; }

		[Required, DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}")]
		public DateTime StartTime { get; set; } = DateTime.Now;
		[Required, DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}")]
		public DateTime EndTime { get; set; } = DateTime.Now;
		[Required, DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime Date { get; set; } = DateTime.Now;
		//public int Price { get; set; }


		//[ForeignKey("Service")]
		//public virtual int ServiceId { get; set; }
		//public virtual Service Service { get; set; }
		//[ForeignKey("BookingSystem")]
		//public virtual int BookingSystemId { get; set; }
		//public virtual BookingSystem BookingSystem { get; set; }



	}
}