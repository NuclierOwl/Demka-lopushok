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

    public virtual DbSet<PRODUCTMATERIAL_B_IMPORT> PRODUCTMATERIAL_B_IMPORTs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost:5432;User Id=simp;Password=1234;Database=bazaV5");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MATERIALS_SHORT_B_IMPORT>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MATERIALS_SHORT_B_IMPORT", "Demo");

            entity.Property(e => e._ЕДИНИЦА_ИЗМЕРЕНИЯ).HasMaxLength(50);
            entity.Property(e => e._КОЛИЧЕСТВО_В_УПАКОВКЕ).HasMaxLength(50);
            entity.Property(e => e._КОЛИЧЕСТВО_НА_СКЛАДЕ).HasMaxLength(50);
            entity.Property(e => e._МИНИМАЛЬНЫЙ_ВОЗМОЖНЫЙ_ОСТАТОК).HasMaxLength(50);
            entity.Property(e => e._СТОИМОСТЬ).HasMaxLength(50);
            entity.Property(e => e._ТИП_МАТЕРИАЛА).HasMaxLength(50);
            entity.Property(e => e.НАИМЕНОВАНИЕ_МАТЕРИАЛА).HasMaxLength(50);
        });

        modelBuilder.Entity<PRODUCT>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRODUCTS", "Demo");

            entity.Property(e => e.IMAGE).HasMaxLength(50);
            entity.Property(e => e.NAME_PRODUCT).HasMaxLength(50);
            entity.Property(e => e.NOMER_CEHA).HasMaxLength(50);
            entity.Property(e => e.TYPE).HasMaxLength(50);
        });

        modelBuilder.Entity<PRODUCTMATERIAL_B_IMPORT>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRODUCTMATERIAL_B_IMPORT", "Demo");

            entity.Property(e => e.MATIRIAL).HasMaxLength(50);
            entity.Property(e => e.NAME).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
