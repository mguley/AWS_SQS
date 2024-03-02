using Customers.Api.Abstractions;
using Customers.Messaging.Abstractions;

namespace Customers.Api.Services;

/// <summary>
/// Provides a service for dispatching application events to a message queue.
/// </summary>
public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private readonly ISqsMessenger _sqsMessenger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventService"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging information and errors.</param>
    /// <param name="sqsMessenger">The service used for sending messages to AWS SQS.</param>
    public EventService(ILogger<EventService> logger, ISqsMessenger sqsMessenger)
    {
        _logger = logger;
        _sqsMessenger = sqsMessenger;
    }
    
    /// <summary>
    /// Asynchronously dispatches an application event to the configured message queue.
    /// </summary>
    /// <param name="applicationEvent">The event to dispatch.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DispatchAsync(IApplicationEvent applicationEvent)
    {
        try
        {
            var payload = applicationEvent.GetPayload();
            await _sqsMessenger.SendMessageAsync(message: payload);
            _logger.LogInformation($"Event dispatched: {applicationEvent.GetType().Name}");
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Error dispatching event to SQS.");
        }
    }
}
