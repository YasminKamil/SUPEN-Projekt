﻿using System.Collections.Generic;
using SUPEN_Projekt.Models;

namespace SUPEN_Projekt.Logic {
	public class BookingSystemsViewModel {
		public int Id { get; set; }
		public IEnumerable<BookingSystem> bookingSystems { get; set; }
	}
}