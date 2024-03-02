using Customers.DAL.Data;
using Customers.DAL.Data.InitDataFactory;
using Customers.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Data;

/// <summary>
/// Provides extension methods for initializing and migrating the database.
/// This includes applying any pending migrations and seeding the database with initial data.
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Applies any pending migrations for the context to the database and seeds the database with initial data.
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="app">The web application to which the database migration and data seeding are applied.</param>
    /// <returns>A task representing the asynchronous operation of the database migration and data seeding.</returns>
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using CustomersContext dbContext = scope.ServiceProvider.GetRequiredService<CustomersContext>();
        await dbContext.Database.MigrateAsync();
        await SeedDataAsync(dbContext: dbContext, app: app);
    }
    
    /// <summary>
    /// Seeds the database with initial data if the Customers table is empty.
    /// Is intended to be run after the database migration is complete.
    /// </summary>
    /// <param name="dbContext">The database context for accessing the Customers table.</param>
    /// <param name="app">The web application to resolve the <see cref="AbstractDataFactory"/> for generating initial data.</param>
    /// <returns>A task representing the asynchronous operation of the data seeding.</returns>
    private static async Task SeedDataAsync(CustomersContext dbContext, WebApplication app)
    {
        if (!dbContext.Customers.Any())
        {
            using IServiceScope scope = app.Services.CreateScope();
            AbstractDataFactory dataFactory = scope.ServiceProvider.GetRequiredService<AbstractDataFactory>();
            IEnumerable<Customer> customersData = dataFactory.GetCustomersInitialData();
            await dbContext.Customers.AddRangeAsync(entities: customersData);
            await dbContext.SaveChangesAsync();
        }
    }
}
