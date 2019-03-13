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
            //Bookings is a prop in IUnitOfWork
            IEnumerable<Booking> listbookings = _unitofwork.Bookings.GetAll();
            return View(listbookings);
        }

        public ActionResult Index2()
        {
            //BookingSystems is a prop in IUnitOfWork
            //Förut använde jag GetAllBookingSystems men bytte till GetALL()
            IEnumerable<BookingSystem> listbookingsys = _unitofwork.BookingSystems.GetAll();
            return View(listbookingsys);
        }
    }
}