using Customers.Api.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Customers.Api.Extensions;

/// <summary>
/// Provides extension methods for registering and mapping endpoints.
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Adds endpoint services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="assembly">The assembly to scan for endpoints.</param>
    /// <returns>The original IServiceCollection for chaining.</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && 
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();
        
        services.TryAddEnumerable(descriptors: serviceDescriptors);
        
        return services;
    }

    /// <summary>
    /// Maps all registered IEndpoint instances to the application's routing mechanism.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
    /// <param name="routeGroupBuilder">Optional RouteGroupBuilder for grouping routes.</param>
    /// <returns>The original WebApplication for chaining.</returns>
    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}
