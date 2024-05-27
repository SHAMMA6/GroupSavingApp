using DomainLayer.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructurlayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GroupSavingPlan> GroupSavingPlans { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // UserProfile entity configuration
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfile>(up => up.UserId);

            // GroupSavingPlan entity configuration
            modelBuilder.Entity<GroupSavingPlan>()
                .HasOne(gsp => gsp.CreatedByUser)
                .WithMany(u => u.GroupSavingPlans)
                .HasForeignKey(gsp => gsp.CreatedByUserId);

            modelBuilder.Entity<GroupSavingPlan>()
                .HasMany(gsp => gsp.Participants)
                .WithMany(u => u.GroupSavingPlans)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroupSavingPlan",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<GroupSavingPlan>()
                        .WithMany()
                        .HasForeignKey("GroupSavingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // Contribution entity configuration
            modelBuilder.Entity<Contribution>()
                .HasOne(c => c.GroupSavingPlan)
                .WithMany(gsp => gsp.Contributions)
                .HasForeignKey(c => c.GroupSavingPlanId);

            modelBuilder.Entity<Contribution>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contributions)
                .HasForeignKey(c => c.UserId);

            // Notification entity configuration
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
        }


    }
}
