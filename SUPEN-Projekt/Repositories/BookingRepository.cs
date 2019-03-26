using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return GetAll();
        }

        public Booking CreateBooking(int id, string name)
        {
            IEnumerable<BookingSystem> allBookingSystems = ApplicationDbContext.Set<BookingSystem>().Include(x => x.Services).ToList();
            BookingSystem bookingSystem = allBookingSystems.Single(x=> x.BookingSystemId == id);
            
            Service serv = bookingSystem.Services.Single(x => x.ServiceName == name);
            
            Booking booking = new Booking();
            booking.UserName = bookingSystem.CompanyName;
            booking.UserMail = bookingSystem.ContactEmail;
            booking.UserMobile = bookingSystem.ContactPhone;
            booking.Subject = serv.ServiceName;
            //booking.StartTime = DateTime.Today;
            //booking.EndTime = DateTime.Today;
            //booking.Date = DateTime.Today;
          //  booking.BookingSystem = bookingSystem;
            booking.Services = new List<Service>{ serv};

            Add(booking);
            return booking;
            
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

    }

   
}