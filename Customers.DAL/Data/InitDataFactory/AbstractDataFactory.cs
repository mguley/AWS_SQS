using Customers.DAL.Entities;

namespace Customers.DAL.Data.InitDataFactory;

/// <summary>
/// An abstract class that provides a contract for creating initial data factories.
/// </summary>
public abstract class AbstractDataFactory
{
    /// <summary>
    /// Gets an array of customers with their initial data.
    /// </summary>
    /// <returns>An array of <see cref="Customer"/> objects populated with initial data.</returns>
    public abstract Customer[] GetCustomersInitialData();
}
