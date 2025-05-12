using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Utilisateur
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdUtilisateur { get; set; }

    [Required]
    [StringLength(50)]
    public required string Nom { get; set; }

    [Required]
    [StringLength(50)]
    public required string Prenom { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public required string Email { get; set; }

    [StringLength(20)]
    public required string Telephone { get; set; }

    [Required]
    [StringLength(255)]
    public required string MotDePasse { get; set; }

    public int ProgrammeFidelite { get; set; } = 0;

    [Required]
    [StringLength(20)]
    public required string Role { get; set; }  // <<--- ajouté ici

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Avis> Avis { get; set; } = new List<Avis>();
}
