using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>().HasData(
            new Address { Id = 1, Street = "Street 1" },
            new Address { Id = 2, Street = "Street 2" });

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "User 1", AddressId = 1 },
            new User { Id = 2, Name = "User 2", AddressId = 1 },
            new User { Id = 3, Name = "User 3", AddressId = 2 });
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Address> Addresses { get; set; }
}