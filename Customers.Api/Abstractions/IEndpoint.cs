namespace Customers.Api.Abstractions;

/// <summary>
/// Defines a contract for mapping an endpoint to the ASP.NET Core routing system.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Maps the current endpoint to the application's routing mechanism.
    /// </summary>
    /// <param name="app">The endpoint route builder to configure.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}