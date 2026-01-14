using System;
using System.Collections.Generic;
using Lopushok.Hardik.Dao;
using Microsoft.EntityFrameworkCore;

namespace Lopushok.Hardik.Connector;

public partial class dbBaza : DbContext
{
    public dbBaza()
    {
    }

    public dbBaza(DbContextOptions<dbBaza> options)
        : base(options)
    {
    }

    public virtual DbSet<MATERIALS_SHORT_B_IMPORT> MATERIALS_SHORT_B_IMPORTs { get; set; }

    public virtual DbSet<PRODUCT> PRODUCTs { get; set; }

    public virtual DbSet<PRODUCTMATERIAL> PRODUCTMATERIALs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost:5432;User Id=simp;Password=1234;Database=bazaV5");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MATERIALS_SHORT_B_IMPORT>(entity =>
        {
            entity.HasKey(e => e.Material_Name).HasName("materials_short_b_import_pk");

            entity.ToTable("MATERIALS_SHORT_B_IMPORT", "Demo");

            entity.Property(e => e.Material_Name).HasMaxLength(50);
            entity.Property(e => e.Cost).HasMaxLength(50);
            entity.Property(e => e.Izmerenie).HasMaxLength(50);
            entity.Property(e => e.Kolichestvo).HasMaxLength(50);
            entity.Property(e => e.Kolikhestvo_sklad).HasMaxLength(50);
            entity.Property(e => e.Min).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<PRODUCT>(entity =>
        {
            entity.HasKey(e => e.NAME_PRODUCT).HasName("products_pk");

            entity.ToTable("PRODUCTS", "Demo");

            entity.Property(e => e.NAME_PRODUCT).HasMaxLength(50);
            entity.Property(e => e.IMAGE).HasMaxLength(50);
            entity.Property(e => e.NOMER_CEHA).HasMaxLength(50);
            entity.Property(e => e.TYPE).HasMaxLength(50);
        });

        modelBuilder.Entity<PRODUCTMATERIAL>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRODUCTMATERIAL", "Demo");

            entity.Property(e => e.MATIRIAL).HasMaxLength(50);
            entity.Property(e => e.NAME).HasMaxLength(50);

            entity.HasOne(d => d.MATIRIALNavigation).WithMany()
                .HasForeignKey(d => d.MATIRIAL)
                .HasConstraintName("productmaterial_materials_short_b_import_fk");

            entity.HasOne(d => d.NAMENavigation).WithMany()
                .HasForeignKey(d => d.NAME)
                .HasConstraintName("productmaterial_products_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
