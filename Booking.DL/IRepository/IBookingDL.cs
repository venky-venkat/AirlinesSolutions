using Booking.DL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DL.IRepository
{
   public interface IBookingDL
    {
        public List<Bookings> GetallBookings();
        public Bookings GetBookingByPNR(string PNR);
        public Bookings GetBookingById(int id);
        public List<Bookings> GetBookingsByUser(string Username);
        public Bookings AddBooking(Bookings booking);
        public Bookings UpdateBooking(Bookings booking);
        public Bookings CancelBookingByID(int id);
        public Bookings CancelBookingByPNR(string pnr);
    }
}
