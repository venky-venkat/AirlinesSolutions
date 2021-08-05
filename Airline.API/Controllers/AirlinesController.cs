using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Airline.BL.DTO;
using Airline.BL.IBusiness;
using Airline.DL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AirlinesController : ControllerBase
    {
        private readonly IAirlineBL _airlineBL;
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public AirlinesController(IAirlineBL airlineBL, IHostingEnvironment hostingEnvironment)
        {
            _airlineBL = airlineBL;
            _hostingEnvironment = hostingEnvironment;
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
        [Obsolete]
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

        [HttpPost]
        [Route("Uploadlogo")]
        [RequestSizeLimit(500 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public IActionResult PostUploadImage()
        {
            string imageName = null;
            var httpRequest = HttpContext.Request;
            //Upload Image
            var postedFile = httpRequest.Form.Files["Image"];
            var id = httpRequest.Form["id"];
            //Create custom filename
            if (postedFile != null)
            {
                //imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + id + Path.GetExtension(postedFile.FileName);
                var filePath = ("F:/CTS Training/VKAirlines/VKairlines/src/assets/images/" + imageName);

                if (postedFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                         postedFile.CopyToAsync(stream);
                    }
                }

                return Ok();
            }
            return NoContent();
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
