using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using TuringIA.Server.Models;

namespace TuringIA.Server.Context;

public partial class TuringDbContext : DbContext
{
    public TuringDbContext()
    {
    }

    public TuringDbContext(DbContextOptions<TuringDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CasosDiario> CasosDiarios { get; set; }

    public virtual DbSet<Condado> Condados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Hospitalizacione> Hospitalizaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3309;database=turingtestbd;user=root;password=contrasenaturingtest", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CasosDiario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CondadoId, "condado_id");

            entity.HasIndex(e => e.EstadoId, "estado_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Casos).HasColumnName("casos");
            entity.Property(e => e.CondadoId).HasColumnName("condado_id");
            entity.Property(e => e.EstadoId).HasColumnName("estado_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Muertes).HasColumnName("muertes");

            entity.HasOne(d => d.Condado).WithMany(p => p.CasosDiarios)
                .HasForeignKey(d => d.CondadoId)
                .HasConstraintName("CasosDiarios_ibfk_2");

            entity.HasOne(d => d.Estado).WithMany(p => p.CasosDiarios)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CasosDiarios_ibfk_1");
        });

        modelBuilder.Entity<Condado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.EstadoId, "estado_id");

            entity.HasIndex(e => e.Fips, "fips").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoId).HasColumnName("estado_id");
            entity.Property(e => e.Fips).HasColumnName("fips");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Estado).WithMany(p => p.Condados)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Condados_ibfk_1");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Abreviacion, "abreviacion").IsUnique();

            entity.HasIndex(e => e.Fips, "fips").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abreviacion)
                .HasMaxLength(10)
                .HasColumnName("abreviacion");
            entity.Property(e => e.Fips).HasColumnName("fips");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Hospitalizacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.EstadoId, "estado_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EnUciActualmente).HasColumnName("en_uci_actualmente");
            entity.Property(e => e.EnVentiladorActualmente).HasColumnName("en_ventilador_actualmente");
            entity.Property(e => e.EstadoId).HasColumnName("estado_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HospitalizacionesAcumuladas).HasColumnName("hospitalizaciones_acumuladas");
            entity.Property(e => e.HospitalizadosActualmente).HasColumnName("hospitalizados_actualmente");

            entity.HasOne(d => d.Estado).WithMany(p => p.Hospitalizaciones)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Hospitalizaciones_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasea)
                .HasMaxLength(255)
                .HasColumnName("contrasea");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasColumnType("enum('admin','usuario')")
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
