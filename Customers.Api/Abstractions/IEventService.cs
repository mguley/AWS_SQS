namespace Customers.Api.Abstractions;

/// <summary>
/// Defines a service responsible for dispatching application events in a consistent manner.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Asynchronously dispatches an application event.
    /// </summary>
    /// <param name="applicationEvent">The event to dispatch.</param>
    /// <returns>A task representing the asynchronous operation of event dispatch.</returns>
    Task DispatchAsync(IApplicationEvent applicationEvent);
}
