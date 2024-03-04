using Amazon.SQS;
using Amazon.SQS.Model;
using Customers.Messaging.Models;
using Customers.Messaging.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace Customers.Messaging.UnitTests;

public class SqsMessengerTests
{
    private readonly Mock<IAmazonSQS> _mockSqsClient;
    private readonly Mock<IOptions<QueueSettings>> _mockQueueSettings;
    private readonly SqsMessenger _service;

    public SqsMessengerTests()
    {
        // Initialize mocks
        _mockSqsClient = new Mock<IAmazonSQS>();
        _mockQueueSettings = new Mock<IOptions<QueueSettings>>();
        
        // Set up default behavior for mocks
        _mockQueueSettings.Setup(s => s.Value)
            .Returns(new QueueSettings { Name = "test-queue" });
        
        // Initialize the service with the mocks
        _service = new SqsMessenger(sqs: _mockSqsClient.Object, queueSettings: _mockQueueSettings.Object);
    }
    
    [Fact]
    public async Task SendMessageAsync_CallsGetQueueUrlAndSendsMessage()
    {
        // Arrange
        var text = "Message we send to the AWS SQS.";
        var testMessage = new { Content = text };
        var queueUrl = "https://sqs.test.amazonaws.com/123/test-queue";
        
        _mockSqsClient.Setup(x => x.GetQueueUrlAsync(It.IsAny<string>(), default))
            .ReturnsAsync(new GetQueueUrlResponse { QueueUrl = queueUrl });
        _mockSqsClient.Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), default))
            .ReturnsAsync(new SendMessageResponse());
        
        // Act
        await _service.SendMessageAsync(message: testMessage);
        
        // Assert
        _mockSqsClient.Verify(x => x.GetQueueUrlAsync("test-queue", default), Times.Once);
        _mockSqsClient.Verify(
            x => x.SendMessageAsync(
                It.Is<SendMessageRequest>(req => req.QueueUrl == queueUrl && req.MessageBody.Contains(text)),
                default), Times.Once);
    }

    [Fact]
    public async Task GetQueueUrlAsync_CachesUrl()
    {
        // Arrange
        var queueUrl = "https://test123.amazonaws.com/555/test-queue";

        _mockSqsClient.Setup(x => x.GetQueueUrlAsync(It.IsAny<string>(), default))
            .ReturnsAsync(new GetQueueUrlResponse { QueueUrl = queueUrl});
        
        // Act
        var url1 = await _service.GetQueueUrlAsync();
        var url2 = await _service.GetQueueUrlAsync();
        
        // Assert
        Assert.Equal(url1, url2);
        _mockSqsClient.Verify(x => x.GetQueueUrlAsync("test-queue", default), Times.Once);
    }
}
