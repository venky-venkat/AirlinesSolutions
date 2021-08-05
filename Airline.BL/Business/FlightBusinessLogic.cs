using System;
using System.Collections.Generic;
using System.Text;
using Airline.DL.IRepository;
using Airline.DL.Models;
using Airline.BL.IBusiness;
using Airline.BL.DTO;

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

        public FlightDTO GetFlightByID(int ID)
        {
           
            var flight = _flightDL.GetFlightByID(ID);
            var airlinename = _flightDL.GetAirlinename(flight.AirlineID);
            FlightDTO flightDTO = new FlightDTO
            {
                Id = flight.Id,
                AirlineID = flight.AirlineID,
                AirlineName = airlinename,
                Discountamount = flight.Discountamount,
                DiscountCode = flight.DiscountCode,
                Flightname = flight.Flightname,
                From = flight.From,
                Landing = flight.Landing,
                Meals = flight.Meals,
                Numberofbusinessseats = flight.Numberofbusinessseats,
                Numberofnonbusinessseats = flight.Numberofnonbusinessseats,
                Numberofrows = flight.Numberofrows,
                Scheduling = flight.Scheduling,
                Status = flight.Status,
                Takeoff = flight.Takeoff,
                To = flight.To,
                Totalcost = flight.Totalcost
            };
            return flightDTO;
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
