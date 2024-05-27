using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pr5SP.Models;

namespace Pr5SP.Context;

public partial class Pr5SpContext : DbContext
{
    public Pr5SpContext()
    {
    }

    public Pr5SpContext(DbContextOptions<Pr5SpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Exhibit> Exhibits { get; set; }

    public virtual DbSet<Pavilion> Pavilions { get; set; }

    public virtual DbSet<ResponsibleEmployee> ResponsibleEmployees { get; set; }

    public virtual DbSet<TypeOfExhibit> TypeOfExhibits { get; set; }
    
    public virtual DbSet<AuditViewModel> AuditViewModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost:5432;database=Pr5SP;username=postgres;password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("Country_pkey");

            entity.ToTable("Country");

            entity.Property(e => e.CountryId).HasColumnName("countryID");
            entity.Property(e => e.NameOfCountry)
                .HasMaxLength(50)
                .HasColumnName("nameOfCountry");
        });

        modelBuilder.Entity<Exhibit>(entity =>
        {
            entity.HasKey(e => e.ExhibitId).HasName("Exhibit_pkey");

            entity.ToTable("Exhibit");

            entity.Property(e => e.ExhibitId).HasColumnName("ExhibitID");
            entity.Property(e => e.CountryId).HasColumnName("countryID");
            entity.Property(e => e.DateOfArrival).HasColumnName("dateOfArrival");
            entity.Property(e => e.DiscoveryDate).HasColumnName("discoveryDate");
            entity.Property(e => e.NameOfExhibit)
                .HasMaxLength(128)
                .HasColumnName("nameOfExhibit");
            entity.Property(e => e.PavilionId).HasColumnName("pavilionID");
            entity.Property(e => e.TypeOfExhibit).HasColumnName("typeOfExhibit");

            entity.HasOne(d => d.Country).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("countryFK");

            entity.HasOne(d => d.Pavilion).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.PavilionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pavilionFK");

            entity.HasOne(d => d.TypeOfExhibitNavigation).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.TypeOfExhibit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TypeOfExhibitFK");
        });

        modelBuilder.Entity<Pavilion>(entity =>
        {
            entity.HasKey(e => e.PavilionId).HasName("Pavilion_pkey");

            entity.ToTable("Pavilion");

            entity.Property(e => e.PavilionId).HasColumnName("pavilionID");
            entity.Property(e => e.NameOfPavilion)
                .HasMaxLength(256)
                .HasColumnName("nameOfPavilion");
            entity.Property(e => e.ResponsibleEmployee).HasColumnName("responsibleEmployee");

            entity.HasOne(d => d.ResponsibleEmployeeNavigation).WithMany(p => p.Pavilions)
                .HasForeignKey(d => d.ResponsibleEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("responsibleEmployeeFK");
        });

        modelBuilder.Entity<ResponsibleEmployee>(entity =>
        {
            entity.HasKey(e => e.ResponsibleEmployeeId).HasName("responsibleEmployee_pkey");

            entity.ToTable("responsibleEmployee");

            entity.Property(e => e.ResponsibleEmployeeId).HasColumnName("responsibleEmployeeID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(64)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("lastName");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(64)
                .HasColumnName("patronymic");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(64)
                .HasColumnName("telephoneNumber");
        });

        modelBuilder.Entity<TypeOfExhibit>(entity =>
        {
            entity.HasKey(e => e.TypeOfExhibitId).HasName("TypeOfExhibit_pkey");

            entity.ToTable("TypeOfExhibit");

            entity.Property(e => e.TypeOfExhibitId).HasColumnName("TypeOfExhibitID");
            entity.Property(e => e.NameOfType)
                .HasMaxLength(50)
                .HasColumnName("nameOfType");
        });
        modelBuilder.Entity<AuditViewModel>(entity =>
        {
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
        
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
