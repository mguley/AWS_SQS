using Customers.Api.Abstractions;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the POST operation for customers.
/// </summary>
public class Post : IEndpoint
{
    /// <summary>
    /// Maps the "post" action to the POST HTTP method.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "post", handler: () => "Post endpoint");
    }
}
