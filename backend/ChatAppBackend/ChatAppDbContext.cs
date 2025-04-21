using ChatAppBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatAppBackend
{
    public class ChatAppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ChatAppDbContext(DbContextOptions options) : base(options) { }
    }
}
