using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Device.Location;
using System.Threading.Tasks;


namespace SUPEN_Projekt.Repositories
{
    //Ett repository för metoder som hanterar bokningsystem.
    public class BookingSystemRepository : Repository<BookingSystem>, IBookingSystemRepository
    {

        //Konstruktor med ApplicationDbContext som parametervärde
        public BookingSystemRepository(ApplicationDbContext context) : base(context) { }

        //Gör ett anrop till ApplicationDbContext och returnerar contexten
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        //Retunerar alla bokningssystem finns lagrade
        public async Task<IEnumerable<BookingSystem>> GetBookingSystems()
        {

            return await ApplicationDbContext.Set<BookingSystem>().Include(i => i.Services.Select(s => s.Branch).Select(u => u.BranchRelations))
                .Include(i => i.Services.Select(b => b.Bookings)).ToListAsync();
        }

        //Returnerar det specifika bokningssystemet
        public async Task<BookingSystem> GetBookingSystem(int id)
        {
            //Hämtar först alla bokningssystem som är lagrade
            IEnumerable<BookingSystem> listbookingsystems = await GetBookingSystems();

            //Hämtar ut ett system genom att konrollera att parametervärdet stämmer överens med id:et på bokningssystemet som man försöker hämta
            BookingSystem bookingSystem = listbookingsystems.Single(x => x.BookingSystemId == id);
            return bookingSystem;
        }

        //Returnerar tjänsten för bokningsystemet
        public async Task<Service> GetBookingSystemService(int id, int ServiceId)
        {
            BookingSystem bookingsystem = await Get(id);
            Service service = bookingsystem.Services.Single(x => x.ServiceId == ServiceId);
            return service;
        }

        class DistanceScoreAndBookingSystem
        {
            public double distanceScore;
            public BookingSystemOfInterest bookingSystemOfInterest;
        }
        public class ClickOfService
        {
            public double score;
            public Service service;
        }

        //Hämtar relevanta bokningssystem som endast har lediga tider
        public async Task <List<BookingSystem>> GetRelevantBookingSystemOnlyWithAvailableTimes(int bookingSystemId, int serviceId, int bookingId)
        {
            BookingSystem selectedBookingSystem = await GetBookingSystem(bookingSystemId);
            Service selectedService = ApplicationDbContext.Services.Single(x => x.ServiceId == serviceId);
            Booking booking = ApplicationDbContext.Bookings.Single(x => x.BookingId == bookingId);

            /*Skapar listor på bokningssystem inom ett visst avstånd, inom andra branscher, ordnade enligt distansen,
			 med endast lediga tider och hämtar endast en från varje bransch*/
            List<BookingSystem> bookingSystemsInRange = await GetBookingSystemsInRange(selectedBookingSystem);
            List<BookingSystem> bookingSystemsInOtherBranches = GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
            List<BookingSystem> orderedByDistance = OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);
            List<BookingSystem> onlyWithAvailableTimes = GetBookingSystemsWithAvailableBooking(orderedByDistance, booking);
            List<BookingSystem> onlyOneOfEachBranch = GetBookingSystemsWithOnlyOneOfEachBranch(onlyWithAvailableTimes);

            int totalClicks = GetTotalClicksInBranchRelations(onlyOneOfEachBranch, selectedBookingSystem, selectedService);

            List<DistanceScoreAndBookingSystem> distanceScoreAndBookingSystem = new List<DistanceScoreAndBookingSystem>();
            //Tilldelar variabeln värdet på bokningssystem det högsta distans poäng.
            distanceScoreAndBookingSystem = GetBookingSystemsWithDistanceScore(onlyOneOfEachBranch, selectedBookingSystem);

            List<ClickOfService> clickOfServices = new List<ClickOfService>();
            //Tilldelar variabeln ett värde på antalet klickpoäng som tjänsten har. 
            clickOfServices = GetClickOfServices(distanceScoreAndBookingSystem, selectedService, totalClicks);

            //Ordnar tjänsterna från högst till lägst poäng.
            List<BookingSystem> orderedByPoints = GetBookingSystemsOrderedByPoints(clickOfServices.OrderByDescending(x => x.score).ToList(), onlyOneOfEachBranch);

            return orderedByPoints;
        }

        //Hämtar bokningssystemen som har tjänster som har fått flest bokningar
        private List<BookingSystem> GetBookingSystemsOrderedByPoints(List<ClickOfService> clickOfServices, List<BookingSystem> onlyOneOfEachBranch)
        {
            List<BookingSystem> orderedByPoints = new List<BookingSystem>();

            foreach (var item in clickOfServices)
            {
                if (onlyOneOfEachBranch.Count() != 0 && !orderedByPoints.Contains(onlyOneOfEachBranch.Single(x => x.Services.Any(y => y.ServiceId == item.service.ServiceId))))
                {
                    if (!orderedByPoints.Contains(onlyOneOfEachBranch.Single(x => x.Services.Any(y => y.ServiceId == item.service.ServiceId))))
                    {

                        orderedByPoints.Add(onlyOneOfEachBranch.Single(x => x.Services.Any(y => y.ServiceId == item.service.ServiceId)));
                    }
                }
            }
            return orderedByPoints;
        }

        //Hämtar antalet poäng som den valda tjänsten har fått 
        private List<ClickOfService> GetClickOfServices(List<DistanceScoreAndBookingSystem> distanceScoreAndBookingSystem, Service selectedService, int totKlick)
        {
            BranchRepository branchRepository = new BranchRepository(ApplicationDbContext);

            List<ClickOfService> clickOfService = new List<ClickOfService>();
            foreach (var item in distanceScoreAndBookingSystem)
            {
                //Distanspoäng + baspoäng
                double counter = item.distanceScore + 5;

                foreach (var service in item.bookingSystemOfInterest.bookingSystem.Services)
                {
                    double score = 1;
                    if (selectedService.Branch.BranchRelations.Any(y => y.branchBId2 == service.Branch.BranchId.ToString()))
                    {
                        //Skapar en branschrelation med den valda tjänsten
                        BranchRelation branchRelation = selectedService.Branch.BranchRelations.Single(x => x.branchBId2 == service.Branch.BranchId.ToString());
                        //Räknar antalet bokningar(klick för att boka)
                        int click = branchRelation.CountClick;
                        score += (double)click / totKlick;
                    }

                    ClickOfService aObject = new ClickOfService();
                    aObject.service = service;

                    aObject.score = score * counter;
                    clickOfService.Add(aObject);
                }

            }

            return clickOfService;
        }

        //Hämtar bokningssystemen beroende på vad de har för distanspoäng
        private List<DistanceScoreAndBookingSystem> GetBookingSystemsWithDistanceScore(List<BookingSystem> onlyOneOfEachBranch, BookingSystem selectedBookingSystem)
        {
            //Skapar en lista på bokningssystem som hämtar ut ett system får varje bransch baserad på vad användaren har valt för bokningsystemet i början
            List<BookingSystemOfInterest> bookingSystemsOfInterest = GetBookingsWithDistance(onlyOneOfEachBranch, selectedBookingSystem);
            int distanceScore = bookingSystemsOfInterest.Count();

            //Skapar en lista på bokningssystem med distans poäng.
            List<DistanceScoreAndBookingSystem> distanceScoreAndBookingSystem = new List<DistanceScoreAndBookingSystem>();
            foreach (var bookingSystemOfInterest in bookingSystemsOfInterest)
            {
                //Instansierar ett nytt objekt
                DistanceScoreAndBookingSystem aObject = new DistanceScoreAndBookingSystem();
                aObject.distanceScore = distanceScore;
                aObject.bookingSystemOfInterest = bookingSystemOfInterest;

                //Lagrar objektet
                distanceScoreAndBookingSystem.Add(aObject);
                distanceScore--;
            }

            return distanceScoreAndBookingSystem;
        }

        //Hämtar totalt antal klick för bransch relationerna 
        private int GetTotalClicksInBranchRelations(List<BookingSystem> onlyOneOfEachBranch, BookingSystem selectedBookingSystem, Service selectedService)
        {
            List<BookingSystemOfInterest> bookingSystemsOfInterest = GetBookingsWithDistance(onlyOneOfEachBranch, selectedBookingSystem);
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
                if (servicesWithRelationToSelectedService.Any(x => x.Branch.BranchId.ToString() == relation.branchBId2))
                {
                    totKlick += relation.CountClick;
                }
            }

            return totKlick;
        }

        private List<BookingSystemOfInterest> GetBookingsWithDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem)
        {
            List<BookingSystemOfInterest> DistBooking = new List<BookingSystemOfInterest>();

            foreach (var item in inBookingSystems)
            {
                DistBooking.Add(new BookingSystemOfInterest(item, GetDistanceTo(inSelectedBookingSystem, item)));
            }
            return DistBooking;
        }

        /*Returnerar en lista av bookingssystem, där vi endast tar med ett företag av varje bransch. Detta för att ex inte visa 10 hotell, 
        utan endast det som är närmst*/
        private List<BookingSystem> GetBookingSystemsWithOnlyOneOfEachBranch(List<BookingSystem> inBookingSystems)
        {
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

        //Returnerar bokningsystem inom en viss distans inom vald stad//ApplicationDbContext.BookingSystems
        private async Task<List<BookingSystem>> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem)
        {
			var companiesInSelectedCity = await GetBookingSystems();
			companiesInSelectedCity.Where(x => x.City.ToLower() == inSelectedBookingSystem.City.ToLower()
		   && x.CompanyName != inSelectedBookingSystem.CompanyName);


			List<BookingSystem> companiesInRange = new List<BookingSystem>();

            foreach (var item in companiesInSelectedCity)
            {
                if (InDistance(inSelectedBookingSystem.Longitude, inSelectedBookingSystem.Latitude, item.Longitude, item.Latitude, 5000))
                {
                     companiesInRange.Add(item);
                    Console.WriteLine(item.CompanyName);
                }
            }
            return companiesInRange;
        }

        //Beräknar distansen till andra företag. Returnerar true/false beroende på om avståndet är ok.
        private bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance)
        {
            bool isCloseEnough = false;
            //Gör om koordinater till radian
            companyALat = companyALat / 180 * Math.PI;
            companyALong = companyALong / 180 * Math.PI;
            companyBLong = companyBLong / 180 * Math.PI;
            companyBLat = companyBLat / 180 * Math.PI;
            //beräknar distansen mellan de två olika företagen
            double distanceLatitude = (Math.Abs(companyALat - companyBLat)) / 2;
            double distanceLongitude = (Math.Abs(companyALong - companyBLong)) / 2;
            double x = Math.Sin(distanceLatitude) * Math.Sin(distanceLatitude) + Math.Cos(companyALat) * Math.Cos(companyBLat) *
                       Math.Sin(distanceLongitude) * Math.Sin(distanceLatitude);
            double y = 2 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));
            //6371000 är jordens radie
            y = y * 6371000;
            //Om distansen är längre än maxDistance returneras false
            if (y <= maxDistance) isCloseEnough = true;
            return isCloseEnough;
        }

        List<BookingSystemOfInterest> DistBooking = new List<BookingSystemOfInterest>();

        //Används för att koppla distans och bokningssystem, utan att behöva ändra i modellen då distansen är olika i varje sökning. 
        private class BookingSystemOfInterest
        {
            public BookingSystem bookingSystem;
            public double distance;
            public BookingSystemOfInterest(BookingSystem bookingSystem, double distance)
            {
                this.bookingSystem = bookingSystem;
                this.distance = distance;
            }
        }

        //Returnerar endast företag som har lediga tider som börjar strax efter eller slutar en liten stund före bokad tjänst
        private List<BookingSystem> GetBookingSystemsWithAvailableBooking(List<BookingSystem> inBookingSystems, Booking inSelectedBooking)
        {

            //Bokningssystemen har öppet i 8 timmar
            int open = 8;
            foreach (var bookingSystem in inBookingSystems)
            {
                foreach (var service in bookingSystem.Services)
                {
                    decimal inHours = Convert.ToDecimal(service.Duration) / Convert.ToDecimal(60);
                    int iterations = (int)Math.Floor(Convert.ToDecimal(open) / Convert.ToDecimal(inHours));

                    //Presenterar aktuella tider för dagens datum
                    DateTime startTime = DateTime.Today;
                    startTime = startTime.AddHours(8);

                    List<DateTime> dt = new List<DateTime>();

                    for (int i = 0; i < iterations; i++)
                    {
                        //Starttiden för nästa bokning är sluttiden för föregående bokning.
                        DateTime endTime = startTime;
                        endTime = endTime.AddMinutes(service.Duration);

                        if (!service.Bookings.Any(x => x.StartTime == startTime))
                        {

                            Booking booking = new Booking();
                            booking.Available = true;
                            booking.Date = DateTime.Today;
                            booking.StartTime = startTime;
                            booking.EndTime = endTime;
                            service.Bookings.Add(booking);
                        }
                        startTime = endTime;
                    }
                }
            }

            //Ordnar så vi inte behöver iterera genom alla objekt. 
            inBookingSystems = inBookingSystems.Where(x => x.Services.Any(y => y.Bookings.Any(z => (inSelectedBooking.EndTime.AddMinutes(35) > z.StartTime && z.StartTime > inSelectedBooking.EndTime.AddMinutes(15)) || (z.EndTime > inSelectedBooking.StartTime.AddMinutes(-35) && z.EndTime < inSelectedBooking.StartTime.AddMinutes(-15))))).ToList();
            inBookingSystems = inBookingSystems.Where(x => x.Services.Any(y => y.Bookings.Any(f => f.Available == true))).ToList();

            //Skapar en lista för endast lediga tider
            List<BookingSystem> onlyBookingSystemsWithRelevantTimes = new List<BookingSystem>();


            foreach (var bookingSystemItem in inBookingSystems)
            {
                List<Service> someServices = new List<Service>();
                foreach (var serviceItem in bookingSystemItem.Services)
                {
                    /*Kontrollerar så att användaren inte får några förslag på tider som är mindre än 15 minuter eller mer 35 minuter i 
					 när man ska boka ytterligare en tid när man får upp relevanta bokningar i närheten*/
                    List<Booking> bookings = serviceItem.Bookings.Where(z => (inSelectedBooking.EndTime.AddMinutes(35) > z.StartTime && z.StartTime >
                    inSelectedBooking.EndTime.AddMinutes(15)) || (z.EndTime > inSelectedBooking.StartTime.AddMinutes(-35) && z.EndTime <
                    inSelectedBooking.StartTime.AddMinutes(-15))).ToList();

                    if (bookings.Count != 0)
                    {
                        serviceItem.Bookings = bookings.Where(x => x.Available == true).ToList();
                        someServices.Add(serviceItem);
                    }
                }
                bookingSystemItem.Services = someServices;
                onlyBookingSystemsWithRelevantTimes.Add(bookingSystemItem);
            }
            return onlyBookingSystemsWithRelevantTimes.Where(x => x.Services.Any(y => y.Bookings.Count != 0)).ToList();
        }

        //Genom att skicka in en lista av bokningsystem och det valda företaget, sorteras dem efter vilken distans de har till det valda företaget.
        private List<BookingSystem> OrderByDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem)
        {

            foreach (var item in inBookingSystems)
            {
                DistBooking.Add(new BookingSystemOfInterest(item, GetDistanceTo(inSelectedBookingSystem, item)));
            }
            inBookingSystems = new List<BookingSystem>();
            foreach (var item in DistBooking.OrderBy(x => x.distance))
            {
                inBookingSystems.Add(item.bookingSystem);
                Console.WriteLine(item.distance + " " + item.bookingSystem.CompanyName);
            }
            return inBookingSystems;
        }

        //Returnerar distancen mellan 2 företag
        public double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB)
        {
            var aCoord = new GeoCoordinate(bookingSystemA.Latitude, bookingSystemA.Longitude);
            var bCoord = new GeoCoordinate(bookingSystemB.Latitude, bookingSystemB.Longitude);
            return aCoord.GetDistanceTo(bCoord);
        }

        //Returnerar bookingsystem som har services inom andra brancher, kan även returnera selectedBookingService
        private List<BookingSystem> GetBookingSystemsInOtherBranches(List<BookingSystem> inBookingSystems, Service selectedService)
        {
            List<BookingSystem> keep = new List<BookingSystem>();
            BookingSystem tmbBookingSystem = new BookingSystem();

            foreach (var aBookingSystem in inBookingSystems.Where(x => x.Services != null))
            {
                List<Service> service = aBookingSystem.Services
                    .Where(x => x.Branch.BranchName != selectedService.Branch.BranchName)
                    .ToList<Service>();
                if (service.Count() != 0)
                {
                    keep.Add(aBookingSystem);
                }
            }
            return keep;
        }

        //Returnerar brancher för bokningsystemet
        private List<string> GetBranchesInBookingSystem(BookingSystem bookingSystem)
        {
            List<string> branchesInBookingSystem = new List<string>();
            foreach (var item in bookingSystem.Services)
            {
                branchesInBookingSystem.Add(item.Branch.BranchName);

            }
            return branchesInBookingSystem;
        }

        public async Task <BookingSystem> GetBookServiceSuggestion(Booking inBooking, string inServiceName, int inBookingSystemId)
        {
            //Den bokningen vi tar in, det är den bokningen som finns i relevantbookingvyn
            var inBookingSystem = await Get(inBookingSystemId);
            var bookingSystemsInCity = await GetBookingSystemsInRange(inBookingSystem);
			
			// alla services från databasen
			List<Service> formerBookedServices = new List<Service>();//ny service med lista bokningar
            List<int> mostBookings = new List<int>();
            List<Booking> bookings = new List<Booking>();
            List<BookingSystem> otherBookingSystems = new List<BookingSystem>();

            foreach (var bookingSystem in bookingSystemsInCity)
            {
                if (bookingSystem.BookingSystemId != inBookingSystemId && bookingSystem.Services.Count() != 0)
                {
                    foreach (var service in bookingSystem.Services)
                    {//för varje tjänst i från alla tjänster
                        if (service.Bookings.Count > 0 && service.ServiceName != inServiceName)
                        {

                            bookings = service.Bookings.Where(x => x.UserName == inBooking.UserName).ToList();
                            var numberOfTimes = bookings.Count();//antal bokningar,

                            mostBookings.Add(numberOfTimes);

                            if (bookings.Count() > 0)//== mostBookings.Max())
                            {
                                formerBookedServices.Add(service);
                                otherBookingSystems.Add(bookingSystem);
                               
                            }

                        }
                    }

                }
            }
            Service serviceSuggestion = new Service();
            BookingSystem bs = new BookingSystem();
            if (formerBookedServices != null && formerBookedServices.Count() != 0)
            {
                serviceSuggestion = formerBookedServices.Where(x => x.Bookings.Count == mostBookings.Max()).First();

                foreach (var item in otherBookingSystems)
                {
                    if (item.Services.Where(x => x.Bookings.Count() == serviceSuggestion.Bookings.Count()).Any())
                    {
                        bs = item;
                    }
                }
            }

            return bs;//returnerar det företag som har en tjänst som användaren nyttjat flest gånger

        }

        public Booking GetServiceSuggestionBookings(List<BookingSystem> inBookingSystem, Booking inBooking)
        {
            var bookingSystems = GetBookingSystemsWithAvailableBooking(inBookingSystem, inBooking);
            List<Booking> bookings = new List<Booking>();
            Booking serviceSuggestionBooking = new Booking();

            foreach (var bookingSystem in bookingSystems)
            {
                foreach (var service in bookingSystem.Services)
                {
                    foreach (var booking in service.Bookings)
                    {
                        bookings.Add(booking);
                    }                
                 
                }
            }

            if (bookings.Count > 0)
            {
                serviceSuggestionBooking = bookings.First();
            }

            return serviceSuggestionBooking;
        }
    }
}

