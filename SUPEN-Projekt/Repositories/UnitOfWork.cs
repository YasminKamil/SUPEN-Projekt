﻿using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Bookings = new BookingRepository(_context);
        }

        public IBookingRepository Bookings { get; private set; }

        //public IBookingRepository Booking => throw new NotImplementedException();

        public int Complete()
        {
            return _context.SaveChanges();
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            _context.Dispose();
            
        }
      

    }
}