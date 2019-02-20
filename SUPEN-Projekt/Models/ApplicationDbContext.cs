using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class ApplicationDbContext : DbContext{
		public ApplicationDbContext() : base("BookingSystemDbContext") { }

		public DbSet<BookingSystem> BookingSystem { get; set; }
		public DbSet<Booking> Booking { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> {
		protected override void Seed(ApplicationDbContext context) {
			var bookingSystems = new List<BookingSystem>() {
				new BookingSystem {
					SystemName = "boka.se",
					SystemDescription = "Description...",
					Email = "boka@boka.se",
					PhoneNumber = "070 - 000 00 00",
					Website = "boka.se/salongfinest",
					CompanyName = "Salong Finest",
					ContactEmail = "salongfinest@boka.se",
					ContactPhone = "070 - 123 56 78",
					Address = "Köpmangatan 2",
					Latitude = 1,
					Longitude = 2,
					PostalCode = "702 10",
					City = "Örebro"
				}
			};
			bookingSystems.ForEach(x => context.BookingSystem.Add(x));
			context.SaveChanges();
		}
	}
}