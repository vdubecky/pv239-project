using ChatAppBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatAppBackend
{
    public class ChatAppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

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
        }
    }
}
