using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class Service {

        
        public virtual int ServiceId { get; set; }
        public virtual string ServiceName { get; set; }
		public virtual int Duration { get; set; }
        public virtual double Price { get; set; }
        //public string BranchName { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<BookingSystem> BookingSystems { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
    public class stt
    {
    public string namn { get; set; }

    }
}