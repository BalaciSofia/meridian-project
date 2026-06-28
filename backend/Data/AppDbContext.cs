using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<React> Reacts { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventParticipant> EventParticipants { get; set; }

        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<React>()
                .HasIndex(r => new { r.PostId, r.AccountId })
                .IsUnique();
        }
    }
}
