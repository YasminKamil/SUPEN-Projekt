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

        public Booking CreateBooking(int id)
        {
            BookingSystem system = ApplicationDbContext.BookingSystems.Find(id);
            
            Booking booking = new Booking();
            booking.UserName = system.CompanyName;
            booking.UserMail = system.ContactEmail;
            booking.UserMobile = system.ContactPhone;
            booking.Subject = system.Services.Single().ServiceName;
            booking.StartTime = DateTime.Today;
            booking.EndTime = DateTime.Today;
            booking.Date = DateTime.Today;
            booking.BookingSystem = system;
            booking.Service = system.Services.Single();

            Add(booking);
            return booking;
            
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

    }

   
}