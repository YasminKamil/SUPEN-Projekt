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
            return GetAll(); ;
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
            booking.Available = false;
            booking.StartTime = DateTime.Today;
            booking.EndTime = DateTime.Today;
            booking.Date = DateTime.Today;
         //   booking.BookingSystem = bookingSystem;
         //   booking.Service = serv;
            

            Add(booking);
            return booking;
            
        }

		public void UpdateBooking(Booking booking) {
			//IEnumerable<BookingSystem> allBookingSystems = ApplicationDbContext.Set<BookingSystem>().Include(s => s.Services).ToList();
			//BookingSystem bookingSystem = allBookingSystems.Single(x => x.BookingSystemId == id);

			//Service service = bookingSystem.Services.Single(s => s.ServiceName == name);

			var existingBookings = ApplicationDbContext.Bookings.Where(b => b.BookingId == booking.BookingId).FirstOrDefault<Booking>();
			if(existingBookings != null) {
				existingBookings.UserName = booking.UserName;
				existingBookings.UserMail = booking.UserMail;
				existingBookings.UserMobile = booking.UserMobile;
				existingBookings.Available = false;
				existingBookings.StartTime = booking.StartTime;
				existingBookings.EndTime = booking.EndTime;
				existingBookings.Date = booking.Date;
				existingBookings.Price = booking.Price;
                //existingBookings.BookingSystem = bookingSystem;
                //existingBookings.Service = service;
                
			}

		}

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

    }

   
}