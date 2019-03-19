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

        public void CreateBooking(BookingSystem system, Service service)
        {
            Booking booking = new Booking();
            booking.UserName = system.CompanyName;
            booking.UserMail = system.ContactEmail;
            booking.UserMobile = system.ContactPhone;
            booking.Subject = service.ServiceName;
            booking.StartTime = DateTime.Today;
            booking.EndTime = DateTime.Today;
            booking.Date = DateTime.Today;
            booking.BookingSystem = system;
            booking.Service = service;

            Add(booking);

            //return booking;

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

    }

   
}