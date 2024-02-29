using Customers.DAL.Entities;

namespace Customers.DAL.Data.InitDataFactory;

/// <summary>
/// A concrete implementation of <see cref="AbstractDataFactory"/> that provides initial customer data.
/// </summary>
public class DataFactory : AbstractDataFactory
{
    /// <summary>
    /// Provides an array of predefined <see cref="Customer"/> objects to seed the database.
    /// </summary>
    /// <returns>An array of <see cref="Customer"/> objects with predefined initial data.</returns>
    public override Customer[] GetCustomersInitialData()
    {
        return new Customer[]
        {
            new()
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                GitHubUsername = "johndoe123",
            },
            new()
            {
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                GitHubUsername = "janesmith456",
            },
            new()
            {
                FullName = "Bob Johnson",
                Email = "bob.johnson@example.com",
                GitHubUsername = "bobjohnson789"
            }
        };
    }
}
