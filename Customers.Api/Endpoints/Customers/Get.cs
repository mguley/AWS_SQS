using Customers.Api.Abstractions;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the GET operation for customers.
/// </summary>
public class Get : IEndpoint
{
    /// <summary>
    /// Maps the "get" action to the GET HTTP method.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "get", handler: () => "Get endpoint");
    }
}