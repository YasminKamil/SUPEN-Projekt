using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class Service {
        public virtual int ServiceId { get; set; }
        public virtual string serviceName { get; set; }
		public virtual int duration { get; set; }
        public virtual double price { get; set; }
        public virtual Branch branch { get; set; }
    }
    public class stt
    {
    public string namn { get; set; }

    }
}