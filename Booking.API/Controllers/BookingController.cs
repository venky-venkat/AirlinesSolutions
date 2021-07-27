using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Booking.BL.IBusiness;
using Booking.DL.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingBL _bookingBL;
        public BookingController(IBookingBL bookingBL)
        {
            _bookingBL = bookingBL;
        }
       
        [HttpGet]
        [Route("AllBookings")]
        public IActionResult Get()
        {
            var result = _bookingBL.GetallBookings();
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("GetBookingByPNR/{PNR}")]
        public  IActionResult GetByPNR(string PNR)
        {
            var res =  _bookingBL.GetBookingByPNR(PNR);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("GetBookingByID/{id}")]
        public IActionResult GetById(int id)
        {
            var res = _bookingBL.GetBookingById(id);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]

        [Route("GetBookingByUser/{username}")]
        public IActionResult GetByUsername(string username)
        {
            var res = _bookingBL.GetBookingsByUser(username);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Bookings booking)
        {
            var result = _bookingBL.AddBooking(booking);
            if (result != null)
            {
                return Created(HttpContext.Request.Scheme + ":/" + HttpContext.Request.Host + "/" + HttpContext.Request.Path + "/"
                    + booking.Id, booking);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("CancelbookingbyBookingID/{id}")]
        public IActionResult Delete(int id)
        {
            _bookingBL.CancelBookingByID(id);
            return Ok();
        }

        [HttpDelete]
        [Route("CancelbookingbyPNR/{PNR}")]
        public IActionResult Delete(string PNR)
        {
            _bookingBL.CancelBookingByPNR(PNR);
            return Ok();
        }

    }
}
