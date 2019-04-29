using System;

namespace SUPEN_Projekt.Repositories {
	public interface IUnitOfWork : IDisposable {
		IBookingRepository Bookings { get; }
		IBookingSystemRepository BookingSystems { get; }
		IServiceRepository Services { get; }
		IBrancheRepository Branches { get; }
		int Complete();
	}
}
