using AccountManagement.DL.DBContext;
using AccountManagement.DL.IRepositories;
using AccountManagement.DL.Models;
using AccountManagement.DL.TokenValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountManagement.DL.Repositories
{
  public  class LoginRepository : ILoginRepository
    {
        private readonly AccountManagementDBContext _DB;
        public LoginRepository(AccountManagementDBContext accountManagementDBContext)
        {
            _DB = accountManagementDBContext;
        }

        public Login Authenticate(Login l)
        {
            Login ll = new Login();

            ll = _DB.Login.FirstOrDefault(x => x.Username == l.Username && x.Password == l.Password);
            if (ll != null)
            {
                TokenGenerator tokenGenerator = new TokenGenerator();
                ll.Token = tokenGenerator.TokenGenerate(ll.Username, ll.Role);
                return ll;
            }
            return null;
        }
    }
}
