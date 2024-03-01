using Customers.Api.Abstractions;
using Customers.Api.Mapping;
using Customers.Api.Models;
using Customers.DAL.Abstractions;
using Customers.DAL.Entities;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Endpoint for updating customer by their unique identifier.
/// </summary>
public class UpdateCustomer : IEndpoint
{
    /// <summary>
    /// Configures the endpoint for updating a customer by GUID.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(pattern: "customers/{guid}", handler: HandleUpdateCustomer);
    }

    /// <summary>
    /// Handles the PUT request to update a customer. Validates the provided GUID, checks for the existence of the customer,
    /// updates the customer, and returns the appropriate HTTP response.
    /// </summary>
    /// <param name="customerRepository">The repository used for accessing customer data.</param>
    /// <param name="customerDto">The data transfer object containing the updated customer information.</param>
    /// <param name="logger">The logger used for logging information or errors.</param>
    /// <param name="guid">The GUID of the customer to be updated, extracted from the URL.</param>
    /// <returns>A task representing the asynchronous operation, resulting in an HTTP response indicating the outcome.</returns>
    private async Task<IResult> HandleUpdateCustomer(ICustomerRepository customerRepository, CustomerDto customerDto,
        ILogger<UpdateCustomer> logger, Guid guid)
    {
        try
        {
            if (!guid.Equals(customerDto.Guid))
            {
                return Results.BadRequest(error: "GUID in the URL doesn't match GUID in the payload.");
            }

            Customer? existingCustomer = await customerRepository.GetByIdAsync(guid: guid);
            if (existingCustomer is null)
            {
                return Results.NotFound(value: $"Customer with GUID {guid} not found.");
            }
            
            existingCustomer.UpdateFromDto(customerDto: customerDto);
            bool isCustomerUpdated = await customerRepository.UpdateAsync(customer: existingCustomer);
            if (!isCustomerUpdated)
            {
                return Results.Problem(detail: $"Failed to update the customer with GUID {guid}.");
            }

            return Results.Ok(value: $"Customer with GUID {guid} was successfully updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(exception: ex, message: $"Error updating customer with GUID: {guid}.");
            return Results.Problem(detail: $"An error occurred while updating the customer with GUID: {guid}.");
        }
    }
}
