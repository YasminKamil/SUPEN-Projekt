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

        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
		public DateTime Date { get; set; }
        public int Price { get; set; }

        [ForeignKey("Service")]
        public virtual int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        [ForeignKey("BookingSystem")]
        public virtual int BookingSystemId { get; set; }
        public virtual BookingSystem BookingSystem { get; set; }

    }
}