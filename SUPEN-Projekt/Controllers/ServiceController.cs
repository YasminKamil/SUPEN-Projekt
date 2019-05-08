﻿using SUPEN_Projekt.Repositories;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using SUPEN_Projekt.Logic.ViewModels;
using Newtonsoft.Json;
using System.Text;
using System;

namespace SUPEN_Projekt.Controllers {
	public class ServiceController : Controller {
		UnitOfWork uw;

		public ServiceController(UnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Returnerar alla tjänster via ett api-anrop
		public async Task<ActionResult> Index() {
			ServicesViewModel list = null;
			HttpClient client = new HttpClient();

			var result = client.GetAsync("http://localhost:55341/api/GetServices").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsAsync<ServicesViewModel>();
			}

			return View(list);
		}

		//Returnerar tjänster som möjliggör en bokning i ett bokningsystem 
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

        public async Task<ActionResult> ServiceSuggestion(int inBookingId, string inServiceName, string inBookingSystemName)
        {
            ServiceViewModel serviceViewModel = null;
            HttpClient client = new HttpClient();

            var result = client.GetAsync("http://localhost:55341/api/GetService/" +
                inBookingId + "/" + inServiceName + "/" + inBookingSystemName).Result;

            if (result.IsSuccessStatusCode)
            {
                serviceViewModel = await result.Content.ReadAsAsync<ServiceViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
            }



            return PartialView(serviceViewModel);
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
