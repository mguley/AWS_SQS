using Customers.Api.Abstractions;

namespace Customers.Api.Endpoints.Customers;

/// <summary>
/// Represents the base class for API endpoints, providing common functionalities and properties
/// needed across different endpoints.
/// </summary>
/// <param name="eventService"></param>
public abstract class BaseEndpoint(IEventService eventService)
{
    /// <summary>
    /// Gets the event service associated with the endpoint, allowing for event dispatching.
    /// </summary>
    protected IEventService EventService { get; private set; } = eventService;
}
