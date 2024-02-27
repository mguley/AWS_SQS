using Customers.Api.Abstractions;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the PUT operation for customers.
/// </summary>
public class Put : IEndpoint
{
    /// <summary>
    /// Maps the "put" action to the PUT HTTP method.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(pattern: "put", handler: () => "Put endpoint");
    }
}