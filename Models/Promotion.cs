using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Promotion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPromotion { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Range(1, 100)]
    public int DiscountPercentage { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [ForeignKey("Restaurant")]
    public int IdRestaurant { get; set; }

    public Restaurant? Restaurant { get; set; }
}

