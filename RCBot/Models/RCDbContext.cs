using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBot.Models
{
    class RCDbContext : DbContext
    {
      
        public DbSet<MemberProfile> members { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Environment.CurrentDirectory;
            Options.UseSqlite($@"Data Source={DbLocation}\Database.sqlite");
        }
    }
}