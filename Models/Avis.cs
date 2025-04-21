using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Avis
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAvis { get; set; }

    public int? Note { get; set; }
    public string Commentaire { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateAvis { get; set; }

    [ForeignKey("Utilisateur")]
    public int IdUtilisateur { get; set; }

    [ForeignKey("Restaurant")]
    public int IdRestaurant { get; set; }

    public Utilisateur? Utilisateur { get; set; }
    public Restaurant? Restaurant { get; set; }
}