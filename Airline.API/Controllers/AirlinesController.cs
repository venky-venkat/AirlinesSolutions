using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airline.BL.DTO;
using Airline.BL.IBusiness;
using Airline.DL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private readonly IAirlineBL _airlineBL;
        public AirlinesController(IAirlineBL airlineBL)
        {
            _airlineBL = airlineBL;
        }

        [HttpGet]
        [Route("ListAllAirlines")]
        public IActionResult Get()
        {
            var result = _airlineBL.GetallAirlines();
            if  (result.Count>0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("GetAirline/{id}")]
        public IActionResult Get(int id)
        {
            var result = _airlineBL.GetAirlinesbyID(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No result found");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AirlinesDTO airlineDTO)
        {
            var result = _airlineBL.AddAirline(airlineDTO);
            if(result == null)
            {
                return Conflict("Airline Already Exist with same name");
            }
            else
            {
               return Created("",result);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] AirlinesDTO airlineDTO)
        {
            var result =  _airlineBL.UpdateAirline(airlineDTO);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("update failed");
            }
        }

        [HttpPut]
        [Route("Blockairlines/{id}")]
        public IActionResult Put(int id)
        {
            var result = _airlineBL.Blockairline(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("update failed");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _airlineBL.DeleteAirline(id);
            return Ok();
        }
    }
}
