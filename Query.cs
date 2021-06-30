using System.Linq;
using HotChocolate;
using HotChocolate.Data;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    [IsProjected]
    public int AddressId { get; set; }

    [IsProjected]
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