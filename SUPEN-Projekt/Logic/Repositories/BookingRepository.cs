using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories {
	public class BookingRepository : Repository<Booking>, IBookingRepository {
		public BookingRepository(ApplicationDbContext context) : base(context) {
		}

		//Returnerar en list över bokningar
		public IEnumerable<Booking> GetBookings() {
			return GetAll(); ;
		}


		//Skapar en hel bokning inklusive tid, datum och pris
		public Booking CreateBooking(Booking inBooking) {
			//IEnumerable<BookingSystem> allBookingSystems = ApplicationDbContext.Set<BookingSystem>().Include(x => x.Services).ToList();
			//BookingSystem bookingSystem = allBookingSystems.Single(x=> x.BookingSystemId == systemId);

			//Service serv = bookingSystem.Services.Single(x => x.ServiceName == name);

			ApplicationDbContext context = new ApplicationDbContext();

			Booking booking = new Booking();
			booking.BookingId = context.Bookings.Count();
			booking.UserName = inBooking.UserName;
			booking.UserMail = inBooking.UserMail;
			booking.UserMobile = inBooking.UserMobile;
			booking.Available = true;
			booking.StartTime = inBooking.StartTime;
			booking.EndTime = inBooking.EndTime;
			booking.Date = inBooking.Date;
			//booking.Price = inBooking.Price;

			Add(booking);
			return booking;


	}

		//Skapar en bokning med tillgängliga tider som går att bokas
		//public void UpdateBooking(Booking booking) {
		//	var existingBookings = ApplicationDbContext.Bookings.Where(b => b.BookingId == booking.BookingId).FirstOrDefault<Booking>();
		//	if (existingBookings != null) {
		//		existingBookings.UserName = booking.UserName;
		//		existingBookings.UserMail = booking.UserMail;
		//		existingBookings.UserMobile = booking.UserMobile;
		//		existingBookings.Available = false;
		//		existingBookings.StartTime = booking.StartTime;
		//		existingBookings.EndTime = booking.EndTime;
		//		existingBookings.Date = booking.Date;
		//		//existingBookings.Price = booking.Price;

		//	}

		//}

		public ApplicationDbContext ApplicationDbContext {
			get { return Context as ApplicationDbContext; }
		}

	}


}