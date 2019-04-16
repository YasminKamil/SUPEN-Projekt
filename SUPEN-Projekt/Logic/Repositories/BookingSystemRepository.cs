using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Device.Location;

namespace SUPEN_Projekt.Repositories {
	public class BookingSystemRepository : Repository<BookingSystem>, IBookingSystemRepository {

		public BookingSystemRepository(ApplicationDbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        //Retunerar alla bokningsystem
        public IEnumerable<BookingSystem> GetBookingSystems() {

			return ApplicationDbContext.Set<BookingSystem>().Include(i => i.Services.Select(s => s.Branch))
				.Include(i => i.Services.Select(b => b.Bookings));
		}

		//Redigerar bokningsystemets information
		public void EditBookingSystem(BookingSystem bookingSystem) {
			ApplicationDbContext.Entry(bookingSystem).State = EntityState.Modified;
			ApplicationDbContext.SaveChanges();
		}

		//Returnerar det specifika bokningsystemet
		public BookingSystem GetBookingSystem(int id) {
			IEnumerable<BookingSystem> listbookingsystems = GetBookingSystems();
			BookingSystem bookingSystem = listbookingsystems.Single(x => x.BookingSystemId == id);

			return bookingSystem;
		}


		public void AddBookingSystem(BookingSystem bookingSystem) {

			BookingSystem bs = new BookingSystem();
			bs.BookingSystemId = bookingSystem.BookingSystemId;
			bs.Address = bookingSystem.Address;
			bs.City = bookingSystem.City;
			bs.CompanyName = bookingSystem.CompanyName;
			bs.ContactEmail = bookingSystem.ContactEmail;
			bs.ContactPhone = bookingSystem.ContactPhone;
			bs.Email = bookingSystem.Email;
			bs.Latitude = bookingSystem.Latitude;
			bs.Longitude = bookingSystem.Longitude;
			bs.PhoneNumber = bookingSystem.PhoneNumber;
			bs.PostalCode = bookingSystem.PostalCode;
			bs.SystemDescription = bookingSystem.SystemDescription;
			bs.SystemName = bookingSystem.SystemName;
			bs.Website = bookingSystem.Website;

			Add(bs);
		}

		//Tar bort bokningsystemet
		public void RemoveBookingSystem(int id) {
			BookingSystem bookingsystem = Get(id);
			Remove(bookingsystem);
		}

		//Skapar en ny tjänst för bokningsystemet
		public void AddService(Service service, int id) {
			Get(id).Services.Add(service);

			//bookingsystem.Services.Add()    
		}


		//Returnerar tjänsten för bokningsystemet
		public Service GetBookingSystemService(int id, int ServiceId) {
			BookingSystem bookingsystem = Get(id);
			Service service = bookingsystem.Services.Single(x => x.ServiceId == ServiceId);
			return service;
		}


		public List<BookingSystem> GetRelevantBookingSystemOnlyWithAvailableTimes(int bookingSystemId, int serviceId, int bookingId) {
			BookingSystem selectedBookingSystem = GetBookingSystem(bookingSystemId);

			Service selectedService = ApplicationDbContext.Services.Single(x => x.ServiceId == serviceId);
			Booking booking = ApplicationDbContext.Bookings.Single(x => x.BookingId == bookingId);

			List<BookingSystem> bookingSystemsInRange = GetBookingSystemsInRange(selectedBookingSystem);
			List<BookingSystem> bookingSystemsInOtherBranches = GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
			List<BookingSystem> orderedByDistance = OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
			List<BookingSystem> onlyWithAvailableTimes = GetBookingSystemsWithAvailableBooking(orderedByDistance, booking);

			return onlyWithAvailableTimes;
		}

		//Returnerar bokningsystem inom en viss distans inom vald stad
		private List<BookingSystem> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem) {
			var companiesInSelectedCity = ApplicationDbContext.BookingSystems.Where(x => x.City.ToLower() == inSelectedBookingSystem.City.ToLower() && x.CompanyName != inSelectedBookingSystem.CompanyName);
			List<BookingSystem> companiesInRange = new List<BookingSystem>();

			foreach (var item in companiesInSelectedCity) {
				if (InDistance(inSelectedBookingSystem.Longitude, inSelectedBookingSystem.Latitude, item.Longitude, item.Latitude, 5000)) {
					companiesInRange.Add(item);
					Console.WriteLine(item.CompanyName);
				}
			}
			return companiesInRange;
		}

		//Beräknar distansen till andra företag. Returnerar true/false beroende på om avståndet är ok.
		private bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance) {
			bool isCloseEnough = false;
			//Gör om koordinater till radian
			companyALat = companyALat / 180 * Math.PI;
			companyALong = companyALong / 180 * Math.PI;
			companyBLong = companyBLong / 180 * Math.PI;
			companyBLat = companyBLat / 180 * Math.PI;
			//beräknar distansen mellan de två olika företagen
			double distanceLatitude = (Math.Abs(companyALat - companyBLat)) /2;
			double distanceLongitude = (Math.Abs(companyALong - companyBLong)) /2;
			double x = Math.Sin(distanceLatitude) * Math.Sin(distanceLatitude) + Math.Cos(companyALat) * Math.Cos(companyBLat) * Math.Sin(distanceLongitude) * Math.Sin(distanceLatitude);
			double y = 2 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));
			//6371000 är jordens radie
			y = y * 6371000;
			//Om distansen är längre än maxDistance returneras false
			if (y <= maxDistance) isCloseEnough = true;
			return isCloseEnough;
		}

		//löser uppgift 2 i kravspecen
		//public string GetBrachesCount(List<BookingSystem> inBookingSystems)
		//{
		//    string branchesGrouped = "";
		//    List<Branch> t1Branches = new List<Branch>();
		//    foreach (var item in inBookingSystems)
		//    {
		//        List<Branch> t2Branches = new List<Branch>();
		//        foreach (var y in item.Services)
		//        {
		//            if (!t2Branches.Contains(y.Branch))
		//            {
		//                t2Branches.Add(y.Branch);
		//            }
		//        }
		//        t1Branches.AddRange(t2Branches);
		//    }

		//    foreach (var item in t1Branches.GroupBy(x => x.BranchName))
		//    {
		//        branchesGrouped += item.Key + " " + item.Count() + "\n";
		//    }
		//    return branchesGrouped;
		//}

		List<BookingSystemOfInterest> DistBooking = new List<BookingSystemOfInterest>();

		//Används för att koppla distans och bokningssystem, utan att behöva ändra i modellen då distansen är olika i varje sökning. 
		private class BookingSystemOfInterest {
			public BookingSystem bookingSystem;
			public double distance;
			public BookingSystemOfInterest(BookingSystem bookingSystem, double distance) {
				this.bookingSystem = bookingSystem;
				this.distance = distance;
			}
		}
		//returnerar endast företag som har lediga tider som börjar strax efter eller slutar en liten stund före bokad tjänst
		private List<BookingSystem> GetBookingSystemsWithAvailableBooking(List<BookingSystem> inBookingSystems, Booking inSelectedBooking) {
			inBookingSystems = inBookingSystems.Where(x => x.Services.Any(y => y.Bookings.Any(z => (inSelectedBooking.EndTime.AddMinutes(35) > z.StartTime && z.StartTime > inSelectedBooking.EndTime.AddMinutes(15)) || (z.EndTime > inSelectedBooking.StartTime.AddMinutes(-35) && z.EndTime < inSelectedBooking.StartTime.AddMinutes(-15))))).ToList();
			inBookingSystems = inBookingSystems.Where(x => x.Services.Any(y => y.Bookings.Any(f => f.Available == true))).ToList();
			return inBookingSystems;
		}
		//Genom att skicka in en lista av bokningsystem och det valda företaget, sorteras dem efter vilken distans de har till det valda företaget.
		private List<BookingSystem> OrderByDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem) {

			foreach (var item in inBookingSystems) {
				DistBooking.Add(new BookingSystemOfInterest(item, GetDistanceTo(inSelectedBookingSystem, item)));
			}
			inBookingSystems = new List<BookingSystem>();
			foreach (var item in DistBooking.OrderBy(x => x.distance)) {
				inBookingSystems.Add(item.bookingSystem);
				Console.WriteLine(item.distance + " " + item.bookingSystem.CompanyName);
			}
			return inBookingSystems;
		}

		//Returnerar distancen mellan 2 företag
		public double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB) {
			var aCoord = new GeoCoordinate(bookingSystemA.Latitude, bookingSystemA.Longitude);
			var bCoord = new GeoCoordinate(bookingSystemB.Latitude, bookingSystemB.Longitude);
			return aCoord.GetDistanceTo(bCoord);
		}

		//Returnerar bookingsystem som har services inom andra brancher, kan även returnera selectedBookingService
		private List<BookingSystem> GetBookingSystemsInOtherBranches(List<BookingSystem> inBookingSystems, Service selectedService) {
			List<BookingSystem> keep = new List<BookingSystem>();
			BookingSystem tmbBookingSystem = new BookingSystem();

			foreach (var aBookingSystem in inBookingSystems.Where(x => x.Services != null)) {
				List<Service> service = aBookingSystem.Services
					.Where(x => x.Branch.BranchName != selectedService.Branch.BranchName)
					.ToList<Service>();
				if (service.Count() != 0) {
					keep.Add(aBookingSystem);
				}
			}
			return keep;
		}

		//Returnerar brancher för bokningsystemet
		private List<string> GetBranchesInBookingSystem(BookingSystem bookingSystem) {
			List<string> branchesInBookingSystem = new List<string>();
			foreach (var item in bookingSystem.Services) {
				branchesInBookingSystem.Add(item.Branch.BranchName);

			}
			return branchesInBookingSystem;
		}
	}
}

