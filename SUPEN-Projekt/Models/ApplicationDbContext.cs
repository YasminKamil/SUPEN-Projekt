﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("BookingSystemDbContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<BookingSystem> BookingSystems { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Service> Services { get; set; }
    }

    public class DatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //Lägger till Branscher via addBranches metoden
           List<string> branches = new List<string> { "Frisör", "Besiktning", "Café", "Fordonsuthyrning", "Massör", "Verkstad",
               "Idrottsförening", "Kontor", "Utbildning", "Restaurang", "Sjukvård", "Transport", "Hotell", "Media", "IT",
               "Bank", "Bygg", "Konsultation", "Däck", "Tatuering" };
            addBranches(context, branches);

            //Lägger till BookingSystems via metoden addBookingSystem
            addBookingSystem(context, "boka.se", "Description...", "ArtofHair@boka.se", "070 - 000 00 00", "boka.se/ArtofHair", "Art of Hair", "ArtofHair@boka.se",
            "070 - 123 56 78", "Fabriksgatan 13", 59.2703188, 15.2074733, "702 10", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "bullvivan@boka.se", "070 - 000 00 00", "boka.se/bullvivan", "Bullvivan", "bullvivan@boka.se", 
            "070 - 123 56 78", "Kyrkvärdsvägen 17",59.27412, 15.2066, "702 84", "Örebro");

            addBookingSystem(context, "boka.se", "Description...","Epiroc@boka.se","070 - 000 00 00", "boka.se/Epiroc","Epiroc","Epiroc@boka.se",
            "070 - 123 56 78","Tackjärnsgatan 8",59.291713,15.204345, "703 83", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "Frisorkompaniet@boka.se", "070 - 000 00 00", "boka.se/salongfinest", "Frisörkompaniet","Frisörkompaniet@boka.se", 
            "070 - 123 56 78", "Kristinagatan 10", 59.270042, 15.229628,"602 26", "Norrköping");

            addBookingSystem(context, "boka.se", "Description...", "Carspect@boka.se", "070 - 000 00 00", "boka.se/Carspect", "Carspect", "Carspect@boka.se",
            "070 - 123 56 78", "Bangårdsg. 5", 58.593966, 16.204253, "602 28", "Norrköping");

            addBookingSystem(context, "boka.se", "Description...", "Sodermalmdack@boka.se", "070 - 000 00 00", "boka.se/Sodermalmdack", "Södermalm däck & bilrekond","Sodermalmdack@boka.se", 
            "070 - 123 56 78", "Rutger Fuchsgatan 4", 59.3079479, 18.0789683, "116 67", "Stockholm");

            addBookingSystem(context, "boka.se", "Description...", "Noir@boka.se", "070 - 000 00 00", "boka.se/Noir", "Noir", "Noir@boka.se", 
            "070 - 123 56 78","Regeringsgatan 80", 59.3378022, 18.0674249, "111 39", "Stockholm");

            addBookingSystem(context, "boka.se", "Description...", "BodyFace@boka.se", "070 - 000 00 00", "boka.se/BodyFace", "BodyFace", "BodyFace@boka.se",
            "070 - 123 56 78", "Fredriksbergsgatan 6", 55.6066851, 13.0183526, "212 11", "Malmö");

            addBookingSystem(context, "boka.se", "Description...", "Besikta@boka.se", "070 - 000 00 00", "boka.se/Besikta", "Besikta", "Besikta@boka.se", 
            "070 - 123 56 78","Källvattengatan 7", 55.6059, 13.0007, "212 23", "Malmö");

            addBookingSystem(context, "boka.se", "Description...", "Bilexpo@boka.se", "070 - 000 00 00", "boka.se/Bilexpo", "Bilexpo", "Bilexpo@boka.se",
            "070 - 123 56 78", "Karosserigatan 9", 59.296808, 15.234905, "703 69", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "Bilhusetiorebro@boka.se", "070 - 000 00 00", "boka.se/Bilhusetiorebro", "Bilhuset i Örebro", "Bilhusetiorebro@boka.se",
            "070 - 123 56 78", "Skjutbanevägen 4", 59.297731, 15.238027, "70369", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "LillanTennisklubb@boka.se", "070 - 000 00 00", "boka.se/LillanTennisklubb", "Lillån Tennisklubb", "LillanTennisklubb@boka.se",
            "070 - 123 56 78", "Kyrkvägen 2A", 59.322887, 15.227972, "703 75", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "Lillavallen@boka.se", "070 - 000 00 00", "boka.se/Lillåvallen", "Lillåvallen", "Lillavallen@boka.se",
            "070 - 123 56 78", "Kyrkvägen 2A", 59.323670, 15.226550, "70375", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "CityHotel@boka.se", "070 - 000 00 00", "boka.se/CityHotel", "City Hotel", "CityHotel@boka.se",
            "070 - 123 56 78", "Kungsgatan 24", 59.268741, 15.212420, "702 24", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "FirstHotel@boka.se", "070 - 000 00 00", "boka.se/FirstHotel", "First Hotel", "FirstHotel@boka.se",
            "070 - 123 56 78", "Storgatan 24", 59.277223, 15.216221, "703 61", "Örebro");

            addBookingSystem(context, "boka.se", "Description...", "EliteArtwork@boka.se", "070 - 000 00 00", "boka.se/EliteArtwork", "Elite Artwork", "EliteArtwork@boka.se",
            "070 - 123 56 78", "Storgatan 20", 59.276952, 15.215910, "703 61", "Örebro");

            context.SaveChanges();

            //Lillån Tennisklubb, Lillåvallen har inte tilldelats bransch och service

            //Lägger till och skapar services via addService metoden
            //context , servicenamn, t

            addService(context, "Service", 30, 1300, "Verkstad", "Bilexpo");
            addService(context, "Service", 35, 1260, "Verkstad", "Bilhuset i Örebro");
            addService(context, "Hotellrum", 25, 1250, "Hotell", "City Hotel");
            addService(context, "Hotellrum", 25, 1200, "Hotell", "First Hotel");
            addService(context, "Tatuering", 60, 800, "Tatuering", "Elite Artwork");
            addService(context, "Klippning", 25, 100, "Frisör","Art of Hair");
            addService(context, "Färgning", 45, 200, "Frisör", "Art of Hair");
            addService(context, "Bullfika", 45, 200, "Café", "Bullvivan");
            addService(context, "Hyr en dumper", 45, 200, "Fordonsuthyrning", "Epiroc");
            addService(context, "Klippning", 45, 200, "Frisör", "Frisörkompaniet");
            addService(context, "Däckbyte", 45, 200, "Däck", "Carspect");
            addService(context, "Däckbyte", 45, 200, "Däck", "Södermalm däck & bilrekond");
            addService(context, "Färgning", 45, 200, "Frisör", "Noir");
            addService(context, "Färgning", 45, 200, "Frisör", "BodyFace");
            addService(context, "Klippning", 25, 200, "Frisör", "BodyFace");
            addService(context, "Besiktning", 25, 200, "Besiktning", "Besikta");

            context.SaveChanges();

            base.Seed(context);
        }
        void addService(ApplicationDbContext context, string inServiceName, int inDuration, int inPrice, string inBranchName, string cName)
        {
            Service aService = new Service();
            aService.ServiceId = context.Services.Count();
            aService.ServiceName = inServiceName;
            aService.Duration = inDuration;
            aService.Price = inPrice;
            aService.Branch = context.Branches.Single(x => x.BranchName == inBranchName);
            aService.Bookings = getBookings(context,inPrice, inDuration);
            context.Services.Add(aService);
            context.SaveChanges();

            var bs = context.BookingSystems.SingleOrDefault(c => c.CompanyName == cName);
            var serv = bs.Services.SingleOrDefault(i => i.ServiceId == aService.ServiceId);
            if (serv == null)
                bs.Services.Add(context.Services.Single(i => i.ServiceId == aService.ServiceId));
            context.SaveChanges();

        }


        List<Booking> getBookings(ApplicationDbContext context, int inPrice, int inDuration)
        {

            List<Service> services = new List<Service>();
            Random randomNumber = new Random();
            //List<int> durations = new List<int> { 25, 30, 35, 40, 45, 50, 55, 60 };
            int duration = inDuration; //durations.OrderBy(x=> randomNumber.Next()).First();
            int price = inPrice; // randomNumber.Next(150, 400);
            int hoursOpen = randomNumber.Next(2, 10);
            List<Booking> listOfBookings = new List<Booking>();
            listOfBookings = seedBookings(duration, price, hoursOpen);
            context.Bookings.AddRange(listOfBookings);
            context.SaveChanges();

            return listOfBookings;
        }

        //returnerar en lista på bokningar under öppentiden, skapar så många som möjligt under öppettiden.
        //getBookings -> GetBookings
        public List<Booking> seedBookings(int duration, int price, int totalHoursOpen)
        {
            var bookings = new List<Booking>();
            decimal inHours = Convert.ToDecimal(duration) / Convert.ToDecimal(60);
            int iterations = (int)Math.Floor(Convert.ToDecimal(totalHoursOpen) / Convert.ToDecimal(inHours));

            DateTime previousEndTime = new DateTime();
            int i;
            for (i = 0; i < iterations; ++i)
            {
                var booking = new Booking();

                booking.Available = true;
                if (i == 0) booking.StartTime = DateTime.Today.AddHours(i + 8);
                else booking.StartTime = previousEndTime;

                previousEndTime = booking.EndTime = booking.StartTime.AddMinutes(duration);

                booking.Date = DateTime.Today;
                booking.Price = price;

                bookings.Add(booking);
            }
            return bookings;
        }

        void addBranches(ApplicationDbContext context, List<string> inBranchList)
        {
            List<String> branchString = inBranchList;
            foreach (var item in branchString)
            {
                Branch aBranch = new Branch();
                aBranch.BranchName = item;
                context.Branches.Add(aBranch);
            }
            context.SaveChanges();
        }


        void addBookingSystem(ApplicationDbContext context, string inSystemName, string inSystemDescription, string inEmail, string inPhoneNumber, string inWebsite, string inCompanyName, string inContactEmail, string inContactPhone, string inAddress, double inLatitude, double inLongitude, string inPostalCode, string inCity)
        {



            BookingSystem aBookingSystem = new BookingSystem
            {

                SystemName = inSystemName,
                SystemDescription = inSystemDescription,
                Email = inEmail,
                PhoneNumber = inPhoneNumber,
                Website = inWebsite,
                CompanyName = inCompanyName,
                ContactEmail = inContactEmail,
                ContactPhone = inContactPhone,
                Address = inAddress,
                Latitude = inLatitude,
                Longitude = inLongitude,
                PostalCode = inPostalCode,
                City = inCity,
                Services = new List<Service>() //{ context.Services.Single(x => x.ServiceId == 1), context.Services.Single(x => x.ServiceId == 2) }
            };
            context.BookingSystems.Add(aBookingSystem);
            context.SaveChanges();
        }
    }
}
