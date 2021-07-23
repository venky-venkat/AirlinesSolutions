using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline.BL.IBusiness;
using Airline.DL.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightBL _flightBL;
        public FlightController(IFlightBL flightBL)
        {
            _flightBL = flightBL;
        }

        [HttpGet]
        [Route("ListAllFlights")]
        public IActionResult Get()
        {
           var result =  _flightBL.GetFlights();
            if(result == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("GetFlight/{id}")]
        public IActionResult Get(int id)
        {
            var result = _flightBL.GetFlightByID(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Invalid ID");
            }
        }

        [HttpGet]
        [Route("SearchFlights/{from}/{to}")]
        public IActionResult Get(string from, string to)
        {
            var result = _flightBL.GetFlightByFromandTo(from, to);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No flight found on this route");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Flight flight)
        {
            var result = _flightBL.Addflight(flight);
            if (result !=null)
            {
                return Created(HttpContext.Request.Scheme + ":/" + HttpContext.Request.Host + "/" +
                    HttpContext.Request.Path + "/" + flight.Id, result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Flight flight)
        {
            var result = _flightBL.UpdateFlight(flight);
            if (result == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut]
        [Route("CancelFlight/{id}")]
        public IActionResult PutCancelFlight(int id)
        {
            var result = _flightBL.CancelFlight(id);
            if (result == null)
            {
                return NoContent();
            }
            else
            {
                //RabbitMQ concept.....Produce flight status and it will be consume by Booking service
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "FlightStatus",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );

                        string flightstatus = result.Id.ToString() + "/" + result.Flightname + "/" + result.Status;
                        var body = Encoding.UTF8.GetBytes(flightstatus);
                        channel.BasicPublish(exchange: "",
                                            routingKey: "FlightStatus",
                                            basicProperties: null,
                                            body: body);
                    }
                }
                return Ok(result);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _flightBL.Deleteflight(id);
            return Ok();
        }
    }
}
