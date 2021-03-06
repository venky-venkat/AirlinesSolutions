using Airline.DL.DBContext;
using Airline.DL.IRepository;
using Airline.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.DL.Repositories
{
   public class FlightRepository : IFlight
    {
        private readonly AirlinesDBContext _DB;
        public FlightRepository(AirlinesDBContext airlinesDBContext)
        {
            _DB = airlinesDBContext;
        }
        public Flight Addflight(Flight flight)
        {
            var result = _DB.Flights.FirstOrDefault(x => x.Flightname == flight.Flightname);
            if (result != null)
            {
                return null;
            }
            flight.Status = "ON TIME";
            _DB.Flights.Add(flight);
            _DB.SaveChanges();
            return flight;
        }

        public Flight CancelFlight(int Id)
        {
            var res = GetFlightByID(Id);
            if (res != null)
            {
                res.Status = "CANCEL";
                _DB.SaveChanges();
                return res;
            }

            return null;
        }

        public void Deleteflight(int id)
        {
            var result = GetFlightByID(id);
            if (result != null)
            {
                _DB.Flights.Remove(result);
            }
        }

        public List<Flight> GetFlightByFromandTo(string froms, string to)
        {

            var result = (from f in _DB.Flights
                          join a in _DB.Airlines
                          on f.AirlineID equals a.Id
                          where a.status != "BLOCKED"
                          where f.From == froms
                          where f.To == to
                          select f).ToList();

            //var result = _DB.Flights.Where(x => x.From == from && x.To == to).ToList<Flight>();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<Flight> GetFlightBydate(string from, string to,string ondate)
        {
            var result = _DB.Flights.Where(x => x.From == from && x.To == to).ToList<Flight>();
            var rr = new List<Flight>();
            if (result != null)
            {
                foreach(var v in result)
                {
                    var dt = v.Takeoff.Split("T");
                    if (ondate == dt[0])
                    {
                        rr.Add(v);
                    }
                }
                return rr;
            }
            return null;
        }

        public Flight GetFlightByID(int ID)
        {
            return _DB.Flights.FirstOrDefault(x => x.Id == ID);
        }

        public string GetAirlinename(int ID)
        {
            return _DB.Airlines.FirstOrDefault(x => x.Id == ID).AirlineName;
        }

        public List<Flight> GetFlights()
        {
            //var result = _DB.Flights.ToList<Flight>();

            var result = (from f in _DB.Flights join a in _DB.Airlines
                     on f.AirlineID equals a.Id where a.status != "BLOCKED" select f).ToList();

            return result;
        }

        public Flight UpdateFlight(Flight flight)
        {

            var res = GetFlightByID(flight.Id);
            if (res != null)
            {
                res.AirlineID = flight.AirlineID;
                res.Flightname = flight.Flightname;
                res.From = flight.From;
                res.Landing = flight.Landing;
                res.Meals = flight.Meals;
                res.Numberofbusinessseats = flight.Numberofbusinessseats;
                res.Numberofnonbusinessseats = flight.Numberofnonbusinessseats;
                res.Numberofrows = flight.Numberofrows;
                res.Scheduling = flight.Scheduling;
                res.Takeoff = flight.Takeoff;
                res.To = flight.To;
                res.Totalcost = flight.Totalcost;
                res.Discountamount = flight.Discountamount;
                res.DiscountCode = flight.DiscountCode;
                _DB.SaveChanges();
                return flight;
            }

            return null;
        }
    }
}
