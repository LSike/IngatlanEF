using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IngatlanEF.Models;

public partial class MiskolcingatlanContext : DbContext
{
    public MiskolcingatlanContext()
    {
    }

    public MiskolcingatlanContext(DbContextOptions<MiskolcingatlanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingatlan> Ingatlans { get; set; }

    public virtual DbSet<Ugyintezo> Ugyintezos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=miskolcingatlan;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingatlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ingatlan");

            entity.HasIndex(e => e.UgyintezoId, "UgyintezoId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cim).HasMaxLength(64);
            entity.Property(e => e.KepNev).HasMaxLength(32);
            entity.Property(e => e.Település).HasMaxLength(64);
            entity.Property(e => e.Tipus).HasMaxLength(32);
            entity.Property(e => e.UgyintezoId).HasColumnType("int(11)");
            entity.Property(e => e.Ár).HasColumnType("int(11)");

            entity.HasOne(d => d.Ugyintezo).WithMany(p => p.Ingatlans)
                .HasForeignKey(d => d.UgyintezoId)
                .HasConstraintName("ingatlan_ibfk_1");
        });

        modelBuilder.Entity<Ugyintezo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ugyintezo");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.Nev).HasMaxLength(32);
            entity.Property(e => e.TelefonSzám).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
