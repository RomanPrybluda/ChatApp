using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL
{
    public class ChatAppDbContext : DbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<UserChat> UserChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            new ChatConfiguration().Configure(modelBuilder.Entity<Chat>());

            new UserChatConfiguration().Configure(modelBuilder.Entity<UserChat>());

            base.OnModelCreating(modelBuilder);

        }
    }
}