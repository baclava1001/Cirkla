using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Emit;

namespace Cirkla_DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPicture> ItemPictures { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStatusChange> ContractStatusChanges { get; set; }
        public DbSet<ContractNotification> ContractNotifications { get; set; }
        public DbSet<Circle> Circles { get; set; }
        public DbSet<CircleRequest> CircleRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .HasOne(i => i.Owner)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Item>()
                .HasMany(i => i.Pictures)
                .WithOne(p => p.Item)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Contract>()
                .HasOne(c => c.Owner)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Contract>()
                .HasOne(c => c.Borrower)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);


            // Configure the relationship between Circle and User for Administrators
            builder.Entity<Circle>()
                .HasMany(c => c.Administrators)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "CircleAdministrator",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientCascade),
                    j => j.HasOne<Circle>().WithMany().HasForeignKey("CircleId").OnDelete(DeleteBehavior.ClientCascade));

            // Configure the relationship between Circle and User for Members
            builder.Entity<Circle>()
                .HasMany(c => c.Members)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "CircleMember",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientCascade),
                    j => j.HasOne<Circle>().WithMany().HasForeignKey("CircleId").OnDelete(DeleteBehavior.ClientCascade));

            // Configure the relationship between Circle and User for CreatedBy
            builder.Entity<Circle>()
                .HasOne(c => c.CreatedBy)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configure the relationship between Circle and User for UpdatedBy
            builder.Entity<Circle>()
                .HasOne(c => c.UpdatedBy)
                .WithMany()
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}