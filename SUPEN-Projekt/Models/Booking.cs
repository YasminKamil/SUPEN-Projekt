using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class Booking {
		public int BookingId { get; set; }
		public string UserName { get; set; }
		public string UserMail { get; set; }
		public string UserMobile { get; set; }

		public string Subject { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int Price { get; set; }
        public int MyProperty { get; set; }
    
        public string Hej { get; set; }

		public string sträng { get; set; }

	}
}