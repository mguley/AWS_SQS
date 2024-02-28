namespace Customers.Api.Models;

/// <summary>
/// Represents a data transfer object for customer information.
/// </summary>
/// <param name="Guid">The unique identifier for the customer.</param>
/// <param name="GitHubUsername">The GitHub username associated with the customer, if available.</param>
/// <param name="FullName">The full name of the customer.</param>
/// <param name="Email">The email address of the customer.</param>
public record CustomerDto(
    Guid Guid,
    string? GitHubUsername,
    string? FullName,
    string? Email);