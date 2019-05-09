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

		//Retunerar tjänster
		public IEnumerable<Service> GetServices() {
			return ApplicationDbContext.Set<Service>().Include(x => x.Branch).Include(b => b.Bookings).Include(x => x.Bookings);
		}


        //Returnerar den specifika tjänsten
        public Service GetService(int id) {
			IEnumerable<Service> services = GetServices();
			Service service = services.Single(x => x.ServiceId == id);
            service.Bookings = ApplicationDbContext.Services.Single(x=>x.ServiceId == id).Bookings;
			return service;
		}

		//Skapar en ny tjänst för bokning
		public void AddBooking(Booking booking, int id) {

			IEnumerable<Service> services = GetServices();
			Service service = services.Single(x => x.ServiceId == id);
			service.Bookings.Add(booking);

		}

        public Service GetServiceSuggestion(BookingSystem bookingSystem) {

            List<int> mostBookings = new List<int>();
            List<Booking> selectedBookings = new List<Booking>();
            Service selectedService = new Service();

            foreach (var services in bookingSystem.Services) {

                selectedBookings = services.Bookings.Where(x => x.Available == false).ToList();
                var numberOfTimes = selectedBookings.Count();//antal bokningar

                mostBookings.Add(numberOfTimes);
            }

            
            
                 selectedService = bookingSystem.Services.Where(x => x.Bookings.Count() == mostBookings.Max()).Single();                
            

            
            return selectedService;
        }

		public ApplicationDbContext ApplicationDbContext {
			get { return Context as ApplicationDbContext; }
		}
	}
}