using ChatAppBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatAppBackend;

public class ChatAppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ConversationEntity> Conversations { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<ConversationMember> ConversationMembers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // I suggest explicitly defining on-delete behavior for relationships
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

        modelBuilder.Entity<ConversationEntity>()
            .HasOne(c => c.LastMessage)
            .WithMany()
            .HasForeignKey(c => c.LastMessageId)
            .IsRequired(false);
    }
}