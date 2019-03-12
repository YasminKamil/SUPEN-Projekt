using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class Service {
		public string serviceName { get; set; }
		public int duration { get; set; }
        public double price { get; set; }
        public Branch branch { get; set; }
    }
}