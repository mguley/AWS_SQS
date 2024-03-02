using Customers.Api.Abstractions;
using Customers.Api.Models;

namespace Customers.Api.Events.Customer;

/// <summary>
/// Represents an event that occurs when a new customer is created.
/// This event carries the data associated with the newly created customer.
/// </summary>
/// <param name="customerDto"></param>
public class CustomerCreatedEvent(CustomerDto customerDto) : IApplicationEvent
{
    /// <summary>
    /// Gets customer data associated with this event.
    /// </summary>
    /// <returns></returns>
    public object GetPayload() => customerDto;
}
