using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Models;

namespace SignalR_Chat.Infrastructure
{
    public class ChatDbContext : DbContext 
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(50);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.SentAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Status).HasDefaultValue(MessageStatus.Sent);

                entity.HasOne(e => e.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(e => e.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(e => new { e.ConversationId, e.UserId });
                entity.Property(e => e.JoinedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Conversations)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Conversation)
                      .WithMany(c => c.Participants)
                      .HasForeignKey(e => e.ConversationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
