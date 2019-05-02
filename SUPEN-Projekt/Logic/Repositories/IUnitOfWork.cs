using System;

namespace SUPEN_Projekt.Repositories {
	public interface IUnitOfWork : IDisposable {
		IBookingRepository Bookings { get; }
		IBookingSystemRepository BookingSystems { get; }
		IServiceRepository Services { get; }
		IBranchRepository Branches { get; }
		int Complete();
	}
}
