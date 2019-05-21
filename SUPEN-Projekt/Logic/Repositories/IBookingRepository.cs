using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    //Interface för BookingRepository för att kunna återanvända metoderna i presentationslagret/API:er
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> CreateBooking(Booking inBooking);
        Task<IEnumerable<Booking>> GetBookings();
    }
}
