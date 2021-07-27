using System;
using System.Collections.Generic;
using System.Text;
using Airline.BL.DTO;
using Airline.BL.IBusiness;
using Airline.DL.IRepository;
using Airline.DL.Models;

namespace Airline.BL.Business
{
    public class AirlinesBL : IAirlineBL
    {
        private readonly IAirlineRepository _airlineDL;

        public AirlinesBL(IAirlineRepository airlineDL)
        {
            _airlineDL = airlineDL; ;
        }

        public Airlines AddAirline(AirlinesDTO airline)
        {
            Airlines al = new Airlines()
            {
                AirlineName = airline.AirlineName,
                Contactaddress = airline.Contactaddress,
                Contactnumber = airline.Contactnumber,
                Logopath = airline.Logopath
            };
            return _airlineDL.AddAirline(al);
        }

        public Airlines Blockairline(int id)
        {
            return _airlineDL.Blockairline(id);
        }

        public void DeleteAirline(int id)
        {
            _airlineDL.DeleteAirline(id);
        }

        public AirlinesDTO GetAirlinesbyID(int id)
        {
            var result = _airlineDL.GetAirlinesbyID(id);
            if (result != null)
            {
                AirlinesDTO airlineDTO = new AirlinesDTO()
                {
                    Id = result.Id,
                    AirlineName = result.AirlineName,
                    Contactaddress = result.Contactaddress,
                    Contactnumber = result.Contactnumber,
                    Logopath = result.Logopath,
                    Status = result.status
                };
                return airlineDTO;
            }
            else
            {
                return null;
            }

        }

        public List<AirlinesDTO> GetallAirlines()
        {
            var v = _airlineDL.GetallAirlines();
            var al = new List<AirlinesDTO>();

            foreach (var vv in v)
            {
                var a = new AirlinesDTO()
                {
                    Id = vv.Id,
                    AirlineName = vv.AirlineName,
                    Contactaddress = vv.Contactaddress,
                    Contactnumber = vv.Contactnumber,
                    Logopath = vv.Logopath,
                    Status = vv.status

                };
                al.Add(a);
            }
            return al;
        }

        public Airlines UpdateAirline(AirlinesDTO airline)
        {
            Airlines al = new Airlines()
            {
                AirlineName = airline.AirlineName,
                Contactaddress = airline.Contactaddress,
                Contactnumber = airline.Contactnumber,
                Logopath = airline.Logopath
            };
            return _airlineDL.UpdateAirline(al);
        }
    }
}
