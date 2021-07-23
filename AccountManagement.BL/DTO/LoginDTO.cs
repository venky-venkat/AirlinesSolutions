using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.BL.DTO
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
