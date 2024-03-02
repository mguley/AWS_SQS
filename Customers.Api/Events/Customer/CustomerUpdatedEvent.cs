using Customers.Api.Abstractions;
using Customers.Api.Models;

namespace Customers.Api.Events.Customer;

/// <summary>
/// Represents an event that occurs when an existing customer is updated.
/// This event carries the data associated with the updated customer.
/// </summary>
/// <param name="customerDto"></param>
public class CustomerUpdatedEvent(CustomerDto customerDto) : IApplicationEvent
{
    /// <summary>
    /// Gets customer data associated with this event.
    /// </summary>
    /// <returns></returns>
    public object GetPayload() => customerDto;
}
