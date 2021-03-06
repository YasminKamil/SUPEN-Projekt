﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SUPEN_Projekt.Models {
	public class ApplicationDbContext : DbContext {
		public ApplicationDbContext() : base("BookingSystemDbContext") {
			this.Configuration.ProxyCreationEnabled = false;
		}
		public DbSet<BookingSystem> BookingSystems { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Service> Services { get; set; }
        public DbSet<BranchRelation> BranchRelations { get; set; }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> {
		protected override void Seed(ApplicationDbContext context) {
            //Lägger till Branscher viaAaddBranches metoden
            //Lägger till "branschnamn" så skapas en bransch.
			//Lägger även bilder till bilder för varje bransch med tillhörande bilder
            string folderPath = "$~/Content/Images/";
            List<string> branches = new List<string> { "Frisör"+folderPath+"frisör.png", "Besiktning"+folderPath+"bil2.png", "Café"+folderPath+"kaffe.png", "Fordonsuthyrning"+folderPath+"bil1.png", "Massör"+folderPath+"massage1.png", "Verkstad"+folderPath+"bil2.png",
			   "Idrottsförening"+folderPath+"sport.png", "Kontor"+folderPath+"rektangulärlogga.png", "Utbildning"+folderPath+"rektangulärlogga.png", "Restaurang"+folderPath+"mat.png", "Sjukvård"+folderPath+"rektangulärlogga.png", "Transport"+folderPath+"bil1.png", "Hotell"+folderPath+"hotell.png", "Media"+folderPath+"rektangulärlogga.png", "IT"+folderPath+"rektangulärlogga.png",
			   "Bank"+folderPath+"rektangulärlogga.png", "Bygg"+folderPath+"rektangulärlogga.png", "Konsultation"+folderPath+"rektangulärlogga.png", "Däck"+folderPath+"bil1.png", "Tatuering"+folderPath+"tatuering.png", "Sport"+folderPath+"gym.png" };
			AddBranches(context, branches);

			//Lägger till BookingSystems via metoden addBookingSystem, data skrivs in i ordningen nedan.
			//context(ApplicationDbContext), systemnamn(string), systembeskrivning(string), epost(string), tel.nr (string), 
			//webbadress(string), företagsnamn(string), kontakt epost(string), kontakt tel.(string), Address(string), 
			//Latitude(double), Longitude(double),postnummer(string), stad(string)
			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "ArtofHair@boka.se", "070 - 000 00 00", "boka.se/ArtofHair", "Art of Hair", "ArtofHair@boka.se",
				"070 - 123 56 78", "Fabriksgatan 13", 59.272207, 15.208576, "702 10", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "ArtofHair@boka.se", "070 - 000 00 00", "boka.se/bullvivan", "Bullvivan", "bullvivan@boka.se",
				"070 - 123 56 78", "Krykvärdsvägen 17", 59.27412, 15.2066, "702 84", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.", 
				 "Epiroc@boka.se", "070 - 000 00 00", "boka.se/Epiroc", "Epiroc", "Epiroc@boka.se",
			"070 - 123 56 78", "Tackjärnsgatan 8", 59.291713, 15.204345, "703 83", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.", 
				 "Frisorkompaniet@boka.se", "070 - 000 00 00", "boka.se/salongfinest", "Frisörkompaniet", "Frisörkompaniet@boka.se",
			"070 - 123 56 78", "Kristinagatan 10", 59.270042, 15.229628, "602 26", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Carspect@boka.se", "070 - 000 00 00", "boka.se/Carspect", "Carspect", "Carspect@boka.se",
			"070 - 123 56 78", "Bangårdsg. 5", 58.593966, 16.204253, "602 28", "Norrköping");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Sodermalmdack@boka.se", "070 - 000 00 00", "boka.se/Sodermalmdack", "Södermalm däck & bilrekond", "Sodermalmdack@boka.se",
			"070 - 123 56 78", "Rutger Fuchsgatan 4", 59.3079479, 18.0789683, "116 67", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Noir@boka.se", "070 - 000 00 00", "boka.se/Noir", "Noir", "Noir@boka.se",
			"070 - 123 56 78", "Regeringsgatan 80", 59.3378022, 18.0674249, "111 39", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "BodyFace@boka.se", "070 - 000 00 00", "boka.se/BodyFace", "BodyFace", "BodyFace@boka.se",
			"070 - 123 56 78", "Fredriksbergsgatan 6", 55.6066851, 13.0183526, "212 11", "Malmö");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Besikta@boka.se", "070 - 000 00 00", "boka.se/Besikta", "Besikta", "Besikta@boka.se",
			"070 - 123 56 78", "Källvattengatan 7", 55.60575, 13.07308, "212 23", "Malmö");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Bilexpo@boka.se", "070 - 000 00 00", "boka.se/Bilexpo", "Bilexpo", "Bilexpo@boka.se",
				"070 - 123 56 78", "Karosserigatan 9", 59.296808, 15.234905, "703 69", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Bilhusetiorebro@boka.se", "070 - 000 00 00", "boka.se/Bilhusetiorebro", "Bilhuset i Örebro", "Bilhusetiorebro@boka.se",
			"070 - 123 56 78", "Skjutbanevägen 4", 59.297731, 15.238027, "70369", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "LillanTennisklubb@boka.se", "070 - 000 00 00", "boka.se/LillanTennisklubb", "Lillån Tennisklubb", "LillanTennisklubb@boka.se",
			"070 - 123 56 78", "Kyrkvägen 2A", 59.322887, 15.227972, "703 75", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Lillavallen@boka.se", "070 - 000 00 00", "boka.se/Lillåvallen", "Lillåvallen", "Lillavallen@boka.se",
			"070 - 123 56 78", "Kyrkvägen 2A", 59.323670, 15.226550, "70375", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.", 
				 "CityHotel@boka.se", "070 - 000 00 00", "boka.se/CityHotel", "City Hotel", "CityHotel@boka.se",
			"070 - 123 56 78", "Kungsgatan 24", 59.268741, 15.212420, "702 24", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "FirstHotel@boka.se", "070 - 000 00 00", "boka.se/FirstHotel", "First Hotel", "FirstHotel@boka.se",
			"070 - 123 56 78", "Storgatan 24", 59.277223, 15.216221, "703 61", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "EliteArtwork@boka.se", "070 - 000 00 00", "boka.se/EliteArtwork", "Elite Artwork", "EliteArtwork@boka.se",
			"070 - 123 56 78", "Storgatan 20", 59.276952, 15.215910, "703 61", "Örebro");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Fitness24seven@boka.se", "070 - 000 00 00", "boka.se/Fitness24seven", "Fitness24seven", "Fitness24seven@boka.se",
			"070 - 123 56 78", "Blekingegatan 63", 59.310865, 18.076742, "111 62", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "Gildasrum@boka.se", "070 - 000 00 00", "boka.se/Gildasrum", "Gildas rum", "Gildasrum@boka.se",
			"070 - 123 56 78", "Skånegatan 80", 59.312875, 18.083208, "111 35", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "PrimeBurger@boka.se", "070 - 000 00 00", "boka.se/PrimeBurger", "Prime Burger", "PrimeBurger@boka.se",
			"070 - 123 56 78", "Folkungagatan 122", 59.315465, 18.086169, "116 30", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.", 
				 "MosebackeHostel@boka.se", "070 - 000 00 00", "boka.se/MosebackeHostel", "Mosebacke Hostel", "MosebackeHostel@boka.se",
			"070 - 123 56 78", "Blekingegatan 63", 59.317173, 18.076266, "111 20", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.", 
				 "HiltonSlussen@boka.se", "070 - 000 00 00", "boka.se/HiltonSlussen", "Hilton Slussen", "HiltonSlussen@boka.se",
			"070 - 123 56 78", "Guldgränd 8", 59.320569, 18.069208, "104 65", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "TatueringStockholm@boka.se", "070 - 000 00 00", "boka.se/TatueringStockholm", "Tatuering Stockholm", "TatueringStockholm@boka.se",
			"070 - 123 56 78", "Brännkyrkagatan 48", 59.319217, 18.059823, "118 22", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "CaféMariaberget@boka.se", "070 - 000 00 00", "boka.se/CaféMariaberget", "Café Mariaberget", "CaféMariaberget@boka.se",
			"070 - 123 56 78", "Bastugatan 19", 59.320178, 18.062243, "118 25", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.",
				 "GuldgrandHotelApartments@boka.se", "070 - 000 00 00", "boka.se/GuldgrandHotelApartments", "Guldgrand Hotel Apartments", "GuldgrandHotelApartments@boka.se",
				"070 - 123 56 78", "Guldgränd 5", 59.320200, 18.069490, "118 20", "Stockholm");

			AddBookingSystem(context, "boka.se", "Lorem ipsum dolor sit amet, consectetur " +
				"adipiscing elit. Fusce iaculis sapien lorem, vel hendrerit nisi interdum " +
				"hendrerit. Nullam auctor leo ut odio rutrum pellentesque.Phasellus gravida " +
				"id ex sed interdum. Donec fringilla aliquam diam interdum egestas. Morbi eu " +
				"nibh quis mi imperdiet congue id sit amet nisl. Proin mattis tempus dolor, " +
				"sit amet sodales lorem feugiat quis. Class aptent taciti sociosqu ad " +
				"litora torquent per conubia nostra, per inceptos himenaeos. Donec ex dui, " +
				"aliquet et elementum et, venenatis vitae est. Praesent vehicula, odio eu mollis " +
				"convallis, arcu urna mattis tellus, ut aliquet diam ipsum a purus. Phasellus in blandit " +
				 "massa.Aliquam in pharetra turpis.Ut et ornare ipsum, vel tempor arcu.Sed " +
				"semper tristique ante, sit amet facilisis quam feugiat vitae.Nunc vel orci porta, " +
				"ultricies massa vitae, tempus nunc.Nulla volutpat metus a nulla ultricies, eu feugiat " +
				"risus hendrerit.Curabitur consectetur ac risus commodo scelerisque.Sed ut ornare metus.Mauris " +
				"ac ornare eros. Proin in tristique sem, at hendrerit arcu.", 
				 "BestWesternCapital@boka.se", "070 - 000 00 00", "boka.se/BestWesternCapital", "Best Western Capital", "BestWesternCapital@boka.se",
			"070 - 123 56 78", "Stadsgården", 59.319575, 18.075197, "116 45", "Stockholm");

			context.SaveChanges();
			//Lägger till och skapar services via AddService metoden, data skrivs in i ordningen nedan.
			//context(ApplicationDbContext), servicenamn(string), tidsåtgång(int), pris(int), branschnamn(string), företagsnamn(string)
			AddService(context, "Fika", 45, 200, "Café", "Café Mariaberget");
			AddService(context, "Tatuering", 60, 1750, "Tatuering", "Tatuering Stockholm");
			AddService(context, "Standard double", 30, 70, "Hotell", "Hilton Slussen");
			AddService(context, "Superior double", 30, 70, "Hotell", "Hilton Slussen");
			AddService(context, "Standard", 30, 70, "Hotell", "Hilton Slussen");
			AddService(context, "Sovsal", 30, 70, "Hotell", "Mosebacke Hostel");
			AddService(context, "Boka bord", 60, 180, "Restaurang", "Prime Burger");
			AddService(context, "Fika", 60, 180, "Café", "Gildas rum");
			AddService(context, "Gympass", 60, 360, "Sport", "Fitness24seven");
			AddService(context, "Hyr fotbollsplanen", 60, 180, "Sport", "Lillåvallen");
			AddService(context, "Fotbollsträning med PT", 60, 180, "Sport", "Lillåvallen");
			AddService(context, "Hyr en bana", 60, 70, "Sport", "Lillån Tennisklubb");
			AddService(context, "Träna tennis med PT", 60, 70, "Sport", "Lillån Tennisklubb");
			AddService(context, "Service", 30, 1300, "Verkstad", "Bilexpo");
			AddService(context, "Felsökning", 60, 1700, "Verkstad", "Bilexpo");
			AddService(context, "Service", 35, 1260, "Verkstad", "Bilhuset i Örebro");
			AddService(context, "Hyr en bil", 35, 1260, "Fordonsuthyrning", "Bilhuset i Örebro");
			AddService(context, "Standard", 60, 1250, "Hotell", "City Hotel");
			AddService(context, "Svit", 60, 1250, "Hotell", "City Hotel");
			AddService(context, "Standard double", 60, 1250, "Hotell", "City Hotel");
			AddService(context, "Superior double", 60, 1250, "Hotell", "City Hotel");
			AddService(context, "Standard", 60, 1200, "Hotell", "First Hotel");
			AddService(context, "Familjerum", 60, 1200, "Hotell", "First Hotel");
			AddService(context, "Tatuering", 60, 800, "Tatuering", "Elite Artwork");
			AddService(context, "Klippning", 25, 100, "Frisör", "Art of Hair");
			AddService(context, "Färgning", 45, 200, "Frisör", "Art of Hair");
			AddService(context, "Fika", 45, 200, "Café", "Bullvivan");
			AddService(context, "Hyr en dumper", 45, 200, "Fordonsuthyrning", "Epiroc");
			AddService(context, "Klippning", 45, 200, "Frisör", "Frisörkompaniet");
			AddService(context, "Däckbyte", 45, 200, "Däck", "Carspect");
			AddService(context, "Däckbyte", 45, 200, "Däck", "Södermalm däck & bilrekond");
			AddService(context, "Färgning", 45, 450, "Frisör", "Noir");
			AddService(context, "Färgning", 45, 480, "Frisör", "BodyFace");
			AddService(context, "Klippning", 25, 450, "Frisör", "BodyFace");
			AddService(context, "Besiktning", 25, 380, "Besiktning", "Besikta");
			AddService(context, "Standard", 60, 1250, "Hotell", "Guldgrand Hotel Apartments");
			AddService(context, "Svit", 60, 1250, "Hotell", "Guldgrand Hotel Apartments");
			AddService(context, "Standard double", 60, 1250, "Hotell", "Guldgrand Hotel Apartments");
			AddService(context, "Superior double", 60, 1250, "Hotell", "Guldgrand Hotel Apartments");
			AddService(context, "Standard", 60, 1250, "Hotell", "Best Western Capital");
			AddService(context, "Svit", 60, 1250, "Hotell", "Best Western Capital");
			AddService(context, "Standard double", 60, 1250, "Hotell", "Best Western Capital");
			AddService(context, "Superior double", 60, 1250, "Hotell", "Best Western Capital");
			context.SaveChanges();
			base.Seed(context);
		}

		//Lägger till en service
		void AddService(ApplicationDbContext context, string inServiceName, int inDuration, int inPrice, string inBranchName, string cName) {
			Service service = new Service();
			service.ServiceId = context.Services.Count();
			service.ServiceName = inServiceName;
			service.Duration = inDuration;
			service.Price = inPrice;
			service.Branch = context.Branches.Single(x => x.BranchName == inBranchName);
			service.Bookings = GetBookings(context, inPrice, inDuration);
			context.Services.Add(service);
			context.SaveChanges();
			//Kollar så att den inte redan finns
			var bs = context.BookingSystems.SingleOrDefault(c => c.CompanyName == cName);
			var serv = bs.Services.SingleOrDefault(i => i.ServiceId == service.ServiceId);
			if (serv == null)
				bs.Services.Add(context.Services.Single(i => i.ServiceId == service.ServiceId));
			context.SaveChanges();
		}

		//Skapar en lista av bokningar och använder sig av SeedBookings för att skapa rätt antal under företagets öppettid
		List<Booking> GetBookings(ApplicationDbContext context, int inPrice, int inDuration) {
			List<Service> services = new List<Service>();
			Random randomNumber = new Random();
			int duration = inDuration;
			int price = inPrice; 
            int hoursOpen = 8; 
			List<Booking> listOfBookings = new List<Booking>();
			listOfBookings = SeedBokings(duration, price, hoursOpen);
			context.SaveChanges();
			return listOfBookings;
		}

		//Returnerar en lista på bokningar under öppentiden, skapar så många som möjligt under öppettiden.
		public List<Booking> SeedBokings(int duration, int price, int totalHoursOpen) {
			var bookings = new List<Booking>();
			decimal inHours = Convert.ToDecimal(duration) / Convert.ToDecimal(60);
			int iterations = (int)Math.Floor(Convert.ToDecimal(totalHoursOpen) / Convert.ToDecimal(inHours));
			DateTime previousEndTime = new DateTime();
			int i;
			for (i = 0; i < iterations; ++i) {
				var booking = new Booking();
				booking.Available = true;
				if (i == 0) booking.StartTime = DateTime.Today.AddHours(i + 8);
				else booking.StartTime = previousEndTime;

				previousEndTime = booking.EndTime = booking.StartTime.AddMinutes(duration);
				booking.Date = DateTime.Today;
			}
			return bookings;
		}

		//Skapar en brancsh för varje string i listan som skickas in i metoden
		void AddBranches(ApplicationDbContext context, List<string> inBranchList ){
			List<String> branchString = inBranchList;
			foreach (var item in branchString) {
				Branch aBranch = new Branch();
                aBranch.BranchName = item.Split('$')[0];
                aBranch.PictureUrl = item.Split('$')[1];
                context.Branches.Add(aBranch);
			}
			context.SaveChanges();
		}

		//Lägger till BookingSystem i databasen
		void AddBookingSystem(ApplicationDbContext context, string inSystemName, string inSystemDescription, string inEmail, string inPhoneNumber, string inWebsite, string inCompanyName, string inContactEmail, string inContactPhone, string inAddress, double inLatitude, double inLongitude, string inPostalCode, string inCity) {
			BookingSystem bookingsystem = new BookingSystem {
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
				Services = new List<Service>()
			};
			context.BookingSystems.Add(bookingsystem);
			context.SaveChanges();
		}
	}
}
