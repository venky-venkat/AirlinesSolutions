using AccountManagement.DL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.DL.DBContext
{
   public class AccountManagementDBContext : DbContext
    {
        public AccountManagementDBContext(DbContextOptions<AccountManagementDBContext> options) : base(options)
        {

        }

        public DbSet<Login> Login { get; set; }
    }
}
