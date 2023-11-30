using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HomeMade.Domain.Models;

namespace HomeMade.DataAccess.Context
{
    public class HomeMadeDbContext : DbContext
    {
        public HomeMadeDbContext(DbContextOptions<HomeMadeDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
