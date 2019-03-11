using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    interface IBookingSystemRepository : IRepository<BookingSystem>
    {

        IEnumerable<BookingSystem> GetAllBookingSystems();

    }
    
}
