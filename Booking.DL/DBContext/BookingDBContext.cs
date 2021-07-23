using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Booking.DL.Models;
namespace Booking.DL.DBContext
{
   public class BookingDBContext:DbContext
    {
        
        public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options)
        {

        }
        public DbSet<Bookings> Bookings { get; set; }
        
    }
}
