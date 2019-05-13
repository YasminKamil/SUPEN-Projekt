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
        public int serviceId { get; set; }
        public string serviceName { get; set; }
        public int bookingSystemId { get; set; }
        public string bookingSystemName { get; set; }
        public int bookingId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int branchAId { get; set; }
        public virtual string PictureUrl { get; set; }
    }
}