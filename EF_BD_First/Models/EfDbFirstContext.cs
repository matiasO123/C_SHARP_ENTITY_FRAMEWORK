using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EF_BD_First.Models;

public partial class EfDbFirstContext : DbContext
{

    public EfDbFirstContext(DbContextOptions<EfDbFirstContext> options)
        : base(options)
    {
    }

    private readonly IConfiguration _configuration;

    /*public EfDbFirstContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }*/



    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("cs"));
        }
    }
    //dotnet ef dbcontext scaffold "server=127.0.0.1; port= 3306; database= ef_db_first; user=root;password=;"  MySql.EntityFrameworkCore -o Models
    //Command to get the info from the database

    // dotnet ef dbcontext scaffold "server=127.0.0.1;port=3306;database=ef_db_first;user=root;password=;" MySql.EntityFrameworkCore -o Models --force
    //Command to update the info from database to code

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.DepartamentoId, "departamento_ID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .HasColumnName("apellido");
            entity.Property(e => e.DepartamentoId)
                .HasColumnType("int(11)")
                .HasColumnName("departamento_ID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("direccion");
            entity.Property(e => e.Edad)
                .HasColumnType("int(11)")
                .HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Personas)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_departamento");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
