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
        List<Branch> GetBranchesInBookingSystem(BookingSystem bookingSystem);
        List<BookingSystem> GetBookingSystemsInOtherBranches(List<BookingSystem> inBookingSystems, Service selectedService);
        double GetDistanceTo(BookingSystem bookingSystemA, BookingSystem bookingSystemB);
        List<BookingSystem> OrderByDistance(List<BookingSystem> inBookingSystems, BookingSystem inSelectedBookingSystem);
        string GetBrachesCount(List<BookingSystem> inBookingSystems);
        bool InDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance);
        IEnumerable<BookingSystem> GetAllBookingSystems();//tas bort?
        List<BookingSystem> GetBookingSystemsInRange(BookingSystem inSelectedBookingSystem);
        //void AddBookingSystem(BookingSystem bookingsystem);
        void EditBookingSystem(BookingSystem bookingSystem);
        void RemoveBookingSystem(int id);

    }
    
}
