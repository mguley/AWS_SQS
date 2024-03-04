using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Customers.Messaging.Abstractions;
using Customers.Messaging.Models;
using Microsoft.Extensions.Options;

namespace Customers.Messaging.Services;

/// <summary>
/// Implements the <see cref="ISqsMessenger"/> interface to provide messaging services using Amazon SQS.
/// </summary>
public class SqsMessenger : ISqsMessenger
{
    private readonly IAmazonSQS _amazonSqs;
    private readonly IOptions<QueueSettings> _queueSettings;
    private string? _queueUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqsMessenger"/> class.
    /// </summary>
    /// <param name="sqs">The Amazon SQS client.</param>
    /// <param name="queueSettings">The settings for the SQS queue.</param>
    public SqsMessenger(IAmazonSQS sqs, IOptions<QueueSettings> queueSettings)
    {
        _amazonSqs = sqs;
        _queueSettings = queueSettings;
    }
    
    /// <summary>
    /// Asynchronously sends a message to the configured Amazon SQS queue.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <typeparam name="T">The type of the message being sent.</typeparam>
    /// <returns>A task representing the asynchronous operation, with a <see cref="SendMessageResponse"/>.</returns>
    public async Task<SendMessageResponse> SendMessageAsync<T>(T message)
    {
        string queueUrl = await GetQueueUrlAsync();

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(value: message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _amazonSqs.SendMessageAsync(request: sendMessageRequest);
    }

    /// <summary>
    /// Retrieves the URL for the configured Amazon SQS queue.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with the queue URL as a string.</returns>
    public async Task<string> GetQueueUrlAsync()
    {
        if (_queueUrl is not null)
        {
            return _queueUrl;
        }

        var queueUrlResponse = await _amazonSqs.GetQueueUrlAsync(queueName: _queueSettings.Value.Name);
        _queueUrl = queueUrlResponse.QueueUrl;
        return _queueUrl;
    }
}
