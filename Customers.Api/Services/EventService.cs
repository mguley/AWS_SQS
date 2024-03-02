using Customers.Api.Abstractions;

namespace Customers.Api.Services;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;

    public EventService(ILogger<EventService> logger)
    {
        _logger = logger;
    }
    
    public async Task DispatchAsync(IApplicationEvent applicationEvent)
    {
        // Logic to dispatch event, e.g., through messaging service, event bus, etc
        _logger.LogInformation($"Event dispatched: {applicationEvent.GetType().Name}");
        
        // Placeholder for actual dispatch logic
        await Task.CompletedTask;
    }
}
