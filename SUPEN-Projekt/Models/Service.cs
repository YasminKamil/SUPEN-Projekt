using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class Service {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
		public int Duration { get; set; }
        public double Price { get; set; }
        public Branch Branch { get; set; }
    }
}