using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Salle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdSalle { get; set; }

    [Required]
    [StringLength(50)]
    public string Nom { get; set; }

    [Required]
    public int Capacite { get; set; }

    [Required]
    [StringLength(50)]
    public string Type { get; set; }

    // Foreign Key for Restaurant
    [Required]  // Make sure it's required
    [ForeignKey("Restaurant")]
    public int IdRestaurant { get; set; }

    // Navigation property
public Restaurant? Restaurant { get; set; }  // ← make it nullable

    // Initialize the collection to avoid null reference issues
    public ICollection<TableRestaurant> TablesRestaurant { get; set; } = new List<TableRestaurant>();
}
