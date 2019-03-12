using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers
{
    public class RepoController : Controller
    {
        IBookingRepository _bookingRepos;
        IBookingSystemRepository _bookingSysRepos;

        // oop principle: depend on the abstraction not on the concrete classes
        public RepoController(IBookingRepository bookingRepos, IBookingSystemRepository bookingsysrepos)
        {
            _bookingRepos = bookingRepos;
            _bookingSysRepos = bookingsysrepos;
        }

        // GET: Repo
        public ActionResult Index()
        {
            IEnumerable<Booking> listbookings = _bookingRepos.GetAllBookings();
            return View(listbookings);
        }

        public ActionResult Index2()
        {
            IEnumerable<BookingSystem> listbookingsys = _bookingSysRepos.GetAllBookingSystems();
            return View(listbookingsys);
        }
    }
}