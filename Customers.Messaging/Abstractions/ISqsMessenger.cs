using Amazon.SQS.Model;

namespace Customers.Messaging.Abstractions;

/// <summary>
/// Defines a contract for a messenger service that interacts with Amazon SQS.
/// </summary>
public interface ISqsMessenger
{
    /// <summary>
    /// Sends a message asynchronously to an Amazon SQS queue.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <typeparam name="T">The type of the message being sent.</typeparam>
    /// <returns>The response from the Amazon SQS service.</returns>
    Task<SendMessageResponse> SendMessageAsync<T>(T message);
}
