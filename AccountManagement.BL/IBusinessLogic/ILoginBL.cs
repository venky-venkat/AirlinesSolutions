using AccountManagement.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.BL.IBusinessLogic
{
    public interface ILoginBL
    {
        public LoginDTO Authenticate(LoginDTO l);
    }
}
