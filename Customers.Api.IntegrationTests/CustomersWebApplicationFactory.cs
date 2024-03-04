using Customers.DAL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Customers.Api.IntegrationTests;

/// <summary>
/// A factory for creating instances of the web application under test with specific services
/// overridden for integration testing purposes. It configures the application to use a SQLite
/// database file.
/// </summary>
internal class CustomersWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string AppSettingsFileName = "appsettings.json";
    private const string TestDbName = "TestDb";
    
    /// <summary>
    /// Configures the web host for integration testing. Overrides the application's database context
    /// to use a SQLite database file named 'IntegrationTests.db' located in the project's output directory.
    /// </summary>
    /// <param name="builder">The IWebHostBuilder instance used to configure the web host.</param>
    /// <exception cref="InvalidOperationException"></exception>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove the existing CustomersContext registration to replace it with a test-specific one.
            services.RemoveAll(typeof(DbContextOptions<CustomersContext>));
            
            // Build configuration from a local appsettings.json specific to the integration tests.
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, AppSettingsFileName);
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath)
                .Build();
            
            string connString = configuration.GetConnectionString(name: TestDbName) ??
                                throw new InvalidOperationException(
                                    message: $"Connection string '{TestDbName}' not found.");
            services.AddDbContext<CustomersContext>(optionsAction: options =>
            {
                options.UseSqlite(connectionString: connString);
            });

            // Ensure the database is created and deleted for each test to maintain isolation
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<CustomersContext>();
            dbContext.Database.EnsureDeleted();
        });
    }
}
