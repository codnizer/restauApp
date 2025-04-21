using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRestaurant { get; set; }

    [Required]
    [StringLength(100)]
    public string Nom { get; set; }

    [Required]
    public string Adresse { get; set; }

    [StringLength(20)]
    public string Telephone { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    public string Horaires { get; set; }
    public string Description { get; set; }
    public string Photo { get; set; }
    public string Menu { get; set; }

    public ICollection<Salle> Salles { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Avis> Avis { get; set; }
    public ICollection<Employe> Employes { get; set; }
}