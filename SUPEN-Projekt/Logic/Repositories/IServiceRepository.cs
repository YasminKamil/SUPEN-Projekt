﻿using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	public interface IServiceRepository : IRepository<Service> {
		IEnumerable<Service> GetServices();
		void AddBooking(Booking booking, int id);
		Service GetService(int id);
	}
}