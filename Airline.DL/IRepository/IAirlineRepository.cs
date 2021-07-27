
using System;
using System.Collections.Generic;
using System.Text;
using Airline.DL.Models;
namespace Airline.DL.IRepository
{
   public interface IAirlineRepository
    {
        public List<Airlines> GetallAirlines();
        public Airlines GetAirlinesbyID(int id);
        public Airlines AddAirline(Airlines airline);
        public Airlines UpdateAirline(Airlines airline);
        public void DeleteAirline(int id);
        public Airlines Blockairline(int id);
    }
}
