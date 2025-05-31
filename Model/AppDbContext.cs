using Microsoft.EntityFrameworkCore;

namespace bloodDonationAppBackend.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<userProfile> userProfile { get; set; }
        public DbSet<Post> post { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
