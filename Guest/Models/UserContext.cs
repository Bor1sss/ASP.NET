using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
namespace Guest.Models
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Users us = new Users { Name = "Admin", Password = "1234" };
                Users?.Add(us);
                Messages?.Add(new Messages { Message = "Hello World", User = us });
                SaveChanges();
            }
        }
    }
}
