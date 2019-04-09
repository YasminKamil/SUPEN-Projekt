﻿using SUPEN_Projekt.Repositories;
using System.Web.Mvc;
using SUPEN_Projekt.Logic;
using System.Threading.Tasks;
using System.Net.Http;

namespace SUPEN_Projekt.Controllers {
	public class ServiceController : Controller {
		UnitOfWork uw;

		public ServiceController(UnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		public async Task<ActionResult> Index() {
			ServicesViewModel list = null;
			HttpClient client = new HttpClient();

			var result = client.GetAsync("http://localhost:55341/api/GetBookings").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsAsync<ServicesViewModel>();
			}

			return View(list);
		}

		// GET: Service
		public async Task<ActionResult> BookService(int inBookingSystemId, int inServiceId) {

			BookingSystemServiceBookingViewModel bsSBVM = null;
			HttpClient client = new HttpClient();

			var result = client.GetAsync("http://localhost:55341/api/GetService/" + inBookingSystemId + "/" + inServiceId).Result;

			if (result.IsSuccessStatusCode) {
				bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}

			return View(bsSBVM);
		}

		//// GET: Service/Edit/5
		//public ActionResult Edit(int id) {
		//	return View();
		//}

		//// POST: Service/Edit/5
		//[HttpPost]
		//public ActionResult Edit(int id, FormCollection collection) {
		//	try {
		//		// TODO: Add update logic here

		//		return RedirectToAction("Index");
		//	} catch {
		//		return View();
		//	}
		//}


	}
}
