using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.BL.DTO;
using AccountManagement.BL.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBL _login;
        public LoginController(ILoginBL loginBL)
        {
            _login = loginBL;
        }
        [HttpGet]
        public IActionResult GetAuthenticate(string username, string password)
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Password = password,
                Username = username
            };

            loginDTO = _login.Authenticate(loginDTO);
            if (loginDTO != null)
            {
                return Ok(loginDTO);
            }
            else
            {
                return NotFound("Login Failed");
            }

        }
    }
}
