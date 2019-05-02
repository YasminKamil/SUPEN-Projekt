using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	public class BookingSystemServiceBookingBranchBranchRelationViewModel
    {
		public int Id { get; set; }
		public BookingSystem bookingSystem { get; set; }
		public Service service { get; set; }
		public Booking booking { get; set; }
        public DateTime startTime { get; set; }
        public Branch branch { get; set; }
        public ICollection<BranchRelation> branchRelations { get; set; }
    }
}