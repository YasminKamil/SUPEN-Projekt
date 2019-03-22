﻿using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SUPEN_Projekt.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SUPEN_Projekt.Controllers
{
    public class BookingSystemApiController : ApiController{
       IUnitOfWork uw;

        public BookingSystemApiController(IUnitOfWork unitofwork) {
            uw = unitofwork;
        }

		[HttpGet]
		public IEnumerable<BookingSystem> Get() {

			IEnumerable<BookingSystem> list = uw.BookingSystems.GetAll();
			return list;
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
