using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPEN_Projekt.Controllers {
	public class ServiceController : Controller {
		UnitOfWork uw;

		public ServiceController(UnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		// GET: Service
		public ActionResult Index(int id, int systemId) {
			ViewModel2 vm2 = new ViewModel2();

			vm2.bookingSystem = uw.BookingSystems.GetBookingSystem(systemId);
			vm2.service = vm2.bookingSystem.Services.Single(x => x.ServiceId == id);//uw.Services.GetTheService(id);

			return View(vm2);

		}

		// GET: Service/Details/5
		public ActionResult Details(int id) {
			return View();
		}

		// GET: Service/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Service/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Service/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: Service/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Service/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Service/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}
	}
}
