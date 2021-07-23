using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Airline.DL.DBContext;
using Airline.DL.IRepository;
using Airline.DL.Models;

namespace Airline.DL.Repositories
{
    public class AirlinesRepository : IAirlineRepository
    {
        public readonly AirlinesDBContext _DB;

        public AirlinesRepository(AirlinesDBContext airlines)
        {
            _DB = airlines;
        }

        public Airlines AddAirline(Airlines airline)
        {
            var result = _DB.Airlines.SingleOrDefault(b => b.AirlineName.ToLower() == airline.AirlineName.ToLower());
            if (result != null)
            {
                return null;
            }
            _DB.Airlines.Add(airline);
            _DB.SaveChanges();
            return airline;
        }

        public void DeleteAirline(int id)
        {
            var result = _DB.Airlines.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                _DB.Airlines.Remove(result);
            }
        }

        public Airlines GetAirlinesbyID(int id)
        {
            var result = _DB.Airlines.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<Airlines> GetallAirlines()
        {
            return _DB.Airlines.ToList();
        }

        public Airlines UpdateAirline(Airlines airline)
        {
            var result = _DB.Airlines.SingleOrDefault(b => b.Id == airline.Id);
            if (result != null)
            {
                result.AirlineName = airline.AirlineName;
                result.Contactaddress = airline.Contactaddress;
                result.Contactnumber = airline.Contactnumber;
                _DB.SaveChanges();
                return airline;
            }
            return null;
        }
    }
}
