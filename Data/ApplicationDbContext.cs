using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CalorieTracker.Models;

namespace CalorieTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure DailyLog relationships
            builder.Entity<DailyLog>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DailyLog>()
                .HasOne(d => d.FoodItem)
                .WithMany(f => f.DailyLogs)
                .HasForeignKey(d => d.FoodItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
