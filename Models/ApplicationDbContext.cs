using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

     public DbSet<AccidentReport> AccidentReports { get; set; }
     public DbSet<User> Users { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          base.OnModelCreating(modelBuilder);

          // Define One-to-Many Relationship
          modelBuilder.Entity<AccidentReport>()
              .HasOne(a => a.User)
              .WithMany(u => u.AccidentReports)
              .HasForeignKey(a => a.UserId);
     }
}