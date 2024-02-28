using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customers.DAL.Entities;

/// <summary>
/// Represents a customer with properties for identification, contact, and metadata.
/// </summary>
public class Customer
{
    /// <summary>
    /// Gets the primary key for the customer.
    /// </summary>
    /// <value>The primary key as an auto-incremented integer value.</value>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// Gets the unique identifier for the customer.
    /// </summary>
    /// <value>The customer's globally unique identifier (GUID).</value>
    public Guid Guid { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// Gets or sets the GitHub username of the customer.
    /// </summary>
    /// <value>The GitHub username associated with the customer, if any.</value>
    [MaxLength(255)]
    public string? GitHubUsername { get; set; }
    
    /// <summary>
    /// Gets or sets the full name of the customer.
    /// </summary>
    /// <value>The customer's full name. This field is required.</value>
    [MaxLength(255)]
    public string? FullName { get; set; }
    
    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    /// <value>The email address associated with the customer, if any.</value>
    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }
    
    /// <summary>
    /// Gets or sets date and time when the customer was created.
    /// </summary>
    /// <value>The date and time when the customer record was created.</value>
    public DateTime Created { get; private set; } = DateTime.UtcNow;
}
