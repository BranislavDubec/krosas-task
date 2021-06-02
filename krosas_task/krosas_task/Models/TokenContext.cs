using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace krosas_task.Models
{
    public class TokenContext : DbContext
    {
        public TokenContext(DbContextOptions<TokenContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<zamestnanec> zamestnanec { get; set; }
        public DbSet<firma> firma { get; set; }
        public DbSet<divizia> divizia { get; set; }
        public DbSet<projekt> projekt { get; set; }
        public DbSet<oddelenie> oddelenie { get; set; }
    }
}
