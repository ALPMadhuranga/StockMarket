using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // ApplicationDBContext is gient class that is going to allow us search our individual tables in the database 
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)  // Constructor for the ApplicationDBContext class
        : base(dbContextOptions) // Calling the base class constructor
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
    }
}