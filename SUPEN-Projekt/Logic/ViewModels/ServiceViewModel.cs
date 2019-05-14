using System;

namespace SUPEN_Projekt.Logic.ViewModels {
	//En vymodell för en service och dess egenskaper
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