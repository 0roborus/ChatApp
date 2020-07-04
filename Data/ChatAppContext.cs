using Microsoft.EntityFrameworkCore;
using ChatApp.Models;

namespace ChatApp.Data
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext (DbContextOptions<ChatAppContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Message { get; set; }

        public DbSet<Chat> Chat { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey( m => m.ChatId);
        }
    }
}