using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Codecool.EinsenhowerMatrix.Models;

public partial class EisMatrixContext : DbContext
{
    //PM> Scaffold-DbContext "Host=localhost;Database=EisMatrix;Username=eisMatrix_usr;Password=EisEisBaby" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models
    public EisMatrixContext()
    {
    }

    public EisMatrixContext(DbContextOptions<EisMatrixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> Todoitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=EisMatrix;Username=eisMatrix_usr;Password=EisEisBaby");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.Idtodoitem).HasName("todoitems_pkey");

            entity.ToTable("todoitems");

            entity.Property(e => e.Idtodoitem).HasColumnName("idtodoitem");
            entity.Property(e => e.Deadline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.IsDone).HasColumnName("isdone");
            entity.Property(e => e.IsImportant).HasColumnName("isimportant");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
