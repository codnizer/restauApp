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

    [ForeignKey("Restaurant")]
    public int IdRestaurant { get; set; }

    public Restaurant Restaurant { get; set; }
    public ICollection<TableRestaurant> TablesRestaurant { get; set; }
}