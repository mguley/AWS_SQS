using System.Net;
using Amazon.SQS.Model;
using Customers.Messaging.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Api.IntegrationTests.Customer.Messaging.Services;

public class SqsMessengerTests
{
    [Fact]
    public async Task SendMessageAsync_SendsMessageToAmazonSQSQueue()
    {
        // Arrange
        var factory = new CustomersWebApplicationFactory();
        var sqsMessenger = factory.Services.GetRequiredService<ISqsMessenger>();
        
        // Act
        SendMessageResponse response = await sqsMessenger.SendMessageAsync(message: new { Payload = "hello" });

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
        Assert.False(string.IsNullOrWhiteSpace(response.MessageId));
        Assert.False(string.IsNullOrWhiteSpace(response.ResponseMetadata.RequestId));
    }
}
