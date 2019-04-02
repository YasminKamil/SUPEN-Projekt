using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SUPEN_Projekt.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Data.Entity;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemApiController : ApiController{
       IUnitOfWork uw;

        public BookingSystemApiController(IUnitOfWork unitofwork) {
            uw = unitofwork;
        }
       
        [HttpGet]
        public IEnumerable<BookingSystem> Get()
        {

            IEnumerable<BookingSystem> list = uw.BookingSystems.GetAll();
            return list;
        }
        [Route("api/getstr")]
        [HttpGet]
        public IEnumerable<BookingSystem> GetStr()
        {

          IEnumerable<BookingSystem> list = uw.BookingSystems.GetAll();
                  //   var a=  JsonConvert.SerializeObject(list);
           
            return list;
        }
        [Route("api/getRelevant/{bookingSystemId:int}/{serviceId:int}")]
        [HttpGet]
        public IEnumerable<BookingSystem> GetRelevant(int bookingSystemId, int serviceId)
        {
            try
            {

            BookingSystem selectedBookingSystem = uw.BookingSystems.GetTheBookingSystem(bookingSystemId);
            Service selectedService = uw.Services.Get(serviceId);
            List<BookingSystem> bookingSystemsInRange = uw.BookingSystems.GetBookingSystemsInRange(selectedBookingSystem);
            List<BookingSystem> bookingSystemsInOtherBranches = uw.BookingSystems.GetBookingSystemsInOtherBranches(bookingSystemsInRange, selectedService);
            List<BookingSystem> orderedByDistance = uw.BookingSystems.OrderByDistance(bookingSystemsInOtherBranches, selectedBookingSystem);

                
            return orderedByDistance;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/post")]
		public IHttpActionResult Post(JObject insystem) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}

            BookingSystem system = JsonConvert.DeserializeObject<BookingSystem>(insystem.ToString());

            uw.BookingSystems.Add(new BookingSystem() {
				BookingSystemId = system.BookingSystemId,
				Address = system.Address,
				City = system.City,
				CompanyName = system.CompanyName,
				ContactEmail = system.ContactEmail,
				ContactPhone = system.ContactPhone,
				Email = system.Email,
				Latitude = system.Latitude,
				Longitude = system.Longitude,
				PhoneNumber = system.PhoneNumber,
				PostalCode = system.PostalCode,
				SystemDescription = system.SystemDescription,
				SystemName = system.SystemName,
				Website = system.Website
		});
			uw.Complete();
			return Ok();
		}
    }
}
