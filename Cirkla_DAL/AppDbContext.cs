using Cirkla_DAL.Events;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cirkla_DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPicture> ItemPictures { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStatusChange> ContractStatusChanges { get; set; }
        
        // Event handler for when a Contract entity changes
        public event EventHandler<EntityChangedEventArgs> EntityChanged;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .HasOne(i => i.Owner)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OwnerId);

            builder.Entity<Item>()
                .HasMany(i => i.Pictures)
                .WithOne(p => p.Item);

            builder.Entity<Contract>()
                .HasOne(c => c.Owner)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Contract>()
                .HasOne(c => c.Borrower)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }

        // Overrides SaveChanges to detect changes to Contract entities
        public override int SaveChanges()
        {
            // Make a list of all the entities that have changed
            var changedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in changedEntities)
            {
                if (entry.Entity is Contract)
                {
                    OnEntityChanged(entry);
                }
            }
            return base.SaveChanges();
        }


        // Raises the EntityChanged event when a Contract entity changes
        protected virtual void OnEntityChanged(EntityEntry entry)
        {
            var entity = entry.Entity as Contract;
            if (entity is not null)
            {
                // Raise an event with the entity details
                EntityChanged?.Invoke(this, new EntityChangedEventArgs(entity));
            }
        }
    }
}
