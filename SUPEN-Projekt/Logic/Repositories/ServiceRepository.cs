using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;


namespace SUPEN_Projekt.Repositories {
	public class ServiceRepository : Repository<Service>, IServiceRepository {
		public ServiceRepository(ApplicationDbContext context) : base(context) { }

		public ApplicationDbContext ApplicationDbContext {
			get { return Context as ApplicationDbContext; }
		}

		//Retunerar tjänster
		public async Task<IEnumerable<Service>> GetServices() {
			return await ApplicationDbContext.Set<Service>().Include(x => x.Branch).Include(b => b.Bookings).Include(x => x.Bookings).ToListAsync();
		}

		//Returnerar den specifika tjänsten
		public async Task <Service> GetService(int id) {
			IEnumerable<Service> services =  await GetServices();
			Service service = services.Single(x => x.ServiceId == id);
			service.Bookings = ApplicationDbContext.Services.Single(x => x.ServiceId == id).Bookings;
			return await Task.FromResult(service);
		}

		//Skapar en ny tjänst för bokning
		public void AddBooking(Booking booking, int id) {

			IEnumerable<Service> services = ApplicationDbContext.Set<Service>().Include(x => x.Branch).Include(b => b.Bookings).Include(x => x.Bookings);
			Service service = services.Single(x => x.ServiceId == id);
			service.Bookings.Add(booking);	 
		}

		public async Task<Service> GetServiceSuggestion(BookingSystem bookingSystem) {
			List<int> mostBookings = new List<int>();
			Service serviceSuggestion = new Service();

			if (bookingSystem.Services.Count > 0) {
				foreach (var service in bookingSystem.Services) {
					if (service.Bookings.Count > 0) {
						var numberOfTimes = service.Bookings.Count();//antal bokningar
						mostBookings.Add(numberOfTimes);
					}
				}

				serviceSuggestion = bookingSystem.Services.
					Where(x => x.Bookings.Count == mostBookings.Max()).
					First();
			}
			return await Task.FromResult(serviceSuggestion);
		}

	}
}