using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {

        IEnumerable<Booking> GetAllBookings();
    }
}
