using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers
{
    public class RepoController : Controller
    {
        IUnitOfWork uw;
        public RepoController(IUnitOfWork unitofwork)
        {
            uw = unitofwork;
        }

        // GET: Repo
        public ActionResult Index()
        {
            //Bookings is a prop in IUnitOfWork
            IEnumerable<Booking> listbookings = uw.Bookings.GetAll();
            return View(listbookings);
        }

        public ActionResult Index2()
        {
            //BookingSystems is a prop in IUnitOfWork
            //Förut använde jag GetAllBookingSystems men bytte till GetALL()
            IEnumerable<BookingSystem> listbookingsys = uw.BookingSystems.GetAll();
            return View(listbookingsys);
        }

        [Route("BookingSystem/{id:int}")]
        public ActionResult Index3(int id)
        {
            BookingSystem bookingSystem = uw.BookingSystems.Get(id);
            ViewModel2 viewModel2 = new ViewModel2();
            viewModel2.bookingSystem = uw.BookingSystems.Get(id);
            viewModel2.services = uw.Services.GetAll();
            ViewBag.Message = bookingSystem.CompanyName;
            //var tupleModel = new Tuple<BookingSystem, IEnumerable<Service>>(bookingSystem, uw.Services.GetAll());
            return View(viewModel2);//bookingSystem
        }

        public PartialViewResult Index4()
        {
           
                ViewBag.Message = "Welcome to my demo!";
            
                var tupleModel = new Tuple<IEnumerable<BookingSystem>, IEnumerable<Service>>(uw.BookingSystems.GetAll(), uw.Services.GetAll());
                return PartialView(tupleModel);
            
            //IEnumerable<Service> list = uw.Services.GetAll();
            //return PartialView(list);
        }

        public ActionResult Index5()
        {

            IEnumerable<Branch> list = uw.Branches.GetAll();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult CreateBooking(int? systemid, int? serviceid)
        {
            BookingSystem s = uw.BookingSystems.Get(systemid);
            Service service = uw.Services.Get(serviceid);
            uw.Bookings.CreateBooking(s, service);
            uw.Complete();

            
            var booking = uw.Bookings.Find(x => x.BookingSystemId == s.BookingSystemId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId, UserName, UserMail, UserMobile, " +
            "Subject, StartTime, EndTime, Price," +
            "BookingSystemId, ServiceId")]Booking booking)
        {
            //booking.BookingSysId


            if (ModelState.IsValid)
            {
                
                uw.Bookings.Add(booking);
                uw.BookingSystems.AddBooking(booking, booking.BookingSystemId);
                uw.Complete();

                return RedirectToAction("Index4");
            }

            return View("booking");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSystem bookingSystem = uw.BookingSystems.Get(id);

            if (bookingSystem == null)
            {
                return HttpNotFound();
            }
            return View(bookingSystem);
        }

       



    }
}