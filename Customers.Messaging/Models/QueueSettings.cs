namespace Customers.Messaging.Models;

/// <summary>
/// Represents the settings for an Amazon SQS queue.
/// </summary>
public class QueueSettings
{
    /// <summary>
    /// The configuration key used to retrieve queue settings.
    /// </summary>
    public const string Key = "Queue";
    
    /// <summary>
    /// Gets or sets the name of the SQS queue.
    /// </summary>
    public required string Name { get; init; }
}
