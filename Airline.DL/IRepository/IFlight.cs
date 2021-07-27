using Airline.DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.DL.IRepository
{
   public interface IFlight
    {
        public List<Flight> GetFlights();
        public Flight GetFlightByID(int ID);
        public List<Flight> GetFlightByFromandTo(string from, string to);
        public List<Flight> GetFlightBydate(string from, string to, string ondate);
        public Flight Addflight(Flight flight);
        public Flight UpdateFlight(Flight flight);
        public void Deleteflight(int id);
        public Flight CancelFlight(int Id);
    }
}
