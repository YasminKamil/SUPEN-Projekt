using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
	public class ApplicationDbContext : DbContext{
		public ApplicationDbContext() : base("BookingSystemDbContext") { }

		public DbSet<BookingSystem> BookingSystems { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Service> Services { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> {
		protected override void Seed(ApplicationDbContext context) {
            // skapar kategorier, som sne kan läggas på företag. 
            // frågan är den om det inte är bättre att ha dessa på tjän
            Branch b1 = new Branch();
            b1.BranchId = 1;
            b1.BranchName = "Frisör";

            Branch b2 = new Branch();
            b2.BranchId = 2;
            b2.BranchName = "Besiktning";

            Branch b3 = new Branch();
            b3.BranchId = 3;
            b3.BranchName = "Däck";

            Branch b4= new Branch();
            b3.BranchId = 4;
            b3.BranchName = "Café";

            Service s1 = new Service();
            s1.ServiceName = "Klippning";
            s1.Price = 250;
            s1.Duration = 30;

            Service s2 = new Service();
            s1.ServiceName = "Färgning";
            s1.Price = 450;
            s1.Duration = 90;

            Service s3 = new Service();
            s1.ServiceName = "däckbyte";
            s1.Price = 250;
            s1.Duration = 30;


            s1.Branch = b1;
            s2.Branch = b1;
            s3.Branch = b3;

            var bookingSystems = new List<BookingSystem>() {
                new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "ArtofHair@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/ArtofHair",
                    CompanyName = "Art of Hair",
                    ContactEmail = "ArtofHair@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = " Fabriksgatan 13",
                    Latitude = 59.2703188,
                    Longitude = 15.2074733,
                    PostalCode = "702 10",
                    City = "Örebro", services = new List<Service> () {s1, s2}
                },

                new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "bullvivan@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/bullvivan",
                    CompanyName = "Bullvivan",
                    ContactEmail = "bullvivan@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Kyrkvärdsvägen 17",
                    Latitude = 59.27412,
                    Longitude = 15.2066,
                    PostalCode = "702 84",
                    City = "Örebro" 
                },
                 new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "Epiroc@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/Epiroc",
                    CompanyName = "Epiroc",
                    ContactEmail = "Epiroc@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Tackjärnsgatan 8",
                    Latitude = 59.291713,
                    Longitude = 15.204345,
                    PostalCode = "703 83",
                    City = "Örebro"  
                },
                 new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "Frisorkompaniet@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/salongfinest",
                    CompanyName = "Frisörkompaniet",
                    ContactEmail = "Frisörkompaniet@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Kristinagatan 10",
                    Latitude = 59.270042,
                    Longitude = 15.229628,
                    PostalCode = "602 26",
                    City = "Norrköping"  
                },
                 new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "Carspect@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/Carspect",
                    CompanyName = "Carspect",
                    ContactEmail = "Carspect@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Bangårdsg. 5",
                    Latitude = 58.593966,
                    Longitude = 16.204253,
                    PostalCode = "602 28",
                    City = "Norrköping"
                },
                 new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "Sodermalmdack@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/Sodermalmdack",
                    CompanyName = "Södermalm däck & bilrekond",
                    ContactEmail = "Sodermalmdack@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Rutger Fuchsgatan 4",
                    Latitude = 59.3079479,
                    Longitude = 18.0789683,
                    PostalCode = "116 67",
                    City = "Stockholm", services = new List<Service> () {s3}
                },
                 new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "Noir@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/Noir",
                    CompanyName = "Noir",
                    ContactEmail = "Noir@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Regeringsgatan 80",
                    Latitude = 59.3378022,
                    Longitude = 18.0674249,
                    PostalCode = "111 39",
                    City = "Stockholm"
                },
                 new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "BodyFace@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/BodyFace",
                    CompanyName = "BodyFace",
                    ContactEmail = "BodyFace@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Fredriksbergsgatan 6",
                    Latitude = 55.6066851,
                    Longitude = 13.0183526,
                    PostalCode = "212 11",
                    City = "Malmö"
                },
				  new BookingSystem {
                    SystemName = "boka.se",
                    SystemDescription = "Description...",
                    Email = "Besikta@boka.se",
                    PhoneNumber = "070 - 000 00 00",
                    Website = "boka.se/Besikta",
                    CompanyName = "Besikta",
                    ContactEmail = "Besikta@boka.se",
                    ContactPhone = "070 - 123 56 78",
                    Address = "Källvattengatan 7",
                    Latitude = 55.6059,
                    Longitude = 13.0007,
                    PostalCode = "212 23",
                    City = "Malmö"
                }
            };
			bookingSystems.ForEach(x => context.BookingSystems.Add(x));
			context.SaveChanges();
		}
	}
}