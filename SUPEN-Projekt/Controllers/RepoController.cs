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
        IUnitOfWork _unitofwork;
        public RepoController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        // GET: Repo
        public ActionResult Index()
        {
            IEnumerable<Booking> listbookings = _unitofwork.Bookings.GetAllBookings();
            return View(listbookings);
        }

        public ActionResult Index2()
        {
            IEnumerable<BookingSystem> listbookingsys = _unitofwork.BookingSystems.GetAllBookingSystems();
            return View(listbookingsys);
        }
    }
}