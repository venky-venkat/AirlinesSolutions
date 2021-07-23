using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Airline.DL.Models;
namespace Airline.DL.DBContext
{
    public class AirlinesDBContext : DbContext
    {
        public AirlinesDBContext(DbContextOptions<AirlinesDBContext> options) : base(options)
        {

        }
        public DbSet<Airlines> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }

    }
}
