using Customers.Api.Models;
using Customers.DAL.Entities;

namespace Customers.Api.Mapping;

/// <summary>
/// Provides extension methods for mapping between CustomerDto and Customer entities.
/// </summary>
public static class CustomerMapping
{
    /// <summary>
    /// Converts a CustomerDto object to a Customer entity.
    /// </summary>
    /// <param name="customerDto">The CustomerDto object to convert.</param>
    /// <returns>A new Customer entity populated with the data from the provided CustomerDto.</returns>
    public static Customer ToEntity(this CustomerDto customerDto)
    {
        return new Customer
        {
            Guid = customerDto.Guid ?? Guid.NewGuid(),
            GitHubUsername = customerDto.GitHubUsername,
            FullName = customerDto.FullName,
            Email = customerDto.Email
        };
    }

    /// <summary>
    /// Converts a Customer entity to a CustomerDto object.
    /// </summary>
    /// <param name="customer">The Customer entity to convert.</param>
    /// <returns>A new CustomerDto object populated with the data from the provided Customer entity.</returns>
    public static CustomerDto ToDto(this Customer customer)
    {
        return new CustomerDto(
            Guid: customer.Guid,
            GitHubUsername: customer.GitHubUsername,
            FullName: customer.FullName,
            Email: customer.Email);
    }
}
