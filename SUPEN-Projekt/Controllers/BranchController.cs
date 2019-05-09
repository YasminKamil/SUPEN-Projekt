using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers
{
    public class BranchController : Controller
    {



        //Returnerar det valda bokningsystemets tjänster
        public async Task<ActionResult> Branch(int inBranchId)
        {
            BranchViewModel bVM = null;
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:55341/api/GetBranch/" + inBranchId).Result;
            if (result.IsSuccessStatusCode)
            {
                bVM = await result.Content.ReadAsAsync<BranchViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(bVM);
        }

        //Returnerar det valda bokningsystemets tjänster
        public async Task<ActionResult> Branches()
        {
            BranchesViewModel bVM = null;
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:55341/api/GetBranches/").Result;
            if (result.IsSuccessStatusCode)
            {
                bVM = await result.Content.ReadAsAsync<BranchesViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(bVM);
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











    }
}