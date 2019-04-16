using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Repositories {
	public interface IServiceRepository : IRepository<Service> {
		IEnumerable<Service> GetServices();
		void AddBooking(Booking booking, int id);
		Service GetService(int id);
	}
}