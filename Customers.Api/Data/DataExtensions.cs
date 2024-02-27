using Customers.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Data;

/// <summary>
/// Provides extension methods for initializing and migrating the database.
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="app">The web application to which the database migration is applied.</param>
    /// <returns>A task representing the asynchronous operation of the database migration.</returns>
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CustomersContext>();
        await dbContext.Database.MigrateAsync();
    }
}
