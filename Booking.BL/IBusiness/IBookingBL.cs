
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Booking.DL.Models;
namespace Booking.BL.IBusiness
{
   public interface IBookingBL
    {
        public List<Bookings> GetallBookings();
        public Bookings GetBookingByPNR(string PNR);
        public List<Bookings> GetBookingsByUser(string Username);
        public Bookings AddBooking(Bookings booking);
        public Bookings UpdateBooking(Bookings booking);
        public Bookings CancelBookingByID(int id);
        public Bookings CancelBookingByPNR(string pnr);
        public Bookings GetBookingById(int id);
    }
}
