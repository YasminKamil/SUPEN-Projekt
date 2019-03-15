﻿using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Device.Location;

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

        public void AddBooking(Booking booking, int id)
        {
            Get(id).Bookings.Add(booking);
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
        public bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance)
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

        //löser uppgift 2 i kravspecen
        public string GetBrachesCount(List<BookingSystem> inBookingSystems)
        {
            string branchesGrouped = "";
            List<Branch> t1Branches = new List<Branch>();
            foreach (var item in inBookingSystems)
            {
                List<Branch> t2Branches = new List<Branch>();
                foreach (var y in item.services)
                {
                    if (!t2Branches.Contains(y.branch))
                    {
                        t2Branches.Add(y.branch);
                    }
                }
                t1Branches.AddRange(t2Branches);
            }

            foreach (var item in t1Branches.GroupBy(x => x.branchName))
            {
                branchesGrouped += item.Key + " " + item.Count() + "\n";
            }
            return branchesGrouped;
        }
        List<BookingSystemOfInterest> DistBooking = new List<BookingSystemOfInterest>();
        //används för att koppla distans och bokninssystem, utan att behöva ändra i modellen då distansen är olika i varje sökning. 
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
        //Genom att skicka in en lista av bokningsystem och det valta företaget, sorteras dem efter vilken distans de har till det valda företaget.
        public List<BookingSystem> OrderByDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem)
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
        //returnerar distancen mellan 2 företag
        public double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB)
        {
            var aCoord = new GeoCoordinate(bookingSystemA.Latitude, bookingSystemA.Longitude);
            var bCoord = new GeoCoordinate(bookingSystemB.Latitude, bookingSystemB.Longitude);
            return aCoord.GetDistanceTo(bCoord);
        }
        //Returnerar bookingsystems som har services inom andra brancher, kan även returnera selectedBookingService
        public List<BookingSystem> GetBookingSystemsInOtherBranches(List<BookingSystem> inBookingSystems, Service selectedService)
        {
            List<BookingSystem> keep = new List<BookingSystem>();
            BookingSystem tmbBookingSystem = new BookingSystem();

            foreach (var aBookingSystem in inBookingSystems.Where(x => x.services != null))
            {
                List<Service> servi = aBookingSystem.services.Where(x => x.branch.branchName.ToString() != selectedService.branch.branchName).ToList<Service>();
                if (servi.Count() != 0)
                {
                    keep.Add(aBookingSystem);
                }
            }
            return keep;
        }
        public List<Branch> GetBranchesInBookingSystem(BookingSystem bookingSystem)
        {
            List<Branch> branchesInBookingSystem = new List<Branch>();
            foreach (var item in bookingSystem.services)
            {
                branchesInBookingSystem.Add(item.branch);
            }
            return branchesInBookingSystem;
        }

    }
}
    
