using Customers.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.DAL.Data;

/// <summary>
/// Database context representing a session with the database allowing for querying and saving instances of entities.
/// </summary>
public class CustomersContext(DbContextOptions<CustomersContext> options) : DbContext(options: options)
{
    /// <summary>
    /// Gets the set of Customer entities that are tracked by the context.
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();
}