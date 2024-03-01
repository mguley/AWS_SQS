using Customers.Api.Abstractions;
using Customers.DAL.Abstractions;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Endpoint for deleting a customer by their unique identifier.
/// </summary>
public class DeleteCustomer : IEndpoint
{
    /// <summary>
    /// Configures the endpoint for deleting a customer by GUID.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(pattern: "customers/{rawId}", HandleDeleteCustomer);
    }

    /// <summary>
    /// Handles the HTTP DELETE request to delete a customer by their GUID.
    /// </summary>
    /// <param name="customerRepository">The customer repository for data access.</param>
    /// <param name="rawId">The GUID of the customer to retrieve.</param>
    /// <param name="logger">The logger for logging errors or information.</param>
    /// <returns>An HTTP result representing the outcome of the operation.</returns>
    private async Task<IResult> HandleDeleteCustomer(ICustomerRepository customerRepository, string rawId,
        ILogger<DeleteCustomer> logger)
    {
        try
        {
            var guid = Guid.Parse(input: rawId);
            bool result = await customerRepository.DeleteByIdAsync(guid: guid);

            if (!result)
            {
                return Results.NotFound(value: $"Customer with GUID {rawId} not found.");
            }

            return Results.Ok(value: $"Customer with GUID {rawId} successfully deleted.");
        }
        catch (Exception ex)
        {
            logger.LogError(exception: ex, message: $"Error deleting customer with GUID: {rawId}.");
            return Results.Problem(detail: $"An error occurred while deleting the customer with GUID: {rawId}");
        }
    }
}
