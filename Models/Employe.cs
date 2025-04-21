using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdEmploye { get; set; }

    [Required]
    [StringLength(50)]
    public string Nom { get; set; }

    [Required]
    [StringLength(50)]
    public string Prenom { get; set; }

    [Required]
    [StringLength(50)]
    public string Role { get; set; }

    [ForeignKey("Restaurant")]
    public int IdRestaurant { get; set; }

    public string TablesAssignees { get; set; } // Storing as JSON array or comma-separated values

    public Restaurant? Restaurant { get; set; }
}