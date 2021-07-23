using System;
using System.Collections.Generic;
using System.Text;
using Booking.BL.IBusiness;
using Booking.DL.Models;
using Booking.DL.IRepository;
using Booking.DL.StatusEnums;
using System.Threading.Tasks;

namespace Booking.BL.Business
{
    public class BookingBusiness : IBookingBL
    {
        private readonly IBookingDL _bookingDL;
        public BookingBusiness(IBookingDL bookingDL)
        {
            _bookingDL = bookingDL;
        }
        public Bookings AddBooking(Bookings booking)
        {
            booking.Bookingtime = DateTime.Now.ToString();
            booking.FlightStatus = Status.Ontime;
            booking.BookingStatus = Status.Confirmed;
            booking.JourneyStatus = Status.Notyetstarted;
            booking.RefundStatus = Status.na;
            return _bookingDL.AddBooking(booking);
        }

        public Bookings CancelBookingByID(int id)
        {
            return _bookingDL.CancelBookingByID(id);
        }

        public Bookings CancelBookingByPNR(string pnr)
        {
            return _bookingDL.CancelBookingByPNR(pnr);
        }

        public List<Bookings> GetallBookings()
        {
            return  _bookingDL.GetallBookings();
        }

        public  Bookings GetBookingByPNR(string PNR)
        {
            return  _bookingDL.GetBookingByPNR(PNR);
        }

        public  List<Bookings> GetBookingsByUser(string Username)
        {
            return  _bookingDL.GetBookingsByUser(Username);
        }

        public Bookings UpdateBooking(Bookings booking)
        {
            throw new NotImplementedException();
        }
    }
}
