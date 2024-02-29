using Customers.Api.Abstractions;
using Customers.Api.Mapping;
using Customers.DAL.Abstractions;
using Customers.DAL.Entities;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the GET operation to retrieve all customers.
/// </summary>
public class GetAllCustomers : IEndpoint
{
    /// <summary>
    /// Configures the endpoint for retrieving all customers.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "customers/", handler: HandleGetAllCustomers);
    }

    /// <summary>
    /// Handles the retrieval of all customers.
    /// </summary>
    /// <param name="customerRepository">The customer repository for data access.</param>
    /// <param name="logger">The logger for logging errors or information.</param>
    /// <returns>An HTTP result representing the outcome of the operation.</returns>
    private async Task<IResult> HandleGetAllCustomers(ICustomerRepository customerRepository,
        ILogger<GetAllCustomers> logger)
    {
        try
        {
            IEnumerable<Customer> customers = await customerRepository.GetAllAsync();
            return Results.Ok(customers.Select(customer => customer.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(exception: ex, message: "Error retrieving all customers.");
            return Results.Problem(detail: "An error occurred while retrieving customers.");
        }
    }
}
