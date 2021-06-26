using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    [IsProjected]
    public int AddressId { get; set; }

    public string? Temp { get; set; }

    public Address? Address { get; set; }

    // this works even if AddressId is not selected
    public int GetProjectedAddressId([Parent] User parent) => parent.AddressId;
}

public class Address
{
    public int Id { get; set; }

    public string Street { get; set; }
}

public class Query
{
    [UseDbContext(typeof(DatabaseContext))]
    [UseProjection]
    public IQueryable<User> GetUsers([ScopedService] DatabaseContext databaseContext)
    {
        return databaseContext.Users;
    }
}

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

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPooledDbContextFactory<DatabaseContext>(b => b.UseSqlServer("Server=localhost;Database=test;User Id=sa;Password=Password!;"));

        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddProjections();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            // By default the GraphQL server is mapped to /graphql
            // This route also provides you with our GraphQL IDE. In order to configure the
            // the GraphQL IDE use endpoints.MapGraphQL().WithToolOptions(...).
            endpoints.MapGraphQL();
        });
    }
}