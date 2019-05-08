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
        public IEnumerable<BookingSystem> GetBookingSystems()
        {

            return ApplicationDbContext.Set<BookingSystem>().Include(i => i.Services.Select(s => s.Branch).Select(u => u.BranchRelations))
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
        class distanceScoreAndBookingSystem
        {
            public double distanceScore;
            public BookingSystemOfInterest bookingSystemOfInterest;
        }
        public class clickOfService
        {
            public double score;
            public Service service;
        }


        public List<BookingSystem> GetRelevantBookingSystemOnlyWithAvailableTimes(int bookingSystemId, int serviceId, int bookingId) {
			BookingSystem selectedBookingSystem = GetBookingSystem(bookingSystemId);

			Service selectedService = ApplicationDbContext.Services.Single(x => x.ServiceId == serviceId);
			Booking booking = ApplicationDbContext.Bookings.Single(x => x.BookingId == bookingId);

			List<BookingSystem> bookingSystemsInRange = GetBookingSystemsInRange(selectedBookingSystem);
			List<BookingSystem> bookingSystemsInOtherBranches = GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
			List<BookingSystem> orderedByDistance = OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
			List<BookingSystem> onlyWithAvailableTimes = GetBookingSystemsWithAvailableBooking(orderedByDistance, booking);
            List<BookingSystem> onlyOneOfEachBranch = GetBookingSystemsWithOnlyOneOfEachBranch(onlyWithAvailableTimes);



            int totalClicks = GetTotalClicksInBranchRelations(onlyOneOfEachBranch, selectedBookingSystem, selectedService);

            List<distanceScoreAndBookingSystem> distanceScoreAndBookingSystem = new List<distanceScoreAndBookingSystem>();
            distanceScoreAndBookingSystem = GetBookingSystemsWithDistanceScore(onlyOneOfEachBranch, selectedBookingSystem);

            List<clickOfService> ClickOfServices = new List<clickOfService>();
            ClickOfServices = GetClickOfServices(distanceScoreAndBookingSystem, selectedService, totalClicks);

            List<BookingSystem> orderedByPoints = GetBookingSystemsOrderedByPoints( ClickOfServices,  onlyOneOfEachBranch);

            return orderedByPoints;
		}

        private List<BookingSystem> GetBookingSystemsOrderedByPoints(List<clickOfService> ClickOfServices, List<BookingSystem> onlyOneOfEachBranch) {
            List<BookingSystem> orderedByPoints = new List<BookingSystem>();

            foreach (var item in ClickOfServices)
            {
                if (onlyOneOfEachBranch.Count() != 0 && !orderedByPoints.Contains(onlyOneOfEachBranch.Single(x => x.Services.Any(y => y.ServiceId == item.service.ServiceId))))
                {
                    if (!orderedByPoints.Contains(onlyOneOfEachBranch.Single(x => x.Services.Any(y => y.ServiceId == item.service.ServiceId))))
                    {
                    orderedByPoints.Add( onlyOneOfEachBranch.Single(x=> x.Services.Any(y=>y.ServiceId == item.service.ServiceId)));
                    } 
                }
            }
            return orderedByPoints;
        }
        private List<clickOfService> GetClickOfServices(List<distanceScoreAndBookingSystem> distanceScoreAndBookingSystem, Service selectedService, int totKlick)
        {
            BranchRepository branchRepository = new BranchRepository(ApplicationDbContext);

            List<clickOfService> clickOfService = new List<clickOfService>();
            foreach (var item in distanceScoreAndBookingSystem)
            {
                double räknare = item.distanceScore;

                foreach (var service in item.bookingSystemOfInterest.bookingSystem.Services)
                {
                    double score = 1;
                    if (selectedService.Branch.BranchRelations.Any(y => y.branchBId2 == service.Branch.BranchId.ToString()))
                    {
                    BranchRelation br = selectedService.Branch.BranchRelations.Single(x=>x.branchBId2 == service.Branch.BranchId.ToString());
                    int klick = br.CountClick;
                    score += (double)klick/totKlick;
                    }
                    
                    clickOfService aObject = new clickOfService();
                    aObject.service = service;
                    aObject.score = score * räknare;
                    clickOfService.Add(aObject);
                }

            }

            return clickOfService;
        }

        private List<distanceScoreAndBookingSystem> GetBookingSystemsWithDistanceScore(List<BookingSystem> onlyOneOfEachBranch, BookingSystem selectedBookingSystem){

            List<BookingSystemOfInterest> bookingSystemsOfInterest = getBookingsWithDistance(onlyOneOfEachBranch, selectedBookingSystem);
            int distanceScore = bookingSystemsOfInterest.Count();

            List<distanceScoreAndBookingSystem> distanceScoreAndBookingSystem = new List<distanceScoreAndBookingSystem>();
            foreach (var bookingSystemOfInterest in bookingSystemsOfInterest)
            {
                distanceScoreAndBookingSystem aObject = new distanceScoreAndBookingSystem();
                aObject.distanceScore = distanceScore;
                aObject.bookingSystemOfInterest = bookingSystemOfInterest;

                distanceScoreAndBookingSystem.Add(aObject);
                distanceScore--;
            }

            return distanceScoreAndBookingSystem;
        }



        private int GetTotalClicksInBranchRelations(List<BookingSystem> onlyOneOfEachBranch, BookingSystem selectedBookingSystem, Service selectedService) {
            List<BookingSystemOfInterest> bookingSystemsOfInterest = getBookingsWithDistance(onlyOneOfEachBranch, selectedBookingSystem);
            BranchRepository branchRepository = new BranchRepository(ApplicationDbContext);
            int totKlick = 0;


            List<Service> servicesWithRelationToSelectedService = new List<Service>();

            foreach (var bookingSystemOfInterest in bookingSystemsOfInterest)
            {
                foreach (var service in bookingSystemOfInterest.bookingSystem.Services)
                {
                    if (selectedService.Branch.BranchRelations.Any(x => x.branchBId2 == service.Branch.BranchId.ToString()))
                    {
                        servicesWithRelationToSelectedService.Add(service);
                    }
                }
            }

            foreach (var relation in selectedService.Branch.BranchRelations)
            {
                if (servicesWithRelationToSelectedService.Any(x=>x.Branch.BranchId.ToString() == relation.branchBId2))
                {
                    totKlick += relation.CountClick;
                }
            }

            return totKlick;
        }


        private List<BookingSystemOfInterest> getBookingsWithDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem)
        {
            List<BookingSystemOfInterest> DistBooking = new List<BookingSystemOfInterest>();

            foreach (var item in inBookingSystems)
            {
                DistBooking.Add(new BookingSystemOfInterest(item, GetDistanceTo(inSelectedBookingSystem, item)));
            }
            return DistBooking;
        }





        /*Returnerar en lista av bookingssystem, där vi endast tar med ett företag av varje branch. Detta för att ex inte visa 10 hotell, 
        utan endast det som är närmst*/
        private List<BookingSystem> GetBookingSystemsWithOnlyOneOfEachBranch(List<BookingSystem> inBookingSystems) {
            List<BookingSystem> onlyOneOfEachBranch = new List<BookingSystem>();
            List<Branch> branches = new List<Branch>();

            foreach (var item in inBookingSystems)
            {
                bool alreadyInList = false;
                foreach (var y in item.Services)
                {
                    if (branches.Where(x => x.BranchName == y.Branch.BranchName).Count() == 0)
                    {
                        branches.Add(y.Branch);
                    }
                    else
                    {
                        alreadyInList = true;
                    }
                }
                if (alreadyInList == false)
                {
                    onlyOneOfEachBranch.Add(item);
                }
            }
            return onlyOneOfEachBranch;
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

            int open = 8; 
            foreach (var bookingSystem in inBookingSystems)
            {
                foreach (var service in bookingSystem.Services)
                {
                    decimal inHours = Convert.ToDecimal(service.Duration) / Convert.ToDecimal(60);
                    int iterations = (int)Math.Floor(Convert.ToDecimal(open) / Convert.ToDecimal(inHours));
                   
                    //behöver bytas ut när vi ordnat så att en bokning har ett annat datum. 
                    DateTime startTime = DateTime.Today;
                    startTime = startTime.AddHours(8);

                    List<DateTime> dt = new List<DateTime>();

                    for (int i = 0; i < iterations; i++)
                    {
                        DateTime endTime = startTime;
                        endTime = endTime.AddMinutes(service.Duration);

					if (!service.Bookings.Any(x => x.StartTime == startTime)) {

                            Booking aBooking = new Booking();
                            aBooking.Available = true;
                            aBooking.Date = DateTime.Today;
                            aBooking.StartTime = startTime;
                            aBooking.EndTime = endTime;
                            service.Bookings.Add(aBooking);
                        }
                    startTime = endTime;
				}
                }
            }

            //ordnar så vi inte behöver iterera genom alla objekt. 
            inBookingSystems = inBookingSystems.Where(x => x.Services.Any(y => y.Bookings.Any(z => (inSelectedBooking.EndTime.AddMinutes(35) > z.StartTime && z.StartTime > inSelectedBooking.EndTime.AddMinutes(15)) || (z.EndTime > inSelectedBooking.StartTime.AddMinutes(-35) && z.EndTime < inSelectedBooking.StartTime.AddMinutes(-15))))).ToList();
			inBookingSystems = inBookingSystems.Where(x => x.Services.Any(y => y.Bookings.Any(f => f.Available == true))).ToList();

            List<BookingSystem> onlyBookingSystemsWithRelevantTimes = new List<BookingSystem>();


            foreach (var bookingSystemItem in inBookingSystems)
            { 
                List<Service> someServices = new List<Service>();
                foreach (var serviceItem in bookingSystemItem.Services)
                {
                    List<Booking> bookings = serviceItem.Bookings.Where(z => (inSelectedBooking.EndTime.AddMinutes(35) > z.StartTime && z.StartTime > inSelectedBooking.EndTime.AddMinutes(15)) || (z.EndTime > inSelectedBooking.StartTime.AddMinutes(-35) && z.EndTime < inSelectedBooking.StartTime.AddMinutes(-15))).ToList();
                    if (bookings.Count != 0)
                    {
                        serviceItem.Bookings = bookings.Where(x=>x.Available == true).ToList();
                        someServices.Add(serviceItem);
                    }
                }
                bookingSystemItem.Services = someServices;
                onlyBookingSystemsWithRelevantTimes.Add(bookingSystemItem);
            }
			return onlyBookingSystemsWithRelevantTimes.Where(x=>x.Services.Any(y=>y.Bookings.Count != 0)).ToList();
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

