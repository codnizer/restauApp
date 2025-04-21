using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TableRestaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTable { get; set; }

    [Required]
    public int Numero { get; set; }

    [ForeignKey("Salle")]
    public int IdSalle { get; set; }

    public Salle? Salle { get; set; }

    // ✅ Initialize this collection to avoid null reference issues
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
