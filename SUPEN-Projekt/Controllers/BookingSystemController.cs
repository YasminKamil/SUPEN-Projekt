using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemController : Controller
    {
        
        IUnitOfWork unitofwork;
        public BookingSystemController(IUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        // GET: BookingSystem
        public ActionResult Index()
        {
            IEnumerable<BookingSystem> listbookingsys = unitofwork.BookingSystems.GetAll();
            return View(listbookingsys);
            
        }

        // GET: BookingSystem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSystem bookingSystem = unitofwork.BookingSystems.Get(id);
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
                unitofwork.BookingSystems.Add(bookingSystem);
                unitofwork.Complete();

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
            BookingSystem bookingSystem = unitofwork.BookingSystems.Get(id);
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
                unitofwork.BookingSystems.EditBookingSystem(bookingSystem);
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
            BookingSystem bookingSystem = unitofwork.BookingSystems.Get(id);
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
            unitofwork.BookingSystems.RemoveBookingSystem(id);
            unitofwork.Complete();
            return RedirectToAction("Index");
        }
    }
}
