using Airline.BL.DTO;
using Airline.DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.BL.IBusiness
{
   public interface IFlightBL
    {
        public List<Flight> GetFlights();
        public FlightDTO GetFlightByID(int ID);
        public List<Flight> GetFlightByFromandTo(string from, string to);
        public List<Flight> GetFlightBydate(string from, string to, string ondate);
        public Flight Addflight(Flight flight);
        public Flight UpdateFlight(Flight flight);
        public void Deleteflight(int id);
        public Flight CancelFlight(int Id);
    }
}
