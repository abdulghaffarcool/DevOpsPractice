using System.ComponentModel.DataAnnotations;

namespace DevOpsPractice.Web.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0.01, 100000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}