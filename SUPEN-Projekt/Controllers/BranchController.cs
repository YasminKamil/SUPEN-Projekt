using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers {
	public class BranchController : Controller {

		//Returnerar en branch
		public async Task<ActionResult> Branch(int inBranchId) {
			BranchViewModel bVM = null;
			HttpClient client = new HttpClient();

			//Gör ett api-anrop för att hämta en bransch
			var result = client.GetAsync("http://localhost:55341/api/GetBranch/" + inBranchId).Result;
			if (result.IsSuccessStatusCode) {
				bVM = await result.Content.ReadAsAsync<BranchViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			return View(bVM);
		}

		//Returnerar flera branscher 
		public async Task<ActionResult> Branches() {
			BranchesViewModel bVM = null;
			HttpClient client = new HttpClient();

			//Gör ett api-anrop för att hämta flera branscher
			var result = client.GetAsync("http://localhost:55341/api/GetBranches/").Result;
			if (result.IsSuccessStatusCode) {
				bVM = await result.Content.ReadAsAsync<BranchesViewModel>();
			} else {
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			return View(bVM);
		}

		/*Metoden används för att göra interna metodanrop, som tar in url:en som används för att kontrollera url:en som används
		  för att göra api-anrop och objekten som ska tas in i api-anropet*/
		public async Task<bool> APIContact(string inUrl, Object inObject) {
			bool works = false;
			var url = inUrl;
			using (var client = new HttpClient()) {

				//Serialiserar objekten som tas in till JSON-objekt
				var content = new StringContent(JsonConvert.SerializeObject(inObject), Encoding.UTF8, "application/json");
				var result = await client.PostAsync(url, content);
				if (result.IsSuccessStatusCode) {
					works = true;
				}
			}
			return works;
		}
	}
}