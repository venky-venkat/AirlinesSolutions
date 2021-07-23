using AccountManagement.BL.DTO;
using AccountManagement.BL.IBusinessLogic;
using AccountManagement.DL.IRepositories;
using AccountManagement.DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.BL.BusinessLogic
{
   public class LoginBusiness : ILoginBL
    {
        private readonly ILoginRepository _loginDL;
        public LoginBusiness(ILoginRepository loginRepository)
        {
            _loginDL = loginRepository;
        }

        public LoginDTO Authenticate(LoginDTO l)
        {
            Login login = new Login()
            {
                Password = l.Password,
                Username = l.Username
            };
            login = _loginDL.Authenticate(login);
            if (login != null)
            {
                LoginDTO loginDTO = new LoginDTO()
                {
                    Username = login.Username,
                    Role = login.Role,
                    Token = login.Token
                };
                return loginDTO;
            }
            return null;
        }
    }
}
