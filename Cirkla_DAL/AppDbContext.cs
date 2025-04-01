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
        public DbSet<CircleJoinRequest> CircleJoinRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .HasMany(i => i.Pictures)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Item>()
                .HasOne(i => i.Owner)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Contract>()
                .HasOne(c => c.Owner)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); //TODO: Change or handle delete behaviour for Contracts and Users. Softdelete Contracts or maybe some kind of flat object archive?

            builder.Entity<Contract>()
                .HasOne(c => c.Borrower)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Contract>()
                .HasMany(c => c.StatusChanges)
                .WithOne(csc => csc.Contract)
                .HasForeignKey(csc => csc.ContractId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Circle>()
                .HasOne(c => c.CreatedBy)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Circle>()
                .HasOne(c => c.UpdatedBy)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull); // Sets to null if the user is deleted

            // Configure many-to-many for Administrators. Cascade only deletes the join table records, not the related entities.
            builder.Entity<Circle>()
                .HasMany(c => c.Administrators)
                .WithMany(u => u.AdministeredCircles)
                .UsingEntity<Dictionary<string, object>>(
                    "CircleAdministrators",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Circle>()
                        .WithMany()
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // Configure many-to-many for Members. Cascade only deletes the join table records, not the related entities.
            builder.Entity<Circle>()
                .HasMany(c => c.Members)
                .WithMany(u => u.MemberCircles)
                .UsingEntity<Dictionary<string, object>>(
                    "CircleMembers",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Circle>()
                        .WithMany()
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            builder.Entity<CircleJoinRequest>()
                .HasOne(cr => cr.FromUser)
                .WithMany()
                .HasForeignKey("FromUserId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CircleJoinRequest>()
                .HasOne(cr => cr.TargetUser)
                .WithMany()
                .HasForeignKey("TargetUserId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CircleJoinRequest>()
                .HasOne(cr => cr.UpdatedByUser)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("UpdatedByUserId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CircleJoinRequest>()
                .HasOne(cr => cr.Circle)
                .WithMany()
                .HasForeignKey("CircleId")
                .OnDelete(DeleteBehavior.NoAction);

            // Update the foreign key constraint for ContractStatusChanges
            builder.Entity<ContractStatusChange>()
                .HasOne(csc => csc.Contract)
                .WithMany(c => c.StatusChanges)
                .HasForeignKey("ContractId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ContractStatusChange>()
                .HasOne(csc => csc.ChangedBy)
                .WithMany()
                .HasForeignKey("ChangedById")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}