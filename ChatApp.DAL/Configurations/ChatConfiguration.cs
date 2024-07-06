using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DAL
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder
                .ToTable("Chat");

            builder
                .HasKey(c => c.ChatId);

            builder
                .Property(c => c.ChatName)
                .IsRequired();

            builder
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedChats)
                .HasForeignKey(c => c.CreatorUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}