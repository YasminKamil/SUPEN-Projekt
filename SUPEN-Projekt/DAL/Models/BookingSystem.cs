﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class BookingSystem {

		public BookingSystem() {
			Services = new HashSet<Service>();
			//Bookings = new HashSet<Booking>();
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
		//public bool c { get; set; }

		public virtual ICollection<Service> Services { get; set; }
		//public virtual ICollection<Booking> Bookings { get; set; }

	}
}