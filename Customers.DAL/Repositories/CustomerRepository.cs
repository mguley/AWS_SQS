using Customers.DAL.Abstractions;
using Customers.DAL.Data;
using Customers.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.DAL.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomersContext _customersContext;
    private readonly ILogger<ICustomerRepository> _logger;
    private readonly DbSet<Customer> _dbSet;

    public CustomerRepository(CustomersContext customersContext, ILogger<ICustomerRepository> logger)
    {
        _customersContext = customersContext ?? throw new ArgumentNullException(nameof(customersContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbSet = customersContext.Set<Customer>();
    }
    
    /// <summary>
    /// Asynchronously creates a new customer record.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating success or failure.</returns>
    public async Task<bool> CreateAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(argument: customer);
        try
        {
            _dbSet.Add(entity: customer);
            return await _customersContext.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(exception: ex,
                message: "An error occurred while adding a new customer. Error: {ErrorMessage}", ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Asynchronously retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the customer.</param>
    /// <returns>A task representing the asynchronous operation, containing the customer if found; otherwise, null.</returns>
    public async Task<Customer?> GetByIdAsync(Guid guid)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Guid == guid);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex,
                message: "An error occurred while retrieving the customer with GUID {CustomerGuid}.", guid);
            return null;
        }
    }

    /// <summary>
    /// Asynchronously retrieves all customer records.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of all customers.</returns>
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        try
        { 
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "An error occurred while retrieving all customers.");
            return Enumerable.Empty<Customer>();
        }
    }

    /// <summary>
    /// Asynchronously updates an existing customer record.
    /// </summary>
    /// <param name="customer">The customer with updated information.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating success or failure.</returns>
    public async Task<bool> UpdateAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(argument: customer);
        try
        {
            _dbSet.Update(entity: customer);
            return await _customersContext.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(exception: ex, 
                message: "An error occurred while updating the customer with ID {CustomerId}. Error: {ErrorMessage}",
                customer.Id, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Asynchronously deletes a customer by their unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the customer to delete.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating success or failure.</returns>
    public async Task<bool> DeleteByIdAsync(Guid guid)
    {
        var customer = await _dbSet.FirstOrDefaultAsync(c => c.Guid == guid);
        if (customer is null)
        {
            _logger.LogWarning(
                message: "Attempted to delete a customer with GUID {CustomerId}, but they were not found.", guid);
            return false;
        }

        try
        {
            _dbSet.Remove(entity: customer);
            return await _customersContext.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(exception: ex, 
                message: "An error occurred while deleting the customer with GUID {CustomerGuid}. Error: {ErrorMessage}", 
                guid, ex.Message);
            return false;
        }
    }
}
