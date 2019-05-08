using System;

namespace SUPEN_Projekt.Repositories {

	//Interface på vilka repositories och metoden som UnitOfWork hanterar för att kunna återanvända koden i Controllerna
	public interface IUnitOfWork : IDisposable {
		IBookingRepository Bookings { get; }
		IBookingSystemRepository BookingSystems { get; }
		IServiceRepository Services { get; }
		IBranchRepository Branches { get; }
		int Complete();
	}
}
