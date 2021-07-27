using Airline.BL.DTO;
using Airline.DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.BL.IBusiness
{
   public interface IAirlineBL
    {
        public List<AirlinesDTO> GetallAirlines();
        public AirlinesDTO GetAirlinesbyID(int id);
        public Airlines AddAirline(AirlinesDTO airline);
        public Airlines UpdateAirline(AirlinesDTO airline);
        public void DeleteAirline(int id);
        public Airlines Blockairline(int id);
    }
}
