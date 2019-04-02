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
        public async Task<ActionResult> Index()
        {
            string list = "";
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:55341/api/GetStr").Result;
            if (result.IsSuccessStatusCode)
            {
                list = await result.Content.ReadAsStringAsync();
            }
            List<BookingSystem> objects = JsonConvert.DeserializeObject<List<BookingSystem>>(list);
            return View(objects);
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

        // GET: BookingSystem/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BookingSystem system)
        {
            var url = "http://localhost:55341/api/post";

            if (await APIContact(url, system))
            {
                return RedirectToAction("Index");
            }
            return View(system);

        }

        //GET: BookingSystem/RelevantBookingSystems/?BookingSystemId=1&serviceId=1
        public async Task<ActionResult> RelevantBookingSystems(int bookingSystemId, int serviceId)
        {

            string list1 = "";
            HttpClient client1 = new HttpClient();
            string url1 = "http://localhost:55341/api/GetBookingSystem/" + bookingSystemId.ToString();
            var result1 = client1.GetAsync(url1).Result;
            if (result1.IsSuccessStatusCode)
            {
                list1 = await result1.Content.ReadAsStringAsync();
            }
            BookingSystem selectedBookingSystem = JsonConvert.DeserializeObject<BookingSystem>(list1);


            if (selectedBookingSystem == null || !selectedBookingSystem.Services.Any(x=> x.ServiceId == serviceId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

       List<BookingSystem> orderedByDistance = new List<BookingSystem>();
            
            string list = "";
            HttpClient client = new HttpClient();
            string url = "http://localhost:55341/api/getRelevant/"+bookingSystemId.ToString()+"/"+ serviceId.ToString() ;
            var result = client.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
            {
                list = await result.Content.ReadAsStringAsync();
            }
            orderedByDistance = JsonConvert.DeserializeObject<List<BookingSystem>>(list);
            
            ViewModel5 vm5 = new ViewModel5();
            List<BookingSystemAndDistance> listOfBookingSystems = new List<BookingSystemAndDistance>();
            foreach (var item in orderedByDistance)
            {
                BookingSystemAndDistance pairedObject = new BookingSystemAndDistance();
                pairedObject.BookingSystem = item;
                pairedObject.Distance =Math.Round(uw.BookingSystems.GetDistanceTo(uw.BookingSystems.GetTheBookingSystem(bookingSystemId), item));
                listOfBookingSystems.Add(pairedObject);
            
            }
            vm5.SelectedBookingSystem = selectedBookingSystem;
            vm5.BookingsWithDistance = listOfBookingSystems;
            return PartialView(vm5);
        }
 

    }


}
