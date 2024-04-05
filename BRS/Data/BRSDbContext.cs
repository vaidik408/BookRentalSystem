using BRS.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BRS.Data
{
    public class BRSDbContext : DbContext 
    {
        public BRSDbContext(DbContextOptions<BRSDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }
        public DbSet<BookRental> BookRental {  get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<RentHistory> RentHistory { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
            .HasOne(u => u.Roles)          
            .WithMany(r => r.Users)        
            .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<BookStatus>()
             .HasOne(r => r.Books)
             .WithOne(u => u.BookStatus)
             .HasForeignKey<Books>(u => u.StatusId);

            modelBuilder.Entity<BookRental>()
             .HasOne(r => r.Books)
             .WithMany(u => u.BookRental)
             .HasForeignKey(u => u.BookId);

            modelBuilder.Entity<BookRental>()
                .HasOne(r => r.Users)
                .WithOne(u => u.BookRental)
                .HasForeignKey<BookRental>(u => u.UserId);

            modelBuilder.Entity<RentHistory>()
                .HasOne(r => r.BookRental)
                .WithMany(u => u.RentHistory)
                .HasForeignKey(u => u.RentId);

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string? connectionString = configuration.GetConnectionString("BRS");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync();
        }
    }

}

