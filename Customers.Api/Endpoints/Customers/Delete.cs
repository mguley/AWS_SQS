using Customers.Api.Abstractions;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the DELETE operation for customers.
/// </summary>
public class Delete : IEndpoint
{
    /// <summary>
    /// Maps the "delete" action to the DELETE HTTP method.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(pattern: "delete", handler: () => "Delete endpoint");
    }
}