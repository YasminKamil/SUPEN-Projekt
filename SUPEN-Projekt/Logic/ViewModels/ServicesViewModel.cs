using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;


namespace SUPEN_Projekt.Logic.ViewModels {
	//En vymodell som håller id och lista av services
	public class ServicesViewModel {
		public int Id { get; set; }
		public IEnumerable<Service> services { get; set; }
	}
}