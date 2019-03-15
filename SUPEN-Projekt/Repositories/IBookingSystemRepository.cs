using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    public interface IBookingSystemRepository : IRepository<BookingSystem>
    {

        IEnumerable<BookingSystem> GetAllBookingSystems();//tas bort?
        List<BookingSystem> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem);
        //void AddBookingSystem(BookingSystem bookingsystem);
        void EditBookingSystem(BookingSystem bookingSystem);
        void RemoveBookingSystem(int id);
        void AddBooking(Booking booking, int id);

    }
    
}
