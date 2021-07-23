using AccountManagement.DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.DL.IRepositories
{
    public interface ILoginRepository
    {
        public Login Authenticate(Login l);
    }
}
