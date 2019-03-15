using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get;}
        IBookingSystemRepository BookingSystems { get; }
		IBranchRepository Branches { get; }
		IServiceRepository Services { get; }
        int Complete();
    }
}
