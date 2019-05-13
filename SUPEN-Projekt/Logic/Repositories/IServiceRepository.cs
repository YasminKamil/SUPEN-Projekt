using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	public interface IServiceRepository : IRepository<Service> {
		Task<IEnumerable<Service>> GetServices();
		void AddBooking(Booking booking, int id);
		Task<Service> GetService(int id);
        Task<Service> GetServiceSuggestion(BookingSystem bookingSystem);

    }
}