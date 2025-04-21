using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Salle> Salles { get; set; }
    public DbSet<TableRestaurant> TablesRestaurant { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Avis> Avis { get; set; }
    public DbSet<Employe> Employes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and constraints
        modelBuilder.Entity<Salle>()
            .HasOne(s => s.Restaurant)
            .WithMany(r => r.Salles)
            .HasForeignKey(s => s.IdRestaurant);

        modelBuilder.Entity<TableRestaurant>()
            .HasOne(t => t.Salle)
            .WithMany(s => s.TablesRestaurant)
            .HasForeignKey(t => t.IdSalle);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Utilisateur)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.IdUtilisateur);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.TableRestaurant)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.IdTable);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Restaurant)
            .WithMany(r => r.Reservations)
            .HasForeignKey(r => r.IdRestaurant);

        modelBuilder.Entity<Avis>()
            .HasOne(a => a.Utilisateur)
            .WithMany(u => u.Avis)
            .HasForeignKey(a => a.IdUtilisateur);

        modelBuilder.Entity<Avis>()
            .HasOne(a => a.Restaurant)
            .WithMany(r => r.Avis)
            .HasForeignKey(a => a.IdRestaurant);

        modelBuilder.Entity<Employe>()
            .HasOne(e => e.Restaurant)
            .WithMany(r => r.Employes)
            .HasForeignKey(e => e.IdRestaurant);
    }
}