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
            ViewBag.Message = bookingSystem.CompanyName;
            return View(bookingSystem);
        }

        public ActionResult Index4()
        {
           
            IEnumerable<Service> list = uw.Services.GetAll();
            return View(list);
        }

        public ActionResult Index5()
        {

            IEnumerable<Branch> list = uw.Branches.GetAll();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId, UserName, UserMail, UserMobile, " +
            "Subject, StartTime, EndTime, Price," +
            "BookingSysId")]Booking booking)
        {
            //booking.BookingSysId


            if (ModelState.IsValid)
            {
                uw.Bookings.Add(booking);
                uw.BookingSystems.AddBooking(booking, booking.BookingSysId);
                uw.Complete();

                return RedirectToAction("Detail");
            }

            return View("Detail");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = uw.Bookings.Get(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }


    }
}