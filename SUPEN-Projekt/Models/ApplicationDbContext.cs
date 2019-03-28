using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("BookingSystemDbContext") {
			this.Configuration.ProxyCreationEnabled = false;
		}

		public DbSet<BookingSystem> BookingSystems { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<BookingSystem>()
              .HasMany(c => c.Services).WithMany(i => i.BookingSystems)
              .Map(t => t.MapLeftKey("BookingSystemId")
                  .MapRightKey("ServiceId")
                  .ToTable("BookingSystemService"));
        }

    }
    public class DatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {

            var branches = new List<Branch>() {
                new Branch {
            BranchName = "Frisör"},
                new Branch {
            BranchName = "Besiktning"  },
                new Branch {
            BranchName = "Däck" },
                new Branch {
            BranchName = "Café"  },
            }; branches.ForEach(x => context.Branches.Add(x));
            //context.Branches.AddRange(branches);
            context.SaveChanges();









            getBookings(15, 200, 6);











            List<Booking> b1 = getBookings(45, 100, 8);
            context.Bookings.AddRange(b1);

            List<Booking> b2 = getBookings(25, 200, 5);
            context.Bookings.AddRange(b2);

            List<Booking> b3 = getBookings(45, 300, 9);
            context.Bookings.AddRange(b3);

            List<Booking> b4 = getBookings(60, 70, 12);
            context.Bookings.AddRange(b4);
            context.SaveChanges();

            var services = new List<Service>() {
                new Service{
                ServiceName = "Klippning",
                Duration = 45,
                Price = 100,
                Branch= context.Branches.Single(x=> x.BranchName =="Frisör" ),
                Bookings = b1},
                new Service {ServiceName = "Färgning",
                Duration = 25,
                Price = 200,
                Branch = context.Branches.Single(x=> x.BranchName =="Frisör" ),
                Bookings = b2},
                new Service { ServiceName = "Däckbyte",
                Duration = 45,
                Price = 300,
                Branch = context.Branches.Single(x=> x.BranchName =="Däck" ),
                Bookings = b3},
                new Service { ServiceName = "Bullfika",
                Duration = 60,
                Price = 70,
                Branch = context.Branches.Single(x=> x.BranchName =="Café" ),
                Bookings = b4},            
            };
            services.ForEach(x => context.Services.Add(x));

            context.SaveChanges();
          
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
                    City = "Örebro",
                    Services = new List<Service>() //{ context.Services.Single(x => x.ServiceId == 1), context.Services.Single(x => x.ServiceId == 2) }
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
                    City = "Örebro",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 4) }
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
                    City = "Örebro",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 3) }
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
                    City = "Norrköping",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 1), context.Services.Single(x => x.ServiceId == 2) }
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
                    City = "Norrköping",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 3)}
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
                    City = "Stockholm",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 3)}
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
                    City = "Stockholm",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 1), context.Services.Single(x => x.ServiceId == 2) }
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
                    City = "Malmö",
                    Services = new List<Service>()//{ context.Services.Single(x => x.ServiceId == 1), context.Services.Single(x => x.ServiceId == 2) }
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
                    City = "Malmö",
                    Services = new List<Service>()
                }
            };

            
            bookingSystems.ForEach(x => context.BookingSystems.Add(x));
           context.SaveChanges();

            AddServices(context, "Art of Hair", "Klippning");
            AddServices(context, "Art of Hair", "Färgning");
            AddServices(context, "Bullvivan", "Bullfika");
            AddServices(context, "Epiroc", "Däckbyte");
            AddServices(context, "Frisörkompaniet", "Klippning");
            AddServices(context, "Carspect", "Däckbyte");
            AddServices(context, "Södermalm däck & bilrekond", "Däckbyte");
            AddServices(context, "Noir", "Färgning");
            AddServices(context, "BodyFace", "Färgning");
            //AddServices(context, "BodyFace", "Klippning");
            AddServices(context, "Besikta", "Däckbyte");

            context.SaveChanges();

            base.Seed(context);      
        }

        void AddServices(ApplicationDbContext context, string cName, string sName)
        {
            var bs = context.BookingSystems.SingleOrDefault(c => c.CompanyName == cName);
            var serv = bs.Services.SingleOrDefault(i => i.ServiceName == sName);
            if (serv == null)
                bs.Services.Add(context.Services.Single(i => i.ServiceName == sName));
        }


        //returnerar en lista på bokningar under öppentiden, skapar så många som möjligt under öppettiden.
        public List<Booking> getBookings(int duration, int price, int totalHoursOpen)
        {
            var bookings = new List<Booking>();


            decimal inHours = Convert.ToDecimal(duration)/Convert.ToDecimal(60) ;

            int iterations = (int)Math.Floor(Convert.ToDecimal(totalHoursOpen) / Convert.ToDecimal(inHours));

            DateTime previousEndTime = new DateTime();
            int i;
            for (i = 0; i < iterations; ++i)
            {
                var booking = new Booking();

                booking.Available = false;
                if (i == 0) booking.StartTime = DateTime.Today.AddHours(i + 8);
                else booking.StartTime = previousEndTime;

                previousEndTime = booking.EndTime = booking.StartTime.AddMinutes(duration);
                
                booking.Date = DateTime.Today;
                booking.Price =price;

                bookings.Add(booking);
            }
            return bookings;
        }
    }





}
