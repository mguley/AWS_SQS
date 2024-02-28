using Customers.Api.Abstractions;
using Customers.Api.Mapping;
using Customers.Api.Models;
using Customers.DAL.Abstractions;
using Customers.DAL.Entities;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the POST operation for creating a new customer in the system.
/// </summary>
public class CreatePost : IEndpoint
{
    /// <summary>
    /// Configures the endpoint for creating a new customer, mapping the POST action to the specified route.
    /// </summary>
    /// <param name="app">The endpoint route builder used for configuring the endpoint route.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "customers", handler: HandleCreateCustomer);
    }

    /// <summary>
    /// Handles the HTTP POST request to create a new customer entity based on the provided customer data transfer object (DTO).
    /// </summary>
    /// <param name="customerRepository">The repository responsible for customer data access.</param>
    /// <param name="logger">The logger for capturing log information.</param>
    /// <param name="customerDto">The customer DTO containing the data for creating a new customer record.</param>
    /// <returns>A task representing the asynchronous operation, resulting in an HTTP response indicating success or failure of customer creation.</returns>
    private async Task<IResult> HandleCreateCustomer(ICustomerRepository customerRepository, ILogger<CreatePost> logger,
        CustomerDto customerDto)
    {
        try
        {
            Customer customerEntity = customerDto.ToEntity();
            bool result = await customerRepository.CreateAsync(customer: customerEntity);
            
            if (!result)
            {
                return Results.Problem(detail: "Failed to create the customer.");
            }
            
            return Results.CreatedAtRoute(
                routeName: GetCustomerByGuid.GetCustomerEndpointName, 
                routeValues: new { rawId = customerEntity.Guid }, 
                value: customerEntity.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(exception: ex, message: "Error creating a new customer.");
            return Results.Problem(detail: "An error occurred while creating the customer.");
        }
    }
}
