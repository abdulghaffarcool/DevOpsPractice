namespace DevOpsPractice.Web.Models;

using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}