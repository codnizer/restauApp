using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdReservation { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateRes { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan Heure { get; set; }

    [Required]
    public int NombrePersonnes { get; set; }

    [StringLength(100)]
    public string ServiceSpecial { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "en attente";

    [ForeignKey("Utilisateur")]
    public int IdUtilisateur { get; set; }

    [ForeignKey("TableRestaurant")]
    public int IdTable { get; set; }

    [ForeignKey("Restaurant")]
    public int IdRestaurant { get; set; }

    public Utilisateur Utilisateur { get; set; }
    public TableRestaurant TableRestaurant { get; set; }
    public Restaurant Restaurant { get; set; }
}