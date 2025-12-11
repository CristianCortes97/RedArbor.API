using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RedArbor.Domain.Entities;

namespace RedArbor.Infraestructure.Context;

public partial class DbredArborContext : DbContext
{
    public DbredArborContext()
    {
    }

    public DbredArborContext(DbContextOptions<DbredArborContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employe> Employes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employes__3214EC07C0AEFC0C");

            entity.Property(e => e.CompanyId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeleteOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lastlogin).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
