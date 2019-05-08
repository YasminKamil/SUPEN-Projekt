using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;


namespace SUPEN_Projekt.Logic.ViewModels {
	public class ServicesViewModel {
		public int Id { get; set; }
		public IEnumerable<Service> services { get; set; }
	}

    public class ServiceViewModel {
        public int Id { get; set; }
        public Booking booking { get; set; }
        public string serviceName { get; set; }
        public string bookingSystemName { get; set; }
    }
}