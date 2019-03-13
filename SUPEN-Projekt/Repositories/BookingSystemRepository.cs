using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories
{
    public class BookingSystemRepository : Repository<BookingSystem>, IBookingSystemRepository
    {
        //private readonly IUnitOfWork _unitOfWork;
        public BookingSystemRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<BookingSystem> GetAllBookingSystems()
        {
            return ApplicationDbContext.Set<BookingSystem>().ToList();
        }

        public void EditBookingSystem(BookingSystem bookingSystem)
        {
            ApplicationDbContext.Entry(bookingSystem).State = EntityState.Modified;
            ApplicationDbContext.SaveChanges();
        }

        public void RemoveBookingSystem(int id)
        {
            BookingSystem bookingSystem = Get(id);
            Remove(bookingSystem);    
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public List<BookingSystem> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem)
        {
            var companiesInSelectedCity = ApplicationDbContext.BookingSystems.Where(x => x.City.ToLower() == inSelectedBookingSystem.City.ToLower() && x.CompanyName != inSelectedBookingSystem.CompanyName);
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

        //beräknar distansen till andra företag. Returnerar t/f beroende på om avståndet är ok.
        private bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance)
        {
            bool isCloseEnough = false;
            companyALat = companyALat / 180 * Math.PI;
            companyALong = companyALong / 180 * Math.PI;
            companyBLong = companyBLong / 180 * Math.PI;
            companyBLat = companyBLat / 180 * Math.PI;
            double distanceLatitude = (Math.Abs(companyALat - companyBLat)) / 2;
            double distanceLongitude = (Math.Abs(companyALong - companyBLong)) / 2;
            double x = Math.Sin(distanceLatitude) * Math.Sin(distanceLatitude) + Math.Cos(companyALat) * Math.Cos(companyBLat) * Math.Sin(distanceLongitude) * Math.Sin(distanceLatitude);
            double y = 2 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));
            y = y * 6371000;
            if (y <= maxDistance) isCloseEnough = true;
            return isCloseEnough;
        }
    }
}
    
