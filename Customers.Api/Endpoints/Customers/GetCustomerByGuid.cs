using Customers.Api.Abstractions;
using Customers.Api.Mapping;
using Customers.DAL.Abstractions;
using Customers.DAL.Entities;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Endpoint for retrieving a customer by their unique identifier.
/// </summary>
public class GetCustomerByGuid : IEndpoint
{
    public const string GetCustomerEndpointName = "GetCustomer";
    
    /// <summary>
    /// Configures the endpoint for retrieving a customer by GUID.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "customers/{rawId}", HandleGetCustomerByGuid)
            .WithName(GetCustomerEndpointName);
    }

    /// <summary>
    /// Handles the HTTP GET request to retrieve a customer by their GUID.
    /// </summary>
    /// <param name="customerRepository">The customer repository for data access.</param>
    /// <param name="rawId">The GUID of the customer to retrieve.</param>
    /// <param name="logger">The logger for logging errors or information.</param>
    /// <returns>An HTTP result representing the outcome of the operation.</returns>
    private async Task<IResult> HandleGetCustomerByGuid(ICustomerRepository customerRepository, string rawId,
        ILogger<GetCustomerByGuid> logger)
    {
        try
        {
            var guid = Guid.Parse(input: rawId);
            Customer? customer = await customerRepository.GetByIdAsync(guid: guid);
            
            return customer is null ? Results.NotFound() : Results.Ok(value: customer.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(exception: ex, message: "Error retrieving customer with GUID: {Guid}", rawId);
            return Results.Problem(detail: $"An error occurred while retrieving the customer with GUID: {rawId}");
        }
    }
}
