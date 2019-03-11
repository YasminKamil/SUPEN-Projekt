using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories
{
    public class BookingSystemRepository : Repository<BookingSystem>, IBookingSystemRepository
    {
        public BookingSystemRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<BookingSystem> GetAllBookingSystems()
        {
            return ApplicationDbContext.Set<BookingSystem>().ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
    
