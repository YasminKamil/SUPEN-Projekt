using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUPEN_Projekt.Models;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookingSystem
        public ActionResult Index()
        {
            return View(db.BookingSystems.ToList());
        }

        // GET: BookingSystem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSystem bookingSystem = db.BookingSystems.Find(id);
            if (bookingSystem == null)
            {
                return HttpNotFound();
            }
            return View(bookingSystem);
        }

        // GET: BookingSystem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingSystem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingSystemId,SystemName,SystemDescription,Email,PhoneNumber,Website,CompanyName,ContactEmail,ContactPhone,Address,Latitude,Longitude,PostalCode,City")] BookingSystem bookingSystem)
        {
            if (ModelState.IsValid)
            {
                db.BookingSystems.Add(bookingSystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingSystem);
        }

        // GET: BookingSystem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSystem bookingSystem = db.BookingSystems.Find(id);
            if (bookingSystem == null)
            {
                return HttpNotFound();
            }
            return View(bookingSystem);
        }

        // POST: BookingSystem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingSystemId,SystemName,SystemDescription,Email,PhoneNumber,Website,CompanyName,ContactEmail,ContactPhone,Address,Latitude,Longitude,PostalCode,City")] BookingSystem bookingSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingSystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingSystem);
        }

        // GET: BookingSystem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSystem bookingSystem = db.BookingSystems.Find(id);
            if (bookingSystem == null)
            {
                return HttpNotFound();
            }
            return View(bookingSystem);
        }

        // POST: BookingSystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingSystem bookingSystem = db.BookingSystems.Find(id);
            db.BookingSystems.Remove(bookingSystem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //selected booking system är det företag som vi utgår från. 
        private List<BookingSystem> getBookingSystemsInRange(BookingSystem inSelectedBookingSystem)
        {
            var companiesInSelectedCity = bookingSystems.Where(x => x.City.ToLower() == inSelectedBookingSystem.City.ToLower() && x.CompanyName != inSelectedBookingSystem.CompanyName);
            List<BookingSystem> companiesInRange = new List<BookingSystem>();
            foreach (var item in companiesInSelectedCity)
            {
                if (inDistance(inSelectedBookingSystem.Longitude, inSelectedBookingSystem.Latitude, item.Longitude, item.Latitude, 5000))
                {
                    companiesInRange.Add(item);
                    Console.WriteLine(item.CompanyName);
                }
            }
            return companiesInRange;
        }


        //beräknar distansen till andra spelare. Returnerar t/f beroende på om avståndet är ok.
        private bool inDistance(double companyALong, double companyALat, double companyBLong, double companyBLat, int maxDistance)
        {
            bool isCloseEnough = false;
            companyALat = companyALat / 180 * Math.PI;
            companyALong = companyALong / 180 * Math.PI;
            companyBLong = companyBLong / 180 * Math.PI;
            companyBLat = companyBLat / 180 * Math.PI;
            double distanceLatitude = (Math.Abs(companyALat - companyBLat)) / 2;
            double distanceLongitude = (Math.Abs(companyALong - companyBLong)) / 2;
            double x = Math.Sin(distanceLatitude) * Math.Sin(distanceLatitude) + Math.Cos(companyALat) * Math.Cos(companyBLat) * Math.Sin(distanceLongitude) * Math.Sin(distanceLatitude);
            double y = 2 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));
            y = y * 6371000;
            if (y <= maxDistance) isCloseEnough = true;
            return isCloseEnough;
        }
    }
}
