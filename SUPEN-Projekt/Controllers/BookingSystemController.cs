using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemController : Controller
    {
        
        IUnitOfWork uw;
        public BookingSystemController(IUnitOfWork unitofwork)
        {
            uw = unitofwork;
        }

        // GET: BookingSystem
        public ActionResult Index()
        {
           IEnumerable<BookingSystem> listbookingsys = uw.BookingSystems.GetAll();
           return View(listbookingsys);  
        }

        //GET: BookingSystem/RelevantBookingSystems/?BookingSystemId=1&serviceId=1
        public ActionResult RelevantBookingSystems(int? BookingSystemId, int? serviceId) {
            if (BookingSystemId == null || serviceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (uw.BookingSystems.Get(BookingSystemId) == null || uw.Services.Get(serviceId) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSystem selectedBookingSystem = uw.BookingSystems.Get(BookingSystemId);
            Service selectedService = uw.Services.Get(serviceId);
            List<BookingSystem> bookingSystemsInRange =  uw.BookingSystems.GetBookingSystemsInRange(selectedBookingSystem);
            List<BookingSystem> bookingSystemsInOtherBranches = uw.BookingSystems.GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
            List<BookingSystem> orderedByDistance = uw.BookingSystems.OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);

            return View(orderedByDistance);
        }

        // GET: BookingSystem/Details/5
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

        // GET: BookingSystem/Create
        public ActionResult Create(){
            return View();
        }

		[HttpPost]
		public async Task<ActionResult> Create(BookingSystem system) {
                     var url = "http://localhost:55341/api/post";

            if (await uw.BookingSystems.APIContact(url, system))
            {
                return RedirectToAction("Index");
            }
            return View(system);
		}

        // GET: BookingSystem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            if (id != null)
            {
                BookingSystem aBookingSystem = uw.BookingSystems.Get(id);
                var url = "http://localhost:55341/api/Delete";


                if (await uw.BookingSystems.APIContact(url, aBookingSystem))
                {
                    return RedirectToAction("Index");
                }


                return View(uw.BookingSystems.GetAll());

            }

return HttpNotFound();
       


            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //BookingSystem bookingSystem = uw.BookingSystems.Get(id);
            //if (bookingSystem == null)
            //{
            //    return HttpNotFound();
            //}
            
        }

        // POST: BookingSystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uw.BookingSystems.RemoveBookingSystem(id);
            uw.Complete();
            return RedirectToAction("Index");
        }
    }
}
