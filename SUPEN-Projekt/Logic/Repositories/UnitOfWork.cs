using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories {

	/*Klassen innefattar operationen spara och med klassen kan man även återanvända koden. Genom att använda UnitOfWork görs allting i en enda transaktion när man 
	 * ska spara, istället för att göra flera databastransaktioner. */
	public class UnitOfWork : IUnitOfWork {
		private readonly ApplicationDbContext _context;
		public IBookingRepository Bookings { get; private set; }
		public IBookingSystemRepository BookingSystems { get; private set; }
		public IServiceRepository Services { get; private set; }
		public IBranchRepository Branches { get; private set; }


		//Sparar alla tillgänliga repositories som finns i systemet i vår ApplicationDbContext
		public UnitOfWork(ApplicationDbContext context) {
			_context = context;
			Bookings = new BookingRepository(_context);
			BookingSystems = new BookingSystemRepository(_context);
			Services = new ServiceRepository(_context);
			Branches = new BranchRepository(_context);

		}

		//Sparar ändringar i databasen
		public int Complete() {
			return _context.SaveChanges();
		}

		//Koden har implementerats för att på ett korrekt sätt kunna genomföra dispose-mönster
		public void Dispose() {
			_context.Dispose();

		}
	}
}