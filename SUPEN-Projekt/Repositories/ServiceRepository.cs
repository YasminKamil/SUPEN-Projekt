using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace SUPEN_Projekt.Repositories {
	public class ServiceRepository : Repository<Service>, IServiceRepository {
		public ServiceRepository(ApplicationDbContext context) : base(context) { }

		//GetServices
		public IEnumerable<Service> GetAllServices() {
			return ApplicationDbContext.Set<Service>().Include(x => x.Branch).Include(b => b.Bookings);
		}

		//GetService
		public Service GetTheService(int id) {
			IEnumerable<Service> services = GetAllServices();
			Service service = services.Single(x => x.ServiceId == id);
			return service;
		}

		public void AddBooking(Booking booking, int id) {

			IEnumerable<Service> services = GetAllServices();
			Service service = services.Single(x => x.ServiceId == id);
			service.Bookings.Add(booking);

		}

		public ApplicationDbContext ApplicationDbContext {
			get { return Context as ApplicationDbContext; }
		}
	}
}