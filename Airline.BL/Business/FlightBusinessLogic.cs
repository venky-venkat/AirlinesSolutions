using System;
using System.Collections.Generic;
using System.Text;
using Airline.DL.IRepository;
using Airline.DL.Models;
using Airline.BL.IBusiness;

namespace Airline.BL.Business
{
    public class FlightBusinessLogic : IFlightBL
    {
        private readonly IFlight _flightDL;
        public FlightBusinessLogic(IFlight flight)
        {
            _flightDL = flight;
        }

        public Flight Addflight(Flight flight)
        {
            return _flightDL.Addflight(flight);
        }

        public Flight CancelFlight(int Id)
        {
            return _flightDL.CancelFlight(Id);
        }

        public void Deleteflight(int id)
        {
            _flightDL.Deleteflight(id);
        }

        public List<Flight> GetFlightBydate(string from, string to, string ondate)
        {
            return _flightDL.GetFlightBydate(from, to,ondate);
        }

        public List<Flight> GetFlightByFromandTo(string from, string to)
        {
            return _flightDL.GetFlightByFromandTo(from, to);
        }

        public Flight GetFlightByID(int ID)
        {
            return _flightDL.GetFlightByID(ID);
        }

        public List<Flight> GetFlights()
        {
            return _flightDL.GetFlights();
        }

        public Flight UpdateFlight(Flight flight)
        {
            return _flightDL.UpdateFlight(flight);
        }
    }
}
