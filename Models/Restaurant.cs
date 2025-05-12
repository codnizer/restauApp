using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRestaurant { get; set; }

    [Required]
    [StringLength(100)]
    public required string Nom { get; set; }

    [Required]
    public required string Adresse { get; set; }

    [StringLength(20)]
    public required string Telephone { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public required string Email { get; set; }

    public required string Horaires { get; set; }
    public required string Description { get; set; }
    public required string Photo { get; set; }
    public required string Menu { get; set; }

    public ICollection<Salle> Salles { get; set; } = new List<Salle>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Avis> Avis { get; set; } = new List<Avis>();
    public ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
