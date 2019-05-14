using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	/*En vymodell som håller ett bokningssystem, en service, en bokning, en branch och
	 har en till många relation till branchrelations*/
	public class BookingSystemServiceBookingBranchBranchRelationViewModel {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
		public DateTime startTime { get; set; }
		public Branch branch { get; set; }
		public ICollection<BranchRelation> branchRelations { get; set; }
	}
}