using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
            }
        }
    }
}
