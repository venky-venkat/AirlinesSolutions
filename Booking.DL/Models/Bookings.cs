using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DL.Models
{
   public class Bookings 
    {
        public int Id { get; set; }
        public string AirlinesName { get; set; }
        public string FlightName { get; set; }
        public string PassengerName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public string SeatNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TakeoffTime { get; set; }
        public string Landingtime { get; set; }
        public string Bookingtime { get; set; }
        public int TotalCost { get; set; }
        public string UserName { get; set; }
        public string PNR { get; set; }
        public string Coupencode { get; set; }
        public int DiscountAmount { get; set; }
        public string FlightStatus { get; set; }
        public string BookingStatus { get; set; }
        public string JourneyStatus { get; set; }
        public string RefundStatus { get; set; }
    }
}
