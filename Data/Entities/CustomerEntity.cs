using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerName { get; set; } = null!;

    [Required]
    public string CustomerEmail { get; set; } = null!;

    [Required]
    public string CustomerPhoneNumber { get; set; } = null!;
}
