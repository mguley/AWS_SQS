using Customers.DAL.Entities;

namespace Customers.DAL.Abstractions;

/// <summary>
/// Defines the contract for customer data operations.
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Asynchronously creates a new customer record.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating success or failure.</returns>
    Task<bool> CreateAsync(Customer customer);

    /// <summary>
    /// Asynchronously retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the customer.</param>
    /// <returns>A task representing the asynchronous operation, containing the customer if found; otherwise, null.</returns>
    Task<Customer?> GetByIdAsync(Guid guid);

    /// <summary>
    /// Asynchronously retrieves all customer records.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of all customers.</returns>
    Task<IEnumerable<Customer>> GetAllAsync();

    /// <summary>
    /// Asynchronously updates an existing customer record.
    /// </summary>
    /// <param name="customer">The customer with updated information.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating success or failure.</returns>
    Task<bool> UpdateAsync(Customer customer);

    /// <summary>
    /// Asynchronously deletes a customer by their unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the customer to delete.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating success or failure.</returns>
    Task<bool> DeleteByIdAsync(Guid guid);
}
