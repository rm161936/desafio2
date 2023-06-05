using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CinePlus.Models.ContextModels;

public partial class CinePlusContext : DbContext
{
    public CinePlusContext()
    {
    }

    public CinePlusContext(DbContextOptions<CinePlusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Puntuacion> Puntuacions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3214EC078DC59715");

            entity.ToTable("Genero");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pelicula__3214EC0784B0C8F8");

            entity.Property(e => e.Director)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sinopsis)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Genero).WithMany(p => p.Peliculas)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__Peliculas__IdGen__267ABA7A");

            entity.Navigation(e => e.Genero).AutoInclude(true);
        });

        modelBuilder.Entity<Puntuacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Puntuaci__3214EC074312890F");

            entity.ToTable("Puntuacion");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Pelicula).WithOne(p => p.Puntuacion)
                .HasForeignKey<Puntuacion>(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Puntuacion__Id__2A4B4B5E");

            entity.Navigation(e => e.Pelicula).AutoInclude(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
