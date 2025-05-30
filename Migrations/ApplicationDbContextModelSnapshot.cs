﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace RestauApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Avis", b =>
                {
                    b.Property<int>("IdAvis")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Commentaire")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateAvis")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdRestaurant")
                        .HasColumnType("int");

                    b.Property<int>("IdUtilisateur")
                        .HasColumnType("int");

                    b.Property<int?>("Note")
                        .HasColumnType("int");

                    b.HasKey("IdAvis");

                    b.HasIndex("IdRestaurant");

                    b.HasIndex("IdUtilisateur");

                    b.ToTable("Avis", (string)null);
                });

            modelBuilder.Entity("Employe", b =>
                {
                    b.Property<int>("IdEmploye")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdRestaurant")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TablesAssignees")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdEmploye");

                    b.HasIndex("IdRestaurant");

                    b.ToTable("Employes", (string)null);
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRes")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("Heure")
                        .HasColumnType("time(6)");

                    b.Property<int>("IdRestaurant")
                        .HasColumnType("int");

                    b.Property<int>("IdTable")
                        .HasColumnType("int");

                    b.Property<int>("IdUtilisateur")
                        .HasColumnType("int");

                    b.Property<int>("NombrePersonnes")
                        .HasColumnType("int");

                    b.Property<string>("ServiceSpecial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdReservation");

                    b.HasIndex("IdRestaurant");

                    b.HasIndex("IdTable");

                    b.HasIndex("IdUtilisateur");

                    b.ToTable("Reservations", (string)null);
                });

            modelBuilder.Entity("Restaurant", b =>
                {
                    b.Property<int>("IdRestaurant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Horaires")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Menu")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdRestaurant");

                    b.ToTable("Restaurants", (string)null);
                });

            modelBuilder.Entity("Salle", b =>
                {
                    b.Property<int>("IdSalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacite")
                        .HasColumnType("int");

                    b.Property<int>("IdRestaurant")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdSalle");

                    b.HasIndex("IdRestaurant");

                    b.ToTable("Salles", (string)null);
                });

            modelBuilder.Entity("TableRestaurant", b =>
                {
                    b.Property<int>("IdTable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdSalle")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("IdTable");

                    b.HasIndex("IdSalle");

                    b.ToTable("TablesRestaurant", (string)null);
                });

            modelBuilder.Entity("Utilisateur", b =>
                {
                    b.Property<int>("IdUtilisateur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ProgrammeFidelite")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdUtilisateur");

                    b.ToTable("Utilisateurs", (string)null);
                });

            modelBuilder.Entity("Avis", b =>
                {
                    b.HasOne("Restaurant", "Restaurant")
                        .WithMany("Avis")
                        .HasForeignKey("IdRestaurant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Utilisateur", "Utilisateur")
                        .WithMany("Avis")
                        .HasForeignKey("IdUtilisateur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("Employe", b =>
                {
                    b.HasOne("Restaurant", "Restaurant")
                        .WithMany("Employes")
                        .HasForeignKey("IdRestaurant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.HasOne("Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("IdRestaurant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TableRestaurant", "TableRestaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("IdTable")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Utilisateur", "Utilisateur")
                        .WithMany("Reservations")
                        .HasForeignKey("IdUtilisateur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("TableRestaurant");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("Salle", b =>
                {
                    b.HasOne("Restaurant", "Restaurant")
                        .WithMany("Salles")
                        .HasForeignKey("IdRestaurant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("TableRestaurant", b =>
                {
                    b.HasOne("Salle", "Salle")
                        .WithMany("TablesRestaurant")
                        .HasForeignKey("IdSalle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salle");
                });

            modelBuilder.Entity("Restaurant", b =>
                {
                    b.Navigation("Avis");

                    b.Navigation("Employes");

                    b.Navigation("Reservations");

                    b.Navigation("Salles");
                });

            modelBuilder.Entity("Salle", b =>
                {
                    b.Navigation("TablesRestaurant");
                });

            modelBuilder.Entity("TableRestaurant", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Utilisateur", b =>
                {
                    b.Navigation("Avis");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
