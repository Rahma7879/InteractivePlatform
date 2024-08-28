using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractivePlatform.DAL.Model
{
   public class InteractivePlatformContext: DbContext
    {
        public InteractivePlatformContext()
        {
        }

        public InteractivePlatformContext(DbContextOptions<InteractivePlatformContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<FinanceRequest> FinanceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary key
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.ProfilePicture).HasMaxLength(1000);

                // Define the relationship with Role
                entity.HasOne(e => e.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(e => e.RoleId) 
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            // Configure the Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId); // Primary key
                entity.Property(e => e.RoleName).IsRequired().HasMaxLength(100).IsUnicode(false);

                // Optional: Seed initial data
                entity.HasData(
                    new Role { RoleId = 1, RoleName = "Admin" }
                    
                );
            });

            // Configure the FinanceRequest entity
            modelBuilder.Entity<FinanceRequest>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary key
                entity.Property(e => e.RequestNumber).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PaymentAmount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentPeriod).IsRequired();
                entity.Property(e => e.TotalProfit).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            });
        }
    }
}


