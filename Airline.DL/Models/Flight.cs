using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.DL.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int AirlineID { get; set; }
        public string Flightname { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Takeoff { get; set; }
        public string Landing { get; set; }
        public int Numberofbusinessseats { get; set; }
        public int Numberofnonbusinessseats { get; set; }
        public int Numberofrows { get; set; }
        public string Scheduling { get; set; }
        public int Totalcost { get; set; }
        public string DiscountCode { get; set; }
        public int Discountamount { get; set; }
        public string Meals { get; set; }
        public string Status { get; set; }
    }
}
