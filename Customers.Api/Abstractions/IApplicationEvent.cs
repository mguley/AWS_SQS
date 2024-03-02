namespace Customers.Api.Abstractions;

/// <summary>
/// Represents a base interface for application events, allowing for a common handling mechanism
/// while supporting a variety of event types.
/// </summary>
public interface IApplicationEvent
{
    /// <summary>
    /// Gets payload associated with an event.
    /// </summary>
    /// <returns></returns>
    object GetPayload();
}
