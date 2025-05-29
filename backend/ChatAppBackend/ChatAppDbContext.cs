using ChatAppBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatAppBackend
{
    public class ChatAppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ConversationEntity> Conversations { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }

        public ChatAppDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserEntity>()
                .Property(p => p.ProfilePicture)
                .IsRequired(false);

            modelBuilder.Entity<ConversationMember>()
                .HasKey(cm => new { cm.UserId, cm.ConversationId });

            modelBuilder.Entity<ConversationMember>()
                .HasOne(cm => cm.ConversationEntity)
                .WithMany(c => c.Members)
                .HasForeignKey(cm => cm.ConversationId);

            modelBuilder.Entity<ConversationMember>()
                .HasOne(cm => cm.UserEntity)
                .WithMany(u => u.Conversations)
                .HasForeignKey(cm => cm.UserId);

            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.ConversationEntity)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.SenderEntity)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId);
        }
    }
}
