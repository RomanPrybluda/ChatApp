using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DAL
{
    public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {

            builder
                .ToTable("UserChat");

            builder
                .HasKey(uc => new { uc.UserId, uc.ChatId });


            builder
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserId);

            builder
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ChatId);

            builder
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

        }
    }
}