using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers {
	public class BookingController : Controller {

		//Returnerar alla bokningar med ett api-anrop
		public async Task<ActionResult> Index() {
			BookingsViewModel list = null;
			HttpClient client = new HttpClient();
			var result = client.GetAsync("http://localhost:55341/api/GetBookings").Result;
			if (result.IsSuccessStatusCode) {
				list = await result.Content.ReadAsAsync<BookingsViewModel>();
			}

			return View(list);
		}

		//Returnerar information om bokningen
		public async Task<ActionResult> Details(int inBookingSystemId, int inServiceId) {

			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
			HttpClient client = new HttpClient();

			BookingSystemServiceBookingViewModel getSystem = null;
			HttpClient client1 = new HttpClient();

			var result1 = client1.GetAsync("http://localhost:55341/api/GetBookingSystem/" + inBookingSystemId).Result;
			if (result1.IsSuccessStatusCode) {
				getSystem = await result1.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			bsSBVM.bookingSystem = getSystem.bookingSystem;


			BookingSystemServiceBookingViewModel getService = null;
			HttpClient client2 = new HttpClient();

			var result2 = client2.GetAsync("http://localhost:55341/api/GetService/" + inBookingSystemId + "/" + inServiceId).Result;

			if (result2.IsSuccessStatusCode) {
				getService = await result2.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();

			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}

			bsSBVM.service = getService.service;


			BookingSystemServiceBookingViewModel getBookingWithMaxId = null;
			HttpClient client3 = new HttpClient();

			var result3 = client3.GetAsync("http://localhost:55341/api/GetBooking/GetMaxId").Result;
			if (result3.IsSuccessStatusCode) {
				getBookingWithMaxId = await result3.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}

			var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + getBookingWithMaxId.booking.BookingId).Result;

			if (result.IsSuccessStatusCode) {
				getBookingWithMaxId = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			bsSBVM.booking = getBookingWithMaxId.booking;

			return View(bsSBVM);
		}

		[HttpGet]
		public async Task<ActionResult> BookService(int inBookingSystemId, int inServiceId, string inStartTime, int? branchAId) {
			BookingSystemServiceBookingViewModel bsSBVM = new BookingSystemServiceBookingViewModel();
			HttpClient client = new HttpClient();
    

            var result = client.GetAsync("http://localhost:55341/api/GetBooking/" + inBookingSystemId + "/" + inServiceId).Result;

			if (result.IsSuccessStatusCode) {
				bsSBVM = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
			}
            bsSBVM.branchAId = branchAId ?? default(int);
        bsSBVM.startTime = Convert.ToDateTime(inStartTime);
			return View(bsSBVM);
		}
        [HttpPost]
        public async Task updateBranchRelationAsync(int inBranchA, int inBranchB)
        {
            object branchId = new { branchA = inBranchA, branchB = inBranchB };
            var url = "http://localhost:55341/api/UpdateBranchRelation/" + inBranchA.ToString() + "/" + inBranchB.ToString();
            await APIContact(url, branchId);
        }

        public async Task<bool> APIContact(string inUrl, Object inObject)
        {

            bool works = false;
            var url = inUrl;

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(inObject), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    works = true;
                }
            }

            return works;
        }
        [HttpPost, ActionName("BookService")]
		public async Task<ActionResult> BookServiceConfirmed(int inBookingSystemId, int inServiceId, BookingSystemServiceBookingViewModel model) {
			try {
				BookingSystemServiceBookingViewModel getService = new BookingSystemServiceBookingViewModel();
				HttpClient client = new HttpClient();

				var result = client.GetAsync("http://localhost:55341/api/GetService/" + inBookingSystemId + "/" + inServiceId).Result;

				if (result.IsSuccessStatusCode) {
					 getService = await result.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
					
				} else {
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
				}

				model.service = getService.service;




                BookingSystemServiceBookingViewModel getSystem = null;
				HttpClient client1 = new HttpClient();

				var result1 = client1.GetAsync("http://localhost:55341/api/GetBookingSystem/" + inBookingSystemId).Result;
				if (result1.IsSuccessStatusCode) {
					getSystem = await result1.Content.ReadAsAsync<BookingSystemServiceBookingViewModel>();
				} else {
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
				model.bookingSystem = getSystem.bookingSystem;

               

                //Om man nyligen har bokat en tjänst läggs det till ett klick på relationen eller så skapas den
                if (model.branchAId != 0 && model.branchAId != null)
                {                   
                    await updateBranchRelationAsync(model.branchAId ?? default(int), model.service.Branch.BranchId);
                }
  











                model.branchAId = model.service.Branch.BranchId;
				var url = "http://localhost:55341/api/PostBooking";
               
                if (await APIContact(url, model)) {
					return RedirectToAction("Details",
						new { inBookingSystemId, inServiceId});
				}
                
			} catch (DataException) {
				ModelState.AddModelError("", "Unable to save changes, please try again");
			}

		return View(model);
		}


		}
	}

